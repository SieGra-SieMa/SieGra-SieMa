using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Contestant
    {
        public int ContestId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual User User { get; set; }
    }
}
