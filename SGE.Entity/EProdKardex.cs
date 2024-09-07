using System;

namespace SGE.Entity
{
    public class EProdKardex : EAuditoria
    {
      
        public int kardc_iid_correlativo { get; set; }
        public DateTime kardx_sfecha { get; set; }
        public int iid_almacen { get; set; }
        public int iid_producto { get; set; }
        public int? puvec_icod_punto_venta { get; set; }
        public decimal Cantidad { get; set; }
        public int NroNota { get; set; }
        public string vNroNota { get; set; }
        public int iid_tipo_doc { get; set; }
        public string NroDocumento { get; set; }
        public int movimiento { get; set; }
        public int Motivo { get; set; }
        public string Beneficiario { get; set; }
        public string Observaciones { get; set; }
        public int? Item { get; set; }
        public int operacion { get; set; }
        public string stocc_codigo_producto { get; set; }
        public string strDocumento { get; set; }
        public string vmotivo { get; set; }
        public string strAlmacen { get; set; }
        public string strCodProducto { get; set; }
        public string strProducto { get; set; }
        public decimal cantidad_saldo_prod { get; set; }
        public decimal cantidad_ingreso { get; set; }
        public decimal cantidad_salida { get; set; }
        public int kardc_ianio { get; set; }
        public string strUnidadMedida { get; set; }
        public int orped_icod_item_orden_pedido { get; set; }
    }
}
