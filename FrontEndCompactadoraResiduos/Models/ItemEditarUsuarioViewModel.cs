using FrontEndCompactadoraResiduos.Model.DTOS;

namespace FrontEndCompactadoraResiduos.Models
{
    public class ItemEditarUsuarioViewModel
    {
        public UsuarioDTO itemUsuario { get; set; }
        public List<TiposUsuarioDTO> tiposUsuario { get; set; }
    }
}
