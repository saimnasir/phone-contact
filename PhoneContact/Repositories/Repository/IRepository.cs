using DataModels;
using PhoneContact.Repositories;
using PhoneContact.ViewModels.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T> where T : DataModel
    {
        IUnitOfWork UnitOfWork { get; }

        public T Create(object parameters);

        public T Read(long id);

        public T Update(object parameters);

        public bool Delete(long id);

        public IEnumerable<T> ListAll();

        public IEnumerable<T> ListAllByMaster(long masterId);

        public IEnumerable<T> Search(object parameters);

        public T Find(object parameters);
         
    }
}
