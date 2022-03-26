using FrontEndCompactadoraResiduos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Bussiness.Usuarios
{
    public class UsuarioBussiness
    {

        /// <summary>
        /// Hace una peticionn get a al api  retorna un json
        /// Cambiar la url para hcer la peticion
        /// </summary>
        /// <returns></returns>
        public async Task<List<UsuarioDTO>> HttpGet()
        {
            string page = "https://localhost:7022/api/Usuarios/obtener-todo";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                         var listaUsuarios = JsonConvert.DeserializeObject<List<UsuarioDTO>>(result);
                        
                        if (listaUsuarios != null)
                        {
                            return listaUsuarios.ToList();
                        }

                        return listaUsuarios = new List<UsuarioDTO>();
                    }
                    
                }

            }
            catch (Exception ex)
            {
              return null;
            }   
      
        }


        /// <summary>
        /// Peticion para obtener un elemento de usuario
        /// </summary>
        /// <returns></returns>
        public async Task<UsuarioDTO> obtenerElemento(int id)
        {
            string page = "https://localhost:7022/api/Usuarios/obtener-elemento/" + id;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaUsuarios = JsonConvert.DeserializeObject<UsuarioDTO>(result);

                        if (listaUsuarios != null)
                        {
                            return listaUsuarios;
                        }

                        return listaUsuarios = new UsuarioDTO ();
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
