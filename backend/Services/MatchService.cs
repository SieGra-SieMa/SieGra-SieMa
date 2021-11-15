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
    public class MatchService : IMatchService
    {
        private readonly SieGraSieMaContext _context;

        public IConfiguration Configuration { get; set; }

        public MatchService(SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IEnumerable<TeamInGroup> AddTeamsToGroup(int tournamentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Team> CheckCorectnessOfTeams(int tournamentId)
        {
            var teams = _context.Teams.Include(t => t.Players)
                                //.Include(t => t.TeamInTournaments)
                                //.ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .ToList();
            if (teams.Count == 0)
            {
                throw new Exception("Bad tournament or no teams register for it");
            }
            var users = new Dictionary<int, int>();
            foreach (var team in teams)
            {
                foreach (var player in team.Players)
                {
                    if (users.ContainsKey(player.UserId))
                    {
                        //users.Add(player.UserId, users[player.UserId]);
                        users[player.UserId]++;
                    }
                    else
                    {
                        users.Add(player.UserId,1);
                    }
                }
            }
            var badTeams = new List<Team>();
            foreach(var user in users)
            {
                if (user.Value > 1)
                {
                    badTeams.AddRange(_context.Teams.Include(t => t.Players)
                                //.Include(t => t.TeamInTournaments)
                                //.ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid == true))
                                .Where(t => t.Players.Any(p => p.UserId == user.Key))
                                .ToList());
                }
            }
            return badTeams;
            //throw new NotImplementedException();
        }

        public int CheckCountPaidTeamsInTournament(int tournamentId)
        {
            var counter = _context.Teams.Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId && tr.Paid==true))
                                .Count();
            return counter;
        }

        public int CheckCountTeamsInTournament(int tournamentId)
        {

            var counter = _context.Teams.Include(t => t.TeamInTournaments)
                                .ThenInclude(tr => tr.Tournament)
                                .Where(t => t.TeamInTournaments.Any(tr => tr.TournamentId == tournamentId))
                                .Count();
            return counter;
        }

        public IEnumerable<Group> ComposeLadderGroups(int tournamentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> CreateBasicGroups(int tournamentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> CreateLadderGroups(int tournamentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> CreateMatchTemplates(int tournamentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamInGroup> CreateTeamTemplatesInLadder(int tournamentId)
        {
            throw new NotImplementedException();
        }

        public Match InsertMatchResult(int matchId)
        {
            throw new NotImplementedException();
        }
    }
}
