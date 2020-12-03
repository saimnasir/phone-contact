using Core.Enums;
using System.Collections.Generic;

namespace ViewModels
{
    public class Report : ViewModel
    {
        public ReportStatuses Status { get; set; }

        public IEnumerable<ReportDetail> ReportDetails { get; set; }
    }
}
