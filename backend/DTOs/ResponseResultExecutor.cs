using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.ResponseWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.DTOs
{
    internal class ResponseResultExecutor : ObjectResultExecutor
    {
        public ResponseResultExecutor(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory, ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory, loggerFactory, mvcOptions)
        {
        }

        public override Task ExecuteAsync(ActionContext context, ObjectResult result)
        {
            if(result.Value is ResponseErrorDTO)
                return base.ExecuteAsync(context, result);
            var response = new ResponseDTO<object>();
            response.Data = result.Value;

           
            TypeCode typeCode = Type.GetTypeCode(result.Value.GetType());
            if (typeCode == TypeCode.Object)
                result.Value = response;

            return base.ExecuteAsync(context, result);
        }
    }
}
