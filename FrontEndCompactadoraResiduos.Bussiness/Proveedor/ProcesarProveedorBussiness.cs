﻿using CreativeReduction.Bussiness.Handling;
using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Proveedor
{
    public class ProcesarProveedorBussiness
    {

        /// <summary>
        /// Pärseamos y enviamos la infromacion al API
        /// </summary>
        /// <param name="proveedorFront"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> crear(string proveedorFront, string host)
        {

            try
            {
                //*****************************************************************
                //Inicio de la funcion 
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************

                string page = host + "/api/Proveedores";

                using (var client = new HttpClient(handler))
                {
                    var content = new StringContent(proveedorFront, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var _oRespuesta = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);
                        if (_oRespuesta == null)
                        {
                            return new ResponseDTO()
                            {
                                estatus = "error",
                                mensaje = "El API retorno nulo",
                                codigo = 200
                            };

                        }
                        else
                        {
                            return _oRespuesta;
                        }

                    }
                    catch (Exception ex)
                    {
                        return new ResponseDTO()
                        {
                            estatus = "error",
                            mensaje = "Error al intentar hacer la peticion con el API",
                            codigo = 200
                        };
                        ;

                    }


                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }
        /// <summary>
        /// Obtenemos todos los poveedores que nos proprociona el APÍ
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<List<ProveedorFrontDTO>> GetProveedores(string host)
        {
            string page = host + "/api/Proveedores";
            try
            {
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();

                using (var client = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaUsuarios = JsonConvert.DeserializeObject<List<ProveedorFrontDTO>>(result);

                        if (listaUsuarios != null)
                        {
                            return listaUsuarios.ToList();
                        }
                        else
                        {
                            return new List<ProveedorFrontDTO>();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                return new List<ProveedorFrontDTO>();
            }

        }

        public async Task<ProveedorFrontDTO> GetElementProveedor(int id, string host)
        {
            string page = host + "/api/Proveedores/elementoProveedor/" + id;
            try
            {
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                using (var client = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaUsuarios = JsonConvert.DeserializeObject<ProveedorFrontDTO>(result);

                        if (listaUsuarios != null)
                        {
                            return listaUsuarios;
                        }
                        else
                        {
                            return new ProveedorFrontDTO();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                return new ProveedorFrontDTO();
            }

        }

        public async Task<ResponseDTO> editar(ProveedorFrontDTO oProveedor, string host)
        {
            ProveedorDB proveedorDB = new ProveedorDB()
            {
                Id = oProveedor.id,
                Nombre = oProveedor.nombre,
                Descripcion = oProveedor.descripcion,
                Direccion = oProveedor.direccion,
                Rfc = oProveedor.rfc,
            };
            try
            {

                string page = host + "/api/Proveedores";
                var proveedorJSON = JsonConvert.SerializeObject(proveedorDB);
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                using (var client = new HttpClient(handler))
                {
                    var content = new StringContent(proveedorJSON, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(page, content);

                    var contenido = response.Content.ReadAsStringAsync();

                    try
                    {
                        var _oRespuesta = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);
                        if (_oRespuesta == null)
                        {
                            return new ResponseDTO()
                            {
                                estatus = "error",
                                mensaje = "El API retorno nulo",
                                codigo = 200
                            };

                        }
                        else
                        {
                            return _oRespuesta;
                        }

                    }
                    catch (Exception ex)
                    {
                        return new ResponseDTO()
                        {
                            estatus = "error",
                            mensaje = "Error al intentar hacer la peticion con el API",
                            codigo = 200
                        };
                        ;

                    }


                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseDTO> EliminarProveedor(string host, int id)
        {
            try
            {
                //*****************************************************************
                //Inicio de la funcion 
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************

                string page = host + "/api/Proveedores/eliminarProveedor/" + id;
                using (var client = new HttpClient(handler))
                {

                    var response = await client.DeleteAsync(page);
                    var contenido = response.Content.ReadAsStringAsync();

                    var respuesta = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);
                    if (respuesta is null || respuesta.estatus is null)
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