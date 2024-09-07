using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAgenciaAduana
    {
        [DataMember]
        public int idd_icod_aduana { get; set; }
        [DataMember]
        public int idd_aduana { get; set; }
        [DataMember]
        public string vidd_aduana { get; set; }
        [DataMember]
        public string razon { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string ruc { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int usuario_crea { get; set; }
        [DataMember]
        public DateTime fecha_crea { get; set; }
        [DataMember]
        public string add_vpc_crea { get; set; }
        [DataMember]
        public int usuario_modifica { get; set; }
        [DataMember]
        public DateTime fecha_modifica { get; set; }
        [DataMember]
        public string add_vpc_modifica { get; set; }
    }
}
