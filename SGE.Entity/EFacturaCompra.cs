using System;

namespace SGE.Entity
{
    public class EFacturaCompra : EAuditoria
    {
        public int fcoc_icod_doc { get; set; }
        public int proc_icod_proveedor { get; set; }
        public string fcoc_num_doc { get; set; }
        public DateTime fcoc_sfecha_doc { get; set; }
        public int almac_icod_almacen { get; set; }
        public long doxpc_icod_correlativo { get; set; }
        public DateTime fcoc_svencimiento { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public int fcoc_iforma_pago { get; set; }
        public string fcoc_vreferencia { get; set; }
        public decimal fcoc_nporcent_imp_doc { get; set; }
        public decimal fcoc_nmonto_destino_gravado { get; set; }
        public decimal fcoc_nmonto_destino_mixto { get; set; }
        public decimal fcoc_nmonto_destino_nogravado { get; set; }
        public decimal fcoc_nmonto_nogravado { get; set; }
        public decimal fcoc_nmonto_imp_destino_gravado { get; set; }
        public decimal fcoc_nmonto_imp_destino_mixto { get; set; }
        public decimal fcoc_nmonto_imp_destino_nogravado { get; set; }
        public decimal fcoc_nmonto_tipo_cambio { get; set; }
        public decimal fcoc_nmonto_adelanto { get; set; }
        public Int16 fcoc_imes_iid_proceso { get; set; }
        public bool fcoc_bincluye_igv { get; set; }
        public string fcoc_vnro_depo_detraccion { get; set; }
        public DateTime? fcoc_sfecha_depo_detraccion { get; set; }
        public decimal fcoc_nmonto_neto_detalle { get; set; }
        public decimal fcoc_nmonto_total_detalle { get; set; }
        public int fcoc_isituacion { get; set; }
        public Int16? fcoc_anio { get; set; }
        public int? prep_icod_presupuesto { get; set; }
        public string prep_cod_presupuesto { get; set; }
        public Boolean sflag_relacion_presupuesto { get; set; }

        public int? ococ_icod_orden_compra { get; set; }
        public string ococ_numero_orden_compra { get; set; }
        /**/
        public string strProveedor { get; set; }
        public string strAlmacen { get; set; }
        public string strMoneda { get; set; }
        public string strFormaPago { get; set; }
        public string strSituacion { get; set; }
        public Int64 intDXP { get; set; }
        public Boolean fcoc_flag_importacion { get; set; }
        public int fcoc_iestado_recep { get; set; }
        public string DesEstadoRecepcion { get; set; }
        public int? impd1_icod_import_factura { get; set; }
        /*Ingreso Kardex*/
        public string impc_vnumero_importacion { get; set; }
        public int impc_icod_importacion { get; set; }

        public decimal MontoTotalCabecera { get; set; }

        public int fcoc_tipo_producto { get; set; }
        public int fcoc_tipo_documento { get; set; }
        public int fcoc_icod_documento { get; set; }
        public string NumTD { get; set; }
    }
}
