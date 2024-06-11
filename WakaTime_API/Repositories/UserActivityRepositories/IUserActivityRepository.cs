using WakaTime_API.Dtos.UserActivityDtos;

namespace WakaTime_API.Repositories.UserActivityRepositories
{
	public interface IUserActivityRepository
	{
		Task<List<ResultUserActivityDto>> GetAllUserActivityAsync();
		Task<GetByUserIdUserActivityDto> GetByUserIdUserActivityAsync(int userId);
		Task UpdateUserActivityAsync(int userId,float totalSeconds, string totalTimeText, int dailyAverageSeconds, string dailyTimeText);
		Task CreateUserActivityAsync(int userId);
	}
}
