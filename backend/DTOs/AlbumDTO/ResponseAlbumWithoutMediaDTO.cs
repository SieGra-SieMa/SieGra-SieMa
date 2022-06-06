using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.AlbumDTO
{
    public class ResponseAlbumWithoutMediaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProfilePicture { get; set; }

    }
}
