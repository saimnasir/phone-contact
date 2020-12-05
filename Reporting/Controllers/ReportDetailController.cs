using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reporting.Extensions;
using Reporting.ViewModels;
using Reporting.ViewModels.Requests;
using Repositories;
using Serilog;

namespace PhoneContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportDetailController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IReportDetailRepository _repository;
        private readonly IMapper _mapper;
        public ReportDetailController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _repository = _repositoryFactory.ReportDetailRepository;
        }

        // GET: api/ReportDetail
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<IEnumerable<ReportDetail>> ListAll()
        {
            try
            {
                var dataModels = _repository.ListAll();
                var viewModels = _mapper.Map<List<ReportDetail>>(dataModels);
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ReportDetail failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/ReportDetail
        [HttpPost]
        [Route("ListAllByMaster")]
        public ActionResult<IEnumerable<ReportDetail>> ListAllByMaster(ReadByMasterRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.MasterId);
                dataModel.ModelCheck(request.MasterUIID);

                var dataModels = _repository.ListAllByMaster(request.MasterId);
                var viewModels = _mapper.Map<List<ReportDetail>>(dataModels);
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ReportDetail failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/ReportDetail/Read
        [HttpPost]
        [Route("Read")]
        public ActionResult<ReportDetail> Read(ReadModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);
                var viewModel = _mapper.Map<ReportDetail>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ReportDetail failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        /* NOT USED METHODS
        // POST: api/ReportDetail/Create
        [HttpPost]
        [Route("Create")]
        public ActionResult<ReportDetail> Create(ReportDetail viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.ReportDetail>(viewModel);
                dataModel = _repository.Create(dataModel);
                viewModel = _mapper.Map<ReportDetail>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} {viewModel.GetType().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // PUT: api/ReportDetail/Create
        [HttpPut]
        [Route("Update")]
        public ActionResult<ReportDetail> Update(ReportDetail viewModel)
        {
            try
            {
                var dataModel = _repository.Read(viewModel.Id);
                dataModel.ModelCheck(viewModel.UIID);

                dataModel = _mapper.Map<DataModels.ReportDetail>(viewModel);
                dataModel = _repository.Update(dataModel);
                viewModel = _mapper.Map<ReportDetail>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} {viewModel.GetType().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/ReportDetail/Delete
        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(DeleteModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);

                var messageResponse = "ReportDetail Deleted.";
                if (_repository.Delete(request.Id))
                {
                    messageResponse = $"Delete ReportDetail failed.";
                    Log.Error(messageResponse);
                    throw new Exception(messageResponse);
                }
                return new JsonResult(new { messageResponse });
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ReportDetail failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }
        */
    }
}