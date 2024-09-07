using System;

namespace SGE.Entity
{
    public class EEmpaquePlantillaDet : EAuditoria
    {
        public int plemd_iid { get; set; }
        public int plemd_iitem { get; set; }
        public string plemd_vitem { get; set; }
        public int prdc_icod_producto { get; set; }
        public decimal plemd_ncantidad { get; set; }
        public Boolean plemd_flag_estado { get; set; }
        public int intTipoOperacion { get; set; }
    }
}
