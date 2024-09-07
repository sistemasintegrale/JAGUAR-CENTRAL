using System;

namespace SGE.Entity
{

    public class EProdNotaSalidaAlmacen : EAuditoria
    {
        //[DataMember]
        public int numero_icod_nota_salida { get; set; }
        //[DataMember]
        public int numero_nota_salida { get; set; }
        //[DataMember]
        public string numerof_nota_salida { get; set; }
        //[DataMember]
        public int id_almacen { get; set; }
        //[DataMember]
        public int id_motivo { get; set; }
        //[DataMember]
        public string descripcion_motivo { get; set; }
        //[DataMember]
        public DateTime fecha_nota_salida { get; set; }
        //[DataMember]
        public int id_tipo_doc { get; set; }
        //[DataMember]
        public string descripcion_tipo_doc { get; set; }
        //[DataMember]
        public string numero_doc { get; set; }
        //[DataMember]
        public string referencia { get; set; }
        //[DataMember]
        public string observaciones { get; set; }
        public string descripcion_almacen { get; set; }
        //[DataMember]
        public string Numero { get; set; }
        //[DataMember]
        public string vid_almacen { get; set; }
        //[DataMember]
        public string vNumeroDocumento { get; set; }
        //[DataMember]
        public DateTime fecha_inicio { get; set; }
        //[DataMember]
        public DateTime fecha_fin { get; set; }
        //[DataMember]
        public decimal cantidadNota { get; set; }


        #region transferencia

        public int ningc_icod_nota_salida_almacen { get; set; }
        //[DataMember]
        public int ningc_inumero_nota_salida { get; set; }
        //[DataMember]
        public int ningc_iid_almacen { get; set; }
        //[DataMember]
        public int ningc_iid_motivo { get; set; }
        //[DataMember]
        public int puvec_icod_punto_venta { get; set; }

        public DateTime ningc_sfecha_nota_ingreso { get; set; }
        //[DataMember]
        public int ningc_iid_tipo_doc { get; set; }
        //[DataMember]
        public string ningc_inumero_doc { get; set; }
        //[DataMember]
        public string ningc_vreferencia { get; set; }
        //[DataMember]
        public string ningc_vobservaciones { get; set; }
        //[DataMember]
        public int ningc_iactivo { get; set; }

        #endregion
    }
}
