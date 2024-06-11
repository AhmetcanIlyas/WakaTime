using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.UserActivityRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserActivityController : ControllerBase
	{
		private readonly IUserActivityRepository _userActivityRepository;

		public UserActivityController(IUserActivityRepository userActivityRepository)
		{
			_userActivityRepository = userActivityRepository;
		}

		[HttpGet("GetAllUserActivityList")]
		public async Task<IActionResult> GetAllUserActivityAsync()
		{
			var values = await _userActivityRepository.GetAllUserActivityAsync();
			return Ok(values);
		}

		[HttpGet("GetUserActivityByUserId")]
		public async Task<IActionResult> GetUserActivityByUserIdAsync(int userId)
		{
			var values = await _userActivityRepository.GetByUserIdUserActivityAsync(userId);
			return Ok(values);
		}

		[HttpPost("UpdateUserActivity")]
		public async Task<IActionResult> UpdateUserActivityAsync(int userId, float totalSeconds, string totalTimeText, int dailyAverageSeconds, string dailyTimeText)
		{
			await _userActivityRepository.UpdateUserActivityAsync(userId, totalSeconds, totalTimeText, dailyAverageSeconds, dailyTimeText);
			return Ok("Başarıyla Güncellendi");
		}
	}
}
