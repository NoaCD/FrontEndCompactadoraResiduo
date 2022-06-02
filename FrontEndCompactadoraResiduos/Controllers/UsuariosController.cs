using FrontEndCompactadoraResiduos.Bussiness.Usuarios;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        private ConsultasEstatus estatus = new ConsultasEstatus();




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
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            var usuarios = usuarioBussiness.HttpGet(host);//Obtenemos una lista de usuarios
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

            var usuario = usuarioBussiness.obtenerElemento(host, _oUsuario.iId);
            var modelo = new ItemEditarUsuarioViewModel() { itemUsuario = usuario.Result, tiposUsuario = catTipoUsuario.Result };

            return View(modelo);

        }

        /// <summary>
        /// Modal de para ver la informacion del usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult VerUsuario()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            //Recibimos el objeto que viene del ajax
            string id = Request.Form["datos"]; //tenemos el id
            var _oUsuario = JsonConvert.DeserializeObject<UsuarioDTO>(id); //deserializamos con el dto para tener el id
            var usuario = usuarioBussiness.obtenerElemento(host, _oUsuario.iId); //hacemos la peticion
            var modelo = new ItemUsuarioViewModel() { itemUsuario = usuario.Result };
            return View(modelo);
        }

        /// <summary>
        /// Muestra el modal con formulario para su creacion
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearUsuario()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            var catTipoUsuario = tipoUsuarios.todosTipoUsuarios(host);
            var modelo = new CatTipoUsuarioViewModel() { tiposUsuario = catTipoUsuario.Result };
            return View(modelo);
        }


        /// <summary>
        /// Se procesa la informacion recibida para que se envie en API
        /// </summary>
        /// <returns></returns>
        public JsonResult GuardarUsuario()
        {

            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            string jsonUsuario = Request.Form["datos"];
            var _oUsuario = JsonConvert.DeserializeObject<UsuarioCreacionDTO>(jsonUsuario); //de
            var respuesta = usuarioBussiness.GuardarUsuario(_oUsuario, host);
            if (respuesta.Result.All(char.IsDigit))
            {
                try
                {
                    int idUsuario = Int32.Parse(respuesta.Result);
                    //Hacemos una busqeuda al usuairo que se acaba de crear
                    var usuario = usuarioBussiness.obtenerElemento(host, idUsuario); //hacemos la peticion
                    return Json(new { estatus = "success", mensaje = "Usuario creado con exito", titulo = "Existoso!", data = usuario.Result.nombre });

                }
                catch
                {
                    return Json(new { estatus = "error", mensaje = respuesta.Result, titulo = "Fallido" });
                }

            }
            else
            {
                //Si se envia el mismo arroja un mensjae de error
                return Json(new { estatus = "error", mensaje = "Error! ya existe un usuario con ese nombre", titulo = "Error!" });
            }



        }
        /// <summary>
        /// Una vez enviado el formulario de edicion se procede a enviarlo para que se guarde
        /// </summary>
        /// <returns>mensaje de creado correctamente o mensaje de erorr</returns>
        public JsonResult ActualizarUsuario()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            var jsonUsuario = Request.Form["datos"];
            var _oUsuario = JsonConvert.DeserializeObject<UsuarioEdicionDTO>(jsonUsuario); //de
            var repuesta = usuarioBussiness.ActualizarUsuario(_oUsuario, host);
            var indicador = repuesta.Result.ToString();

            if (indicador.ToUpper() == "\"OK\"")
            {

                return Json(new { titulo = "Usuario actualizado", mensaje = "Usuario Editado con Exito", estatus = "success" });
            }
            else
            {
                return Json(new { titulo = "Error", mensaje = repuesta.Result.ToString(), estatus = "error" });

            }

        }

        /// <summary>
        /// Obtenenos el id del formulario luego se lo enviamos al Bussiness para procesarlo
        /// El status define el error que va a mostrar
        /// La respuesta puede tomar 3 o 4 valores 
        /// 1 ok
        /// 2 block
        /// 3 error
        /// 4 
        /// </summary>
        /// <returns>Json</returns>
        public JsonResult EliminarUsuario()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            var jsonIdUser = Request.Form["datos"];
            var oUsuario = JsonConvert.DeserializeObject<UsuarioEliminacionDTO>(jsonIdUser);

            var respuesta = usuarioBussiness.eliminarUsuario(oUsuario, host);

            switch (respuesta.Result)
            {
                case "ok":
                    return Json(new { mensaje = "Usuario eliminado satisfactoriamente", estatus = "success" });
                case "block":
                    return Json(new { mensaje = "Algo anda, mal este usuario ya ha sido eliminado", estatus = "error" });
                case "error":
                    return Json(new { mensaje = "Error no se pudo eliminar, error interno", estatus = "error" });
                default:
                    return Json(new { mensaje = "Algo anda mal, contacte al admin", estatus = "error" });
            }


        }




    }

}



