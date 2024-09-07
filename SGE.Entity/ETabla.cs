using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ETabla
    {
        [DataMember]
        public int tablc_iid_tipo_tabla { get; set; }
        [DataMember]
        public string tablc_vdescripcion { get; set; }
        [DataMember]
        public char tablc_cestado { get; set; }
        [DataMember]
        public string strEstado { get; set; }
    }
}
