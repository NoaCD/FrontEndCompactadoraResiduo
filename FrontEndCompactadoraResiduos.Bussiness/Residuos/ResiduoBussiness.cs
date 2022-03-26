using FrontEndCompactadoraResiduos.Model.ResiduosDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Bussiness.Residuos
{
    public class ResiduoBussiness
    {
        /// <summary>
        /// Peticion get al api para obtener todos los residuos
        /// </summary>
        /// <returns>Task ResiduoDTO o un null</returns>
        public async Task<List<ResiduoDTO>> HttpGet()
        {
            string page = "https://localhost:7022/api/Residuos";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaResiduos = JsonConvert.DeserializeObject<List<ResiduoDTO>>(result);

                        if (listaResiduos != null)
                        {
                            return listaResiduos.ToList();
                        }

                        return listaResiduos = new List<ResiduoDTO>();
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
