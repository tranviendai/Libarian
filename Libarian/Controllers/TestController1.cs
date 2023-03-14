using Microsoft.AspNetCore.Mvc;

namespace Librarian.Controllers
{
    public class TestController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
