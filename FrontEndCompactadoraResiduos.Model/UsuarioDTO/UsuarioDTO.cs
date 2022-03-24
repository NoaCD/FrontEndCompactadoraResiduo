namespace FrontEndCompactadoraResiduos.Models
{
    public class UsuarioDTO
    {
        public int iId { get; set; }
        public string cNombre { get; set; }
        public string cApellidoPaterno { get; set; }
        public string cApellidoMaterno { get; set; }
        public int iNumeroEmpleado { get; set; }
        public DateTime dtFechaCreacion { get; set; }
    }
}
