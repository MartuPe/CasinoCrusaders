using System.Diagnostics;
using Entidades;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioServicio servicio;

        public UsuarioController(IUsuarioServicio servicio)
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
        public IActionResult Registrar(Usuario usuario)
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
