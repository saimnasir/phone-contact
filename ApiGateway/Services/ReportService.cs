using ApiGateway.RequestModels;
using ApiGateway.ResponseModels;
using ApiGateway.ServiceContracts;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Net;

namespace ApiGateway.Services
{
    public class ReportService : IReportService
    {
        private IConfiguration _configuration;
        public ReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CreateReportResponse RequestReport(CreateReportRequest createReportRequest)
        {
            try
            {
                var client = new RestClient($"{getReportingApiBaseUrl()}/api/report/create");
                var request = new RestRequest(Method.POST);

                //request.AddHeader("postman-token", "865928a5-721d-38c9-f6d7-7cff4253fc59");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                var input = Newtonsoft.Json.JsonConvert.SerializeObject(createReportRequest);
                request.AddParameter("application/json", input, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateReportResponse>(response.Content);
                    result.Success = true;
                    return result;
                }
                else
                {
                    return new CreateReportResponse
                    {
                        Success = false,
                        Message = $"Rapor talebi oluşturulamadı.{response.ErrorMessage}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new CreateReportResponse
                {
                    Success = false,
                    Message = $"Rapor talebi oluşturmada hata alındı. Hata:{ex.Message}"
                };
            }
        }

        public GetReportResponse GetReports()
        {
            try
            {
                var client = new RestClient($"{getReportingApiBaseUrl()}/api/report/listall");
                var request = new RestRequest(Method.GET);

                //request.AddHeader("postman-token", "865928a5-721d-38c9-f6d7-7cff4253fc59");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                 request.AddParameter("application/json", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<GetReportResponse>(response.Content);
                    result.Success = true;
                    return result;
                }
                else
                {
                    return new GetReportResponse
                    {
                        Success = false,
                        Message = $"Rapor talebi oluşturulamadı.{response.ErrorMessage}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GetReportResponse
                {
                    Success = false,
                    Message = $"Rapor talebi oluşturmada hata alındı. Hata:{ex.Message}"
                };
            }
        }

        public ReadReportResponse ReadReport(ReadReportRequest readReportRequest)
        {
            try
            {
                var client = new RestClient($"{getReportingApiBaseUrl()}/api/report/read");
                var request = new RestRequest(Method.POST);

                //request.AddHeader("postman-token", "865928a5-721d-38c9-f6d7-7cff4253fc59");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                var input = Newtonsoft.Json.JsonConvert.SerializeObject(readReportRequest);
                request.AddParameter("application/json", input, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadReportResponse>(response.Content);
                    result.Success = true;
                    return result;
                }
                else
                {
                    return new ReadReportResponse
                    {
                        Success = false,
                        Message = $"Rapor okuma hata aldı.{response.ErrorMessage}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ReadReportResponse
                {
                    Success = false,
                    Message = $"Rapor okuma sırasında hata alındı. Hata:{ex.Message}"
                };
            }
        }

    
        private string getReportingApiBaseUrl()
        {
            return _configuration.GetSection("ApiBaseUrls:ReportingApiBaseUrl").Value;
        }


    }
}
