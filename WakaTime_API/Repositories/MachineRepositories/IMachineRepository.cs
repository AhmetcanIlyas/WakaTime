using WakaTime_API.Dtos.MachineDtos;

namespace WakaTime_API.Repositories.MachineRepositories
{
	public interface IMachineRepository
	{
		Task SaveMachineAsync(int UserId, string MachineName, string MachineId, float TotalDailySeconds, string TimeText, DateTime Date);
		Task<List<ResultMachineDto>> ResultMachineAsync();
		Task<List<GetAllMachineByUserIdDto>> GetAllMachineByUserIdAsync(int userId);
		Task<List<ResultMachineWithUserNameDto>> ResultMachineWithUserNameAsync();
		Task<ResultMachineDto> GetMachineLastDayAsync(int userId);

    }
}
