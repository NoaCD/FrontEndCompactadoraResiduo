using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class InformacionEmbarque
    {
        public int? idEstatusAlmacen { get; set; }
        public int? idCarga { get; set; }
        public int? idResiduo { get; set; }
        public double pesoBrutoCarga { get; set; }
        public double? pesoContenedorCarga { get; set; }
        public string? folioCarga { get; set; }
        public string? nombreResiduo { get; set; }
        public string? nombreProveedorBasura { get; set; }
    }
}
