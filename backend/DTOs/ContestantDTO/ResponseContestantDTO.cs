using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.ContestantDTO
{
    public class ResponseContestantDTO
    {
        //public int ContestId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Points { get; set; }
    }
}
