using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace WakaTime_UI.Controllers
{
    public class RefreshController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RefreshController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var referrer = Request.Headers["Referer"].ToString();
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync("https://localhost:44370/api/SaveDataWakaTime",null);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccesMessage"] = "Data Refresh Successful";
                return Redirect(referrer);
            }
            else
            {
                TempData["ErrorMessage"] = "Something Went Wrong";
                return Redirect(referrer);
            }
        }
    }
}