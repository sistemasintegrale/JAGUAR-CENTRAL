using System;

namespace SGE.Entity
{
    public class EInventarioCab : EInventarioDet
    {
        public int invc_icod_inventario { get; set; }
        public int invc_iid_correlativo { get; set; }
        public string invc_vid_correlativo { get; set; }
        public int almac_icod_almacen { get; set; }
        public DateTime invc_sfecha_inventario { get; set; }
        public int tablc_iid_tipo_inventario { get; set; }
        public int tablc_iid_situacion { get; set; }
        public int? ningc_icod_nota_ingreso { get; set; }
        public int? nsalc_icod_nota_salida { get; set; }
        public string invc_vobservaciones { get; set; }
        /**/
        public string strTipoInventario { get; set; }
        public string strSituacion { get; set; }
        public string strAlmacen { get; set; }
    }
}
