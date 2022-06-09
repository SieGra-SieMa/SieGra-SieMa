using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Medium
    {
        public Medium()
        {
            MediumInAlbums = new HashSet<MediumInAlbum>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        
        public virtual ICollection<MediumInAlbum> MediumInAlbums { get; set; }
        public virtual ICollection<Tournament> Tournament { get; set; }
        public virtual ICollection<Team> Team { get; set; }

    }
}
