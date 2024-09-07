namespace SGE.Entity
{
    public class EOrdenCompraImportacionDetalle : EAuditoria
    {
        public int ocid_icod_detalle_oci { get; set; }
        public int ocic_icod_oci { get; set; }
        public int ocid_iitem { get; set; }
        public int? prdc_icod_producto { get; set; }
        public decimal ocid_ncantidad { get; set; }
        public decimal ocid_ncunitario { get; set; }
        public decimal ocid_nmonto_total { get; set; }
        public int operacion { get; set; }
        public decimal orpdi_nprecio_soles { get; set; }
        public decimal orpdi_nprecio_dolares { get; set; }
        public string strCodigoProducto { get; set; }
        public string strDescProducto { get; set; }
        public string strMedida { get; set; }
        public string strAbrevUniMed { get; set; }
        public string ocid_vdescripcion { get; set; }
        public decimal ocid_ndescuento_item { get; set; }
        public string ocid_vcaracteristicas { get; set; }
        public string prdc_vcodigo_fabricante { get; set; }
        public bool ocid_flag_estado { get; set; }

        public string famic_vdescripcion { get; set; }
        public string famic_vabreviatura { get; set; }
        public string famid_vdescripcion { get; set; }
        public string famid_vabreviatura { get; set; }
    }
}
