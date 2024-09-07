using System;

namespace SGE.Entity
{
    public class ETransferenciaAlmacen : EAuditoria
    {
        public int trfc_icod_transf { get; set; }
        public int trfc_inum_transf { get; set; }
        public DateTime trfc_sfecha_transf { get; set; }
        public int almac_icod_alm_sal { get; set; }
        public int almac_icod_alm_ing { get; set; }
        public int? trnfc_iid_motivo { get; set; }
        public string trnfc_vobservaciones { get; set; }

        public string strAlmacenSal { get; set; }
        public string strAlmacenIng { get; set; }
        public string strMotivo { get; set; }
    }
}
