using FrontEndCompactadoraResiduos.Bussiness.Residuos;
using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Model.ResiduosDTO;
using FrontEndCompactadoraResiduos.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Controllers
{
    public class ResiduosController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;

        public ResiduosController(IConfiguration configuration, IHostEnvironment environment)
        {
            _environment = environment;
            _configuration = configuration;

        }
        private readonly ResiduoBussiness residuos = new ResiduoBussiness();


        /// <summary>
        /// Obtenemos la lista de residuos y se lo pasamos al controlador
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080
            var ListResiduos = residuos.HttpGet(host);//Obtenemos una lista de residuos
            var modelo = new ResiduosCatalogoViewModel() { Residuos = ListResiduos.Result };//Retornamos el modelo instancia a la vista
            return View(modelo);
        }

        /// <summary>
        /// Retornta el modal para crear un residuo
        /// </summary>
        /// <returns>MODAL</returns>
        public IActionResult CrearResiduo()
        {

            return View();
        }


        /// <summary>
        /// Metodo que guarda el residuo en el api
        /// </summary>
        /// <param name="residuo">Objeto</param>
        /// <returns>json </returns>
        public JsonResult GuardarResiduo([FromForm] ResiduoCreacionDTO residuo)
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            var imagen = Request.Form.Files[0]; //imagen que llega de dropzone


            string webRootPath = _environment.ContentRootPath; // variable de entorno que nos dice donde esta nuestro proyecto fisico
            ResiduoBussiness residuoBussiness = new ResiduoBussiness();
            var urlLocalImage = residuoBussiness.guardarLocalmenteImagen(imagen, webRootPath);// Iniciamo el guardo local
            var respuestaSendImage = residuoBussiness.GuardarResiduoFormData(residuo, host, urlLocalImage); //empezamos a mandar la data al API;
            if (respuestaSendImage.Result.ToUpper() == "\"OK\"")
            {
                residuoBussiness.eliminarImagen(urlLocalImage);

                return new JsonResult(new { estatus = "ok", mensaje = "Residuo Creado con Exito" });

            }
            else
            {
                return new JsonResult(new { estatus = "error", mensaje = "No se creo el residuo" });
            }
        }


        /// <summary>
        /// Encuentra el objeto a editar, 
        /// 
        /// </summary>
        /// <returns>View</returns>
        public IActionResult EditarResiduo()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            //recibimos el objeto json desde el request
            var jsonResiduo = Request.Form["datos"];

            //deserializamos
            var _oResiduo = JsonConvert.DeserializeObject<ResiduoBindingDTO>(jsonResiduo);

            //Obtenemos el Residuo
            var getResiduoObjet = residuos.obtenerElemento(host, _oResiduo.iId);

            var modelo = new ItemResiduoViewModel() { residuo = getResiduoObjet.Result };


            return View(modelo);
        }

        /// <summary>
        /// Recibimos los cambios y lo mandamos al API
        /// </summary>
        /// <param name="residuo"></param>
        /// <returns></returns>
        public JsonResult ActualizarResiduo()
        {
            //Obtenemos el form data sin la imagen

            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            string webRootPath = _environment.ContentRootPath; // variable de entorno que nos dice donde esta nuestro proyecto fisico

            var JSONRESIDUO = Request.Form["datos"]; //Obtenemos el JSON
            try
            {
                var ResiduoNuevo = JsonConvert.DeserializeObject<ResiduoCreacionDTO>(JSONRESIDUO);//deserializamos el objeto
                var residuoAnterior = residuos.obtenerElemento(host, ResiduoNuevo.iId); //obtenemos el objeto anterior para obtener su imagen
                var imagen = residuos.obtenerImagen(webRootPath, ResiduoNuevo.cCodigo, residuoAnterior.Result.cImagen);//obtenemos la imagen anterior

                var respuesta = residuos.enviarActualizacion(ResiduoNuevo, host, imagen.Result); //Mandmos a actualizar los datos
                if (respuesta.Result.ToString().ToUpper() != "\"OK\"")
                {
                    return new JsonResult(new { estatus = "error", mensaje = "Error al conectar con el servidor" });

                }
                else
                {
                    residuos.eliminarImagen(imagen.Result);//funcion void que elimina la imagen que se acaba de crear

                    return new JsonResult(new { estatus = "success", mensaje = "Residuo Creado con exito!!!" });
                }



            }
            catch (Exception ex)
            {
                return new JsonResult(new { estatus = "error", mensaje = "Error al recibir datos (try)" });
            }

        }

        /// <summary>
        /// Controlador que devuelve una vista para cambiar la imagen del residuo
        /// 
        /// </summary>
        /// <returns>View</returns>
        public IActionResult CambiarImagen()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            //recibimos el objeto json desde el request
            var jsonResiduo = Request.Form["datos"];

            //deserializamos
            var _oResiduo = JsonConvert.DeserializeObject<ResiduoBindingDTO>(jsonResiduo);

            //Obtenemos el Residuo
            var getResiduoObjet = residuos.obtenerElemento(host, _oResiduo.iId);

            var modelo = new ItemResiduoViewModel() { residuo = getResiduoObjet.Result };


            return View(modelo);
        }


        public JsonResult UpdateImagen(ResiduoBindingDTO residuo)
        {
            var imagen = Request.Form.Files[0]; //Obtener la imagen 
            if (imagen == null)
            {
                return new JsonResult(new { estatus = "error", mensaje = "Error al intentar guardar la imagen" });
            }
            else
            {
                var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

                string webRootPath = _environment.ContentRootPath; // variable de entorno que nos dice donde esta nuestro proyecto fisico

                var imgSaveLocal = residuos.guardarLocalmenteImagen(imagen, webRootPath);//url local de la imagen que se acaba de guardar

                var _OResiduo = residuos.obtenerElemento(host, residuo.iId);
                var _oResiduoCreacion = new ResiduoCreacionDTO()
                {
                    iId = _OResiduo.Result.iId,
                    cNombre = _OResiduo.Result.cNombre,
                    cCodigo = _OResiduo.Result.cCodigo,
                    cDescripcion = _OResiduo.Result.cDescripcion,
                };

                var respuesta = residuos.enviarActualizacion(_oResiduoCreacion, host, imgSaveLocal);

                try
                {
                    if (respuesta.Result.ToString().ToUpper() != "\"OK\"")
                    {
                        return new JsonResult(new { estatus = "error", mensaje = "Error al conectar con el servidor" });

                    }
                    else
                    {
                        residuos.eliminarImagen(imgSaveLocal);//funcion void que elimina la imagen que se acaba de crear

                        return new JsonResult(new { estatus = "success", mensaje = "Residuo Creado con exito!!!" });
                    }
                }
                catch(Exception ex)
                {
                    return new JsonResult(new { estatus = "error", mensaje = "Error al recibir respuesta" });

                }


            }

        }

        /// <summary>
        /// EliminarResiduo funcion que recibe un id para eliminar el residuo
        /// 
        /// </summary>
        /// <returns>mensajes de error o de ok </returns>
        public JsonResult EliminarResiduo()
        {
            var host = _configuration.GetValue<string>("HostAPI"); //Host del api localhost:8080 | 127.0.0.1:8080

            var idResiduo = Request.Form["datos"]; //obtenemos el id del residuo  por el json
            var _oResiduo = JsonConvert.DeserializeObject<ResiduoBindingDTO>(idResiduo); //convertimos en objeto el id del residuo

            var respuesta = residuos.eliminarResiduo(host, _oResiduo.iId);
            switch (respuesta.Result)
            {
                case "ok":
                    return new JsonResult(new { estatus = "success", mensaje = "Residuo eliminado con exito" });
                    break;
                case "error_request":
                    return new JsonResult(new { estatus = "error", mensaje = "Error al intentar conectarse con el API" });
                    break;
                case "error":
                    try
                    {
                        var respuestraString = respuesta.Result.ToString();
                        return new JsonResult(new { estatus = "error", mensaje = respuestraString });

                    }
                    catch (Exception)
                    {
                        return new JsonResult(new { estatus = "error", mensaje = "No logre convertir el error en texto" });

                    }
                    break;

            }


            return new JsonResult(new { estatus = "error", mensaje = "error al parsear la informacion" });

        }


    }
}
