using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class ETablaPlanillaDetalle : EAuditoria
    {
        [DataMember]
        public int tbpd_icod_tabla_planilla_detalle { set; get; }
        [DataMember]
        public int tbpc_icod_tabla_planilla { set; get; }
        [DataMember]
        public string tbpd_iid_vcodigo_tabla_planilla_detalle { set; get; }
        [DataMember]
        public string tbpd_vdescripcion_detalle { set; get; }
        [DataMember]
        public string tbpd_vabreviado_detalle { set; get; }
        [DataMember]
        public string tbpd_votros_datos { set; get; }
        [DataMember]
        public bool tbpd_flag_estado { set; get; }


    }
}
