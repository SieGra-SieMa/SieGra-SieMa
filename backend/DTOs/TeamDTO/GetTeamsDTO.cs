using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs
{
    public class GetTeamsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public string CaptainName { get; set; }
        public string CaptainSurname { get; set; }
        public string Code { get; set; }
    }
}
