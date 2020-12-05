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

        public ReportDetail Create(ReportDetail reportDetail)
        {
            var parameters = new
            {
                reportDetail.Report,
                reportDetail.NearbyPersonCount,
                reportDetail.NearbyPhoneNumberCount
            };
            return base.Create(parameters);
        }

        public ReportDetail Update(ReportDetail reportDetail)
        {
            var parameters = new
            {
                reportDetail.Id,
                reportDetail.Report,
                reportDetail.NearbyPersonCount,
                reportDetail.NearbyPhoneNumberCount
            };
            return base.Update(parameters);
        }

    }
}
