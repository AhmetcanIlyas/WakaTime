using Microsoft.AspNetCore.Mvc;

namespace WakaTime_UI.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
