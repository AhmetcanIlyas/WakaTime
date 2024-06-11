using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.DailyUserActivityRepositories;

namespace WakaTime_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyUserActivityController : ControllerBase
    {
        private readonly IDailyUserActivityRespository _dailyUserActivityRespository;

        public DailyUserActivityController(IDailyUserActivityRespository dailyUserActivityRespository)
        {
            _dailyUserActivityRespository = dailyUserActivityRespository;
        }

        [HttpGet("GetLast12DayDailyUserActivity")]
        public async Task<IActionResult> GetLast12DayDailyUserActivityAsync(int userId)
        {
            var values = await _dailyUserActivityRespository.GetLast12DayDailyUserActivityAsync(userId);
            return Ok(values);
        }
    }
}
