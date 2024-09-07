using System;

namespace SGE.Entity
{
    public class ENotaCreditoProveedorDet : EAuditoria
    {
        public int ncpd_icod_detalle { get; set; }
        public int ncpc_icod_nota_cred { get; set; }
        public int ncpd_iitem { get; set; }
        public int prd_icod_producto { get; set; }
        public int prd_iid_clasificacion_prod { get; set; }
        public decimal ncpd_ncantidad { get; set; }
        public decimal ncpd_nmonto_unit { get; set; }
        public decimal ncpd_nmonto_total { get; set; }
        public Int64? ncpd_icod_kardex { get; set; }
        public string ncpd_vdescripcion_item { get; set; }
        /**/
        public string strCodProducto { get; set; }
        public string strDesUM { get; set; }
        public string strMoneda { get; set; }
        public string strLinea { get; set; }
        public string strCodLinea { get; set; }
        public string strSubLinea { get; set; }
        public string strClasificacion { get; set; }
        public int intTipoOperacion { get; set; }
        public int intCtaContable { get; set; }
        public bool flagCCosto { get; set; }
        public int intTipoAnalitica { get; set; }
        public int intAnaliticaProveedor { get; set; }


    }
}
