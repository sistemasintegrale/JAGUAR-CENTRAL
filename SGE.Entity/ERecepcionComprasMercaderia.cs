using System;

namespace SGE.Entity
{
    public class ERecepcionComprasMercaderia : EAuditoria
    {
        public int rcmc_icod_rcm { get; set; }
        public string rcmc_vnumero_rcm { get; set; }
        public DateTime rcmc_sfecha_rcm { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int ococ_icod_orden_compra { get; set; }
        public int almac_icod_almacen { get; set; }
        public int tablc_iid_motivo { get; set; }
        public int tablc_iid_situacion_rcm { get; set; }
        public DateTime rcmc_sfecha_ingreso { get; set; }
        public decimal rcmc_ncantidad { get; set; }
        public bool rcmc_flag_estatdo { get; set; }
        /*-------------------------------------------------------*/
        public string NomProveedor { get; set; }
        public string NumOC { get; set; }
        public string DesAlmacen { get; set; }
        public string Motivo { get; set; }
        public string Situacion { get; set; }
        public string NumFac { get; set; }

        public bool flag_multiple { get; set; }
    }
}
