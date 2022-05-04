using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class GetAvailableGroupMatchesDTO
    {
        public virtual ICollection<GetGroupsMatchesDTO> GroupsMatches { get; set; }
    }
}
