using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Executers;
using Queries.Commands;

namespace Repositories
{
    public class ReportDetailRepository : Repository<ReportDetail>, IReportDetailRepository
    {
        public ReportDetailRepository(IConfiguration configuration, ICommandText commandText, IExecuters executers, string tableName)
            : base(configuration, commandText, executers, tableName)
        {
        }

        public ReportDetail Create(ReportDetail report)
        {
            var parameters = new
            {
                report.Id,
                report.UIID,
            };
            return base.Create(parameters);
        }

        public ReportDetail Update(ReportDetail report)
        {
            var parameters = new
            {
                report.Id,
                report.UIID,
            };
            return base.Update(parameters);
        }

    }
}
