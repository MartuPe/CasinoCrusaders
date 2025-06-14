using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers;

public class HomeController : Controller
{
    [RolRequerido("Admin")]
    public IActionResult IndexAdmin()
    {
        return View();
    }
    public IActionResult AccesoDenegado()
    {
        return View();
    }

    [RolRequerido("Usuario")]
    public IActionResult IndexUsuario()
    {
        return View();
    }

}
