using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class ResponseCargaDTO
    {
        public string estatus { get; set; }
        public string mensaje { get; set; }
        public int codigo { get; set; }
        public MostrarCargaDTO? carga { get; set; }
    }
}
