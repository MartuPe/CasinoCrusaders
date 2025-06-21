using CasinoCrusaders.ViewModels.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelApiController : ControllerBase
    {

        private readonly INivelSerivicio _nivelServicio;

        public NivelApiController(INivelSerivicio nivelServicio)
        {
            _nivelServicio = nivelServicio;
        }

        [HttpGet]
        public IActionResult ObtenerNivel(int id)
        {
            var nivel = _nivelServicio.ObtenerNivel(id);

            if (nivel == null)
            {
                return NotFound("no se encontró nivel");
            }

            NivelDto nivelDto = new NivelDto
            {
                IdNivel = nivel.IdNivel,
                IdEnemigo = nivel.IdEnemigo,
                Nombre = nivel.Nombre,
                IdTipoCasillero = nivel.IdTipoCasillero
            };

            return Ok(nivelDto);


        }
    }
}
