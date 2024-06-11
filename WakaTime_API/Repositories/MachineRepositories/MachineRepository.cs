using Dapper;
using WakaTime_API.Dtos.MachineDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.MachineRepositories
{
	public class MachineRepository : IMachineRepository
	{
		private readonly Context _context;
		public MachineRepository(Context context)
		{
			_context = context;
		}

		public async Task<List<GetAllMachineByUserIdDto>> GetAllMachineByUserIdAsync(int userId)
		{
			string query = "Select * From Machines Where UserId=@userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<GetAllMachineByUserIdDto>(query,parameters);
				return values.ToList();
			}
		}

        public async Task<ResultMachineDto> GetMachineLastDayAsync(int userId)
        {
			string query = "SELECT TOP 1 *FROM Machines WHERE UserId = @userId ORDER BY Date DESC";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);
			using (var connection = _context.CreateConnection()) 
			{
				var values = await connection.QueryFirstAsync<ResultMachineDto>(query, parameters);
				return values;
			}
        }

        public async Task<List<ResultMachineDto>> ResultMachineAsync()
		{
			string query = "Select * From Machines ORDER BY Date DESC";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultMachineDto>(query);
				return values.ToList();
			}
		}

        public async Task<List<ResultMachineWithUserNameDto>> ResultMachineWithUserNameAsync()
        {
            string query = "SELECT M.*, U.UserName FROM Machines M INNER JOIN Users U ON M.UserId = U.UserId ORDER BY M.Date DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultMachineWithUserNameDto>(query);
                return values.ToList();
            }
        }

        public async Task SaveMachineAsync(int UserId, string MachineName, string MachineId, float TotalDailySeconds, string TimeText, DateTime Date)
		{
			string query = "MERGE INTO Machines AS target USING (VALUES (@date, @userId, @machineName, @machineId)) AS source (Date, UserId, MachineName, MachineId) " +
			"ON target.Date = source.Date AND target.UserId = source.UserId AND target.MachineName = source.MachineName AND target.MachineId = source.MachineId WHEN MATCHED THEN " +
            "UPDATE SET TotalDailySeconds = @totalDailySeconds, TimeText=@timeText WHEN NOT MATCHED THEN INSERT (Date, UserId, MachineName,MachineId, TotalDailySeconds, TimeText) " +
            "VALUES (@date, @userId, @machineName,@machineId, @totalDailySeconds, @timeText);";
			var parametres = new DynamicParameters();
			parametres.Add("@date", Date);
			parametres.Add("@userId", UserId);
			parametres.Add("@machineName", MachineName);
			parametres.Add("@machineId", MachineId);
			parametres.Add("@totalDailySeconds", TotalDailySeconds);
			parametres.Add("@timeText", TimeText);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parametres);
			}
		}
	}
}
