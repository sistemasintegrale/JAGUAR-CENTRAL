using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EDocPorPagarDetalleNacional
    {
        [DataMember]
        public int doxpcd_icod_detalle_nacional { get; set; }
        [DataMember]
        public int proc_icod_proveedor { get; set; }
        [DataMember]
        public int tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int tdodc_iid_correlativo { get; set; }
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public int prep_iid_anio { get; set; }
        [DataMember]
        public int prep_viid_presupuesto_anio { get; set; }
        [DataMember]
        public string prep_viid_presupuesto { get; set; }
        [DataMember]
        public int prep_icod_presupuesto { get; set; }
        [DataMember]
        public int prepd_icod_detalle { get; set; }
        [DataMember]
        public string cimcc_iid_detalle_concepto { get; set; }
        [DataMember]
        public string prepd_vdescripcion_concepto { get; set; }
        [DataMember]
        public int pespc_icod_producto_especifico { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal doxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal doxpcd_nmonto_rubro { get; set; }
        [DataMember]
        public decimal? faccd_nmonto_unit { get; set; }
        [DataMember]
        public decimal faccd_ncant_producto { get; set; }
        [DataMember]
        public DateTime doxpcd_sfecha_pago { get; set; }
        [DataMember]
        public string doxpcd_glosa_descripcion { get; set; }
        [DataMember]
        public int doxpcd_iusuario_crea { get; set; }
        [DataMember]
        public DateTime doxpcd_sfecha_crea { get; set; }
        [DataMember]
        public string doxpcd_vpc_crea { get; set; }
        [DataMember]
        public int doxpcd_iusuario_modifica { get; set; }
        [DataMember]
        public DateTime doxpcd_sfecha_modifica { get; set; }
        [DataMember]
        public string doxpcd_vpc_modifica { get; set; }
        [DataMember]
        public bool doxpcd_flag_estado { get; set; }
        [DataMember]
        public string ctacc_vnumero_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnombre_descripcion_larga { get; set; }

        [DataMember]
        public string prep_cod_presupuesto { get; set; }
        [DataMember]
        public int cpnd_icod_detalle_nacional { get; set; }
        [DataMember]
        public string cpnd_vdescripcion { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string facc_num_doc { get; set; }
        [DataMember]
        public string TipoMoneda { get; set; }
        [DataMember]
        public decimal facc_nmonto_total_doc { get; set; }
        [DataMember]
        public string prodc_vdescripcion { get; set; }
        [DataMember]
        public decimal doxpcd_nmonto_rubro_temp { get; set; }
        [DataMember]
        public int TipOper { get; set; }

        [DataMember]
        public decimal prep_ncant_recibida { get; set; }
        [DataMember]
        public decimal prep_ncant_presupuesto { get; set; }
    }
}
