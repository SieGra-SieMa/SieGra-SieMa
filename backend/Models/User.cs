using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            Contestants = new HashSet<Contestant>();
            Logs = new HashSet<Log>();
            Newsletters = new HashSet<Newsletter>();
            Players = new HashSet<Player>();
            Teams = new HashSet<Team>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Contestant> Contestants { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Newsletter> Newsletters { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
