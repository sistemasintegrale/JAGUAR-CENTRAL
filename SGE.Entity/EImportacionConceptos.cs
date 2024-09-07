namespace SGE.Entity
{
    public class EImportacionConceptos : EAuditoria
    {
        public int? impd_icod_importacion_detalle { set; get; }
        public int? impc_icod_importacion { set; get; }
        public int? cpn_icod_concepto_nacional { set; get; }
        public int? cpnd_icod_detalle_nacional { set; get; }
        public decimal? impd_nmont_tot_concepto { set; get; }
        public decimal? impd_nmont_unit_concepto { set; get; }
        public int? tablc_iid_tipo_moneda_origen { set; get; }
        public decimal? impd_nmont_tot_concepto_origen { set; get; }
        public decimal? impd_nmont_tot_ejecut { set; get; }
        public decimal? impd_nmont_unit_ejecut { set; get; }
        public bool? impd_flag_estado { set; get; }

        //-----------------------------
        public string cpnd_vdescripcion { set; get; }
        public string cpn_vdescripcion_concepto_nacional { get; set; }
        public decimal? prepd_nmont_tot_concepto { set; get; }
        public decimal? prepd_nmont_unit_concepto { set; get; }
        public decimal? prepd_nmont_tot_concepto_origen { set; get; }
        public string TipoMoneda { set; get; }
        public string strCod { set; get; }
        //---------------------------------------
        public decimal? impd_nmonto_concepto_sol { set; get; }
        public decimal? impd_nmonto_concepto_dol { set; get; }

    }
}
