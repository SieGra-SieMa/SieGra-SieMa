using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class MatchResultDTO
    {
        public int TournamentId { get; set; }
        public int Phase { get; set; }
        public int MatchId { get; set; }
        public int HomeTeamPoints { get; set; }
        public int AwayTeamPoints { get; set; }

    }
}
