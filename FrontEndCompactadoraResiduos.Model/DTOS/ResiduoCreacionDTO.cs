using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class ResiduoCreacionDTO
    {
        public int iId { set; get; }
        public string cNombre { set; get; }
        public string cDescripcion { set; get; }
        public string cCodigo { set; get; }
        public IFormFile fImagen { set; get; }
        public DateTime? dtFechaCreacion { set; get; }
    }
}
