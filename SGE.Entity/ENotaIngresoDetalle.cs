using System;

namespace SGE.Entity
{
    public class ENotaIngresoDetalle : EAuditoria
    {
        public int dninc_icod_detalle_ingreso { get; set; }
        public int ningc_icod_nota_ingreso { get; set; }
        public int dninc_nro_item { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal dninc_cantidad { get; set; }
        public decimal dninc_monto_unitario { get; set; }
        public decimal dninc_monto_total { get; set; }
        public int kardc_icod_correlativo { get; set; }
        /*-----------------------------------------------------*/
        public int intTipoOperacion { get; set; }
        /*-----------------------------------------------------*/
        public string cabNroNota { get; set; }
        public DateTime cabFechaNota { get; set; }
        public string cabMotivo { get; set; }
        public string cabDocumento { get; set; }
        public string cabReferencia { get; set; }
        public string cabObservacion { get; set; }

        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strSubCategoriaDos { get; set; }
        public string strEditorial { get; set; }
        public string strCodeProducto { get; set; }
        public string strProducto { get; set; }
        public string strUnidadMedida { get; set; }
        public decimal dnicc_ncantidad_recibida { get; set; }
    }
}
