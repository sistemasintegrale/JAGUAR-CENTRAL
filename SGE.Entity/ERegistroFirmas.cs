namespace SGE.Entity
{
    public class ERegistroFirmas : EAuditoria
    {
        public int regf_icod_registro_firmas { get; set; }
        public string regf_ocl_elavorado { get; set; }
        public string regf_ocl_autorizado { get; set; }
        public string regf_ocl_revisado { get; set; }
        public string regf_oci_aprobado1 { get; set; }
        public string regf_oci_aprobado2 { get; set; }
        public string regf_oci_aprobado3 { get; set; }
        public string regf_oci_aprobado4 { get; set; }
        public string regf_ocs_elavorado { get; set; }
        public string regf_ocs_revisado { get; set; }
    }
}
