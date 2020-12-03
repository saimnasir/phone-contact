using DataModels;
using Queries.Commands;
using Queries.Executers;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private IReportRepository _reportRepository;
        private IReportDetailRepository _reportDetailRepository;

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

        public IReportDetailRepository ReportDetailRepository
        {
            get
            {
                if (_reportDetailRepository == null)
                {
                    _reportDetailRepository = new ReportDetailRepository(_configuration, CommandText, Executers, TableName<ReportDetail>());
                }
                return _reportDetailRepository;
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
