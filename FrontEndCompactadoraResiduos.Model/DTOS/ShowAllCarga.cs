using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompactadoraDeResiduos.Model.DTO
{
    public class ShowAllCarga
    {

        public int? idCarga { get; set; }
        public DateTime? fechaCreacionCarga { get; set; }
        public DateTime? fechaEliminacionCarga { get; set; }
        public int? idResiduo { get; set; }
        public string? nombreResiduo { get; set; }
        public string? descripcionResido { get; set; }
        public string codigoResiduo { get; set; }
        public int? idEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int numeroEmpleado { get; set; }


    }
}
