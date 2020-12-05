using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.RequestModels
{
    public class ReadReportRequest
    {
        public long Id { get; set; }
        public Guid UIID { get; set; }
    }
}
