using System;

namespace SGE.Entity
{
    public class EVentaDirecta : EAuditoria
    {
        public int dvdc_icod_doc_venta_directa { get; set; }
        public string dvdc_vnumero_doc_venta_directa { get; set; }
        public DateTime dvdc_sfecha_doc_venta_directa { get; set; }
        public int dvdc_icod_cliente { get; set; }
        public string clic_vcod_cliente { get; set; }
        public string dvdc_vdireccion_cliente { get; set; }
        public string dvdc_vruc { get; set; }
        public string dvdc_vdni { get; set; }
        public int? dvdc_iid_vehiculo { get; set; }
        public string dvdc_vcolor { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal dvdc_npor_imp_igv { get; set; }
        public decimal dvdc_nmonto_neto { get; set; }
        public decimal dvdc_nmonto_imp { get; set; }
        public decimal dvdc_nmonto_total { get; set; }
        public int tablc_iid_situacion { get; set; }
        /**/
        public string strDesCliente { get; set; }
        public string strPlaca { get; set; }
        public string strMarca { get; set; }
        public string strModelo { get; set; }
        public string strSituacion { get; set; }
        public string strMoneda { get; set; }
        public int intAnioVehiculo { get; set; }
    }
}
