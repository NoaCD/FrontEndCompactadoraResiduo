using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class EditarCargaDTO
    {
        public int iId { get; set; }
   
        public int iId_Residuo { get; set; }
        
        public int iId_User { get; set; }
        
        public double dPesoBruto { get; set; }
 
        public double dPesoContenedor { get; set; }
        public string? cNombreMaquina { get; set; }
        public string? cComentario { get; set; }
        public string? eliminar { get; set; } = "off";

    }
}
