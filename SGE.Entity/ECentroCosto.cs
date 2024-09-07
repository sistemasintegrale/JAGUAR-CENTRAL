using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECentroCosto : EAuditoria
    {
        [DataMember]
        public int cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public string cecoc_vcodigo_centro_costo { get; set; }
        [DataMember]
        public string cecoc_vdescripcion { get; set; }
        [DataMember]
        public bool cecoc_situacion_centro_costo { get; set; }
        [DataMember]
        public bool cecoc_flag_estado { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        public int pryc_icod_proyecto { get; set; }
    }
}
