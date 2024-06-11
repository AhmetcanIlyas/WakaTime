using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WakaTime_API.Repositories.EditorRepositories;

namespace WakaTime_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EditorsController : ControllerBase
	{
		private readonly IEditorRepository _editorRepository;

		public EditorsController(IEditorRepository editorRepository)
		{
			_editorRepository = editorRepository;
		}

		[HttpGet("ResultEditor")]
		public async Task<IActionResult> ResultEditorAsync()
		{
			var values = await _editorRepository.ResultEditorAsync();
			return Ok(values);
		}

		[HttpGet("GetAllEditorByUserId")]
		public async Task<IActionResult> GetAllEditorAsync(int userId)
		{
			var values = await _editorRepository.GetAllEditorByUserIdAsync(userId);
			return Ok(values);
		}

        [HttpGet("ResultEditorWithUserName")]
        public async Task<IActionResult> ResultEditorWithUserNameAsync()
        {
            var values = await _editorRepository.ResultEditorWithUserNameAsync();
            return Ok(values);
        }
    }
}
