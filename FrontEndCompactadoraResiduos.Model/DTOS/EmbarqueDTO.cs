namespace FrontEndCompactadoraResiduos.Model.DTOS
{
    public class EmbarqueDTO
    {

        public int? id { get; set; }
        public string? folioEmbarque { get; set; }
        public string? area { get; set; }
        public DateTime? fechaEmbarque { get; set; }
        public List<InformacionEmbarque> informacionEmbarque { get; set; }


    }
}
