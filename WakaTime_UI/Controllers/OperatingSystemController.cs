using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WakaTime_UI.Dtos.OperatingSystemDtos;

namespace WakaTime_UI.Controllers
{
    public class OperatingSystemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OperatingSystemController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> PersonalOperatingSystem()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/OperatingSystems/GetAllOperatingSystemListByUserId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOperatingSystemDto>>(jsonData);
                
                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                }
                if (TempData["SuccesMessage"] != null)
                {
                    ViewBag.SuccesMessage = TempData["SuccesMessage"].ToString();
                }
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TeamOperatingSystem()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/OperatingSystems/ResultOperatingSystemWithUserName");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOperatingSystemWithUserNamedDto>>(jsonData);
                
                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                }
                if (TempData["SuccesMessage"] != null)
                {
                    ViewBag.SuccesMessage = TempData["SuccesMessage"].ToString();
                }
                return View(values);
            }
            return View();
        }
    }
}
