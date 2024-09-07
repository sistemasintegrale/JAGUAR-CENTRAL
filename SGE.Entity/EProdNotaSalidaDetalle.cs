using System;

namespace SGE.Entity
{
    public class EProdNotaSalidaDetalle : EAuditoria
    {
        public string Documento;

        //[DataMember]
        public int salgcd_icod_nota_salida { get; set; }
        //[DataMember]
        public int salgc_icod_nota_salida { get; set; }
        //[DataMember]
        public int? salgc_iid_almacen { get; set; }
        //[DataMember]
        public int? salgcd_num_item { get; set; }
        //[DataMember]
        public int? tablc_iid_motivo { get; set; }
        //[DataMember]
        public int? pr_icod_producto { get; set; }
        //[DataMember]
        public string pr_vcodigo_externo { get; set; }
        //[DataMember]
        public string pr_vdescripcion_producto { get; set; }
        //[DataMember]
        public decimal? pr_ncant_total_producto { get; set; }
        //[DataMember]
        public int salgcd_icod_serie { get; set; }
        public string salgcd_rango_talla { get; set; }
        public string resec_vdescripcion { get; set; }
        //[DataMember]
        public int? salgcd_iid_moneda { get; set; }
        //[DataMember]
        public decimal? salgcd_monto_unit { get; set; }
        //[DataMember]
        public decimal? salgcd_monto_total { get; set; }
        //[DataMember]
        public int? salgcd_talla1 { get; set; }

        //[DataMember]
        public int? salgcd_talla2 { get; set; }
        //[DataMember]
        public int? salgcd_talla3 { get; set; }
        //[DataMember]
        public int? salgcd_talla4 { get; set; }
        //[DataMember]
        public int? salgcd_talla5 { get; set; }
        //[DataMember]
        public int? salgcd_talla6 { get; set; }
        //[DataMember]
        public int? salgcd_talla7 { get; set; }
        //[DataMember]
        public int? salgcd_talla8 { get; set; }
        //[DataMember]
        public int? salgcd_talla9 { get; set; }
        //[DataMember]
        public int? salgcd_talla10 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla1 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla2 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla3 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla4 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla5 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla6 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla7 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla8 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla9 { get; set; }
        //[DataMember]
        public int? salgcd_cant_talla10 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex1 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex2 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex3 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex4 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex5 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex6 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex7 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex8 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex9 { get; set; }
        //[DataMember]
        public long? salgcd_iid_kardex10 { get; set; }
        //[DataMember]
        public int salgcd_icod_producto1 { get; set; }
        public int salgcd_icod_producto2 { get; set; }
        public int salgcd_icod_producto3 { get; set; }
        public int salgcd_icod_producto4 { get; set; }
        public int salgcd_icod_producto5 { get; set; }
        public int salgcd_icod_producto6 { get; set; }
        public int salgcd_icod_producto7 { get; set; }
        public int salgcd_icod_producto8 { get; set; }
        public int salgcd_icod_producto9 { get; set; }
        public int salgcd_icod_producto10 { get; set; }

        //[DataMember]
        public int? salgcd_iactivo { get; set; }
        //[DataMember]
        public string TallaAcumulada { get; set; }

        public int operacion { get; set; }
        public int? salgc_inumero_nota_salida { get; set; }
        public string salgc_vnumero_nota_salida { get; set; }
        public DateTime? salgc_sfecha_nota_salida { get; set; }
        public string descripcionMotivo { get; set; }
        public string salgc_vdescripcion_tipo_doc { get; set; }
        public string salgc_inumero_doc { get; set; }
        public string salgc_vreferencia { get; set; }
        public string salgc_vobservaciones { get; set; }
    }
}
