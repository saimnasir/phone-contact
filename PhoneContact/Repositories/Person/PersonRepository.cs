using DataModels;
using Microsoft.Extensions.Configuration;
using PhoneContact.Repositories;
using Queries.Commands;
using Queries.Executers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            return base.Create(parameters);
        }
 

        public Person Update(Person person)
        {
            var parameters = new
            {
                person.Id,
                person.FirstName,
                person.MiddleName,
                person.LastName,
                person.CompanyName,
            };
            return base.Update(parameters);
        }

    }
}
