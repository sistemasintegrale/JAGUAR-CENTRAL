namespace SGE.Entity
{
    public class EhojaCostosSubConceptos : EAuditoria
    {
        public int hjcd2_icod_subconcepto_hc { get; set; }
        public string hjcd2_iitem_orden { get; set; }
        public int hjcc_icod_hoja_costo { get; set; }
        public int hjcd1_icod_concepto_hc { get; set; }
        public string hjcc1_iiten { get; set; }
        public string hjcd2_vcodigo_concepto_hc { get; set; }
        public string hjcd2_vdescripcion { get; set; }
        public decimal hjcd2_nmonto_presup { get; set; }
        public decimal hjcd2_nmonto_real { get; set; }
        public bool hjcd2_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
