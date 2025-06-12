using System.Diagnostics;
using CasinoCrusaders.ViewModels;
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
            var usuarioValidado = servicio.ValidarLogin(usuario.NombreUsuario, usuario.Contraseña);

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

            TempData["MensajeDeExito"] = "Inicio de sesión exitoso.";
            return RedirectToAction("registrar");
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


        public IActionResult AdministrarUsuarios(string busqueda)
        {
            var todosLosUsuarios = servicio.ObtenerTodosLosUsuarios();

            if (!string.IsNullOrEmpty(busqueda))
            {
                todosLosUsuarios = todosLosUsuarios
                    .Where(u => u.Gmail.Contains(busqueda, StringComparison.OrdinalIgnoreCase) ||
                                u.NombreUsuario.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var model = new UsuariosViewModel
            {
                Usuarios = todosLosUsuarios,
                Usuario = new EditarUsuarioViewModel()
            };

            if (model.Usuarios.Count > 0)
            {
                return View(model);
            }

            ViewBag.Mensaje = string.IsNullOrEmpty(busqueda)
                ? "No hay usuarios registrados"
                : $"No se encontraron usuarios que coincidan con: \"{busqueda}\"";

            return View(model);
        }


        [HttpPost]
        public IActionResult EditarUsuario(EditarUsuarioViewModel usuario)
        {
            if (servicio.ExisteOtroUsuarioConEseEmail(usuario.IdUsuario, usuario.Gmail))
            {
                ModelState.AddModelError("Usuario.Gmail", "El correo electrónico ya está en uso por otro usuario.");
            }

            if (ModelState.IsValid)
            {
                var usuarioNuevo = servicio.ObtenerUsuarioPorId(usuario.IdUsuario);
                usuarioNuevo.Gmail = usuario.Gmail;
                usuarioNuevo.NombreUsuario = usuario.NombreUsuario;
                servicio.ActualizarUsuario(usuarioNuevo);
                return RedirectToAction("AdministrarUsuarios");
            }

            var model = new UsuariosViewModel
            {
                Usuarios = servicio.ObtenerTodosLosUsuarios(),
                Usuario = usuario
            };

            ViewBag.MostrarModal = true;

            ViewBag.MensajeError = "Error al editar el usuario. Verifica los datos ingresados.";

            return View("AdministrarUsuarios", model);
        }



        [HttpPost]
        public IActionResult HacerAdministrador(int id)
        {
            if (id > 0)
            {
                var usuario = servicio.ObtenerUsuarioPorId(id);
                usuario.TipoUsuario = "Admin";
                servicio.ActualizarUsuario(usuario);
                return RedirectToAction("AdministrarUsuarios");
            }
            var model = new UsuariosViewModel
            {
                Usuarios = servicio.ObtenerTodosLosUsuarios(),
                Usuario = new EditarUsuarioViewModel()
            };

            ViewBag.MensajeError = "Error al editar el usuario. Verifica los datos ingresados.";

            return View("AdministrarUsuarios", model);
        }


        [HttpPost]
        public IActionResult EliminarUsuario(int id)
        {

            if (id > 0)
            {
                servicio.EliminarUsuario(id);
                return RedirectToAction("AdministrarUsuarios");
            }

            return RedirectToAction("AdministrarUsuarios");
        }


    }
}
