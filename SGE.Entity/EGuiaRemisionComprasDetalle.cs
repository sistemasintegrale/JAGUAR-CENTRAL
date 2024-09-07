namespace SGE.Entity
{
    public class EGuiaRemisionComprasDetalle : EAuditoria
    {
        public int grcd_icod_detalle { get; set; }
        public int grcd_iid_detalle { get; set; }
        public int grcc_icod_grcc { get; set; }
        public int prdc_icod_producto { get; set; }
        public int kardc_icod_correlativo { get; set; }
        public int ocod_icod_detalle_oc { get; set; }
        public decimal grcd_ncantidad { get; set; }
        public decimal grcd_ncantidad_recibida { get; set; }
        public decimal grcd_ncantidad2 { get; set; }
        public bool grcd_flag_estado { get; set; }
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
