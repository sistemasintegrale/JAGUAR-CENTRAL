namespace SGE.Entity
{
    public class EInicial_Prov_planilla_Det : EAuditoria
    {
        public int ippd_icod_inicial_provision_planilla_detalle { set; get; }
        public int? ippc_icod_inicial_provision_planilla { set; get; }
        public string ippd_iid_inicial_provision_planilla_detalle { set; get; }
        public int? perc_icod_personal { set; get; }
        public decimal? ippd_ninicial { set; get; }
        public bool? ippd_sflag_estado { set; get; }

        public string strIdPersonal { set; get; }
        public string strNomyApell { set; get; }
    }
}
