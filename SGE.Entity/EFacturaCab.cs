using System;

namespace SGE.Entity
{
    public class EFacturaCab : EAuditoria
    {
        public int intCorrelativo { get; set; }
        public int favc_icod_factura { get; set; }
        public string favc_vnumero_factura { get; set; }
        public DateTime favc_sfecha_factura { get; set; }
        public DateTime favc_sfecha_vencim_factura { get; set; }
        public int favc_icod_cliente { get; set; }
        public string clic_vcod_cliente { get; set; }
        public string favc_vdireccion_cliente { get; set; }
        public string cliec_vnombre_cliente { get; set; }
        public string favc_vruc { get; set; }
        public string bovc_vnombre_cliente { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public int tablc_iid_forma_pago { get; set; }
        public int tablc_iid_situacion { get; set; }
        public decimal favc_npor_imp_igv { get; set; }
        public decimal favc_nmonto_neto { get; set; }
        public decimal favc_noperacion_gravadas { get; set; }
        public decimal favc_noperacion_inafectas { get; set; }
        public decimal favc_nmonto_imp { get; set; }
        public decimal favc_nmonto_total { get; set; }
        public decimal favc_nmonto_descuento { get; set; }
        public Int64 doxcc_icod_correlativo { get; set; }
        public Boolean favc_bincluye_igv { get; set; }
        public int ubicc_icod_ubicacion { get; set; }
        public int cliec_nnumero_dias { get; set; }
        public bool favc_flag_estado { get; set; }
        public string favc_vobservacion { get; set; }

        public int? remic_icod_remision { get; set; }
        public string remic_vnumero_remision { get; set; }
        //public Boolean favc_bafecto_igv { get; set; }
        public int? favc_tipo_factura { get; set; }
        public decimal favc_nanticipo { get; set; }
        public string favc_vnomb_anticipo { get; set; }
        public string favc_vnomb_garantia { get; set; }
        public decimal favc_nmonto_garantia { get; set; }
        public int vendc_icod_vendedor { get; set; }
        public int puvec_icod_punto_venta { get; set; }
        public int favc_iid_almacen { get; set; }
        public string favc_vnumero_orden { get; set; }
        public string favc_vnumero_guia { get; set; }
        public string NomVendedor { get; set; }
        //public string favc_vdescripcion_almacen { get; set; }
        public string favc_viid_almacen { get; set; }
        /**/


        public string strFormaPago { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
        public string strTelefonoCliente { get; set; }
        public string strTipoFactura { get; set; }

        public string strDistritoCliente { get; set; }
        public string strTipoVenta { get; set; }
        public int intEjercicio { get; set; }
        public int intTipoDoc { get; set; }
        public decimal dcmlTipoCambio { get; set; }

        public int almac_icod_almacen { get; set; }
        public int almac_icod_almacen_ingreso { get; set; }

        public int favc_itipo_venta { get; set; }
        public string favc_vnumero_orden_pedido { get; set; }
        public int favc_icod_pvt { get; set; }
        public string DesPVT { get; set; }
        public string DesAlmacen { get; set; }

        public string favc_descripcion_motivo_baja { get; set; }

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
        public decimal? facv_nmonto_credito { get; set; }
        public decimal? MontoCuota { get; set; }
        public string FechaPago { get; set; }

        public string FormaPagoS { get; set; }
        public decimal? MontoTotalPago { get; set; }
        public decimal? facv_nmonto_1era_cuota { get; set; }
        public DateTime? facv_sfecha_pago_1era_cuota { get; set; }
        public string FEmisionPresentacion { get; set; }
        public int Dias { get; set; }
        public decimal? facv_nporc_retencion { get; set; }
        public decimal? facv_nmonto_retencion { get; set; }
        public bool actualizarSerie { get; set; }
    }
}
