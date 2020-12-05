using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneContact.ViewModels.Requests;
using Repositories;
using Serilog;
using PhoneContact.ViewModels;
using PhoneContact.Extensions;
using PhoneContact.ViewModels.Responses;
using Core.Enums;

namespace PhoneContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IContactInfoRepository _repository;
        private readonly IPersonRepository _personRepository; 
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        public ContactInfoController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _repository = _repositoryFactory.ContactInfoRepository;
            _personRepository = _repositoryFactory.PersonRepository;
            _locationRepository = _repositoryFactory.LocationRepository;
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
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/ContactInfo/Read
        [HttpPost]
        [Route("Read")]
        public ActionResult<ContactInfo> Read(ReadModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);
                dataModel = _repository.Read(request.Id);
                var viewModel = _mapper.Map<ContactInfo>(dataModel);
                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/ContactInfo
        [HttpPost]
        [Route("ListAllByMaster")]
        public ActionResult<IEnumerable<ContactInfo>> ListAllByMaster(ReadByMasterRequest request)
        {
            try
            {
                var master = _personRepository.Read(request.MasterId);
                master.ModelCheck(request.MasterUIID);

                var dataModels = _repository.ListAllByMaster(request.MasterId);
                var viewModels = _mapper.Map<List<ContactInfo>>(dataModels);
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
        public ActionResult<ContactInfo> Create(CreateContactInfoRequest request)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.ContactInfo>(request);
                dataModel = _repository.Create(dataModel);
                if (dataModel.InfoType == InfoType.Location)
                {
                    var point = dataModel.Information.Split(",");
                    var locationViewModel = new Location
                    {
                        Latitude = Convert.ToDecimal(point[0].Replace(".", ",")),
                        Longitude = Convert.ToDecimal(point[1].Replace(".", ",")),
                        ContactInfo = dataModel.Id
                    };
                    var locationDataModel = _mapper.Map<DataModels.Location>(locationViewModel);
                    _locationRepository.CreateOrUpdate(locationDataModel);
                }
                var viewModel = _mapper.Map<ContactInfo>(dataModel);

                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // PUT: api/ContactInfo/Create
        [HttpPut]
        [Route("Update")]
        public ActionResult<ContactInfo> Update(UpdateContactInfoRequest request)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.ContactInfo>(request);
                dataModel.ModelCheck(request.UIID);
                dataModel = _repository.Read(request.Id);
                dataModel.Information = request.Information;
                dataModel = _repository.Update(dataModel);
                if (dataModel.InfoType == InfoType.Location)
                {
                    var point = dataModel.Information.Split(",");
                    var locationViewModel = new Location
                    {
                        Latitude = Convert.ToDecimal(point[0].Replace(".", ",")),
                        Longitude = Convert.ToDecimal(point[1].Replace(".", ",")),
                        ContactInfo = dataModel.Id
                    };
                    var locationDataModel = _mapper.Map<DataModels.Location>(locationViewModel);
                    _locationRepository.CreateOrUpdate(locationDataModel);
                }
                var viewModel = _mapper.Map<ContactInfo>(dataModel);

                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"{MethodBase.GetCurrentMethod().Name} ContactInfo failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

        // POST: api/ContactInfo/Delete
        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(DeleteModelRequest request)
        {
            try
            {
                var dataModel = _repository.Read(request.Id);
                dataModel.ModelCheck(request.UIID);

                var messageResponse = "ContactInfo Deleted.";
                if (!_repository.Delete(request.Id))
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

        // POST: api/ContactInfo/Delete
        [HttpPost]
        [Route("GetNearBy")]
        public ActionResult<GetNearbyCountsResponse> GetNearBy(GetNearbyCountsRequest request)
        {
            try
            {
                var inputModel = _mapper.Map<DataModels.NearbyCountInputModel>(request);
                var dataModel = _repository.GetNearbyCounts(inputModel);
                return _mapper.Map<GetNearbyCountsResponse>(dataModel);
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