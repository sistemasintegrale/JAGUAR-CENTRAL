using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class ECliente
    {
        [DataMember]
        public int cliec_icod_cliente { get; set; }
        [DataMember]
        public int giroc_icod_giro { get; set; }
        [DataMember]
        public string giroc_vnombre_giro { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre_comercial { get; set; }
        [DataMember]
        public string cliec_vdireccion_cliente { get; set; }
        [DataMember]
        public int? ubicc_icod_ubicacion { get; set; }
        [DataMember]
        public string ubicc_vnombre_ubicacion { get; set; }
        [DataMember]
        public string cliec_vnro_telefono { get; set; }
        [DataMember]
        public string cliec_vnro_fax { get; set; }
        [DataMember]
        public string cliec_vnro_celular { get; set; }
        [DataMember]
        public int? tabl_iid_tipo_documento { get; set; }
        [DataMember]
        public string TipoDoc { get; set; }
        [DataMember]
        public string cliec_vnumero_doc_cli { get; set; }
        [DataMember]
        public string cliec_vnombre_contacto { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_relacion_cli { get; set; }
        [DataMember]
        public string EmpresaRelacion { get; set; }
        [DataMember]
        public int? vendc_icod_vendedor { get; set; }
        [DataMember]
        public string vendc_vnombre_vendedor { get; set; }
        [DataMember]
        public string vendc_iid_vendedor { get; set; }
        [DataMember]
        public DateTime? cliec_sfecha_registro_cliente { get; set; }
        [DataMember]
        public int cliec_iid_situacion_cliente { get; set; }
        [DataMember]
        public string Situacion { get; set; }
        [DataMember]
        public int usuario { get; set; }
        [DataMember]
        public string pc { get; set; }
        [DataMember]
        public string cliec_vcorreo_electronico { get; set; }
        [DataMember]
        public string cliec_vapellido_paterno { get; set; }
        [DataMember]
        public string cliec_vapellido_materno { get; set; }
        [DataMember]
        public string cliec_vnombres { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_cliente { get; set; }
        [DataMember]
        public string cliec_cruc { get; set; }
        [DataMember]
        public bool cambiar { get; set; }
        [DataMember]
        public int? cliec_icod_autoventa { get; set; }
        [DataMember]
        public string cliec_vcod_cliente { get; set; }
        [DataMember]
        public Boolean? cliec_bcredito { get; set; }
        [DataMember]
        public int? cliec_nnumero_dias { get; set; }
        [DataMember]
        public int? anac_icod_analitica { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_saldo_soles { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_saldo_dolares { get; set; }
        [DataMember]
        public int? pcomc_icod_pcompra { get; set; }
        [DataMember]
        public string anac_iid_analitica { get; set; }
        [DataMember]
        public int? tarec_icorrelativo { get; set; }

        //***REPORTE**//
        [DataMember]
        public decimal? MontoUS { get; set; }
        [DataMember]
        public decimal? dias_0_30 { get; set; }
        [DataMember]
        public decimal? dias_31_60 { get; set; }
        [DataMember]
        public decimal? dias_61_90 { get; set; }
        [DataMember]
        public decimal? dias_91_120 { get; set; }
        [DataMember]
        public decimal? dias_121_180 { get; set; }
        [DataMember]
        public decimal? dias_181_mas { get; set; }
        public string cliec_vubigeo { get; set; }
        public bool cliec_bagente_retenedor { get; set; }
    }
}
