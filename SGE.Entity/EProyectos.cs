using System;

namespace SGE.Entity
{
    public class EProyectos : EAuditoria
    {
        public int pryc_icod_proyecto { get; set; }
        public int pryc_ianio { get; set; }
        public string pryc_vcorrelativo { get; set; }
        public string pryc_vdescripcion { get; set; }
        public int pryc_icod_cliente { get; set; }
        public int almac_icod_almacen { get; set; }
        public string pryc_vdireccion_prov { get; set; }
        public int pryc_icod_ccosto { get; set; }
        public DateTime pryc_sfecha_inicio { get; set; }
        public DateTime pryc_sfecha_emtrega { get; set; }
        public int pryc_icod_acta_entrega { get; set; }
        public decimal pryc_nrentabilidad { get; set; }
        public int pryc_iestado { get; set; }
        public bool pryc_flag_estado { get; set; }

        public string NomCliente { get; set; }
        public string CentroCosto { get; set; }
        public string strActaEntrega { get; set; }
        public string strEstado { get; set; }
        public string StrAlmacen { get; set; }
        public string StrAlmacenDir { get; set; }
        public string RUC { get; set; }
        public string NumHojaCosto { get; set; }
        public int hjcc_icod_hoja_costo { get; set; }
        public int almac_icod_almacen_2 { get; set; }
    }
}
