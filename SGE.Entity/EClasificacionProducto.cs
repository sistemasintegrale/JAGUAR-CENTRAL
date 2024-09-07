namespace SGE.Entity
{
    public class EClasificacionProducto : EAuditoria
    {
        public int clasc_icod_clasificacion { get; set; }
        public int clasc_iid_clasificacion { get; set; }
        public string clasc_vdescripcion { get; set; }
        public int? ctacc_icod_cuenta_contable_producto { get; set; }
        public int? ctacc_icod_cuenta_contable_importacion { get; set; }
        public int? ctacc_icod_cuenta_contable_compra { get; set; }
        public int? ctacc_icod_cuenta_contable_costos { get; set; }
        public int almcc_icod_almacen { get; set; }

        public string strDesAlmacenCtbl { get; set; }
        public string strCuentaProducto { get; set; }
        public string strCuentaImportacion { get; set; }
        public string strCuentaCompra { get; set; }
        public string strCuentaCostos { get; set; }

        public string strDesCuentaProducto { get; set; }
        public string strDesCuentaImportacion { get; set; }
        public string strDesCuentaCompra { get; set; }
        public string strDesCuentaCostos { get; set; }
    }
}
