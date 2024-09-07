namespace SGE.Entity
{
    public class EFichaTecnicaDet : EAuditoria
    {
        public int fited_icod_item_ficha_tecnica { get; set; }
        public int fitec_iid_ficha_tecnica { get; set; }
        public int fited_iitem_ficha_tecnica { get; set; }
        public int fited_iarea { get; set; }
        public int fited_iubicacion { get; set; }
        public string fited_vdescripcion { get; set; }
        public int prdc_icod_producto { get; set; }
        public string strCodeProducto { get; set; }
        public string strProducto { get; set; }
        public string strUnidadMedida { get; set; }
        public decimal fited_ncantidad { get; set; }
        public int fited_imedida { get; set; }
        public int intusuario { get; set; }
        public string vpc { get; set; }
        public bool fited_flag_estado { get; set; }
        public string strarea { get; set; }
        public string strubicacion { get; set; }
        public int intTipoOperacion { get; set; }
        public string strDescripUnidMed { get; set; }
        //IMPRESION
        public int numero { get; set; }
        public string indicaciones { get; set; }

        public string strareaG { get; set; }
    }
}
