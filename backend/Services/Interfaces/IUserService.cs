using SieGraSieMa.DTOs.Users;
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
        UserDTO UpdateUser(string email, UserDetailsDTO userDetailsDTO);
        void DeleteUser(int Id);
        User GetUser(int Id);
        User GetUser(string Email);
        IEnumerable<User> GetUsers();
    }
}
