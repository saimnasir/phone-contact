using ApiGateway.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.ResponseModels
{
    public class Report
    {
        public long Id { get; set; }
        public Guid UIID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; } 
        public double Radius { get; set; } 
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public ReportStatuses Status { get; set; }
        public string StatusName { get; set; }

    }
}
