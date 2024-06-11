
using WakaTime_API.Dtos.ProjectDtos;

namespace WakaTime_API.Repositories.ProjectRepositories
{
	public interface IProjectRepository
	{
		Task SaveProjectAsync(int UserId, string ProjectName, float TotalDailySeconds, string TimeText, DateTime Date);
		Task<List<ResultProjectDto>> ResultProjectAsync();
		Task<List<GetAllProjectByUserIdDto>> GetAllProjectByUserIdAsync(int userId);
		Task<List<ResultProjectWithUserNameDto>> ResultProjectWithUserNameAsync();
		Task<List<GetLast12DaysProjectDto>> GetLast12DaysProjectByUserIdAsync(int userId);
	}
}
