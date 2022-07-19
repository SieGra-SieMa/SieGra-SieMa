using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.TeamsDTO
{
    public class TeamsWithPagging
    {
        public int TotalCount { get; set; }
        public List<GetTeamsDTO> Items { get; set; }
    }
}
