using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WakaTime_UI.Dtos.CategoryDtos;

namespace WakaTime_UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> PersonalCategory()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Categories/GetAllCategoryListByUserId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetAllCategoryByUserIdDto>>(jsonData);
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
        public async Task<IActionResult> TeamCategory()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Categories/ResultCategoryWithUserName");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryWithUserNameDto>>(jsonData);      
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
