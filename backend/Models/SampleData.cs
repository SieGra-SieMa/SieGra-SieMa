using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace SieGraSieMa.Models
{
    public class SampleData
    {
        public async static void Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<SieGraSieMaContext>();
            UserManager<User> _userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            string[] roles = new string[] {"Admin", "Employee", "User"};

            foreach (string role in roles)
            {

                var roleStore = new RoleStore<IdentityRole<int>,SieGraSieMaContext,int>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole<int>() { Name = role, NormalizedName = role });
                    //new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "Admin" }
                    //await roleStore.CreateAsync(new IdentityRole<int>() {Name=role, NormalizedName=role });
                }
            }

            var user = new User
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
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var result = await _userManager.CreateAsync(user, configuration.GetConnectionString("adminPassword"));
                //var password = new PasswordHasher<User>();
                //var hashed = password.HashPassword(user, "Haslo+123");
                //user.PasswordHash = hashed;
                //var userStore = new UserStore<User>(context);
                //var result = userStore.CreateAsync(user);
            }
            user = await _userManager.FindByEmailAsync(user.Email);
            if (!(await _userManager.IsInRoleAsync(user, "Admin")))
            {
                await _userManager.AddToRolesAsync(user, roles);
            }

            //AssignRoles(serviceProvider, user.Email, roles);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<User> _userManager = services.GetService<UserManager<User>>();
            User user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
