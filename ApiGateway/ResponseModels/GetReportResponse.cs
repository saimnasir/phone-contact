using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.ResponseModels
{
    public class GetReportResponse : BaseResponse
    {
        public IEnumerable<Report> Reports { get; set; }
    }
}
