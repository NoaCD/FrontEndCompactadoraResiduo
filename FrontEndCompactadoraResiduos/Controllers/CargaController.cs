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
        private readonly ResiduoBussiness residuos = new ResiduoBussiness();


        // GET: CargaController
        public ActionResult Index()
        {

            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            var ListResiduos = residuos.HttpGet(host);//Obtenemos una lista de residuos
            var modelo = new ResiduosCatalogoViewModel() { Residuos = ListResiduos.Result };//Retornamos el modelo instancia a la vista
            return View(modelo);

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
