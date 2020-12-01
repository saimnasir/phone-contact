using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
   public interface IPersonRepository : IRepository<Person>
    {
        public Person Create(Person person);
        public Person Update(Person person);
    }
}
