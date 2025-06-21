using CasinoCrusaders.ViewModels.DTO;
using Entidades.EF;
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

        [HttpGet("ObtenerPorEmail")]
        public ActionResult ObtenerUsuarioPorEmail(String email)
        {
            var usuario = _usuarioServicio.ObtenerUsuarioPorEmail(email);

            if (usuario == null)
            {
                return NotFound("No se encontro ningun usuario");
            }

            UsuarioDto usuarioDto = convertirUsuarioAUsuarioDto(usuario);

            return Ok(usuarioDto);

        }
        [HttpGet("ObtenerPorId")]
        public ActionResult ObtenerUsuarioPorId(int id)
        {
            var usuario = _usuarioServicio.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound($"No se encontro ningun usuario con id {id} ");
            }


            UsuarioDto usuarioDto = convertirUsuarioAUsuarioDto(usuario);


            return Ok(usuarioDto);

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var usuario = _usuarioServicio.ValidarLoginJuego(request.Gmail, request.Contraseña);

            if (usuario == null)
                return Unauthorized("Credenciales invalidas");


            UsuarioDto usuarioDto = convertirUsuarioAUsuarioDto(usuario);
          

            return Ok(usuarioDto);
        }

        private UsuarioDto convertirUsuarioAUsuarioDto(Usuario usuario) {
            UsuarioDto usuarioDto = new UsuarioDto();
            usuarioDto.IdUsuario = usuario.IdUsuario;
            usuarioDto.Gmail = usuario.Gmail;
            usuarioDto.NombreUsuario = usuario.NombreUsuario;
            usuarioDto.TipoUsuario = usuario.TipoUsuario;
            usuarioDto.IdPersonaje = usuario.IdPersonaje;

            return usuarioDto;
        }
    }
}
