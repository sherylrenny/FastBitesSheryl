using Microsoft.AspNetCore.Mvc;

namespace FastBites.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
