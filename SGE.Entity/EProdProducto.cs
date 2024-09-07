using System;

namespace SGE.Entity
{
    public class EProdProducto : EAuditoria
    {
        //[DataMember]
        public int pr_icod_producto { get; set; }
        //[DataMember]
        public int? pr_iid_producto { get; set; }
        //[DataMember]
        public int? puvec_icod_punto_venta { get; set; }
        //[DataMember]
        public int? pr_iid_marca { get; set; }
        //[DataMember]
        public string pr_viid_marca { get; set; }
        //[DataMember]
        public int? pr_iid_modelo { get; set; }
        //[DataMember]
        public string pr_viid_modelo { get; set; }
        //[DataMember]
        public int? pr_iid_color { get; set; }
        //[DataMember]
        public string pr_viid_color { get; set; }
        //[DataMember]
        public int? pr_iid_talla { get; set; }
        //[DataMember]
        public string pr_viid_talla { get; set; }

        //[DataMember]
        public string pr_vcodigo_externo { get; set; }
        //[DataMember]
        public string pr_vdescripcion_producto { get; set; }
        //[DataMember]
        public string pr_vabreviatura_producto { get; set; }
        //[DataMember]
        public int pr_isituacion_producto { get; set; }
        //[DataMember]
        public int pr_iestado_producto { get; set; }
        //[DataMember]
        public int pr_tarec_icorrelativo { get; set; }
        //[DataMember]
        public string pr_ruta { get; set; }
        //[DataMember]
        public string pr_vsituacion { get; set; }
        //[DataMember]
        public decimal? stocc_nstock_prod { get; set; }
        //[DataMember]
        public int? unidc_icod_unidad_medida { get; set; }
        //[DataMember]
        public string descripUnidadMedi { get; set; }
        //[DataMember]
        public string abreviaUnidadMedi { get; set; }
        //[DataMember]
        public string rutaModelo { get; set; }
        public int stocc_ianio { get; set; }
        public Boolean pr_afecto_igv { get; set; }
        public decimal pr_nporcentaje_igv { get; set; }
        public string CodigoSUNAT { get; set; }
        public string strUM { get; set; }
        public int pr_icod_serie { get; set; }
        public string resec_vtalla_inicial { get; set; }
        public string resec_vtalla_final { get; set; }
        public int icodmarca { get; set; }
        public int icodcolor { get; set; }
        public int codmodelo { get; set; }
        public string strSituacion { get { return pr_isituacion_producto == 1 ? "Activo" : "Inactivo"; } }
        public bool Select { get; set; }
    }
}
