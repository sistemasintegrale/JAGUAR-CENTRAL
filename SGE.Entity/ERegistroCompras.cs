using System;
using System.Runtime.Serialization;

namespace SGE.Entity
{
    public class ERegistroCompras
    {
        [DataMember]
        public long doxpc_icod_correlativo { get; set; }
        [DataMember]
        public string doxpc_viid_correlativo { get; set; }
        [DataMember]
        public string tdocc_vabreviatura_tipo_doc { get; set; }
        [DataMember]
        public int? tdodc_iid_correlativo { get; set; }
        [DataMember]
        public string doxpc_vnumero_doc { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_doc { get; set; }
        [DataMember]
        public string proc_vcod_proveedor { get; set; }
        [DataMember]
        public string proc_vnombrecompleto { get; set; }
        [DataMember]
        public string simboloMoneda { get; set; }
        [DataMember]
        public int? tablc_iid_tipo_moneda { get; set; }

        //montos
        [DataMember]
        public decimal? doxpc_nmonto_tipo_cambio { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_gravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_referencial_cif { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_servicio_no_domic { get; set; }
        [DataMember]
        public decimal? valor_compra { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_gravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_mixto { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_imp_destino_nogravado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_isc { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_pagado { get; set; }
        [DataMember]
        public decimal? doxpc_nmonto_total_saldo { get; set; }
        //montos necesarios para el reporte

        [DataMember]
        public string situacion_documento { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public string doxpc_vnro_deposito_detraccion { get; set; }
        [DataMember]
        public DateTime? doxpc_sfec_deposito_detraccion { get; set; }

        //variables para el reporte
        [DataMember]
        public string str_doxpc_icod_correlativo { get; set; }
        [DataMember]
        public string str_doxpc_sfecha_doc { get; set; }
        [DataMember]
        public string str_doxpc_sfecha_vencimiento_doc { get; set; }
        [DataMember]
        public string tdocc_vcodigo_tipo_doc_sunat { get; set; }
        [DataMember]
        public string tip_doc_proveedor { get; set; }
        [DataMember]
        public string num_doc_proveedor { get; set; }
        [DataMember]
        public string str_doxpc_sfec_deposito_detraccion { get; set; }
        [DataMember]
        public string tdocc_vdescripcion { get; set; }

        //montos reporte        
        [DataMember]
        public string str_doxpc_nmonto_destino_gravado { get; set; } //Base Imponible Oper.Gravada
        [DataMember]
        public string str_doxpc_nmonto_destino_mixto { get; set; } //Base Imponible Oper.Comun
        [DataMember]
        public string str_doxpc_nmonto_destino_nogravado { get; set; } //Oper.Destino No gravado
        [DataMember]
        public string str_doxpc_nmonto_nogravado { get; set; } //Adq.No Gravada 
        [DataMember]
        public string str_doxpc_nmonto_referencial_cif { get; set; }
        [DataMember]
        public string str_doxpc_nmonto_imp_destino_gravado { get; set; } //igv op. gravada
        [DataMember]
        public string str_doxpc_nmonto_imp_destino_mixto { get; set; } //igv op. común
        [DataMember]
        public string str_doxpc_nmonto_imp_destino_nogravado { get; set; } //igv op. no gravada
        [DataMember]
        public string str_doxpc_nmonto_isc { get; set; }

        [DataMember]
        public string str_doxpc_nmonto_total_documento { get; set; }
        [DataMember]
        public string str_doxpc_nmonto_tipo_cambio { get; set; }



        //variables referencia de nota de crédito
        [DataMember]
        public string nc_dxc_tipodoc { get; set; }
        //[DataMember]
        //public string nc_dxc_numdoc { get; set; }
        //[DataMember]
        //public string nc_dxc_fecha { get; set; }


        [DataMember]
        public int? tdocc_icod_tipo_doc { get; set; }
        [DataMember]
        public int? doxpc_numdoc_tipo { get; set; }


        [DataMember]
        public int? doxpc_tipo_comprobante_referencia { get; set; }
        [DataMember]
        public string doxpc_num_serie_referencia { get; set; }
        [DataMember]
        public string doxpc_num_comprobante_referencia { get; set; }
        [DataMember]
        public DateTime? doxpc_sfecha_emision_referencia { get; set; }
        public string CUO { get; set; }
        public string doxpc_vnumero_serio { get; set; }
        public string doxpc_vnumero_correlativo { get; set; }
        [DataMember]
        public string vcocc_numero_vcontable { get; set; }
        [DataMember]
        public int subdi_icod_subdiario { get; set; }
        [DataMember]
        public string ViddAdquisicion { get; set; }

        public int doxpc_iid_correlativo { get; set; }
        /*DUA*/
        public string doxpc_codigo_aduana { get; set; }
        public string doxpc_anio { get; set; }
        public string doxpc_numero_declaracion { get; set; }
        public decimal MontoDUA { get; set; }
        public decimal MontoTotalDUA { get; set; }
        /*PROVEEDOR*/
        public int proc_pais_nodomic { get; set; }
        public string proc_vdireccion { get; set; }
        public string proc_vdni { get; set; }
        public decimal doxpc_otros_impuestos { get; set; }
    }
}
