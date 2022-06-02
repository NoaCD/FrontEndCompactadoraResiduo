using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Usuarios
{
    public class ConsultasEstatus
    {
        /// <summary>
        /// Hacemos una peticion al api para obtener todos los 
        /// estatus que hay en el sistema
        /// </summary>
        /// <param name="host"></param>
        /// <returns> EstatusDTO, null </returns>
        public async Task<List<EstatusDTO>> allEstatus(string host)
        {
            string page = host + "/api/Usuarios/todos-estatus";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaEstatus = JsonConvert.DeserializeObject<List<EstatusDTO>>(result);

                        if (listaEstatus != null)
                        {
                            return listaEstatus;
                        }

                        return listaEstatus = new List<EstatusDTO>();
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
