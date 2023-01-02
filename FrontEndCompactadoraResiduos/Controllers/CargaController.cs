using FrontEndCompactadoraResiduos.Bussiness.Residuos;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
    [Authorize]
    public class CargaController : Controller
    {
        private readonly CargaBussiness cargaBus = new CargaBussiness();

        private readonly IConfiguration _configuration;
        public CargaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // GET: CargaController
        public ActionResult Index()
        {
            //retorna la vista para observar todas las cargas
            return View();
        }

        /// <summary>
        /// Retorna todas las cargas ehchas hasta ahora
        /// </summary>
        /// <returns></returns>
        public JsonResult ApiCargas()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            CargaBussiness CargaBuss = new CargaBussiness();
            var respuesta = CargaBuss.cargasGet(host);

            return new JsonResult(new { data = respuesta.Result });
        }


        // GET: CargaController/Details/5
        public ActionResult DetalleCarga()
        {

            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            //Recibimos el objeto que viene del ajax
            string JsonId = Request.Form["datos"]; //tenemos el id
            var _oCarga = JsonConvert.DeserializeObject<CargaBinding>(JsonId); //deserializamos con el dto para tener el id
            var CargaGet = cargaBus.obtenerElementoCarga(host, _oCarga.iId); //hacemos la peticion
            try
            {
                var dataCarga = CargaGet.Result;
                if (dataCarga == null)
                {
                    return new JsonResult(new { estatus = "error", mensjae = "carga vacia", codigo = "error" });
                }
                else
                {
                    var model = new ItemCargaViewModel { Carga = dataCarga.carga };
                    return View(model);
                    //return new JsonResult(new { estatus = "Correcto", mensjae = "carga vacia", codigo = "error" });

                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new { estatus = "error", mensaje = "error en el controlador de detalle carga" });
            }
        }

        // PUT: CargaController/EditarCarga
        public ActionResult EditarCarga()
        {
            try
            {
                var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
                var jsonCarga = Request.Form["datos"];
                var _idObCarga = JsonConvert.DeserializeObject<CargaBinding>(jsonCarga);//Id Objeto Carga
                var ObjetoCarga = cargaBus.obtenerElementoCarga(host, _idObCarga.iId);

                var modelo = new ItemCargaViewModel() { Carga = ObjetoCarga.Result.carga };
                return View(modelo);


            }
            catch (Exception ex)
            {
                return new JsonResult(new { estatus = "error", mensjae = "error en el controlador EditarCargfa" });
            }

        }

        public ActionResult<ResponseCargaDTO> ActualizarCarga()
        {
            string cargaJson = Request.Form["datos"];
            var cargaDTO = JsonConvert.DeserializeObject<EditarCargaDTO>(cargaJson);
            cargaDTO.iId_User = Int32.Parse(User.Claims.Where(x => x.Type == "idUsuario").FirstOrDefault().Value);

            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            var eliminar = cargaDTO.eliminar;
            if (eliminar == "checked")
            {
                //mandar a eliminar
            };

            //Editamos

            var editarCarga = cargaBus.modificarCarga(host, cargaDTO);

            return editarCarga.Result;

        }

     
        // GET: CargaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CargaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
