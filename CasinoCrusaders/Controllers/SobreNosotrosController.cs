using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers
{
    public class SobreNosotrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
