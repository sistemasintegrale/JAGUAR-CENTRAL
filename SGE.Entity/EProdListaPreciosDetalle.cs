namespace SGE.Entity
{
    public class EProdListaPreciosDetalle
    {
        //[DataMember]
        public int lpred_icod_det_precio { get; set; }
        //[DataMember]
        public int lprec_icod_precio { get; set; }
        //[DataMember]
        public string lpred_vrango_tallas { get; set; }
        //[DataMember]
        public decimal lpred_nprecio_vtamin { get; set; }
        //[DataMember]
        public decimal lpred_nprecio_vtamay { get; set; }
        //[DataMember]
        public decimal lpred_nprecio_vtaotros { get; set; }
        //[DataMember]
        public int lpred_iactivo { get; set; }
        //[DataMember]
        public int operacion { get; set; }
        //[DataMember]
        public int iusuario { get; set; }
        //descripcion
        //[DataMember]
        public string descripcionProducto { get; set; }
        //para filtrar tallas
        //[DataMember]
        public string destalla { get; set; }
        //[DataMember]
        public string pr_vcodigo_externo { get; set; }
    }
}
