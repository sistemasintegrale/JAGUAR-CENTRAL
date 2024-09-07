namespace SGE.Entity
{
    public class EAreaUbicacion : EAuditoria
    {
        public int arubd_iid_area_ubicacion { get; set; }
        public int arprc_iid_codigo { get; set; }
        public int arubd_codigo { get; set; }
        public string arubd_vcodigo { get; set; }
        public string tarec_vabreviatura { get; set; }
        public string descripcion { get; set; }

        //PARA ENVIO DE INFORMACION

        public string strEstado { get; set; }
        public char Estado { get; set; }
    }
}
