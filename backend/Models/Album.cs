using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Album
    {
        public Album()
        {
            Media = new HashSet<Medium>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CreateDate { get; set; }
        public int? TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<Medium> Media { get; set; }
    }
}
