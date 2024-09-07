using System.Collections;
using System.ComponentModel;


namespace SGE.Entity
{
    public class EEstadoGanPerFuncion : EAuditoria
    {
        public int egpfc_icod_estado_gan_per_funcion { get; set; }
        public string egpfc_vlinea { get; set; }
        public int tablc_icod_linea_registro { get; set; }
        public string egpfc_vconcepto { get; set; }
        public int tablc_icod_signo_monto { get; set; }
        public int tablc_icod_tipo_total { get; set; }
        public bool egpfc_flag_estado { get; set; }
        public string DesLinea { get; set; }
        public string Signo { get; set; }
        public string Total { get; set; }
        /*resultado Estado Por Centro Costo*/
        public decimal Monto { get; set; }
        public EEstadoGanPerCtasCollection eEstadoGanPerCtas = new EEstadoGanPerCtasCollection();

        public EEstadoGanPerCtasCollection EEstadoGanPerCtasC { get { return eEstadoGanPerCtas; } }

    }

    public class EEstadoGanPerFuncionCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors != null && listAccessors.Length > 0)
            {
                PropertyDescriptor listAccessor = listAccessors[listAccessors.Length - 1];
                if (listAccessor.PropertyType.Equals(typeof(EEstadoGanPerCtasCollection)))
                    return TypeDescriptor.GetProperties(typeof(EBalanceCtas));
            }
            return TypeDescriptor.GetProperties(typeof(EEstadoGanPerFuncion));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EEstadoGanPerFuncions";
        }
    }

}
