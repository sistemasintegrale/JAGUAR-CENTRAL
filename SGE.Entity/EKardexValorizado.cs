using System;

namespace SGE.Entity
{
    public class EKardexValorizado : EAuditoria
    {
        public Int64 kardv_icod_correlativo { get; set; }
        public int kardv_ianio { get; set; }
        public DateTime kardv_sfecha_movimiento { get; set; }
        public int almcc_icod_almacen { get; set; }
        public int prdc_icod_producto { get; set; }
        public string prdc_vcode_producto { get; set; }
        public decimal kardv_icantidad_prod { get; set; }
        public int tdocc_icod_tipo_doc { get; set; }
        public string kardv_inumero_doc { get; set; }
        public int kardv_itipo_movimiento { get; set; }
        public int kardv_iid_motivo { get; set; }
        public string Motivo { get; set; }
        public string kardv_vbeneficiario { get; set; }
        public string kardv_vobservaciones { get; set; }
        public decimal? kardv_monto_total_compra { get; set; }
        public decimal kardv_precio_costo_promedio { get; set; }
        public decimal kardv_monto_saldo_valorizado { get; set; }
        public decimal kardv_monto_unitario_compra { get; set; }
        public decimal? kardv_nmonto_ingreso_manual { get; set; }
        public int? kardv_itipo_actualizacion { get; set; }

        public int intIidAlmaCont { get; set; }
        public string strDesAlmacenCtbl { get; set; }
        public string strCodProducto { get; set; }
        public string strDesProducto { get; set; }
        public string strTipoDoc { get; set; }
        public string strTipoNroDoc { get; set; }
        public decimal? dcmlIngreso { get; set; }
        public decimal? dcmlSalida { get; set; }
        public decimal kardv_icantidad_prod_ant { get; set; }

        public string situc_vdescripcion { get; set; }
        public string unidc_vabreviatura_unidad_medida { get; set; }
        public decimal? Stock { get; set; }
        public decimal? StockAnterior { get; set; }
        public decimal PrimerCosto { get; set; }
        public decimal CostoAlto { get; set; }
        public decimal kardv_monto_total_costo { get; set; }


        public int Cont_registro_valorizado { get; set; }
        public string tdocc_vcodigo_tipo_doc_sunat { get; set; }
        public string unidc_vtipo_ume { get; set; }
        public string alco_vtipo_existencia { get; set; }
        public string tarec_vtipo_operacion { get; set; }
        public decimal StockCosto { get; set; }
        public decimal StockAnteriorCosto { get; set; }
        public decimal? IngresoCosto { get; set; }
        public decimal? SalidaCosto { get; set; }
        public decimal ValorPresupuesto { get; set; }
        public string prep_cod_presupuesto { get; set; }
        public decimal CostoPresupuesto { get; set; }
        public string Documento { get; set; }

        public int intOperacion { get; set; }
        public decimal prdc_precio_costo { get; set; }
        public decimal precio_compra_total { get; set; }

    }
}
