using System;

namespace SGE.Entity
{

    public class EProdNotaIngreso : EAuditoria
    {
        public int ningc_icod_nota_ingreso { get; set; }
        //[DataMember]
        public int? ningc_inumero_nota_ingreso { get; set; }
        //[DataMember]
        public string ningc_vnumero_nota_ingreso { get; set; }
        //[DataMember]
        public DateTime? ningc_sfecha_nota_ingreso { get; set; }
        //[DataMember]
        public int? tablc_iid_motivo { get; set; }
        //[DataMember]
        public string descripcionMotivo { get; set; }
        //[DataMember]
        public int? ningc_iid_almacen { get; set; }
        //[DataMember]
        public string ningc_viid_almacen { get; set; }
        //[DataMember]
        public string ningc_vdescripcion_almacen { get; set; }
        //[DataMember]
        public int? ningc_iid_tipo_doc { get; set; }
        public int? puvec_icod_punto_venta { get; set; }
        //[DataMember]
        public string ningc_vdescripcion_tipo_doc { get; set; }
        //[DataMember]
        public string ningc_inumero_doc { get; set; }
        // [DataMember]
        public string ningc_vreferencia { get; set; }
        //[DataMember]
        public string ningc_vobservaciones { get; set; }
        //[DataMember]
        public DateTime? ningc_sfecha_compra { get; set; }
        //[DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        //[DataMember]

        public int? ningc_iactivo { get; set; }
        //[DataMember]
        public int proc_icod_proveedor { get; set; }
        //[DataMember]
        public string proc_vnombrecompleto { get; set; }
        //[DataMember]
        public string proc_vcod_proveedor { get; set; }
        //[DataMember]
        public string Documento { get; set; }
        //[DataMember]
        public decimal cantidadNota { get; set; }
        public int? orpec_iid_orden_pedido { get; set; }
        public string numero_pedido { get; set; }
        public int? orped_icod_item_orden_pedido { get; set; }
        public int? numero_item_pedido { get; set; }
    }
}
