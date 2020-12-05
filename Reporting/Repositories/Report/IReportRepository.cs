using DataModels;
using System.Collections.Generic;

namespace Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        public Report Create(Report report);
        public Report Update(Report report);
        public IEnumerable<Report> ListPendingReports();
    }
}
