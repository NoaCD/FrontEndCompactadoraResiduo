using CompactadoraDeResiduos.Model.DTO;
using FrontEndCompactadoraResiduos.Bussiness.Residuos;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class CargaController : Controller
    {

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
        public ActionResult Details(int id)
        {
            return View();
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
