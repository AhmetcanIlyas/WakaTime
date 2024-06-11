using WakaTime_API.Dtos.DependenceDtos;

namespace WakaTime_API.Repositories.DependenceRepositories
{
	public interface IDependenceRepository
	{
		Task SaveDependenceAsync(int UserId, string DependenceName, float TotalDailySeconds, string TimeText, DateTime Date);
		Task<List<ResultDependenceDto>> ResultDependenceAsync();
		Task<List<GetAllDependenceByUserIdDto>> GetAllDependenceByUserIdAsync(int userId);
		Task<List<ResultDependenceWithUserNameDto>> ResultDependenceWithUserNameAsync();
		Task<List<ResultDependenceDto>> GetDailyDependenceByUserIdAsync(int userId);
	}
}
