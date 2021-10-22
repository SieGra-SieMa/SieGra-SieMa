using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Models
{
    public class SiegraSiemaInitializer : DropCreateDatabaseAlways<SieGraSieMaContext>
    {
        protected override void Seed(SieGraSieMaContext context)
        {

            context.Roles.Add(new Role() { Id = 1, Name = "Administrator" });
            context.Roles.Add(new Role() { Id = 2, Name = "Pracownik" });
            context.Roles.Add(new Role() { Id = 3, Name = "Użytkownik" });

            var salt = CreateSalt();
            var password = GetPassword(accountRequestDTO.Password, salt);
            context.Users.Add(new User() { Id = 1, Name = "Adm", Surname = "In", Email = "admin@gmail.com", Password = GetPassword("haslo123", salt), Salt = salt });
            context.Users.Add(new User() { Id = 2, Name = "Prac", Surname = "Ownik", Email = "pracownik@gmail.com", Password = GetPassword("haslo123", salt), Salt = salt });
            context.Users.Add(new User() { Id = 3, Name = "Kap", Surname = "Itan", Email = "kapitan@gmail.com", Password = GetPassword("haslo123", salt), Salt = salt });
            context.Users.Add(new User() { Id = 4, Name = "Gr", Surname = "acz", Email = "gracz@gmail.com", Password = GetPassword("haslo123", salt), Salt = salt });

            context.UserRoles.Add(new UserRole() { UserId = 1, RoleId = 1 });
            context.UserRoles.Add(new UserRole() { UserId = 2, RoleId = 2 });
            context.UserRoles.Add(new UserRole() { UserId = 2, RoleId = 3 });
            context.UserRoles.Add(new UserRole() { UserId = 3, RoleId = 3 });
            context.UserRoles.Add(new UserRole() { UserId = 4, RoleId = 3 });

            context.Teams.Add(new Team() { Id = 1, Name = "Bogowie", CaptainId = 3, Code = "ABCD" });
            context.Teams.Add(new Team() { Id = 2, Name = "Demony", CaptainId = 3, Code = "DCBA" });

            context.Players.Add(new Player() { TeamId = 1, UserId = 3 });
            context.Players.Add(new Player() { TeamId = 1, UserId = 4 });
            context.Players.Add(new Player() { TeamId = 2, UserId = 3 });

            context.Newsletters.Add(new Newsletter() { Id  =  1,  UserId  =  3  });

            base.Seed(context);
        }
        private string GetPassword(string password, string salt)
        {
            var valueBytes =
                       KeyDerivation.Pbkdf2(
                            password,
                            Encoding.UTF8.GetBytes(salt),
                            KeyDerivationPrf.HMACSHA512,
                            1000,
                            256 / 8
                        );
            return Convert.ToBase64String(valueBytes);
        }
    }
}
