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
        private readonly IConfiguration _configuration;
        public UsuariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private UsuarioBussiness usuarioBussiness = new UsuarioBussiness();
        private TipoUsuarioBussiness tipoUsuarios = new TipoUsuarioBussiness();



        public IActionResult Index()
        {
            return View();

        }
        /// <summary>
        /// Seccion ver todos los usuarios, nos carga una array de objetos de todos los 
        /// usuarios activos
        /// </summary>
        /// <returns></returns>
        public IActionResult CatalogoUsuarios()
        {
            var usuarios = usuarioBussiness.HttpGet();//Obtenemos una lista de usuarios
            var modelo = new UsuariosCatalogoViewModel() { Usuarios = usuarios.Result };
            return View(modelo);
        }



        /// <summary>
        /// Este metodo devuelve un modal
        /// </summary>
        /// <returns></returns>
        ///
        public IActionResult EditarUsuario()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            //Buscamos al usuario para mostrarlo
            string id = Request.Form["datos"]; //tenemos el id
            var _oUsuario = JsonConvert.DeserializeObject<UsuarioDTO>(id); //tenemos el objeto 

            //Pedismo todos los tipos de usuario que existen

            var catTipoUsuario = tipoUsuarios.todosTipoUsuarios(host);

            var usuario = usuarioBussiness.obtenerElemento(_oUsuario.iId);
            var modelo = new ItemEditarUsuarioViewModel() { itemUsuario = usuario.Result, tiposUsuario = catTipoUsuario.Result };

            return View(modelo);

        }

        /// <summary>
        /// Modal de para ver la informacion del usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult VerUsuario()
        {

            //Recibimos el objeto que viene del ajax
            string id = Request.Form["datos"]; //tenemos el id
            var _oUsuario = JsonConvert.DeserializeObject<UsuarioDTO>(id); //deserializamos con el dto para tener el id
            var usuario = usuarioBussiness.obtenerElemento(_oUsuario.iId); //hacemos la peticion
            var modelo = new ItemUsuarioViewModel() {itemUsuario = usuario.Result  };
            return View(modelo);
        }
        public ActionResult CrearUsuario()
        {
            return View();
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



