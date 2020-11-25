using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Serilog;

namespace PhoneContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public PersonController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _personRepository = _repositoryFactory.PersonRepository;

        }


        // GET: api/Person
        [HttpGet]
        [Route("ListAll")]
        public ActionResult<IEnumerable<Person>> ListAll()
        {
            try
            {
                var dataModels = _personRepository.ListAll();
                var viewModels = _mapper.Map<List<Person>>(dataModels);
                //viewModels.ForEach(viewModel =>
                //{
                //    // getPersonDetails(viewModel);
                //});
                return viewModels;
            }
            catch (Exception ex)
            {
                var messageResponse = $"List Persons failed.{ex.Message}";
                Log.Error(messageResponse);
                throw;
            }
        }

        // PUT: api/Person/Create
        [HttpPost]
        [Route("Create")]
        public ActionResult<Person> Create(Person viewModel)
        {
            try
            {
                var dataModel = _mapper.Map<DataModels.Person>(viewModel);
                dataModel = _personRepository.Create(dataModel);
                viewModel = _mapper.Map<Person>(dataModel);
                // getPersonDetails(viewModel);

                return viewModel;
            }
            catch (Exception ex)
            {
                var messageResponse = $"Create Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw;
            }
        }

        // DELETE: api/Article/Delete
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(long id)
        {
            try
            {
                var messageResponse = "Person Deleted.";
                if (_personRepository.Delete(id))
                {
                    messageResponse = $"Delete Person failed.";
                    Log.Error(messageResponse);
                    throw new Exception(messageResponse);
                }
                return new JsonResult(new { messageResponse });
            }
            catch (Exception ex)
            {
                var messageResponse = $"Delete Person failed.{ex.Message}";
                Log.Error(messageResponse);
                throw new Exception(messageResponse);
            }
        }

    }
}