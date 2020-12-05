using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.ViewModels.Responses
{
    public class ListAllReportsResponse
    {
        public IEnumerable<Report> Reports { get; set; }
    }
}
