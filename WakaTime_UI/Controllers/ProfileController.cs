using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WakaTime_UI.Dtos.UsersDtos;

namespace WakaTime_UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            if (TempData["SuccesMessage"] != null)
            {
                ViewBag.SuccesMessage = TempData["SuccesMessage"].ToString();
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Users/GetUserByUserId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUserDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44370/api/Users/UpdateUser", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccesMessage"] = "Update Successful";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
