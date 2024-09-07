namespace SGE.Entity
{
    public class EProformaDet : EAuditoria
    {
        public int prfd_icod_item_proforma { get; set; }
        public int prfc_icod_proforma { get; set; }
        public int prfd_iitem_proforma { get; set; }
        public int prfd_iid_producto { get; set; }
        public decimal prfd_ncantidad { get; set; }
        public string prfd_vdescripcion { get; set; }
        public decimal prfd_nprecio_unitario_item { get; set; }
        public decimal prfd_nprecio_total_item { get; set; }
        /**/
        public string strCodProducto { get; set; }
        public string strDesUM { get; set; }
        public string strMoneda { get; set; }
        public string strLinea { get; set; }
        public string strSubLinea { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
