using System.Diagnostics;
using Entidades;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioServicio servicio;

        public UsuarioController(IUsuarioServicio servicio)
        {
            this.servicio = servicio;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                servicio.AgregarUsuario(usuario);
                return RedirectToAction("Login");
            }
            return View(usuario);
        }

        [HttpGet("/Verificar")]
        public IActionResult Verificar(string token)
        {
            var resultado = servicio.VerificarEmailConToken(token);

            if (resultado == EmailVerificationResult.TokenInvalido)
                return BadRequest("Token inválido.");

            if (resultado == EmailVerificationResult.TokenExpirado)
                return BadRequest("El token ha expirado. Solicita uno nuevo.");

            TempData["MensajeDeExito"] = "Correo verificado exitosamente. ¡Bienvenido a CasinoCrusaders!";
            return RedirectToAction("Login");
        }

        [HttpPost("/reenviar-verificacion")]
        public IActionResult ReenviarVerificacion(string email)
        {
            var resultado = servicio.ReenviarToken(email);

            return resultado switch
            {
                EmailResendResult.UsuarioNoEncontrado => NotFound("Usuario no encontrado."),
                EmailResendResult.YaVerificado => BadRequest("El correo ya fue verificado."),
                EmailResendResult.Enviado => Ok("Correo de verificación reenviado."),
                _ => StatusCode(500)
            };
        }
    }
}
