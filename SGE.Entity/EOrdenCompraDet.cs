using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EOrdenCompraDet :EAuditoria
    {
        [DataMember]
        public int occd_icod_orden_compra_deta { get; set; }
        [DataMember]
        public int occd_iitem { get; set; }
        [DataMember]
        public int prd_icod_producto { get; set; }
        [DataMember]
        public decimal occd_ncantidad_pedida { get; set; }
        [DataMember]
        public decimal occd_nmonto_unit { get; set; }
        [DataMember]
        public decimal occd_nmonto_total { get; set; }
        [DataMember]
        public decimal occd_nporcentaje_descuento { get; set; }
        [DataMember]
        public decimal occd_ncantidad_atendida { get; set; }
        [DataMember]
        public Boolean occd_flag_estado { get; set; }
        [DataMember]
        public int intTipoOperacion { get; set; }
        [DataMember]
        public string prd_vcodigo_externo { get; set; }
        [DataMember]
        public string prd_vdescripcion_producto { get; set; }
        [DataMember]
        public string unidc_vdescripcion { get; set; }
        [DataMember]
        public decimal CantidadGuiaEntrada { get; set; }
        [DataMember]
        public decimal Cantidad_saldo { get; set; }

        [DataMember]
        public int? gepd_icod_guia_entrada_ocp_deta { get; set; }
        [DataMember]
        public long? kardc_icod_correlativo { get; set; }
    }
}
