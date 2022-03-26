using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.ResiduosDTO
{
    public class ResiduoDTO
    {
        public int iId { set; get; }
        public string cNombre { set; get; }
        public string cDescripcion { set; get; }
        public string cCodigo { set; get; }
        public string cImagen { set; get; }
        public DateTime? dtFechaCreacion { set; get; }
        public DateTime? dtFechaModificacion { set; get; }
        public DateTime? dtFechaEliminacion { set; get; }
    }
}
