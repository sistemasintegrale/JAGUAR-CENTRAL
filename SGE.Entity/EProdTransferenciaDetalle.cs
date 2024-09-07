namespace SGE.Entity
{
    public class EProdTransferenciaDetalle
    {
        //[DataMember]
        public int trfcd_icod_transf { get; set; }
        // [DataMember]
        public int trfc_icod_transf { get; set; }
        //[DataMember]
        public int trfcd_num_item { get; set; }
        //[DataMember]
        public int trfcd_icod_producto { get; set; }
        // [DataMember]
        public string trfcd_vcodigo_externo { get; set; }
        // [DataMember]
        public string pr_vdescripcion_producto { get; set; }
        //[DataMember]
        public decimal trfcd_ncantidad { get; set; }
        //[DataMember]
        public int trfcd_iid_alm_sal { get; set; }
        //[DataMember]
        public string DescripAlmacenSalida { get; set; }
        //[DataMember]
        public long trfcd_idsal_kardex { get; set; }
        //[DataMember]
        public int trfcd_iid_alm_ing { get; set; }
        //[DataMember]
        public string DescripAlmacenIngreso { get; set; }
        //[DataMember]



        public long trfcd_iding_kardex { get; set; }
        //[DataMember]
        public int? trfcd_iactivo { get; set; }
        //[DataMember]
        public int iusuario { get; set; }
        //[DataMember]
        public int operacion { get; set; }
        //[DataMember]
        public string UnidadMedi { get; set; }
    }
}
