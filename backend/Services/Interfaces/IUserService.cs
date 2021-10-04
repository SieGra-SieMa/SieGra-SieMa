using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    public interface IUserService
    {
        void AddUser(User User);
        void UpdateUser(int Id, User User);
        void DeleteUser(int Id);
        User GetUser(int Id);
        User GetUser(string Email);
        IEnumerable<User> GetUsers();
    }
}
