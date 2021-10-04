using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Contest
    {
        public Contest()
        {
            Contestants = new HashSet<Contestant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<Contestant> Contestants { get; set; }
    }
}
