using System;

namespace SGE.Entity
{
    public class EKardexValorizadoCompras : EAuditoria
    {
        public int anio { get; set; }
        public int icod_almacen_contable { get; set; }
        public int icod_producto_especifico { get; set; }
        public int icod_tipo_doc { get; set; }
        public string doc_numero { get; set; }
        public int tip_movimiento { get; set; }
        public int icod_motivo { get; set; }
        public int flag_estado { get; set; }
        public decimal costo_unitario { get; set; }

        public string alco_vdescripcion { get; set; }
        public string prodc_vdescripcion { get; set; }
        public string estac_vdescripcion { get; set; }
        public string situc_vdescripcion { get; set; }
        public string documento { get; set; }
        public string motivo { get; set; }
        public DateTime fecha { get; set; }
        public decimal cant_ingreso { get; set; }
        public decimal cant_salida { get; set; }
        public string unidad_medida { get; set; }
        public decimal monto_total { get; set; }
        public string referencia { get; set; }
        public string observacion { get; set; }
        public string prodc_iid_producto_generico { get; set; }
        public bool flag_compras_import { get; set; }
        public decimal fcoc_nmonto_tipo_cambio { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }

    }
}
