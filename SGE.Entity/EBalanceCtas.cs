using System.Collections;
using System.ComponentModel;

namespace SGE.Entity
{
    public class EBalanceCtas : EAuditoria
    {
        public int blgd_icod_ctas_balance { get; set; }
        public int blgc_icod_balance { get; set; }
        public int blgd_iid_cuenta_contable { get; set; }
        public bool blgd_flag_estado { get; set; }
        /*Cunetas*/
        public string ctacc_nombre_descripcion { get; set; }
        public string ctacc_numero_cuenta_contable { get; set; }

        public decimal MontosCC { get; set; }

    }

    public class EBalanceCtasCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(EBalanceCtas));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EBalanceCtasC";
        }
    }

}
