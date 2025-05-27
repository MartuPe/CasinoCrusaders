using System.Diagnostics;
using Entidades;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    public class HomeController : Controller
    {
        IUsuarioServicio servicio;

        public HomeController(IUsuarioServicio servicio)
        {
            this.servicio = servicio;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                servicio.AgregarUsuario(usuario);
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

    }
}
