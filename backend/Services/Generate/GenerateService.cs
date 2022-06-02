using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IGenerateService
    {
        public Task<IEnumerable<Team>> GenerateTeams(int amount, int tournamentId);
        public Task<IEnumerable<Match>> GenerateMatchResults(int tournamentId, int phase);
    }
    public class GenerateService : IGenerateService
    {
        private readonly SieGraSieMaContext _context;
        private readonly UserManager<User> _userManager;

        public IConfiguration Configuration { get; set; }

        public GenerateService(UserManager<User> userManager, SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Team>> GenerateTeams(int amount, int tournamentId)
        {
            List<Team> TeamsList = new();
            for (int teamCounter = 0; teamCounter < amount; teamCounter++)
            {
                //tworzenie kapitana
                var emm = RandomString(6) + "@gmail.com";
                var username = RandomString(8);
                var captain = new User
                {
                    Name = RandomString(6),
                    Surname = RandomString(8),
                    UserName = username,
                    Email = emm,
                    NormalizedEmail = emm.ToUpper(),
                    NormalizedUserName = username.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    TwoFactorEnabled = false
                };
                await _userManager.CreateAsync(captain, "bardzoTr00dneH@slo");
                await _userManager.AddToRoleAsync(captain, "User");
                //tworzenie teamu
                var team = new Team { Code = RandomString(5), Captain = captain, Name = RandomString(10) };
                //dodawanie graczy do teamu
                team.Players.Add(new Player { Team = team, User = captain });
                for (int player = 1; player < 5; player++)
                {
                    emm = RandomString(6) + "@gmail.com";
                    username = RandomString(8);
                    var user = new User
                    {
                        Name = RandomString(6),
                        Surname = RandomString(8),
                        UserName = username,
                        Email = emm,
                        NormalizedEmail = emm.ToUpper(),
                        NormalizedUserName = username.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        TwoFactorEnabled = false
                    };
                    await _userManager.CreateAsync(user, "bardzoTr00dneH@slo");
                    await _userManager.AddToRoleAsync(user, "User");
                    team.Players.Add(new Player() { Team = team, User = user });
                }
                TeamsList.Add(team);
                team.TeamInTournaments.Add(new TeamInTournament { Team = team, TournamentId = tournamentId, Paid = true });

            }
            _context.Teams.AddRange(TeamsList);
            await _context.SaveChangesAsync();
            return TeamsList;
        }
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)])
                                                    .ToArray());
        }

        public async Task<IEnumerable<Match>> GenerateMatchResults(int tournamentId, int phase)
        {
            Random r = new();
            List<Match> Matches = _context.Matches
                .Where(m => m.TournamentId == tournamentId
                        && m.TeamAwayScore == null
                        && m.TeamHomeScore == null
                        && m.Phase == phase).ToList();
            Matches.ForEach(m =>
            {
                m.TeamHomeScore = r.Next(0, 40);
                do m.TeamAwayScore = r.Next(0, 40); while (m.TeamHomeScore == m.TeamAwayScore);
            });
            _context.Matches.UpdateRange(Matches);
            await _context.SaveChangesAsync();
            return Matches.Select(m=>new Match {TournamentId=m.TournamentId,Phase=m.Phase, MatchId=m.MatchId});
        }
    }
}
