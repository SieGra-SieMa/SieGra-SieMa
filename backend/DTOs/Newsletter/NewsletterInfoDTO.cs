using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.Newsletter
{
    public class NewsletterInfoDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int? TournamentId { get; set; }
    }
}
