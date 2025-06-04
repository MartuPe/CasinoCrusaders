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

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            var usuarioValidado = servicio.ValidarLogin(usuario.NombreUsuario, usuario.Contrase�a);

            if (usuarioValidado == null)
            {
                TempData["ErrorLogin"] = "Nombre de usuario o contrase�a incorrectos";
                return View(usuario);
            }

            if (!servicio.ValidarSiGmailExiste(usuarioValidado.Gmail))
            {
                TempData["ErrorVerificarEmail"] = "El correo no fue verificado. Por favor, verifica tu correo electr�nico.";
                return View(usuario);
            }

            TempData["MensajeDeExito"] = "Inicio de sesi�n exitoso.";
            return RedirectToAction("Registrar");
        }

        [HttpGet]
        public IActionResult Verificar(string token)
        {
            var resultado = servicio.VerificarEmailConToken(token);

            if (resultado == EmailVerificationResult.TokenInvalido)
                return BadRequest("Token inv�lido.");

            if (resultado == EmailVerificationResult.TokenExpirado)
                return BadRequest("El token ha expirado. Solicita uno nuevo.");

            TempData["MensajeDeExito"] = "Correo verificado exitosamente. �Bienvenido a CasinoCrusaders!";
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
                EmailResendResult.Enviado => Ok("Correo de verificaci�n reenviado."),
                _ => StatusCode(500)
            };
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            var usuarioValidado = servicio.ValidarLogin(usuario.NombreUsuario, usuario.Contrase�a);

            if (usuarioValidado == null)
            {
                if (!servicio.EmailVerificadoCorrectamente(usuario.Gmail))
                    TempData["ErrorLogin"] = "El correo electr�nico no ha sido verificado. Por favor, verifica tu correo antes de iniciar sesi�n.";
                else
                    TempData["ErrorLogin"] = "Nombre de usuario o contrase�a incorrectos, o el correo no fue verificado.";

                return View(usuario);
            }

            // TODO: guardar login en sesi�n o autenticaci�n real
            TempData["MensajeDeExito"] = "Inicio de sesi�n exitoso.";
            return RedirectToAction("Registrar");
        }

    }
}
