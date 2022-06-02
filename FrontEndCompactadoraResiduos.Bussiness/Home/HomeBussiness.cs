using CreativeReduction.Bussiness.Handling;
using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Home
{
    public class HomeBussiness
    {


        /// <summary>
        /// obtenemos el total de Cargas
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> getTotalCargas(string host)
        {
            var data = new ResponseDTO();
            var page = host + "/api/TotalCargas";
            //*****************************************************************
            //Inicio de la funcion 
            var handling = new handlingsbussines();
            var handler = handling.hanlingbusines();
            //con esta funcion invalidamos las credenciales SSL
            // FIN DE LA FUNCION 
            //**********************************************************************
            var client = new HttpClient(handler);

            var response = await client.GetAsync(page);
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<ResponseDTO>(result);

            }
            return data;
        }

        /// <summary>
        /// obtenemos el total de Usuarios
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> getTotalUsuarios(string host)
        {
            var data = new ResponseDTO();
            var page = host + "/api/TotalUsuarios";
            //*****************************************************************
            //Inicio de la funcion 
            var handling = new handlingsbussines();
            var handler = handling.hanlingbusines();
            //con esta funcion invalidamos las credenciales SSL
            // FIN DE LA FUNCION 
            //**********************************************************************
            var client = new HttpClient(handler);

            var response = await client.GetAsync(page);
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<ResponseDTO>(result);

            }
            return data;
        }
        /// <summary>
        /// obtenemos el total de Proveedores
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> getTotalProveedores(string host)
        {
            var data = new ResponseDTO();
            var page = host + "/api/totalProveedores";
            //*****************************************************************
            //Inicio de la funcion 
            var handling = new handlingsbussines();
            var handler = handling.hanlingbusines();
            //con esta funcion invalidamos las credenciales SSL
            // FIN DE LA FUNCION 
            //**********************************************************************
            var client = new HttpClient(handler);
   
            var response = await client.GetAsync(page);
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<ResponseDTO>(result);

            }
            return data;
        }

        /// <summary>
        /// obtenemos el total de Residuos
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> getTotalResiduos(string host)
        {
            var data = new ResponseDTO();
            var page = host + "/api/TotalResiduos";
            //*****************************************************************
            //Inicio de la funcion 
            var handling = new handlingsbussines();
            var handler = handling.hanlingbusines();
            //con esta funcion invalidamos las credenciales SSL
            // FIN DE LA FUNCION 
            //**********************************************************************
            var client = new HttpClient(handler);

            var response = await client.GetAsync(page);
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<ResponseDTO>(result);

            }
            return data;
        }



    }
}
