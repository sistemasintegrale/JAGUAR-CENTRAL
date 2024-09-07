namespace SGE.Entity
{
    public class EOrdenProduccionDet : EAuditoria
    {
        public int orprd_icod_item_orden_produccion { get; set; }
        public int orprc_iid_orden_produccion { get; set; }
        public int orprd_iitem_orden_produccion { get; set; }
        public int orprd_iproceso { get; set; }
        public int orprd_iubicacion { get; set; }
        public int prdc_icod_producto { get; set; }
        public string strCodeProducto { get; set; }
        public string strProducto { get; set; }
        public string orprd_vubicacion { get; set; }
        public string orprd_vmaterial { get; set; }
        public decimal orprd_ncantidad { get; set; }
        public int orprd_imedida { get; set; }
        public int intTipoOperacion { get; set; }
        public string strproceso { get; set; }
        public string strmedida { get; set; }
    }
}
