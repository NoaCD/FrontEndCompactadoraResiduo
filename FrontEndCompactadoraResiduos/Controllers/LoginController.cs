using CreativeReduction.Model.DTOS;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class LoginController : Controller
    {
        public const string SessionKeyNombre = "_Nombre";
        public const string SessionKeyNumero = "_Numero";
        public const string SessionKeyTipo = "_Tipo";
        public const int SessionKeyid = 0;

        private readonly IConfiguration _configuration;
        private List<SessionDTO> _sessions;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Vista del login principal
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// determina el inicio del acceso a la plataforma generando el estado de login
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Acceso()
        {
            LoginerDTO _cuentaDeUsuario = JsonConvert.DeserializeObject<LoginerDTO>(Request.Form["datos"]);

            var host = _configuration.GetValue<string>("HostAPI");
            var respuesta = new LoginBussiness();
            if (_cuentaDeUsuario.cNombreUsuario != null && _cuentaDeUsuario.cContrasenia != null)
            {
                var response = respuesta.IniciarSecionAdministrativo(_cuentaDeUsuario.cNombreUsuario, _cuentaDeUsuario.cContrasenia, host);
                response.Wait();

                if (response.Result.estatus == "success")
                {
                    var elementosLogin = respuesta.ObtenerUsuarioLoged(response.Result.mensaje, host);
                    elementosLogin.Wait();

                    switch (elementosLogin.Result.Nombre)
                    {
                        case "contactoServidor":
                            return new JsonResult(new { estatus = "warning", mensaje = "no se pudo establecer contacto con el servidor", codigo = 202 });
                            break;

                        case "noInformacion":
                            return new JsonResult(new { estatus = "warning", mensaje = "No se pudo obtener informacion contacte a su desarrollador", codigo = 202 });
                            break;
                        case "noType":
                            return new JsonResult(new { estatus = "watning", mensaje = "No se pudo enparejar con un tipo de usuario contacte a TI", codigo = 202 });
                            break;
                        case "noPermice":
                            return new JsonResult(new { estatus = "watning", mensaje = "No cuenta con los permisos para acceder a la plataforma de administración", codigo = 202 });
                            break;
                        case null:
                            return new JsonResult(new { estatus = "warning", mensaje = "No se pudo obtener informacion de usuarios", codigo = 202 });
                            break;
                        default:

                            HttpContext.Session.SetString(SessionKeyNombre, elementosLogin.Result.Nombre);
                            HttpContext.Session.SetInt32(SessionKeyNumero, elementosLogin.Result.iId);
                            HttpContext.Session.SetInt32(SessionKeyTipo, elementosLogin.Result.iId_TipoUsuario);

                            string name = HttpContext.Session.GetString(SessionKeyNombre);

                            return new JsonResult(new { estatus = response.Result.estatus, mensaje = response.Result.mensaje + name, codigo = response.Result.codigo, data = response.Result.data });

                            break;
                    }
                }
                else
                {
                    return new JsonResult(response.Result);
                }
            }
            else
            {
                return new JsonResult(new { estatus = "warning", mensaje = "informacion incompleta", codigo = 202 });
            }

            return new JsonResult(new { estatus = "warning", messaje = "Error no castalogado o de llaves de seguridad SSL", codigo = 231 });
        }
    }
}
