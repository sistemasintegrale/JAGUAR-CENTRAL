namespace SGE.Entity
{
    public class EGuiaRemisionComprasMercaderiaDetalle : EAuditoria
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
        /*---------------------------------------------------------*/
        public string grcd_rango_talla { get; set; }
        public int? grcd_talla1 { get; set; }
        public int? grcd_talla2 { get; set; }
        public int? grcd_talla3 { get; set; }
        public int? grcd_talla4 { get; set; }
        public int? grcd_talla5 { get; set; }
        public int? grcd_talla6 { get; set; }
        public int? grcd_talla7 { get; set; }
        public int? grcd_talla8 { get; set; }
        public int? grcd_talla9 { get; set; }
        public int? grcd_talla10 { get; set; }
        public int? grcd_cant_talla1 { get; set; }
        public int? grcd_cant_talla2 { get; set; }
        public int? grcd_cant_talla3 { get; set; }
        public int? grcd_cant_talla4 { get; set; }
        public int? grcd_cant_talla5 { get; set; }
        public int? grcd_cant_talla6 { get; set; }
        public int? grcd_cant_talla7 { get; set; }
        public int? grcd_cant_talla8 { get; set; }
        public int? grcd_cant_talla9 { get; set; }
        public int? grcd_cant_talla10 { get; set; }
        public long? grcd_iid_kardex1 { get; set; }
        public long? grcd_iid_kardex2 { get; set; }
        public long? grcd_iid_kardex3 { get; set; }
        public long? grcd_iid_kardex4 { get; set; }
        public long? grcd_iid_kardex5 { get; set; }
        public long? grcd_iid_kardex6 { get; set; }
        public long? grcd_iid_kardex7 { get; set; }
        public long? grcd_iid_kardex8 { get; set; }
        public long? grcd_iid_kardex9 { get; set; }
        public long? grcd_iid_kardex10 { get; set; }
        /*Datos Referenciales Orden Compra Mercaderia*/
        public int? CantOC1 { get; set; }
        public int? CantOC2 { get; set; }
        public int? CantOC3 { get; set; }
        public int? CantOC4 { get; set; }
        public int? CantOC5 { get; set; }
        public int? CantOC6 { get; set; }
        public int? CantOC7 { get; set; }
        public int? CantOC8 { get; set; }
        public int? CantOC9 { get; set; }
        public int? CantOC10 { get; set; }
        public int? SaldoOC1 { get; set; }
        public int? SaldoOC2 { get; set; }
        public int? SaldoOC3 { get; set; }
        public int? SaldoOC4 { get; set; }
        public int? SaldoOC5 { get; set; }
        public int? SaldoOC6 { get; set; }
        public int? SaldoOC7 { get; set; }
        public int? SaldoOC8 { get; set; }
        public int? SaldoOC9 { get; set; }
        public int? SaldoOC10 { get; set; }
    }
}
