using FrontEndCompactadoraResiduos.Bussiness.Residuos;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class ResiduosController : Controller
    {
        private readonly IConfiguration _configuration;
        public ResiduosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly ResiduoBussiness residuos = new ResiduoBussiness();


        /// <summary>
        /// Obtenemos la lista de residuos y se lo pasamos al controlador
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            var ListResiduos = residuos.HttpGet(host);//Obtenemos una lista de residuos
            var modelo = new ResiduosCatalogoViewModel() { Residuos = ListResiduos.Result };//Retornamos el modelo instancia a la vista
            return View(modelo);
        }

        /// <summary>
        /// Retornta el modal para crear un residuo
        /// </summary>
        /// <returns>MODAL</returns>
        public IActionResult CrearResiduo()
        {

            return View();
        }
        
        public JsonResult GuardarResiduo([FromForm] ResiduoCreacionDTO residuo)
        {
            var residuoss = residuo;

            return new JsonResult("d");
        }

    }
}
