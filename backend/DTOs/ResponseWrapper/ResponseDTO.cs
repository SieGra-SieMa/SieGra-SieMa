using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs.ResponseWrapper
{
    internal class ResponseDTO<T>
    {
        public T Data { set; get; }

    }
}
