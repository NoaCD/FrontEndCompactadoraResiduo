using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class UsuarioCreacionDTO
    {
        public int? iId { get; set; }
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
        [Required]
        public string cNombreUsuario { get; set; }//NombreUsuario se guardan en la tbl_Login
        [Required]
        public string cContrasenia { get; set; } //Contrasenia se guarda en la tbl_Login
        [Required]
        public DateTime dtFechaCreacion { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public DateTime? dtFechaEliminacion { get; set; }
    }
}
