using System;
using System.Drawing;

namespace SGE.Entity
{
    public class ELetraPorPagar : EAuditoria
    {
        public int lexpc_icod_correlativo { get; set; }
        public int lexpc_icorrelativo { get; set; }
        public int anioc_iid_anio { get; set; }
        public int mesec_iid_mes { get; set; }
        public string lexpc_vnumero_letra { get; set; }
        public string lexpc_vnumero_letra_proveedor { get; set; }
        public int? lexpc_inumero_renovacion { get; set; }
        public DateTime lexpc_sfecha_letra { get; set; }
        public int proc_icod_proveedor { get; set; }
        public string lexpc_vaval { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal lexpc_nmonto_total { get; set; }
        public decimal lexpc_nmonto_pagado { get; set; }
        public decimal lexpc_nmonto_tipo_cambio { get; set; }
        public DateTime lexpc_sfecha_vencimiento { get; set; }
        public int tablc_iid_situacion_letra { get; set; }
        public int? tablc_iid_condicion_letra { get; set; }
        public int? efinc_icod_entidad_financiera { get; set; }
        public string lexpc_vnumero_ubd { get; set; }
        public string lexpc_vobservaciones { get; set; }
        public int? tablc_iid_ubicacion_letra { get; set; }
        public Int64 doxpc_icod_correlativo { get; set; }
        public int? lexpc_icod_correlativo_renovacion { get; set; }
        public int? vcocc_iid_voucher_contable { get; set; }
        public string strDesProveedor { get; set; }
        public string strSituacion { get; set; }
        public string strCondicion { get; set; }
        public string strUbicacion { get; set; }
        public string strMoneda { get; set; }
        public string strNumero { get; set; }
        public string fitec_vruta { get; set; }
        public Image imagen { get; set; }
    }
}
