using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{

    public class EOrdenCompraMercaderiaDetalle : EAuditoria
    {
        public int ocod_icod_detalle_oc { get; set; }
        public int ococ_icod_orden_compra { get; set; }
        [DataMember]
        public int ocod_iitem { get; set; }
        [DataMember]
        public int? prdc_icod_producto { get; set; }
        [DataMember]
        public int? mnoc_icod_mano_obra { get; set; }
        [DataMember]
        public string mnoc_vdescrip_corta { get; set; }
        [DataMember]
        public string mnoc_vdescrip_larga { get; set; }
        [DataMember]
        public decimal ocod_ncantidad { get; set; }
        public decimal ocod_ncantidad_recibida { get; set; }
        public decimal ocod_ncantidad_saldo { get; set; }
        [DataMember]
        public decimal ocod_ncunitario { get; set; }
        [DataMember]
        public decimal ocod_nmonto_total { get; set; }
        [DataMember]
        public long kardc_iid_correlativo { get; set; }
        [DataMember]
        public Boolean ocod_flag_estado { get; set; }
        [DataMember]
        public int operacion { get; set; }

        [DataMember]
        public decimal orpdi_nprecio_soles { get; set; }
        [DataMember]
        public decimal orpdi_nprecio_dolares { get; set; }

        [DataMember]
        public string strCodigoProducto { get; set; }
        [DataMember]
        public string strDescProducto { get; set; }
        [DataMember]
        public string strMedida { get; set; }
        [DataMember]
        public string strAbrevUniMed { get; set; }
        [DataMember]
        public string famic_vdescripcion { get; set; }
        [DataMember]
        public string famic_vabreviatura { get; set; }
        [DataMember]
        public string famid_vdescripcion { get; set; }
        [DataMember]
        public string famid_vabreviatura { get; set; }
        [DataMember]
        public decimal ocod_ncantidad_ant { get; set; }
        public string ocod_vdescripcion { get; set; }
        public decimal ocod_ndescuento_item { get; set; }
        public string ocod_vdireccion_documento { get; set; }
        public string ocod_vcaracteristicas { get; set; }
        public DateTime ocod_dfecha_entrega { get; set; }
        public string prdc_vcodigo_fabricante { get; set; }

        public string ocod_rango_talla { get; set; }
        public int? ocod_talla1 { get; set; }
        public int? ocod_talla2 { get; set; }
        public int? ocod_talla3 { get; set; }
        public int? ocod_talla4 { get; set; }
        public int? ocod_talla5 { get; set; }
        public int? ocod_talla6 { get; set; }
        public int? ocod_talla7 { get; set; }
        public int? ocod_talla8 { get; set; }
        public int? ocod_talla9 { get; set; }
        public int? ocod_talla10 { get; set; }
        public int? ocod_cant_talla1 { get; set; }
        public int? ocod_cant_talla2 { get; set; }
        public int? ocod_cant_talla3 { get; set; }
        public int? ocod_cant_talla4 { get; set; }
        public int? ocod_cant_talla5 { get; set; }
        public int? ocod_cant_talla6 { get; set; }
        public int? ocod_cant_talla7 { get; set; }
        public int? ocod_cant_talla8 { get; set; }
        public int? ocod_cant_talla9 { get; set; }
        public int? ocod_cant_talla10 { get; set; }
        public int? ocod_cant_recibida1 { get; set; }
        public int? ocod_cant_recibida2 { get; set; }
        public int? ocod_cant_recibida3 { get; set; }
        public int? ocod_cant_recibida4 { get; set; }
        public int? ocod_cant_recibida5 { get; set; }
        public int? ocod_cant_recibida6 { get; set; }
        public int? ocod_cant_recibida7 { get; set; }
        public int? ocod_cant_recibida8 { get; set; }
        public int? ocod_cant_recibida9 { get; set; }
        public int? ocod_cant_recibida10 { get; set; }
        public int? ocod_cant_saldo1 { get; set; }
        public int? ocod_cant_saldo2 { get; set; }
        public int? ocod_cant_saldo3 { get; set; }
        public int? ocod_cant_saldo4 { get; set; }
        public int? ocod_cant_saldo5 { get; set; }
        public int? ocod_cant_saldo6 { get; set; }
        public int? ocod_cant_saldo7 { get; set; }
        public int? ocod_cant_saldo8 { get; set; }
        public int? ocod_cant_saldo9 { get; set; }
        public int? ocod_cant_saldo10 { get; set; }

        public int intTipoOperacion { get; set; }

    }
}
