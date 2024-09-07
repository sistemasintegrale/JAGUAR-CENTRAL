using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocPorPagar : EAuditoria
    {
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public int mesec_iid_mes { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int idTipodocuemnto { get; set; }
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_vencimiento_doc { get; set; }

        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal doxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string doxpc_vdescrip_transaccion { get; set; }
        [DataMember]
        public decimal doxpc_nmonto_destino_gravado { get; set; }
        [DataMember]
        public decimal doxpc_nmonto_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_referencial_cif { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_servicio_no_domic { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_gravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_retencion_rh { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_retenido { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo { get; set; }
        [DataMember]
        public decimal Diferencia { get; set; }
        [DataMember]
        public decimal Diferencia2 { get; set; }
        [DataMember]
        public decimal? doxpc_nporcentaje_igv { get; set; }
        [DataMember]
        public decimal? doxpc_nporcentaje_imp_renta { get; set; }
        [DataMember]
        public decimal? cdxpc_suma_monto { get; set; }
        [DataMember]
        public int? tablc_iid_situacion_documento { get; set; }
        [DataMember]
        public int? doxpc_tipo_comprobante_referencia { get; set; }
        [DataMember]
        public string doxpc_num_serie_referencia { get; set; }
        [DataMember]
        public string doxpc_num_comprobante_referencia { get; set; }
        [DataMember]
        public int percc_icod_percepcion { get; set; }


        [DataMember]
        public decimal? doxpc_nporcentaje_isc { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_isc { get; set; }
        [DataMember]
        public string doxpc_vnro_deposito_detraccion { get; set; }
        [DataMember]
        public DateTime? doxpc_sfec_deposito_detraccion { get; set; }
        [DataMember]
        public long doxpc_iid_correlativo { get; set; }
        [DataMember]
        public string doxpc_viid_correlativo { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string tdodc_descripcion { get; set; }
        [DataMember]
        public int proc_icod_proveedor { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string proc_iid_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public int proc_analitica_icod { get; set; }
        [DataMember]
        public string clase_viid_correlativo { get; set; }
        [DataMember]
        public int idDetalleClaseDocumento { get; set; }
        [DataMember]
        public string vSituacion { get; set; }
        [DataMember]
        public string vMoneda { get; set; }

        [DataMember]
        public string doxpc_origen { get; set; }
        [DataMember]
        public decimal MontoDolares { get; set; }
        [DataMember]
        public decimal SaldoDolares { get; set; }
        [DataMember]
        public bool doxpc_flag_estado { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_emision_referencia { get; set; }
        [DataMember]
        public string proc_vcta_bco_nacion { get; set; }
        [DataMember]
        public short? doxpc_numdoc_tipo { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public decimal ValorVenta { get; set; }
        [DataMember]
        public string TipoRegistro { get; set; }
        [DataMember]
        public bool dxp_dcta_cuadra { get; set; }


        //---------------------adicionales       
        [DataMember]
        public int? anac_icod_analitica { get; set; }
        [DataMember]
        public string anac_iid_analitica { get; set; }
        [DataMember]
        public int? tipo_analitica { get; set; }
        [DataMember]
        public int facc_icod_doc { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
        [DataMember]
        public int CodigoClaseDocumento { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public decimal Valorcompra { get; set; }
        [DataMember]
        public int? vcocc_iid_voucher_contable { get; set; }
        [DataMember]
        public string doxpc_nmonto_rho_dolar { get; set; }
        [DataMember]
        public string cuenta_contable { get; set; }
        [DataMember]
        public int icod_cuenta_contable { get; set; }
        [DataMember]
        public string vdescripcion_cuenta_contable { get; set; }
        [DataMember]
        public int? tdodc_iestado_registro { get; set; }

        [DataMember]
        public decimal? SALDO_SOLES { get; set; }
        [DataMember]
        public decimal? PAGADO_SOLES { get; set; }
        [DataMember]
        public decimal? TOTAL_SOLES { get; set; }
        [DataMember]
        public decimal? SALDO_DOLARES { get; set; }
        [DataMember]
        public decimal? PAGADO_DOLARES { get; set; }
        [DataMember]
        public decimal? TOTAL_DOLARES { get; set; }
        [DataMember]
        public bool flag { get; set; }
        public bool flag_multiple { get; set; }

        public int? ctacc_icod_cuenta_gastos_nac { get; set; }

        public int doxpc_itipo_adquisicion { get; set; }
        public string doxpc_vnumero_serio { get; set; }
        public string doxpc_vnumero_correlativo { get; set; }
        public int doxpc_icod_documento { get; set; }
        /*DUA*/
        public string doxpc_codigo_aduana { get; set; }
        public string doxpc_anio { get; set; }
        public string doxpc_numero_declaracion { get; set; }
        /*Pago Reporte*/
        public string DocPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string MonedaPago { get; set; }
        public decimal MontoPago { get; set; }
        /*NC*/
        public int anad_icod_analitica { get; set; }
        public int TipoAnalitica { get; set; }
        public string NumAnalitica { get; set; }

        public string Color { get; set; }
        public int tablc_iid_clasificacion_compras { get; set; }
        public string ClasificacionCompras { get; set; }


        public int ocsc_icod_ocs { get; set; }
        public string NumOCS { get; set; }
        public string proc_vruc { get; set; }
    }

}
