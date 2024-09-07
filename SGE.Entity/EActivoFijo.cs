using System;
using System.Runtime.Serialization;
namespace SGE.Entity
{
    [DataContract]
    public class EActivoFijo : EAuditoria
    {
        [DataMember]
        public int acfc_icod_activo_fijo { set; get; }
        [DataMember]
        public string acfc_iid_activo_fijo { set; get; }
        [DataMember]
        public int? tarec_iid_clasificacion_af { set; get; }
        [DataMember]
        public int? tarec_iid_situacion_af { set; get; }
        [DataMember]
        public string acfc_vdescripcion { set; get; }
        [DataMember]
        public string acfc_vmarca { set; get; }
        [DataMember]
        public string acfc_vmodelo { set; get; }
        [DataMember]
        public string acfc_vserie { set; get; }
        [DataMember]
        public string acfc_vcaracterist { set; get; }
        [DataMember]
        public int? acfc_icantidad { set; get; }
        [DataMember]
        public int? tarec_iid_tip_moneda { set; get; }
        [DataMember]
        public decimal? acfc_ncosto_act { set; get; }
        [DataMember]
        public decimal? acfc_ntotal_deprec { set; get; }
        [DataMember]
        public string acfc_vcodigo_invent { set; get; }
        [DataMember]
        public int? lafc_icod_localidad { set; get; }
        [DataMember]
        public DateTime? acfc_sfech_adqui { set; get; }
        [DataMember]
        public decimal? acfc_ncosto_adqui { set; get; }
        [DataMember]
        public int? acfc_ianio_vida { set; get; }
        [DataMember]
        public decimal? acfc_nporct_deprec { set; get; }
        [DataMember]
        public DateTime? acfc_sfech_alta { set; get; }
        [DataMember]
        public DateTime? acfc_sfecha_inic_uso { set; get; }
        [DataMember]
        public int? ccoc_icod_centro_costo { set; get; }
        [DataMember]
        public int? ctacc_icod_cuenta_contable { set; get; }
        [DataMember]
        public int? proc_icod_proveedor { set; get; }
        [DataMember]
        public DateTime? acfc_sfecha_baja { set; get; }
        [DataMember]
        public string acfc_vmotivo { set; get; }
        [DataMember]
        public string acfc_vfoto { set; get; }
        [DataMember]
        public string ccoc_numero_centro_costo { set; get; }
        [DataMember]
        public string ctacc_numero_cuenta_contable { set; get; }
        [DataMember]
        public bool? acfc_flag_estado { set; get; }
        [DataMember]
        public string lafc_vdescripcion { set; get; }
        [DataMember]
        public string proc_vnombrecompleto { set; get; }
        //-----------------------------------------------
        [DataMember]
        public string NClasificacion { set; get; }
        [DataMember]
        public string NSituacion { set; get; }
        [DataMember]
        public string NMoneda { set; get; }

    }
}
