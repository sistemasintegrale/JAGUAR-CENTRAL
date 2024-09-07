using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EDxPDatosAdicionales
    {
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public int? doxpc_tipo_compra { get; set; }
        [DataMember]
        public int? doxpc_tipo_comprobante { get; set; }
        [DataMember]
        public int? proc_tipo_persona { get; set; }
        [DataMember]
        public int? doxpc_tipo_documento { get; set; }
        [DataMember]
        public int? doxpc_tipo_destino { get; set; }
        [DataMember]
        public int? doxpc_ind_detraccion { get; set; }
        [DataMember]
        public string doxpc_cod_tasa_detrac { get; set; }
        [DataMember]
        public int? doxpc_ind_retencion { get; set; }
        [DataMember]
        public string doxpc_num_serie { get; set; }
        [DataMember]
        public string doxpc_cod_aduana { get; set; }
        [DataMember]
        public int? doxpc_anio_aduana { get; set; }
        [DataMember]
        public string doxpc_corre_aduana { get; set; }
        [DataMember]
        public string doxpc_num_doc_domiciliado { get; set; }
        [DataMember]
        public string doxpc_num_serie_referencia { get; set; }
        [DataMember]
        public string doxpc_num_comprobante_referencia { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_emision_referencia { get; set; }
        [DataMember]
        public string tipo_compra_descripcion { get; set; }
        [DataMember]
        public string tipo_comprobante_codigo { get; set; }
        [DataMember]
        public string tipo_comprobante_descripcion { get; set; }
        [DataMember]
        public string tipo_persona_codigo { get; set; }
        [DataMember]
        public string tipo_persona_descripcion { get; set; }
        [DataMember]
        public string tipo_documento_codigo { get; set; }
        [DataMember]
        public string tipo_documento_descripcion { get; set; }
        [DataMember]
        public string tipo_destino_descripcion { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public string proc_vnombre { get; set; }
        [DataMember]
        public string proc_vpaterno { get; set; }
        [DataMember]
        public string proc_vmaterno { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public int? doxpc_tipo_comprobante_referencia { get; set; }
        [DataMember]
        public string tipo_comprobante_codigo_ref { get; set; }
        [DataMember]
        public string tipo_comprobante_descripcion_ref { get; set; }
        [DataMember]
        public string doxpc_vnro_doc_referencia { get; set; }
        [DataMember]
        public int? doxpc_numdoc_tipo { get; set; }
        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_gravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_base_impon_ivap { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_isc { get; set; }
        [DataMember]
        public decimal? monto_igv { get; set; }
        [DataMember]
        public decimal? monto_otros { get; set; }
        [DataMember]
        public int? doxpc_iid_tipo_doc_referencia { get; set; }
        [DataMember]
        public int? condicion { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_referencial_cif { get; set; }
        [DataMember]
        public decimal? doxpc_nporcentaje_igv { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }
        [DataMember]
        public int? doxpc_numero_destino { get; set; }
        [DataMember]
        public string doxpc_vnro_deposito_detraccion { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
    }
}
