namespace SGE.Entity
{
    public class EProdTabla
    {
        public int IdTipoTabla { get; set; }
        public string vidTipoTabla { get; set; }
        public int? tarec_icorrelativo_registro { get; set; }
        public string Descripcion { get; set; }
        public char Estado { get; set; }
        public string vestado { get; set; }
        public byte IndicaModificar { get; set; }
        public int IndicaRegistar { get; set; }
        public int IndicaEliminar { get; set; }
        public string EstadoNombre { get; set; }

        //
        public int tablc_iid_tipo_tabla { get; set; }
        public string tablc_vdescripcion { get; set; }
        public char tablc_cestado { get; set; }
        public string strEstado { get; set; }

    }
}
