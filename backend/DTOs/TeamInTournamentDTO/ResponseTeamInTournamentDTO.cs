using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.TeamInTournamentDTO
{
    public class ResponseTeamInTournamentDTO
    {
        public int TeamId { get; set; }
        public int TournamentId { get; set; }
        public string TeamName { get; set; }
        public string TeamProfileUrl { get; set; }
        public bool Paid { get; set; }
    }
}
