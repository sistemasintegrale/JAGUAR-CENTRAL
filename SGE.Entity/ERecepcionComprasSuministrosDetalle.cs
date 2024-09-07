namespace SGE.Entity
{
    public class ERecepcionComprasSuministrosDetalle : EAuditoria
    {
        public int rcsd_icod_detalle { get; set; }
        public int rcsd_iid_detalle { get; set; }
        public int rcsc_icod_rcs { get; set; }
        public int prdc_icod_producto { get; set; }
        public int kardc_icod_correlativo { get; set; }
        public int ocod_icod_detalle_oc { get; set; }
        public decimal rcsd_ncantidad { get; set; }
        public decimal rcsd_ncantidad_recibida { get; set; }
        public decimal rcsd_ncantidad2 { get; set; }
        public bool rcsd_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
        /*----------------------------------------------------------*/
        public string Unidad { get; set; }
        public string DesProducto { get; set; }
        public string strCodProd { get; set; }
        public decimal CantidadSaldo { get; set; }
        public decimal CantidadRecibidaOC { get; set; }
        public decimal CantidadRecibidaGR { get; set; }
    }
}
