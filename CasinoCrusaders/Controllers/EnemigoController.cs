using CasinoCrusaders.ViewModels;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    public class EnemigoController : Controller
    {

        private readonly IEnemigoServicio _enemigoServicio;

        public EnemigoController(IEnemigoServicio enemigoServicio)
        {
            _enemigoServicio = enemigoServicio;
        }

        public IActionResult Enemigos()
        {

            var model = new EnemigosViewModel
            {
                Enemigos = _enemigoServicio.ObtenerListaDeEnemigos(),
                NuevoEnemigo = new Enemigo()
            };

            if (model.Enemigos.Count > 0)
            {
                return View(model);
            }

            ViewBag.Mensaje = "No hay enemigos registrados";
            return View(model);

        }

        [HttpPost]
        public IActionResult RegistrarEnemigo(EnemigosViewModel enemigo)
        {
            if (ModelState.IsValid)
            {
                _enemigoServicio.RegistrarEnemigo(enemigo.NuevoEnemigo);
                return RedirectToAction("Enemigos");
            }

            return RedirectToAction("Enemigos");
        }
    }
}
