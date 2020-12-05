using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApiGateway.RequestModels;
using ApiGateway.ResponseModels;
using ApiGateway.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // POST: api/Report/Create
        [HttpPost]
        [Route("Create")]
        public ActionResult<CreateReportResponse> Create(CreateReportRequest request)
        {
            try
            {
                return _reportService.RequestReport(request);
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // GET: api/Report
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<GetReportResponse> ListAll()
        {
            try
            {
                return _reportService.GetReports();
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

       
        // POST: api/Report/Create
        [HttpPost]
        [Route("Read")]
        public ActionResult<ReadReportResponse> Read(ReadReportRequest request)
        {
            try
            {
                return _reportService.ReadReport(request);
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

    }
}
