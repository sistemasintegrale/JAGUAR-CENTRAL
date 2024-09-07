namespace SGE.Entity
{
    public class EVendedor : EAuditoria
    {
        public int vdrsc_icod_vendedor { get; set; }
        public string vdrsc_vcod_vendedor { get; set; }
        public string vdrsc_vnombre_vendedor { get; set; }
        public string vdrsc_vdireccion { get; set; }
        public string vdrsc_vnumero_telefono { get; set; }
        public string vdrsc_vnumero_dni { get; set; }
        public string vdrsc_vcorreo { get; set; }
        public int tablc_iid_tipo_personal { get; set; }
        public bool vdrsc_iactivo { get; set; }
    }
}
