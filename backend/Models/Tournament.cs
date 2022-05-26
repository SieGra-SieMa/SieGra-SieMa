using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Albums = new HashSet<Album>();
            Contests = new HashSet<Contest>();
            Groups = new HashSet<Group>();
            TeamInTournaments = new HashSet<TeamInTournament>();
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Contest> Contests { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<TeamInTournament> TeamInTournaments { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
