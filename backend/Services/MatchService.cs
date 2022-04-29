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
        public Task<Match> InsertMatchResult(MatchResultDTO matchResultDTO);//done
        public Task<IEnumerable<Group>> ComposeLadderGroups(int tournamentId);
        public Task<GetAvailableGroupMatchesDTO> GetAvailableGroupMatches(int tournamentId, MatchesEnum matchesEnum);


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
            if (!(await CheckCorectnessOfTeams(tournamentId)).Any()) throw new Exception("Teams for this tournament are not correct");
            //else if (await _context.Groups.Where(g => g.TournamentId == tournamentId).CountAsync() != 0) throw new Exception("Groups for this tournament already exist");
            else if (!_context.Groups.Where(g => g.TournamentId == tournamentId).Any()) throw new Exception("Groups for this tournament already exist");
            else
            {
                var teams = await _context.Teams.Include(t => t.Players)
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

                /*if (teams.Count() < 16) 
                {
                    if (teams.Count() < 8)
                    {
                        nonLadderGroupCount = 'D';
                        ladderGroupCount = 4; //2?
                    }
                    else
                    {
                        nonLadderGroupCount = 'H';
                        ladderGroupCount = 8; //4?
                    }
                }
                else if (teams.Count() < 25) 
                {
                    nonLadderGroupCount = 'D';
                    ladderGroupCount = 8;//4?
                }
                else if (teams.Count() < 37) 
                {
                    nonLadderGroupCount = 'F';
                    ladderGroupCount = 16;//8?
                }
                else 
                {
                    nonLadderGroupCount = 'H';
                    ladderGroupCount = 16;//8?
                }*/

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
            if (!_context.TeamInGroups
                .Include(g => g.Group)
                .Where(g => g.Group.TournamentId == tournamentId)
                .Any()) throw new Exception("TeamsInGroup for this tournament already exist");

            var groups = _context.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id })
                                .ToListAsync();
            var teams = _context.Teams
                                .Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .Select(t => new { t.Id })
                                .ToListAsync();

            int i = 0;
            List<TeamInGroup> teamInGroups = new();
            await groups;
            await teams;
            int groupcount = groups.Result.Count;
            foreach (var team in teams.Result)
            {
                if (i >= groupcount) i = 0;
                teamInGroups.Add(new TeamInGroup
                {
                    GroupId = groups.Result[i].Id,
                    TeamId = team.Id
                });
                i++;
            }
            await _context.TeamInGroups.AddRangeAsync(teamInGroups);
            await _context.SaveChangesAsync();
            return teamInGroups;
        }
        public async Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId)
        {
            var groups = await _context.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { s.Id })
                                .ToListAsync();
            List<Match> matchTemplates = new();
            int matchId = 1;
            groups.ForEach(group =>
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

            int phases = (_context.TeamInTournaments.Where(t => t.TournamentId == tournamentId && t.Paid == true).Count()) switch
            {
                < 2 => throw new Exception("Error while getting numbers of teams in tournament"),
                < 8 => 3,
                < 37 => 4,
                < 200 => 5,
                _ => throw new Exception("Error while getting numbers of teams in tournament")
            };

            /*switch ((_context.TeamInTournaments.Where(t => t.TournamentId == tournamentId && t.Paid == true).Count()))
            {

                case < 2:
                    throw new Exception("Error while getting numbers of teams in tournament");
                case < 8:
                    phases = 3;
                    break;
                case < 37:
                    phases = 4;
                    break;
                case < 200:
                    phases = 5;
                    break;
                default:
                    throw new Exception("Error while getting numbers of teams in tournament");
            }*/

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
            }

            await _context.Matches.AddRangeAsync(matchTemplates);
            await _context.SaveChangesAsync();

            return matchTemplates;
        }
        public async Task<Match> InsertMatchResult(MatchResultDTO matchResultDTO)
        {
            var match = _context.Matches.Find(matchResultDTO.TournamentId, matchResultDTO.Phase, matchResultDTO.MatchId);
            if (match == null) throw new Exception("No match found with this PKs");
            if (match.TeamHomeId == null || match.TeamAwayId == null) throw new Exception("Match has no teams");
            match.TeamHomeScore = matchResultDTO.HomeTeamPoints;
            match.TeamAwayScore = matchResultDTO.AwayTeamPoints;
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
            return match;
        }
        public async Task<IEnumerable<Group>> ComposeLadderGroups(int tournamentId)
        {
            throw new NotImplementedException();
        }
        /*public async Task<IEnumerable<TeamInGroup>> CreateTeamTemplatesInLadder(int tournamentId)
        {
            throw new NotImplementedException();
        }*/



        public async Task<GetAvailableGroupMatchesDTO> GetAvailableGroupMatches(int tournamentId, IMatchService.MatchesEnum matchesEnum = IMatchService.MatchesEnum.All)
        {
            List<GetGroupsMatchesDTO> Groups = await _context.Groups.Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                            .Select(s => new GetGroupsMatchesDTO() { GroupName = s.Name, GroupId = s.Id })
                            .ToListAsync();
            Groups.ForEach(group =>
            {
                IQueryable<Match> query = _context.Matches.Include(m => m.TeamAway).Include(m => m.TeamHome)
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
                    TeamAway = new DTOs.TeamDTO() { Name = m.TeamAway.Team.Name }
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
    }
}
