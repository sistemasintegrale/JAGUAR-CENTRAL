using System;

namespace SGE.Entity
{
    public class EProdUnidadMedida : EAuditoria
    {
        public int id_unidad_medida { get; set; }
        public string idf_unidad_medida { get; set; }
        public string abreviatura_unidad_medida { get; set; }
        public string descripcion { get; set; }
        public string unidc_vcodigo_sunat { get; set; }
        public int? usuario_crea { get; set; }
        public int? usuario_modifica { get; set; }
        public DateTime? fecha_crea { get; set; }
        public DateTime? fecha_modifica { get; set; }

        //para tranasferencia
        public int unidc_icod_unidad_medida { get; set; }
        public int unidc_iid_unidad_medida { get; set; }
        public string unidc_vabreviatura_unidad_medida { get; set; }
        public string unidc_vdescripcion { get; set; }
        public int unidc_iactivo { get; set; }
    }
}
