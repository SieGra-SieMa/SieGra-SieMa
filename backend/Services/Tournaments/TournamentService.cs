using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.GroupDTO;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.DTOs.TeamInTournamentDTO;
using SieGraSieMa.DTOs.TeamsDTO;
using SieGraSieMa.DTOs.TournamentDTO;
using SieGraSieMa.Models;
using static SieGraSieMa.Services.Tournaments.ITournamentsService;

namespace SieGraSieMa.Services.Tournaments
{
    public interface ITournamentsService
    {
        public Task<IEnumerable<TournamentListDTO>> GetTournaments();

        public Task<ResponseTournamentDTO> GetTournament(int id);

        public Task<TournamentListDTO> CreateTournament(Tournament tournament);

        public Task<ResponseTournamentDTO> UpdateTournament(int id, Tournament tournament);

        public Task<bool> DeleteTournament(int id);
        public enum TeamsEnum { All, Paid }
        public Task<int> CheckCountTeamsInTournament(int tournamentId, TeamsEnum teamsEnum);//do wyświetlania ilości teamów zapisanych i opłaconych w turnieju
        public Task<int> CheckCountGroupsInTournament(int tournamentId);//do sprawdzenia czy zostały już utworzone grupy dla turnieju
        public Task<IEnumerable<Team>> CheckCorectnessOfTeams(int tournamentId);//do sprawdzenia czy w turnieju nie ma zapisanej jednej osoby w dwóch różnych teamach
        public Task<IEnumerable<Group>> CreateBasicGroups(int tournamentId);//do stworzenia grup fazy grupowej
        public Task<IEnumerable<Group>> CreateLadderGroups(int tournamentId, int groupCount);//do stworzenia grup fazy drabinkowej
        public Task<IEnumerable<TeamInGroup>> AddTeamsToGroup(int tournamentId);//do dodania zespołów do grup turniejowych
        public Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId);//do stworzenia wszystkich meczy oraz "teamInGroup" dla wszystkich meczy drabinkowych
        public Task<IEnumerable<GetMatchDTO>> ComposeLadderGroups(int tournamentId);//zbiera najlepsze teamy i wypełnia nimi drabinkę
        public Task<bool> CheckUsersInTeam(List<User> users, int tournamentId);
        public Task<bool> AddTeamToTournament(int teamId, int tournamentId);
        public List<ResponseTeamScoresDTO> GetTeamScoresInGroups(int tournamentId, int groupId);


    }
    public class TournamentService : ITournamentsService
    {

        private readonly SieGraSieMaContext _SieGraSieMaContext;
        private readonly IMatchService _matchService;

        public TournamentService(SieGraSieMaContext SieGraSieMaContext, IMatchService matchService)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
            _matchService = matchService;
        }
        public async Task<TournamentListDTO> CreateTournament(Tournament tournament)
        {
            await _SieGraSieMaContext.Tournaments.AddAsync(tournament);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return new TournamentListDTO { Id = tournament.Id, Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };

            throw new Exception("Unable to add tournament");
        }
        public async Task<bool> DeleteTournament(int id)
        {
            var tournament = await _SieGraSieMaContext.Tournaments.FindAsync(id);
            _SieGraSieMaContext.Tournaments.Remove(tournament);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }
        public async Task<ResponseTournamentDTO> GetTournament(int id)
        {
            GetLadderDTO ladder = await _matchService.GetLadderMatches(id);

            var tournament = await _SieGraSieMaContext.Tournaments
                .Include(t => t.TeamInTournaments)
                .ThenInclude(t => t.Team)
                .Include(t => t.Groups)
                .Include(t => t.Contests)
                .Include(t => t.Albums)
                .Where(t => t.Id == id)
                .Select(t => new ResponseTournamentDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description,
                    Address = t.Address,
                    Albums = t.Albums.Select(a => new ResponseAlbumDTO
                    {
                        Id = a.Id,
                        Name = a.Name,
                        CreateDate = a.CreateDate,
                        TournamentId = a.TournamentId,
                        Media = a.Media.Select(m => new ResponseMediumDTO
                        {
                            Id = m.Id,
                            AlbumId = m.AlbumId,
                            Url = m.Url
                        })
                    }),
                    Groups = t.Groups.Where(g => g.Ladder == false).Select(g => new ResponseGroupDTO
                    {
                        Id = g.Id,
                        Name = g.Name,
                        TournamentId = g.TournamentId,
                        //Teams = GetTeamScoresInGroups(g.TournamentId,g.Id)
                        //TODO team punktacja
                    }),
                    Ladder = ladder
                    //Teams = t.TeamInTournaments.Select(t => t.Team.Name)
                    //TeamInTournaments = t.TeamInTournaments.Select(i => new ResponseTeamInTournamentDTO { TeamId = i.TeamId, TournamentId = i.TournamentId, Paid = i.Paid })
                })
                .FirstOrDefaultAsync();
            tournament.Groups.ToList().ForEach(g =>
            {
                g.Teams = GetTeamScoresInGroups(g.TournamentId, g.Id);
            });

            return tournament;
        }
        public async Task<IEnumerable<TournamentListDTO>> GetTournaments()
        {
            var tournaments = await _SieGraSieMaContext.Tournaments
                .Include(t => t.TeamInTournaments)
                .Include(t => t.Groups)
                .Include(t => t.Contests)
                .Include(t => t.Albums)
                .Select(t => new TournamentListDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description,
                    Address = t.Address
                })
                .ToListAsync();

            return tournaments;
        }
        public async Task<ResponseTournamentDTO> UpdateTournament(int id, Tournament tournament)
        {
            var oldTournament = await _SieGraSieMaContext.Tournaments.FindAsync(id);
            if (oldTournament == null)
                throw new Exception("Tournament not found");
            oldTournament.Name = tournament.Name;
            oldTournament.StartDate = tournament.StartDate;
            oldTournament.EndDate = tournament.EndDate;
            oldTournament.Description = tournament.Description;
            oldTournament.Address = tournament.Address;
            _SieGraSieMaContext.Tournaments.Update(oldTournament);
            await _SieGraSieMaContext.SaveChangesAsync();
            //TODO how to change this?
            return await GetTournament(oldTournament.Id);

        }
        public async Task<int> CheckCountTeamsInTournament(int tournamentId, TeamsEnum teamsEnum)
        {
            IQueryable<Team> query = _SieGraSieMaContext.Teams.Include(t => t.TeamInTournaments).ThenInclude(tr => tr.Tournament)
                            .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId));
            if (teamsEnum == TeamsEnum.Paid) query = query.Where(t => t.TeamInTournaments.Any(tr => tr.Paid == true));
            return await query.CountAsync();
        }
        public async Task<int> CheckCountGroupsInTournament(int tournamentId)
        {
            return await _SieGraSieMaContext.Groups.Where(t => t.TournamentId == tournamentId)
                                .Select(s => new { s.Id }).CountAsync();
        }
        public async Task<IEnumerable<Team>> CheckCorectnessOfTeams(int tournamentId)
        {
            var teams = await _SieGraSieMaContext.Teams.Include(t => t.Players)
                                .Include(t => t.TeamInTournaments)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .ToListAsync();
            if (teams.Count == 0) throw new Exception("Bad tournament or no teams register for it");
            var users = new Dictionary<int, int>();
            teams.ForEach(team =>
            {
                foreach (var player in team.Players)
                {
                    if (users.ContainsKey(player.UserId)) users[player.UserId]++;
                    else users.Add(player.UserId, 1);
                }
            });
            var badTeams = new List<Team>();
            foreach (var user in users)
            {
                if (user.Value > 1) badTeams.AddRange(await _SieGraSieMaContext.Teams.Include(t => t.Players)
                                    .Include(t => t.TeamInTournaments)
                                    .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true)
                                                && t.Players.Any(p => p.UserId == user.Key))
                                    .ToListAsync());
            }
            return badTeams;
        }
        public async Task<IEnumerable<Group>> CreateBasicGroups(int tournamentId)
        {
            if ((await CheckCorectnessOfTeams(tournamentId)).Any()) throw new Exception("Teams for this tournament are not correct");
            else if (_SieGraSieMaContext.Groups.Where(g => g.TournamentId == tournamentId).Any()) throw new Exception("Groups for this tournament already exist");
            else
            {
                var teams = await _SieGraSieMaContext.Teams.Include(t => t.TeamInTournaments)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .ToListAsync();
                if (teams.Count == 0) throw new Exception("There is no teams for this tournament");
                List<Group> groups = new();
                char nonLadderGroupCount; //oznacza ilość grup niedrabinkowych
                int ladderGroupCount; //oznacza ilość grup w drabince

                switch (teams.Count)
                {
                    case < 4:
                        throw new Exception("Not enough teams to start a tournament");
                    case < 8://wzór pseudodrabinki, gdzie w każdej grupie są 2 lub 1 zespół
                        nonLadderGroupCount = 'D';
                        ladderGroupCount = 4;
                        break;
                    case < 16://wzór pseudodrabinki, gdzie w każdej grupie są 2 lub 1 zespół
                        nonLadderGroupCount = 'H';
                        ladderGroupCount = 8;
                        break;
                    case < 25://prawdziwe grupy, minimum 4 zespoły w 4 grupach
                        nonLadderGroupCount = 'D';
                        ladderGroupCount = 8;
                        break;
                    case < 37://prawdziwe grupy, minimum 4 zespoły w 6 grupach + 2 zespoły lucky loosers
                        nonLadderGroupCount = 'F';
                        ladderGroupCount = 16;
                        break;
                    case < 200://prawdziwe grupy, minimum 4 zespoły w 8 grupach
                        nonLadderGroupCount = 'H';
                        ladderGroupCount = 16;
                        break;
                    default:
                        throw new Exception("Too much teams");
                }

                for (char i = 'A'; i <= nonLadderGroupCount; i++)
                {
                    groups.Add(new Group()
                    {
                        Name = i.ToString(),
                        TournamentId = tournamentId,
                        Ladder = false,
                        Phase = 0
                    });
                }
                await _SieGraSieMaContext.Groups.AddRangeAsync(groups);
                await _SieGraSieMaContext.SaveChangesAsync();
                groups.AddRange(await CreateLadderGroups(tournamentId, ladderGroupCount));
                return groups;
            }
        }
        public async Task<IEnumerable<Group>> CreateLadderGroups(int tournamentId, int groupCount)
        {
            List<Group> groups = new();
            int counter = 1, lastPhase = 0;
            for (int phase = 1; Math.Pow(2, phase) < groupCount; phase++)
            {
                for (int i = 0; i < groupCount / phase / 2; i++)
                {
                    groups.Add(new Group()
                    {
                        Name = counter.ToString(),
                        TournamentId = tournamentId,
                        Ladder = true,
                        Phase = phase
                    });
                    counter++;
                }
                lastPhase = phase;
            }

            groups.Add(new Group() //finaliści
            {
                Name = counter.ToString(),
                TournamentId = tournamentId,
                Ladder = true,
                Phase = lastPhase + 2
            });

            counter++;
            groups.Add(new Group() //o 3 miejsce
            {
                Name = counter.ToString(),
                TournamentId = tournamentId,
                Ladder = true,
                Phase = lastPhase + 1
            });

            await _SieGraSieMaContext.Groups.AddRangeAsync(groups);
            await _SieGraSieMaContext.SaveChangesAsync();
            return groups;
        }
        public async Task<IEnumerable<TeamInGroup>> AddTeamsToGroup(int tournamentId)
        {
            if (_SieGraSieMaContext.TeamInGroups.Include(g => g.Group).Where(g => g.Group.TournamentId == tournamentId).Any()) throw new Exception("TeamsInGroup for this tournament already exist");
            var groups = await _SieGraSieMaContext.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id })
                                .ToListAsync();
            var teams = await _SieGraSieMaContext.Teams
                                .Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .Select(t => new { t.Id })
                                .ToListAsync();
            int currentGroup = 0;
            List<TeamInGroup> teamInGroups = new();
            int groupcount = groups.Count;
            foreach (var team in teams)
            {
                if (currentGroup >= groupcount) currentGroup = 0;
                teamInGroups.Add(new TeamInGroup
                {
                    GroupId = groups[currentGroup].Id,
                    TeamId = team.Id
                });
                currentGroup++;
            }
            await _SieGraSieMaContext.TeamInGroups.AddRangeAsync(teamInGroups);
            await _SieGraSieMaContext.SaveChangesAsync();
            return teamInGroups;
        }
        public async Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId)
        {
            if (_SieGraSieMaContext.Matches.Where(m => m.TournamentId == tournamentId && m.Phase == 0).Any())
                throw new Exception("Matches for this tournament already exist");
            var groupsNonLadder = await _SieGraSieMaContext.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id }).ToListAsync();
            List<Match> matchTemplates = new();
            int matchId = 1;
            groupsNonLadder.ForEach(group =>//tworze mecze dla rozgrywek grupowych - szablon meczu z wpisanym zespołem
            {
                var teamsInGroups = _SieGraSieMaContext.TeamInGroups
                                .Where(t => t.GroupId == group.Id)
                                .Select(t => new { t.Id })
                                .ToList();
                for (int i = 0; i < teamsInGroups.Count - 1; i++)
                {
                    for (int j = i + 1; j < teamsInGroups.Count; j++)
                    {
                        matchTemplates.Add(new Match
                        {
                            TeamHomeId = teamsInGroups[i].Id,
                            TeamAwayId = teamsInGroups[j].Id,
                            Phase = 0,
                            TournamentId = tournamentId,
                            MatchId = matchId,
                        });
                        matchId++;
                    }
                }
            });
            var groupsLadder = await _SieGraSieMaContext.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == true)
                                .OrderBy(g => g.Name)
                                .ToListAsync();
            int phases = groupsLadder.Select(g => g.Phase).Max();
            List<TeamInGroup> teamInGroups = new();
            for (int i = 1; i <= phases; i++)
            {
                int matches = 1;
                groupsLadder.Where(g => g.Phase == i).ToList().ForEach(g =>
                {
                    TeamInGroup homes = new TeamInGroup
                    {
                        Group = g
                    };
                    teamInGroups.Add(homes);
                    TeamInGroup aways = new TeamInGroup
                    {
                        Group = g
                    };
                    teamInGroups.Add(aways);
                    matchTemplates.Add(new Match
                    {
                        Phase = g.Phase,
                        TournamentId = tournamentId,
                        MatchId = matches++,
                        TeamHome = homes,
                        TeamAway = aways
                    });
                });
            }
            await _SieGraSieMaContext.TeamInGroups.AddRangeAsync(teamInGroups);
            await _SieGraSieMaContext.Matches.AddRangeAsync(matchTemplates);
            await _SieGraSieMaContext.SaveChangesAsync();

            return matchTemplates;
        }
        public async Task<IEnumerable<GetMatchDTO>> ComposeLadderGroups(int tournamentId)
        {
            List<Group> Groups = await _SieGraSieMaContext.Groups.Include(g => g.TeamInGroups).ThenInclude(t => t.Team)
                .Where(t => t.TournamentId == tournamentId && t.Ladder == false).OrderBy(g => g.Name).ToListAsync();

            var list = new List<(Team team, int points, int sumOfScores, string group)>(); //docelowa lista drużyn do drabinki
            var luckyLoosersList = new List<(Team team, int points, int sumOfScores, string group)>(); //zapasowa lista gdybyśmy potrzebowali lucky loosers
            int howMuchWeTakeToLadder = Groups.Select(g => g.TeamInGroups.Count).Sum() < 16 ? 1 : 2; //w przypadku pseudogrup (<16) bierzemy jedynego wygranego
            Groups.ForEach(g =>
            {
                var groupList = new List<(Team team, int points, int sumOfScores, string group)>();//dla każdej grupy tworzymy oddzielną listę teamów
                g.TeamInGroups.ToList().ForEach(t =>
                {
                    int points = 0;
                    int sumOfScores = 0;
                    var matches = _SieGraSieMaContext.Matches.Where(m => (m.TeamAwayId == t.Id || m.TeamHomeId == t.Id)).ToList();
                    matches.ForEach(m =>//dla każdego teamu wyliczamy jego statystyki
                    {
                        if (m.TeamHomeScore == m.TeamAwayScore) points += 1;
                        else if ((m.TeamHomeId.Value == t.Id && m.TeamHomeScore.Value > m.TeamAwayScore.Value) ||
                                 (m.TeamAwayId.Value == t.Id && m.TeamHomeScore.Value < m.TeamAwayScore.Value))
                        {
                            points += 3;
                            sumOfScores += Math.Abs(m.TeamHomeScore.GetValueOrDefault() - m.TeamAwayScore.GetValueOrDefault());
                        }
                        //else throw new Exception("Error while searching matches for team");
                    });
                    groupList.Add(new() { team = t.Team, points = points, sumOfScores = sumOfScores, group = g.Name });
                });
                groupList = groupList.OrderByDescending(l => l.points).ThenByDescending(l => l.sumOfScores).ToList();
                list.AddRange(groupList.Take(howMuchWeTakeToLadder));//bierzemy do drabinki tylko tyle ile potrzebujemy
                luckyLoosersList.AddRange(groupList.Skip(howMuchWeTakeToLadder));//resztę wrzucamy do zapasowej listy
            });
            if (Groups.Count == 6)
            {
                luckyLoosersList = luckyLoosersList.OrderByDescending(l => l.points).ThenByDescending(l => l.sumOfScores).ToList();
                list.AddRange(luckyLoosersList.Take(4));//tutaj korzystamy z zapasowej listy
            }

            int[] order = list.Count switch
            {
                4 => new int[] { 1, 4, 3, 2 },//to nie są losowe numerki :) 
                8 => new int[] { 1, 8, 5, 4, 6, 3, 7, 2 },
                16 => new int[] { 1, 16, 8, 9, 5, 12, 4, 13, 6, 11, 3, 14, 7, 10, 2, 15 },
                _ => throw new Exception("Incorrect teams to ladder number")
            };
            var matches = _SieGraSieMaContext.Matches.Include(m => m.TeamAway)
                                          .Include(m => m.TeamHome)
                                          .Where(m => m.Phase == 1 && m.TournamentId == tournamentId)
                                          .OrderBy(m => m.MatchId).ToList();
            for (int i = 0; i < list.Count / 2; i++)
            {
                matches[i].TeamHome.Team = list[i].team;
                matches[i].TeamAway.Team = list[list.Count - 1 - i].team;
            }
            _SieGraSieMaContext.UpdateRange(matches);
            _SieGraSieMaContext.SaveChanges();
            var result = _SieGraSieMaContext.Matches.Include(m => m.TeamAway).ThenInclude(t => t.Team)
                                .Include(m => m.TeamHome).ThenInclude(t => t.Team)
                                .Where(m => m.Phase == 1)
                                .Select(m => new GetMatchDTO()
                                {
                                    TournamentId = m.TournamentId,
                                    Phase = m.Phase,
                                    MatchId = m.MatchId,
                                    TeamHome = new DTOs.TeamDTO() { Name = m.TeamHome.Team.Name },
                                    TeamAway = new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name },
                                    TeamHomeScore = m.TeamHomeScore,
                                    TeamAwayScore = m.TeamAwayScore
                                }).ToList();
            return result;
        }
        public async Task<bool> CheckUsersInTeam(List<User> users, int tournamentId)
        {
            var emptyList = await _SieGraSieMaContext.Tournaments.Where(t => t.Id == tournamentId)
                .Include(t => t.TeamInTournaments)
                .ThenInclude(t => t.Team)
                .ThenInclude(t => t.Players)
                .Where(p => p.TeamInTournaments.Any(t => t.Team.Players.Any(p => users.Any(u => u == p.User))))
                .ToListAsync();

            if (emptyList.Count == 0)
                return true;

            return false;
        }
        public async Task<bool> AddTeamToTournament(int teamId, int tournamentId)
        {
            var team = await _SieGraSieMaContext.Teams.FindAsync(teamId);
            var tournament = await _SieGraSieMaContext.Tournaments.FindAsync(tournamentId);
            if (team == null && tournament == null)
                return false;

            _SieGraSieMaContext.TeamInTournaments.Add(new TeamInTournament { TeamId = teamId, TournamentId = tournamentId, Paid = false });
            await _SieGraSieMaContext.SaveChangesAsync();
            return true;
        }
        public List<ResponseTeamScoresDTO> GetTeamScoresInGroups(int tournamentId, int groupId)
        {
            List<TeamInGroup> tig = _SieGraSieMaContext.TeamInGroups.Include(t => t.Team).Include(t => t.Group)
                .Where(t => t.Group.TournamentId == tournamentId && t.GroupId == groupId).ToList();
            var list = new List<ResponseTeamScoresDTO>();
            tig.ForEach(t =>
            {
                var team = new ResponseTeamScoresDTO();
                team.Name = t.Team.Name;
                var matches = _SieGraSieMaContext.Matches
                            .Where(m => (m.TeamAwayId == t.Id || m.TeamHomeId == t.Id)
                                    && m.TeamAwayScore != null && m.TeamHomeScore != null)
                            .ToList();
                matches.ForEach(m =>//dla każdego teamu wyliczamy jego statystyki
                {
                    team.PlayedMatches++;
                    if (m.TeamHomeId.Value == t.Id && m.TeamHomeScore.Value > m.TeamAwayScore.Value)//jest home i wygrał
                    {
                        team.WonMatches++;
                        team.Points += 3;
                        team.GoalScored += m.TeamHomeScore.GetValueOrDefault();
                        team.GoalConceded += m.TeamAwayScore.GetValueOrDefault();
                    }
                    else if (m.TeamAwayId.Value == t.Id && m.TeamHomeScore.Value < m.TeamAwayScore.Value)//jest away i wygrał
                    {
                        team.WonMatches++;
                        team.Points += 3;
                        team.GoalScored += m.TeamAwayScore.GetValueOrDefault();
                        team.GoalConceded += m.TeamHomeScore.GetValueOrDefault();
                    }
                    else if (m.TeamHomeScore == m.TeamAwayScore)//remis
                    {
                        team.TiedMatches++;
                        team.Points++;
                        team.GoalScored += m.TeamAwayScore.GetValueOrDefault();
                        team.GoalConceded += m.TeamAwayScore.GetValueOrDefault();
                    }
                    else if (m.TeamHomeId.Value == t.Id && m.TeamHomeScore.Value < m.TeamAwayScore.Value)//jest home i przegrał
                    {
                        team.LostMatches++;
                        team.GoalScored += m.TeamHomeScore.GetValueOrDefault();
                        team.GoalConceded += m.TeamAwayScore.GetValueOrDefault();
                    }
                    else if (m.TeamAwayId.Value == t.Id && m.TeamHomeScore.Value > m.TeamAwayScore.Value)//jest away i przegrał
                    {
                        team.LostMatches++;
                        team.GoalScored += m.TeamHomeScore.GetValueOrDefault();
                        team.GoalConceded += m.TeamAwayScore.GetValueOrDefault();
                    }
                    else throw new Exception("Error while counting results matches for team");
                });
                list.Add(team);
            });
            //TODO Sortowanie listy po ilosci
            return list;
        }
    }
}
