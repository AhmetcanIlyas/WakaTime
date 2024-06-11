using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.OperatingSystemRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OperatingSystemsController : ControllerBase
	{
		private readonly IOperatingSystemRepository _operatingSystemRepository;

		public OperatingSystemsController(IOperatingSystemRepository operatingSystemRepository)
		{
			_operatingSystemRepository = operatingSystemRepository;
		}

		[HttpGet("ResultOperatingSystemList")]
		public async Task<IActionResult> ResultOperatingSystemAsync()
		{
			var values = await _operatingSystemRepository.ResultOperatingSystemAsync();
			return Ok(values);
		}

		[HttpGet("GetAllOperatingSystemListByUserId")]
		public async Task<IActionResult> GetAllOperatingSystemByUserId(int userId)
		{
			var values = await _operatingSystemRepository.GetAllOperatingSystemByUserIdAsync(userId);
			return Ok(values);
		}

        [HttpGet("ResultOperatingSystemWithUserName")]
        public async Task<IActionResult> ResultOperatingSystemWithUserNameAsync()
        {
            var values = await _operatingSystemRepository.ResultOperatingSystemWithUserNameAsync();
            return Ok(values);
        }

        [HttpGet("GetOperatingSystemLastDay")]
        public async Task<IActionResult> GetOperatingSystemLastDayAsync(int userId)
        {
            var values = await _operatingSystemRepository.GetOperatingSystemLastDayAsync(userId);
            return Ok(values);
        }
    }
}
