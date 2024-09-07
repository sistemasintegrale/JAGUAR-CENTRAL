using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EEmpresa : EAuditoria
    {
        [DataMember]
        public int cia_icod_empresa { set; get; }
        [DataMember]
        public string cia_vrazon_social { set; get; }
        [DataMember]
        public string cia_vruc { set; get; }
        [DataMember]
        public string cia_vdireccion_empr { set; get; }
        [DataMember]
        public string cia_vtelefonos { set; get; }
        [DataMember]
        public string cia_vregistro_patronal { set; get; }
        [DataMember]
        public string cia_vrepresentante_legal { set; get; }
        [DataMember]
        public string cia_vpagina_web { set; get; }

    }
}
