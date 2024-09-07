using System;

namespace SGE.Entity
{
    public class ERecepcionComprasSuministros : EAuditoria
    {
        public int rcsc_icod_rcs { get; set; }
        public string rcsc_vnumero_rcs { get; set; }
        public DateTime rcsc_sfecha_rcs { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int ococ_icod_orden_compra { get; set; }
        public int almac_icod_almacen { get; set; }
        public int tablc_iid_motivo { get; set; }
        public int tablc_iid_situacion_rcs { get; set; }
        public DateTime rcsc_sfecha_ingreso { get; set; }
        public decimal rcsc_ncantidad { get; set; }
        public bool rcsc_flag_estatdo { get; set; }
        /*-------------------------------------------------------*/
        public string NomProveedor { get; set; }
        public string NumOC { get; set; }
        public string DesAlmacen { get; set; }
        public string Motivo { get; set; }
        public string Situacion { get; set; }
        public bool flag_multiple { get; set; }
    }
}
