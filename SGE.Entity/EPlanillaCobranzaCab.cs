using System;

namespace SGE.Entity
{
    public class EPlanillaCobranzaCab : EAuditoria
    {
        public int plnc_icod_planilla { get; set; }
        public string plnc_vnumero_planilla { get; set; }
        public DateTime plnc_sfecha_planilla { get; set; }
        public int tblc_iid_tipo_moneda { get; set; }
        public int tblc_iid_situacion { get; set; }
        public decimal plnc_nmonto_importe { get; set; }
        public decimal plnc_nmonto_pagado { get; set; }
        public string plnc_vobservaciones { get; set; }
        public string strSituacion { get; set; }
        public int intTipoOperacion { get; set; }
        public int? plnc_icod_ent_finan_mov_sol { get; set; }
        public int? plnc_icod_ent_finan_mov_dol { get; set; }
        public decimal? plnc_monto_soles { get; set; }
        public decimal? plnc_monto_dolares { get; set; }
        public DateTime? plnc_fecha_modi { get; set; }

        public int? intAnaliticaCtaBancariaSol { get; set; }
        public string strCodAnaliticaCtaBancariaSol { get; set; }
        public int? intCtaContableCtaBancariaSol { get; set; }
        public decimal? dcmlTipoCambioSol { get; set; }
        public decimal? dcmlTotalSol { get; set; }

        public int? intAnaliticaCtaBancariaDol { get; set; }
        public string strCodAnaliticaCtaBancariaDol { get; set; }
        public int? intCtaContableCtaBancariaDol { get; set; }
        public decimal? dcmlTipoCambioDol { get; set; }
        public decimal? dcmlTotalDol { get; set; }
    }
}
