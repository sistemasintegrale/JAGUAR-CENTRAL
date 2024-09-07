using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EEditorial : EAuditoria
    {
        [DataMember]
        public int edit_icod_editorial { get; set; }
        [DataMember]
        public int edit_iid_editorial { get; set; }
        [DataMember]
        public Boolean edit_iSituacion { get; set; }
        [DataMember]
        public string edit_vdescripcion { get; set; }
        [DataMember]
        public Boolean tarec_festado { get; set; }

        [DataMember]
        public int proc_icod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
    }
}
