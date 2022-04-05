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
        [Required]
        public string cNombre { set; get; }

        [Required]
        public string cDescripcion { set; get; }
        [Required]
        public string cCodigo { set; get; }
        [Required]
        public IFormFile fImagen { set; get; }
        public DateTime? dtFechaCreacion { set; get; }
    }
}
