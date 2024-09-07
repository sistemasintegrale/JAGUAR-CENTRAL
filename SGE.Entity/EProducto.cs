using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    [DataContract]
    public class EProducto : EAuditoria
    {
        [DataMember]
        public int prdc_icod_producto { get; set; }
        [DataMember]
        public string prdc_vcode_producto { get; set; }
        [DataMember]
        public string prdc_vdescripcion_larga { get; set; }
        [DataMember]
        public string prdc_vdescripcion_corta { get; set; }

        [DataMember]
        public int? Catc_iid_tipo_tabla { get; set; }
        [DataMember]
        public int? CatNUno_iid_tabla_registro { get; set; }
        [DataMember]
        public int? CatNDos_iid_tabla_registro { get; set; }

        [DataMember]
        public int? edit_icod_editorial { get; set; }
        [DataMember]
        public string prdc_vAutor { get; set; }
        [DataMember]
        public int? unidc_icod_unidad_medida { get; set; }
        [DataMember]
        public int? proc_icod_proveedor { get; set; }

        [DataMember]
        public string prdc_vUbicacion { get; set; }
        [DataMember]
        public Boolean? prdc_bAfecto { get; set; }
        [DataMember]
        public Boolean? prdc_bCombo { get; set; }
        [DataMember]
        public bool prdc_isituacion { get; set; }
        [DataMember]
        public decimal? prdc_stock_minimo { get; set; }
        [DataMember]
        public decimal? prdc_mprecio_costo { get; set; }
        [DataMember]
        public decimal? prdc_mprecio_venta { get; set; }
        [DataMember]
        public bool prdc_flag_estado { get; set; }

        /*----------------------------------------------------*/
        [DataMember]
        public string strCategoria { get; set; }
        [DataMember]
        public string strSubCategoriaUno { get; set; }
        [DataMember]
        public string strSubCategoriaDos { get; set; }
        [DataMember]
        public string strEditorial { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        [DataMember]
        public string StrUnidadMedida { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public decimal dblStock { get; set; }

        public bool flag_select { get; set; }
        [DataMember]
        public int intOperacion { get; set; }



        [DataMember]
        public decimal prdc_nMonto_stock { get; set; }
        [DataMember]
        public decimal prdc_nPrecio_soles { get; set; }
        [DataMember]
        public decimal prdc_nPrecio_dolares { get; set; }
        [DataMember]
        public decimal prdc_nPor_Descuento { get; set; }
        [DataMember]
        public decimal prdc_nPrecio_venta { get; set; }
        [DataMember]
        public string prdc_Codigo_Precio { get; set; }
        [DataMember]
        public decimal prdc_ntipo_cambio { get; set; }
        [DataMember]
        public decimal prdc_nPrecio_venta_Dol { get; set; }
        [DataMember]
        public decimal prdc_ntipo_cambio_Precio { get; set; }
        public bool flag_select_mod { get; set; }

        public int? famic_icod_familia { get; set; }
        public int? famid_icod_familia { get; set; }
        public int? tarec_icorrelativo_registro_tipo { get; set; }


        public string strFamilia { get; set; }

        public string strSubFamilia { get; set; }
        public string strDesFamilia { get; set; }
        public string strDesSubFamilia { get; set; }
        public string CodProducto { get; set; }

        public string prdc_vmarca { get; set; }
        public string prdc_vmodelo { get; set; }
        public string prdc_vparte { get; set; }
        public string prdc_vserie { get; set; }
        public string prdc_vcaracteristica { get; set; }

        public string prdc_vcodigo_fabricante { get; set; }
        public int ctacc_icod_cuenta_contable { get; set; }
        public string NumCunetaContable { get; set; }

        public decimal dcmlCostoSol { get; set; }
        public decimal dcmlCostoDol { get; set; }

        public decimal prdc_calibre { get; set; }
        public int tarec_icorrelativo_colorprod { get; set; }
        public int prdc_icod_proveedor { get; set; }
        public decimal prdc_precio_costo { get; set; }
        public int prdc_icod_tipo { get; set; }
        public string strcolor { get; set; }

        public string strTipo { get; set; }
        public int prdc_italla { get; set; }
        public string CodigoSUNAT { get; set; }

    }
}
