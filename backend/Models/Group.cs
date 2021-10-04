using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Group
    {
        public Group()
        {
            TeamInGroups = new HashSet<TeamInGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentId { get; set; }
        public bool Ladder { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<TeamInGroup> TeamInGroups { get; set; }
    }
}
