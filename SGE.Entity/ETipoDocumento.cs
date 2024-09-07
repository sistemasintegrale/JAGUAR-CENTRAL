using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETipoDocumento : EAuditoria
    {
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int tdocc_iid_tipo_doc { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string tdocc_vdescripcion { get; set; }
        [DataMember]
        public string tdocc_coa { get; set; }
        [DataMember]
        public int? tdocc_nro_correlativo { get; set; }
        [DataMember]
        public bool tdocc_flag_estado { get; set; }
        public string tdocc_nro_serie { get; set; }

        public int? tdocc_nro_correlativo_2 { get; set; }
        public string tdocc_nro_serie_2 { get; set; }

        public string strNroCorrelativo { get; set; }
        public string strNroCorrelativo_2 { get; set; }
    }
}
