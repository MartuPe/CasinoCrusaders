using Microsoft.AspNetCore.Mvc;

namespace CasinoCrusaders.Controllers
{
    public class RankingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
