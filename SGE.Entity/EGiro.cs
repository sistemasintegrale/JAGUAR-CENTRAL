using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EGiro
    {
        [DataMember]
        public int giroc_icod_giro { get; set; }
        [DataMember]
        public char? giroc_iid_giro { get; set; }
        [DataMember]
        public string giroc_vnombre_giro { get; set; }
        [DataMember]
        public int? tablc_iid_situacion_giro { get; set; }
        [DataMember]
        public string DescripSituacion { get; set; }
        [DataMember]
        public bool giroc_bindicador_expo_nextel { get; set; }
        [DataMember]
        public string indicador_expo_nextel { get; set; }
    }
}
