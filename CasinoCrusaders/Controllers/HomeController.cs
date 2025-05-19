using System.Diagnostics;
using CasinoCrusaders.Models;
using Entidades;
using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Registar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                // Aquí puedes guardar el usuario en la base de datos
                // y redirigir a otra acción o vista.
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

    }
}
