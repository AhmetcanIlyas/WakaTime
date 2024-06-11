using Dapper;
using WakaTime_API.Dtos.OperatingSystemDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.OperatingSystemRepositories
{
	public class OperatingSystemRepository : IOperatingSystemRepository
	{
		private readonly Context _context;
		public OperatingSystemRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllOperatingSystemByUserIdDto>> GetAllOperatingSystemByUserIdAsync(int userId)
		{
			string query = "Select * From OperatingSystems Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllOperatingSystemByUserIdDto>(query, parameters);
				return values.ToList();
			}
		}

        public async Task<ResultOperatingSystemDto> GetOperatingSystemLastDayAsync(int userId)
        {
			string query = "SELECT TOP 1 * FROM OperatingSystems WHERE UserId = @userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryFirstAsync<ResultOperatingSystemDto>(query, parameters);
				return values;
			}
        }

        public async Task<List<ResultOperatingSystemDto>> ResultOperatingSystemAsync()
		{
			string query = "Select * From OperatingSystems ORDER BY Date DESC";
			using (var connectin = _context.CreateConnection())
			{
				var values = await connectin.QueryAsync<ResultOperatingSystemDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultOperatingSystemWithUserNamedDto>> ResultOperatingSystemWithUserNameAsync()
        {
            string query = "SELECT O.*, U.UserName FROM OperatingSystems O INNER JOIN Users U ON O.UserId = U.UserId ORDER BY O.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultOperatingSystemWithUserNamedDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveOperatingSystemAsync(int UserId, string OperatingSystemName, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO OperatingSystems AS target USING (VALUES (@date, @userId, @operatingSystemName)) AS source (Date, UserId, OperatingSystemName) " +
				"ON target.Date = source.Date AND target.UserId = source.UserId AND target.OperatingSystemName = source.OperatingSystemName WHEN MATCHED THEN " +
                "UPDATE SET TotalDailySeconds = @totalDailySeconds, TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, OperatingSystemName, TotalDailySeconds, TimeText) " +
                "VALUES (@date, @userId, @operatingSystemName, @totalDailySeconds, @timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@operatingSystemName", OperatingSystemName);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
