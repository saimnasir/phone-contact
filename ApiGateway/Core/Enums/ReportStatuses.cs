using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Core.Enums
{
    public enum ReportStatuses
    {
        Requested = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4,
        Cancelled = 5,
    }
}
