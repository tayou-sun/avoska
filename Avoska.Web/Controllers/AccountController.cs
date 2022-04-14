using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace TokenApp.Controllers
{




    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IUserRepository _orderRepository;

        /*         private readonly UserManager<User> _userManager;
                private readonly SignInManager<User> _signInManager; */

        public AccountController(ILogger<AccountController> logger, IUserRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            /*  _userManager = userManager;
             _signInManager = signInManager; */
        }


        // тестовые данные вместо использования базы данных
        /*   private List<Person> people = new List<Person>
          {
                new Person {Login="+7", Password="1", Role = "admin" },
              new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
              new Person { Login="qwerty@gmail.com", Password="55555", Role = "user" }
          }; */


        [HttpPost("token")]
        public IActionResult Token(User p)
        {
            var identity = GetIdentity(p.Phone, p.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name,
                userNane = identity.FindFirst("UserName").Value,
                address = identity.FindFirst("Address").Value,
                phone = identity.FindFirst("Phone").Value,
                //id = identity.
            };

            return Json(response);
        }

       
        [HttpPost("register")]
        public IActionResult Register(User p)
        {
            var identity = CreateIdentity(p.Phone, p.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                login = identity.Name,
                userNane = identity.FindFirst("UserName").Value,
                address = identity.FindFirst("Address").Value,
                phone = identity.FindFirst("Phone").Value,
                //id = identity.
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string phone, string password)
        {
            var person = _orderRepository.Get(phone, password);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login ?? ""),
                       new Claim("Id", person.Id.ToString()),
                         new Claim("Phone", person.Phone ?? ""),
                          new Claim("Address", person.Address ?? ""),
                           new Claim("UserName", person.Name ?? ""),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }


          private ClaimsIdentity CreateIdentity(string phone, string password)
        {
            var person = _orderRepository.Create(phone, password);

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login ?? ""),
                       new Claim("Id", person.Id.ToString()),
                         new Claim("Phone", person.Phone ?? ""),
                          new Claim("Address", person.Address ?? ""),
                           new Claim("UserName", person.Name ?? ""),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }



        [Authorize]
        [HttpGet("a")]
        public int PostMessage1()
        {

            var a = 1 + 1;
            return 1;
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult Update(User user)
        {

            var res = _orderRepository.Update(user);


            var response = new
            {

                login = res.Login,
                userNane = res.Name,
                address = res.Address,
                phone = res.Phone,
                //id = identity.
            };

            return Json(response);
        }
    }
}