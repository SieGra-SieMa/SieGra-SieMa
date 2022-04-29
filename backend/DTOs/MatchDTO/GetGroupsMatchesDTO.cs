using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.MatchDTO
{
    public class GetGroupsMatchesDTO
    {
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public virtual ICollection<GetMatchDTO> Matches { get; set; }
    }
}
