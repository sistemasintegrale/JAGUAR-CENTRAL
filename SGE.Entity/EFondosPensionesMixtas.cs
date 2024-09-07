using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class EFondosPensionesMixtas : EAuditoria
    {
        [DataMember]
        public int fdpd2_icod_fp_concepto_mixto { set; get; }
        [DataMember]
        public string fdpd2_iid_vcodigo_fp_concepto_mixto { set; get; }
        [DataMember]
        public string fdpd2_vdescripcion_concepto_mixto { set; get; }
        [DataMember]
        public decimal fdpd2_nporcentaje_concepto_mixto { set; get; }
        [DataMember]
        public decimal fdpd2_ntope_concepto_mixto { set; get; }
        [DataMember]
        public bool fdpd2_flag_estado { set; get; }
        [DataMember]
        public int? fdpc_icod_fondo_pension { set; get; }
    }
}
