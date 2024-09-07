using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EOrdenCompraMercaderia : EOrdenCompraMercaderiaDetalle
    {
        [DataMember]
        public int ococ_icod_orden_compra { get; set; }
        public int ococ_ianio { get; set; }
        [DataMember]
        public string ococ_numero_orden_compra { get; set; }
        [DataMember]
        public DateTime ococ_sfecha_orden_compra { get; set; }
        public DateTime ococ_sfecha_entrega { get; set; }
        [DataMember]
        public int almac_icod_almacen { get; set; }
        [DataMember]
        public int proc_icod_proveedor { get; set; }
        [DataMember]
        public string prvc_vcod_proveedor { get; set; }
        [DataMember]
        public int orpc_iid_orden_reparacion { get; set; }
        [DataMember]
        public int tablc_iid_tipo_moneda { get; set; }
        [DataMember]
        public decimal ococ_npor_imp_igv { get; set; }
        [DataMember]
        public decimal ococ_nmonto_neto { get; set; }
        [DataMember]
        public decimal ococ_nmonto_imp { get; set; }
        [DataMember]
        public decimal ococ_nmonto_total { get; set; }
        [DataMember]
        public string ococ_vobservaciones { get; set; }
        [DataMember]
        public int tablc_iid_situacion_oc { get; set; }
        [DataMember]
        public int tablc_iforma_pago { get; set; }
        [DataMember]
        public Boolean ococ_flag_estado { get; set; }
        [DataMember]
        public string orpc_vnumero_orden { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public string strMoneda { get; set; }
        [DataMember]
        public string str_situacion { get; set; }
        [DataMember]
        public Boolean facc_bIncluido_igv { get; set; }
        [DataMember]
        public string orpc_vnumero_vin { get; set; }
        [DataMember]
        public string proc_vruc { get; set; }
        [DataMember]
        public string proc_vdireccion { get; set; }
        [DataMember]
        public string proc_vcorreo { get; set; }
        [DataMember]
        public string proc_vtelefono { get; set; }

        [DataMember]
        public int? lpedi_icod_proveedor { get; set; }
        [DataMember]
        public string lpedi_Numerolista { get; set; }

        [DataMember]
        public int orpc_itipo_facturacion { get; set; }
        [DataMember]
        public int orpc_itipo_combustible { get; set; }
        [DataMember]
        public decimal orpc_nmonto_sub_Total { get; set; }
        [DataMember]
        public decimal orpc_npor_descuento { get; set; }
        [DataMember]
        public decimal orpc_nmonto_descuento { get; set; }

        public string ococ_vtelefono { get; set; }
        public string ococ_vreferencia { get; set; }
        public int ococ_iid_motivo { get; set; }
        public int ococ_iid_proyecto { get; set; }
        public string ococ_vlugar_entrega { get; set; }
        public string ococ_vgarantia { get; set; }
        public string ococ_vnota_ocl { get; set; }
        public string ococ_vcontacto { get; set; }
        public string str_motivo { get; set; }
        public string cecoc_vcodigo_centro_costo { get; set; }

        public string ococ_vforma_pago { get; set; }
        public string ococ_vrecepcion { get; set; }
        public string ococ_vcotizacion { get; set; }

        public string ococ_vcomprobante { get; set; }
        public string ococ_vciudad { get; set; }
        public Boolean ococ_flag_serie { get; set; }
    }
}
