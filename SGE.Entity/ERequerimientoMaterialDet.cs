namespace SGE.Entity
{
    public class ERequerimientoMaterialDet : EAuditoria
    {
        public int rqmad_icod_item_requerimiento_material { get; set; }
        public int rqmac_iid_requerimiento_material { get; set; }
        public int rqmad_iitem_requerimiento_material { get; set; }
        public int rqmad_iproceso { get; set; }
        public int rqmad_iubicacion { get; set; }
        public int prdc_icod_producto { get; set; }
        public string strCodeProducto { get; set; }
        public string strProducto { get; set; }
        public string rqmad_vmaterial { get; set; }
        public decimal rqmad_ncantidad_requerida { get; set; }
        public decimal rqmad_ncantidad_entregada { get; set; }
        public decimal rqmad_ncantidad_devuelta { get; set; }
        public int orprd_imedida { get; set; }
        public bool rqmad_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        public string strproceso { get; set; }
        public string strubicacion { get; set; }
        public string strmedida { get; set; }
        public int rqmad_kardc_icod_correlativo { get; set; }
        public decimal rqmad_cantidad_saldo { get; set; }
        public int icod_entrega_material { get; set; }
        public string NumeroEntrega { get; set; }
    }
}
