using DataModels;

namespace Repositories
{
    public interface IReportRepository : IRepository<Report>
    {
        public Report Create(Report report);
        public Report Update(Report report);
    }
}
