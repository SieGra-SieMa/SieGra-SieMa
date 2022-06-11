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
        public Task<bool> GenerateMatchResults(int tournamentId, int phase);
        public Task SeedBasicData();
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

        public async Task<bool> GenerateMatchResults(int tournamentId, int phase)
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
            return await _context.SaveChangesAsync() > 0;
            //return Matches.Select(m => new Match { TournamentId = m.TournamentId, Phase = m.Phase, MatchId = m.MatchId });
        }

        public async Task SeedBasicData()
        {
            _context.RemoveRange(_context.Matches.ToList());
            _context.RemoveRange(_context.TeamInGroups.ToList());
            _context.RemoveRange(_context.Groups.ToList());
            _context.RemoveRange(_context.MediumInAlbum.ToList());
            _context.RemoveRange(_context.Media.ToList());
            _context.RemoveRange(_context.Albums.ToList());
            _context.RemoveRange(_context.Contestants.ToList());
            _context.RemoveRange(_context.Contests.ToList());
            _context.RemoveRange(_context.Tournaments.ToList());
            _context.RemoveRange(_context.TeamInTournaments.ToList());
            _context.RemoveRange(_context.Players.ToList());
            _context.RemoveRange(_context.Teams.ToList());
            _context.RemoveRange(_context.Newsletters.ToList());
            _context.RemoveRange(_context.Logs.ToList());
            _context.RemoveRange(_context.RefreshTokens.ToList());
            _context.RemoveRange(_context.Users.ToList());
            _context.SaveChanges();

            var password = "Haslo+123";

            var admin = new User
            {
                Name = "Jasio",
                Surname = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            var result = await _userManager.CreateAsync(admin, password);
            await _userManager.AddToRoleAsync(admin, "Admin");
            await _userManager.AddToRoleAsync(admin, "Emp");
            await _userManager.AddToRoleAsync(admin, "User");

            var pracownik = new User
            {
                Name = "Kuba",
                Surname = "Pracownik",
                UserName = "pracownik@gmail.com",
                Email = "pracownik@gmail.com",
                NormalizedEmail = "pracownik@gmail.com".ToUpper(),
                NormalizedUserName = "pracownik@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            await _userManager.CreateAsync(pracownik, password);
            await _userManager.AddToRoleAsync(pracownik, "Emp");
            await _userManager.AddToRoleAsync(pracownik, "User");

            var kapitan = new User
            {
                Name = "Taras",
                Surname = "Kapitan",
                UserName = "kapitan@gmail.com",
                Email = "kapitan@gmail.com",
                NormalizedEmail = "kapitan@gmail.com".ToUpper(),
                NormalizedUserName = "kapitan@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            await _userManager.CreateAsync(kapitan, password);
            await _userManager.AddToRoleAsync(kapitan, "User");

            var gosc = new User
            {
                Name = "Anon",
                Surname = "Gość",
                UserName = "gosc@gmail.com",
                Email = "gosc@gmail.com",
                NormalizedEmail = "gosc@gmail.com".ToUpper(),
                NormalizedUserName = "gosc@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            await _userManager.CreateAsync(gosc, password);
            await _userManager.AddToRoleAsync(gosc, "User");

            await _context.SaveChangesAsync();

            var teams = new List<Team>() {
            new Team() {Name = "Bogowie", Captain=kapitan, Code = "ABCDE" },
            new Team() {Name = "Demony", Captain=kapitan, Code = "BCDEF" },
            new Team() {Name = "Ancymony", Captain=kapitan, Code = "CDEFG" },
            new Team() {Name = "Gałgany", Captain=kapitan, Code = "DEFGH" },
            new Team() {Name = "Hultaje", Captain=kapitan, Code = "EFGHI" } };
            _context.Teams.AddRange(teams);
            await _context.SaveChangesAsync();

            teams.ForEach(t => t.Players.Add(new Player() { Team = t, User = kapitan }));

            var listaGraczy = new List<User>();
            for (int i = 0; i < 20; i++)
            {
                var gracz = new User
                {
                    Name = "Kubuś",
                    Surname = "Gracz" + i,
                    UserName = "gracz" + i + "@gmail.com",
                    Email = "gracz" + i + "@gmail.com",
                    NormalizedEmail = "gracz" + i + "@gmail.com".ToUpper(),
                    NormalizedUserName = "gracz" + i + "@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = null,
                    TwoFactorEnabled = false
                };
                await _userManager.CreateAsync(gracz, password);
                await _userManager.AddToRoleAsync(gracz, "User");
                teams.ForEach(t => {
                    if(t.Id%5==i%5)t.Players.Add(new Player() { Team = t, User = gracz });
                    });
                listaGraczy.Add(gracz);
            }
            await _context.SaveChangesAsync();
            _context.Newsletters.Add(new Newsletter() { User = kapitan });

            _context.Tournaments.AddRange(new Tournament()
            {
                Name = "Turniej testowy numer 1",
                Address = "Zbożowa -1",
                Description = "Taki tam turniej",
                StartDate = DateTime.Parse("10.06.2022 10:00"),
                EndDate = DateTime.Parse("10.06.2022 20:00")
            },
            new Tournament()
            {
                Name = "Turniej testowy numer 2",
                Address = "Zbożowa -1",
                Description = "Taki tam turniej",
                StartDate = DateTime.Parse("10.06.2022 10:00"),
                EndDate = DateTime.Parse("10.06.2022 20:00")
            },
            new Tournament()
            {
                Name = "Turniej testowy numer 3",
                Address = "Zbożowa -1",
                Description = "Taki tam turniej",
                StartDate = DateTime.Parse("10.06.2022 10:00"),
                EndDate = DateTime.Parse("10.06.2022 20:00")
            },
            new Tournament()
            {
                Name = "Turniej testowy numer 4",
                Address = "Zbożowa -1",
                Description = "Taki tam turniej",
                StartDate = DateTime.Parse("10.06.2022 10:00"),
                EndDate = DateTime.Parse("10.06.2022 20:00")
            });

            await _context.SaveChangesAsync();
        }
    }
}
