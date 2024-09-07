using System;

namespace SGE.Entity
{
    public class ELetraPorCobrar : EAuditoria
    {
        public int lexcc_icod_correlativo { get; set; }
        public int lexcc_icorrelativo { get; set; }
        public int anioc_iid_anio { get; set; }
        public int mesec_iid_mes { get; set; }
        public string lexcc_vnumero_letra { get; set; }
        public int? lexcc_inumero_renovacion { get; set; }
        public DateTime lexcc_sfecha_letra { get; set; }
        public int cliec_icod_cliente { get; set; }
        public string lexcc_vaval { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal lexcc_nmonto_total { get; set; }
        public decimal lexcc_nmonto_pagado { get; set; }
        public decimal lexcc_nmonto_tipo_cambio { get; set; }
        public DateTime lexcc_sfecha_vencimiento { get; set; }
        public int tablc_iid_situacion_letra { get; set; }
        public int? tablc_iid_condicion_letra { get; set; }
        public int? efinc_icod_entidad_financiera { get; set; }
        public string lexcc_vnumero_ubd { get; set; }
        public string lexcc_vobservaciones { get; set; }
        public int? tablc_iid_ubicacion_letra { get; set; }
        public Int64 doxcc_icod_correlativo { get; set; }
        public int? lexcc_icod_correlativo_renovacion { get; set; }
        public int? vcocc_iid_voucher_contable { get; set; }

        public string strDesCliente { get; set; }
        public string strSituacion { get; set; }
        public string strCondicion { get; set; }
        public string strUbicacion { get; set; }
        public string strMoneda { get; set; }
    }
}
