using CasinoCrusaders.ViewModels.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgresoApiController : ControllerBase
    {

        private readonly IProgresoServicio _progresoServicio;

        public ProgresoApiController(IProgresoServicio progresoServicio)
        {

            _progresoServicio = progresoServicio;

        }


        [HttpGet]
        public IActionResult ObtenerProgreso(int id)
        {

            var progreso = _progresoServicio.ObtenerProgreso(id);

            if (progreso != null)
            {
                ProgresoDto progresoDto = new ProgresoDto
                {
                    IdProgreso = progreso.IdProgreso,
                    IdNivel = progreso.IdNivel,
                    IdPersonaje = progreso.IdPersonaje,
                    FechaCreacion = progreso.FechaCreacion
                };
                return Ok(progresoDto);
            }

            return NotFound("No se encontro personaje");
        }



        [HttpPut]
        public IActionResult ActualizarProgreso(int id, int nivel, DateTime fecha)
        {

            var progreso = _progresoServicio.ObtenerProgreso(id);

            if (progreso != null)
            {
                _progresoServicio.ActualizarProgreso(id, nivel, fecha);
                return Ok("personaje actualizado");
            }

            return NotFound("No se encontro personaje");
        }


    }
}
