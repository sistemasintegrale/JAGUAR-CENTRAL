using System;

namespace SGE.Entity
{
    public class EEmpaquePlantilla : EEmpaquePlantillaDet
    {
        public int plemc_iid { get; set; }
        public int? plemc_icod { get; set; }
        public string plemc_vcod { get; set; }
        public DateTime? plemc_sfecha { get; set; }
        public string plemc_vobservacion { get; set; }
        public int? prdc_icod_producto { get; set; }
        public string prdc_vcode_producto { get; set; }
        public string prdc_vdescripcion_larga { get; set; }
        public string unidc_vabreviatura_unidad_medida { get; set; }
    }
}
