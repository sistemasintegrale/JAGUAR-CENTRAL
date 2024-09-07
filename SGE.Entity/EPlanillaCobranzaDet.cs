using System;

namespace SGE.Entity
{
    public class EPlanillaCobranzaDet : EAuditoria
    {
        public int plnd_icod_detalle { get; set; }
        public int plnc_icod_planilla { get; set; }
        public int tablc_iid_tipo_mov { get; set; } // FAC, PAGO, ANTICIPO
        public int tablc_iid_tipo_moneda { get; set; } // FAC, PAGO, ANTICIPO
        public DateTime plnd_sfecha_doc { get; set; }
        public int? plnd_icod_tipo_doc { get; set; }
        public int? plnd_icod_documento { get; set; }
        public int? pgoc_icod_pago { get; set; }
        public int? antc_icod_anticipo { get; set; }
        public string plnd_vnumero_doc { get; set; }
        public decimal plnd_nmonto { get; set; }
        public decimal plnd_nmonto_pagado { get; set; }
        public decimal plnd_tipo_cambio { get; set; }
        public string strTipoDoc { get; set; }
        public string strTipoMov { get; set; }
        public string strCliente { get; set; }
        public string strTipoMoneda { get; set; }
        public int intCliente { get; set; }
        /**/
        public Int64? pgoc_dxc_icod_pago { get; set; }
        public Int64? intIcodDxc { get; set; }
        public int? intTipoPago { get; set; }
        public int? intNotaCredito { get; set; }
        public int? intTipoTarjeta { get; set; }
        public int intSituacionFavBov { get; set; }
        public string strSituacionFavBov { get; set; }
        public string strPagoDescripcion { get; set; }
        public string StrNotaCredito { get; set; }
        public string strAnticipo { get; set; }
        public string strAdelantoCliente { get; set; }
        public string strMonedaGroup { get; set; }

        public decimal dblPagoEfectivo { get; set; }
        public decimal dblPagoTarjetaCredito { get; set; }
        public decimal dblPagoNotaCredito { get; set; }
        public decimal dblPagoCheque { get; set; }
        public decimal dblPagoTransferencia { get; set; }
        public decimal dblPagoCredito { get; set; }
        public decimal dblPagoAnticipo { get; set; }
        public int? intIcodEntidadFinanMov { get; set; }

        public int intAnaliticaCliente { get; set; }
        public string strCodAnaliticaCliente { get; set; }
        public int? tdocd_iid_correlativo { get; set; }

        public int? intAnaliticaBancoTarjetaBanco { get; set; }
        public string strCodAnaliticaBancoTarjetaBanco { get; set; }
        public string strNroOt { get; set; }
        public int? intCtaCbleTarjetaBanco { get; set; }
        public int? intTipoDocDelPago { get; set; }

        public string tdocc_vabreviatura_tipo_docPago { get; set; }
        public int ctacc_icod_cuenta_contable_nac { get; set; }
        public int tdocd_iid_codigo_doc_det { get; set; }
        public int tablc_iid_tipo_pago { get; set; }
    }
}
