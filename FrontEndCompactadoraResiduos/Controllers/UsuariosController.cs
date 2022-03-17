using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
