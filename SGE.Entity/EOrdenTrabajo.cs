using System;

namespace SGE.Entity
{
    public class EOrdenTrabajo : EAuditoria
    {
        public int otrc_icod_orden_trabajo { get; set; }
        public string otrc_vnumero_orden { get; set; }
        public int? prfc_iid_proforma { get; set; }
        public DateTime otrc_sfecha_orden { get; set; }
        public int otrc_icod_cliente { get; set; }
        public string otrc_vnumero_dni { get; set; }
        public int otrc_iid_vehiculo { get; set; }
        public string otrc_vkilometraje { get; set; }
        public string otrc_vcolor { get; set; }
        public string otrc_vnumero_vin { get; set; }
        public int otrc_porc_nivel_combustible { get; set; }
        public int otrc_itipo_pago { get; set; }
        public int? otrc_icod_cliente_afacturar { get; set; }
        public int otrc_itipo_facturacion { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal otrc_nmonto_neto { get; set; }
        public decimal otrc_npor_imp_igv { get; set; }
        public decimal otrc_nmonto_igv { get; set; }
        public decimal otrc_nmonto_total { get; set; }
        public decimal otrc_nmonto_descuento { get; set; }
        public int otrc_iid_situacion_orden { get; set; }
        public int pm_icod_parametro { get; set; }
        /**/
        public string strDesCliente { get; set; }
        public string strTelefonoCliente { get; set; }
        public string strRuc { get; set; }
        public string strDni { get; set; }
        public string strDireccionCliente { get; set; }
        public string strCorreoCliente { get; set; }
        public string strDesClienteFactura { get; set; }
        public string strRucFactura { get; set; }
        public string strDireccionClienteFactura { get; set; }
        public string strPlaca { get; set; }
        public string strMarca { get; set; }
        public string strModelo { get; set; }
        public string strSituacion { get; set; }
        public int? strAnioVehiculo { get; set; }
        public string strNroProforma { get; set; }
        public string strUsuarioCierra { get; set; }
        public string strDocFacturacion { get; set; }

    }
}
