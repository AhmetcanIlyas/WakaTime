using WakaTime_API.Dtos.DailyUserActivityDtos;

namespace WakaTime_API.Repositories.DailyUserActivityRepositories
{
    public interface IDailyUserActivityRespository
    {
        Task SaveDailyUserActivityAsync(int userId, float generalTotalDailyActivitySeconds,string timeText, DateTime date);
        Task<List<ResultDailyUserActivityDto>> GetLast12DayDailyUserActivityAsync(int userId);
    }
}
