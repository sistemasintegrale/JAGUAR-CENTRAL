using System;

namespace SGE.Entity
{
    public class EProdInventarioFisico
    {
        //[DataMember]
        public int invfi_icod_invent { get; set; }
        //[DataMember]
        public int almac_icod_almacen { get; set; }
        //[DataMember]
        public string invfi_vnumero_invent { get; set; }
        //[DataMember]
        public DateTime invfi_sinvent { get; set; }
        //[DataMember]
        public int invfi_itipo_invent { get; set; }

        public int tarec_correlative { get; set; }
        //[DataMember]
        public int gingc_icod_guia_ingreso { get; set; }
        //[DataMember]
        public int gsalc_icod_guia_salida { get; set; }
        //[DataMember]
        public int invfi_bsituacion { get; set; }
        //[DataMember]
        public int tarec_iid_marca { get; set; }
        //[DataMember]
        public int mo_iid_modelo { get; set; }
        //[DataMember]
        public int? tarec_iid_color { get; set; }
        //[DataMember]
        public int invfi_iestado { get; set; }
        //[DataMember]
        public int iusuario { get; set; }
        //DESCRIPCION
        public string almac_vdescripcion { get; set; }
        //[DataMember]
        public string DesTipoInventario { get; set; }
        //[DataMember]
        public string numeroGuiaIngreso { get; set; }
        //[DataMember]
        public string numeroGuiaSalida { get; set; }
        //[DataMember]
        public string desSituacion { get; set; }
        //[DataMember]
        public string desMarca { get; set; }
        //[DataMember]
        public string DesModelo { get; set; }
        //[DataMember]
        public string DesColor { get; set; }
    }
}
