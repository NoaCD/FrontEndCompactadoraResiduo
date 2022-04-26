namespace FrontEndCompactadoraResiduos.Models
{
    public class HomeViewModel
    {
        public long? totalCargas { get; set; }
        public long? totalUsuarios { get; set; }
        public long? totalResiduos { get; set; }
        public long? totalReportes { get; set; }
        public string mensaje { get; set; } //mensaje de success o de error depende del API
        public string estatus { get; set; } 

    }
}
