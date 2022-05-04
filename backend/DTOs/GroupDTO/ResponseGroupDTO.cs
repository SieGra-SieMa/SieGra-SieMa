using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.GroupDTO
{
    public class ResponseGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentId { get; set; }
        public bool Ladder { get; set; }
    }
}
