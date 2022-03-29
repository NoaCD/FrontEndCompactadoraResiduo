using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class EstatusDTO
    {
        public int iId { get; set; }
        public string cNombre { get; set; }
        public string cDescripcion { get; set; }
        public string cCodigo { get; set; }
        public DateTime dtFechaCreacion { get; set; }

    }
}
