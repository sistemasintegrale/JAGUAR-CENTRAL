using System;

namespace SGE.Entity
{
    public class EProvisionPlanillaPersonalDetalle : EAuditoria
    {
        public int pland_icod_planilla_personal_det { get; set; }
        public int pland_iid_planilla_personal_det { get; set; }
        public int planc_icod_planilla_personal { get; set; }
        public string pland_ape_nom { get; set; }
        public string pland_num_doc { get; set; }
        public string pland_cussp { get; set; }
        public decimal pland_rem_basica { get; set; }
        public bool pland_flag_estado { get; set; }
        public int pland_icod_personal { get; set; }
        public bool? pland_beps { get; set; }
        public DateTime? pland_sfecha_incio { get; set; }
        public DateTime? pland_sfecha_cese { get; set; }
        public decimal? pland_nmonto_basico { get; set; }
        public bool? pland_basignacion_familiar { get; set; }
        public decimal? pland_nasignacion_familiar { get; set; }
        public decimal? pland_nrem_computable { get; set; }
        public decimal? pland_nmonto_gratificacion { get; set; }
        public decimal? pland_nbonificacion_mes { get; set; }
        public string strEPS { get; set; }
        //-----CTS----//
        public decimal? pland_ncts_gratificacion { get; set; }
        public decimal? pland_nctssexto_gratificacion { get; set; }
        public decimal? pland_ncts_comision { get; set; }
        public decimal? pland_nctssexto_comision { get; set; }
        public decimal? pland_ncts_total { get; set; }
        public int? pland_icts_meses { get; set; }
        public int? pland_icts_dias { get; set; }
        public decimal? pland_ncts_meses_monto { get; set; }
        public decimal? pland_ncts_dias_monto { get; set; }
        public decimal? pland_ncts_por_mes { get; set; }
        public decimal? pland_nctsprovision_acumulada { get; set; }
        public decimal? pland_nctsprovision_mes { get; set; }
        public int? pland_ncts_horas_extras { get; set; }

        public decimal? pland_vac_dias_Prov_mensual { get; set; }
        public decimal? pland_vac_dias_acumulado { get; set; }
        public decimal? pland_vac_provision_mes { get; set; }
        public decimal? pland_vac_ajuste_mes { get; set; }
        public decimal? pland_vac_prov_tot_mensual { get; set; }
        //*****Tabla Parametro Planilla
        public decimal? prpc_nasignacion_familiar { get; set; }
        public decimal? prpc_ngratificacion_essalud { get; set; }
        public decimal? prpc_ngratificacion_eps { get; set; }
        public int operacion { get; set; }
        public string strCargo { get; set; }

        /**/
        public int iid_analitica { get; set; }
        public string analitica_descripcion { get; set; }
        public int anad_icod_analitica { get; set; }
    }
}
