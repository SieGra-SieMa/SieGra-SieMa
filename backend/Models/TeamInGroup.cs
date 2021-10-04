using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class TeamInGroup
    {
        public TeamInGroup()
        {
            MatchTeamAways = new HashSet<Match>();
            MatchTeamHomes = new HashSet<Match>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TeamId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Match> MatchTeamAways { get; set; }
        public virtual ICollection<Match> MatchTeamHomes { get; set; }
    }
}
