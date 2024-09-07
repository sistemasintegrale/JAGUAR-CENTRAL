namespace SGE.Entity
{
    public class EConceptosCostos : EAuditoria
    {
        public int chc_icod_detalle_hc { get; set; }
        public string chc_iitem_orden { get; set; }
        public int chc_icod_hoja_costo { get; set; }
        public string chc_vcodigo_concepto_hc { get; set; }
        public string chc_vdescripcion { get; set; }
        public decimal chc_nmonto_presup { get; set; }
        public decimal chc_nmonto_real { get; set; }
        public bool chc_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
