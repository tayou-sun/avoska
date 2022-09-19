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
using System.Text;

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

        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            // passwordBuilder.Append(RandomString(4, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            //passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }

        private bool RandomString(int v1, bool v2)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> CheckIs(string phone)
        {
            /*   var res = true;

               userVerify = _orderRepository.GetCurrentUserVerify(phone);

              var userVerify1 = _orderRepository.GetLastMessage(phone);

              if (userVerify == null)
                  return true; */


            var userVerify = _orderRepository.GetCurrentUserVerify(phone);

            var client = new RestClient($"https://gate.smsaero.ru/v2/sms/status?id={userVerify.MessageId}");

            var request = new RestRequest();
            request.AddHeader("Authorization", "Basic ZXVnZW5lLm1hdnJpbkBnbWFpbC5jb206cFhxa3EyZGNEWU5FTHc1bGwxZG42SVE1VFY4WQ==");
            request.AddHeader("Cookie", "_csrf=a55c41aaa56810c8f36d6486d8ca0e0f964c0ea508c8b75bcf75acc2c300f83da%3A2%3A%7Bi%3A0%3Bs%3A5%3A%22_csrf%22%3Bi%3A1%3Bs%3A32%3A%22cakAcz5isCTO4vU4CJ7jvEdRSKi7yjAC%22%3B%7D");
            var response = await client.ExecuteAsync<SmsResponce>(request);

            if ((SmsStatus)response.Data.data.status == SmsStatus.canceled || (SmsStatus)response.Data.data.status == SmsStatus.NotDelivered)
                return true;



            return false;
        }

        [HttpGet("code")]
        public async Task<SmsResultDto> GetCodeAsync(string number)
        {
            // var res = await CheckIfSenAvailable(number);
            var isSendAvailvale = _orderRepository.GetLastMessage(number);

            if (isSendAvailvale == 0)
                return new SmsResultDto()
                {
                    status = "ERROR",
                    status_text = "Лимит исчерпан, повторите через 10 минут"
                    //code = 1234
                };


            //var count = _orderRepository.CheckIsSendAvailale(number);
            var status = "OK";
            var errorText = "";
            /*  if (res)
             { */
            var code = RandomPassword();


            var client = new RestClient($"https://gate.smsaero.ru/v2/sms/send?number={number}&text=Код для входа в Авоську: {code}&sign=SMS Aero");

            var request = new RestRequest();
            request.AddHeader("Authorization", "Basic ZXVnZW5lLm1hdnJpbkBnbWFpbC5jb206cFhxa3EyZGNEWU5FTHc1bGwxZG42SVE1VFY4WQ==");
            request.AddHeader("Cookie", "_csrf=ac1625255f575a1b1d8b272ab117bff0d5379cc7a17fb42ba25f7b7889e1731da%3A2%3A%7Bi%3A0%3Bs%3A5%3A%22_csrf%22%3Bi%3A1%3Bs%3A32%3A%224071YjtXfAc67YdnoYwJ_nd_k_DQ2n_M%22%3B%7D");
            var response = await client.ExecuteAsync<SmsResponce>(request);





            var v = new UserVerify()
            {
                Phone = number,
                Code = long.Parse(code),
                IsVerify = false,
                CreateDate = DateTime.Now,
                MessageId = response.Data.data.id

            };
            _orderRepository.SaveToken(v);
            /*  } */
            /*  else
             {
                 status = "ERROR";
                 errorText = "Неверный код";
             } */
            return new SmsResultDto()
            {
                status = status,
                status_text = errorText
                //code = 1234
            };
        }

    }
}