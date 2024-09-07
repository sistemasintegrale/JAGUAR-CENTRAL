using System;
using System.Collections;
using System.ComponentModel;

namespace SGE.Entity
{
    public class EPersonal : EAuditoria
    {
        public int perc_icod_personal { get; set; }
        public string perc_iid_personal { get; set; }
        public DateTime? perc_sfecha_registro { get; set; }

        public string perc_vapellidos_nombres { get; set; }
        public string perc_vdni { get; set; }

        public int? tbpd_icod_tip_doc { get; set; }
        public string perc_vnum_doc { get; set; }
        public int? tbpd_icod_pais_emi_doc { get; set; }
        public DateTime? perc_sfecha_nacimiento { get; set; }
        public string perc_vapellido_pat { get; set; }
        public string perc_vapellido_mat { get; set; }
        public string perc_vnombres { get; set; }
        public int? perc_icod_sexo { get; set; }
        public int? tbpd_icod_nacionalidad { get; set; }
        public int? tbpd_icod_telf_ldn { get; set; }
        public string perc_vnum_telf_ldn { get; set; }
        public string perc_vcorreo { get; set; }
        public int? tbpd_icod_tip_via { get; set; }
        public string perc_vnomb_via { get; set; }
        public string perc_vnum_via { get; set; }
        public string perc_vdepartamento { get; set; }
        public string perc_vinterior { get; set; }
        public string perc_vmanzana { get; set; }
        public string perc_vlote { get; set; }
        public string perc_vkilometro { get; set; }
        public string perc_vblock { get; set; }
        public string perc_vetapa { get; set; }
        public int? tbpd_icod_tip_zona { get; set; }
        public string perc_vnomb_zona { get; set; }
        public string perc_vreferencia { get; set; }
        public int? tbpd_icod_ubi_geo { get; set; }
        public string tbpd_vnom_ubi_geo { get; set; }
        public int? tarec_icod_est_civil { get; set; }


        public int? tablc_iid_tipo_cargo { get; set; }
        public int? tablc_iid_tipo_area { get; set; }

        public string perc_vruc { get; set; }
        public string perc_vnum_seguro { get; set; }
        public int? perc_icod_tip_fdo_pension { get; set; }
        public DateTime? perc_sfech_inicio { get; set; }
        public int? perc_icod_afp { get; set; }
        public int? perc_icod_tip_comision { get; set; }
        public string perc_vcuspp { get; set; }
        public bool? perc_beps { get; set; }
        public int? perc_icod_tip_personal { get; set; }
        public decimal? perc_nmont_basico { get; set; }
        public int? tarec_icod_moneda { get; set; }
        public bool? perc_brta_5ta { get; set; }
        public decimal? perc_nmont_ant_afecto { get; set; }
        public decimal? perc_nmont_retenido { get; set; }
        public bool? perc_basig_familiar { get; set; }
        public DateTime? perc_sfech_cese { get; set; }

        public int perc_iid_situacion_perso { get; set; }
        public bool perc_flag_comprador { get; set; }
        /*----------------------------------------------------*/
        public string strCargo { get; set; }
        public string strArea { get; set; }
        public string strEstado { get; set; }
        public string strComprador { get; set; }
        public string ApellNomb { get; set; }
        public string strEPS { get; set; }

        public string perc_hora_inical_LV { get; set; }
        public string perc_hora_final_LV { get; set; }
        public string perc_hora_inical_S { get; set; }
        public string perc_hora_final_S { get; set; }
        public string perc_hora_total_LV { get; set; }
        public string perc_hora_total_S { get; set; }

        public int? perc_icod_tip_contrato { get; set; }
        public int? perc_icod_banc_haber { get; set; }
        public string perc_vbanc_haber { get; set; }
        public int? perc_icod_banc_cts { get; set; }
        public string perc_vbanc_cts { get; set; }
        public DateTime? perc_sfecha_cese { get; set; }
        public int? perc_icod_motiv_cese { get; set; }
        public decimal? perc_retenc_judicial { get; set; }
        public string NumAnalitica { get; set; }
        public int anac_icod_analitica { get; set; }
        public decimal? perc_nasig_transporte { get; set; }

        #region variables de contratacion

        public int? pctd_icod_ccosto { get; set; }

        public DateTime? pctd_sfecha_ini_contrato { get; set; }
        public DateTime? pctd_sfecha_fin_contrato { get; set; }
        public DateTime? pctd_sfecha_ini_actividad { get; set; }
        public DateTime? pctd_sfecha_fin_actividad { get; set; }
        public DateTime? pctd_sfecha_cese { get; set; }
        public int? tbpd_icod_motivo_cese { get; set; }
        public int tiporegistro { get; set; }
        public string ocod_vdireccion_documento { get; set; }

        public string strMotivo { get; set; }
        public int intTpoOperacion { set; get; }
        public int? pctd_icod_persona_contratacion { get; set; }
        #endregion
        public EPersonalCCostosCollection ePersonalCCostos = new EPersonalCCostosCollection();

        public EPersonalCCostosCollection EPersonalCCostosX { get { return ePersonalCCostos; } }
    }
    public class EPersonalCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EPersonalCCostosCollection)))
                    return TypeDescriptor.GetProperties(typeof(EPersonalCCostos));
            }
            return TypeDescriptor.GetProperties(typeof(EPersonal));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EPersonalX";
        }
    }
}
