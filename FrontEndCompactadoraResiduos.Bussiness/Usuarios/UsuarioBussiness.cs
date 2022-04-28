using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Bussiness.Usuarios
{
    public class UsuarioBussiness
    {

        /// <summary>
        /// Hace una peticionn get a al api  retorna un json
        /// Cambiar la url para hcer la peticion
        /// </summary>
        /// <returns>ListUsuarioDTO</returns>
        public async Task<List<UsuarioDTO>> HttpGet()
        {
            string page = "https://localhost:44307/api/Usuarios/obtener-todo";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaUsuarios = JsonConvert.DeserializeObject<List<UsuarioDTO>>(result);

                        if (listaUsuarios != null)
                        {
                            return listaUsuarios.ToList();
                        }
                        listaUsuarios = new List<UsuarioDTO>()
                        {
                            new UsuarioDTO() {cNombre = "No se encontro informacion ",
                                               cApellidoPaterno = "Registrar unformacion ",
                                              cApellidoMaterno = " o contacte a su administrador de TI"
                            }

                        };
                        return listaUsuarios;
                    }

                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        /// <summary>
        /// Peticion para obtener un elemento de usuario
        /// </summary>
        /// <returns></returns>
        public async Task<UsuarioDTO> obtenerElemento(int id)
        {
            string page = "https://localhost:44307/api/Usuarios/obtener-elemento/" + id;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaUsuarios = JsonConvert.DeserializeObject<UsuarioDTO>(result);

                        if (listaUsuarios != null)
                        {
                            return listaUsuarios;
                        }

                        return listaUsuarios = new UsuarioDTO();
                    }

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Funcions que envia un json del usuario a crear
        /// </summary>
        /// <param name="usuario"> Objeto UsuarioCreacionDTO </param>
        /// <param name="host">constante del api</param>
        /// <returns></returns>
        public async Task<string> GuardarUsuario(UsuarioCreacionDTO usuario, string host)
        {
            try
            {

                string page = host + "/api/Usuarios";
                var usuarioJSON = JsonConvert.SerializeObject(usuario);
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(usuarioJSON, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var _oUsuarioCreado = JsonConvert.DeserializeObject<UsuarioDTO>(contenido.Result);
                        if (_oUsuarioCreado == null)
                        {
                            return contenido.Result.ToString();

                        }
                        else
                        {
                            return _oUsuarioCreado.iId.ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        return ex.Message.ToString();

                    }


                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Recibimo un objeto UsuarioEdicionDTO para enviarselo al 
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<string> ActualizarUsuario(UsuarioEdicionDTO usuario, string host)
        {
            try
            {
                string page = host + "/api/Usuarios";
                var usuarioJSON = JsonConvert.SerializeObject(usuario);
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(usuarioJSON, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var respuesta = contenido.Result.ToString();
                        return respuesta;

                    }
                    catch (Exception)
                    {
                        return "Intentar convertir la respuesta del API en un string";

                    }

                }

            }
            catch (Exception)
            {
                return "Fallo al conectarse con la API";
            }
        }

        /// <summary>
        /// La funcion eliminar usuario, se encarga de enviar la solicitud al api para eliminarlo
        /// </summary>
        /// <param name="iId">id del usuario a eliminar</param>
        /// <param name="host"> url base del api </param>
        /// <returns></returns>
        public async Task<string> eliminarUsuario(UsuarioEliminacionDTO usuarioEliminacionDTO, string host)
        {

            try
            {
                string page = host + "/api/Usuarios/EliminarUsuario/" + usuarioEliminacionDTO.iId_User;
                var usuarioJSON = JsonConvert.SerializeObject(usuarioEliminacionDTO);
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(usuarioJSON, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var respuesta = JsonConvert.DeserializeObject<RespuestaDTO>(contenido.Result);
                        return respuesta.estatus.ToString();

                    }
                    catch (Exception)
                    {
                        return "Intentar convertir la respuesta del API en un string";

                    }

                }

            }
            catch (Exception)
            {
                return "Fallo al conectarse con la API";
            }

        }
    }
}
