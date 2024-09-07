namespace SGE.Entity
{
    public class ESeries : EAuditoria
    {
        public int rs_icod_registro_serie { get; set; }
        public string rs_vserie { get; set; }
        public int rs_icorrelativo { get; set; }
        public int rs_icod_pvt { get; set; }
        public int rs_itipo_documento { get; set; }
        public string DesPVT { get; set; }
        public string strTipoDoc { get; set; }
    }
}
