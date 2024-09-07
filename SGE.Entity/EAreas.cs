using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class EAreas : EAuditoria
    {

        [DataMember]
        public int arec_icod_cargo { get; set; }
        [DataMember]
        public string arec_vdescripcion { get; set; }
        [DataMember]
        public string arec_vabreviado { get; set; }
        [DataMember]
        public bool arec_sflag_estado { get; set; }
    }
}
