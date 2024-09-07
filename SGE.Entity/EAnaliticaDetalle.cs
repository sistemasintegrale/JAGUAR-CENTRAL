using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EAnaliticaDetalle : EAuditoria
    {
        [DataMember]
        public int anad_icod_analitica { get; set; }
        [DataMember]
        public string anad_iid_analitica { get; set; }
        [DataMember]
        public string anad_vdescripcion { get; set; }
        [DataMember]
        public int tarec_icorrelativo_tipo_analitica { get; set; }
        [DataMember]
        public string anad_nombre { get; set; }
        [DataMember]
        public string anad_apepaterno { get; set; }
        [DataMember]
        public string anad_apematerno { get; set; }
        [DataMember]
        public int? tarec_icorrelativo_tipo_persona { get; set; }
        [DataMember]
        public int? anad_origen { get; set; }
        [DataMember]
        public bool anad_flag_estado { get; set; }
        [DataMember]
        public bool anad_situacion { get; set; }
        /*-----------------------------------------------*/
        [DataMember]
        public string strTipoAnalitica { get; set; }
        [DataMember]
        public string strTipoPersona { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        [DataMember]
        public int? id_entidad { get; set; }
    }
}
