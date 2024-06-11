using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WakaTime_UI.Dtos.DailyUserActivityDtos;
using WakaTime_UI.Dtos.DependenceDtos;
using WakaTime_UI.Dtos.LanguageDtos;
using WakaTime_UI.Dtos.MachineDtos;
using WakaTime_UI.Dtos.OperatingSystemDtos;
using WakaTime_UI.Dtos.ProjectDtos;
using WakaTime_UI.Dtos.UserActivityDtos;
using WakaTime_UI.Dtos.UsersDtos;

namespace WakaTime_UI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            if (TempData["SuccesMessage"] != null)
            {
                ViewBag.SuccesMessage = TempData["SuccesMessage"].ToString();
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }

            var client = _httpClientFactory.CreateClient();
            var userId = HttpContext.Session.GetInt32("UserId");
            var response = await client.GetAsync("https://localhost:44370/api/Statistics/TotalWorkedProjectCount?userId=" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<int>(jsonData);
                ViewBag.ProjectCount = values;
            }
            var response2 = await client.GetAsync("https://localhost:44370/api/Statistics/GetActiveDaysCount?userId=" + userId);
            if (response2.IsSuccessStatusCode)
            {
                var jsonData = await response2.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<int>(jsonData);
                ViewBag.ActiveDayCount = values;
            }
            var response3 = await client.GetAsync("https://localhost:44370/api/UserActivity/GetUserActivityByUserId?userId=" + userId);
            if (response3.IsSuccessStatusCode)
            {
                var jsonData = await response3.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByUserIdUserActivityDto>(jsonData);
                ViewBag.TotalTimeText = values.TotalTimeText;
                ViewBag.DailyTimeText = values.DailyTimeText;
            }
            var response4 = await client.GetAsync("https://localhost:44370/api/Users/GetUserByUserId?userId=" + userId);
            if (response4.IsSuccessStatusCode)
            {
                var jsonData = await response4.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);
                ViewBag.Photo = values.Photo;
                ViewBag.UserName = values.UserName;
                ViewBag.Role = values.Role;
            }
            var response5 = await client.GetAsync("https://localhost:44370/api/Machines/GetMachineLastDay?userId=" + userId);
            if (response5.IsSuccessStatusCode)
            {
                var jsonData = await response5.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultMachineDto>(jsonData);
                ViewBag.MachineName = values.MachineName;
                ViewBag.MachineDate = values.Date;
            }
            var response6 = await client.GetAsync("https://localhost:44370/api/OperatingSystems/GetOperatingSystemLastDay?userId=" + userId);
            if (response6.IsSuccessStatusCode)
            {
                var jsonData = await response6.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultOperatingSystemDto>(jsonData);
                ViewBag.OperatingSystemName = values.OperatingSystemName;
                ViewBag.OperatingSystemTime = values.TimeText;
            }
            var response7 = await client.GetAsync("https://localhost:44370/api/DailyUserActivity/GetLast12DayDailyUserActivity?userId=" + userId);
            if (response7.IsSuccessStatusCode)
            {
                var jsonData = await response7.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDailyUserActivityDto>>(jsonData);
                ViewBag.Times = new List<string>();
                ViewBag.Dates = new List<string>();
                foreach (var item in values)
                {
                    int totalSeconds = (int)item.GeneralTotalDailyActivitySeconds;
                    string hours = (totalSeconds / 3600).ToString();
                    string minutes = ((totalSeconds % 3600) / 60).ToString();
                    string hhmm = hours + "." + minutes;
                    ViewBag.Times.Add(hhmm);
                    ViewBag.Dates.Add(item.Date.ToString("dd/MM"));
                }
            }
            var response8 = await client.GetAsync("https://localhost:44370/api/Dependencies/GetDailyDependenceByUserId?userId=" + userId);
            if (response8.IsSuccessStatusCode)
            {
                var jsonData = await response8.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultDependenceDto>>(jsonData);
                ViewBag.DependenceTimeText = new List<string>();
                ViewBag.DependenceName = new List<string>();
                foreach (var item in values)
                {
                    ViewBag.DependenceTimeText.Add(item.TimeText);
                    ViewBag.DependenceName.Add(item.DependenceName);
                }
            }
            var response9 = await client.GetAsync("https://localhost:44370/api/Languages/GetDailyLanguageByUserId?userId=" + userId);
            if (response9.IsSuccessStatusCode)
            {
                var jsonData = await response9.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLangluageDto>>(jsonData);
                ViewBag.LanguageTime = new List<int>();
                ViewBag.LanguageName = new List<string>();
                foreach (var item in values)
                {
                    int minutes = (int)(item.TotalDailySeconds / 60);
                    ViewBag.LanguageTime.Add(minutes);
                    ViewBag.LanguageName.Add(item.LanguageName);
                }
            }
            var response10 = await client.GetAsync("https://localhost:44370/api/Projects/GetLast12DaysProjectByUserId?userId=" + userId);
            if (response10.IsSuccessStatusCode)
            {
                var jsonData = await response10.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<GetLast12DaysProjectDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
