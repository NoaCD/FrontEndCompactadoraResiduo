using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class AlmacenesController : Controller
    {
        // GET: AlmacenesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AlmacenesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AlmacenesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlmacenesController/Create
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

        // GET: AlmacenesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AlmacenesController/Edit/5
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

        // GET: AlmacenesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AlmacenesController/Delete/5
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
