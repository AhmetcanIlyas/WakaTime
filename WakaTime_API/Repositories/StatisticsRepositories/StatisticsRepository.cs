
using Dapper;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.StatisticsRepositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly Context _context;
        public StatisticsRepository(Context context)
        {
            _context = context;
        }
        public async Task<int> TotalWorkedProjectCountAsync(int userId)
        {
            string query = "SELECT COUNT(DISTINCT ProjectName) AS FarkliProjeSayisi FROM Projects WHERE UserId = @userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QuerySingleAsync<int>(query, parameters);
                return values;
            }
        }
        public async Task<int> GetActiveDaysCountAsync(int userId)
        {
            string query = "SELECT COUNT(DISTINCT Date) AS UniqueDateCount FROM DailyUserActivity WHERE UserId = @userId AND GeneralTotalDailyActivitySeconds <> 0";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<int>(query, parameters);
            }
        }

    }
}
