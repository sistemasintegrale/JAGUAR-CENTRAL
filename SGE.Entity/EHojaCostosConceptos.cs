namespace SGE.Entity
{
    public class EHojaCostosConceptos : EAuditoria
    {
        public int hjcd1_icod_detalle_hc { get; set; }
        public string hjcd1_iitem_orden { get; set; }
        public int hjcc_icod_hoja_costo { get; set; }
        public string hjcd1_vcodigo_concepto_hc { get; set; }
        public string hjcd1_vdescripcion { get; set; }
        public decimal hjcd1_nmonto_presup { get; set; }
        public decimal hjcd1_nmonto_real { get; set; }
        public bool hjcd1_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        public int Indicador { get; set; }
    }
}
