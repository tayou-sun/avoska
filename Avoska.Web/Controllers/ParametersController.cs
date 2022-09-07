using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParametersController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IParametersRepository _paramRepo;
        public ParametersController(ILogger<WeatherForecastController> logger,IParametersRepository roomRepository)
        {
            _logger = logger;
            _paramRepo = roomRepository;
        }

        [HttpGet]
        public IEnumerable<Parameters> Get()
        {
            var parameters = _paramRepo.GetAll();
            return parameters;
        }


        
        
    }
}
