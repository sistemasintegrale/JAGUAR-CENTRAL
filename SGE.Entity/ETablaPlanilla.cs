using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETablaPlanilla : EAuditoria
    {
        [DataMember]
        public int tbpc_icod_tabla_planilla { set; get; }
        [DataMember]
        public string tbpc_iid_vcodigo_tabla_planilla { set; get; }
        [DataMember]
        public string tbpc_vdescripcion { set; get; }
        [DataMember]
        public bool tbpc_flag_estado { set; get; }


    }
}
