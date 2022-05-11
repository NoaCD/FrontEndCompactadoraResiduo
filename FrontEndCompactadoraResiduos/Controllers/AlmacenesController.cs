using FrontEndCompactadoraResiduos.Bussiness.Almacen;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class AlmacenesController : Controller
    {

        private readonly IConfiguration _configuration;
        public AlmacenesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: AlmacenesController
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TodosAlmacenes()
        {
            string host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            AlmacenBussiness almacenBuss = new AlmacenBussiness();
            var respuesta = almacenBuss.obtenerTodos(host);
            return new JsonResult(new
            {
                data = respuesta.Result
            });
        }


        // GET: AlmacenesController/Details/5

        public IActionResult detalleAlmacen(int id)
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            AlmacenBussiness almacenBuss = new AlmacenBussiness();
            var almacenes = almacenBuss.elemento(id, host);
            var modelo = new AlmacenViewModel() { itemAlmacen = almacenes.Result };
            return View(modelo);
        }
        // GET: AlmacenesController/Details/5

        public IActionResult editarAlmacen(int id)
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            AlmacenBussiness almacenBuss = new AlmacenBussiness();
            var almacenes = almacenBuss.elemento(id, host);
            var modelo = new AlmacenViewModel() { itemAlmacen = almacenes.Result };
            return View(modelo);
        }

        /// <summary>
        /// Mandamos al API los datos para que se guarden
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        public JsonResult GuardarEdicionAlmacen()
        {
            string JsonAlmacen = Request.Form["datos"];
            if (JsonAlmacen == "")
            {
                return new JsonResult(new
                {
                    mensaje = "Llego vacio desde el formulario",
                    estatus = "error",

                });
            }
            else
            {
                ///Vamos a mandarlo al API para actualizarlo
                AlmacenBussiness almacenBuss = new AlmacenBussiness(); //Instanciamos el bussiness
                AlmacenFrontDTO oAlmacen = JsonConvert.DeserializeObject<AlmacenFrontDTO>(JsonAlmacen);
                var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
                var respuesta = almacenBuss.editarAlmacen(host, oAlmacen);
                return new JsonResult(respuesta.Result);
            }
        }
        /// <summary>
        /// Vista crear almacen
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult VistaCrearAlmacen()
        {
            return View();
        }


        /// <summary>
        /// Manda al api la informacion para guardarla. No retorna una vista
        /// </summary>
        /// <returns></returns>
        public JsonResult CrearAlmacen()
        {
            string JsonAlmacen = Request.Form["datos"];
            if (JsonAlmacen == "")
            {
                return new JsonResult(new
                {
                    mensaje = "Llego vacio desde el formulario",
                    estatus = "error",

                });
            }
            else
            {
                ///Vamos a mandarlo al API para actualizarlo
                AlmacenBussiness almacenBuss = new AlmacenBussiness(); //Instanciamos el bussiness
                var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
                var respuesta = almacenBuss.crearAlmacen(host, JsonAlmacen);
                return new JsonResult(respuesta.Result);
            }
        }


    }
}
