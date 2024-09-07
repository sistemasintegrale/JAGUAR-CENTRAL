namespace SGE.Entity
{
    public class EAreaEmpresa : EAuditoria
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
        public int aremc_iid_codigo { get; set; }
        public string aremc_vdescripcion { get; set; }
        public string aremc_vabreviatura { get; set; }
        public char aremc_cestado { get; set; }
        public string strEstado { get; set; }
    }
}
