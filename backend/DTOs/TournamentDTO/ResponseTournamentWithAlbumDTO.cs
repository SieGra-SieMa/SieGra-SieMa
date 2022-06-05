using SieGraSieMa.DTOs.AlbumDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.TournamentDTO
{
    public class ResponseTournamentWithAlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
        public virtual IEnumerable<ResponseAlbumWithoutMediaDTO> Albums { get; set; }
    }
}
