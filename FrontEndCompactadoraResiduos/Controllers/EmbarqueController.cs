using FrontEndCompactadoraResiduos.Bussiness.Embarque;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    [Authorize]
    public class EmbarqueController : Controller
    {

        private readonly IConfiguration _configuration;
        public EmbarqueController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: EmbarqueController
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult todosEmbarques()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            EmbarqueBussiness embarqueBuss = new EmbarqueBussiness();

            var resp = embarqueBuss.getAllEmbarques(host);

            return new JsonResult(new { data = resp.Result });

        }




        [HttpGet]
        // GET: EmbarqueController/Details/5
        public ActionResult MostrarPDFDesign(int id)
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            EmbarqueBussiness embarqueBuss = new EmbarqueBussiness();
            var resp = embarqueBuss.getElementEmbarque(host, id);
            return View(resp.Result);
        }


        /// <summary>
        /// Diseño de pedf PODEROSO, para usarlo en el futuro, no se usa en el software
        /// </summary>
        /// <returns></returns>
        public ActionResult MostrarPDF()
        {

            return View();
        }

        // GET: EmbarqueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmbarqueController/Create
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

        // GET: EmbarqueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmbarqueController/Edit/5
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

        // GET: EmbarqueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmbarqueController/Delete/5
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
