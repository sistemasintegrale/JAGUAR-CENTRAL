using System;

namespace SGE.Entity
{
    public class ERequerimientoMateriales : EAuditoria
    {
        public int rqmc_icod_requerimiento_materiales { get; set; }
        public string rqmc_numero_req_material { get; set; }
        public DateTime rqmc_sfecha_req_material { get; set; }
        public int tablc_iid_situación_hc { get; set; }
        public int tablc_iid_tipo_requerimiento { get; set; }
        public int pryc_icod_proyecto { get; set; }
        public string rqmc_vdescripcion { get; set; }
        public bool rqmc_flag_estado { get; set; }
        public bool rqmc_bautorizado { get; set; }
        /*--------------------------------------------------*/
        public string Tipo { get; set; }
        public string Situacion { get; set; }
        public string CentroCostos { get; set; }
        public string DesProyecto { get; set; }
        public string NomCliente { get; set; }
        public string NumHojaCosto { get; set; }
        public int hjcc_icod_hoja_costo { get; set; }

    }
}
