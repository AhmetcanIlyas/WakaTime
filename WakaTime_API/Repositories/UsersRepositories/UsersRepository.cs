using Dapper;
using WakaTime_API.Dtos.UsersDtos;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.UsersRepositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly Context _context;
		public UsersRepository(Context context)
		{
			_context = context;
		}

		public async Task CreateUserAsync(CreateUserDto createUserDto)
		{
			DateTime startDate = DateTime.Now;
			string query = "insert into Users (UserName,Role,Email,Password,StartDate,ApiKey,PhoneNumber,Gender,Photo) " +
                "values (@userName,@role,@email,@password,@startDate,@apiKey,@phoneNumber,@gender,@photo)";
			var parameteres = new DynamicParameters();
			parameteres.Add("@userName", createUserDto.UserName);
			parameteres.Add("@role", createUserDto.Role);
			parameteres.Add("@email", createUserDto.Email);
			parameteres.Add("@password", createUserDto.Password);
			parameteres.Add("@startDate", startDate);
			parameteres.Add("@apiKey", createUserDto.ApiKey);
			parameteres.Add("@phoneNumber", createUserDto.PhoneNumber);
			parameteres.Add("@gender", createUserDto.Gender);
			parameteres.Add("@photo", createUserDto.Photo);
			using(var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameteres);
			}
		}

		public async Task<List<ResultUsersDto>> GetAllUsersAsync()
		{
			string query = "Select * From Users";
			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultUsersDto>(query);
				return values.ToList();
			}
		}

        public async Task<ResultUsersDto> GetUserByEmailAsync(string email)
        {
            string query = "Select * From Users Where Email=@email";
			var parameters = new DynamicParameters();
			parameters.Add("@email", email);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QuerySingleAsync<ResultUsersDto>(query,parameters);
				return values;
            }
        }

        public async Task<ResultUsersDto> GetUserByUserIdAsync(int userId)
        {
            string query = "Select * From Users Where UserId=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", userId);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QuerySingleAsync<ResultUsersDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            string query = "Update Users Set UserName=@userName,Email=@email,PhoneNumber=@phoneNumber where UserId=@userId";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", updateUserDto.UserId);
			parameters.Add("@userName", updateUserDto.UserName);
			parameters.Add("@email", updateUserDto.Email);
			parameters.Add("@phoneNumber", updateUserDto.PhoneNumber);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query,parameters);
			}
        }
    }
}
