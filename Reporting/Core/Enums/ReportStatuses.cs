using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum ReportStatuses
    {
        Requested = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4,
        Cancelled = 5,
    }

    public static class ReportStatuseName
    {

        public static string GetStatusName(this ReportStatuses status)
        {
            return Enum.GetName(typeof(ReportStatuses), status);
        }
    }
}
