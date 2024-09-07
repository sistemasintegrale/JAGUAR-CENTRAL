using System;

namespace SGE.Entity
{
    public class EProdconsultaKardex
    {
        //[DataMember]
        public int? kardc_iid_correlativo { get; set; }
        //[DataMember]
        public DateTime? kardc_sfecha_movimiento { get; set; }
        // [DataMember]
        public string DescripcionMotivo { get; set; }
        // [DataMember]
        public int? kardc_iid_producto { get; set; }
        //[DataMember]
        public string pr_vdescripcion_producto { get; set; }
        //[DataMember]
        public string pr_vcodigo_externo { get; set; }
        //[DataMember]
        public string DesUnidadMedida { get; set; }
        //[DataMember]
        public int? kardc_itipo_movimiento { get; set; }
        //[DataMember]
        public int? tipo_movimiento_ingreso { get; set; }
        //[DataMember]
        public int? tipo_movimiento_salida { get; set; }
        // [DataMember]
        public string nro_documento { get; set; }
        //[DataMember]
        public string kardc_vbeneficiario { get; set; }
        // [DataMember]
        public string kardc_vobservaciones { get; set; }
        //[DataMember]
        public int? kardc_inumero_nota { get; set; }
        //[DataMember]
        public string descripcionAlmacen { get; set; }
        // [DataMember]
        public int? stockaAnterior { get; set; }
        // [DataMember]
        public int? stockActual { get; set; }
        // [DataMember]
        public int? kardc_iidalmacen { get; set; }
        //[DataMember]
        public decimal kardc_icantidad_prod { get; set; }
    }
}
