using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.SaveDataWakaTimeRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SaveDataWakaTimeController : ControllerBase
	{
		private readonly ISaveDataWakaTimeRepository _saveDataWakaTimeRepository;

		public SaveDataWakaTimeController(ISaveDataWakaTimeRepository saveDataWakaTimeRepository)
		{
			_saveDataWakaTimeRepository = saveDataWakaTimeRepository;
		}

		[HttpPost]
		public async Task<IActionResult> SaveDataWakaTimeAsync()
		{
			await _saveDataWakaTimeRepository.SaveDataForAllUser();
			return Ok("Kayıt Başarılı");
		}
	}
}
