using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EVoucherContableDet : EAuditoria
    {
        [DataMember]
        public int vcocd_icod_det { get; set; }
        [DataMember]
        public int vcocc_icod_vcontable { get; set; }
        [DataMember]
        public int? vcocd_nro_item_det { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string vcocd_numero_doc { get; set; }
        [DataMember]
        public long? ctacc_icod_cuenta_contable { get; set; }
        [DataMember]
        public int? cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_analitica { get; set; }
        [DataMember]
        public int? anad_icod_analitica { get; set; }
        [DataMember]
        public string vcocd_vglosa_linea { get; set; }
        [DataMember]
        public int tablc_iid_moneda { get; set; }
        [DataMember]
        public decimal? vcocd_tipo_cambio { get; set; }
        [DataMember]
        public decimal? vcocd_nmto_tot_debe_sol { get; set; }
        [DataMember]
        public decimal? vcocd_nmto_tot_haber_sol { get; set; }
        [DataMember]
        public decimal? vcocd_nmto_tot_debe_dol { get; set; }
        [DataMember]
        public decimal? vcocd_nmto_tot_haber_dol { get; set; }
        [DataMember]
        public long? ctacc_iid_cuenta_contable_ref { get; set; }
        [DataMember]
        public bool vcocd_flag_estado { get; set; }
        [DataMember]
        public int tarec_icorrelativo_origen_vcontable { get; set; }
        /*---------------------------------------------------------------------------------*/
        [DataMember]
        public DateTime dtFechaVContable { get; set; }
        [DataMember]
        public string strMonedaVContable { get; set; }
        [DataMember]
        public string strNroVContable { get; set; }
        [DataMember]
        public string strTipoDoc { get; set; }
        [DataMember]
        public string strTipNroDocumento { get; set; }
        [DataMember]
        public string strNroCuenta { get; set; }
        [DataMember]
        public string strDesCuenta { get; set; }
        [DataMember]
        public string strCodCCosto { get; set; }
        [DataMember]
        public string strDesCCosto { get; set; }
        [DataMember]
        public string strTipoAnalitica { get; set; }
        [DataMember]
        public string strCodAnaliica { get; set; }
        [DataMember]
        public string strDesAnalitica { get; set; }
        [DataMember]
        public string strAnalisis { get; set; }
        [DataMember]
        public string strAnalCCosto { get; set; }
        [DataMember]
        public int intTipoOperacion { get; set; }
        [DataMember]
        public int? ctacc_icod_cuenta_debe_auto { get; set; }
        [DataMember]
        public int? ctacc_icod_cuenta_haber_auto { get; set; }
        /*---------------------------------------------------------------------------------*/
        [DataMember]
        public decimal dblCuentaSaldoAntSol { get; set; }
        [DataMember]
        public decimal dblCuentaSaldoAntDol { get; set; }
        [DataMember]
        public decimal dblCuentaSaldoActSol { get; set; }
        [DataMember]
        public decimal dblCuentaSaldoActDol { get; set; }
        /**/
        public string anac_cecoc_tipo { get; set; }
        public string anac_cecoc_code { get; set; }
        public string anac_cecoc_vdescripcion { get; set; }
        public bool detFlag { get; set; }
        /**/
        public decimal? ctacc_iid_cuenta_contable_acumulado_sol { get; set; }
        public decimal? ctacc_iid_cuenta_contable_acumulado_dol { get; set; }

        public decimal? ctacc_iid_cuenta_contable_saldo_sol { get; set; }
        public decimal? ctacc_iid_cuenta_contable_saldo_dol { get; set; }

        public decimal? cuenta_iid_cuenta_contable_acumulado_sol { get; set; }
        public decimal? cuenta_iid_cuenta_contable_acumulado_dol { get; set; }

        public decimal? cuenta_iid_cuenta_contable_saldo_sol { get; set; }
        public decimal? cuenta_iid_cuenta_contable_saldo_dol { get; set; }
        public DateTime fec_cab { get; set; }
        public string iid_subdiario_vnum_voucher { get; set; }
        public int? id_mes { get; set; }
        public int? id_subdiario { get; set; }
        // CAMPOS PARA EL REPORTE DE RESUMEN DE MOVIMIENTOS (MAYOR AUXILIAR ACUMULADO)
        public decimal? subdi_vent_nmto_tot_debe_sol { get; set; }
        public decimal? subdi_vent_nmto_tot_haber_sol { get; set; }
        public decimal? subdi_vent_nmto_saldo { get; set; }

        public decimal? subdi_comp_nmto_tot_debe_sol { get; set; }
        public decimal? subdi_comp_nmto_tot_haber_sol { get; set; }
        public decimal? subdi_comp_nmto_saldo { get; set; }

        public decimal? subdi_banc_nmto_tot_debe_sol { get; set; }
        public decimal? subdi_banc_nmto_tot_haber_sol { get; set; }
        public decimal? subdi_banc_nmto_saldo { get; set; }
        /**/
        public string cta_padre { get; set; }
        public string cta_vdespadre { get; set; }

        public int doxpc_icod_correlativo { get; set; }
        public int doxcc_icod_correlativo { get; set; }

        /*Inventario Resultado*/
        public decimal? Deudor { get; set; }
        public decimal? Acreedor { get; set; }

        public decimal? Activo { get; set; }
        public decimal? Pasivo { get; set; }

        public decimal? NPerdida { get; set; }
        public decimal? NGanancia { get; set; }

        public decimal? FPerdida { get; set; }
        public decimal? FGanancia { get; set; }

        public int tablc_iid_tipo_moneda_CB { get; set; }
        public decimal? TipoCambio { get; set; }
    }
}
