using System;

namespace SGE.Entity
{
    public class EVentaDirectaDet : EAuditoria
    {
        public int dvdd_iid_icod_doc_venta_directa { get; set; }
        public int dvdc_icod_doc_venta_directa { get; set; }
        public int dvdd_iitem_doc_venta_directa { get; set; }
        public int dvdd_iid_producto { get; set; }
        public int dvdd_iid_almacen { get; set; }
        public decimal dvdd_ncantidad { get; set; }
        public string dvdd_vdescripcion { get; set; }
        public bool dvdd_bbonificacion { get; set; }
        public decimal dvdd_nprecio_unitario_item { get; set; }
        public decimal dvdd_nmonto_impuesto_item { get; set; }
        public decimal dvdd_nprecio_total_item { get; set; }

        public int? dvdd_icod_personal { get; set; }
        public string dvdd_area_personal { get; set; }
        public int dvdd_clasificacion { get; set; }// con este campo se identificará si es producto o servicio
        public decimal dvdd_nporc_productividad { get; set; }
        public decimal dvdd_nmonto_productividad { get; set; }
        public DateTime? dvdd_fecha_servicio { get; set; }


        /**/
        public string strCodProducto { get; set; }
        public string strCategoria { get; set; }
        public string strSubCategoriaUno { get; set; }
        public string strDesUM { get; set; }
        public string strDesProducto { get; set; }
        public string strMoneda { get; set; }
        public string strAlmacen { get; set; }
        public decimal? dblStockDisponible { get; set; }
        public int intTipoOperacion { get; set; }
        //public int intClasificacionProducto { get; set; }
        public string strPersonal { get; set; }
    }
}
