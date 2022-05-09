using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class ProveedorDB
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Descripcion { get; set; }
        public string? Rfc { get; set; }
    }
}
