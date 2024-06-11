using Dapper;
using WakaTime_API.Dtos.UserActivityDtos;
using WakaTime_API.Dtos.UsersDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.UserActivityRepositories
{
	public class UserActivityRepository : IUserActivityRepository
	{
		private readonly Context _context;
		public UserActivityRepository(Context context)
		{
			_context = context;
		}

		public async Task CreateUserActivityAsync(int userId)
		{
			int totalSeconds = 0;
			string query = "insert into UserActivity (UserId,TotalSeconds) values (@userId,@totalSeconds)";
			var parametres = new DynamicParameters();
			parametres.Add("@userId", userId);
			parametres.Add("@totalSeconds", totalSeconds);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}

		public async Task<List<ResultUserActivityDto>> GetAllUserActivityAsync()
		{
			string query = "Select * From UserActivity";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultUserActivityDto>(query);
				return values.ToList();
			}
		}

		public async Task<GetByUserIdUserActivityDto> GetByUserIdUserActivityAsync(int userId)
		{
			string query = "Select * From UserActivity Where UserId=@userId";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryFirstOrDefaultAsync<GetByUserIdUserActivityDto>(query, parameters);
				return values;
			}
		}

		public async Task UpdateUserActivityAsync(int userId, float totalSeconds, string totalTimeText, int dailyAverageSeconds, string dailyTimeText)
		{
			string queary = "Update UserActivity Set TotalSeconds=@totalSeconds, TotalTimeText=@totalTimeText, DailyAverageSeconds=@dailyAverageSeconds, DailyTimeText=@dailyTimeText where UserId=@userId";
			var parameters = new DynamicParameters();
			parameters.Add("@totalSeconds", totalSeconds);
			parameters.Add("@totalTimeText", totalTimeText);
			parameters.Add("@userId", userId);
			parameters.Add("@dailyAverageSeconds", dailyAverageSeconds);
			parameters.Add("@dailyTimeText", dailyTimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(queary, parameters);
			}
		}
	}
}
