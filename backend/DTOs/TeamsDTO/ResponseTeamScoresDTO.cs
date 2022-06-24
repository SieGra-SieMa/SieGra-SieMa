using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.TeamsDTO
{
    public class ResponseTeamScoresDTO
    {
        public int IdTeam { get; set; }
        //public string Name { get; set; }
        public int PlayedMatches { get; set; } = 0;
        public int WonMatches { get; set; } = 0;
        public int TiedMatches { get; set; } = 0;
        public int LostMatches { get; set; } = 0;
        public int GoalScored { get; set; } = 0;
        public int GoalConceded { get; set; } = 0;
        public int Points { get; set; } = 0;
    }
}
