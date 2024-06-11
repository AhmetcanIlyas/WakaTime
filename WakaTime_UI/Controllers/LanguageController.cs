using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WakaTime_UI.Dtos.LanguageDtos;

namespace WakaTime_UI.Controllers
{
    public class LanguageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public LanguageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> PersonalLanguage()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Languages/GetAllLanguageListByUserId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLangluageDto>>(jsonData);
                
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
        public async Task<IActionResult> TeamLanguage()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Languages/ResultLanguageWithUserName");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLanguageWithUserNameDto>>(jsonData);
                
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
