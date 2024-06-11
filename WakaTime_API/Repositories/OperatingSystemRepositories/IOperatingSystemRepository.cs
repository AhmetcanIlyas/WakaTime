using WakaTime_API.Dtos.OperatingSystemDtos;

namespace WakaTime_API.Repositories.OperatingSystemRepositories
{
	public interface IOperatingSystemRepository
	{
		Task SaveOperatingSystemAsync(int UserId, string OperatingSystemName, float TotalDailySeconds, string TimeText, DateTime Date);
		Task<List<ResultOperatingSystemDto>> ResultOperatingSystemAsync();
		Task<List<GetAllOperatingSystemByUserIdDto>> GetAllOperatingSystemByUserIdAsync(int userId);
		Task<List<ResultOperatingSystemWithUserNamedDto>> ResultOperatingSystemWithUserNameAsync();
		Task<ResultOperatingSystemDto> GetOperatingSystemLastDayAsync(int userId);
	}
}
