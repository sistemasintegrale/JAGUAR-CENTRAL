using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EMotonaves
    {
        [DataMember]
        public int idd_icod_motonaves { get; set; }
        [DataMember]
        public int idd_motonaves { get; set; }
        [DataMember]
        public string vidd_motonaves { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int? usuario_crea { get; set; }
        [DataMember]
        public string usuario_pc_crea { get; set; }
        [DataMember]
        public int? usuario_modifica { get; set; }
        [DataMember]
        public string usuario_pc_modifica { get; set; }
        [DataMember]
        public DateTime? fecha_crea { get; set; }
        [DataMember]
        public DateTime? fecha_modifica { get; set; }
    }
}
