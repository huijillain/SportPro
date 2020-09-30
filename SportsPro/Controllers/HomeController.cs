using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("About")] //Add Route, change from /home/about to only /about, page 211
        public IActionResult About()
        {
            return View();
        }
    }
}