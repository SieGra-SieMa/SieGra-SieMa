using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class User
    {
        public User()
        {
            Contestants = new HashSet<Contestant>();
            Logs = new HashSet<Log>();
            Newsletters = new HashSet<Newsletter>();
            Players = new HashSet<Player>();
            Teams = new HashSet<Team>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Contestant> Contestants { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Newsletter> Newsletters { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
