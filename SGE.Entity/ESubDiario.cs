using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ESubDiario : EAuditoria
    {
        [DataMember]
        public int subdi_icod_subdiario { get; set; }
        [DataMember]
        public string subdi_vdescripcion { get; set; }
        [DataMember]
        public bool subdi_iactivo { get; set; }
        [DataMember]
        public bool subdi_flag_estado { get; set; }
        /*-------------------------------------------*/
        [DataMember]
        public string strEstado { get; set; }
    }
}
