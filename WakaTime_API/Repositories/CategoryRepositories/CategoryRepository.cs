using Dapper;
using WakaTime_API.Dtos.CategoryDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.CategoryRepositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly Context _context;
		public CategoryRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllCategoryByUserIdDto>> GetAllCategoryByUserIdAsync(int userId)
		{
			string query = "Select * From Categories Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllCategoryByUserIdDto>(query, parameters);
				return values.ToList();
			}
		}

		public async Task<List<ResultCategoryDto>> ResultCategoryAsync()
		{
			string query = "Select * From Categories ORDER BY Date DESC";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultCategoryDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultCategoryWithUserNameDto>> ResultCategoryWithUserNameAsync()
        {
            string query = "SELECT C.*, U.UserName FROM Categories C INNER JOIN Users U ON C.UserId = U.UserId ORDER BY C.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryWithUserNameDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveCategoryAsync(int UserId, string CategoryName, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO Categories AS target USING (VALUES (@date, @userId, @categoryName)) AS source (Date, UserId, CategoryName) " +
			"ON target.Date = source.Date AND target.UserId = source.UserId AND target.CategoryName = source.CategoryName WHEN MATCHED THEN " +
            "UPDATE SET TotalDailySeconds = @totalDailySeconds, TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, CategoryName, TotalDailySeconds,TimeText) " +
            "VALUES (@date, @userId, @categoryName, @totalDailySeconds,@timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@categoryName", CategoryName);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
