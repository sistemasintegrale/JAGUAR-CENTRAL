using System;

namespace SGE.Entity
{
    public class EPlanillaPersonalDetalle : EAuditoria
    {
        public int pland_icod_planilla_personal_det { get; set; }
        public int pland_iid_planilla_personal_det { get; set; }
        public int planc_icod_planilla_personal { get; set; }
        public string pland_ape_nom { get; set; }
        public string pland_num_doc { get; set; }
        public string pland_cussp { get; set; }
        public decimal pland_sueldo_basico { get; set; }
        public decimal pland_rem_basica { get; set; }
        public bool pland_flag_estado { get; set; }
        public int pland_icod_personal { get; set; }
        public DateTime? pland_sfecha_incio { get; set; }
        public DateTime? pland_sfecha_cese { get; set; }
        public decimal? pland_nasignacion_familiar { get; set; }
        public decimal? pland_nrem_computable { get; set; }

        public decimal? pland_reg_pension { get; set; }
        public decimal? pland_comision { get; set; }
        public decimal? pland_cargo { get; set; }
        public decimal? pland_hijo { get; set; }
        public decimal? pland_dias { get; set; }
        public decimal? pland_faltas { get; set; }
        public decimal? pland_vacaciones { get; set; }
        public decimal? pland_descanso_medico { get; set; }
        public decimal? pland_dias_subsidios { get; set; }
        public decimal? pland_dias_efectivos { get; set; }
        public decimal? pland_nmonto_vacaciones { get; set; }
        public decimal? pland_nhoras_25 { get; set; }
        public decimal? pland_nhoras_35 { get; set; }
        public decimal? pland_nferiado_descanso { get; set; }
        public decimal? pland_notros_ingresos { get; set; }
        public decimal? pland_nsubsidios_essalud { get; set; }
        public decimal? pland_ncomision_venta { get; set; }
        public decimal? pland_ncomision_eventual { get; set; }
        public decimal? pland_nasignacion_transporte { get; set; }
        public decimal? pland_nvales_alimentos { get; set; }
        public decimal? pland_nadelanto_sueldo { get; set; }
        public decimal? pland_ngratif_afecto { get; set; }
        public decimal? pland_nbonif_afecto { get; set; }
        public decimal? pland_nvacaciones_truncas { get; set; }
        public decimal? pland_ngratif_no_afecto { get; set; }
        public decimal? pland_nbonif_no_afecto { get; set; }
        public decimal? pland_nCTS { get; set; }
        public decimal? pland_nutilidades { get; set; }
        public decimal? pland_nremun_bruta { get; set; }
        public decimal? pland_nremun_computable_neta { get; set; }
        public decimal? pland_ninasistencias { get; set; }
        public decimal? pland_ntardanzas { get; set; }
        public decimal? pland_npago_utilid { get; set; }

        public string str_reg_pension { get; set; }
        public string str_comision { get; set; }
        public string str_hijo { get; set; }
        public string str_cargo { get; set; }
        public string str_fondo_pension { get; set; }

        public decimal? pland_desc_onp { get; set; }
        public decimal? pland_desc_fondo { get; set; }
        public decimal? pland_desc_comision { get; set; }
        public decimal? pland_desc_seguro { get; set; }
        public decimal? pland_desc_tot_afp { get; set; }
        public decimal? pland_desc_renta5 { get; set; }
        public decimal? pland_desc_adelanto { get; set; }
        public decimal? pland_desc_prestamo { get; set; }
        public decimal? pland_desc_eps { get; set; }
        public decimal? pland_desc_otros_desc_no_afect { get; set; }
        public decimal? pland_desc_retenc_judicial { get; set; }
        public decimal? pland_desc_otros_desc_afect { get; set; }
        public decimal? pland_desc_total_desc { get; set; }
        public decimal? pland_aport_essalud9 { get; set; }
        public decimal? pland_aport_eps_pacifico { get; set; }
        public decimal? pland_aport_essalud { get; set; }
        public decimal? pland_total_neto_pagar { get; set; }
        public int? pland_icod_fondo_pension { get; set; }
        public decimal? pland_nutilidad_convencional { get; set; }
        public decimal? pland_npago_utilidad_convencional { get; set; }
        public decimal? pland_desc_aporte_c_prov { get; set; }
        public decimal? pland_desc_aporte_s_prov { get; set; }

        public decimal? vccn_nvacaciones { get; set; }
        public decimal? vccn_nfondo { get; set; }
        public decimal? vccn_ncomision { get; set; }
        public decimal? vccn_nseguro { get; set; }
        public decimal? vccn_ntotal_afp { get; set; }
        public decimal? vccn_nopn { get; set; }
        public decimal? vccn_nrenta5 { get; set; }
        public decimal? vccn_notros_desc { get; set; }
        public decimal? vccn_nessalud { get; set; }
        public decimal? vccn_nvacaciones_neto { get; set; }
        public decimal? rmcn_remun_computable { get; set; }
        public decimal? rmcn_fondo { get; set; }
        public decimal? rmcn_comision { get; set; }
        public decimal? rmcn_seguro { get; set; }
        public decimal? rmcn_total_afp { get; set; }
        public decimal? rmcn_onp { get; set; }
        public decimal? rmcn_rta_5ta { get; set; }
        public decimal? rmcn_otros_dstos { get; set; }
        public decimal? rmcn_reten_judicial { get; set; }
        public decimal? rmcn_essalud { get; set; }
        public decimal? rmcn_remun_neto { get; set; }
        public decimal? rmcn_aporte_c_prov { get; set; }
        public decimal? rmcn_aporte_s_prov { get; set; }

        public int operacion { get; set; }

        public int iid_analitica { get; set; }
        public string analitica_descripcion { get; set; }
        public int anad_icod_analitica { get; set; }
        //**EXCEL AFPNET**//
        public string ApellPat { get; set; }
        public string ApellMat { get; set; }
        public string Nombres { get; set; }
        public int? afp { get; set; }
        public int? TipoDoc { get; set; }
        public int? AporteVoluntarioCon { get; set; }
        public int? AporteVoluntarioSin { get; set; }
        public int? AporteVoluntarioEmpleador { get; set; }
        public string TipoTrabajador { get; set; }
        public string RelacionLaboral { get; set; }
        public string InicioRL { get; set; }
        public string CeseRL { get; set; }
        public decimal? RemuneracionAsegurable { get; set; }
        //**EXCEL TELECREDITO**//
        public string TipoRegistro { get; set; }
        public string TipoCuenta { get; set; }
        public string CuentaAbono { get; set; }
        public string TipoMoneda { get; set; }
        public string ValidacionIDC { get; set; }

    }
}
