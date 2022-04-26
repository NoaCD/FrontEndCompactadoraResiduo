using CompactadoraDeResiduos.Model.DTO;
using FrontEndCompactadoraResiduos.Bussiness.Residuos;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
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


        public JsonResult ApiCargas()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            CargaBussiness CargaBuss = new CargaBussiness();
            var respuesta = CargaBuss.cargasGet(host);

            return new JsonResult(new { data = respuesta.Result } );
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
                    var model = new ItemCargaViewModel { Carga = dataCarga.carga};
                    return View(model);
                    //return new JsonResult(new { estatus = "Correcto", mensjae = "carga vacia", codigo = "error" });

                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new { estatus = "error", mensaje = "error en el controlador de detalle carga" });
            }
        }

        // GET: CargaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CargaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CargaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
