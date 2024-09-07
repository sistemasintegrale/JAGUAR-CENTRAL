using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EModulo : EAuditoria
    {
        [DataMember]
        public int moduc_icod_modulo { get; set; }
        [DataMember]
        public string moduc_vdescripcion { get; set; }
        [DataMember]
        public bool moduc_flag_estado { get; set; }
        /*----------------------------------------------------*/
        [DataMember]
        public int intCorrelativo { get; set; }
    }
}
