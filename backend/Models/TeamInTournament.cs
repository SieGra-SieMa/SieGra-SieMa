using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class TeamInTournament
    {
        public int TeamId { get; set; }
        public int TournamentId { get; set; }
        public bool Paid { get; set; }

        public virtual Team Team { get; set; }
        public virtual Tournament Tournament { get; set; }
    }
}
