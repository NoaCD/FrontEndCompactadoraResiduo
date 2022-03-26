using FrontEndCompactadoraResiduos.Bussiness.Usuarios;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class UsuariosController : Controller
    {

        
        private UsuarioBussiness usuarioBussiness = new UsuarioBussiness();

        public IActionResult Index()
        {
            return View();

        }
        //Retona vista CatalogoUsuarios
        public IActionResult CatalogoUsuarios()
        {
            var usuarios = usuarioBussiness.HttpGet();//Obtenemos una lista de usuarios
            var modelo = new UsuariosCatalogoViewModel() { Usuarios = usuarios.Result } ;
            return View(modelo);
        }



        /// <summary>
        /// Este metodo devuelve un modal
        /// </summary>
        /// <returns></returns>
        ///
        public IActionResult VerUsuario()
        {

            string id = Request.Form["datos"];
            var _oUsuario = JsonConvert.DeserializeObject<UsuarioDTO>(id);

            var usuario =  usuarioBussiness.obtenerElemento(_oUsuario.iId);
            var modelo = new ItemUsuarioViewModel() { itemUsuario = usuario.Result };

            return View(modelo);

        }

        public JsonResult disparador()
        {

            return new JsonResult(Ok("d"));
        }


        #region
        //using(var http = new HttpClient())
        //{
        //    http.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        //    http.DefaultRequestHeaders.Accept.Clear();
        //    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    //Hacemos la llamada al API
        //    var response = await http.GetAsync("todos");

        //    // Si el servicio responde correctamente
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Lee el response y lo deserializa como un Product
        //        var re = await response.Content.ReadFromJsonAsync<UsuarioDTO>();
        //        return re;
        //    }
        //    // Sino devuelve null
        //    return await Task.FromResult<UsuarioDTO>(null);
        #endregion

    }

}



