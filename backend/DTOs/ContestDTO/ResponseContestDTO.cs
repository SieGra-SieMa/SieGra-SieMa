using SieGraSieMa.DTOs.ContestantDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.ContestDTO
{
    public class ResponseContestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TournamentId { get; set; }

        public virtual ICollection<ResponseContestantDTO> Contestants { get; set; }

    }
}
