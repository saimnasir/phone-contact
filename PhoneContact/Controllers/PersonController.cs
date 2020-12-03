using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneContact.ViewModels.Requests;
using Repositories;
using Serilog;
using PhoneContact.ViewModels;
using PhoneContact.Extensions;

namespace PhoneContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPersonRepository _repository;
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IMapper _mapper;
        public PersonController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _repository = _repositoryFactory.PersonRepository;
            _contactInfoRepository = _repositoryFactory.ContactInfoRepository;
        }

        // GET: api/Person/ListAll
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<IEnumerable<Person>> ListAll()
        {
            try
            {
                var dataModels = _repository.ListAll();
                var viewModels = _mapper.Map<List<Person>>(dataModels);
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/Person/ListAllWithDetails
        [HttpPost]
        [Route("ListAllWithDetails")]
        public ActionResult<IEnumerable<Person>> ListAllWithDetails(ReadByMasterRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.MasterId);
                dataModel.ModelCheck(request.MasterUIID);

                var dataModels = _repository.ListAllByMaster(request.MasterId);
                var viewModels = _mapper.Map<List<Person>>(dataModels);
                viewModels.ForEach(model =>
                {
                    model.ContactInfos = listPersons(new ReadByMasterRequest { MasterId = model.Id, MasterUIID = model.UIID }).ToList();
                });
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/Person/Read
        [HttpPost]
        [Route("Read")]
        public ActionResult<Person> Read(ReadModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);
                var viewModel = _mapper.Map<Person>(dataModel);
                viewModel.ContactInfos = listPersons(new ReadByMasterRequest { MasterId = request.Id, MasterUIID = request.UIID }).ToList();
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/Person/Create
        [HttpPost]
        [Route("Create")]
        public ActionResult<Person> Create(Person viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Person>(viewModel);
                dataModel = _repository.Create(dataModel);
                viewModel = _mapper.Map<Person>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} {viewModel.GetType().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // PUT: api/Person/Create
        [HttpPut]
        [Route("Update")]
        public ActionResult<Person> Update(Person viewModel)
        {
            try
            {
                var dataModel = _repository.Read(viewModel.Id);
                dataModel.ModelCheck(viewModel.UIID); 
                
                dataModel = _mapper.Map<DataModels.Person>(viewModel);
                dataModel = _repository.Update(dataModel);
                viewModel = _mapper.Map<Person>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} {viewModel.GetType().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // POST: api/Person/Delete
        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(DeleteModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID); 
                
                var messageResponse = "Person Deleted.";
                if (_repository.Delete(request.Id))
                {
                    messageResponse = $"Delete Person failed.";
                    Log.Error(messageResponse);
                    throw new Exception(messageResponse);
                }
                return new JsonResult(new { messageResponse });
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        private IEnumerable<ContactInfo> listPersons(ReadByMasterRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.MasterId);
                dataModel.ModelCheck(request.MasterUIID);

                var dataModels = _contactInfoRepository.ListAllByMaster(request.MasterId);
                var viewModels = _mapper.Map<List<ContactInfo>>(dataModels);
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