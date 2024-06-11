using Microsoft.AspNetCore.Mvc;

namespace WakaTime_UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
