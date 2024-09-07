namespace SGE.Entity
{
    public class ESubAreaEmpresa : EAuditoria
    {
        public int aremd_iid_sub_area_empresa { get; set; }
        public int aremc_iid_codigo { get; set; }
        public int aremd_codigo { get; set; }
        public string aremd_vcodigo { get; set; }
        public string tarec_vabreviatura { get; set; }
        public string descripcion { get; set; }

        //PARA ENVIO DE INFORMACION

        public string strEstado { get; set; }
        public char Estado { get; set; }
    }
}
