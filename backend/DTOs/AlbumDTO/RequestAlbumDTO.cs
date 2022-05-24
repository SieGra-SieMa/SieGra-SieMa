using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.AlbumDTO
{
    public class RequestAlbumDTO
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int TournamentId { get; set; }

    }
}
