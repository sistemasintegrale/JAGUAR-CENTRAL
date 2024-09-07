using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EFamiliaDet : EAuditoria
    {
        [DataMember]
        public int famid_icod_familia_tipo { get; set; }
        [DataMember]
        public int famic_icod_familia { get; set; }
        [DataMember]
        public int famid_iid_familia { get; set; }
        [DataMember]
        public string famid_vabreviatura { get; set; }
        [DataMember]
        public string famid_vdescripcion { get; set; }
        [DataMember]
        public bool famid_flag_estado { get; set; }
        /*---------------------------------------------------------*/
        [DataMember]
        public int intCodigoFamCab { get; set; }
        [DataMember]
        public string strAbreviaturaFamCab { get; set; }
        [DataMember]
        public string strDescripcionFamCab { get; set; }
        [DataMember]
        public int? cuenta_iservicio_tercero { get; set; }
        [DataMember]
        public int? cuenta_iservicio_propio { get; set; }
        [DataMember]
        public string NumeroCuentaSerTer { get; set; }
        [DataMember]
        public string DesCuentaSerTer { get; set; }
        [DataMember]
        public string NumeroCuentaSerPro { get; set; }
        [DataMember]
        public string DesCuentaSerPro { get; set; }
    }
}
