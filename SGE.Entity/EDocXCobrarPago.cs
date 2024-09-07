using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocXCobrarPago : EAuditoria
    {
        [DataMember]
        public long pdxcc_icod_correlativo { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string Abreviatura { get; set; }
        [DataMember]
        public string pdxcc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? pdxcc_sfecha_cobro { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public decimal? pdxcc_nmonto_cobro { get; set; }
        [DataMember]
        public decimal? pdxcc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string pdxcc_vobservacion { get; set; }
        [DataMember]
        public int? efctc_icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public string CuentaBancaria { get; set; }
        [DataMember]
        public string EntidadFinanciera { get; set; }
        [DataMember]
        public int? cliec_icod_cliente { get; set; }
        [DataMember]
        public int? ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string CuentaContable { get; set; }
        [DataMember]
        public string DescripcionCuentaContable { get; set; }
        [DataMember]
        public int? IndicadorCosto { get; set; }
        [DataMember]
        public int? cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public string CentroCosto { get; set; }
        [DataMember]
        public string CentroCostoDesc { get; set; }
        [DataMember]
        public int? anac_icod_analitica { get; set; }
        [DataMember]
        public int? anac_icod_analitica_det { get; set; }
        [DataMember]
        public string TipoAnalitica { get; set; }
        [DataMember]
        public string Analitica { get; set; }
        [DataMember]
        public string pdxcc_vorigen { get; set; }
        [DataMember]
        public bool pdxcc_flag_estado { get; set; }
        [DataMember]
        public decimal? pdxcc_nmonto_cobro_dxc { get; set; }
        [DataMember]
        public long doxcc_icod_correlativo_pago { get; set; }
        [DataMember]
        public long? adpac_icod_correlativo { get; set; } //si el pago es por adelanto
        [DataMember]
        public long? ncpac_icod_correlativo { get; set; } //si el pago es por nota de crédito
        [DataMember]
        public DateTime? fecha_movimiento { get; set; } //auditoría pago
        [DataMember]
        public string tipo_movimiento { get; set; } //auditoría pago
        [DataMember]
        public string SimboloMoneda_DxC { get; set; } //auditoría pago - moneda del documento que se está pagando
        [DataMember]
        public string tdoc_abrv_dxc { get; set; } //auditoría pago - tipo dxc que se está pagando
        [DataMember]
        public string num_doc_dxc { get; set; } //auditoría pago - Nº dxc que se está pagando
        [DataMember]
        public DateTime? fecha_dxc { get; set; } //auditoría pago - fecha dxc que se está pagando
        [DataMember]
        public int? moneda_dxc { get; set; } //auditoría pago - tipo moneda dxc que se está pagando
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        [DataMember]
        public decimal? saldoDxP { get; set; }
        [DataMember]
        public decimal? pagoDxP { get; set; }
        /**/
        public int? tdodc_iid_correlativo { get; set; }
        public int? intTipoDoc { get; set; }
        public string strNroDoc { get; set; }
        public int id_tipo_moneda_dxc { get; set; }

        public int anac_iid_anio { get; set; }
    }
}
