using System;

namespace SGE.Entity
{
    public class EProdNotaIngresoDetalleAlmacen : EAuditoria
    {
        public int numero_nota_ingreso { get; set; }
        public int item { get; set; }
        public int id_almacen { get; set; }
        public string vcodigo_externo { get; set; }
        public string descripcion_producto { get; set; }
        public string descripcion_unidad_medida { get; set; }
        public decimal cantidad { get; set; }

        public string flag { get; set; }
        public int Operacion { get; set; }
        public int icod_nota_ingreso_detalle { get; set; }
        public long? idkardex { get; set; }
        public int pr_icod_producto { get; set; }

        //para transferencia
        public int dninc_icod_detalle_ingreso_almacen { get; set; }
        public int dninc_iitem { get; set; }
        public int dninc_inumero_nota_ingreso { get; set; }
        public int dningc_icod_nota_ingreso_almacen { get; set; }
        public int dninc_iid_almacen { get; set; }
        public string dninc_vcodigo_externo { get; set; }
        public decimal dninc_ncantidad { get; set; }
        public int dninc_iactivo { get; set; }
        public long? dninc_kardex { get; set; }
        public int numero_icod_nota_ingreso { get; set; }
        public int id_motivo { get; set; }
        public string descripcion_motivo { get; set; }
        public DateTime fecha_nota_ingreso { get; set; }
        public int id_tipo_doc { get; set; }
        public string descripcion_tipo_doc { get; set; }
        public string numero_doc { get; set; }
        public string referencia { get; set; }
        public string vNumeroDocumento { get; set; }
        public string observaciones { get; set; }
    }
}
