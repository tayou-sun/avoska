﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
         Counter counter = Metrics.CreateCounter("my_counter","Metrics counter");

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IProductRepository _productRepo;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IProductRepository roomRepository)
        {
            _logger = logger;
            _productRepo = roomRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
                counter.Inc(); // Increment the counter
            var rng = new Random();
            var a = _productRepo.GetProducts();
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
