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

        [HttpPost]
        public IActionResult EditarEnemigo(Enemigo enemigo)
        {
            if (ModelState.IsValid)
            {
                _enemigoServicio.EditarEnemigo(enemigo);
                return RedirectToAction("Enemigos");
            }

            
            var model = new EnemigosViewModel
            {
                Enemigos = _enemigoServicio.ObtenerListaDeEnemigos(),
                NuevoEnemigo = enemigo 
            };

            ViewBag.MensajeError = "Error al editar el enemigo. Verifica los datos ingresados.";

            return View("Enemigos", model);
        }


        [HttpPost]
        public IActionResult EliminarEnemigo(int id) {

            if (id > 0)
            {
                _enemigoServicio.EliminarEnemigo(id);
                return RedirectToAction("Enemigos");
            }

            return RedirectToAction("Enemigos");
        }
    }
}
