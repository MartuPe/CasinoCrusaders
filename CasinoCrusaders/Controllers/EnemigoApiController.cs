using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnemigoApiController : ControllerBase
    {

        private readonly IEnemigoServicio _enemigoServicio;
        public EnemigoApiController(IEnemigoServicio enemigoServicio )
        {
            _enemigoServicio = enemigoServicio;
        }


        [HttpGet]
        public IActionResult ObtenerEnemigo(int id)
        {
            var enemigo = _enemigoServicio.ObtenerEnemigo(id);

            if (enemigo == null)
            {
                return NotFound($"No se encontro ningun enemigo con id {id}");
            }

            return Ok(enemigo);
        }

    }
}
