using System;

namespace SGE.Entity
{
    public class EProdStockProducto : EAuditoria
    {
       
        public string descripcion_almacen { get; set; }
        public int? puvec_icod_punto_venta { get; set; }
        public int id_producto_generico { get; set; }
        public string descripcion_producto { get; set; }
        public int id_estado { get; set; }
        public string descripcion_estado { get; set; }
        public int id_situacion { get; set; }
        public string descripcion_situacion { get; set; }
        public int id_unidad_medida { get; set; }
        public string descripcion_unidad_medida { get; set; }
        public decimal stock_prod { get; set; }
        public string vcodigo_externo { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechafin { get; set; }
        public decimal dblSaldoReal { get; set; }

        public int stocc_ianio { get; set; }
        public int stocc_iid_almacen { get; set; }
        public int stocc_iid_producto { get; set; }
        public decimal stocc_nstock_prod { get; set; }
        public int intTipoMovimiento { get; set; }
    }
}
