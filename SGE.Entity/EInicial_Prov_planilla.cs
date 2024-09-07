namespace SGE.Entity
{
    public class EInicial_Prov_planilla : EAuditoria
    {
        public int ippc_icod_inicial_provision_planilla { set; get; }
        public string ippc_iid_inicial_provision_planilla { set; get; }
        public string ippc_vdescripcion { set; get; }
        public bool? ippc_sflag_estado { set; get; }
    }
}
