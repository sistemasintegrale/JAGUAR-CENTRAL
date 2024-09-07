using System;

using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ELibroBancosDetalle
    {
        [DataMember]
        public int icod_correlativo { get; set; }
        [DataMember]
        public int iid_correlativo { get; set; }
        [DataMember]
        public string vnumero_doc { get; set; }
        [DataMember]
        public int? icod_analitica { get; set; }
        [DataMember]
        public long? iid_cuenta_contable { get; set; }
        [DataMember]
        public decimal mto_mov_soles { get; set; }
        [DataMember]
        public decimal mto_mov_dolar { get; set; }
        [DataMember]
        public decimal mto_retenido_soles { get; set; }
        [DataMember]
        public decimal mto_retenido_dolar { get; set; }
        [DataMember]
        public decimal mto_detalle_soles { get; set; }
        [DataMember]
        public decimal mto_detalle_dolar { get; set; }
        [DataMember]
        public string vglosa { get; set; }
        [DataMember]
        public int? icod_centro_costo { get; set; }
        [DataMember]
        public int iusuario_crea { get; set; }
        [DataMember]
        public DateTime sfecha_crea { get; set; }
        [DataMember]
        public string vpc_crea { get; set; }
        [DataMember]
        public int iusuario_modifica { get; set; }
        [DataMember]
        public DateTime sfecha_modifica { get; set; }
        [DataMember]
        public string vpc_modifica { get; set; }
        [DataMember]
        public int icod_correlativo_cabecera { get; set; }
        [DataMember]
        public int? IdTipoAnalitica { get; set; }
        [DataMember]
        public string TipoAnalitica { get; set; }
        [DataMember]
        public string NumeroAnalitica { get; set; }
        [DataMember]
        public string NumeroCuentaContable { get; set; }
        [DataMember]
        public string DescripcionCuentaContable { get; set; }
        [DataMember]
        public string CodigoCentroCosto { get; set; }
        [DataMember]
        public string DescripcionCentroCosto { get; set; }
        [DataMember]
        public bool mobdc_flag_estado { get; set; }
        [DataMember]
        public long? doxpc_icod_correlativo { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }

        public int adci_icod_correlativo { get; set; }

        [DataMember]
        public decimal doxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal MontoDolares { get; set; }
        [DataMember]
        public decimal docxp_nmonto_total_saldo { get; set; }
        [DataMember]
        public decimal docxc_nmonto_total_saldo { get; set; }
        [DataMember]
        public decimal SaldoDolares { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public long? doxcc_icod_correlativo { get; set; }
        [DataMember]
        public long? docxp_icod_pago { get; set; }//pdxpc_icod_correlativo
        [DataMember]
        public long? docxc_icod_pago { get; set; }//pdxcc_icod_correlativo
        [DataMember]
        public long? adclie_icod_pago { get; set; } //adpac_icod_correlativo
        [DataMember]
        public long? ncclie_icod_pago { get; set; }//ncpac_icod_correlativo
        [DataMember]
        public long? adprov_icod_pago { get; set; }//adpap_icod_correlativo
        [DataMember]
        public long? ncprov_icod_pago { get; set; }//ncpap_icod_correlativo      
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_doc { get; set; }
        [DataMember]
        public decimal docxc_nmonto_total_documento { get; set; }
        [DataMember]
        public decimal docxp_nmonto_total_documento { get; set; }
        [DataMember]
        public decimal doxcc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public long? docxc_icod_documento { get; set; }//adelanto o nota de crédito de clientes
        [DataMember]
        public long? doxpc_icod_documento { get; set; }//adelanto o nota de crédito de proveedores
        [DataMember]
        public string mobdc_vcta_bco_nacion { get; set; }
        [DataMember]
        public int? mobdc_iid_anio { get; set; }
        [DataMember]
        public int? mobdc_iid_mes_periodo { get; set; }
        [DataMember]
        public int TipOper { get; set; }

        public int? mobdc_icod_proveedor { get; set; }
        public int? mobdc_icod_cliente { get; set; }
        public int? tablc_icod_tipo_analitica { get; set; }
        public string tarec_vdescripcion { get; set; }
        public string vdes_analisis { get; set; }
        public decimal mnto { get; set; }
        public int? mobac_inumero_orden { get; set; }
        public string anac_iid_analitica { get; set; }
        public string anac_vdescripcion { get; set; }

        public Int64 doxcc_icod_correlativo_ADC { get; set; }

        /**/
        public int MonedaDXC { get; set; }
        public int MonedaDXP { get; set; }

        public DateTime? mobdc_sfecha { get; set; }
        public int mobdc_icod_concepto_caja { get; set; }
        public string concepto_abreviatura { get; set; }

    }
}
