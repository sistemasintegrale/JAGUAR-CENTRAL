using System;

namespace SGE.Entity
{
    public class EPagoDocVenta : EAuditoria
    {
        public int pgoc_icod_pago { get; set; }
        public int tdocc_icoc_tipo_documento { get; set; }
        public int? pgoc_icod_documento { get; set; }
        public DateTime pgoc_sfecha_pago { get; set; }
        public int pgoc_tipo_pago { get; set; }
        public int? pgoc_icod_nota_credito { get; set; }
        public int? pgoc_icod_tipo_tarjeta { get; set; }
        public int pgoc_icod_tipo_moneda { get; set; }
        public string pgoc_descripcion { get; set; }
        public decimal pgoc_nmonto { get; set; }
        public decimal pgoc_tipo_cambio { get; set; }
        public Int64 pgoc_dxc_icod_pago { get; set; }
        public int pgoc_iid_tipo_doc_docventa { get; set; }
        public string strTipoPago { get; set; }
        public int intTipoOperacion { get; set; }

        public string pgoc_vnumero_planilla { get; set; }
        public int pgoc_icod_cliente { get; set; }
        public Int64 pgoc_dxc_icod_doc { get; set; }
        public Int64? pgoc_dxc_icod_canje_doc { get; set; }

        public DateTime? pgoc_fecha_venc_credito { get; set; }
        public int? tblc_iid_banco_cheque { get; set; }
        public string pgoc_nro_cheque { get; set; }
        public DateTime? pgoc_fecha_cob_cheque { get; set; }
        public int? bcoc_icod_banco { get; set; }
        public int? bcod_icod_banco_cuenta { get; set; }
        public int? pgoc_icod_anticipo { get; set; }
        public int? pgoc_icod_entidad_finan_mov { get; set; }

        public string strNroNotaCredito { get; set; }
        public string strNroAnticipo { get; set; }
        public string strNroCuenta { get; set; }
        public string strTipoMoneda { get; set; }

        public int intCliente { get; set; }

        public int? intCtaContableBcoTarjeta { get; set; }
        public int? intAnaliticaBcoTarjeta { get; set; }
        public string strCodAnaliticaBcoTarjeta { get; set; }

        public int? tdodc_iid_correlativo_nota_credito { get; set; }
        public int? tdodc_iid_correlativo_anticipo { get; set; }

        public int? intAnaliticaClienteNC { get; set; }
        public string strCodAnaliticaClienteNC { get; set; }
        public int? intAnaliticaClienteAnticipo { get; set; }
        public string strCodAnaliticaClienteAnticipo { get; set; }

    }
}
