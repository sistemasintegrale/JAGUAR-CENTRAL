namespace SGE.Entity
{
    public class EAreaDescripcion : EAuditoria
    {
        public int arded_iid_area_descripcion { get; set; }
        public int arprc_iid_codigo { get; set; }
        public int arded_codigo { get; set; }
        public string arded_vcodigo { get; set; }
        public string tarec_vabreviatura { get; set; }
        public string descripcion { get; set; }

        //PARA ENVIO DE INFORMACION
        public string strEstado { get; set; }
        public char Estado { get; set; }
    }
}
