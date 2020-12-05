using Core.Enums;
using DataModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Queries.Commands;
using Queries.Executers;
using Reporting.PhoneContact.Requests;
using Reporting.PhoneContact.Responses;
using Repositories;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Reporting.PhoneContact
{
    public class PhoneContactOperations : IPhoneContactOperations
    {
        private IReportRepository _reportRepository;
        private IReportDetailRepository _reportDetailRepository;

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

        public string TableName<M>()
        {
            var culture = new CultureInfo("en-EN", false);
            return typeof(M).Name.ToUpper(culture);
        }

        public void ProcessPendingReports()
        {
            try
            {
                var reports = ReportRepository.ListPendingReports();
                foreach (var report in reports)
                {
                    report.Status = ReportStatuses.Processing;
                    ReportRepository.Update(report);
                    try
                    {
                        var nearbyCounts = getNearbyCounts(new GetNearbyCountsRequest
                        {
                            Radius = report.Radius,
                            Latitude = report.Latitude,
                            Longitude = report.Longitude,
                        });
                        var reportDetail = new ReportDetail
                        {
                            Report = report.Id,
                            NearbyPersonCount = nearbyCounts.NearbyPersonCount,
                            NearbyPhoneNumberCount = nearbyCounts.NearbyPhoneNumberCount
                        };
                        ReportDetailRepository.Create(reportDetail);
                        report.Status = ReportStatuses.Completed;
                        ReportRepository.Update(report);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message, ex);
                        report.Status = ReportStatuses.Failed;
                        ReportRepository.Update(report);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        private GetNearbyCountsResponse getNearbyCounts(GetNearbyCountsRequest getNearbyCountsRequest)
        {
            var client = new RestClient($"{getPhoneContactApiBaseUrl()}/api/contactInfo/GetNearBy");
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "865928a5-721d-38c9-f6d7-7cff4253fc59");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            var input = JsonConvert.SerializeObject(getNearbyCountsRequest);
            request.AddParameter("application/json", input, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var getNearbyCountsResponse = JsonConvert.DeserializeObject<GetNearbyCountsResponse>(response.Content);

            return getNearbyCountsResponse;
        }

        private string getPhoneContactApiBaseUrl()
        {
            return _configuration.GetSection("ApiBaseUrls:PhoneContactApiBaseUrl").Value;
        }

    }
}
