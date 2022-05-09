using FrontEndCompactadoraResiduos.Bussiness.Proveedor;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        public ProveedoresController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Inicio muestra la tabla con los datios ya llenados
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {

            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            ProcesarProveedorBussiness procesarBus = new ProcesarProveedorBussiness();
            var proveedores = procesarBus.GetProveedores(host);

            var respuesta = proveedores.Result;
            var modelo = new ProveedoresViewModel() { proveedores = respuesta };

            return View(modelo);
        }
        /// <summary>
        /// Retonra la vista, el formulario modal para agregar un nuevo producvto
        /// </summary>
        /// <returns></returns>
        public IActionResult CrearProveedor()
        {
            return View();
        }
        public JsonResult GuardarProveedor()
        {
            string host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            string proveedorJSON = Request.Form["datos"];
            if (proveedorJSON is not null)
            {

                ProveedorFrontDTO _oProveedor = JsonConvert.DeserializeObject<ProveedorFrontDTO>(proveedorJSON);
                ProcesarProveedorBussiness procesarBus = new ProcesarProveedorBussiness();
                var respuesta = procesarBus.crear(_oProveedor, host);
                if (respuesta.Result != null)
                {
                    return new JsonResult(new { estatus = respuesta.Result.estatus, mensaje = respuesta.Result.mensaje });

                }
                else
                {
                    return new JsonResult(new { estatus = "error", mensaje = "llego nulo la repsuesta, es probable que el proveedor no se hay creado" });

                }
            }
            else
            {
                return new JsonResult(new { estatus = "error", mensaje = "no se logro parsear el formualario" });

            }
        }

        /// <summary>
        /// Requiere que le envien el id por la url para poder obtener la informacion del provedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult EditarProveedor(int id)
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            ProcesarProveedorBussiness procesarBus = new ProcesarProveedorBussiness();
            var proveedores = procesarBus.GetElementProveedor(id, host);
            var modelo = new ProveedorViewModel() { proveedor = proveedores.Result };
            return View(modelo);

        }
        /// <summary>
        /// Editar Proveedor
        /// Vista para editarlo
        /// </summary>
        /// <returns></returns>
        public JsonResult GuardarEdicionProveedor()
        {

            string host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            string proveedorJSON = Request.Form["datos"];
            if (proveedorJSON is not null)
            {

                ProveedorFrontDTO _oProveedor = JsonConvert.DeserializeObject<ProveedorFrontDTO>(proveedorJSON);
                ProcesarProveedorBussiness procesarBus = new ProcesarProveedorBussiness();
                var respuesta = procesarBus.editar(_oProveedor, host);
                if (respuesta.Result != null)
                {
                    return new JsonResult(new { estatus = respuesta.Result.estatus, mensaje = respuesta.Result.mensaje });

                }
                else
                {
                    return new JsonResult(new { estatus = "error", mensaje = "llego nulo la repsuesta, es probable que el proveedor no se hay creado" });

                }
            }
            else
            {
                return new JsonResult(new { estatus = "error", mensaje = "llego nulo el formularion, no se puede procesar" });

            }

        }
    }
}
