using DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {         
        public Person Create(Person person);
        public Person Update(Person person);
    }
}
