using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IMatchService
    {
        public enum MatchesEnum { All, NotPlayed, Played }
        public Task<int> CheckCountTeamsInTournament(int tournamentId);//done
        public Task<int> CheckCountPaidTeamsInTournament(int tournamentId);//done
        public Task<int> CheckCountGroupsInTournament(int tournamentId);//done
        public Task<IEnumerable<Team>> CheckCorectnessOfTeams(int tournamentId);//done
        public Task<IEnumerable<Group>> CreateBasicGroups(int tournamentId);//done
        public Task<IEnumerable<Group>> CreateLadderGroups(int tournamentId, int groupCount);//done
        public Task<IEnumerable<TeamInGroup>> AddTeamsToGroup(int tournamentId);//done
        public Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId);//done
        //public Task<IEnumerable<TeamInGroup>> CreateTeamTemplatesInLadder(int tournamentId);
        public Task<Match> InsertMatchResult(MatchResultDTO matchResultDTO);//przetestować w drabince
        public Task<IEnumerable<GetMatchDTO>> ComposeLadderGroups(int tournamentId);//przetestować w drabince
        public Task<GetAvailableGroupMatchesDTO> GetAvailableGroupMatches(int tournamentId, MatchesEnum matchesEnum);//done
        public Task<GetLadderDTO> GetLadderMatches(int tournamentId);//przetestować w drabince


    }
    public class MatchService : IMatchService
    {
        private readonly SieGraSieMaContext _context;
        public IConfiguration Configuration { get; set; }
        public MatchService(SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public async Task<int> CheckCountPaidTeamsInTournament(int tournamentId)
        {
            var counter = await _context.Teams.Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .CountAsync();
            return counter;
        }
        public async Task<int> CheckCountTeamsInTournament(int tournamentId)
        {
            var counter = await _context.Teams.Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId))
                                .CountAsync();
            return counter;
        }
        public async Task<int> CheckCountGroupsInTournament(int tournamentId)
        {
            var counter = await _context.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id })
                                .CountAsync();
            return counter;
        }
        public async Task<IEnumerable<Team>> CheckCorectnessOfTeams(int tournamentId)
        {
            var teams = await _context.Teams.Include(t => t.Players)
                                //.Include(t => t.TeamInTournaments)
                                //.ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .ToListAsync();
            if (teams.Count == 0)
            {
                throw new Exception("Bad tournament or no teams register for it");
            }
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
                if (user.Value > 1) badTeams.AddRange(await _context.Teams.Include(t => t.Players)
                                    //.Include(t => t.TeamInTournaments)
                                    //.ThenInclude(tr => tr.Tournament)
                                    .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                    .Where(t => t.Players.Any(p => p.UserId == user.Key))
                                    .ToListAsync());
            }
            return badTeams;
        }
        public async Task<IEnumerable<Group>> CreateBasicGroups(int tournamentId)
        {
            //if ((await CheckCorectnessOfTeams(tournamentId)).Count() != 0) throw new Exception("Teams for this tournament are not correct");
            if ((await CheckCorectnessOfTeams(tournamentId)).Any()) throw new Exception("Teams for this tournament are not correct");
            //else if (await _context.Groups.Where(g => g.TournamentId == tournamentId).CountAsync() != 0) throw new Exception("Groups for this tournament already exist");
            else if (_context.Groups.Where(g => g.TournamentId == tournamentId).Any()) throw new Exception("Groups for this tournament already exist");
            else
            {
                var teams = await _context.Teams//.Include(t => t.Players)
                                                //.Include(t => t.TeamInTournaments)
                                                //.ThenInclude(tr => tr.Tournament)
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
                await _context.Groups.AddRangeAsync(groups);
                await _context.SaveChangesAsync();
                groups.AddRange(await CreateLadderGroups(tournamentId, ladderGroupCount));
                return groups;
            }
        }
        public async Task<IEnumerable<Group>> CreateLadderGroups(int tournamentId, int groupCount)
        {
            List<Group> groups = new();
            int counter = 1;
            int lastPhase = 0;
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

            await _context.Groups.AddRangeAsync(groups);
            await _context.SaveChangesAsync();
            return groups;
        }
        public async Task<IEnumerable<TeamInGroup>> AddTeamsToGroup(int tournamentId)
        {
            if (_context.TeamInGroups
                .Include(g => g.Group)
                .Where(g => g.Group.TournamentId == tournamentId)
                .Any()) throw new Exception("TeamsInGroup for this tournament already exist");
            var groups = await _context.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id })
                                .ToListAsync();
            var teams = await _context.Teams
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
            await _context.TeamInGroups.AddRangeAsync(teamInGroups);
            await _context.SaveChangesAsync();
            return teamInGroups;
        }
        public async Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId)
        {
            if (_context.Matches
                .Where(m => m.TournamentId == tournamentId && m.Phase == 0)
                .Any()) throw new Exception("Matches for this tournament already exist");

            var groupsNonLadder = await _context.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id })
                                .ToListAsync();
            List<Match> matchTemplates = new();
            int matchId = 1;
            groupsNonLadder.ForEach(group =>//tworze mecze dla rozgrywek grupowych - szablon meczu z wpisanym zespołem
            {
                var teamsInGroups = _context.TeamInGroups
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
            var groupsLadder = await _context.Groups
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


            /*int phases = (_context.TeamInTournaments.Where(t => t.TournamentId == tournamentId && t.Paid == true).Count()) switch
            {
                < 2 => throw new Exception("Error while getting numbers of teams in tournament"),
                < 8 => 3,
                < 37 => 4,
                < 200 => 5,
                _ => throw new Exception("Error while getting numbers of teams in tournament")
            };*/
            /*
            int matches = 1;
            for (int phase = phases; phase >= 1; phase--)
            {
                for (int match = 1; match <= matches; match++)
                {
                    matchTemplates.Add(new Match
                    {
                        Phase = phase,
                        TournamentId = tournamentId,
                        MatchId = match
                    });
                }
                matches *= 2;
                if (phase == phases) matches--;
            }*/
            await _context.TeamInGroups.AddRangeAsync(teamInGroups);
            await _context.Matches.AddRangeAsync(matchTemplates);
            await _context.SaveChangesAsync();

            return matchTemplates;
        }
        public async Task<Match> InsertMatchResult(MatchResultDTO DTO)
        {
            var match = _context.Matches.Find(DTO.TournamentId, DTO.Phase, DTO.MatchId);
            if (match == null) throw new Exception("No match found with this PKs");
            if (match.TeamHomeId == null || match.TeamAwayId == null) throw new Exception("Match has no teams");
            match.TeamHomeScore = DTO.HomeTeamPoints;
            match.TeamAwayScore = DTO.AwayTeamPoints;

            if (DTO.Phase != 0)//jeśli w drabince
            {
                int phases = _context.Groups.Where(g => g.TournamentId == DTO.TournamentId).Select(g => g.Phase).Max();
                if (DTO.Phase < phases - 1)//jeśli nie finał i nie 3 miejsce
                {
                    _context.Entry(match).Reference(m => m.TeamHome).Load();
                    _context.Entry(match).Reference(m => m.TeamAway).Load();
                    _context.Entry(match.TeamHome).Reference(tt => tt.Team).Load();
                    _context.Entry(match.TeamAway).Reference(tt => tt.Team).Load();
                    Team winner = DTO.HomeTeamPoints > DTO.AwayTeamPoints ? match.TeamHome.Team : match.TeamAway.Team;
                    /*Team winner = _context.Teams.Include(t => t.TeamInGroups)
                            .ThenInclude(t => DTO.HomeTeamPoints > DTO.AwayTeamPoints ? t.MatchTeamHomes : t.MatchTeamAways)
                            .Where(t => t.TeamInGroups.Any(tt => DTO.HomeTeamPoints > DTO.AwayTeamPoints ? tt.MatchTeamHomes.Contains(match) : tt.MatchTeamAways.Contains(match)))
                            .FirstOrDefault();*/

                    IQueryable<Match> query = _context.Matches.Include(m => m.TeamHome).Include(m => m.TeamAway)
                                        .Where(m => m.TournamentId == DTO.TournamentId);
                    if (DTO.Phase < phases - 2)//jeśli wcześniej niż półfinał
                    {
                        query = query.Where(m => m.Phase == DTO.Phase + 1 && m.MatchId == (int)Math.Ceiling(DTO.MatchId / 2.0));
                    }
                    else if (DTO.Phase == phases - 2)//jeśli półfinał
                    {
                        query = query.Where(m => m.Phase == DTO.Phase + 2 && m.MatchId == 1);
                    }

                    Match nextMatch = query.FirstOrDefault();
                    if (nextMatch.TeamHome.TeamId == null) nextMatch.TeamHome.Team = winner;
                    else if (nextMatch.TeamAway.TeamId == null) nextMatch.TeamAway.Team = winner;
                    /*nextMatch.TeamHome.Team = null;
                    nextMatch.TeamAway.Team = null;*/
                    //else throw new Exception("Trying update next match, which already have both teams");
                    //TODO: Zastanowić się nad końcówką pozwalającą ręcznie zaktualizować zespoły na wypadek błędu podczas wpisywania
                    _context.Matches.Update(nextMatch);

                    if (DTO.Phase == phases - 2)//jeśli półfinał
                    {
                        Match thirdPlace = _context.Matches.Include(m => m.TeamHome).Include(m => m.TeamAway)
                                        .Where(m => m.TournamentId == DTO.TournamentId && m.Phase == DTO.Phase + 1 && m.MatchId == 1)
                                        .FirstOrDefault();
                        //_context.Matches.Find(DTO.TournamentId, DTO.Phase + 1, 1)
                        //Team looser;
                        Team looser = DTO.HomeTeamPoints < DTO.AwayTeamPoints ? match.TeamHome.Team : match.TeamAway.Team;
                        /*Team looser = _context.Teams.Include(t => t.TeamInGroups)
                            .ThenInclude(tt => (DTO.HomeTeamPoints < DTO.AwayTeamPoints ? tt.MatchTeamHomes : tt.MatchTeamAways))
                            .Where(t => t.TeamInGroups.Any(tt => (DTO.HomeTeamPoints < DTO.AwayTeamPoints ? tt.MatchTeamHomes.Contains(match) : tt.MatchTeamAways.Contains(match))))
                            .FirstOrDefault();*/
                        /*if (DTO.HomeTeamPoints < DTO.AwayTeamPoints)
                        {
                            *//*looser = _context.TeamInGroups.Include(t => t.MatchTeamHomes).Include(t => t.Team)
                                .Where(t => t.MatchTeamHomes == match).Select(t => t.Team).FirstOrDefault();*//*
                            looser = qlooser.Where(t => t.TeamInGroups.Any(tt => tt.MatchTeamHomes == match)).FirstOrDefault();
                        }
                        else
                        {
                            *//*looser = _context.TeamInGroups.Include(t => t.MatchTeamAways).Include(t => t.Team)
                                .Where(t => t.MatchTeamAways == match).Select(t => t.Team).FirstOrDefault();*//*
                        }*/
                        if (thirdPlace.TeamHome.TeamId == null) thirdPlace.TeamHome.Team = looser;
                        else if (thirdPlace.TeamAway.TeamId == null) thirdPlace.TeamAway.Team = looser;
                        /*thirdPlace.TeamHome.Team = null;
                        thirdPlace.TeamAway.Team = null;*/
                        //else throw new Exception("Trying update next match, which already have both teams");
                        //TODO: Zastanowić się nad końcówką pozwalającą ręcznie zaktualizować zespoły na wypadek błędu podczas wpisywania
                        _context.Matches.Update(thirdPlace);
                    }
                }
            }
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
            return match;
        }
        public async Task<GetAvailableGroupMatchesDTO> GetAvailableGroupMatches(int tournamentId, IMatchService.MatchesEnum matchesEnum = IMatchService.MatchesEnum.All)
        {
            List<GetGroupsMatchesDTO> Groups = await _context.Groups.Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                            .Select(s => new GetGroupsMatchesDTO() { GroupName = s.Name, GroupId = s.Id })
                            .ToListAsync();
            Groups.ForEach(group =>
            {
                IQueryable<Match> query = _context.Matches.Include(m => m.TeamAway).ThenInclude(t => t.Team).Include(m => m.TeamHome).ThenInclude(t => t.Team)
                            .Where(m => m.TeamAway.GroupId == group.GroupId || m.TeamHome.GroupId == group.GroupId);
                switch (matchesEnum)
                {
                    case IMatchService.MatchesEnum.NotPlayed:
                        query = query.Where(m => m.TeamAwayScore == null && m.TeamHomeScore == null);
                        break;
                    case IMatchService.MatchesEnum.Played:
                        query = query.Where(m => m.TeamAwayScore != null && m.TeamHomeScore != null);
                        break;
                    case IMatchService.MatchesEnum.All:
                        break;
                }
                //List<GetMatchDTO> Matches = query.Select(m => new GetMatchDTO()
                var Matches = query.Select(m => new GetMatchDTO()
                {
                    TournamentId = m.TournamentId,
                    Phase = m.Phase,
                    MatchId = m.MatchId,
                    TeamHome = new DTOs.TeamDTO() { Name = m.TeamHome.Team.Name },
                    TeamAway = new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name },
                    TeamHomeScore = m.TeamHomeScore,
                    TeamAwayScore = m.TeamAwayScore
                }).ToListAsync();

                /*List<GetMatchDTO> Matches = _context.Matches.Include(m => m.TeamAway).Include(m => m.TeamHome)
                            .Where(m => m.TeamAway.GroupId == group.GroupId || m.TeamHome.GroupId == group.GroupId)
                            .Select(m => new GetMatchDTO() {TeamHome= new DTOs.TeamDTO() {Name= m.TeamHome.Team.Name },TeamAway= new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name } })
                            .ToList();*/
                group.Matches = Matches.Result;
            });

            GetAvailableGroupMatchesDTO result = new() { GroupsMatches = Groups };
            return result;
        }
        public async Task<GetLadderDTO> GetLadderMatches(int tournamentId)
        {
            List<GetPhasesWithMatchesDTO> Phases = await _context.Matches.GroupBy(m => m.Phase)
                            .Where(m => m.Key != 0)
                            .Select(m => new GetPhasesWithMatchesDTO() { Phase = m.Key })
                            .ToListAsync();
            Phases.ForEach(phase =>
            {
                phase.Matches = _context.Matches.Include(m => m.TeamAway).ThenInclude(t => t.Team)
                                .Include(m => m.TeamHome).ThenInclude(t => t.Team)
                                .Where(m => m.Phase == phase.Phase)
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
            });
            GetLadderDTO result = new()
            {
                Phases = Phases
            };
            return result;

        }
        public async Task<IEnumerable<GetMatchDTO>> ComposeLadderGroups(int tournamentId)
        {
            List<Group> Groups = await _context.Groups.Include(g => g.TeamInGroups).ThenInclude(t => t.Team)
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
                    var matches = _context.Matches.Where(m => (m.TeamAwayId == t.Id || m.TeamHomeId == t.Id)).ToList();
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

            //list = list.OrderBy(l => l.group).ToList();//sortujemy według grup żeby się nie spotkały //zakomentowane bo liczę że są w odpowiedniej kolejności

            
            int[] order = list.Count switch
            {
                4 => new int[] { 1, 4, 3, 2 },
                8 => new int[] { 1, 8, 5, 4, 6, 3, 7, 2 },
                16 => new int[] { 1, 16, 8, 9, 5, 12, 4, 13, 6, 11, 3, 14, 7, 10, 2, 15 },
                _ => throw new Exception("Incorrect teams to ladder number")
            };

            var matches = _context.Matches.Include(m => m.TeamAway)
                                          .Include(m => m.TeamHome)
                                          .Where(m => m.Phase == 1 && m.TournamentId == tournamentId)
                                          .OrderBy(m => m.MatchId).ToList();
            for (int i = 0; i < list.Count / 2; i++)
            {
                matches[i].TeamHome.Team = list[i].team;
                matches[i].TeamAway.Team = list[list.Count - 1 - i].team;
            }
            _context.UpdateRange(matches);
            _context.SaveChanges();
            var result = _context.Matches.Include(m => m.TeamAway).ThenInclude(t => t.Team)
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
        /*public async Task<IEnumerable<TeamInGroup>> CreateTeamTemplatesInLadder(int tournamentId)
        {
            throw new NotImplementedException();
        }*/



    }
}
