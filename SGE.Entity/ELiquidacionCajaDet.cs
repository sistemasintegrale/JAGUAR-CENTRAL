using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ELiquidacionCajaDet : EAuditoria
    {
        [DataMember]
        public int lqcd_icod_deta_liquid { get; set; }
        [DataMember]
        public int lqcc_icod_liquid_caja { get; set; }
        [DataMember]
        public int lqcd_inro_item { get; set; }
        [DataMember]
        public DateTime lqcd_sfecha_liquid { get; set; }
        [DataMember]
        public int lqcd_icod_concepto_caja { get; set; }
        [DataMember]
        public string lqcd_vdescripcion_movim { get; set; }
        [DataMember]
        public decimal lqcd_nmonto_afecto { get; set; }
        [DataMember]
        public decimal lqcd_nmonto_inafecto { get; set; }
        [DataMember]
        public decimal lqcd_nmonto_dest_mixto { get; set; }
        [DataMember]
        public decimal lqcd_nporcent_igv { get; set; }
        [DataMember]
        public decimal lqcd_nmonto_igv { get; set; }
        [DataMember]
        public decimal lqcd_nmonto_pago { get; set; }
        [DataMember]
        public int? lqcd_iid_cuenta_contable { get; set; }
        [DataMember]
        public int? lqcd_iid_centro_costo { get; set; }
        [DataMember]
        public int? lqcd_iid_tipo_analitica { get; set; }
        [DataMember]
        public int? lqcd_iid_analitica { get; set; }
        [DataMember]
        public int? lqcd_iid_proveedor { get; set; }
        [DataMember]
        public int? lqcd_iid_tipo_doc { get; set; }
        [DataMember]
        public int? lqcd_iid_clase_tipo_doc { get; set; }
        [DataMember]
        public string lqcd_vnumero_doc { get; set; }
        [DataMember]
        public int? docxp_icod_pago { get; set; }
        [DataMember]
        public decimal lqcd_ntipo_cambio_pago { get; set; }
        [DataMember]
        public decimal lqcd_nporc_rta_cuarta { get; set; }
        [DataMember]
        public decimal lqcd_nmonto_rta_cuarta { get; set; }
        [DataMember]
        public int lqcd_flag_estado { get; set; }
        [DataMember]
        public int? operacion { get; set; }
        [DataMember]
        public string lqcd_vtipo_movimiento { get; set; }

        //** CAMPOS COMPLEMENTARIOS **//            

        [DataMember]
        public string concepto_abreviatura { get; set; }
        [DataMember]
        public string numero_cuenta_contable { get; set; }
        [DataMember]
        public string cuenta_descripcion { get; set; }
        [DataMember]
        public string codigo_ccosto { get; set; }
        [DataMember]
        public string ccosto_descripcion { get; set; }
        [DataMember]
        public string iid_analitica { get; set; }
        [DataMember]
        public string analisis { get; set; }
        [DataMember]
        public string analitica_descripcion { get; set; }
        [DataMember]
        public string codigo_provedor { get; set; }
        [DataMember]
        public string proveedor_nombre { get; set; }
        [DataMember]
        public string tip_doc_abreviatura { get; set; }
        [DataMember]
        public long? docxp_icod_correlativo { get; set; }
        [DataMember]
        public decimal? docxp_mto_total { get; set; }
        [DataMember]
        public decimal? docxp_mto_pagado { get; set; }
        [DataMember]
        public decimal? docxp_mto_saldo { get; set; }
        [DataMember]
        public int? lq_icod_tipo_doc_pago { get; set; }
        [DataMember]
        public int? lq_icod_clase_doc_pago { get; set; }
        [DataMember]
        public string lq_nro_doc_pago { get; set; }

        //*****//

        public int intIidClaseDoc { get; set; }
        public int MonedaDXP { get; set; }

    }
}
