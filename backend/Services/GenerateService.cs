using Microsoft.Extensions.Configuration;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public class GenerateService : IGenerateService
    {
        private readonly SieGraSieMaContext _context;

        public IConfiguration Configuration { get; set; }

        public GenerateService(SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public IEnumerable<Team> GenerateTeams(int amount, int tournamentId)
        {
            //List<User> UsersList = new List<User>();
            List<Team> TeamsList = new List<Team>();
            string salt = CreateSalt();

            for (int teamCounter = 0; teamCounter < amount; teamCounter++)
            {
                var captain = new User() { Name = RandomString(6), Surname = RandomString(8), Email = RandomString(6) + "@gmail.com", Password = GetPassword("haslo123", salt), Salt = salt };
                captain.UserRoles.Add(new UserRole() { User = captain, RoleId = 3 });
                var team = new Team() { Code = RandomString(5), Captain = captain, Name = RandomString(10) };
                team.Players.Add(new Player() { Team = team, User = captain });
                //UsersList.Add(captain);
                for (int player = 1; player < 5; player++)
                {
                    var user = new User() { Name = RandomString(6), Surname = RandomString(8), Email = RandomString(6) + "@gmail.com", Password = GetPassword("haslo123", salt), Salt = salt };
                    user.UserRoles.Add(new UserRole() { User = user, RoleId = 3 });
                    team.Players.Add(new Player() { Team = team, User = user });
                    //UsersList.Add(user);
                }
                TeamsList.Add(team);
                team.TeamInTournaments.Add(new TeamInTournament() { Team = team, TournamentId = tournamentId, Paid = true });

            }

            _context.Teams.AddRange(TeamsList);
            _context.SaveChanges();

            //modelBuilder.Entity<User>().HasData(UsersList);
            //modelBuilder.Entity<Team>().HasData(TeamsList);



            return TeamsList;
        }
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)])
                                                    .ToArray());
        }
        private static string GetPassword(string password, string salt)
        {
            var valueBytes =
                       Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                            password,
                            System.Text.Encoding.UTF8.GetBytes(salt),
                            Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA512,
                            1000,
                            256 / 8
                        );
            return Convert.ToBase64String(valueBytes);
        }
        private static string CreateSalt(int maximumSaltLength = 32)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}
