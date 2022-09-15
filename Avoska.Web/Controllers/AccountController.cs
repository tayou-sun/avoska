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
using RestSharp;

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

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
        "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
        "abcdefghijkmnopqrstuvwxyz",    // lowercase
        "0123456789",                   // digits
        "!@$?_-"                        // non-alphanumeric
    };
            var rand = new Random();
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
        [HttpPost("token")]
        public IActionResult Token([FromBody] User1 p)
        {
            var check = _orderRepository.Verify(p);
            if (check == null)
            {
                return BadRequest();
            }
            var identity = GetIdentity(p.Phone);


            if (identity == null)
            {
                // return BadRequest(new { errorText = "Invalid username or password." });

                identity = CreateIdentity(p.Phone, GenerateRandomPassword());
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
        public IActionResult Register([FromBody] User1 p)
        {
            var identity = CreateIdentity(p.Phone, "p.Password");
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

        private ClaimsIdentity GetIdentity(string phone)
        {
            var person = _orderRepository.Get(phone);

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
        public IActionResult Update(UserChange user)
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




        [HttpGet("code")]
        public async Task<SmsResponce> GetCodeAsync(string number)
        {

            var client = new RestClient($"https://sms.ru/code/call?phone={number}&ip=-1&api_id=9281F0C8-148F-DDA7-ED8C-42A0A6218A68");

            var request = new RestRequest();
            //request.AddHeader("Cookie", "dev_id=C5499A72-B294-D150-3947-90543939044B");
            var response = await client.ExecuteAsync<SmsResponce>(request);
            Console.WriteLine(response.Content);


            /*   return response.Data; */

            if (response.Data.status != "ERROR")
            {
                var v = new UserVerify()
                {
                    Phone = number,
                    Code = response.Data.code,
                    IsVerify = false
                };
                _orderRepository.SaveToken(v);
            };
            return new SmsResponce()
            {
                status = response.Data.status,
                //code = 1234
            };
        }

    }
}