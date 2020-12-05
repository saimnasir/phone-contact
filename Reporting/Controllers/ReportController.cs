using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reporting.Extensions;
using Reporting.ViewModels;
using Reporting.ViewModels.Requests;
using Reporting.ViewModels.Responses;
using Repositories;
using Serilog;

namespace PhoneContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IReportRepository _repository;
        private readonly IReportDetailRepository _reportDetailRepository;
        private readonly IMapper _mapper;
        public ReportController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _repository = _repositoryFactory.ReportRepository;
            _reportDetailRepository = repositoryFactory.ReportDetailRepository;
        }

        // GET: api/Report
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<ListAllReportsResponse> ListAll()
        {
            try
            {
                var dataModels = _repository.ListAll();
                var viewModels = _mapper.Map<List<Report>>(dataModels);
                return new ListAllReportsResponse
                {
                    Reports = viewModels
                };
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ReportDetail failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/Report/ListAllWithDetails
        [HttpPost]
        [Route("ListAllWithDetails")]
        public ActionResult<IEnumerable<Report>> ListAllWithDetails(ReadByMasterRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.MasterId);
                dataModel.ModelCheck(request.MasterUIID);

                var dataModels = _repository.ListAllByMaster(request.MasterId);
                var viewModels = _mapper.Map<List<Report>>(dataModels);
                viewModels.ForEach(model =>
                {
                    model.ReportDetails = listReportDetails(new ReadByMasterRequest { MasterId = model.Id, MasterUIID = model.UIID }).ToList();
                });
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Report failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/Report/Read
        [HttpPost]
        [Route("Read")]
        public ActionResult<Report> Read(ReadModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);
                var viewModel = _mapper.Map<Report>(dataModel);
                viewModel.ReportDetails = listReportDetails(new ReadByMasterRequest { MasterId = request.Id, MasterUIID = request.UIID });
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Report failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/Report/Create
        [HttpPost]
        [Route("Create")]
        public ActionResult<Report> Create(CreateReportRequest request)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Report>(request);
                dataModel = _repository.Create(dataModel);

                var viewModel = _mapper.Map<Report>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Report failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }
                 
        // PUT: api/ReportDetail/Create
        [HttpPut]
        [Route("Update")]
        public ActionResult<Report> Update(UpdateReportRequest request)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Report>(request);
                dataModel.ModelCheck(request.UIID);

                dataModel = _repository.Read(request.Id);
                // set properties
                dataModel.Latitude = request.Latitude;
                dataModel.Longitude = request.Longitude;

                dataModel = _repository.Update(dataModel);
                var viewModel = _mapper.Map<Report>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Report failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/Report/Delete
        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(DeleteModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);

                var messageResponse = "Report Deleted.";
                if (_repository.Delete(request.Id))
                {
                    messageResponse = $"Delete Report failed.";
                    Log.Error(messageResponse);
                    throw new Exception(messageResponse);
                }
                return new JsonResult(new { messageResponse });
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Report failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        private IEnumerable<ReportDetail> listReportDetails(ReadByMasterRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.MasterId);
                dataModel.ModelCheck(request.MasterUIID);

                var dataModels = _reportDetailRepository.ListAllByMaster(request.MasterId);
                var viewModels = _mapper.Map<List<ReportDetail>>(dataModels);
                return viewModels;
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