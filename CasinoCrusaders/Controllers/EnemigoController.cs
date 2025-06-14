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

        public IActionResult MostrarEnemigos()
        {

            ViewBag.Rol = HttpContext.Session.GetString("Rol");

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

        public IActionResult EnemigosDetalles(int id)
        {

            ViewBag.Rol = HttpContext.Session.GetString("Rol");

            var enemigo = _enemigoServicio.ObtenerEnemigo(id);
            if (enemigo!= null)
            {
                return View(enemigo);
            }

            TempData["MensajeError"] = "No se encontro el enemigo buscado. Verifica los datos ingresados.";

            return RedirectToAction("MostrarEnemigos");
        }

        [HttpPost]
        public IActionResult RegistrarEnemigo(EnemigosViewModel enemigo)
        {
            if (ModelState.IsValid)
            {
                _enemigoServicio.RegistrarEnemigo(enemigo.NuevoEnemigo);
                return RedirectToAction("mostrarEnemigos");
            }

            return RedirectToAction("mostrarEnemigos");
        }

        [HttpPost]
        public IActionResult EditarEnemigo(Enemigo enemigo)
        {
            if (ModelState.IsValid)
            {
                _enemigoServicio.EditarEnemigo(enemigo);
                return RedirectToAction("EnemigosDetalles", new {id = enemigo.IdEnemigo});
            }


            TempData["MensajeError"] = "Error al editar el enemigo. Verifica los datos ingresados.";

            return RedirectToAction("EnemigosDetalles", new {id = enemigo.IdEnemigo});
        }


        [HttpPost]
        public IActionResult EliminarEnemigo(int id) {

            if (id > 0)
            {
                _enemigoServicio.EliminarEnemigo(id);
                return RedirectToAction("MostrarEnemigos");
            }

            return RedirectToAction("EnemigosDetalles" , new {id = id});
        }
    }
}
