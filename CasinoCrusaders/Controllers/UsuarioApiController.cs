using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApiController : ControllerBase
    {

        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioApiController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet("ObtenerPorNombre")]
        public ActionResult ObtenerUsuarioPorNombre(String nombre)
        {
            var usuario = _usuarioServicio.ObtenerUsuarioPorNombre(nombre);

            if (usuario == null)
            {
                return NotFound($"No se encontro ningun usuario con nombre {nombre}");
            }
            return Ok(usuario);

        }
        [HttpGet("ObtenerPorId")]
        public ActionResult ObtenerUsuarioPorId(int id)
        {
            var usuario = _usuarioServicio.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound($"No se encontro ningun usuario con id {id} ");
            }
            return Ok(usuario);

        }
    }
}
