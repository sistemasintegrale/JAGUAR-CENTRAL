using System;

namespace SGE.Entity
{
    public class EOrdenCompraServicioDetalle : EAuditoria
    {
        public int ocsd_icod_detalle_ocs { get; set; }
        public int ocsc_icod_ocs { get; set; }
        public int ocsd_iitem { get; set; }
        public string ocsd_vcodigo_servicio_prov { get; set; }
        public string ocsd_vdescripcion { get; set; }
        public DateTime ocsd_sfecha_entrega { get; set; }
        public decimal ocsd_ncantidad { get; set; }
        public int unidc_icod_unidad_medida { get; set; }
        public int tablc_iid_tipo_moneda { get; set; }
        public decimal ocsd_ncunitaria { get; set; }
        public decimal ocsd_nvalor_total { get; set; }
        public decimal ocsd_ndescuento { get; set; }
        public string ocsd_vdireccion_documento { get; set; }
        public string ocsd_vcaracteristicas { get; set; }
        public bool ocsd_flag_esatdo { get; set; }

        public decimal orpdi_nprecio_soles { get; set; }

        public decimal orpdi_nprecio_dolares { get; set; }
        public int operacion { get; set; }

        public string strMedida { get; set; }
        public string strAbrevUniMed { get; set; }
    }
}
