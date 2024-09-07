using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECargo : EAuditoria
    {
        [DataMember]
        public int carg_icod_cargo { get; set; }
        [DataMember]
        public string carg_vdescripcion { get; set; }
        [DataMember]
        public string carg_vabreviado { get; set; }
        [DataMember]
        public bool carg_sflag_estado { get; set; }
    }
}
