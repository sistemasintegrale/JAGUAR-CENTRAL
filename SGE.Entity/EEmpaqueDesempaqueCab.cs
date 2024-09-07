using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EEmpaqueDesempaqueCab : EEmpaqueDesempaqueDet
    {
        [DataMember]
        public int emp_icod_desempaque { get; set; }
        [DataMember]
        public DateTime emp_sfecha_desempaque { get; set; }
        [DataMember]
        public string emp_vnuemro_desempaque { get; set; }
        [DataMember]
        public int emp_inuemro_desempaque { get; set; }
        [DataMember]
        public int plemc_iid { get; set; }
        [DataMember]
        public int prdc_icod_producto { get; set; }
        [DataMember]
        public int kardc_icod_correlativo { get; set; }
        [DataMember]
        public int almac_icod_almacen { get; set; }
        [DataMember]
        public int Tablc_itipo_empaque { get; set; }
        [DataMember]
        public string emp_vobservaciones { get; set; }
        [DataMember]
        public decimal emp_dcantidad_desempaque { get; set; }
        [DataMember]
        public Boolean emp_flag_estado { get; set; }
        //descripcion

        [DataMember]
        public string DescripTipo { get; set; }
        [DataMember]
        public string almac_vdescripcion { get; set; }
        [DataMember]
        public string prdc_vdescripcion_larga { get; set; }
        [DataMember]
        public string prdc_vpart_number { get; set; }
        [DataMember]
        public string prdc_vcode_producto { get; set; }
        [DataMember]
        public string unidc_vabreviatura_unidad_medida { get; set; }
        [DataMember]
        public string plemc_Vicod { get; set; }
    }
}
