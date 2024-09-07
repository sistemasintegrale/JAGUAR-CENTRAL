using System;
using System.Collections;
using System.ComponentModel;

namespace SGE.Entity
{
    public class EPersonalCCostos : EAuditoria
    {
        public int pccd_icod_ccosto { set; get; }
        public int perc_icod_personal { set; get; }
        public string perc_vnum_doc { set; get; }
        public DateTime pccd_sfecha { set; get; }
        public string ccoc_numero_centro_costo { set; get; }
        public string ccoc_vdescripcion_ccosto { set; get; }
        public bool? pccd_flag_estado { set; get; }
        public int pccd_imes { set; get; }
        public int pccd_iaño { set; get; }
        public string strMes { set; get; }
        public int intTpoOperacion { set; get; }
        public int Contador { set; get; }
        /*Montos Cnetro Costos*/
        public decimal pccd_nmonto_vacaciones { get; set; }
        public decimal pccd_nmonto_gratificaciones { get; set; }
        public decimal pccd_nmonto_cts { get; set; }
        public int cecoc_icod_centro_costo { get; set; }

        public decimal MontoGeneral { get; set; }

    }

    public class EPersonalCCostosCollection : ArrayList, ITypedList
    {
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return TypeDescriptor.GetProperties(typeof(EPersonalCCostos));
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return "EPersonalCCostosX";
        }
    }
}
