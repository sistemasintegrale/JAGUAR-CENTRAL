using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocXCobrar : EAuditoria
    {
        [DataMember]
        public long doxcc_icod_correlativo { get; set; }
        [DataMember]
        public short? mesec_iid_mes { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public int? cliec_icod_cliente { get; set; }
        [DataMember]
        public int giroc_icod_giro { get; set; }
        [DataMember]
        public string giroc_vnombre_giro { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        public string cliec_cruc { get; set; }
        public string DireccionCliente { get; set; }
        public string NumDocCliente { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_doc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_pago { get; set; }
        [DataMember]
        public string doxcc_vdescrip_transaccion { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_afecto { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_inafecto { get; set; }
        //[DataMember]
        //public decimal? doxcc_nmonto_exportacion { get; set; }
        //[DataMember]
        //public decimal? doxcc_nmonto_servicio_no_domic { get; set; }
        [DataMember]
        public decimal? doxcc_nporcentaje_igv { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_impuesto { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_total { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_saldo { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_pagado { get; set; }
        //[DataMember]
        //public decimal? doxcc_nporcentaje_isc { get; set; }
        //[DataMember]
        //public decimal? doxcc_nmonto_isc { get; set; }
        [DataMember]
        public int? tablc_iid_situacion_documento { get; set; }
        [DataMember]
        public string doxcc_vobservaciones { get; set; }
        [DataMember]
        public bool doxc_bind_cuenta_corriente { get; set; }
        //[DataMember]
        //public int? tabl_iid_tipo_indicador_motivo_cta_xcobrar { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_entrega { get; set; }
        [DataMember]
        public bool doxcc_bind_impresion_nogerencia { get; set; }
        //[DataMember]
        //public int? doxcc_icod_vendedor { get; set; }
        //[DataMember]
        //public decimal? doxcc_nmonto_iva { get; set; }

        public Boolean doxc_bind_situacion_legal { get; set; }
        public Boolean doxc_bind_cierre_cuenta_corriente { get; set; }
        [DataMember]
        public int doxcc_tipo_comprobante_referencia { get; set; }
        [DataMember]
        public string doxcc_num_serie_referencia { get; set; }
        [DataMember]
        public string doxcc_num_comprobante_referencia { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_emision_referencia { get; set; }

        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public string cliec_vcod_cliente { get; set; }
        [DataMember]
        public string documento { get; set; }
        [DataMember]
        public int idTipodocuemnto { get; set; }

        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
        [DataMember]
        public int? CodigoClaseDocumento { get; set; }
        [DataMember]
        public string ClaseDocumento { get; set; }
        [DataMember]
        public string DescripcionClaseDocumento { get; set; }
        [DataMember]
        public decimal? MontoPagar { get; set; }
        [DataMember]
        public string FormaPago { get; set; }
        [DataMember]
        public decimal? ValorVenta { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public decimal MontoDolares { get; set; }
        [DataMember]
        public decimal SaldoDolares { get; set; }
        [DataMember]
        public bool doxcc_flag_estado { get; set; }
        [DataMember]
        public string doxcc_origen { get; set; }
        [DataMember]
        public string DescripcionOrigen { get; set; }
        [DataMember]
        public DateTime? fecha_movimiento { get; set; }
        [DataMember]
        public string tipo_movimiento { get; set; }
        [DataMember]
        public long? ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnumero_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnombre_descripcion_larga { get; set; }
        [DataMember]
        public decimal? monto_total_soles { get; set; }
        [DataMember]
        public decimal? monto_pagado_soles { get; set; }
        [DataMember]
        public decimal? monto_saldo_soles { get; set; }
        [DataMember]
        public decimal? monto_total_dolares { get; set; }
        [DataMember]
        public decimal? monto_pagado_dolares { get; set; }
        [DataMember]
        public decimal? monto_saldo_dolares { get; set; }
        public bool flag { get; set; }
        public bool flag_multiple { get; set; }
        /**/
        public int vcocc_icod_vcontable { get; set; }
        public int intAnaliticaCliente { get; set; }

        public decimal dcmlMontoServicios { get; set; }
        public decimal dcmlMontoMercaderia { get; set; }
        public int intCtaTotal { get; set; }
        public int intCtaIGV { get; set; }
        //public int intCtaServicios { get; set; }
        public int intCtaMercaderia { get; set; }
        public int intCtaServicios { get; set; }

        [DataMember]
        public int anac_icod_analitica { get; set; }
        [DataMember]
        public string anac_iid_analitica { get; set; }

        public int pryc_icod_proyecto { get; set; }
        public string NomProyecto { get; set; }
        public string CentroCossto { get; set; }
        public long? ctacc_icod_cuenta_gastos_nac { get; set; }

        public int tablc_iid_tipo_docxpagar { get; set; }
        public string TipoDXC { get; set; }
        public int docxc_icod_documento { get; set; }
        public int puvec_icod_punto_venta { get; set; }
    }
}
