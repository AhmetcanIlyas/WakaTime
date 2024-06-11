using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.LanguageRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LanguagesController : ControllerBase
	{
		private readonly ILanguageRepository _languageRepository;

		public LanguagesController(ILanguageRepository languageRepository)
		{
			_languageRepository = languageRepository;
		}

		[HttpGet("ResultLanguageList")]
		public async Task<IActionResult> ResultLanguageAsync()
		{
			var values = await _languageRepository.ResultLanguageAsync();
			return Ok(values);
		}

		[HttpGet("GetAllLanguageListByUserId")]
		public async Task<IActionResult> GetAllLanguageByUserIdAsync(int userId)
		{
			var values = await _languageRepository.GetAllLanguageByUserIdAsync(userId);
			return Ok(values);
		}

        [HttpGet("ResultLanguageWithUserName")]
        public async Task<IActionResult> ResultLanguageWithUserNameAsync()
        {
            var values = await _languageRepository.ResultLanguageWithUserNameAsync();
            return Ok(values);
        }

        [HttpGet("GetDailyLanguageByUserId")]
        public async Task<IActionResult> GetDailyLanguageByUserIdAsync(int userId)
        {
            var values = await _languageRepository.GetDailyLanguageByUserIdAsync(userId);
            return Ok(values);
        }
    }
}
