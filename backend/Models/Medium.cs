using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Medium
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
