using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class EComprasDaotDetalle
    {
        [DataMember]
        public long? proc_icod_proveedor { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public string simboloMoneda { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public decimal? valor_compra_soles { get; set; }
        [DataMember]
        public decimal? valor_compra_dolares { get; set; }
        [DataMember]
        public decimal? valor_compra_mond_orig { get; set; }
        [DataMember]
        public string valor_compra_dolares_str { get; set; }
        [DataMember]
        public string doxpc_vdescrip_transaccion { get; set; }
    }

    public class EComprasDaotDetalleCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(EComprasDaotDetalle));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EComprasDaotDetalles";
        }
    }
}
