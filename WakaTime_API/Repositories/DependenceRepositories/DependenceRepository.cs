using Dapper;
using WakaTime_API.Dtos.DependenceDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.DependenceRepositories
{
	public class DependenceRepository : IDependenceRepository
	{
		private readonly Context _context;
		public DependenceRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllDependenceByUserIdDto>> GetAllDependenceByUserIdAsync(int userId)
		{
			string query = "Select * From Dependencies Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllDependenceByUserIdDto>(query, parameters);
				return values.ToList();
			}
		}

        public async Task<List<ResultDependenceDto>> GetDailyDependenceByUserIdAsync(int userId)
        {
			string query = "DECLARE @Today DATE = CAST(GETDATE() AS DATE) SELECT * FROM Dependencies WHERE UserId = @userId AND Date = @Today ORDER BY Id DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultDependenceDto>(query, parameters);
				return values.ToList();
			}
        }

        public async Task<List<ResultDependenceDto>> ResultDependenceAsync()
		{
			string query = "Select * From Dependencies ORDER BY Date DESC";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultDependenceDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultDependenceWithUserNameDto>> ResultDependenceWithUserNameAsync()
        {
            string query = "SELECT D.*, U.UserName FROM Dependencies D INNER JOIN Users U ON D.UserId = U.UserId ORDER BY D.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDependenceWithUserNameDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveDependenceAsync(int UserId, string DependenceName, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO Dependencies AS target USING (VALUES (@date, @userId, @dependenceName)) AS source (Date, UserId, DependenceName) " +
			"ON target.Date = source.Date AND target.UserId = source.UserId AND target.DependenceName = source.DependenceName WHEN MATCHED THEN " +
            "UPDATE SET TotalDailySeconds = @totalDailySeconds, TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, DependenceName, TotalDailySeconds,TimeText) " +
            "VALUES (@date, @userId, @dependenceName, @totalDailySeconds, @timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@dependenceName", DependenceName);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
