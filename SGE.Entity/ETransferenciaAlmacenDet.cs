namespace SGE.Entity
{
    public class ETransferenciaAlmacenDet : EAuditoria
    {
        public int trfd_icod_detalle_transf { get; set; }
        public int trfc_icod_transf { get; set; }
        public int trfd_nro_item { get; set; }
        public int prdc_icod_producto { get; set; }
        public int? kardc_icod_correlativo_sal { get; set; }
        public int? kardc_icod_correlativo_ing { get; set; }
        public decimal trfd_ncantidad { get; set; }

        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public string strCodUnidadMedida { get; set; }
        public int intTipoOperacion { get; set; }
        public decimal dblStockDisponible { get; set; }
    }
}
