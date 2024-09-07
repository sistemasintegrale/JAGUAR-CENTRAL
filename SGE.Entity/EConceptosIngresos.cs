using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class EConceptosIngresos : EAuditoria
    {

        [DataMember]
        public int cipc_icod_concepto_ingreso_plan { set; get; }
        [DataMember]
        public int tbpc_icod_tipo_planilla { set; get; }
        [DataMember]
        public int tbpc_icod_situacion_concepto_plan { set; get; }
        [DataMember]
        public string cipc_iid_concepto_ing_plan { set; get; }
        [DataMember]
        public string cipc_vdescripcion { set; get; }
        [DataMember]
        public bool? cipc_bafecto_renta_quinta { set; get; }
        [DataMember]
        public int tbpc_icod_tipo_concepto_planilla { set; get; }
        [DataMember]
        public int tbpc_icod_tipo_accion_planilla { set; get; }
        [DataMember]
        public decimal cipc_nmonto_calculo_planilla { set; get; }
        [DataMember]
        public decimal cipc_nmonto_porcentaje_planilla { set; get; }
        [DataMember]
        public int tbpc_icod_tipo_calculo_planilla { set; get; }
        [DataMember]
        public int ctcc_icod_cuenta_contable_debe { set; get; }
        [DataMember]
        public int ctcc_icod_cuenta_contable_haber { set; get; }
        [DataMember]
        public bool? cipc_flag_estado { set; get; }
        [DataMember]
        public string ctacc_numero_cuenta_contable { set; get; }
        [DataMember]
        public string ctacc_numero_cuenta_contable_haber { set; get; }
        //--------------------------------------------------------

        public string StrTipoPlanilla { set; get; }
        public string StrSituacion { set; get; }
        public string StrTipo { set; get; }
        public string StrAccion { set; get; }
        public string StrTipoCalculo { set; get; }
    }
}
