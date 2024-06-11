using WakaTime_API.Dtos.CategoryDtos;

namespace WakaTime_API.Repositories.CategoryRepositories
{
	public interface ICategoryRepository
	{
		Task SaveCategoryAsync(int UserId, string CategoryName, float TotalDailySeconds,string TimeText, DateTime Date);
		Task<List<ResultCategoryDto>> ResultCategoryAsync();
		Task<List<GetAllCategoryByUserIdDto>> GetAllCategoryByUserIdAsync(int userId);
        Task<List<ResultCategoryWithUserNameDto>> ResultCategoryWithUserNameAsync();
    }
}
