using Entidades.EF;
using Microsoft.AspNetCore.Mvc;
using Servicio;

namespace CasinoCrusaders.Controllers;

public class RankingController : Controller
{
    private readonly IProgresoServicio _progresoServicio;
    public RankingController(IProgresoServicio progresoServicio)
    {
        _progresoServicio = progresoServicio;
    }
    public IActionResult Index()
    {
        ViewBag.Rol = HttpContext.Session.GetString("Rol");

        var usuariosConProgreso = _progresoServicio.ObtenerLos5UsuariosConMayorProgreso();
        Enemigo enemigoMasDificil = _progresoServicio.ObtenerEnemigoMasDificil();
        Enemigo enemigoMasFacil = _progresoServicio.ObtenerEnemigoMasFacil();

        ViewData["enemigoMasFacil"] = enemigoMasFacil;
        ViewData["enemigoMasDificil"] = enemigoMasDificil;
        ViewData["usuariosConMayorProgreso"] = usuariosConProgreso;
        return View();
    }
}
