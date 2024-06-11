using WakaTime_API.Dtos.LanguageDtos;

namespace WakaTime_API.Repositories.LanguageRepositories
{
	public interface ILanguageRepository
	{
		Task SaveLanguageAsync(int UserId, string LanguageName, float TotalDailySeconds, string TimeText, DateTime Date);
		Task<List<ResultLangluageDto>> ResultLanguageAsync();
		Task<List<GetAllLanguageByUserIdDto>> GetAllLanguageByUserIdAsync(int userId);
		Task<List<ResultLanguageWithUserNameDto>> ResultLanguageWithUserNameAsync();
		Task<List<ResultLangluageDto>> GetDailyLanguageByUserIdAsync(int userId);
	}
}
