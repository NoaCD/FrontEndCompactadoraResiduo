using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Bussiness.Proveedor
{
    public class ProcesarProveedorBussiness
    {
        /// <summary>
        /// Pärseamos y enviamos la infromacion al API
        /// </summary>
        /// <param name="proveedorFront"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> crear(ProveedorFrontDTO proveedorFront, string host)
        {
            ProveedorDB proveedorDB = new ProveedorDB()
            {
                Nombre = proveedorFront.nombre,
                Descripcion = proveedorFront.descripcion,
                Direccion = proveedorFront.direccion,
                Rfc = proveedorFront.rfc,
            };
            try
            {

                string page = host + "/api/Proveedores";
                var proveedorJSON = JsonConvert.SerializeObject(proveedorDB);
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(proveedorJSON, System.Text.Encoding.UTF8, "application/json");

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

        public async Task<List<ProveedorFrontDTO>> GetProveedores(string host)
        {
            string page = host + "/api/Proveedores";
            try
            {
                using (HttpClient client = new HttpClient())
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
                using (HttpClient client = new HttpClient())
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
                using (HttpClient client = new HttpClient())
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
    }
}