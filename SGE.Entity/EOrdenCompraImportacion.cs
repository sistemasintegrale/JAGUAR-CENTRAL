using System;


namespace SGE.Entity
{
    public class EOrdenCompraImportacion : EOrdenCompraImportacionDetalle
    {

        public int ocic_icod_oci { get; set; }
        public int ocic_ianio { get; set; }
        public string ocic_vnumero_oci { get; set; }
        public DateTime ocic_sfecha_oci { get; set; }
        public int proc_icod_proveedor { get; set; }
        public string prvc_vcod_proveedor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal ocic_npor_imp_igv { get; set; }
        public decimal ocic_nmonto_neto { get; set; }
        public decimal ocic_nmonto_imp { get; set; }
        public decimal ocic_nmonto_total { get; set; }
        public int tablc_iid_situacion_oci { get; set; }
        public bool ocic_bincluido_igv { get; set; }
        public decimal ocic_nmonto_sub_Total { get; set; }
        public decimal ocic_npor_descuento { get; set; }
        public decimal ocic_nmonto_descuento { get; set; }
        public bool ocic_flag_estado { get; set; }
        public string ocic_vtelefono { get; set; }
        public string ocic_vreferencia { get; set; }
        public int ocic_iid_motivo { get; set; }
        public int ocic_iid_proyecto { get; set; }
        public string ocic_vlugar_entrega { get; set; }
        public string ocic_vgarantia { get; set; }
        public string ocic_vnota_ocl { get; set; }
        public string ocic_vcontacto { get; set; }
        public string ocic_vforma_pago { get; set; }
        public string strMoneda { get; set; }
        public string str_situacion { get; set; }
        public string proc_vnombrecompleto { get; set; }
        public string proc_vruc { get; set; }
        public string proc_vdireccion { get; set; }
        public string proc_vcorreo { get; set; }
        public string proc_vtelefono { get; set; }
        public int? lpedi_icod_proveedor { get; set; }

        public string str_motivo { get; set; }
        public string cecoc_vcodigo_centro_costo { get; set; }

        public string ocic_vincoterm { get; set; }
        public DateTime? ocic_sfecha_entrega { get; set; }
        public string ocic_vcotizacion { get; set; }
        public string ocic_vsolicitante { get; set; }
    }
}
