using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Entity;
using Threenine.Data;


namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork _UOW;
       
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _UOW = uow;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var repo = _UOW.GetRepository<Person>(); 
            return repo.GetList().ToList<Person>();
         //   return repo.GetListPaging(null,null,null,0,10000, true).Items.ToList();
        }
        [HttpPost]
        [Route("save")]
        public ActionResult WriteData(PersonBindingModel p) {
            _UOW.GetRepository<Person>().Add(new Person
            {
                FirstName = p.FirstName,
                Email = p.Email,
                LastName = p.LastName,
                Profile = p.Profile,
                TagLine = p.TagLine,
                Title = p.Title
            });
            _UOW.SaveChanges();
            _UOW.Dispose();
                return Ok();
        }
    }
    public class PersonBindingModel
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }

        public string TagLine { get; set; }
        public string Title { get; set; }
    }
}
