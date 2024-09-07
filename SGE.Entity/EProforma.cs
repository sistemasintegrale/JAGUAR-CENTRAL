using System;

namespace SGE.Entity
{
    public class EProforma : EAuditoria
    {
        public int prfc_icod_proforma { get; set; }
        public string prfc_vnumero_proforma { get; set; }
        public DateTime prfc_sfecha_proforma { get; set; }
        public int? prfc_iid_orden_trabajo { get; set; }
        public string prfc_vnumero_orden_trabajo { get; set; }
        public int prfc_icod_cliente { get; set; }
        public int prfc_iid_vehiculo { get; set; }
        public string prfc_vkilometraje { get; set; }
        public string prfc_vcolor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal prfc_nmonto_neto { get; set; }
        public decimal prfc_npor_imp_igv { get; set; }
        public decimal prfc_nmonto_igv { get; set; }
        public decimal prfc_nmonto_total { get; set; }
        public decimal prfc_nmonto_descuento { get; set; }
        public int prfc_iid_situacion_proforma { get; set; }
        /**/
        public string prfc_recepcion { get; set; }
        public string prfc_recomendacion { get; set; }
        /**/
        public string strDesCliente { get; set; }
        public string strRUC { get; set; }
        public string strDireccion { get; set; }
        public string strPlaca { get; set; }
        public string strMarca { get; set; }
        public string strModelo { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
        public string strTelefonoCliente { get; set; }
        public string strCorreo { get; set; }
        public int intAnioVehiculo { get; set; }
    }
}
