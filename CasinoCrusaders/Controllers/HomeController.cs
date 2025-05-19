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

                // Aqu� puedes guardar el usuario en la base de datos
                // y redirigir a otra acci�n o vista.
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

    }
}
