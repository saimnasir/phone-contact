using Queries.Commands;
using Queries.Executers;

namespace Repositories
{
    public interface IRepositoryFactory
    {
      //  IConfiguration Configuration { get; }
        IExecuters Executers { get; }
        ICommandText CommandText { get; }
        IPersonRepository PersonRepository { get; }
        IContactInfoRepository ContactInfoRepository { get; }
        ILocationRepository LocationRepository { get; }
    }
}
