namespace SGE.Entity
{
    public class EHojaCostosRubros : EAuditoria
    {
        public int hjcd3_icod_rubros_hc { get; set; }
        public string hjcd3_iitem_orden { get; set; }
        public int hjcc_icod_hoja_costo { get; set; }
        public int hjcd2_icod_subconcepto_hc { get; set; }
        public string hjcd2_iitem { get; set; }
        public string hjcd3_vcodigo_concepto_hc { get; set; }
        public decimal hjcd3_ncantidad { get; set; }
        public int hjcd3_unidc_icod_unidad_medida { get; set; }
        public string hjcd3_vdescripcion { get; set; }
        public int tablc_icod_tipo_moneda { get; set; }
        public decimal hjcd3_nmonto_unitario { get; set; }
        public decimal hjcd3_nmonto_real { get; set; }
        public bool hjcd3_flag_estado { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal hjcd3_ncantidad_requerida { get; set; }
        public decimal hjcd3_ncantidad_autorizada { get; set; }
        public decimal hjcd3_ncantidad_atendida { get; set; }
        public decimal hjcd3_ncantidad_saldo { get; set; }
        public int intTipoOperacion { get; set; }
        /*---------------------------------------------------*/
        public string Unidad { get; set; }
        public string Moneda { get; set; }
        public string str_producto { get; set; }
        public string hjcd3_vcodigo_relacion { get; set; }
        public int tablc_icod_tipo_rubro { get; set; }
        public string TipoModena { get; set; }
        /*---------------Variables Importacion----------------------------*/
        public string ConceptoImport { get; set; }
        public string SubConceptoImport { get; set; }
    }
}
