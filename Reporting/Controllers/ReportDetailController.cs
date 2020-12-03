using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Serilog;
using ViewModels;

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
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

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

        // PUT: api/ContactInfo/Create
        [HttpPut]
        [Route("Update")]
        public ActionResult<ReportDetail> Update(ReportDetail viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.ReportDetail>(viewModel);
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


        // DELETE: api/ReportDetail/Delete
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(long id)
        {
            try
            {
                var messageResponse = "ReportDetail Deleted.";
                if (_repository.Delete(id))
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

    }
}