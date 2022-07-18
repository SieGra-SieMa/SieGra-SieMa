using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.Users
{
    public class UsersWithPagging
    {
        public int TotalCount { get; set; }
        public List<UserDTO> Items { get; set; }
    }
}
