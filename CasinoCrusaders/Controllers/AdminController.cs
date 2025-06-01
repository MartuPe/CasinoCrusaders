using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
