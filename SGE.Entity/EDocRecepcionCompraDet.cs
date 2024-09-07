namespace SGE.Entity
{
    public class EDocRecepcionCompraDet : EAuditoria
    {
        public int drcd_icod_doc_recepcion_compra { get; set; }
        public int drcc_icod_doc_recepcion_compra { get; set; }
        public int drcd_iitem { get; set; }
        public int prd_icod_producto { get; set; }
        public decimal drcd_ncantidad { get; set; }
        public decimal drcd_nmonto_unit { get; set; }
        public decimal drcd_nmonto_total { get; set; }
        public int dcrd_icod_kardex { get; set; }
        public string drcd_vdescripcion_item { get; set; }
        public string strCodProducto { get; set; }
        public string strDesUM { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
