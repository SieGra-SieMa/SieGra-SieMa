using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class GetGroupMatchDTO
    {
        public int TournamentId { get; set; }
        public int Phase { get; set; }
        public int MatchId { get; set; }
        public int GroupId { get; set; }
        public int TeamHomeId { get; set; }
        public int TeamAwayId { get; set; }
        public int? TeamHomeScore { get; set; }
        public int? TeamAwayScore { get; set; }
    }
}
