using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.ResiduosDTO
{
    public class ResiduoDTO
    {
        public int iId;
        public string cNombre;
        public string cDescripcion;
        public string cCodigo;
        public string cImagen;
        public DateTime? dtFechaCreacion;
        public DateTime? dtFechaModificacion;
        public DateTime? dtFechaEliminacion;

       

        public int IdResiduo
        {
            get
            {
                return iId;
            }
            set
            {
                iId = value;
            }
        }

        public string cNombreResiduo
        {
            get
            {
                return cNombre;
            }

            set
            {
                cNombre = value;
            }

        }

        public string cDescripcionResiduo
        {
            get
            {
                return cDescripcion;
            }
            set
            {
                cDescripcion = value;
            }
        }
        public string cCodigoResiduo
        {
            get
            {
                return cCodigo;
            }
            set
            {
                cCodigo = value;
            }
        }
        public string cImagenResiduo
        {
            get
            {
                return cImagen;
            }
            set
            {
                cImagen = value;
            }
        }

        public DateTime? dtFechaCreacionResiduo
        {
            get
            {
                return dtFechaCreacion;
            }
            set
            {
                dtFechaCreacion = value;
            }
        }
        public DateTime? dtFechaModificacionResiduo
        {
            get
            {
                return dtFechaModificacion;
            }
            set
            {
                dtFechaModificacion = value;
            }
        }

        public DateTime? dtFechaEliminacionResiduo
        {
            get
            {
                return dtFechaEliminacion;
            }
            set
            {
                dtFechaEliminacion = value;
            }
        }

    }
}
