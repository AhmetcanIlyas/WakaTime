using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WakaTime_UI.Dtos.DependenceDtos;

namespace WakaTime_UI.Controllers
{
    public class DependenceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DependenceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> PersonalDependence()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Dependencies/GetAllDependenceListByUserId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDependenceDto>>(jsonData);
                
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
        public async Task<IActionResult> TeamDependence()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Dependencies/ResultDependenceWithUserName");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDependenceWithUserNameDto>>(jsonData);
                
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
