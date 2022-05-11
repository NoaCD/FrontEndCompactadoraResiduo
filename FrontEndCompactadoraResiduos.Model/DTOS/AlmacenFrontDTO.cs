using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class AlmacenFrontDTO
    {
        public int? id { get; set; }
        public string? clave { get; set; }
        public string? nombre { get; set; }
        public bool? Default { get; set; }
        public string? ubicacion { get; set; }
        public string? fechaCreacion { get; set; }
        public string? fechaModificacion { get; set; }
    }
}
