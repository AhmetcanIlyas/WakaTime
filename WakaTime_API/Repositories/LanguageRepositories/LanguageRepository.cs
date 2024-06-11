using Dapper;
using WakaTime_API.Dtos.LanguageDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.LanguageRepositories
{
	public class LanguageRepository : ILanguageRepository
	{
		private readonly Context _context;
		public LanguageRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllLanguageByUserIdDto>> GetAllLanguageByUserIdAsync(int userId)
		{
			string query = "Select * From Languages Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllLanguageByUserIdDto>(query, parameters);
				return values.ToList();
			}
		}

        public async Task<List<ResultLangluageDto>> GetDailyLanguageByUserIdAsync(int userId)
        {
			string query = "SELECT * FROM Languages WHERE UserId = @userId AND CAST(Date AS DATE) = CAST(GETDATE() AS DATE)";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultLangluageDto>(query, parameters);
				return values.ToList();
			}
        }

        public async Task<List<ResultLangluageDto>> ResultLanguageAsync()
		{
			string query = "Select * From Languages ORDER BY Date DESC";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultLangluageDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultLanguageWithUserNameDto>> ResultLanguageWithUserNameAsync()
        {
            string query = "SELECT L.*, U.UserName FROM Languages L INNER JOIN Users U ON L.UserId = U.UserId ORDER BY L.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLanguageWithUserNameDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveLanguageAsync(int UserId, string LanguageName, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO Languages AS target USING (VALUES (@date, @userId, @languageName)) AS source (Date, UserId, LanguageName) " +
			"ON target.Date = source.Date AND target.UserId = source.UserId AND target.LanguageName = source.LanguageName WHEN MATCHED THEN " +
            "UPDATE SET TotalDailySeconds = @totalDailySeconds,TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, LanguageName, TotalDailySeconds,TimeText) " +
            "VALUES (@date, @userId, @languageName, @totalDailySeconds,@timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@languageName", LanguageName);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
