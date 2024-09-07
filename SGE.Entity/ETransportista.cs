namespace SGE.Entity
{
    public class ETransportista : EAuditoria
    {
        public int tranc_icod_transportista { get; set; }
        public string tranc_iid_transportista { get; set; }
        public string tranc_vnombre_transportista { get; set; }
        public string tranc_vruc { get; set; }
        public string tranc_vdireccion { get; set; }
        public string tranc_vnumero_telefono { get; set; }
        public string tranc_vmicti { get; set; }
        public string tranc_cnumero_dni { get; set; }
        public string tranc_vnombre_conductor { get; set; }
        public string tranc_vnum_licencia_conducir { get; set; }
        public string tranc_vnum_placa { get; set; }
        public string tranc_vnum_matricula { get; set; }
        public int tranc_iid_situacion_transporte { get; set; }
        public string strSituacion { get; set; }
    }
}
