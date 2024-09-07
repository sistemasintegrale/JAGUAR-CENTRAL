using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EComprobante
    {
        [DataMember]
        public int iid_anio { get; set; }
        [DataMember]
        public int iid_mes { get; set; }
        [DataMember]
        public string vnombre_mes { get; set; }
        [DataMember]
        public int iid_dia { get; set; }
        [DataMember]
        public int iid_voucher_contable { get; set; }
        [DataMember]
        public int subdi_icod_subdiario { get; set; }
        [DataMember]
        public DateTime sfec_nota_contable { get; set; }
        [DataMember]
        public string vglosa { get; set; }
        [DataMember]
        public string vobservacion { get; set; }
        [DataMember]
        public string vnumero_voucher_contable { get; set; }
        [DataMember]
        public int? iid_situacion_voucher_contable { get; set; }
        [DataMember]
        public string vsituacion_voucher { get; set; }
        [DataMember]
        public string tipocambio { get; set; }
        [DataMember]
        public int? iid_origen_voucher_contable { get; set; }
        [DataMember]
        public int? tablc_iid_moneda { get; set; }
        [DataMember]
        public string vmoneda { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_cambio { get; set; }
        [DataMember]
        public int? ticac_iid_tipo_cambio { get; set; }
        [DataMember]
        public decimal? nmto_tot_debe_sol { get; set; }
        [DataMember]
        public decimal? nmto_tot_haber_sol { get; set; }
        [DataMember]
        public decimal? nmto_tot_debe_dol { get; set; }
        [DataMember]
        public decimal? nmto_tot_haber_dol { get; set; }
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
        public int Movimiento { get; set; }
        [DataMember]
        public int idf_SubDiario { get; set; }
        [DataMember]
        public int? vcocc_difcambio { get; set; }
        //
        public string iid_subdiario_vnum_voucher { get; set; }

        public string subdiario_des { get; set; }
        public int alvisoft { get; set; }
        public string tbl_origen { get; set; }
        public int? tbl_reg_icod { get; set; }
        public decimal decimal_tipoCambio { get; set; }
        public string strNroVco { get; set; }


        public int? vcocd_nro_item_det { get; set; }

        public string tdocc_coa { get; set; }
        public int? ctacc_icod_cuenta_contable { get; set; }
        public string vcocd_numero_doc { get; set; }
        /*txt*/
        public string vcocd_numero_doc__show { get; set; }
        public string vcocd_moneda { get; set; }
        public string vcocd_AnioDocu { get; set; }
        public string vcocd_formato_txt { get; set; }
        public string vcocd_Vperido_fech { get; set; }

        public int anad_icod_analitica { get; set; }
        public string anad_iid_analitica { get; set; }
        public int tarec_icorrelativo_tipo_analitica { get; set; }
        public string doxpc_codigo_aduana { get; set; }




    }
}
