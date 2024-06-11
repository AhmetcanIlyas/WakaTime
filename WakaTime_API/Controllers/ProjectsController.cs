using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.ProjectRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProjectsController : ControllerBase
	{
		private readonly IProjectRepository _projectRepository;

		public ProjectsController(IProjectRepository projectRepository)
		{
			_projectRepository = projectRepository;
		}

		[HttpGet("ResultProjectList")]
		public async Task<IActionResult> ResultProjectAsync()
		{
			var values = await _projectRepository.ResultProjectAsync();
			return Ok(values);
		}

		[HttpGet("GetAllProjectListByUserId")]
		public async Task<IActionResult> GetAllProjectByUserIdAsync(int userId) 
		{
			var values = await _projectRepository.GetAllProjectByUserIdAsync(userId);
			return Ok(values);
		}

		[HttpGet("ResultProjectWithUserName")]
		public async Task<IActionResult> ResultProjectWithUserNameAsync()
		{
			var values = await _projectRepository.ResultProjectWithUserNameAsync();
			return Ok(values);
		}

        [HttpGet("GetLast12DaysProjectByUserId")]
        public async Task<IActionResult> GetLast12DaysProjectByUserIdAsync(int userId)
        {
            var values = await _projectRepository.GetLast12DaysProjectByUserIdAsync(userId);
            return Ok(values);
        }

    }
}
