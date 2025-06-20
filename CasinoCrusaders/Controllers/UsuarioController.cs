using CasinoCrusaders.ViewModels;
using Entidades;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            Usuario usuarioValidado = null;

            usuarioValidado = servicio.ValidarLogin(usuario.NombreUsuario, usuario.Contraseña);
            HttpContext.Session.SetString("Rol", usuarioValidado.TipoUsuario);


            if (usuarioValidado == null)
            {
                TempData["ErrorLogin"] = "Nombre de usuario o contraseña incorrectos";
                return View(usuario);
            }

            if (!servicio.ValidarSiGmailExiste(usuarioValidado.Gmail))
            {
                TempData["ErrorVerificarEmail"] = "El correo no fue verificado. Por favor, verifica tu correo electrónico.";
                return View(usuario);
            }


            HttpContext.Session.SetInt32("Id", usuarioValidado.IdUsuario);
            HttpContext.Session.SetString("Nombre", usuarioValidado.NombreUsuario);

            if (usuarioValidado.TipoUsuario == "Admin")
                return RedirectToAction("IndexAdmin", "Admin");

            return RedirectToAction("IndexUsuario", "Home");

        }

        [HttpGet]
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
