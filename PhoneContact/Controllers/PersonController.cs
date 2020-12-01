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

        // GET: api/Person
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<IEnumerable<Person>> ListAll()
        {
            try
            {
                var dataModels = _repository.ListAll();
                var viewModels = _mapper.Map<List<Person>>(dataModels);
                viewModels.ForEach(viewModel =>
                {
                    viewModel.ContactInfos = listContactInfos(viewModel.Id).ToList();
                });
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
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
                viewModel.ContactInfos = listContactInfos(viewModel.Id).ToList();
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
        public ActionResult<Person> Update(Person viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Person>(viewModel);
                dataModel = _repository.Update(dataModel);
                viewModel = _mapper.Map<Person>(dataModel);
                viewModel.ContactInfos = listContactInfos(viewModel.Id).ToList();
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} {viewModel.GetType().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // DELETE: api/Person/Delete
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(long id)
        {
            try
            {
                var messageResponse = "Person Deleted.";
                if (_repository.Delete(id))
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

        private IEnumerable<ContactInfo> listContactInfos(long personId)
        {
            try
            {
                var dataModels = _contactInfoRepository.ListAllByMaster(personId);
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