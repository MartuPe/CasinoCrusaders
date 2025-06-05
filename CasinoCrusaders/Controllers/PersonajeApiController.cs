using Entidades.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajeApiController : ControllerBase
    {
        private readonly IPersonajeServicio _personajeServicio;

        public PersonajeApiController(IPersonajeServicio personajeServicio) { 
            _personajeServicio = personajeServicio;
        }


        [HttpGet]
        public IActionResult ObtenerPersonaje(int IdPersonaje)
        {

            var personaje = _personajeServicio.ObtenerPersonaje(IdPersonaje);
            if (personaje == null)
            {
                return NotFound($"No se encontro ningun Personaje con id {IdPersonaje}");
            }

            return Ok(personaje);
        }
    }
}
