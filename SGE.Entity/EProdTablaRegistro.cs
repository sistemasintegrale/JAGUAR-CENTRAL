using System;

namespace SGE.Entity
{
    public class EProdTablaRegistro : EAuditoria
    {
        public int icod_tabla { get; set; }
        public int iid_tipo_tabla { get; set; }
        public int tarec_iid_correlativo { get; set; }
        public string tarec_viid_correlativo { get; set; }
        public string tarec_vabreviatura { get; set; }
        public string descripcion { get; set; }

        //PARA ENVIO DE INFORMACION
        public int tarec_iid_tabla_registro { get; set; }
        public int? tablc_iid_tipo_tabla { get; set; }
        public int? tarec_icorrelativo_registro { get; set; }
        public string tarec_vdescripcion { get; set; }
        public decimal? tarec_nvalor_numerico { get; set; }
        public string tarec_vvalor_texto { get; set; }
        public char tarec_cestado { get; set; }
        public int? tarec_iusuario_crea { get; set; }
        public int? tarec_iusuario_modifica { get; set; }
        public DateTime? tarec_sfecha_crea { get; set; }
        public DateTime? tarec_sfecha_modifica { get; set; }
        public string strEstado { get; set; }
        public char Estado { get; set; }
        public string estado { get { return Estado == 'A' ? "Activo" : "Inactivo"; } }
    }
}
