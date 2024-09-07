using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ERegistroVentas
    {
        [DataMember]
        public long doxcc_icod_correlativo { get; set; }
        [DataMember]
        public string tdocc_vcodigo_tipo_doc_sunat { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_doc { get; set; }
        [DataMember]
        public long? cliec_icod_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string simboloMoneda { get; set; }
        public string cliec_vcod_cliente { get; set; }

        //montos
        [DataMember]
        public decimal? doxcc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal? doxcc_nporcentaje_igv { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_afecto { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_inafecto { get; set; }
        [DataMember]
        public decimal? valor_venta { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_impuesto { get; set; }

        [DataMember]
        public decimal? doxcc_nmonto_total { get; set; }
        [DataMember]
        public decimal? valor_fact { get; set; }
        [DataMember]
        public decimal? valor_ex { get; set; }
        [DataMember]
        public decimal? base_imp_ivap { get; set; }
        //montos

        //variables para el reporte
        [DataMember]
        public string str_doxcc_icod_correlativo { get; set; }
        [DataMember]
        public string str_doxcc_sfecha_doc { get; set; }
        [DataMember]
        public string str_doxcc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public string tip_doc_cliente { get; set; }
        [DataMember]
        public string num_doc_cliente { get; set; }

        //montos reporte
        [DataMember]
        public string rpt_tipo_cambio { get; set; }
        [DataMember]
        public string rpt_biog { get; set; } //base imponible operación gravada
        [DataMember]
        public string rpt_valor_ex { get; set; } //valor exonerado
        [DataMember]
        public string rpt_valor_fact { get; set; } //valor factoring

        [DataMember]
        public string rpt_ivap { get; set; } //monto ivap
        [DataMember]
        public string rpt_valor_venta { get; set; }
        [DataMember]
        public string rpt_monto_igv { get; set; }

        [DataMember]
        public string rpt_precio_venta { get; set; }

        [DataMember]
        public int? doxcc_tipo_comprobante_referencia { get; set; }
        [DataMember]
        public string doxcc_num_serie_referencia { get; set; }
        [DataMember]
        public string doxcc_num_comprobante_referencia { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_emision_referencia { get; set; }
        [DataMember]
        public string Coareferencia { get; set; }

        //otros
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public int? tablc_iid_situacion_documento { get; set; }
        [DataMember]
        public string tdocc_vdescripcion { get; set; }

        [DataMember]
        public string cliec_vnombre_cliente_2 { get; set; }
        [DataMember]
        public string tip_doc_cliente_2 { get; set; }
        [DataMember]
        public string num_doc_cliente_2 { get; set; }
        public string nc_dxc_tipodoc { get; set; }
        public string CUO { get; set; }
        [DataMember]
        public string vcocc_numero_vcontable { get; set; }
        [DataMember]
        public int subdi_icod_subdiario { get; set; }
    }
}
