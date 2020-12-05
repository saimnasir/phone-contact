using DataModels;
using Reporting.Repositories;
using System;
using System.Collections.Generic;

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

        public IEnumerable<T> ListByCommand(string command);
        public IEnumerable<T> ListByCommand(string command, object parameters);

    }
}
