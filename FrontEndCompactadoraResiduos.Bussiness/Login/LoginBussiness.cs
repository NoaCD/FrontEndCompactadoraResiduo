using CreativeReduction.Bussiness.Handling;
using CreativeReduction.Model.DTOS;
using FrontEndCompactadoraResiduos.Model.DTOS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoginBussiness
{
    public LoginBussiness()
    {
    }

    ~LoginBussiness()
    {
    }

    /// <summary>
    /// variable para el cifrado de contraseñas en la base de datos
    /// </summary>
    private static readonly string clave = "gI0t3*";

    /// <summary>
    /// Realiza la consulta del login al sistema api
    /// </summary>
    /// <param name="cUsuario">contiene el usuario ingresado en la vista</param>
    /// <param name="cContrasenia">contiene la contraseña ingresada a la vista</param>
    /// <returns>Retorna el estado del login si es activo o inactivo</returns>
    public async Task<ResponseDTO> IniciarSecionAdministrativo(string usuario, string contrasenia, string host)
    {
        ResponseDTO respuestas = new ResponseDTO();
        string _oPassword = null;
        int? _oIdDatosUsuario = null;
        string _oEstatus = null;

        var cuentadeusuario = new LoginerDTO
        {
            cNombreUsuario = usuario,
            cContrasenia = contrasenia,
        };

        ///elementos de inicio de api
        string pagina = host + "/api/Login";
        var loginJson = JsonConvert.SerializeObject(cuentadeusuario, Formatting.Indented);

        var handling = new handlingsbussines();
        var handler = handling.hanlingbusines();
        try
        {
            using (HttpClient cliente = new HttpClient(handler))
            {
                var contenidos = new StringContent(loginJson, System.Text.Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync(pagina, contenidos);
                var contenido = response.Content.ReadAsStringAsync();
                try
                {

                    if (contenido.Result != null)
                    {



                        respuestas = JsonConvert.DeserializeObject<ResponseDTO>(contenido.Result);
                    }
                }
                catch (Exception ex)
                {
                    respuestas.mensaje = "problema en la conexion";
                }
            }
        }
        catch (Exception ex)
        {
        }
        return respuestas;
    }

    /// <summary>
    /// Genera la consulta para la obtencion de informacion del usuario
    /// </summary>
    /// <param name="id">id a obtener del usuario</param>
    /// <param name="host">direccion de la obtencion</param>
    /// <returns>Retorna un DTO de informacion del usuario</returns>
    public async Task<datosdeUsuarioDTO> ObtenerUsuarioLoged(string id, string host)
    {
        datosdeUsuarioDTO InformacionUsuario = new datosdeUsuarioDTO();
        string pagina = host + "/api/Usuarios/obtener-elementopos";
        var usuariologin = JsonConvert.SerializeObject(new { iId_User = id }, Formatting.Indented);

        //instanciando referencia hander
        var handling = new handlingsbussines();
        var handler = handling.hanlingbusines();
        try
        {
            using (HttpClient cliente = new HttpClient(handler))
            {
                var consultausuario = new StringContent(usuariologin, System.Text.Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync(pagina, consultausuario);
                var contenido = response.Content.ReadAsStringAsync();
                contenido.Wait();
                if (contenido.Result != "" || contenido.Result != null || contenido.Result != "null")
                {
                    JObject r = JObject.Parse(contenido.Result);

                    TiposUsuarioDTO tipousuario = new TiposUsuarioDTO();
                    tipousuario.cNombre = (string)r["catTipoUsuarioDTO"]["cNombre"];
                    tipousuario.iId = (int)r["catTipoUsuarioDTO"]["iId"];
                    tipousuario.cCodigo = (string)r["catTipoUsuarioDTO"]["cCodigo"];

                    switch (tipousuario.codigo)
                    {
                        case "supersu":
                            InformacionUsuario = JsonConvert.DeserializeObject<datosdeUsuarioDTO>(contenido.Result);
                            return InformacionUsuario;
                            break;
                        case "moderador":
                            InformacionUsuario = JsonConvert.DeserializeObject<datosdeUsuarioDTO>(contenido.Result);
                            return InformacionUsuario;
                            break;
                        case "":
                            InformacionUsuario.Nombre = "noType";
                            break;
                        default:
                            InformacionUsuario.Nombre = "noPermice";
                            break;
                    };

                }
                else
                {
                    InformacionUsuario.Nombre = "noInformacion";
                }
            }
        }
        catch (Exception ex)
        {
            InformacionUsuario.Nombre = "contactoServidor";
            return InformacionUsuario;
        }

        return InformacionUsuario;
    }
}