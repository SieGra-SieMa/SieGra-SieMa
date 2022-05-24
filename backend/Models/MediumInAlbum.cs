using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Models
{
    public class MediumInAlbum
    {
        public int AlbumId { get; set; }
        public int MediumId { get; set; }

        public virtual Album Album { get; set; }
        public virtual Medium Medium { get; set; }
    }
}
