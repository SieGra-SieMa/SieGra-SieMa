using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Interfaces
{
    interface IUserRolesService
    {
        void AddRole(int UserId, int RoleId);
        void RemoveRole(int UserId, int RoleId);
        bool CheckRole(int UserId, int RoleId);
        IEnumerable<Role> GetUserRoles(int UserId);
        IEnumerable<User> GetUsersWithRole(int RoleId);
    }
}
