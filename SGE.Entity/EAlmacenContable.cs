using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAlmacenContable : EAuditoria
    {
        [DataMember]
        public int almcc_icod_almacen { get; set; }
        [DataMember]
        public int almcc_iid_almacen { get; set; }
        [DataMember]
        public string almcc_vabreviatura { get; set; }
        [DataMember]
        public string almcc_vdescripcion { get; set; }
    }
}
