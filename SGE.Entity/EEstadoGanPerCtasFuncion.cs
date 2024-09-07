using System.Collections;
using System.ComponentModel;

namespace SGE.Entity
{
    public class EEstadoGanPerCtasFuncion : EAuditoria
    {
        public int egpfd_icod_ctas_estado_gan_per_funcion { get; set; }
        public int egpfc_icod_estado_gan_per_funcion { get; set; }
        public int egpfd_iid_cuenta_contable { get; set; }
        public bool egpfd_flag_estado { get; set; }
        /*Cunetas*/
        public string ctacc_nombre_descripcion { get; set; }
        public string ctacc_numero_cuenta_contable { get; set; }

        public decimal MontosCC { get; set; }
    }

    public class EEstadoGanPerCtasCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(EEstadoGanPerCtasFuncion));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EEstadoGanPerCtasC";
        }
    }

}
