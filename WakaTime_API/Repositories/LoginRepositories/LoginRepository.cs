
using Dapper;
using WakaTime_API.Models.DapperContext;

namespace WakaTime_API.Repositories.LoginRepositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Context _context;
        public LoginRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> LoginControlAsync(string email, string password)
        {
            string query = "SELECT COUNT(*) AS UserCount FROM Users WHERE Email = @email AND Password = @password";
            var parameters = new DynamicParameters();
            parameters.Add("@email", email);
            parameters.Add("@password", password);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.ExecuteScalarAsync<bool>(query, parameters);
                return values;
            }
        }
    }
}
