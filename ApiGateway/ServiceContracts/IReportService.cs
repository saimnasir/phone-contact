using ApiGateway.RequestModels;
using ApiGateway.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.ServiceContracts
{
    public interface IReportService
    {
        public CreateReportResponse RequestReport(CreateReportRequest request);
        public ReadReportResponse ReadReport(ReadReportRequest request);
         public GetReportResponse GetReports();
    }
}
