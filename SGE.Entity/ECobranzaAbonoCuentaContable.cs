using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECobranzaAbonoCuentaContable
    {
        [DataMember]
        public int coabc_icod_cobranza_abono_cuenta_contable { get; set; }
        [DataMember]
        public int plcoc_icod_planilla_cobranza { get; set; }
        [DataMember]
        public int? deinc_icod_deposito_otro_ingreso { get; set; }
        [DataMember]
        public string deinc_vnumero_documento { get; set; }
        [DataMember]
        public short coabc_iingreso_cobranza_abono_cuenta_contable { get; set; }
        [DataMember]
        public int? cliec_icod_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre { get; set; }
        [DataMember]
        public string ClienteAnalitica { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string coabc_vnumero_documento { get; set; }
        [DataMember]
        public DateTime? coabc_sfecha_documento { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public decimal? coabc_nmonto_pago { get; set; }
        [DataMember]
        public decimal? coabc_nmonto_pago_soles { get; set; }
        [DataMember]
        public decimal? coabc_nmonto_pago_dolares { get; set; }
        [DataMember]
        public decimal coabc_nmonto_pago_ant { get; set; }
        [DataMember]
        public string coabc_vobservacion { get; set; }
        [DataMember]
        public DateTime? coabc_sfecha_pago_planilla_cobranza { get; set; }
        [DataMember]
        public int? efinc_icod_entidad_financiera { get; set; }
        [DataMember]
        public int? efctc_icod_enti_financiera { get; set; }
        [DataMember]
        public decimal? coabc_ntipo_cambio_pago { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_cuenta { get; set; }
        [DataMember]
        public int? ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string CuentaContable { get; set; }
        [DataMember]
        public string DescripcionCuentaContable { get; set; }
        [DataMember]
        public int? cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public string CentroCosto { get; set; }
        [DataMember]
        public string DescripcionCentroCosto { get; set; }
        [DataMember]
        public int? IndicadorCentroCosto { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_analitica { get; set; }
        [DataMember]
        public int? anac_icod_analitica { get; set; }
        [DataMember]
        public string Analitica { get; set; }
        [DataMember]
        public long? doxcc_icod_correlativo { get; set; }
        [DataMember]
        public long? pdxcc_icod_correlativo { get; set; }
        [DataMember]
        public int? coabc_isituacion { get; set; }
        [DataMember]
        public int? usuario { get; set; }
        [DataMember]
        public string pc { get; set; }
        [DataMember]
        public int operacion { get; set; }
        //para mostrar
        [DataMember]
        public string TipoDocumentoDeposito { get; set; }
        [DataMember]
        public string cliec_vcod_cliente { get; set; }
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }


    }
}
