using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Match
    {
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
        public int MatchId { get; set; }
        public int Phase { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public int? TeamHomeScore { get; set; }
        public int? TeamAwayScore { get; set; }
        //public string Referee { get; set; }
        public int? TeamHomeId { get; set; }
        public int? TeamAwayId { get; set; }

        public virtual TeamInGroup TeamAway { get; set; }
        public virtual TeamInGroup TeamHome { get; set; }
    }
}
