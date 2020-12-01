using System;
using System.Collections.Generic;
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
    public class ContactInfoController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IContactInfoRepository _repository;
        private readonly IMapper _mapper;
        public ContactInfoController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _repository = _repositoryFactory.ContactInfoRepository;
        }


        // GET: api/ContactInfo
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<IEnumerable<ContactInfo>> ListAll()
        {
            try
            {
                var dataModels = _repository.ListAll();
                var viewModels = _mapper.Map<List<ContactInfo>>(dataModels);
                //viewModels.ForEach(viewModel =>
                //{
                //    // getContactInfoDetails(viewModel);
                //});
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }


        // GET: api/ContactInfo
        [HttpGet]
        [Route("ListAllByMaster/{personId}")]
        public ActionResult<IEnumerable<ContactInfo>> ListAllByMaster(long personId)
        {
            try
            {
                var dataModels = _repository.ListAllByMaster(personId);
                var viewModels = _mapper.Map<List<ContactInfo>>(dataModels);
                //viewModels.ForEach(viewModel =>
                //{
                //    // getContactInfoDetails(viewModel);
                //});
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/ContactInfo/Create
        [HttpPost]
        [Route("Create")]
        public ActionResult<ContactInfo> Create(ContactInfo viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.ContactInfo>(viewModel);
                dataModel = _repository.Create(dataModel);
                viewModel = _mapper.Map<ContactInfo>(dataModel);
                // getContactInfoDetails(viewModel);

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
        public ActionResult<ContactInfo> Update(ContactInfo viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.ContactInfo>(viewModel);
                dataModel = _repository.Update(dataModel);
                viewModel = _mapper.Map<ContactInfo>(dataModel);
                // getContactInfoDetails(viewModel);

                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} {viewModel.GetType().Name} failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // DELETE: api/ContactInfo/Delete
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(long id)
        {
            try
            {
                var messageResponse = "ContactInfo Deleted.";
                if (_repository.Delete(id))
                {
                    messageResponse = $"Delete ContactInfo failed.";
                    Log.Error(messageResponse);
                    throw new Exception(messageResponse);
                }
                return new JsonResult(new { messageResponse });
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

    }
}