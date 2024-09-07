using System;

namespace SGE.Entity
{

    public class EProdNotaSalida : EAuditoria
    {
        //[DataMember]
        public int salgc_icod_nota_salida { get; set; }
        //[DataMember]
        public int? salgc_inumero_nota_salida { get; set; }
        //[DataMember]
        public string salgc_vnumero_nota_salida { get; set; }
        //[DataMember]
        public DateTime? salgc_sfecha_nota_salida { get; set; }
        //[DataMember]
        public int? tablc_iid_motivo { get; set; }
        //[DataMember]
        public int? puvec_icod_punto_venta { get; set; }
        //[DataMember]
        public string descripcionMotivo { get; set; }
        //[DataMember]
        public int? salgc_iid_almacen { get; set; }
        // [DataMember]
        public string salgc_viid_almacen { get; set; }
        //[DataMember]
        public string salgc_vdescripcion_almacen { get; set; }
        //[DataMember]
        public int? salgc_iid_tipo_doc { get; set; }
        //[DataMember]
        public string salgc_vdescripcion_tipo_doc { get; set; }
        //[DataMember]
        public string salgc_inumero_doc { get; set; }
        //[DataMember]
        public string salgc_vreferencia { get; set; }
        //[DataMember]
        public string salgc_vobservaciones { get; set; }
        //[DataMember]
        public DateTime? salgc_sfecha_compra { get; set; }
        //[DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        //[DataMember]

        //[DataMember]
        public int? salgc_iactivo { get; set; }
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
    }
}
