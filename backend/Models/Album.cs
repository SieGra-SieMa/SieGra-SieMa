using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Album
    {
        public Album()
        {
            MediumInAlbums = new HashSet<MediumInAlbum>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int? TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<MediumInAlbum> MediumInAlbums { get; set; }
        //public virtual ICollection<Medium> Media { get; set; }
    }
}
