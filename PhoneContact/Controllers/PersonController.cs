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
using Core.Enums;
using System.Globalization;

namespace PhoneContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPersonRepository _repository;
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        public PersonController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _repository = _repositoryFactory.PersonRepository;
            _contactInfoRepository = _repositoryFactory.ContactInfoRepository;
            _locationRepository = _repositoryFactory.LocationRepository;
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
                    model.ContactInfos = listContactInfos(new ReadByMasterRequest { MasterId = model.Id, MasterUIID = model.UIID }).ToList();
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
                viewModel.ContactInfos = listContactInfos(new ReadByMasterRequest { MasterId = request.Id, MasterUIID = request.UIID }).ToList();
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
        public ActionResult<Person> Create(CreatePersonRequest request)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Person>(request);
                dataModel = _repository.Create(dataModel);
                foreach (var contactInfo in request.ContactInfos)
                {
                    var contatInfoDataModel = _mapper.Map<DataModels.ContactInfo>(contactInfo);
                    contatInfoDataModel.Person = dataModel.Id;
                    contatInfoDataModel = _contactInfoRepository.Create(contatInfoDataModel);
                    if (contactInfo.InfoType == InfoType.Location)
                    {
                        var point = contatInfoDataModel.Information.Split(",");
                        var locationViewModel = new Location
                        {
                            Latitude = Convert.ToDecimal(point[0].Replace(".",",")),
                            Longitude = Convert.ToDecimal(point[1].Replace(".", ",")),
                            ContactInfo = contatInfoDataModel.Id
                        };
                        var locationDataModel = _mapper.Map<DataModels.Location>(locationViewModel);
                        _locationRepository.CreateOrUpdate(locationDataModel);
                    }
                }
                var viewModel = _mapper.Map<Person>(dataModel);
                viewModel.ContactInfos = listContactInfos(new ReadByMasterRequest { MasterId = viewModel.Id, MasterUIID = viewModel.UIID }).ToList();
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // PUT: api/Person/Create
        [HttpPut]
        [Route("Update")]
        public ActionResult<Person> Update(UpdatePersonRequest request)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Person>(request);
                dataModel.ModelCheck(request.UIID);

                dataModel = _repository.Read(request.Id);
                //set properties
                dataModel.FirstName = request.FirstName;
                dataModel.MiddleName = request.MiddleName;
                dataModel.LastName = request.LastName;
                dataModel.CompanyName = request.CompanyName;

                dataModel = _repository.Update(dataModel);
                var viewModel = _mapper.Map<Person>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} Person failed.{ex.Message}";
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

        private IEnumerable<ContactInfo> listContactInfos(ReadByMasterRequest request)
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