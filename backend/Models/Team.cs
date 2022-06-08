using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
            TeamInGroups = new HashSet<TeamInGroup>();
            TeamInTournaments = new HashSet<TeamInTournament>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CaptainId { get; set; }
        public string Code { get; set; }
        public int? MediumId { get; set; }

        public virtual User Captain { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<TeamInGroup> TeamInGroups { get; set; }
        public virtual ICollection<TeamInTournament> TeamInTournaments { get; set; }
    }
}
