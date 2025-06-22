using CasinoCrusaders.ViewModels;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    public class PersonajeController : Controller
    {

        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IPersonajeServicio _personajeServicio;
        private readonly IProgresoServicio _progresoServicio;
        public PersonajeController(IUsuarioServicio usuarioServicio, IPersonajeServicio personajeServicio, IProgresoServicio progresoServicio)
        {
            _usuarioServicio = usuarioServicio;
            _personajeServicio = personajeServicio;
            _progresoServicio = progresoServicio;
        }

        [RolRequerido("Usuario")]
        public IActionResult MiPersonaje()
        {
            
            var idUsuario = HttpContext.Session.GetInt32("Id");

            var usuario = _usuarioServicio.ObtenerUsuarioPorId(idUsuario.Value);

            if(usuario.IdPersonaje == null)
            {
                ViewBag.ErrorPersonaje = "Todavia no tenes un personaje, Loguea en el juego para generarlo";
                return View();
            }

            var personaje = _personajeServicio.ObtenerPersonaje(usuario.IdPersonaje.Value);

            var progreso = _progresoServicio.ObtenerProgreso(personaje.IdPersonaje);

            MiPersonajeViewModel model = new MiPersonajeViewModel
            {
                Personaje = personaje,
                progreso = progreso
            };

            return View(model);
        }
    }
}
