using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class GetMatchDTO
    {
        public int TournamentId { get; set; }
        public int Phase { get; set; }
        public int MatchId { get; set; }
        public TeamDTO TeamHome { get; set; }
        public TeamDTO TeamAway { get; set; }
        public int? TeamHomeScore { get; set; }
        public int? TeamAwayScore { get; set; }
    }
}
