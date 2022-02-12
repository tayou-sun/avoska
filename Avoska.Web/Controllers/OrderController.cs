using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using RestSharp;


namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOrderRepository _orderRepository;
        public OrderController(ILogger<WeatherForecastController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }


        public int Post(OrderDto order)
        {
            var res = _orderRepository.Create(order);
            var bot = new TelegramService();
            bot.SendMessage(order, res);


            var client = new RestClient("http://api.callmebot.com/start.php?source=web&user=@shainurova_e&text=hello%20everyone&lang=en-US-Standard-B");

            var request = new RestRequest();
            request.AddHeader("Authorization", "Basic cGFydHk6cGFycm90");
            var response =  client.ExecuteAsync(request);

            var client1 = new RestClient("http://api.callmebot.com/start.php?source=web&user=@eugenemavrin&text=hello%20everyone&lang=en-US-Standard-B");
            var request1 = new RestRequest();
            request1.AddHeader("Authorization", "Basic cGFydHk6cGFycm90");
            var response1 =  client1.ExecuteAsync(request1);



            //Console.WriteLine(response.Content);

            return 1;
        }



    }
}
