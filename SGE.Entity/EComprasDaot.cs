using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;
namespace SGE.Entity
{
    public class EComprasDaot
    {
        [DataMember]
        public long? proc_icod_proveedor { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public decimal? valor_compra_dolares { get; set; }
        [DataMember]
        public decimal? valor_compra_soles { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public int? tip_doc_proveedor { get; set; }
        [DataMember]
        public string num_doc_proveedor { get; set; }
        [DataMember]
        public string proc_vnombre1 { get; set; }
        [DataMember]
        public string proc_vnombre2 { get; set; }
        [DataMember]
        public string proc_vpaterno { get; set; }
        [DataMember]
        public string proc_vmaterno { get; set; }

        public EComprasDaotDetalleCollection eComprasDaotDetalles = new EComprasDaotDetalleCollection();

        public EComprasDaotDetalleCollection EComprasDaotDetalles { get { return eComprasDaotDetalles; } }
    }

    public class EComprasDaotCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EComprasDaotDetalleCollection)))
                    return TypeDescriptor.GetProperties(typeof(EComprasDaotDetalle));
            }
            return TypeDescriptor.GetProperties(typeof(EComprasDaot));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EComprasDaots";
        }
    }
}
