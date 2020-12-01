using DataModels;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRepository<T> where T : DataModel
    {
        public T Create(T dataModel, object parameters);

        public T Read(long id);

        public T Update(T dataModel, object parameters);

        public bool Delete(long id);

        public IEnumerable<T> ListAll();

        public IEnumerable<T> ListAllByMaster(long masterId);

        public IEnumerable<T> Search(object parameters);

        public T Find(T dataModel, object parameters);

    }
}
