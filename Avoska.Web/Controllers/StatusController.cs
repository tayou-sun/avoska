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
    public class StatusController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        public StatusController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }



        [HttpGet("detail")]
        public ProductDto GetDetailById(int id)
        {
            //_productRepository.Create(null);
            var products = _productRepository.GetDetailById(id);
            return products;
        }

        [HttpGet("current")]
        public ProductDto GetCurrentOrderForUser(int id)
        {
            //_productRepository.Create(null);
            var products = _productRepository.GetDetailById(id);
            return products;
        }


    }
}
