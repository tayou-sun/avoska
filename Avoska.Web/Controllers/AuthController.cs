using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using RestSharp;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Avoska.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IOrderRepository _orderRepository;

/*         private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; */

        public AuthController(ILogger<WeatherForecastController> logger, IOrderRepository orderRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _orderRepository = orderRepository;
           /*  _userManager = userManager;
            _signInManager = signInManager; */
        }

/* 
    
 [HttpPost]
        public async Task<int> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User { UserName = model.Phone};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                   // return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
           return 1;
        }
     */
      /*   [HttpGet]
        public async Task<string> SendCodeAsync(string phone)
        { */
            /* var client = new RestClient("https://sms.ru/sms/send?api_id=18352862-2DE5-708B-0DD2-20DE24C3D83D&to=79998599597&msg=hello+world&json=1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic cGFydHk6cGFycm90");
            IRestResponse response = client.Execute(request);


*/

         /*    User user = new User { PhoneNumber = phone, UserName="test" };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
               
            } */
/* 
        
        var user = _userManager.Users.FirstOrDefault(x=>x.PhoneNumber == phone);
            const string chars = "0123456789";

            var random = new Random();
            var code = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
 */


/*   var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Phone"); */


          /*   var client = new RestClient("https://sms.ru/sms/send?api_id=18352862-2DE5-708B-0DD2-20DE24C3D83D&to="+phone+"&msg=" + code + "&json=1");
            var request = new RestRequest();
            request.AddHeader("Authorization", "Basic cGFydHk6cGFycm90");
            var responce = await client.ExecuteAsync(request); */

        


/* 
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        } */
    }
}