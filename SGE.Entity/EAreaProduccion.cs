namespace SGE.Entity
{
    public class EAreaProduccion : EAuditoria
    {


        //
        public int arprc_iid_codigo { get; set; }
        public string arprc_vdescripcion { get; set; }
        public string arprc_vabreviatura { get; set; }
        public char arprc_cestado { get; set; }
        public string strEstado { get; set; }
        public int? arprc_icolor { get; set; }
    }
}
