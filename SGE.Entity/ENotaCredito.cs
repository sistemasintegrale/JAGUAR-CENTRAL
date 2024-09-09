using System;

namespace SGE.Entity
{
    public class ENotaCredito : EAuditoria
    {
        public int ncrec_icod_credito { get; set; }
        public string ncrec_vnumero_credito { get; set; }
        public DateTime ncrec_sfecha_credito { get; set; }
        public int cliec_icod_cliente { get; set; }
        public int ncrec_ianio { get; set; }
        public string ncrec_vreferencia { get; set; }
        public int? tdocc_icod_tipo_doc { get; set; }
        public int? tablc_iid_tipo_nota_credito_venta { get; set; }
        public int? tdodc_iid_correlativo { get; set; }
        public string ncrec_vnumero_documento { get; set; }
        public DateTime ncrec_sfecha_documento { get; set; }
        public int? vendc_icod_vendedor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal ncrec_nmonto_neto { get; set; }
        public decimal ncrec_npor_imp_igv { get; set; }
        public decimal ncrec_nmonto_total { get; set; }
        public int ncrec_iid_situacion_credito { get; set; }
        public int? almac_icod_almacen { get; set; }
        public decimal ncrec_tipo_cambio_fecha_doc_venta { get; set; }
        public decimal ncrec_nmonto_iva { get; set; }
        public decimal ncrec_nmonto_pagado { get; set; }
        public Int64 ncrec_icod_dxc { get; set; }
        public Boolean? ncrec_bincluye_igv { get; set; }
        /**/
        public string strSituacion { get; set; }
        public int ubicc_icod_ubicacion { get; set; }
        public string strDesCliente { get; set; }
        public string strRuc { get; set; }
        public string DirecionCliente { get; set; }
        public string strTipoDoc { get; set; }
        public string strMoneda { get; set; }
        public int ncrec_iclase_doc { get; set; }
        public string StrClaseDocumento { get; set; }
        public int ncrec_tipo_nota_credito { get; set; }
        public string ncrec_vmotivo_sunat { get; set; }
        public string ncrec_vdetalle { get; set; }
        public string ncrec_descripcion_motivo_baja { get; set; }

        /*Datos Factura Electronica*/
        public int IdCabecera { get; set; }
        public string idDocumento { get; set; }
        public string StrTipoDoc { get; set; }
        public string fechaEmision { get; set; }
        public string horaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public string tipoDocumento { get; set; }
        public string moneda { get; set; }
        public string CodMotivoNota { get; set; }
        public string DescripMotivoNota { get; set; }
        public string NroDocqModifica { get; set; }
        public string TipoDocqModifica { get; set; }
        public int cantidadItems { get; set; }
        public string nombreComercialEmisor { get; set; }
        public string nombreLegalEmisor { get; set; }
        public string tipoDocumentoEmisor { get; set; }
        public string nroDocumentoEmisior { get; set; }
        public string CodLocalEmisor { get; set; }
        public string nroDocumentoReceptor { get; set; }
        public string tipoDocumentoReceptor { get; set; }
        public string nombreLegalReceptor { get; set; }
        public int CodMotivoDescuento { get; set; }
        public decimal PorcDescuento { get; set; }
        public decimal MontoDescuentoGlobal { get; set; }
        public decimal BaseMontoDescuento { get; set; }
        public decimal MontoTotalImpuesto { get; set; }
        public decimal MontoGravadasIGV { get; set; }
        public int CodigoTributo { get; set; }
        public decimal MontoExonerado { get; set; }
        public decimal MontoInafecto { get; set; }
        public decimal MontoGratuitoImpuesto { get; set; }
        public decimal MontoBaseGratuito { get; set; }
        public decimal totalIgv { get; set; }
        public decimal MontoGravadosISC { get; set; }
        public decimal totalIsc { get; set; }
        public decimal MontoGravadosOtros { get; set; }
        public decimal totalOtrosTributos { get; set; }
        public decimal TotalValorVenta { get; set; }
        public decimal TotalPrecioVenta { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal MontoTotalCargo { get; set; }
        public decimal MontoTotalAnticipo { get; set; }
        public decimal ImporteTotalVenta { get; set; }
        public int doc_icod_documento { get; set; }
        public int EstadoFacturacion { get; set; }
        public string EAnulado { get; set; }
        public string CodigoSunat { get; set; }
        public string EstadoSunat { get; set; }
        public string direccionReceptor { get; set; }
        public string UsuarioOSE { get; set; }
        public int puvec_icod_punto_venta { get; set; }
    }
}
