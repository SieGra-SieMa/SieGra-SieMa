using SieGraSieMa.DTOs.TeamsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.GroupDTO
{
    public class ResponseGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentId { get; set; }
        //public bool Ladder { get; set; }
        //public ICollection<GetTeamsDTO> Teams { get; set; }
        public ICollection<ResponseTeamScoresDTO> Teams { get; set; }
    }
}
