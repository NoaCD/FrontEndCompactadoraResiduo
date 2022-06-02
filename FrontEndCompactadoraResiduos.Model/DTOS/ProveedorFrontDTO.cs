namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class ProveedorFrontDTO
    {
        public int? id { get; set; }
        public string? nombre { get; set; }
        public string? direccion { get; set; }
        public string? rfc { get; set; }
        public string? descripcion { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
    }
}
