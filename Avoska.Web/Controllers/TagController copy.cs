using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;
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
        public TagController(ILogger<WeatherForecastController> logger, ITagRepository roomRepository)
        {
            _logger = logger;
            _productRepo = roomRepository;
        }

        Counter counter = Metrics.CreateCounter("my_counter", "Metrics counter");


        [HttpGet]
        public IEnumerable<Tag> Get()
        { 
            counter.Inc(); // Increment the counter
            var rng = new Random();
            var a = _productRepo.GetTags().OrderBy(x => x.OrderId).ToList();
            var c = Enumerable.Range(1, 5).Select(index => new WeatherForecast
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
