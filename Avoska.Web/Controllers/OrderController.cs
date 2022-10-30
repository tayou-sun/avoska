using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using RestSharp;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Metrics;
using Prometheus;

namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        Counter order_counter = Metrics.CreateCounter("create_order_count", "Create order");
        Counter get_order_count = Metrics.CreateCounter("get_order_count", "Get orders");

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOrderRepository _orderRepository;


        public OrderController(ILogger<WeatherForecastController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }


        public int Post(OrderDto order)
        {
            order_counter.Inc();
            var res = _orderRepository.Create(order);

            var user = res.User;
            var bot = new TelegramService();
            bot.SendMessageAsync(order, res);


            var client = new RestClient("http://api.callmebot.com/start.php?source=web&user=@shainurova_e&text=hello%20everyone&lang=en-US-Standard-B");

            var request = new RestRequest();
            request.AddHeader("Authorization", "Basic cGFydHk6cGFycm90");
            var response = client.ExecuteAsync(request);

            /*    var client1 = new RestClient("http://api.callmebot.com/start.php?source=web&user=@eugenemavrin&text=hello%20everyone&lang=en-US-Standard-B");
               var request1 = new RestRequest();
               request1.AddHeader("Authorization", "Basic cGFydHk6cGFycm90");
               var response1 =  client1.ExecuteAsync(request1); */



            //Console.WriteLine(response.Content);

            return 1;
        }

        [Authorize]
        [HttpGet("a")]
        public List<OrderDto> PostMessage(string login)
        {
            get_order_count.Inc();
            //var bot = new TelegramService();
            var a = _orderRepository.GetOrdersByUserPhone(login);

            return a;


        }


    }
}
