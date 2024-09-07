using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EComprobanteDetalle
    {
        [DataMember]
        public int iid_det_correlat { get; set; }
        [DataMember]
        public int iid_voucher_contable { get; set; }
        [DataMember]
        public int? nro_item_det { get; set; }
        [DataMember]
        public int icod_tipo_doc { get; set; }
        [DataMember]
        public string vdescripcion_tipo_doc { get; set; }
        [DataMember]
        public string tipo_numero_documento { get; set; }
        [DataMember]
        public string vnumero_documento { get; set; }
        [DataMember]
        public int iid_cuenta_contable { get; set; }
        [DataMember]
        public string viid_cuenta_contable { get; set; }
        [DataMember]
        public string vdescripcion_cuenta_contable { get; set; }
        [DataMember]
        public int? icod_centro_costo { get; set; }
        [DataMember]
        public string vicod_centro_costo { get; set; }
        [DataMember]
        public string vcode_centro_costo { get; set; }
        [DataMember]
        public int? iid_tipo_relacion { get; set; }
        [DataMember]
        public int? iid_relacion { get; set; }
        [DataMember]
        public string viid_tipo_relacion { get; set; }
        [DataMember]
        public string viid_relacion { get; set; }
        [DataMember]
        public string viid_relacion_descrip { get; set; }
        [DataMember]
        public string vglosa_linea { get; set; }
        [DataMember]
        public decimal? nmto_tot_debe_sol { get; set; }
        [DataMember]
        public decimal? nmto_tot_haber_sol { get; set; }
        [DataMember]
        public decimal? nmto_tot_debe_dol { get; set; }
        [DataMember]
        public decimal? nmto_tot_haber_dol { get; set; }
        [DataMember]
        public char? corigen_registro_item { get; set; }
        [DataMember]
        public int? iusuario_crea { get; set; }
        [DataMember]
        public int? iusuario_modifica { get; set; }
        [DataMember]
        public string vpc_crea { get; set; }
        [DataMember]
        public string vpc_modifica { get; set; }
        [DataMember]
        public char? cestado { get; set; }
        [DataMember]
        public int operacion { get; set; }
        [DataMember]
        public int? iid_cautomatica_debe { get; set; }
        [DataMember]
        public int? iid_cautomatica_haber { get; set; }
        [DataMember]
        public int id_llave { get; set; }
        [DataMember]
        public string tipocambio { get; set; }
        [DataMember]
        public int? ctacc_iid_cuenta_contable_ref { get; set; }
        [DataMember]
        public string ctacc_vnombre_descripcion_larga { get; set; }
        [DataMember]
        public string tbl_origen { get; set; }
        /**/
        public DateTime? fec_cab { get; set; }/**/
        public string mon_cab { get; set; }/**/
        public bool detFlag { get; set; }

        public string cta_padre { get; set; }
        public string cta_vdespadre { get; set; }
        public int? id_mes { get; set; }
        public int? id_subdiario { get; set; }
        public string vid_subdiario { get; set; }
        /**/

        /// <summary>
        /// <Variables para uso de los reportes de contabilidad>
        /// </summary>

        public decimal? ctacc_iid_cuenta_contable_acumulado_sol { get; set; }
        public decimal? ctacc_iid_cuenta_contable_acumulado_dol { get; set; }

        public decimal? ctacc_iid_cuenta_contable_saldo_sol { get; set; }
        public decimal? ctacc_iid_cuenta_contable_saldo_dol { get; set; }
        public Boolean check_flag { get; set; }

        public decimal vrate_tipo_cambio { get; set; }

        public string iid_subdiario_vnum_voucher { get; set; }/**/
        public string vdes_Analisis { get; set; }
        public string cecoc_vdescripcion { get; set; }
        public string anac_vdescripcion { get; set; }
        public string anac_cecoc_vdescripcion { get; set; }
        public string anac_cecoc_tipo { get; set; }
        public int det_tipo { get; set; }
        public string anac_cecoc_code { get; set; }

        public decimal? acu_nmto_tot_debe_sol { get; set; }
        public decimal? acu_nmto_tot_haber_sol { get; set; }
        public decimal? acu_nmto_tot_debe_dol { get; set; }
        public decimal? acu_nmto_tot_haber_dol { get; set; }


        public decimal? cuenta_iid_cuenta_contable_acumulado_sol { get; set; }
        public decimal? cuenta_iid_cuenta_contable_acumulado_dol { get; set; }

        public decimal? cuenta_iid_cuenta_contable_saldo_sol { get; set; }
        public decimal? cuenta_iid_cuenta_contable_saldo_dol { get; set; }



        //
        public decimal? totgen_iid_cuenta_contable_acumulado_sol { get; set; }
        public decimal? totgen_iid_cuenta_contable_acumulado_dol { get; set; }

        public decimal? totgen_iid_cuenta_contable_saldo_sol { get; set; }
        public decimal? totgen_iid_cuenta_contable_saldo_dol { get; set; }



        public int? vcod_centro_costo { get; set; }

        // CAMPOS PARA EL REPORTE DE RESUMEN DE MOVIMIENTOS (MAYOR AUXILIAR ACUMULADO)
        public decimal? subdi_vent_nmto_tot_debe_sol { get; set; }
        public decimal? subdi_vent_nmto_tot_haber_sol { get; set; }
        public decimal? subdi_vent_nmto_saldo { get; set; }

        public decimal? subdi_comp_nmto_tot_debe_sol { get; set; }
        public decimal? subdi_comp_nmto_tot_haber_sol { get; set; }
        public decimal? subdi_comp_nmto_saldo { get; set; }

        public decimal? subdi_banc_nmto_tot_debe_sol { get; set; }
        public decimal? subdi_banc_nmto_tot_haber_sol { get; set; }
        public decimal? subdi_banc_nmto_saldo { get; set; }

    }
}
