namespace WakaTime_API.Repositories.StatisticsRepositories
{
    public interface IStatisticsRepository
    {
        Task<int> TotalWorkedProjectCountAsync(int userId);
        Task<int> GetActiveDaysCountAsync(int userId);
    }
}
