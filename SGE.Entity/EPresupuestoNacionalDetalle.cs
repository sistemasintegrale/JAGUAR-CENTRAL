using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EPresupuestoNacionalDetalle
    {
        [DataMember]
        public int prepd_icod_detalle { get; set; }
        [DataMember]
        public int prep_icod_presupuesto { get; set; }
        [DataMember]
        public int cpn_icod_concepto_nacional { get; set; }
        [DataMember]
        public int cpnd_icod_detalle_nacional { get; set; }
        [DataMember]
        public decimal prepd_nmont_tot_concepto { get; set; }
        [DataMember]
        public decimal prepd_nmont_unit_concepto { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda_origen { get; set; }
        [DataMember]
        public decimal prepd_nmont_tot_concepto_origen { get; set; }
        [DataMember]
        public decimal prepd_nmont_tot_ejecut { get; set; }
        [DataMember]
        public decimal prepd_nmont_unit_ejecut { get; set; }
        [DataMember]
        public int prepd_iusuario_crea { get; set; }
        [DataMember]
        public DateTime prepd_sfecha_crea { get; set; }
        [DataMember]
        public string prepd_vpc_crea { get; set; }
        [DataMember]
        public int prepd_iusuario_modifica { get; set; }
        [DataMember]
        public DateTime prepd_sfecha_modifica { get; set; }
        [DataMember]
        public string prepd_vpc_modifica { get; set; }
        [DataMember]
        public bool prepd_flag_estado { get; set; }
        [DataMember]
        public string cpnd_vdescripcion { get; set; }
        [DataMember]
        public string cpn_vdescripcion_concepto_nacional { get; set; }
        [DataMember]
        public string TipoMoneda { get; set; }
        [DataMember]
        public int TipOper { get; set; }
        [DataMember]
        public int ctacc_iid_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnumero_cuenta_contable { get; set; }
        [DataMember]
        public string ctacc_vnombre_descripcion_larga { get; set; }
        [DataMember]
        public int prepd_iid_anio { get; set; }
        [DataMember]
        public string strCod { get; set; }

        [DataMember]
        public decimal impd_nmonto_concepto_sol { get; set; }

        [DataMember]
        public decimal impd_nmonto_concepto_dol { get; set; }
    }
}
