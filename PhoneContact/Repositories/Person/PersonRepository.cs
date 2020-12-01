using DataModels;
using Microsoft.Extensions.Configuration;
using Queries.Commands;
using Queries.Executers;

namespace Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(IConfiguration configuration, ICommandText commandText, IExecuters executers, string tableName)
            : base(configuration, commandText, executers, tableName)
        {
        }

        public Person Create(Person person)
        {
            var parameters = new
            {
                person.FirstName,
                person.MiddleName,
                person.LastName,
                person.CompanyName,
            };
            return base.Create(person, parameters);
        }

        public Person Update(Person person)
        {
            var parameters = new
            {
                person.Id,
                person.UIID,
                person.FirstName,
                person.MiddleName,
                person.LastName,
                person.CompanyName,
            };
            return base.Update(person, parameters);
        }

    }
}
