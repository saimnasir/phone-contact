using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.ResponseModels
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}
