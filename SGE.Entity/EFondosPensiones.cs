using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class EFondosPensiones : EAuditoria
    {
        [DataMember]
        public int fdpc_icod_fondo_pension { get; set; }
        [DataMember]
        public string fdpc_iid_vcodigo_fondo { get; set; }
        [DataMember]
        public string fdpc_vdescripcion { get; set; }
        [DataMember]
        public decimal? fdpc_nporcentaje_fijo { get; set; }
        [DataMember]
        public decimal? fdpc_nporcentaje_mixto { get; set; }
        [DataMember]
        public bool? tablc_iid_tipo_fondo_pensiones { get; set; }
        [DataMember]
        public bool? fdpc_situacion { get; set; }
        [DataMember]
        public bool fdpc_flag_estado { get; set; }




    }
}
