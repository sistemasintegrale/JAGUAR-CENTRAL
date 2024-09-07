using System;

namespace SGE.Entity
{
    public class EOTServicioSolicitado : EAuditoria
    {
        public int otss_icod_ot_ssolicitado { get; set; }
        public int otss_iid_orden_trabajo { get; set; }
        public int otss_iitem_ssolicitado { get; set; }
        public int otss_iid_moneda { get; set; }
        public int otss_iid_producto { get; set; }
        public decimal otss_ncantidad { get; set; }
        public decimal otss_nprecio_unitario_item { get; set; }
        public decimal otss_nprecio_total_itam { get; set; }
        public bool otss_flag_extension { get; set; }
        public int? otss_icod_personal { get; set; }
        public string otss_area_personal { get; set; }
        public DateTime? otss_fecha_servicio { get; set; }
        public decimal otss_nporc_productividad { get; set; }
        public decimal otss_nmonto_descuento { get; set; }
        /**/
        public string strDesProducto { get; set; }
        public string strCodProducto { get; set; }
        public string strMoneda { get; set; }
        public string strPersonal { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
