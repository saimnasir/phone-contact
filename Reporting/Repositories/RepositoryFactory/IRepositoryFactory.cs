using Queries.Commands;
using Queries.Executers;

namespace Repositories
{
    public interface IRepositoryFactory
    {
        IExecuters Executers { get; }
        ICommandText CommandText { get; }
        IReportRepository ReportRepository { get; }
        IReportDetailRepository ReportDetailRepository { get; }
    }
}
