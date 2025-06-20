using CasinoCrusaders.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers;

public class AdminController : Controller
{

    IUsuarioServicio servicio;

    public AdminController(IUsuarioServicio servicio)
    {
        this.servicio = servicio;
    }

    [RolRequerido("Admin")]
    public IActionResult IndexAdmin()
    {
        return View();
    }
    
    public IActionResult GestionUsuarios(string busqueda)
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
            return RedirectToAction("GestionUsuarios");
        }

        var model = new UsuariosViewModel
        {
            Usuarios = servicio.ObtenerTodosLosUsuarios(),
            Usuario = usuario
        };

        ViewBag.MostrarModal = true;

        ViewBag.MensajeError = "Error al editar el usuario. Verifica los datos ingresados.";

        return View("GestionUsuarios", model);
    }

    [HttpPost]
    public IActionResult HacerAdministrador(int id)
    {
        if (id > 0)
        {
            var usuario = servicio.ObtenerUsuarioPorId(id);
            usuario.TipoUsuario = "Admin";
            servicio.ActualizarUsuario(usuario);
            return RedirectToAction("GestionUsuarios");
        }
        var model = new UsuariosViewModel
        {
            Usuarios = servicio.ObtenerTodosLosUsuarios(),
            Usuario = new EditarUsuarioViewModel()
        };

        ViewBag.MensajeError = "Error al editar el usuario. Verifica los datos ingresados.";

        return View("GestionUsuarios", model);
    }


    [HttpPost]
    public IActionResult EliminarUsuario(int id)
    {

        if (id > 0)
        {
            servicio.EliminarUsuario(id);
            return RedirectToAction("GestionUsuarios");
        }

        return RedirectToAction("GestionUsuarios");
    }

    public IActionResult GestionObjetos()
    {
        var todosLosUsuarios = servicio.ObtenerTodosLosUsuarios();

        return View();
    }

    public IActionResult GestionEnemigos()
    {
        return RedirectToAction("mostrarEnemigos", "enemigo");
    }
}
