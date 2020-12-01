using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Executers;
using Queries.Commands;

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
                report.Status,
            };
            return base.Create(report, parameters);
        }

        public Report Update(Report report)
        {
            var parameters = new
            {
                report.Id,
                report.UIID,
                report.Status
            };
            return base.Update(report, parameters);
        }

    }
}
