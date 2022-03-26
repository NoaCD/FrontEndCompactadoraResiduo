using FrontEndCompactadoraResiduos.Bussiness.Residuos;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class ResiduosController : Controller
    {
        private readonly ResiduoBussiness residuos = new ResiduoBussiness();


        /// <summary>
        /// Obtenemos la lista y se lo pasamos al controlador
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            var ListResiduos = residuos.HttpGet();//Obtenemos una lista de residuos
            var modelo = new ResiduosCatalogoViewModel() { Residuos = ListResiduos.Result };//Retornamos el modelo instancia a la vista
            return View(modelo);
        }
 
    }
}
