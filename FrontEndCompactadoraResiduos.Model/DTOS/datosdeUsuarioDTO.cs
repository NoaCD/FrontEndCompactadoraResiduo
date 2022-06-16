using FrontEndCompactadoraResiduos.Model.DTOS;

namespace CreativeReduction.Model.DTOS
{
    public class datosdeUsuarioDTO
    {
        public int iId_TipoUsuario;
        public string cNombre;
       
        public string cApellidoPaterno;
        public string cApellidoMaterno;
        public int iId;

        public datosdeUsuarioDTO()
        {
        }

        ~datosdeUsuarioDTO()
        {
        }
       

        public int Tipo
        {
            get { return iId_TipoUsuario; }
            set { iId_TipoUsuario = value; }
        }

        public int id
        {
            get { return iId; }
            set { iId = value; }
        }

        public string Nombre
        {
            get { return cNombre; }
            set { cNombre = value; }
        }

        public string ApellidoPaterno
        {
            get { return cApellidoPaterno; }
            set { cApellidoPaterno = value; }
        }

        public string ApellidoMaterno
        {
            get { return cApellidoMaterno; }
            set { cApellidoMaterno = value; }
        }

        public List<TiposUsuarioDTO> TiposUsuarioDTOs { get; set; }
    }
}