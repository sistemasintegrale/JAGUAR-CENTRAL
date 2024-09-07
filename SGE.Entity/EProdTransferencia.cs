using System;

namespace SGE.Entity
{
    public class EProdTransferencia : EAuditoria
    {
        //[DataMember]
        public int trfc_icod_transf { get; set; }
        //[DataMember]
        public int trfc_inum_transf { get; set; }
        //[DataMember]
        public DateTime trfc_sfecha_transf { get; set; }
        //[DataMember]
        public int puvec_icod_punto_venta { get; set; }
        //[DataMember]
        public int trfc_iid_alm_sal { get; set; }
        //[DataMember]
        public int trfc_iid_alm_ing { get; set; }
        //[DataMember]
        public int id_almacen_ingreso { get; set; }
        //[DataMember]
        public string almac_vdescripcion_ingreso { get; set; }
        //[DataMember]
        public string almac_vdescripcion_salida { get; set; }
        //[DataMember]
        public string trfc_vobservaciones { get; set; }
        //[DataMember]
        public int isuario { get; set; }
        //[DataMember]
        public int? trfc_iactivo { get; set; }
    }
}
