using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class UsuarioEdicionDTO
    {

        public int iId { get; set; }
        [Required]
        public int iId_Estatus { get; set; }
        [Required]
        public int iId_TipoUsuario { get; set; }
        public int? iId_Login { get; set; }
        [Required]
        public int iNumeroEmpleado { get; set; }
        [Required]
        public string cNombre { get; set; }
        [Required]
        public string cApellidoPaterno { get; set; }
        [Required]
        public string cApellidoMaterno { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public DateTime? dtFechaEliminacion { get; set; }

    }
}
