using CompactadoraDeResiduos.Model.DTO;
using CreativeReduction.Bussiness.Handling;
using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Residuos
{
    public class CargaBussiness
    {
        public string estatus = "";
        public string mensaje = "";

        public async Task<List<ShowAllCarga>> cargasGet(string host)
        {
            var page = host + "/api/Cargas";

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
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaCargas = JsonConvert.DeserializeObject<List<ShowAllCarga>>(result);

                        if (listaCargas != null)
                        {
                            return listaCargas.ToList();
                        }

                        return listaCargas = new List<ShowAllCarga>();
                    }

                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }


        /// <summary>
        /// Hace una peticionn get a al api retorna un json
        /// Cambiar la url para hcer la peticion
        /// </summary>
        /// <param name="host"></param>
        /// <param name="idCarga"></param>
        /// <returns>ListUsuarioDTO</returns>
        public async Task<ResponseCargaDTO> obtenerElementoCarga(string host, int idCarga)
        {
            string page = host + "/api/Cargas/obtenerElementoCarga/" + idCarga;
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
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var respuesta = JsonConvert.DeserializeObject<ResponseDTO>(result);

                        try
                        {
                            if (respuesta.estatus == "success")
                            {


                                var CargaAMostrar = new ResponseCargaDTO()
                                {
                                    estatus = respuesta.estatus,
                                    mensaje = respuesta.mensaje,
                                    codigo = respuesta.codigo,
                                    carga = new MostrarCargaDTO()
                                    {
                                        idCarga = respuesta.data["idCarga"],
                                        fechaCreacionCarga = respuesta.data["fechaCreacionCarga"],
                                        fechaModificacion = respuesta.data["fechaModificacion"],
                                        fechaEliminacionCarga = respuesta.data["fechaEliminacionCarga"],
                                        pesoBrutoCarga = respuesta.data["pesoBrutoCarga"],
                                        pesoContenedorCarga = respuesta.data["pesoContenedorCarga"],
                                        idResiduo = respuesta.data["idResiduo"],
                                        nombreResiduo = respuesta.data["nombreResiduo"],
                                        descripcionResido = respuesta.data["descripcionResido"],
                                        codigoResiduo = respuesta.data["codigoResiduo"],
                                        idEmpleado = respuesta.data["idEmpleado"],
                                        numeroEmpleado = respuesta.data["numeroEmpleado"],
                                        nombreEmpleado = respuesta.data["nombreEmpleado"],
                                        apellidoPaterno = respuesta.data["apellidoPaterno"],
                                        apellidoMaterno = respuesta.data["apellidoMaterno"],
                                        folioCarga = respuesta.data["folioCarga"],
                                        estadoAlmacenCompleto = respuesta.data["estadoAlmacenCompleto"],
                                        estadoAlmacenCorto = respuesta.data["estadoAlmacenCorto"],
                                        fechaEnvio = respuesta.data["fechaEnvio"],
                                        nombreAlmacen = respuesta.data["nombreAlmacen"],
                                        nombreProveedorBasura = respuesta.data["nombreProveedorBasura"]
                                    }
                                };
                                return CargaAMostrar;
                            }
                            else
                            {
                                return new ResponseCargaDTO()
                                {
                                    estatus = respuesta.estatus,
                                    mensaje = respuesta.mensaje,
                                    codigo = respuesta.codigo,
                                };
                            }
                        }
                        catch (Exception)
                        {
                            return new ResponseCargaDTO()
                            {
                                estatus = "error",
                                mensaje = "Error al procesar respuesta del API",
                                codigo = 200,
                            };
                        }

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
