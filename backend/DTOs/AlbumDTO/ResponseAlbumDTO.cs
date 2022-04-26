using SieGraSieMa.DTOs.MediumDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.AlbumDTO
{
    public class ResponseAlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreateDate { get; set; }
        public int? TournamentId { get; set; }
        public ICollection<ResponseMediumDTO> Media { get; set; }
    }
}
