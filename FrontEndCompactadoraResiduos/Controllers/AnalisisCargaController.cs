using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{

    /// <summary>
    /// Controlador encargado de hacer reportes para el analisis de resudios bimestrales trimestrales anuales
    /// 
    /// </summary>

    public class AnalisisCargaController : Controller
    {
        // GET: AnalisisCargaController
        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
