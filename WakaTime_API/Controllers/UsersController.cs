using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Dtos.UsersDtos;
using WakaTime_API.Repositories.UsersRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUsersRepository _usersRepository;

		public UsersController(IUsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		[HttpGet("ResultUserList")]
		public async Task<IActionResult> ResultUserListAsync()
		{
			var values = await _usersRepository.GetAllUsersAsync();
			return Ok(values);
		}

		[HttpPost("CreateUser")]
		public async Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto)
		{
			await _usersRepository.CreateUserAsync(createUserDto);
			return Ok("Kullanıcı Eklendi");
		}

		[HttpGet("GetUserByEmail")]
		public async Task<IActionResult> GetUserByEmailAsync(string email)
		{
			var values = await _usersRepository.GetUserByEmailAsync(email);
			return Ok(values);
		}

        [HttpGet("GetUserByUserId")]
        public async Task<IActionResult> GetUserByUserIdAsync(int userId)
        {
            var values = await _usersRepository.GetUserByUserIdAsync(userId);
            return Ok(values);
        }

		[HttpPut("UpdateUser")]
		public async Task<IActionResult> UpdateUserAsync(UpdateUserDto updateUserDto)
		{
            await _usersRepository.UpdateUserAsync(updateUserDto);
            return Ok("Güncellendi");
        }
    }
}
