using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ContestantDTO;
using SieGraSieMa.DTOs.ContestDTO;
using SieGraSieMa.DTOs.GroupDTO;
using SieGraSieMa.DTOs.MatchDTO;
using SieGraSieMa.DTOs.TeamInTournamentDTO;
using SieGraSieMa.DTOs.TeamsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.TournamentDTO
{
    public class ResponseTournamentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public string Description { get; set; }
        public string Address { get; set; }
        public virtual IEnumerable<ResponseAlbumDTO> Albums { get; set; }
        public virtual IEnumerable<ResponseContestDTO> Contests { get; set; }
        public virtual IEnumerable<ResponseGroupDTO> Groups { get; set; }
        //public virtual IEnumerable<string> Teams { get; set; }
        public virtual IEnumerable<GetLadderDTO> Ladder { get; set; }


    }
}
