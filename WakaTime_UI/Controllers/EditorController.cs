using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WakaTime_UI.Dtos.EditorDtos;

namespace WakaTime_UI.Controllers
{
    public class EditorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> PersonalEditor()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Editors/GetAllEditorByUserId?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEditorDto>>(jsonData);
                
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
        public async Task<IActionResult> TeamEditor()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:44370/api/Editors/ResultEditorWithUserName");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultEditorWithUserNameDto>>(jsonData);
                
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
