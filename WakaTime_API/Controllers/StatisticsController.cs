using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.StatisticsRepositories;

namespace WakaTime_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet("TotalWorkedProjectCount")]
        public async Task<IActionResult> TotalWorkedProjectCountAsync(int userId)
        {
            var values = await _statisticsRepository.TotalWorkedProjectCountAsync(userId);
            return Ok(values);
        }

        [HttpGet("GetActiveDaysCount")]
        public async Task<IActionResult> GetActiveDaysCountAsync(int userId)
        {
            var values = await _statisticsRepository.GetActiveDaysCountAsync(userId);
            return Ok(values);
        }
    }
}
