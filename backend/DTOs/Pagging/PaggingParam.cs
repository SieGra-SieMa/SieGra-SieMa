using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.Pagging
{
    public class PaggingParam
    {
        public int Page { get; set; } = 1;
        public int Count { get; set; } = 10;

    }
}
