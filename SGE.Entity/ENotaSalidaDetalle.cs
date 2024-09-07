using System;

namespace SGE.Entity
{
    public class ENotaSalidaDetalle : EAuditoria
    {
        public int dnsalc_icod_detalle_salida { get; set; }
        public int nsalc_icod_nota_salida { get; set; }
        public int dnsalc_nro_item { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal dnsalc_cantidad { get; set; }
        public decimal dnsalc_monto_unitario { get; set; }
        public decimal dnsalc_monto_total { get; set; }
        public int kardc_icod_correlativo { get; set; }
        /*-----------------------------------------------------*/
        public string strCodeProducto { get; set; }
        public string strProducto { get; set; }
        public string strUnidadMedida { get; set; }
        public int intTipoOperacion { get; set; }
        /*-----------------------------------------------------*/
        public string cabNroNota { get; set; }
        public DateTime cabFechaNota { get; set; }
        public string cabMotivo { get; set; }
        public string cabDocumento { get; set; }
        public string cabReferencia { get; set; }
        public string cabObservacion { get; set; }
        public decimal dblStockDisponible { get; set; }

        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strSubCategoriaDos { get; set; }
        public string strEditorial { get; set; }
    }
}
