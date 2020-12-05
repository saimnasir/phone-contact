using Queries.Commands;
using Queries.Executers;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.PhoneContact
{
    public interface IPhoneContactOperations
    {
        IExecuters Executers { get; }
        ICommandText CommandText { get; }
        IReportRepository ReportRepository { get; }
        void ProcessPendingReports();
    }
}
