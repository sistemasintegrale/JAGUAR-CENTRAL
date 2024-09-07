using System.Collections;
using System.ComponentModel;

namespace SGE.Entity
{
    public class EBalance : EAuditoria
    {
        public int blgc_icod_balance { get; set; }
        public string blgc_vlinea { get; set; }
        public int tablc_icod_linea_registro { get; set; }
        public string blgc_vconcepto { get; set; }
        public int tablc_icod_signo_monto { get; set; }
        public int tablc_icod_tipo_total { get; set; }
        public bool blgc_flag_estado { get; set; }
        public string DesLinea { get; set; }
        public string Signo { get; set; }
        public string Total { get; set; }
        /*resultado Estado Por Centro Costo*/
        public decimal Monto { get; set; }

        public EBalanceCtasCollection eBalanceCtas = new EBalanceCtasCollection();

        public EBalanceCtasCollection EBalanceCtasC { get { return eBalanceCtas; } }
    }

    public class EBalanceCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EBalanceCtasCollection)))
                    return TypeDescriptor.GetProperties(typeof(EBalanceCtas));
            }
            return TypeDescriptor.GetProperties(typeof(EBalance));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EBalances";
        }
    }
}
