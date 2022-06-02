using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;

namespace FrontEndCompactadoraResiduos.Bussiness.Embarque
{

    public class EmbarqueBussiness
    {


        public async Task<List<EmbarqueDTO>> getAllEmbarques(string host)
        {
            var page = host + "/api/Embarque/TodosEmbarques";

            try
            {
                //*****************************************************************
                //Inicio de la funcion 
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************

                using (var client = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaEmbarque = JsonConvert.DeserializeObject<List<EmbarqueDTO>>(result);

                        if (listaEmbarque != null)
                        {
                            return listaEmbarque.ToList();
                        }

                        return listaEmbarque = new List<EmbarqueDTO>();
                    }

                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public async Task<EmbarqueDTO> getElementEmbarque(string host, int id)
        {

            var page = host + "/api/Embarque/" + id;

            try
            {
                //*****************************************************************
                //Inicio de la funcion 
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };
                //con esta funcion invalidamos las credenciales SSL
                // FIN DE LA FUNCION 
                //**********************************************************************

                using (var client = new HttpClient(handler))
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var listaEmbarque = JsonConvert.DeserializeObject<EmbarqueDTO>(result);

                        if (listaEmbarque != null)
                        {
                            return listaEmbarque;
                        }

                        return listaEmbarque = new EmbarqueDTO();
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
