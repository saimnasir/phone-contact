using DataModels;
using Queries.Commands;
using Queries.Executers;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IPersonRepository _personRepository;
        private IContactInfoRepository _contactInfoRepository;

        private IExecuters _executers;
        private ICommandText _commandText;

        private IConfiguration _configuration;

        public RepositoryFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TableName<M>()
        {
            var culture = new CultureInfo("en-EN", false);
            return typeof(M).Name.ToUpper(culture);
        }


        public IPersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(_configuration, CommandText, Executers, TableName<Person>());
                }
                return _personRepository;
            }
        }


        public IContactInfoRepository ContactInfoRepository
        {
            get
            {
                if (_contactInfoRepository == null)
                {
                    _contactInfoRepository = new ContactInfoRepository(_configuration, CommandText, Executers, TableName<ContactInfo>());
                }
                return _contactInfoRepository;
            }
        }

        public IExecuters Executers
        {
            get
            {
                if (_executers == null)
                {
                    _executers = new Executers();
                }
                return _executers;
            }
        }
        public ICommandText CommandText
        {
            get
            {
                if (_commandText == null)
                {
                    _commandText = new CommandText();
                }
                return _commandText;
            }
        }
    }
}
