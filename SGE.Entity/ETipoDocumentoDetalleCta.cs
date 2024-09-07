using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETipoDocumentoDetalleCta : EAuditoria
    {
        [DataMember]
        public int tdocd_iid_correlativo { get; set; }
        [DataMember]
        public int tdocd_iid_codigo_doc_det { get; set; }
        [DataMember]
        public string tdocd_descripcion { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_contable_nac { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_contable_extra { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_matris_nac { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_matris_extra { get; set; }
        [DataMember]
        public long? ctacc_icod_subcuenta_nac { get; set; }
        [DataMember]
        public long? ctacc_icod_subcuenta_extra { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_asociada_nac { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_asociada_extra { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_igv_nac { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_isc { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_gastos_nac { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_servicios { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_ivap { get; set; }
        [DataMember]
        public int? tdocd_iestado_registro { get; set; }
        [DataMember]
        public int? tdocd_estado_coa { get; set; }
        [DataMember]
        public bool tdocd_estado { get; set; }
        [DataMember]
        public string reg_compra { get; set; }
        [DataMember]
        public string reg_venta { get; set; }
    }
}
