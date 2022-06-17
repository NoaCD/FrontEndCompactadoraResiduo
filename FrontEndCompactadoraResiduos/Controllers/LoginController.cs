using CreativeReduction.Model.DTOS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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
        public async Task<ActionResult> Acceso(LoginerDTO logindto)
        {
            var host = _configuration.GetValue<string>("HostAPI");
            var respuesta = new LoginBussiness();
            if (logindto.cNombreUsuario != null && logindto.cContrasenia != null)
            {
                var response = respuesta.IniciarSecionAdministrativo(logindto.cNombreUsuario, logindto.cContrasenia, host);
                response.Wait();

                if (response.Result.estatus == "success")
                {
                    var elementosLogin = respuesta.ObtenerUsuarioLoged(response.Result.mensaje, host);
                    elementosLogin.Wait();

                    switch (elementosLogin.Result.Nombre)
                    {
                        case "contactoServidor":
                            //return new JsonResult(new { estatus = "warning", mensaje = "no se pudo establecer contacto con el servidor", codigo = 202 });
                            ModelState.AddModelError("Login", "no se pudo establecer contacto con el servidor");
                            return View("Login", logindto);
                            break;

                        case "noInformacion":
                            //return new JsonResult(new { estatus = "warning", mensaje = "No se pudo obtener informacion contacte a su desarrollador", codigo = 202 });
                            ModelState.AddModelError("Login", "No se pudo obtener informacion contacte a su desarrollador");
                            return View("Login", logindto);
                            break;
                        case "noType":
                            //return new JsonResult(new { estatus = "warning", mensaje = "No se pudo enparejar con un tipo de usuario contacte a TI", codigo = 202 });
                            ModelState.AddModelError("Login", "No se pudo enparejar con un tipo de usuario contacte a TI");
                            return View("Login", logindto);
                            break;
                        case "noPermice":
                            //return new JsonResult(new { estatus = "watning", mensaje = "No cuenta con los permisos para acceder a la plataforma de administración", codigo = 202 });
                            ModelState.AddModelError("Login", "No cuenta con los permisos para acceder a la plataforma de administración");
                            return View("Login", logindto);
                            break;
                        case null:
                            //return new JsonResult(new { estatus = "warning", mensaje = "No se pudo obtener informacion de usuarios", codigo = 202 });
                            ModelState.AddModelError("Login", "No se pudo obtener informacion de usuarios");
                            return View("Login", logindto);
                            break;
                        default:

                            //Aqui ya tenemos todos lo datos
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,elementosLogin.Result.Nombre),
                                new Claim("apellidoPaterno",elementosLogin.Result.ApellidoMaterno),
                                new Claim("apellidoMaterno",elementosLogin.Result.ApellidoPaterno),
                                new Claim("idUsuario", elementosLogin.Result.iId.ToString()),
                                new Claim("idTipoUsuario",elementosLogin.Result.iId_TipoUsuario.ToString()),
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return RedirectToAction("Index", "Home");
                            break;
                    }
                }
                else
                {
                    if(response.Result.mensaje is null)
                    {
                        //return new JsonResult(response.Result);
                        ModelState.AddModelError("Login", "No hay conexion con el API" );
                    }
                    else
                    {
                        ModelState.AddModelError("Login", response.Result.mensaje.ToString());
                        return View("Login", logindto);
                    }

                    
                }
            }
            else
            {
                //return new JsonResult(new { estatus = "warning", mensaje = "informacion incompleta", codigo = 202 });
                ModelState.AddModelError("Login", "informacion incompleta");
                return View("Login", logindto);

            }

            //return new JsonResult(new { estatus = "warning", messaje = "Error no castalogado o de llaves de seguridad SSL", codigo = 231 });
            ModelState.AddModelError("Login", "Error no castalogado o de llaves de seguridad SSL");
            return View("Login", logindto);
        }

        public async Task<IActionResult> salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
