using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EFormulario : EAuditoria
    {
        [DataMember]
        public int formc_icod_forms { get; set; }
        [DataMember]
        public int moduc_icod_modulo { get; set; }
        [DataMember]
        public string formc_vnombre_forms { get; set; }
        [DataMember]
        public string formc_vdescripcion { get; set; }
        /*----------------------------------------------------------------------*/
        [DataMember]
        public string strModulo { get; set; }
        [DataMember]
        public bool flag { get; set; }
        [DataMember]
        public int intUsuarioAcceso { get; set; }
    }
}
