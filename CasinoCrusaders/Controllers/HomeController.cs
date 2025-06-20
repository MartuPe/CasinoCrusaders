using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers;

public class HomeController : Controller
{
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
