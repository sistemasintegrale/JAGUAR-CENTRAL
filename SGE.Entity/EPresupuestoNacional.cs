using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EPresupuestoNacional
    {
        [DataMember]
        public int prep_icod_presupuesto { get; set; }
        [DataMember]
        public int prep_iid_anio { get; set; }
        [DataMember]
        public string prep_cod_presupuesto { get; set; }
        [DataMember]
        public DateTime prep_sfecha_presupuesto { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public int pespc_icod_producto_especifico { get; set; }
        [DataMember]
        public string prep_vconcepto { get; set; }
        [DataMember]
        public int almac_icod_almacen { get; set; }
        [DataMember]
        public decimal prep_ncant_presupuesto { get; set; }
        [DataMember]
        public decimal prep_ncant_recibida { get; set; }
        [DataMember]
        public decimal prep_ncant_facturada { get; set; }
        [DataMember]
        public decimal prep_ntipo_cambio { get; set; }
        [DataMember]
        public int cecoc_icod_centro_costo { get; set; }
        [DataMember]
        public decimal prep_nmont_total { get; set; }
        [DataMember]
        public decimal prep_nmont_unit_prod { get; set; }
        [DataMember]
        public decimal prep_nmont_tot_ejecut { get; set; }
        [DataMember]
        public decimal prep_nmont_unit_ejecut { get; set; }
        [DataMember]
        public DateTime? prep_sfecha_cierre { get; set; }
        [DataMember]
        public int prep_tipo_nacional { get; set; }
        [DataMember]
        public int prep_isituacion { get; set; }
        [DataMember]
        public string SituacionDoc { get; set; }
        [DataMember]
        public long krdx_icod_kardex { get; set; }
        [DataMember]
        public DateTime? krdx_sfecha_kardex { get; set; }
        [DataMember]
        public int prep_iusuario_crea { get; set; }
        [DataMember]
        public DateTime prep_sfecha_crea { get; set; }
        [DataMember]
        public string prep_vpc_crea { get; set; }
        [DataMember]
        public int prep_iusuario_modifica { get; set; }
        [DataMember]
        public DateTime prep_sfecha_modifica { get; set; }
        [DataMember]
        public string prep_vpc_modifica { get; set; }
        [DataMember]
        public bool prep_flag_estado { get; set; }
        [DataMember]
        public int pespc_iid_producto_generico { get; set; }
        [DataMember]
        public string TipoMoneda { get; set; }
        [DataMember]
        public string unidc_vabreviatura_unidad_medida { get; set; }
        [DataMember]
        public string situc_vdescripcion { get; set; }
        [DataMember]
        public string almac_vdescripcion { get; set; }
        [DataMember]
        public string TipoPresupuestoNacional { get; set; }
        [DataMember]
        public decimal CantFacturada { get; set; }
        [DataMember]
        public string prep_vreferencia { get; set; }

        [DataMember]
        public int agm_icod_maritimo { get; set; }
        [DataMember]
        public int add_icod_aduana { get; set; }
        [DataMember]
        public int mnv_icod_motonave { get; set; }
        [DataMember]
        public string mnv_vdescripcion { get; set; }

        [DataMember]
        public DateTime? prep_fecha_llegada { get; set; }
        [DataMember]
        public decimal? prep_npeso_total { get; set; }
        [DataMember]
        public decimal? prep_nvolumen_m3 { get; set; }
        [DataMember]
        public decimal? prep_ncantidad_bultos { get; set; }
    }
}
