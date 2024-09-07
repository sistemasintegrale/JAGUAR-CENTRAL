using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EVentasDaotDetalle
    {
        [DataMember]
        public long? cliec_icod_cliente { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string doxcc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxcc_sfecha_doc { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string simboloMoneda { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal? doxcc_nmonto_total { get; set; }
        [DataMember]
        public decimal? valor_venta_soles { get; set; }
        [DataMember]
        public decimal? valor_venta_dolares { get; set; }
        [DataMember]
        public decimal? valor_venta_mond_orig { get; set; }
        [DataMember]
        public string valor_venta_dolares_str { get; set; }
        [DataMember]
        public string doxcc_vdescrip_transaccion { get; set; }
    }
    public class EVentasDaotDetalleCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(EVentasDaotDetalle));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EVentasDaotDetalles";
        }
    }
}
