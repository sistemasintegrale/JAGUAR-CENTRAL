using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EVentasDaot
    {
        [DataMember]
        public long? cliec_icod_cliente { get; set; }
        [DataMember]
        public string cliec_vcod_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre_cliente { get; set; }
        [DataMember]
        public decimal? valor_venta_dolares { get; set; }
        [DataMember]
        public decimal? valor_venta_soles { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public int? tip_doc_cliente { get; set; }
        [DataMember]
        public string num_doc_cliente { get; set; }
        [DataMember]
        public string cliec_vnombre1 { get; set; }
        [DataMember]
        public string cliec_vnombre2 { get; set; }
        [DataMember]
        public string cliec_vapellido_paterno { get; set; }
        [DataMember]
        public string cliec_vapellido_materno { get; set; }

        public EVentasDaotDetalleCollection eVentasDaotDetalles = new EVentasDaotDetalleCollection();

        public EVentasDaotDetalleCollection EVentasDaotDetalles { get { return eVentasDaotDetalles; } }
    }

    public class EVentasDaotCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EVentasDaotDetalleCollection)))
                    return TypeDescriptor.GetProperties(typeof(EVentasDaotDetalle));
            }
            return TypeDescriptor.GetProperties(typeof(EVentasDaot));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EVentasDaots";
        }
    }
}
