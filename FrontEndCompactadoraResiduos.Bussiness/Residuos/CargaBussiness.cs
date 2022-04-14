using CompactadoraDeResiduos.Model.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                using (HttpClient client = new HttpClient())
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

    }
}
