using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITagRepository _productRepo;
        public TagController(ILogger<WeatherForecastController> logger,ITagRepository roomRepository)
        {
            _logger = logger;
            _productRepo = roomRepository;
        }

        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            var rng = new Random();
            var a = _productRepo.GetTags().OrderBy(x=>x.OrderId).ToList();
            var c =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return a;
        }


        
        [HttpGet("children")]
        public IEnumerable<Tag> GetChildByParentTagId(int id)
        {
            var rng = new Random();
            var a = _productRepo.GetChildsByParentId(id).ToList();
    
            return a;
        }
    }
}
