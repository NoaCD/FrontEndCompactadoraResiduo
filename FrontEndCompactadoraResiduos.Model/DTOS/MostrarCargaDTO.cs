namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class MostrarCargaDTO
    {
        public int? idCarga { get; set; }
        public DateTime? fechaCreacionCarga { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public DateTime? fechaEliminacionCarga { get; set; }
        public DateTime? fechaEnvio { get; set; }
        public double pesoBrutoCarga { get; set; }
        public double? pesoContenedorCarga { get; set; }
        public int? idResiduo { get; set; }
        public string? nombreResiduo { get; set; }
        public string? descripcionResido { get; set; }
        public string codigoResiduo { get; set; }
        public int? idEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string folioCarga { get; set; }
        public int numeroEmpleado { get; set; }
        public string? nombreAlmacen { get; set; }
        public string? nombreProveedorBasura { get; set; }
        public string? estadoAlmacenCompleto { get; set; }
        public string? estadoAlmacenCorto { get; set; }
    }
}
