using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public Task<int> CheckCountTeamsInTournament(int tournamentId);
        public Task<int> CheckCountPaidTeamsInTournament(int tournamentId);
        public Task<IEnumerable<Team>> CheckCorectnessOfTeams(int tournamentId);
        public Task<IEnumerable<Group>> CreateBasicGroups(int tournamentId);
        public Task<IEnumerable<Group>> CreateLadderGroups(int tournamentId, int groupCount);
        public Task<IEnumerable<TeamInGroup>> AddTeamsToGroup(int tournamentId);
        public Task<IEnumerable<TeamInGroup>> CreateTeamTemplatesInLadder(int tournamentId);
        public Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId);
        public Task<Match> InsertMatchResult(int matchId);
        public Task<IEnumerable<Group>> ComposeLadderGroups(int tournamentId);


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
            //foreach (var team in teams)
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
            if ((await CheckCorectnessOfTeams(tournamentId)).Count() != 0) throw new Exception("Teams for this tournament are not correct");
            else if (await _context.Groups.Where(g => g.TournamentId == tournamentId).CountAsync() != 0) throw new Exception("Groups for this tournament already exist");
            else
            {
                var teams = await _context.Teams.Include(t => t.Players)
                                //.Include(t => t.TeamInTournaments)
                                //.ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .ToListAsync();
                if (teams.Count() == 0) throw new Exception("There is no teams for this tournament");
                
                List<Group> groups = new List<Group>();
                char j; //oznacza ilość grup niedrabinkowych
                int k; //oznacza ilość grup w drabince
                if (teams.Count() < 16) //wzór pseudodrabinki, gdzie w każdej grupie są 2 lub 1 zespół
                {
                    if (teams.Count() < 8)
                    {
                        j = 'D';
                        k = 2;
                    }
                    else
                    {
                        j = 'H';
                        k = 4;
                    }
                }
                else if (teams.Count() < 24) //prawdziwe grupy, minimum 4 zespoły w 4 grupach
                {
                    j = 'D';
                    k = 4;
                }
                else if (teams.Count() < 32) //prawdziwe grupy, minimum 4 zespoły w 6 grupach
                {
                    j = 'F';
                    k = 8;
                }
                else //prawdziwe grupy, minimum 4 zespoły w 8 grupach
                {
                    j = 'H';
                    k = 8;
                }

                for (char i = 'A'; i <= j; i++)
                {
                    groups.Add(new Group()
                    {
                        Name = i.ToString(),
                        TournamentId = tournamentId,
                        Ladder = false
                    });
                }
                _context.Groups.AddRange(groups);
                _context.SaveChanges();
                groups.AddRange(await CreateLadderGroups(tournamentId, k));
                return groups;
            }
        }
        public async Task<IEnumerable<Group>> CreateLadderGroups(int tournamentId, int groupCount)
        {
            List<Group> groups = new List<Group>();
            int counter = 0;
            for (int level = 1; Math.Pow(2, level) <= groupCount; level++)
            {
                for (int i = 0; i < groupCount / level; i++)
                {
                    groups.Add(new Group()
                    //_context.Groups.Add(new Group()
                    {
                        Name = counter.ToString(),
                        TournamentId = tournamentId,
                        Ladder = true
                    });
                    counter++;
                    //_context.SaveChanges();
                }
            }

            groups.Add(new Group() //finaliści
            //_context.Groups.Add(new Group()
            {
                Name = counter.ToString(),
                TournamentId = tournamentId,
                Ladder = true
            });
            //_context.SaveChanges();

            counter++;
            groups.Add(new Group() //o 3 miejsce
            //_context.Groups.Add(new Group()
            {
                Name = counter.ToString(),
                TournamentId = tournamentId,
                Ladder = true
            });

            _context.Groups.AddRange(groups);
            _context.SaveChanges();
            return groups;
            //return null;
        }
        public async Task<IEnumerable<TeamInGroup>> AddTeamsToGroup(int tournamentId)
        {
            var groups = _context.Groups
                                .Where(t => t.TournamentId == tournamentId && t.Ladder == false)
                                .Select(s => new { Id = s.Id })
                                .ToListAsync();
            var teams = _context.Teams
                                .Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .Select(t => new { Id = t.Id })
                                .ToListAsync();

            int i = 0;
            List<TeamInGroup> teamInGroups = new List<TeamInGroup>();
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
            _context.SaveChangesAsync();
            return teamInGroups;
        }
        public async Task<IEnumerable<Match>> CreateMatchTemplates(int tournamentId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TeamInGroup>> CreateTeamTemplatesInLadder(int tournamentId)
        {
            throw new NotImplementedException();
        }
        public async Task<Match> InsertMatchResult(int matchId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Group>> ComposeLadderGroups(int tournamentId)
        {
            throw new NotImplementedException();
        }
    }
}
