using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETipoDocumentoDetalle : EAuditoria
    {
        [DataMember]
        public int tdocd_iid_correlativo { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int moduc_icod_modulo { get; set; }
        [DataMember]
        public bool tdocd_flag_estado { get; set; }

    }
}
