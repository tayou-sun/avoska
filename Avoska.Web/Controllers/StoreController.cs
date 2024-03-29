﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus;
using Telegram.Bot;

namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    { 
        
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStoreRepository _productRepo;
        public StoreController(ILogger<WeatherForecastController> logger,IStoreRepository roomRepository)
        {
            _logger = logger;
            _productRepo = roomRepository;
        }

        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            var rng = new Random();
            var a = _productRepo.GetStores().ToList();
           
            var b = a.Select(x=>x.Tags.FirstOrDefault()).ToList();
            return b;
        }


        
        
    }
}
