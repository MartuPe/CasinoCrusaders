using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GestionUsuarios()
        {
            return View(); 
        }

        public IActionResult ModificarEnemigos()
        {
            return View(); 
        }

        public IActionResult ModificarObjetos()
        {
            return View();
        }

        public IActionResult ModificarEventos()
        {
            return View(); 
        }
    }
}
