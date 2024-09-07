namespace SGE.Entity
{
    public class EProdInventarioFisicoDetalle
    {
        //[DataMember]
        public int infid_icod_invent { get; set; }
        //[DataMember]
        public int invfi_icod_invent { get; set; }
        //[DataMember]
        public int pr_icod_producto { get; set; }
        //[DataMember]
        public decimal infid_ncant_fisica { get; set; }
        //[DataMember]
        public decimal infid_ncant_sistema { get; set; }
        //[DataMember]
        public decimal diferencia { get; set; }
        //[DataMember]
        public int infid_iactivo { get; set; }
        //[DataMember]
        public int iusuario { get; set; }
        //[DataMember]
        public int operacion { get; set; }

        public bool flag { get; set; }
        public int pr_tarec_icorrelativo { get; set; }
        //[DataMember]
        public string abreviaUnidadMedi { get; set; }
        //DESCRIPCIONES
        //[DataMember]
        public string pr_vcodigo_externo { get; set; }
        //[DataMember]
        public string pr_vdescripcion_producto { get; set; }

        public string Unidad_medida { get; set; }
        public int icod_unidad_medida { get; set; }
        public string pr_viid_modelo { get; set; }

    }
}
