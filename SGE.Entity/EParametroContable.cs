using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EParametroContable : EAuditoria
    {
        public int parac_iid_parametro { get; set; }
        public string parac_vnombre { get; set; }
        public string parac_vdescripcion { get; set; }
        public decimal? parac_nvalor_numerico { get; set; }
        public string parac_vvalor_texto { get; set; }
        public char? parac_cflag_visible { get; set; }
        public int? parac_id_sd_ingbco { get; set; }
        public int? parac_id_sd_egrbco { get; set; }
        public int? parac_id_sd_cjachic { get; set; }
        public int? parac_id_sd_cosvta { get; set; }
        public int? parac_id_sd_docxpag { get; set; }
        public int? parac_id_sd_docxcob { get; set; }
        public int? parac_id_sd_apert { get; set; }
        public int? parac_id_sd_cieanual { get; set; }
        public int? parac_id_cta_gdifc_mn { get; set; }
        public int? parac_id_cta_gdifc_me { get; set; }
        public int? parac_id_cta_pdifc_mn { get; set; }
        public int? parac_id_cta_pdifc_me { get; set; }
        public int? parac_id_sd_difc_mn { get; set; }
        public int? parac_id_sd_difc_me { get; set; }
        public string parac_nro_comp_difc_mn { get; set; }
        public string parac_nro_comp_difc_me { get; set; }
        public int? parac_id_ccosto_difc { get; set; }
        public int? parac_id_cta_gredo_mn { get; set; }
        public int? parac_id_cta_gredo_me { get; set; }
        public int? parac_id_cta_predo_mn { get; set; }
        public int? parac_id_cta_predo_me { get; set; }
        public int? parac_id_cta_retencion { get; set; }
        public int? parac_id_cta_planilla { get; set; }
        public int? parac_id_ccos_base { get; set; }
        public int tablc_iid_modulo { get; set; }
        public char? parac_cestado { get; set; }
        public string parac_vmascara { get; set; }
        public int? ctacc_icod_cta_ctbl_serv_propio { get; set; }
        public int? ctacc_icod_cta_ctbl_serv_externo { get; set; }
        /*------------------------------------------------------------------*/
        public string strCodeCCostoDifCambio { get; set; }
        public string strDesCCostoDifCambio { get; set; }
        public string strCodeCCostoBase { get; set; }
        public string strDesCCostoBase { get; set; }

        public string strCodCuentaServPropio { get; set; }
        public string strDesCuentaServPropio { get; set; }
        public string strCodCuentaServExterno { get; set; }
        public string strDesCuentaServExterno { get; set; }
        public decimal Porcentaje_de_Retencion { get; set; }

        /*4ta Categoria*/
        public int? parac_id_cta_4ta_cat { get; set; }
        public string strCod4taCategoria { get; set; }
        public decimal parac_Porcentaje_Retencion_ventas { get; set; }
        public int parac_id_sd_planillas { get; set; }
    }
}
