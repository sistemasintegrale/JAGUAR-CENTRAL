namespace SGE.Entity
{
    public class EProdListaPrecios : EProdListaPreciosDetalle
    {
        //[DataMember]
        public int lprec_icod_precio { get; set; }
        //[DataMember]
        public int? pr_icod_producto { get; set; }
        //[DataMember]
        public string pr_vcodigo_externo { get; set; }
        //[DataMember]
        public string pr_vdescripcion_producto { get; set; }
        //[DataMember]
        public decimal lprec_nprecio_costo { get; set; }
        //[DataMember]
        public decimal lprec_nporc_utilidad { get; set; }
        //[DataMember]
        public decimal lprec_nprecio_vtamin { get; set; }
        //[DataMember]
        public decimal lprec_nprecio_vtamay { get; set; }

        public decimal lprec_nprecio_vtaotros { get; set; }
        //[DataMember]
        public bool? lprec_bdetalle { get; set; }
        //[DataMember]
        public int lprec_iactivo { get; set; }
        //[DataMember]
        public int iusuario { get; set; }
        public int pr_iid_marca { get; set; }
        public int pr_iid_modelo { get; set; }
        public int pr_iid_color { get; set; }
    }
}
