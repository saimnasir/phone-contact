using ApiGateway.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.ResponseModels
{
    public class ReadReportResponse: BaseResponse
    {
        public ReportStatuses Status { get; set; }
        public string StatusName { get; set; }
        public IEnumerable<ReportDetail> ReportDetails { get; set; }
        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
     
}
