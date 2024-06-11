using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using WakaTime_UI.Dtos.LoginDtos;
using static System.Net.WebRequestMethods;
using WakaTime_UI.Dtos.UsersDtos;

namespace WakaTime_UI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ControlDto controlDto) 
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:44370/api/Login/LoginControl?email={controlDto.Email}&password={controlDto.Password}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var isValidUser = JsonConvert.DeserializeObject<bool>(jsonData);
                if (isValidUser)
                {
                    var jsonDataUser = await client.GetAsync($"https://localhost:44370/api/Users/GetUserByEmail?email={controlDto.Email}");
                    var jsonUser = await jsonDataUser.Content.ReadAsStringAsync();
                    var userData = JsonConvert.DeserializeObject<ResultUserDto>(jsonUser);
                    HttpContext.Session.SetInt32("UserId", userData.UserId);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userData.UserId.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    var responseRefresh = await client.PostAsync("https://localhost:44370/api/SaveDataWakaTime",null);
                    if (responseRefresh.IsSuccessStatusCode)
                    {
                        TempData["SuccesMessage"] = "Welcome";
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong data is not being refreshed";
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect username or password";
                    return View(controlDto);
                }
            }
            return View();
        }

    }
}
