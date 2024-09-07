namespace SGE.Entity
{
    public class EConceptosCostosDetalle : EAuditoria
    {
        public int chcd_icod_subconcepto_hc { get; set; }
        public string chcd_iitem_orden { get; set; }
        public int hjcc_icod_hoja_costo { get; set; }
        public int chc_icod_concepto_hc { get; set; }
        public string chc_iiten { get; set; }
        public string chcd_vcodigo_concepto_hc { get; set; }
        public string chcd_vdescripcion { get; set; }
        public decimal chcd_nmonto_presup { get; set; }
        public decimal chcd_nmonto_real { get; set; }
        public bool chcd_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
