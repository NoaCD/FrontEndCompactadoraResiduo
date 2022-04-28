using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class TiposUsuarioDTO
    {
        public int iId;
        public string? cNombre;
        public string? cCodigo;


        public int id
        {
            get { return iId; }
            set { iId = value; }

        }
        public string nombre
        {
            get { return cNombre; }
            set { cNombre = value; }

        }
        public string codigo
        {
            get { return cCodigo; }
            set { cCodigo = value; }

        }
       

    }


}
