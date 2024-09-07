using System;

namespace SGE.Entity
{
    public class ELetraPorCobrarDet : EAuditoria
    {
        public int lxcpc_icod_correlativo { get; set; }
        public int lexcc_icod_correlativo { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal lxcpc_nmonto_pago { get; set; }
        public decimal lxcpc_nmonto_tipo_cambio { get; set; }
        public DateTime? lxcpc_sfecha_doc { get; set; }
        public DateTime? lxcpc_sfecha_pago { get; set; }
        public string lxcpc_vconcepto { get; set; }
        public string lxcpc_vglosa { get; set; }
        public Int64? doxcc_icod_correlativo { get; set; }
        public Int64? pdxcc_icod_correlativo { get; set; }
        public int? tdocc_icod_tipo_doc { get; set; }
        public int? tdocc_iid_clase_doc { get; set; }
        public string pdxcc_vnumero_doc { get; set; }
        public int? tablc_iid_tipo_cuenta_debe_haber { get; set; }
        public int? ctacc_iid_cuenta_contable { get; set; }
        public int? cecoc_icod_centro_costo { get; set; }
        public int? anac_icod_analitica { get; set; }
        public int? lxcpc_isituacion { get; set; }
        public string lxcpc_tipo_pago { get; set; }

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
