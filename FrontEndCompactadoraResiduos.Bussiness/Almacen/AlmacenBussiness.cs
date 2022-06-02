using CreativeReduction.Bussiness.Handling;
using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Almacen
{
    public class AlmacenBussiness
    {


        public async Task<List<AlmacenFrontDTO>> obtenerTodos(string host)
        {


            //*****************************************************************
            //Inicio de la funcion 
            var handling = new handlingsbussines();
            var handler = handling.hanlingbusines();
            //con esta funcion invalidamos las credenciales SSL
            // FIN DE LA FUNCION 
            //**********************************************************************
            var client = new HttpClient(handler);

            var page = host + "/api/Almacen";
            try
            {
                var response = await client.GetAsync(page);
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<AlmacenFrontDTO>>(result);
                    if (data != null)
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }



        public async Task<AlmacenFrontDTO> elemento(int id, string host)
        {
            //*****************************************************************
            //Inicio de la funcion 
            var handling = new handlingsbussines();
            var handler = handling.hanlingbusines();
            //con esta funcion invalidamos las credenciales SSL
            // FIN DE LA FUNCION 
            //**********************************************************************
            var client = new HttpClient(handler);
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
                //*****************************************************************
                //Inicio de la funcion 
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************
                using (var client = new HttpClient(handler))
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
                //*****************************************************************
                //Inicio de la funcion 
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************
                using (var client = new HttpClient(handler))
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

        /// <summary>
        /// Enviamos al API EL ID
        /// </summary>
        /// <param name="host"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> eliminarAlmacen(string host, int id)
        {
            try
            {
                string page = host + "/api/Almacen/eliminarAlmacen/" + id;
                //*****************************************************************
                //Inicio de la funcion 
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************
                using (var client = new HttpClient(handler))
                {


                    var response = await client.DeleteAsync(page);
                    var contenido = response.Content.ReadAsStringAsync();

                    var respuesta = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);
                    if (respuesta.estatus is null)
                    {
                        return new ResponseDTO()
                        {
                            estatus = "error",
                            mensaje = "LLego nulo la respuesta del api"
                        };
                    }
                    else
                    {
                        return respuesta;
                    }
                }

            }
            catch (Exception)
            {
                return new ResponseDTO()
                {
                    estatus = "error",
                    mensaje = "Error al intentar establecer conexxion con el API"
                };
            }

        }
    }
}


