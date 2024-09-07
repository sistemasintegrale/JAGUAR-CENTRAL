using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAgenteMaritimo
    {
        #region "Atributos"
        [DataMember]
        public Int32 agm_icod_maritimo { get; set; }
        [DataMember]
        public Int32 agm_iid_maritimo { get; set; }
        [DataMember]
        public String agm_vrazon { get; set; }
        [DataMember]
        public String agm_vDireccion { get; set; }
        [DataMember]
        public String agm_vtelefono { get; set; }
        [DataMember]
        public String agm_vemail { get; set; }
        [DataMember]
        public String agm_vruc { get; set; }
        [DataMember]
        public Int32 agm_iid_usuario_crea { get; set; }
        [DataMember]
        public DateTime agm_sfecha_crea { get; set; }
        [DataMember]
        public String agm_vpc_crea { get; set; }
        [DataMember]
        public Int32 agm_iid_usuario_modifica { get; set; }
        [DataMember]
        public DateTime agm_sfecha_modifica { get; set; }
        [DataMember]
        public String agm_vpc_modifica { get; set; }
        [DataMember]
        public Boolean agm_flag_estado { get; set; }
        [DataMember]
        public string agm_viid_maritimo { get; set; }
        #endregion
    }
}
