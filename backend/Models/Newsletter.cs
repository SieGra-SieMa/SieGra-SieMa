using System;
using System.Collections.Generic;

#nullable disable

namespace SieGraSieMa.Models
{
    public partial class Newsletter
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
