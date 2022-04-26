using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ContestantDTO;
using SieGraSieMa.DTOs.ContestDTO;
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
        public string Description { get; set; }
        public string Address { get; set; }
        public virtual ICollection<ResponseAlbumDTO> Albums { get; set; }
        public virtual ICollection<ResponseContestDTO> Contests { get; set; }
        public virtual ICollection<ResponseAlbumDTO> Groups { get; set; }
        public virtual ICollection<ResponseAlbumDTO> TeamInTournaments { get; set; }



    }
}
