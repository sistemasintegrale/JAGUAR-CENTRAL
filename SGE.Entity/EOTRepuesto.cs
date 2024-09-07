using System;

namespace SGE.Entity
{
    public class EOTRepuesto : EAuditoria
    {
        public int otrs_icod_ot_repuesto { get; set; }
        public int otrs_iid_orden_trabajo { get; set; }
        public int otrs_iitem_repuesto { get; set; }
        public int otrs_iid_moneda { get; set; }
        public int otrs_iid_producto { get; set; }
        public decimal otrs_ncantidad { get; set; }
        public decimal otrs_nprecio_unitario_item { get; set; }
        public decimal otrs_nprecio_total_item { get; set; }
        public int otrs_iid_almacen { get; set; }
        public Int64 otrs_iid_kardex { get; set; }
        public int? otrs_icod_personal { get; set; }
        public string otrs_area_personal { get; set; }
        public DateTime? otrs_fecha_servicio { get; set; }
        public decimal otrs_nmonto_productividad { get; set; }
        public decimal otrs_nmonto_descuento { get; set; }
        /**/
        public string strPersonal { get; set; }
        public string strDesProducto { get; set; }
        public string strCodProducto { get; set; }
        public string strAlmacen { get; set; }
        public string strMoneda { get; set; }
        public string strLinea { get; set; }
        public string strSubLinea { get; set; }
        public string strDesUM { get; set; }
        public string strTipoProducto { get; set; } // esto solo se usa en la impresión de la OT
        public int intTipoOperacion { get; set; }
        public int intClasificacion { get; set; }
        public decimal dblStockDisponible { get; set; }

    }
}
