using CasinoCrusaders.ViewModels;
using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers;

[RolRequerido("Admin")]
public class AdminController : Controller
{

    IUsuarioServicio servicio;
    IObjetoServicio objetoServicio;

    public AdminController(IUsuarioServicio servicio, IObjetoServicio objetoServicio)
    {
        this.servicio = servicio;
        this.objetoServicio =  objetoServicio;
    }

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
         var objetos = objetoServicio.ObtenerListaDeObjetos();

        return View(objetos);
    }

    [HttpPost]
    public IActionResult EliminarObjeto(int id)
    {

        if (id > 0)
        {
            objetoServicio.EliminarObjeto(id);
            return RedirectToAction("GestionObjetos");
        }

        return RedirectToAction("GestionObjetos");
    }

    [HttpPost]
    public IActionResult EditarObjetos(Objeto objeto)
    {
        

        if (ModelState.IsValid)
        {
            var objetoViejo = objetoServicio.ObtenerObjeto(objeto.IdObjeto);
            objetoViejo.Estadistica = objeto.Estadistica;
            objetoViejo.Precio = objeto.Precio;
            objetoViejo.Nombre = objeto.Nombre; 
            objetoServicio.EditarObjeto(objetoViejo);
            return RedirectToAction("GestionObjetos");
        }

        
        return View("GestionObjetos");
    }

    public IActionResult GestionEnemigos()
    {
        return RedirectToAction("mostrarEnemigos", "enemigo");
    }
}
