using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class EmbarqueController : Controller
    {
        // GET: EmbarqueController
        public ActionResult Index()
        {

            return View();
        }

        // GET: EmbarqueController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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
