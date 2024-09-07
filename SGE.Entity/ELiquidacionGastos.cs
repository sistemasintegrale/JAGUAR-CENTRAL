using System;

namespace SGE.Entity
{
    public class ELiquidacionGastos : EAuditoria
    {
        public int lqgc_icod_correlativo { get; set; }
        public int lqgc_icorrelativo { get; set; }
        public int anioc_iid_anio { get; set; }
        public int mesec_iid_mes { get; set; }
        public string lqgc_vnumero_liq_gasto { get; set; }
        public DateTime lqgc_sfecha_liq_gasto { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal lqgc_nmonto_total { get; set; }
        public decimal lqgc_nmonto_pagado { get; set; }
        public decimal lqgc_nmonto_tipo_cambio { get; set; }
        public DateTime lqgc_sfecha_vencimiento { get; set; }
        public int tablc_iid_sit_liq_gasto { get; set; }
        public string lqgc_vconcepto { get; set; }
        public Int64 doxpc_icod_correlativo { get; set; }
        public int? vcocc_iid_voucher_contable { get; set; }

        public string strDesProveedor { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
    }
}
