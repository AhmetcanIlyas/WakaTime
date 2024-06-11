using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.WakaTimeRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WakaTimeController : ControllerBase
	{
		private readonly IWakaTimeRepository _wakaTimeRepository;

		public WakaTimeController(IWakaTimeRepository wakaTimeRepository)
		{
			_wakaTimeRepository = wakaTimeRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetWakaTimeResponse(DateTime startDate, string ApiKey)
		{
			var values = await _wakaTimeRepository.GetWakaTimeResponseAsync(startDate, ApiKey);
			return Ok(values);
		}
	}
}
