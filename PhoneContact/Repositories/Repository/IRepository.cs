using DataModels;
using PhoneContact.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T> where T : DataModel
    {
        IUnitOfWork UnitOfWork { get; }

        public T Create(object parameters);

        public T Read(Guid UIID);

        public T Update(object parameters);

        public bool Delete(Guid UIID);

        public IEnumerable<T> ListAll();

        public IEnumerable<T> ListAllByMaster(Guid masterUIID);

        public IEnumerable<T> Search(object parameters);

        public T Find(object parameters); 

    }
}
