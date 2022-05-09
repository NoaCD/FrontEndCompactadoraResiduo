using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Bussiness.Home
{
    public class HomeBussiness
    {
        static HttpClient client = new HttpClient();


        /// <summary>
        /// obtenemos el total de Cargas
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> getTotalCargas(string host)
        {
            var data = new ResponseDTO();
            var page = host + "/api/TotalCargas";

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
