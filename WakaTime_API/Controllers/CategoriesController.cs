using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WakaTime_API.Repositories.CategoryRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet("ResultCategoryList")]
		public async Task<IActionResult> ResultCategoryListAsync()
		{
			var values = await _categoryRepository.ResultCategoryAsync();
			return Ok(values);
		}

		[HttpGet("GetAllCategoryListByUserId")]
		public async Task<IActionResult> GetAllCategoryListByUserIdAsync(int userId)
		{
			var values = await _categoryRepository.GetAllCategoryByUserIdAsync(userId);
			return Ok(values);
		}

        [HttpGet("ResultCategoryWithUserName")]
        public async Task<IActionResult> ResultCategoryWithUserNameAsync()
        {
            var values = await _categoryRepository.ResultCategoryWithUserNameAsync();
            return Ok(values);
        }
    }
}
