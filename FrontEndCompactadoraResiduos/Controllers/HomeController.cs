using FrontEndCompactadoraResiduos.Bussiness.Home;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly HomeBussiness homeBus = new HomeBussiness();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            var totalResiduos = homeBus.getTotalResiduos(host);
            var totalCargas = homeBus.getTotalCargas(host);
            var totalUsuarios = homeBus.getTotalUsuarios(host);
            var modelo = new HomeViewModel(); 
            if(totalResiduos.Result.estatus == "success" && totalResiduos.Result.codigo == 200 )
            {
               modelo = new HomeViewModel()
               {
                   totalCargas = totalCargas.Result.data,
                   totalUsuarios = totalUsuarios.Result.data,
                   totalResiduos = totalResiduos.Result.data,
                   mensaje = totalResiduos.Result.mensaje,
                   estatus = totalResiduos.Result.estatus

               };
            }
            else
            {
                modelo = new HomeViewModel()
                {
                    totalCargas = totalCargas.Result.data,
                    totalUsuarios = totalUsuarios.Result.data,
                    totalResiduos = totalResiduos.Result.data,
                    mensaje = totalResiduos.Result.mensaje,
                    estatus = totalResiduos.Result.estatus

                };
            }

            return View(modelo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}