using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EDocPorPagarPago : EAuditoria
    {
        [DataMember]
        public long pdxpc_icod_correlativo { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public string pdxpc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? pdxpc_sfecha_pago { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal? pdxpc_nmonto_pago { get; set; }
        [DataMember]
        public decimal? pdxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public string pdxpc_vobservacion { get; set; }
        [DataMember]
        public int? efctc_icod_enti_financiera_cuenta { get; set; }
        [DataMember]
        public string pdxpc_vorigen { get; set; }
        [DataMember]
        public long? doxcc_icod_correlativo { get; set; }
        [DataMember]
        public long? ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public int? cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public string cecoc_ccodigo_centro_costo { get; set; }
        [DataMember]
        public int? anac_icod_analitica { get; set; }
        [DataMember]
        public string anac_viid_analitica { get; set; }
        public string ctacc_numero_cuenta_contable { get; set; }
        [DataMember]
        public string anac_vdescripcion { get; set; }
        [DataMember]
        public bool pdxpc_flag_estado { get; set; }
        [DataMember]
        public int pdxpc_mes { get; set; }
        [DataMember]
        public string DescripcionCuentaContable { get; set; }
        [DataMember]
        public int IdTipoAnalitica { get; set; }
        [DataMember]
        public string Moneda { get; set; }
        [DataMember]
        public int ctacc_iid_cuenta_padre { get; set; }
        [DataMember]
        public string ctacc_vnombre_descripcion_larga { get; set; }
        [DataMember]
        public string cecoc_vdescripcion { get; set; }
        [DataMember]
        public decimal? pdxpc_nmonto_pago_dxp { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo_pago { get; set; }
        [DataMember]
        public int? vcocc_iid_voucher_contable { get; set; }
        //datos
        [DataMember]
        public string Abreviatura { get; set; }
        [DataMember]
        public string SimboloMoneda { get; set; }
        [DataMember]
        public string CuentaBancaria { get; set; }
        [DataMember]
        public string EntidadFinanciera { get; set; }
        [DataMember]
        public string CuentaContable { get; set; }
        [DataMember]
        public int? IndicadorCosto { get; set; }
        [DataMember]
        public string CentroCosto { get; set; }
        [DataMember]
        public string CentroCostoDesc { get; set; }
        [DataMember]
        public long? TipoAnalitica { get; set; }
        [DataMember]
        public decimal saldoDxP { get; set; } //para mandar el saldo del dxp cuando se actualice
        [DataMember]
        public decimal pagoDxP { get; set; }
        /**/
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }
        public int? intTipoDoc { get; set; }
        public string strNroDoc { get; set; }

        public string numero_doc_dxp { get; set; }
        public int? moneda_dxp { get; set; }

        public int cliec_icod_cliente { get; set; }
        public string cliec_vnombre_cliente { get; set; }
        public int IcodTD { get; set; }
        public int IddTD { get; set; }
        public string TDDXC { get; set; }
        public int IcodDXC { get; set; }
        public string NumDXC { get; set; }
        public int MonedaDXC { get; set; }
        public decimal pdxcc_nmonto_cobro { get; set; }
        public int doxcc_icod_correlativo_pago { get; set; }

        public int IcodProveedorDXP { get; set; }
        public int IcodCleinteDXC { get; set; }
        public int MonDXC { get; set; }

    }
}
