using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RolRequeridoAttribute : ActionFilterAttribute
{
    private readonly string _rolPermitido;

    public RolRequeridoAttribute(string rolPermitido)
    {
        _rolPermitido = rolPermitido;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var rolEnSesion = context.HttpContext.Session.GetString("Rol");

        if (rolEnSesion != _rolPermitido)
        {
            context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
        }
    }
}
