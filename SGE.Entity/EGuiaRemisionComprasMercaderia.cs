using System;

namespace SGE.Entity
{
    public class EGuiaRemisionComprasMercaderia : EAuditoria
    {
        public int grcc_icod_grc { get; set; }
        public string grcc_vnumero_grc { get; set; }
        public DateTime grcc_sfecha_grc { get; set; }
        public int proc_icod_proveedor { get; set; }
        public int ococ_icod_orden_compra { get; set; }
        public int almac_icod_almacen { get; set; }
        public int tablc_iid_motivo { get; set; }
        public int tablc_iid_situacion_grc { get; set; }
        public DateTime grcc_sfecha_ingreso { get; set; }
        public decimal grcc_ncantidad { get; set; }
        public bool grcc_flag_estatdo { get; set; }
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
