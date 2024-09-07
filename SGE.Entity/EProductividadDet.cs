using System;

namespace SGE.Entity
{
    public class EProductividadDet
    {
        public int icod_personal { get; set; }
        public DateTime fecha { get; set; }
        public string strTipoDoc { get; set; }
        public string strNroDoc { get; set; }
        public string strNroOrden { get; set; }
        public string vehd_vplaca { get; set; }
        public string prdc_vcode_producto { get; set; }
        public string prdc_vdescripcion_larga { get; set; }
        public decimal dblCantidad { get; set; }
        public decimal dblPrecioUnitario { get; set; }
        public decimal dblMonto { get; set; }
    }
}
