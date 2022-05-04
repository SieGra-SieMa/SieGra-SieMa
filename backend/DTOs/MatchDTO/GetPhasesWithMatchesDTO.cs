using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class GetPhasesWithMatchesDTO
    {
        public int Phase { get; set; }
        public virtual ICollection<GetMatchDTO> Matches { get; set; }
    }
}
