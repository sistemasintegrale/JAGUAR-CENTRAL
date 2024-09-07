namespace SGE.Entity
{
    public class EProdNotaSalidaDetalleAlmacen : EAuditoria
    {
        public int numero_nota_salida { get; set; }
        public int item { get; set; }
        public string itemm { get; set; }
        public int id_almacen { get; set; }
        public string vcodigo_externo { get; set; }
        public string descripcion_producto { get; set; }
        public string descripcion_unidad_medida { get; set; }
        public decimal cantidad { get; set; }
        public string flag { get; set; }
        public int Operacion { get; set; }
        public int icod_nota_salida_detalle { get; set; }
        public long? idkardex { get; set; }
        public int pr_icod_producto { get; set; }


        #region Transferencia
        public int dninc_icod_detalle_salida_almacen { get; set; }
        //[DataMember]
        public int dninc_iitem { get; set; }
        //[DataMember]
        public int dninc_inumero_nota_salida { get; set; }
        //[DataMember]
        public int dninc_iid_almacen { get; set; }
        //[DataMember]
        public string dninc_vcodigo_externo { get; set; }
        //[DataMember]
        public decimal dninc_ncantidad { get; set; }
        //[DataMember]
        public int dninc_iactivo { get; set; }
        //[DataMember]
        public long? dninc_kardex { get; set; }



        #endregion
    }
}
