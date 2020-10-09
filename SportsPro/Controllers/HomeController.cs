using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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