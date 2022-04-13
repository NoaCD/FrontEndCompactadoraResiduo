using FrontEndCompactadoraResiduos.Model.DTOS;
using FrontEndCompactadoraResiduos.Model.ResiduosDTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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
        public async Task<List<ResiduoDTO>> HttpGet(string host)
        {

            string page = host + "/api/Residuos";
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="residuo"> Objeto </param>
        /// <param name="cImaagen">URL local de la imagen, </param>
        /// <returns></returns>
        public async Task<string> GuardarResiduoFormData(ResiduoCreacionDTO residuo, string host, string cImagen)
        {
            var filePath = cImagen;


            using (HttpClient client = new HttpClient())
            {

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    //Add other fields
                    multipartFormContent.Add(new StringContent(residuo.cNombre), name: "cNombre");
                    multipartFormContent.Add(new StringContent(residuo.cCodigo), name: "cCodigo");
                    multipartFormContent.Add(new StringContent(residuo.cDescripcion), name: "cDescripcion");



                    //Add the file
                    var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    multipartFormContent.Add(fileStreamContent, name: "fImagen", fileName: "house.png");

                    //Send it


                    try
                    {
                        //string page = host + "/api/Residuos" + "";
                        var page = new Uri(string.Concat(host, "/api/Residuos"));


                        var response = await client.PostAsync(page, multipartFormContent);

                        var contenido = response.Content.ReadAsStringAsync();
                        return await contenido;


                    }
                    catch (Exception ex)
                    {
                        return "error-request";
                    }

                }
            }

        }


        /// <summary>
        /// Actualizar residuo, enviaremos un objeto por form data, para que se guarden en el API
        /// error_request, algo salio mal al intentar guardar en la API
        /// </summary>
        /// <param name="residuo"> Objeto </param>
        /// <param name="cImaagen">URL local de la imagen, </param>
        /// <returns></returns>
        public async Task<string> enviarImagenControlador(ResiduoCreacionDTO residuo, string host, string cImagen)
        {
            var filePath = cImagen;


            using (HttpClient client = new HttpClient())
            {

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    //Add other fields
                    multipartFormContent.Add(new StringContent(residuo.cNombre), name: "cNombre");
                    multipartFormContent.Add(new StringContent(residuo.cCodigo), name: "cCodigo");
                    multipartFormContent.Add(new StringContent(residuo.cDescripcion), name: "cDescripcion");



                    //Add the file
                    var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    multipartFormContent.Add(fileStreamContent, name: "fImagen", fileName: "house.png");

                    //Send it


                    try
                    {
                        string sub = "/api/Residuos";
                        //string page = host + "/api/Residuos" + "";
                        var page = new Uri(string.Concat(host, "/api/Residuos"));


                        var response = await client.PostAsync(page, multipartFormContent);

                        var contenido = response.Content.ReadAsStringAsync();
                        return await contenido;


                    }
                    catch (Exception ex)
                    {
                        return "error-request";
                    }

                }
            }

        }

        public async Task<string> enviarActualizacion(ResiduoCreacionDTO residuo, string host, string imagen)
        {
            string filePath = imagen;
            using (HttpClient client = new HttpClient())
            {

                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    //Add other fields
                    multipartFormContent.Add(new StringContent(residuo.cNombre), name: "cNombre");
                    multipartFormContent.Add(new StringContent(residuo.cCodigo), name: "cCodigo");
                    multipartFormContent.Add(new StringContent(residuo.cDescripcion), name: "cDescripcion");



                    //Add the file
                    var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    multipartFormContent.Add(fileStreamContent, name: "fImagen", fileName: "panda.png");

                    //Send it

                    var page = host +"/api/residuos/"+ residuo.iId.ToString();
                    var response = await client.PutAsync(page, multipartFormContent);

                    var contenido = response.Content.ReadAsStringAsync();
                    return contenido.Result;
               

                }
            }


            
        }


        /// <summary>
        /// Descarga imagen de internet
        /// retorna la ruta de la imagen que se acaba de guardar
        /// </summary>
        /// <param name="cImagen"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        public async Task<string> obtenerImagen(string webRootPath, string fileName, string url)
        {
            Uri uri = new Uri(url);

            string folderName = "Temp/Residuo/";
            string directoryPath = Path.Combine(webRootPath, folderName);
            using var httpClient = new HttpClient();

            // Get the file extension
            var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
            var fileExtension = Path.GetExtension(uriWithoutQuery);

            // Create file path and ensure directory exists
            var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
            Directory.CreateDirectory(directoryPath);

            // Download the image and write to the file
            var imageBytes = await httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(path, imageBytes);
            return path.ToString();
        }


        /// <summary>
        /// Funcion`para guadar una imagen localmente para enviarselo al servidor
        /// es un medio para enviarlo al servidor
        /// </summary>
        /// <param name="imagen"></param>
        /// <param name="webRootPath"></param>
        /// <returns></returns>
        public string guardarLocalmenteImagen(IFormFile imagen, string webRootPath)
        {
            string folderName = "Temp/Residuo/"; //Carpeta que se quiere guardar, estara en la raiz del proyecto principal

            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            string extention = imagen.ContentType.Split("/")[1];
            string fileName = imagen.FileName;
            string fullPath = Path.Combine(newPath, fileName);
            string envpath = folderName + "/" + fileName;
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                imagen.CopyTo(stream);
            }
            return fullPath.ToString();
        }

        /// <summary>
        /// Metodo para eliminar el residuo que llegue por parametro
        /// </summary>
        /// <param name="iId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> eliminarResiduo(string host, int idResiduo)
        {

            try
            {

                string page = host + "/api/Residuos/" + idResiduo;
                using (HttpClient client = new HttpClient())
                {
                    

                    var response = await client.DeleteAsync(page);

                    var contenido = response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return "ok";

                    }
                    else
                    {
                        return contenido.Result;
                    }
                }

            }
            catch (Exception )
            {
                return "error-request";
            }

            
        }

        /// <summary>
        /// Funcion para eliminar una imagen de la carpeta temp de imagenes
        /// </summary>
        /// <param name="path">URL FISICA DE LA IMAGEN</param>
        public void eliminarImagen(string path)
        {
            string fullPath = path;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        /// <summary>
        /// Peticion para obtener un elemento de residuo
        /// </summary>
        /// <returns></returns>
        public async Task<ResiduoDTO> obtenerElemento(string host, int id)
        {
            string page = host + "/api/Residuos/" + id;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(page))
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var itemResiduo = JsonConvert.DeserializeObject<ResiduoDTO>(result);

                        if (itemResiduo != null)
                        {
                            return itemResiduo;
                        }

                        return itemResiduo = new ResiduoDTO();
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
