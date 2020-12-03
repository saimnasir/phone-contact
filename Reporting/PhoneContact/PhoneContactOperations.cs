using DataModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Queries.Commands;
using Queries.Executers;
using Repositories;
using RestSharp;
using System.Collections.Generic;
using System.Globalization;

namespace Reporting.PhoneContact
{
    public class PhoneContactOperations : IPhoneContactOperations
    {
        private IReportRepository _reportRepository;
        private IExecuters _executers;
        private ICommandText _commandText;

        private IConfiguration _configuration;

        public PhoneContactOperations(IConfiguration configuration)
        {
            _configuration = configuration;
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
        public IReportRepository ReportRepository
        {
            get
            {
                if (_reportRepository == null)
                {
                    _reportRepository = new ReportRepository(_configuration, CommandText, Executers, TableName<Report>());
                }
                return _reportRepository;
            }
        }

        public string TableName<M>()
        {
            var culture = new CultureInfo("en-EN", false);
            return typeof(M).Name.ToUpper(culture);
        }

        public void ProcessPendingReports()
        {
            var reports = getPendingReports();
            foreach (var report in reports)
            {
                var persons = getPersons();
            }
        }

        private IEnumerable<Report> getPendingReports()
        {
            return ReportRepository.ListAll();
        }

        private IEnumerable<Person> getPersons()
        {
            var client = new RestClient("https://localhost:44393/api/person/listall");
            var request = new RestRequest(Method.GET);
            //request.AddHeader("postman-token", "4aeca6e4-a5bc-d646-b60c-9b43e8bf5063");
            //request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(response.Content);

            return people;
        }
    }
}
