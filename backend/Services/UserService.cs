using SieGraSieMa.Models;
using SieGraSieMa.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public class UserService : IUserService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public UserService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }
        public void AddUser(User User)
        {
            _SieGraSieMaContext.Users.Add(User);
            _SieGraSieMaContext.SaveChanges();
        }

        public void DeleteUser(int Id)
        {
            _SieGraSieMaContext.Users.Remove(GetUser(Id));
            _SieGraSieMaContext.SaveChanges();
        }

        public User GetUser(int Id)
        {
            return _SieGraSieMaContext.Users.Where(t => t.Id == Id).SingleOrDefault();
        }

        public User GetUser(string Email)
        {
            return _SieGraSieMaContext.Users.Where(t => t.Email == Email).SingleOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _SieGraSieMaContext.Users.ToList();
        }

        public void UpdateUser(int Id, User NewUser)
        {
            var User = GetUser(Id);
            User.Email = NewUser.Email;
            User.Name = NewUser.Name;
            User.Surname = NewUser.Surname;
            _SieGraSieMaContext.SaveChanges();
        }
    }
}
