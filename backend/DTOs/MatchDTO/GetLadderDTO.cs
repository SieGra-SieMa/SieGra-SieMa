using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class GetLadderDTO
    {
        public virtual ICollection<GetPhasesWithMatchesDTO> Phases { get; set; }
    }
}
