using CreativeReduction.Bussiness.Handling;
using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Usuarios
{
    public class TipoUsuarioBussiness
    {
        public async Task<List<TiposUsuarioDTO>> todosTipoUsuarios(string host)
        {

            string page = host + "/api/Usuarios/tipos-de-usuarios";
            try
            {
                //*****************************************************************
                //Inicio de la funcion 
                var handling = new handlingsbussines();
                var handler = handling.hanlingbusines();
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************
                using (HttpClient client = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaUsuarios = JsonConvert.DeserializeObject<List<TiposUsuarioDTO>>(result);

                        if (listaUsuarios != null)
                        {
                            return listaUsuarios;
                        }

                        return listaUsuarios = new List<TiposUsuarioDTO>();
                    }

                }

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
