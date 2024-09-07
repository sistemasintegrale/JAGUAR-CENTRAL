using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class EFondosPensionesConceptos : EAuditoria
    {
        [DataMember]
        public int? fdpd_icod_fondo_pension_concepto { set; get; }
        [DataMember]
        public string fdpd_iid_vcodigo_fondo_concepto { set; get; }
        [DataMember]
        public string fdpd_vdescripcion_concepto { set; get; }
        [DataMember]
        public decimal? fdpd_nporcentaje_concepto { set; get; }
        [DataMember]
        public decimal? fdpd_ntope_concpeto { set; get; }
        [DataMember]
        public bool fdpd_flag_estado { set; get; }
        [DataMember]
        public int? fdpc_icod_fondo_pension { set; get; }


    }
}
