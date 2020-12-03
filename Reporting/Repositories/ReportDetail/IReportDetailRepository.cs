using DataModels;

namespace Repositories
{
    public interface IReportDetailRepository : IRepository<ReportDetail>
    {
        public ReportDetail Create(ReportDetail report);
        public ReportDetail Update(ReportDetail report);
    }
}
