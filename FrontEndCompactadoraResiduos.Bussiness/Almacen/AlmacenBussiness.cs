using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Bussiness.Almacen
{
    public class AlmacenBussiness
    {
        readonly HttpClient client = new HttpClient();



        public async Task<List<AlmacenFrontDTO>> obtenerTodos(string host)
        {
            var data = new List<AlmacenFrontDTO>();
            var page = host + "/api/Almacen";
            try
            {
                var response = await client.GetAsync(page);
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<AlmacenFrontDTO>>(result);
                }
            }
            catch (Exception ex)
            {
                data = new List<AlmacenFrontDTO> { new AlmacenFrontDTO() };
            }
            return data;
        }



        public async Task<AlmacenFrontDTO> elemento(int id, string host)
        {

            var data = new AlmacenFrontDTO();
            var page = host + "/api/Almacen/elementoAlmacen/" + id;
            try
            {
                var response = await client.GetAsync(page);
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<AlmacenFrontDTO>(result);
                }
            }
            catch (Exception ex)
            {
                data = new AlmacenFrontDTO();
            }
            return data;
        }

        public async Task<ResponseDTO> editarAlmacen(string host, AlmacenFrontDTO oAlmacen)
        {
            //para cambiar a true un almacen
            //var prueba = new AlmacenFrontDTO() { Default = true, clave = oAlmacen.clave, nombre = oAlmacen.nombre, ubicacion = oAlmacen.ubicacion, id = oAlmacen.id };

            var data = new ResponseDTO();
            var page = host + "/api/Almacen";
            var AlmacenJSON = JsonConvert.SerializeObject(oAlmacen);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(AlmacenJSON, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var respuesta = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);
                        return respuesta;

                    }
                    catch (Exception)
                    {
                        return new ResponseDTO()
                        {
                            estatus = "error",
                            mensaje = "Tenemos un problema al intentar recibir" +
                            "la respuesta del servidor",
                            codigo = 500

                        };

                    }

                }

            }
            catch (Exception)
            {
                return new ResponseDTO()
                {
                    estatus = "error",
                    mensaje = "Tenemos un problema al intentar recibir" +
                              "la respuesta del servidor",
                    codigo = 500

                };
            }

        }

        public async Task<ResponseDTO> crearAlmacen(string host, string AlmacenJSON)
        {
            var data = new ResponseDTO();
            var page = host + "/api/Almacen";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(AlmacenJSON, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var respuesta = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);

                        if (respuesta.estatus is null)
                        {
                            return new ResponseDTO()
                            {
                                estatus = "error",
                                mensaje = "LLego vacio la respuesta del API" +
                            "la respuesta del servidor",
                                codigo = 500

                            };
                        }
                        else
                        {
                            return respuesta;
                        }

                    }
                    catch (Exception)
                    {
                        return new ResponseDTO()
                        {
                            estatus = "error",
                            mensaje = "Tenemos un problema al intentar recibir" +
                            "la respuesta del servidor",
                            codigo = 500

                        };

                    }

                }

            }
            catch (Exception)
            {
                return new ResponseDTO()
                {
                    estatus = "error",
                    mensaje = "Tenemos un problema al intentar recibir" +
                              "la respuesta del servidor",
                    codigo = 500

                };
            }

        }
    }
}


