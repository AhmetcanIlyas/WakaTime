using Dapper;
using WakaTime_API.Dtos.ProjectDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.ProjectRepositories
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly Context _context;
		public ProjectRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllProjectByUserIdDto>> GetAllProjectByUserIdAsync(int userId)
		{
			string query = "Select * From Projects Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllProjectByUserIdDto>(query,parameters);
				return values.ToList();
			}
		}

        public async Task<List<GetLast12DaysProjectDto>> GetLast12DaysProjectByUserIdAsync(int userId)
        {
			string query = "SELECT ProjectName,TotalDailySeconds,TimeText, Date FROM Projects WHERE UserId = @userId AND Date >= DATEADD(DAY, -5, GETDATE()) AND Date <= GETDATE() ORDER BY ProjectName, Date";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetLast12DaysProjectDto>(query, parameters);
				return values.ToList();
            }
        }

        public async Task<List<ResultProjectDto>> ResultProjectAsync()
		{
			string query = "Select * From Projects ORDER BY Date DESC";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultProjectDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultProjectWithUserNameDto>> ResultProjectWithUserNameAsync()
        {
            string query = "SELECT P.*, U.UserName FROM Projects P INNER JOIN Users U ON P.UserId = U.UserId ORDER BY P.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProjectWithUserNameDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveProjectAsync(int UserId, string ProjectName, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO Projects AS target USING (VALUES (@date, @userId, @projectName)) AS source (Date, UserId, ProjectName) " +
				"ON target.Date = source.Date AND target.UserId = source.UserId AND target.ProjectName = source.ProjectName WHEN MATCHED THEN " +
                "UPDATE SET TotalDailySeconds = @totalDailySeconds, TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, ProjectName, TotalDailySeconds, TimeText) " +
                "VALUES (@date, @userId, @projectName, @totalDailySeconds, @timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@projectName", ProjectName);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
