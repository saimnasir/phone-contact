using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Executers;
using Queries.Commands;
using Core.Enums;
using System.Collections.Generic;

namespace Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(IConfiguration configuration, ICommandText commandText, IExecuters executers, string tableName)
            : base(configuration, commandText, executers, tableName)
        {
        }

        public Report Create(Report report)
        {
            var parameters = new
            {
                report.Radius,
                report.Latitude,
                report.Longitude,
                Status = ReportStatuses.Requested
            };
            return base.Create(parameters);
        }

        public IEnumerable<Report> ListPendingReports()
        {
            return base.ListByCommand("RPT.LST_REPORTSPENDING_SP");
        }

        public Report Update(Report report)
        {
            var parameters = new
            {
                report.Id,
                report.Radius,
                report.Latitude,
                report.Longitude,
                report.Status
            };
            return base.Update( parameters);
        }

    }
}
