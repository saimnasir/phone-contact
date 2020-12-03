using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Executers;
using Queries.Commands;
using Core.Enums;

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
                report.Lattitude,
                report.Longitude,
                Status = ReportStatuses.Requested
            };
            return base.Create(parameters);
        }

        public Report Update(Report report)
        {
            var parameters = new
            {
                report.UIID,
                report.Status
            };
            return base.Update( parameters);
        }

    }
}
