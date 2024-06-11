using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.MachineRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MachinesController : ControllerBase
	{
		private readonly IMachineRepository _machineRepository;

		public MachinesController(IMachineRepository machineRepository)
		{
			_machineRepository = machineRepository;
		}

		[HttpGet("ResultMachineList")]
		public async Task<IActionResult> ResultMachineAsync()
		{
			var values = await _machineRepository.ResultMachineAsync();
			return Ok(values);
		}

		[HttpGet("GetAllMachineListByUserId")]
		public async Task<IActionResult> GetAllMachineByUserIdAsync(int userId)
		{
			var values = await _machineRepository.GetAllMachineByUserIdAsync(userId);
			return Ok(values);
		}

        [HttpGet("ResultMachineWithUserName")]
        public async Task<IActionResult> ResultMachineWithUserNameAsync()
        {
            var values = await _machineRepository.ResultMachineWithUserNameAsync();
            return Ok(values);
        }

        [HttpGet("GetMachineLastDay")]
        public async Task<IActionResult> GetMachineLastDayAsync(int userId)
        {
            var values = await _machineRepository.GetMachineLastDayAsync(userId);
            return Ok(values);
        }
    }
}
