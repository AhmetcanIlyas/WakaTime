using WakaTime_API.Dtos.UsersDtos;

namespace WakaTime_API.Repositories.UsersRepositories
{
	public interface IUsersRepository
	{
		Task<List<ResultUsersDto>> GetAllUsersAsync();
		Task CreateUserAsync(CreateUserDto createUserDto);
		Task<ResultUsersDto> GetUserByEmailAsync(string email);
        Task<ResultUsersDto> GetUserByUserIdAsync(int userId);
        Task UpdateUserAsync(UpdateUserDto updateUserDto);
    }
}
