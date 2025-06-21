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

        public PersonajeApiController(IPersonajeServicio personajeServicio)
        {
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

        [HttpPut]
        public IActionResult ActualizarPersonaje([FromBody] Personaje datos)
        {
            if (datos == null)
                return BadRequest("Datos invalidos");

            bool actualizado = _personajeServicio.ActualizarPersonaje(datos);
            if (!actualizado)
                return NotFound("Personaje no encontrado");

            return Ok("Personaje actualizado");
        }
    }
}

