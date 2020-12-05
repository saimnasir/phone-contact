using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.ViewModels.Requests
{
    public class DeleteModelRequest
    {
        public long Id { get; set; }
        public Guid UIID { get; set; }
    }
}
