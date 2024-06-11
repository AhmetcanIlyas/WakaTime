using Dapper;
using WakaTime_API.Dtos.DailyUserActivityDtos;
using WakaTime_API.Models.DapperContext;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WakaTime_API.Repositories.DailyUserActivityRepositories
{
    public class DailyUserActivityRespository : IDailyUserActivityRespository
    {
        private readonly Context _context;
        public DailyUserActivityRespository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultDailyUserActivityDto>> GetLast12DayDailyUserActivityAsync(int userId)
        {
            string query = "SELECT * FROM DailyUserActivity WHERE UserId = @userId AND Date >= DATEADD(DAY, -12, GETDATE()) ORDER BY Date ASC";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDailyUserActivityDto>(query,parameters);
                return values.ToList();
            }
        }

        public async Task SaveDailyUserActivityAsync(int userId, float generalTotalDailyActivitySeconds, string timeText, DateTime date)
        {
            string query = "MERGE INTO DailyUserActivity AS target USING (VALUES (@date, @userId)) AS source (Date, UserId) " +
            "ON target.Date = source.Date AND target.UserId = source.UserId WHEN MATCHED THEN " +
            "UPDATE SET GeneralTotalDailyActivitySeconds = @generalTotalDailyActivitySeconds, TimeText = @timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, GeneralTotalDailyActivitySeconds, TimeText) " +
            "VALUES (@date, @userId, @generalTotalDailyActivitySeconds, @timeText);";
            var parametres = new DynamicParameters();
            parametres.Add("@date", date);
            parametres.Add("@userId", userId);
            parametres.Add("@generalTotalDailyActivitySeconds", generalTotalDailyActivitySeconds);
            parametres.Add("@timeText", timeText);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parametres);
            }
        }
    }
}
