using WakaTime_API.Dtos.EditorDtos;

namespace WakaTime_API.Repositories.EditorRepositories
{
	public interface IEditorRepository
	{
		Task SaveEditorAsync(int UserId, string EditorName, float TotalDailySeconds, string TimeText, DateTime Date);
		Task<List<ResultEditorDto>> ResultEditorAsync();
		Task<List<GetAllEditorByUserIdDto>> GetAllEditorByUserIdAsync(int userId);
		Task<List<ResultEditorWithUserNameDto>> ResultEditorWithUserNameAsync();
	}
}
