using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoApiController : ControllerBase
    {

        private readonly IObjetoServicio _objetoServicio;
        public ObjetoApiController(IObjetoServicio objetoServicio)
        {
            _objetoServicio = objetoServicio;
        }

        [HttpGet]
        public IActionResult ObtenerObjetos()
        {
            var objetos = _objetoServicio.ObtenerListaDeObjetos();
            if (objetos.Count() == 0)
            {
                return NotFound("No existen objetos");
            }
            return Ok(objetos);
        }
    }
}