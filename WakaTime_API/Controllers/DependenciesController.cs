using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.CategoryRepositories;
using WakaTime_API.Repositories.DependenceRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DependenciesController : ControllerBase
	{
		private IDependenceRepository _dependenceRepository;

		public DependenciesController(IDependenceRepository dependenceRepository)
		{
			_dependenceRepository = dependenceRepository;
		}

		[HttpGet("ResultDependenceList")]
		public async Task<IActionResult> ResultDependenceListAsync()
		{
			var values = await _dependenceRepository.ResultDependenceAsync();
			return Ok(values);
		}

		[HttpGet("GetAllDependenceListByUserId")]
		public async Task<IActionResult> GetAllDependenceListByUserIdAsync(int userId)
		{
			var values = await _dependenceRepository.GetAllDependenceByUserIdAsync(userId);
			return Ok(values);
		}

        [HttpGet("ResultDependenceWithUserName")]
        public async Task<IActionResult> ResultDependenceWithUserNameAsync()
        {
            var values = await _dependenceRepository.ResultDependenceWithUserNameAsync();
            return Ok(values);
        }

        [HttpGet("GetDailyDependenceByUserId")]
        public async Task<IActionResult> GetDailyDependenceByUserIdAsync(int userId)
        {
            var values = await _dependenceRepository.GetDailyDependenceByUserIdAsync(userId);
            return Ok(values);
        }
    }
}
