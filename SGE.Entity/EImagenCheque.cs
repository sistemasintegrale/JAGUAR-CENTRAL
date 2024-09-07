namespace SGE.Entity
{
    public class EImagenCheque : EAuditoria
    {
        public int ic_icod_imagen_cheque { get; set; }
        public int mobac_icod_correlativo { get; set; }
        public string ic_vruta { get; set; }
        //public Binary ic_vruta { get; set; }
        public byte ic_vruta_2 { get; set; }
    }
}
