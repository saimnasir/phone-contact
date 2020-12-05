using Core.Enums;
using System.Collections.Generic;

namespace Reporting.ViewModels
{
    public class Report : ViewModel
    {
        public double Radius { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ReportStatuses Status { get; set; }

        public IEnumerable<ReportDetail> ReportDetails { get; set; }

        public string StatusName => Status.GetStatusName();
    }
}
