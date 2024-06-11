using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.LoginRepositories;

namespace WakaTime_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpGet("LoginControl")]
        public async Task<IActionResult> LoginControlAsync(string email, string password) 
        {
            var values = await _loginRepository.LoginControlAsync(email, password);
            return Ok(values);
        }
    }
}
