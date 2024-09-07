using System;

namespace SGE.Entity
{
    public class ELiquidacionGastosDet : EAuditoria
    {
        public int lqgd_icod_correlativo { get; set; }
        public int lqgc_icod_correlativo { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal lqgd_nmonto_pago { get; set; }
        public decimal lqgd_nmonto_tipo_cambio { get; set; }
        public DateTime? lqgd_sfecha_doc { get; set; }
        public DateTime? lqgd_sfecha_pago { get; set; }
        public string lqgd_vconcepto { get; set; }
        public Int64? doxpc_icod_correlativo { get; set; }
        public Int64? pdxpc_icod_correlativo { get; set; }
        public int? tdocc_icod_tipo_doc { get; set; }
        public int? tdocc_iid_clase_doc { get; set; }
        public string pdxpc_vnumero_doc { get; set; }
        public int? tablc_iid_tipo_cuenta_debe_haber { get; set; }
        public int? ctacc_iid_cuenta_contable { get; set; }
        public int? cecoc_icod_centro_costo { get; set; }
        public int? anac_icod_analitica { get; set; }
        public string lqgd_tipo_pago { get; set; }

        public int intTipoOperacion { get; set; }

        public string strDesCuenta { get; set; }
        public string strCodCCosto { get; set; }
        public string strDesCCosto { get; set; }
        public string strCodAnalitica { get; set; }
        public string strCodSubAnalitica { get; set; }
        public string strTipoDoc { get; set; }
        public string strDebeHaber { get; set; }
        public decimal dblMontoDoc { get; set; }
        public decimal dblSaldoDoc { get; set; }

    }
}
