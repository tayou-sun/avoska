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
    public class ProductController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<ProductDto> GetByTagId(int tagId, int mode)
        {
            //_productRepository.Create(null);
            var products = _productRepository.GetProductsByChildTagId(tagId, mode).ToList();
            return products;
        }


        [HttpGet("search")]
        public IEnumerable<Product> GetProductsByName(string name, int sort)
        {
            var a = 2;
            return _productRepository.GetProductsByName(name, sort);
        }
        [HttpPost]
        public int Post(List<Product> criterionValues)
        {

            /* var id = _inspectionRepository.Start();
            foreach (var val in criterionValues)
            {
                //val.InspectionId = id;
                _creterionRepo.Create(val, id);
            } */
            return 1;
        }





   [HttpGet("detail")]
        public ProductDto GetDetailById(int id)
        {
            //_productRepository.Create(null);
            var products = _productRepository.GetDetailById(id);
            return products;
        }


    }
}
