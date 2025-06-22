using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers
{
    public class SobreNosotrosController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Rol = HttpContext.Session.GetString("Rol");

            return View();
        }
    }
}
