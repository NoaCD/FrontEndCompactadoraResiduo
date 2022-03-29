using FrontEndCompactadoraResiduos.Model.DTOS;

namespace FrontEndCompactadoraResiduos.Models
{
    public class UsuarioDTO
    {
        public int iId;
        public int iNumeroEmpleado;
        public EstatusDTO catEstatusDTO;
        public TiposUsuarioDTO catTipoUsuarioDTO;
        public string cNombre;
        public string cApellidoPaterno;
        public string cApellidoMaterno;
        public DateTime dtFechaCreacion;
        public DateTime? dtFechaModificacion { set; get; }


        public int id
        {
            get { return iId; }
            set { iId = value; }
        }
        public int numeroEmpleado
        {
            get { return iNumeroEmpleado; }
            set { iNumeroEmpleado = value; }
        }
        public string nombre
        {
            get { return cNombre; }
            set { cNombre = value; }
        }
        public string apellidoPaterno
        {
            get { return cApellidoPaterno; }
            set { cApellidoPaterno = value; }
        }
        public string apellidoMaterno
        {
            get { return cApellidoMaterno; }
            set { cApellidoMaterno = value; }
        }


        public DateTime fechaCreacion
        {
            get { return dtFechaCreacion; }
            set { dtFechaCreacion = value; }
        }
     
        public EstatusDTO estatusDTO
        {
            get { return catEstatusDTO; }
            set { catEstatusDTO = value; }
        }
        public TiposUsuarioDTO tipoUsuarioDTO
        {
            get { return catTipoUsuarioDTO; }
            set { catTipoUsuarioDTO = value; } 
        }


    }
}
