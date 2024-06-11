using Dapper;
using WakaTime_API.Dtos.EditorDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.EditorRepositories
{
	public class EditorRepository : IEditorRepository
	{
		private readonly Context _context;
		public EditorRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllEditorByUserIdDto>> GetAllEditorByUserIdAsync(int userId)
		{
			string query = "Select * From Editors Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllEditorByUserIdDto>(query,parameters);
				return values.ToList();
			}
		}

		public async Task<List<ResultEditorDto>> ResultEditorAsync()
		{
			string query = "Select * From Editors ORDER BY Date DESC";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultEditorDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultEditorWithUserNameDto>> ResultEditorWithUserNameAsync()
        {
            string query = "SELECT E.*, U.UserName FROM Editors E INNER JOIN Users U ON E.UserId = U.UserId ORDER BY E.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEditorWithUserNameDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveEditorAsync(int UserId, string EditorName, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO Editors AS target USING (VALUES (@date, @userId, @editorName)) AS source (Date, UserId, EditorName) " +
			"ON target.Date = source.Date AND target.UserId = source.UserId AND target.EditorName = source.EditorName WHEN MATCHED THEN " +
            "UPDATE SET TotalDailySeconds = @totalDailySeconds,TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, EditorName, TotalDailySeconds,TimeText) " +
            "VALUES (@date, @userId, @editorName, @totalDailySeconds,@timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@editorName", EditorName);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
