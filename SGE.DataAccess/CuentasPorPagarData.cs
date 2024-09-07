using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace SGE.DataAccess
{
    public class CuentasPorPagarData
    {
        public int getSituacionDocPorPagar(Int64 int_dxp)
        {
            int? id_situacion = 0;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_GET_SITUACION_DXP(ref id_situacion, int_dxp);
                }
                return Convert.ToInt32(id_situacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #region Doc x Pagar
        public List<EDocPorPagar> ListarEDocPorPagarPendientes(int intAnio)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_LISTAR_PENDIENTES(intAnio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            doxpc_iid_correlativo = Convert.ToInt64(item.doxpc_iid_correlativo),
                            doxpc_viid_correlativo = String.Format("{0:0000000}", item.doxpc_iid_correlativo),
                            anio = Convert.ToInt32(item.anioc_iid_anio),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            tdodc_iid_correlativo = item.tdocd_iid_correlativo,
                            clase_viid_correlativo = string.Format("{0:00}", item.tdocd_iid_correlativo),
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_nmonto_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_destino_gravado),
                            doxpc_nmonto_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_destino_mixto),
                            doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado,
                            doxpc_nmonto_referencial_cif = item.doxpc_nmonto_referencial_cif,
                            doxpc_nmonto_servicio_no_domic = item.doxpc_nmonto_servicio_no_domic,
                            doxpc_nmonto_imp_destino_gravado = item.doxpc_nmonto_imp_destino_gravado,
                            doxpc_nmonto_imp_destino_mixto = item.doxpc_nmonto_imp_destino_mixto,
                            doxpc_nmonto_imp_destino_nogravado = item.doxpc_nmonto_imp_destino_nogravado,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            doxpc_nporcentaje_igv = item.doxpc_nporcentaje_igv,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            doxpc_tipo_comprobante_referencia = item.doxpc_tipo_comprobante_referencia,
                            doxpc_num_serie_referencia = item.doxpc_num_serie_referencia,
                            doxpc_num_comprobante_referencia = item.doxpc_num_comprobante_referencia,
                            doxpc_sfecha_emision_referencia = item.doxpc_sfecha_emision_referencia,
                            //doxpc_nporcentaje_isc = item.doxpc_nporcentaje_isc,
                            //doxpc_nmonto_isc = item.doxpc_nmonto_isc,
                            doxpc_vnro_deposito_detraccion = item.doxpc_vnro_deposito_detraccion,
                            doxpc_sfec_deposito_detraccion = item.doxpc_sfec_deposito_detraccion,
                            vSituacion = (item.tablc_iid_situacion_documento == 8) ? "Pen" : "Can",
                            vMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "$.",
                            proc_iid_proveedor = string.Format("{0:00}", item.proc_iid_proveedor),
                            doxpc_origen = item.doxpc_origen,
                            proc_vcod_proveedor = item.proc_vcod_proveedor
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListarDocumentoPorPagarTodo(int intEjercicio)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_LISTAR_TODO(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            vMoneda = item.Moneda,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            MontoDolares = Convert.ToDecimal(item.MontoDolares),
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            SaldoDolares = Convert.ToDecimal(item.SaldoDolares),
                            proc_vcta_bco_nacion = item.proc_vcta_bco_nacion,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            anac_icod_analitica = item.anad_icod_analitica,
                            anac_iid_analitica = item.anad_iid_analitica,
                            tipo_analitica = item.tipo_analitica,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            doxpc_nmonto_total_pagado = Convert.ToDecimal(item.doxpc_nmonto_total_pagado)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListarDocumentoPorPagarProveedor(int intProveedor, int intEjercicio)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_LISTAR_POR_PROVEEDOR(intProveedor, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            vMoneda = item.Moneda,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            MontoDolares = Convert.ToDecimal(item.MontoDolares),
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            SaldoDolares = Convert.ToDecimal(item.SaldoDolares),
                            proc_vcta_bco_nacion = item.proc_vcta_bco_nacion,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            anac_icod_analitica = item.anad_icod_analitica,
                            anac_iid_analitica = item.anad_iid_analitica,
                            tipo_analitica = item.tipo_analitica,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            doxpc_nmonto_total_pagado = Convert.ToDecimal(item.doxpc_nmonto_total_pagado),
                            doxpc_nmonto_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_destino_gravado),
                            doxpc_nmonto_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_destino_mixto),
                            doxpc_nmonto_nogravado = Convert.ToDecimal(item.doxpc_nmonto_nogravado),
                            doxpc_nmonto_destino_nogravado = Convert.ToDecimal(item.doxpc_nmonto_destino_nogravado),
                            doxpc_nmonto_imp_destino_gravado = item.doxpc_nmonto_imp_destino_gravado,
                            doxpc_nmonto_imp_destino_mixto = item.doxpc_nmonto_imp_destino_mixto,
                            doxpc_nmonto_imp_destino_nogravado = item.doxpc_nmonto_imp_destino_nogravado
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long getCorrelativoDocPorPagar(int intEjercicio, int intPeriodo)
        {
            long? lngCorrelativo = 0;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_GET_CORRELATIVO(ref lngCorrelativo, intEjercicio, intPeriodo);
                }
                return Convert.ToInt64(lngCorrelativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocPorPagar> listarDocPorPagar(EDocPorPagar oBe)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_LISTAR(oBe.anio, oBe.mesec_iid_mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            doxpc_iid_correlativo = Convert.ToInt64(item.doxpc_iid_correlativo), //correlativo del dxp
                            doxpc_viid_correlativo = String.Format("{0:00000}", item.doxpc_iid_correlativo), //código pero con formato para mostrarlo en pantalla
                            anio = Convert.ToInt32(item.anioc_iid_anio),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            tdodc_iid_correlativo = item.tdocd_iid_correlativo,
                            clase_viid_correlativo = string.Format("{0:00}", item.tdodc_iid_codigo_doc_det),
                            tdodc_descripcion = item.tdocd_descripcion,
                            doxpc_vnumero_serio = item.doxpc_vnumero_serio,
                            doxpc_vnumero_correlativo = item.doxpc_vnumero_correlativo,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_nmonto_destino_gravado = Convert.ToDecimal(item.doxpc_nmonto_destino_gravado),
                            doxpc_nmonto_destino_mixto = Convert.ToDecimal(item.doxpc_nmonto_destino_mixto),
                            doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado,
                            doxpc_nmonto_referencial_cif = item.doxpc_nmonto_referencial_cif,
                            doxpc_nmonto_servicio_no_domic = item.doxpc_nmonto_servicio_no_domic,
                            doxpc_nmonto_imp_destino_gravado = item.doxpc_nmonto_imp_destino_gravado,
                            doxpc_nmonto_imp_destino_mixto = item.doxpc_nmonto_imp_destino_mixto,
                            doxpc_nmonto_imp_destino_nogravado = item.doxpc_nmonto_imp_destino_nogravado,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            doxpc_nporcentaje_igv = item.doxpc_nporcentaje_igv,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,

                            doxpc_tipo_comprobante_referencia = item.doxpc_tipo_comprobante_referencia,
                            doxpc_num_serie_referencia = item.doxpc_num_serie_referencia,
                            doxpc_num_comprobante_referencia = item.doxpc_num_comprobante_referencia,
                            doxpc_sfecha_emision_referencia = item.doxpc_sfecha_emision_referencia,

                            doxpc_vnro_deposito_detraccion = item.doxpc_vnro_deposito_detraccion,
                            doxpc_sfec_deposito_detraccion = item.doxpc_sfec_deposito_detraccion,
                            vSituacion = item.situacion_vdescripcion,
                            vMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "$.",
                            proc_iid_proveedor = string.Format("{0:000}", item.proc_iid_proveedor),
                            doxpc_origen = item.doxpc_origen,
                            doxpc_nporcentaje_imp_renta = item.doxpc_nporcentaje_imp_renta,
                            TipoRegistro = item.TipoRegistro,
                            doxpc_nmonto_nogravado = item.doxpc_nmonto_nogravado,
                            doxpc_nmonto_retencion_rh = item.doxpc_nmonto_retencion_rh,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            doxpc_nmonto_rho_dolar = (item.tablc_iid_tipo_moneda == 4) ? ((item.doxpc_nmonto_destino_gravado != 0) ? item.doxpc_nmonto_destino_gravado : item.doxpc_nmonto_destino_nogravado).ToString() : string.Empty,
                            tdodc_iestado_registro = item.tdocd_iestado_registro,
                            doxpc_numdoc_tipo = item.doxpc_numdoc_tipo,
                            proc_analitica_icod = Convert.ToInt32(item.proc_analitica_icod),
                            Valorcompra = Convert.ToDecimal(item.doxpc_nmonto_subtotal),
                            cdxpc_suma_monto = item.cdxpc_suma_monto,
                            percc_icod_percepcion = Convert.ToInt32(item.percc_icod_percepcion),
                            doxpc_itipo_adquisicion = Convert.ToInt32(item.doxpc_itipo_adquisicion),
                            doxpc_icod_documento = Convert.ToInt32(item.doxpc_icod_documento),
                            doxpc_codigo_aduana = item.doxpc_codigo_aduana,
                            doxpc_anio = item.doxpc_anio,
                            doxpc_numero_declaracion = item.doxpc_numero_declaracion,
                            anac_icod_analitica = item.anad_icod_analitica,
                            TipoAnalitica = Convert.ToInt32(item.TipoAnalitica),
                            NumAnalitica = item.NumAnalitica,
                            tablc_iid_clasificacion_compras = Convert.ToInt32(item.tablc_iid_clasificacion_compras),
                            ClasificacionCompras = item.ClasificacionCompras,
                            ocsc_icod_ocs = Convert.ToInt32(item.ocsc_icod_ocs),
                            NumOCS = item.NumOCS,
                            proc_vruc = item.proc_vruc

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long insertarDocPorPagar(EDocPorPagar oBe)
        {
            long Code = 0;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    long? intId = 0;
                    dc.SGEDXP_DOC_X_PAGAR_INSERTAR(
                                  ref intId,
                                    oBe.anio,
                                    oBe.mesec_iid_mes,
                                    oBe.doxpc_iid_correlativo,
                                    oBe.tdocc_icod_tipo_doc,
                                    oBe.tdodc_iid_correlativo,
                                    oBe.doxpc_vnumero_serio,
                                    oBe.doxpc_vnumero_correlativo,
                                    oBe.doxpc_vnumero_doc,
                                    oBe.doxpc_sfecha_doc,
                                    oBe.doxpc_sfecha_vencimiento_doc,
                                    oBe.proc_icod_proveedor,
                                    oBe.tablc_iid_tipo_moneda,
                                    oBe.doxpc_nmonto_tipo_cambio,
                                    oBe.doxpc_vdescrip_transaccion,
                                    oBe.doxpc_nmonto_destino_gravado,
                                    oBe.doxpc_nmonto_destino_mixto,
                                    oBe.doxpc_nmonto_destino_nogravado,
                                    oBe.doxpc_nmonto_referencial_cif,
                                    oBe.doxpc_nmonto_servicio_no_domic,
                                    oBe.doxpc_nmonto_imp_destino_gravado,
                                    oBe.doxpc_nmonto_imp_destino_mixto,
                                    oBe.doxpc_nmonto_imp_destino_nogravado,
                                    oBe.doxpc_nmonto_retenido,
                                    oBe.doxpc_nmonto_total_documento,
                                    oBe.doxpc_nmonto_total_pagado,
                                    oBe.doxpc_nmonto_total_saldo,
                                    oBe.doxpc_nporcentaje_igv,
                                    oBe.tablc_iid_situacion_documento,
                                    oBe.doxpc_tipo_comprobante_referencia,
                                    oBe.doxpc_num_serie_referencia,
                                    oBe.doxpc_num_comprobante_referencia,
                                    oBe.doxpc_sfecha_emision_referencia,
                                    oBe.doxpc_vnro_deposito_detraccion,
                                    oBe.doxpc_sfec_deposito_detraccion,
                                    oBe.intUsuario,
                                    oBe.strPc,
                                    oBe.doxpc_origen,
                                    oBe.doxpc_flag_estado,
                                    oBe.doxpc_nmonto_nogravado,
                                    oBe.doxpc_numdoc_tipo,
                                    oBe.doxpc_nporcentaje_imp_renta,
                                    oBe.doxpc_nmonto_retencion_rh,
                                    oBe.doxpc_itipo_adquisicion,
                                    oBe.doxpc_icod_documento,
                                    oBe.doxpc_codigo_aduana,
                                    oBe.doxpc_anio,
                                    oBe.doxpc_numero_declaracion,
                                    oBe.tablc_iid_clasificacion_compras,
                                    oBe.ocsc_icod_ocs
                        );
                    Code = Convert.ToInt64(intId);
                    if (Code == 0)
                    {
                        throw new Exception("El tipo y número de documento ya fue utilizado para el proveedor seleccionado");
                    }

                }
                if (Code == 0)
                {
                    throw new Exception("Se repite el tipo,número de documento");
                }
                return Code;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarDocPorPagar(EDocPorPagar oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {

                    dc.SGEDXP_DOC_X_PAGAR_ACTUALIZAR(
                              oBe.doxpc_icod_correlativo,
                              oBe.doxpc_sfecha_doc,
                              oBe.doxpc_sfecha_vencimiento_doc,
                              oBe.doxpc_nmonto_tipo_cambio,
                              oBe.doxpc_vdescrip_transaccion,
                              oBe.doxpc_nmonto_destino_gravado,
                              oBe.doxpc_nmonto_destino_mixto,
                              oBe.doxpc_nmonto_destino_nogravado,
                              oBe.doxpc_nmonto_referencial_cif,
                              oBe.doxpc_nmonto_servicio_no_domic,
                              oBe.doxpc_nmonto_imp_destino_gravado,
                              oBe.doxpc_nmonto_imp_destino_mixto,
                              oBe.doxpc_nmonto_imp_destino_nogravado,
                              oBe.doxpc_nmonto_total_documento,
                              oBe.doxpc_nmonto_total_pagado,
                              oBe.doxpc_nmonto_total_saldo,
                              oBe.doxpc_nporcentaje_igv,
                              oBe.doxpc_tipo_comprobante_referencia,
                              oBe.doxpc_num_serie_referencia,
                              oBe.doxpc_num_comprobante_referencia,
                              oBe.doxpc_sfecha_emision_referencia,
                              oBe.doxpc_vnro_deposito_detraccion,
                              oBe.doxpc_sfec_deposito_detraccion,
                              oBe.intUsuario,
                              oBe.strPc,
                              oBe.doxpc_nmonto_nogravado,
                              oBe.doxpc_nporcentaje_imp_renta,
                              oBe.doxpc_itipo_adquisicion,
                              oBe.doxpc_icod_documento,
                              oBe.doxpc_codigo_aduana,
                              oBe.doxpc_anio,
                              oBe.doxpc_numero_declaracion,
                              oBe.tablc_iid_clasificacion_compras,
                              oBe.doxpc_iid_correlativo
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarDocPorPagar(EDocPorPagar oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {

                    dc.SGEDXP_DOC_X_PAGAR_ELIMINAR(
                        oBe.doxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc
                    );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region PAGOS DE DXP
        public List<EDocPorPagarPago> listarDxpPagos(long intDXP, int Anio)
        {
            List<EDocPorPagarPago> lista = new List<EDocPorPagarPago>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_PAGO_LISTAR(intDXP, Anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagarPago()
                        {
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo),
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            pdxpc_vnumero_doc = item.pdxpc_vnumero_doc,
                            pdxpc_sfecha_pago = item.pdxpc_sfecha_pago,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            pdxpc_nmonto_pago = item.pdxpc_nmonto_pago,
                            pdxpc_nmonto_tipo_cambio = item.pdxpc_nmonto_tipo_cambio,
                            pdxpc_vobservacion = item.pdxpc_vobservacion,
                            efctc_icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            pdxpc_vorigen = item.pdxpc_vorigen,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            ctacc_iid_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            DescripcionCuentaContable = item.DescripcionCuentaContable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            cecoc_ccodigo_centro_costo = item.cecoc_ccodigo_centro_costo,
                            anac_icod_analitica = item.anad_icod_analitica,
                            anac_viid_analitica = item.anad_iid_analitica,
                            anac_vdescripcion = item.anad_vdescripcion,
                            TipoAnalitica = item.TipoAnalitica,
                            Moneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            CuentaContable = item.ctacc_numero_cuenta_contable,
                            EntidadFinanciera = item.EntidadFinanciera,
                            intTipoDoc = item.intTipoDoc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            numero_doc_dxp = item.doc_dxp,
                            moneda_dxp = item.moneda_dxp,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            IcodTD = Convert.ToInt32(item.IcodTD),
                            TDDXC = item.TDDXC,
                            IcodDXC = Convert.ToInt32(item.IcodDXC),
                            NumDXC = item.NumDXC,
                            IcodProveedorDXP = Convert.ToInt32(item.IcodProveedorDXP),
                            MonDXC = Convert.ToInt32(item.MonDXC)

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagarPago> listarDxpPagos2(int Mes)
        {
            List<EDocPorPagarPago> lista = new List<EDocPorPagarPago>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_PAGO_LISTAR2(Mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagarPago()
                        {
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo),
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            pdxpc_vnumero_doc = item.pdxpc_vnumero_doc,
                            pdxpc_sfecha_pago = item.pdxpc_sfecha_pago,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            pdxpc_nmonto_pago = item.pdxpc_nmonto_pago,
                            pdxpc_nmonto_tipo_cambio = item.pdxpc_nmonto_tipo_cambio,
                            pdxpc_vobservacion = item.pdxpc_vobservacion,
                            efctc_icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            pdxpc_vorigen = item.pdxpc_vorigen,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            ctacc_iid_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            cecoc_ccodigo_centro_costo = item.cecoc_ccodigo_centro_costo,
                            anac_icod_analitica = item.anad_icod_analitica,
                            anac_viid_analitica = item.anad_iid_analitica,
                            anac_vdescripcion = item.anad_vdescripcion,
                            TipoAnalitica = item.TipoAnalitica,
                            Moneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            CuentaContable = item.ctacc_numero_cuenta_contable,
                            EntidadFinanciera = item.EntidadFinanciera,
                            intTipoDoc = item.intTipoDoc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            numero_doc_dxp = item.doc_dxp,
                            moneda_dxp = item.moneda_dxp,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            IcodTD = Convert.ToInt32(item.IcodTD),
                            IddTD = Convert.ToInt32(item.IddTD),
                            TDDXC = item.TDDXC,
                            IcodDXC = Convert.ToInt32(item.IcodDXC),
                            NumDXC = item.NumDXC,
                            IcodProveedorDXP = Convert.ToInt32(item.IcodProveedorDXP),
                            IcodCleinteDXC = Convert.ToInt32(item.IcodCleinteDXC),
                            MonDXC = Convert.ToInt32(item.MonDXC)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long insertarDocPorPagarPago(EDocPorPagarPago oBe)
        {
            try
            {
                long? IdDocumentoPorPagarPago = 0;
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_PAGO_INSERTAR(
                                 ref IdDocumentoPorPagarPago,
                                  oBe.doxpc_icod_correlativo,
                                  oBe.tdocc_icod_tipo_doc,
                                  oBe.pdxpc_vnumero_doc,
                                  oBe.pdxpc_sfecha_pago,
                                  oBe.tablc_iid_tipo_moneda,
                                  oBe.pdxpc_nmonto_pago,
                                  oBe.pdxpc_nmonto_tipo_cambio,
                                  oBe.pdxpc_vobservacion,
                                  oBe.efctc_icod_enti_financiera_cuenta,
                                  oBe.pdxpc_vorigen,
                                  oBe.doxcc_icod_correlativo,
                                  oBe.ctacc_iid_cuenta_contable,
                                  oBe.cecoc_icod_centro_costo,
                                  oBe.anac_icod_analitica,
                                  oBe.intUsuario,
                                  oBe.strPc,
                                  oBe.pdxpc_mes,
                                  oBe.anio,
                                  oBe.pdxpc_flag_estado
                      );
                }

                return Convert.ToInt64(IdDocumentoPorPagarPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarDocPorPagarPago(EDocPorPagarPago oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {

                    dc.SGEDXP_DOC_X_PAGAR_PAGO_ACTUALIZAR(
                                oBe.pdxpc_icod_correlativo,
                                oBe.pdxpc_sfecha_pago,
                                oBe.pdxpc_nmonto_pago,
                                oBe.pdxpc_nmonto_tipo_cambio,
                                oBe.pdxpc_vobservacion,
                                oBe.efctc_icod_enti_financiera_cuenta,
                                oBe.pdxpc_vorigen,
                                oBe.ctacc_iid_cuenta_contable,
                                oBe.cecoc_icod_centro_costo,
                                oBe.anac_icod_analitica,
                                oBe.intUsuario,
                                oBe.strPc,
                                oBe.pdxpc_mes,
                                oBe.anio
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarDocPorPagarPago(EDocPorPagarPago oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_PAGO_ELIMINAR(
                        oBe.pdxpc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Pagos de Adelantos


        public List<EDocxPagarPagoAdelanto> ListarAdelantoPago(long doxpc_icod_correlativo_pago, long doxpc_icod_correlativo_adelanto, int anio)
        {
            /*Gerson*/
            List<EDocxPagarPagoAdelanto> lista = new List<EDocxPagarPagoAdelanto>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_ADELANTO_PAGO_LISTAR(doxpc_icod_correlativo_pago, doxpc_icod_correlativo_adelanto, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocxPagarPagoAdelanto()
                        {
                            adpap_icod_correlativo = item.adpap_icod_correlativo,
                            doxpc_icod_correlativo_pago = item.doxpc_icod_correlativo_pago,
                            doxpc_icod_correlativo_adelanto = item.doxpc_icod_correlativo_adelanto,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),

                            AbreviaturaAdelanto = item.AbreviaturaAdelanto,
                            AbreviaturaPago = item.AbreviaturaPago,

                            Vcorrelativo_Pago = item.Vcorrelativo_Pago,
                            Vcorrelativo_adelanto = item.Vcorrelativo_adelanto,

                            vnumero_adelanto = item.vnumero_adelanto,
                            vnumero_pago = item.vnumero_pago,

                            SaldoDXCAdelanto = item.SaldoDXCAdelanto,
                            id_tipo_moneda_adelanto = item.id_tipo_moneda_adelanto,
                            id_tipo_moneda_pago = item.id_tipo_moneda_pago,
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.id_tipo_moneda_adelanto == 3) ? "S/." : "US$",
                            adpap_nmonto_pago = item.adpap_nmonto_pago,
                            adpap_nmonto_tipo_cambio = item.adpap_nmonto_tipo_cambio,
                            adpap_vdescripcion = item.adpap_vdescripcion,
                            adpap_sfecha_pago = item.adpap_sfecha_pago,
                            adpap_iorigen = item.adpap_iorigen,
                            adpap_iusuario_crea = item.adpap_iusuario_crea,
                            adpap_vpc_crea = item.adpap_vpc_crea,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            intProveedor = Convert.ToInt32(item.intProveedor),
                            mobac_icod_correlativo = item.mobac_icod_correlativo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EDocxPagarPagoAdelanto> ListarAdelantoPago2(int mes)
        {
            List<EDocxPagarPagoAdelanto> lista = new List<EDocxPagarPagoAdelanto>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_ADELANTO_PAGO_LISTAR2(mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocxPagarPagoAdelanto()
                        {
                            adpap_icod_correlativo = item.adpap_icod_correlativo,
                            doxpc_icod_correlativo_pago = item.doxpc_icod_correlativo_pago,
                            doxpc_icod_correlativo_adelanto = item.doxpc_icod_correlativo_adelanto,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),

                            AbreviaturaAdelanto = item.AbreviaturaAdelanto,
                            AbreviaturaPago = item.AbreviaturaPago,

                            Vcorrelativo_Pago = item.Vcorrelativo_Pago,
                            Vcorrelativo_adelanto = item.Vcorrelativo_adelanto,

                            vnumero_adelanto = item.vnumero_adelanto,
                            vnumero_pago = item.vnumero_pago,

                            SaldoDXCAdelanto = item.SaldoDXCAdelanto,
                            id_tipo_moneda_adelanto = item.id_tipo_moneda_adelanto,
                            id_tipo_moneda_pago = item.id_tipo_moneda_pago,
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.id_tipo_moneda_adelanto == 3) ? "S/." : "US$",
                            adpap_nmonto_pago = item.adpap_nmonto_pago,
                            adpap_nmonto_tipo_cambio = item.adpap_nmonto_tipo_cambio,
                            adpap_vdescripcion = item.adpap_vdescripcion,
                            adpap_sfecha_pago = item.adpap_sfecha_pago,
                            adpap_iorigen = item.adpap_iorigen,
                            adpap_iusuario_crea = item.adpap_iusuario_crea,
                            adpap_vpc_crea = item.adpap_vpc_crea,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            intProveedor = Convert.ToInt32(item.intProveedor),
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            intProveedorDXP = Convert.ToInt32(item.intProveedorDXP)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long insertarAdelantoPago(EDocxPagarPagoAdelanto obj)
        {
            try
            {
                long? IdAdelantoPago = 0;
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {

                    dc.SGEDXP_DOC_X_PAGAR_ADELANTO_PAGO_INSERTAR(ref IdAdelantoPago,
                        obj.doxpc_icod_correlativo_pago,
                        obj.doxpc_icod_correlativo_adelanto,
                        obj.id_tipo_moneda_adelanto,
                        obj.adpap_nmonto_pago,
                        obj.adpap_nmonto_tipo_cambio,
                        obj.adpap_vdescripcion,
                        obj.adpap_sfecha_pago,
                        obj.adpap_iorigen,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.adpap_iusuario_crea,
                        obj.adpap_vpc_crea,
                        obj.pdxpc_icod_correlativo,
                        obj.adpap_flag_estado,
                        obj.anio,
                        obj.mobac_icod_correlativo
                        );
                }

                return Convert.ToInt64(IdAdelantoPago);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAdelantoPago(EDocxPagarPagoAdelanto obj)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_ADELANTO_PAGO_ACTUALIZAR(
                        obj.adpap_icod_correlativo,
                        obj.adpap_nmonto_pago,
                        obj.adpap_nmonto_tipo_cambio,
                        obj.adpap_vdescripcion,
                        obj.adpap_sfecha_pago,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.adpap_iusuario_modifica,
                        obj.adpap_vpc_modifica
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAdelantoPago(EDocxPagarPagoAdelanto obj)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_ADELANTO_PAGO_ELIMINAR(
                        obj.adpap_icod_correlativo,
                        obj.adpap_vpc_modifica,
                        obj.adpap_iusuario_modifica);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Pagos de Notas de Crédito
        public List<EDocPorPagarNotaCredito> listarDxpPagosNc(long doxpc_icod_correlativo_pago, long doxpc_icod_correlativo_NC, int anio)
        {
            List<EDocPorPagarNotaCredito> lista = new List<EDocPorPagarNotaCredito>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_NOTA_CREDITO_PAGO_LISTAR(doxpc_icod_correlativo_pago, doxpc_icod_correlativo_NC, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagarNotaCredito()
                        {
                            ncpap_icod_correlativo = item.ncpap_icod_correlativo,
                            doxpc_icod_correlativo_pago = item.doxpc_icod_correlativo_pago,
                            doxpc_icod_correlativo_nota_credito = item.doxpc_icod_correlativo_nota_credito,

                            tipo_documento_nota_credito = item.tipo_documento_nota_credito,
                            tipo_documento_pago = Convert.ToInt32(item.tipo_documento_pago),

                            AbreviaturaNotaCredito = item.AbreviaturaNotaCredito,
                            AbreviaturaPago = item.AbreviaturaPago,

                            idd_correlativo_nota_credito = item.idd_correlativo_nota_credito,
                            idd_correlativo_pago = item.idd_correlativo_pago,

                            doc_vnumero_nota_credito = item.doc_vnumero_nota_credito,
                            doc_vnumero_pago = item.doc_vnumero_pago,

                            SaldoDXCNotaCredito = item.SaldoDXCNotaCredito,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            iid_moneda_nota_credito = item.iid_moneda_nota_credito,
                            iid_moneda_pago = item.iid_moneda_pago,
                            SimboloMoneda = (item.iid_moneda_nota_credito == 3) ? "S/." : "US$",
                            ncpap_nmonto_pago = item.ncpap_nmonto_pago,
                            ncpap_nmonto_tipo_cambio = item.ncpap_nmonto_tipo_cambio,
                            ncpap_vdescripcion = item.ncpap_vdescripcion,
                            ncpap_sfecha_pago = item.ncpap_sfecha_pago,
                            ncpap_iorigen = item.ncpap_iorigen,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            intProveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            mobac_icod_correlativo = item.mobac_icod_correlativo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagarNotaCredito> listarDxpPagosNc2(int periodo)
        {
            List<EDocPorPagarNotaCredito> lista = new List<EDocPorPagarNotaCredito>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXP_DOC_X_PAGAR_NOTA_CREDITO_PAGO_LISTAR2(periodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagarNotaCredito()
                        {
                            ncpap_icod_correlativo = item.ncpap_icod_correlativo,
                            doxpc_icod_correlativo_pago = item.doxpc_icod_correlativo_pago,
                            doxpc_icod_correlativo_nota_credito = item.doxpc_icod_correlativo_nota_credito,

                            tipo_documento_nota_credito = item.tipo_documento_nota_credito,
                            tipo_documento_pago = item.tipo_documento_pago,

                            AbreviaturaNotaCredito = item.AbreviaturaNotaCredito,
                            AbreviaturaPago = item.AbreviaturaPago,

                            idd_correlativo_nota_credito = item.idd_correlativo_nota_credito,
                            idd_correlativo_pago = item.idd_correlativo_pago,

                            doc_vnumero_nota_credito = item.doc_vnumero_nota_credito,
                            doc_vnumero_pago = item.doc_vnumero_pago,

                            SaldoDXCNotaCredito = item.SaldoDXCNotaCredito,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            iid_moneda_nota_credito = item.iid_moneda_nota_credito,
                            iid_moneda_pago = item.iid_moneda_pago,
                            SimboloMoneda = (item.iid_moneda_nota_credito == 3) ? "S/." : "US$",
                            ncpap_nmonto_pago = item.ncpap_nmonto_pago,
                            ncpap_nmonto_tipo_cambio = item.ncpap_nmonto_tipo_cambio,
                            ncpap_vdescripcion = item.ncpap_vdescripcion,
                            ncpap_sfecha_pago = item.ncpap_sfecha_pago,
                            ncpap_iorigen = item.ncpap_iorigen,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            intProveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            TDEntidadFinanciera = Convert.ToInt32(item.TDEntidadFinanciera),
                            AbreviaturaEF = item.AbreviaturaEF,
                            NumDOCEF = item.NumDOCEF,
                            GlosaEF = item.GlosaEF,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            CuentaBanco = Convert.ToInt32(item.CuentaBanco)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long insertarNotaCreditoPago(EDocPorPagarNotaCredito obj)
        {
            try
            {
                long? IdNotaCreditoPago = 0;
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {

                    dc.SGEDXP_DOC_X_PAGAR_NOTA_CREDITO_PAGO_INSERTAR(
                        ref IdNotaCreditoPago,
                        obj.doxpc_icod_correlativo_pago,
                        obj.doxpc_icod_correlativo_nota_credito,
                        obj.iid_moneda_nota_credito,
                        obj.ncpap_nmonto_pago,
                        obj.ncpap_nmonto_tipo_cambio,
                        obj.ncpap_vdescripcion,
                        obj.ncpap_sfecha_pago,
                        obj.ncpap_iorigen,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.intUsuario,
                        obj.strPc,
                        obj.pdxpc_icod_correlativo,
                        obj.ncpap_flag_estado,
                        obj.anio,
                        obj.mobac_icod_correlativo
                        );
                }

                return Convert.ToInt64(IdNotaCreditoPago);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarNotaCreditoPago(EDocPorPagarNotaCredito obj)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_NOTA_CREDITO_PAGO_ACTUALIZAR(
                        obj.ncpap_icod_correlativo,
                        obj.iid_moneda_nota_credito,
                        obj.ncpap_nmonto_pago,
                        obj.ncpap_nmonto_tipo_cambio,
                        obj.ncpap_vdescripcion,
                        obj.ncpap_sfecha_pago,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.intUsuario,
                        obj.strPc,
                        obj.anio,
                        obj.mobac_icod_correlativo
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNotaCreditoPago(EDocPorPagarNotaCredito obj)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEDXP_DOC_X_PAGAR_NOTA_CREDITO_PAGO_ELIMINAR(
                        obj.ncpap_icod_correlativo,
                        obj.strPc,
                        obj.intUsuario);

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Doc x Pagar Detalle Cuenta Contable
        public List<EDocPorPagarDetalleCuentaContable> listarDXPDetCtaContable(long IdDocumentoPorPagar)
        {
            List<EDocPorPagarDetalleCuentaContable> lista = new List<EDocPorPagarDetalleCuentaContable>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGT_DOC_X_PAGAR_CUENTA_CONTABLE_LISTAR(IdDocumentoPorPagar);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagarDetalleCuentaContable()
                        {
                            cdxpc_icod_correlativo = item.cdxpc_icod_correlativo,
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            ctacc_iid_cuenta_contable = Convert.ToInt64(item.ctacc_iid_cuenta_contable),
                            NumeroCuentaContable = item.NumeroCuentaContable.ToString(),
                            DescripcionCuentaContable = item.DescripcionCuentaContable,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            CodigoCentroCosto = (item.cecoc_icod_centro_costo == null || item.cecoc_icod_centro_costo == 0) ? "  -  " : item.CodigoCentroCosto,
                            DescripcionCentroCosto = item.DescripcionCentroCosto,
                            ctacc_vnumero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            IdTipoAnalitica = (Convert.ToInt32(item.IdTipoAnalitica) == 0) ? "" : string.Format("{0:00}", item.IdTipoAnalitica),
                            TipoAnalitica = item.TipoAnalitica,
                            NumeroAnalitica = (item.IdTipoAnalitica == null || item.IdTipoAnalitica == 0) ? "  -  " : item.NumeroAnalitica,
                            cdxpc_nmonto_cuenta = Convert.ToDecimal(item.cdxpc_nmonto_cuenta),
                            cdxpc_vglosa = item.cdxpc_vglosa,
                            cdxpc_flag_estado = Convert.ToBoolean(item.cdxpc_flag_estado),
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            doxpc_icod_correlativo_dxp = item.doxpc_icod_correlativo_dxp,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            TipOper = 4, //indicador de Consulta,
                            prep_icod_presupuesto = item.prep_icod_presupuesto,
                            prepd_icod_detalle = item.prepd_icod_detalle,
                            prep_cod_presupuesto = item.prep_cod_presupuesto,
                            cpnd_vdescripcion = item.cpnd_vdescripcion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long insertarDXPDetCtaContable(EDocPorPagarDetalleCuentaContable obj)
        {
            try
            {
                long? IdDetalleCuentaContable = 0;

                if (obj.cecoc_icod_centro_costo == 0)
                {
                    obj.cecoc_icod_centro_costo = null;
                }
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SIGT_DOC_X_PAGAR_CUENTA_CONTABLE_INSERTAR(
                        ref IdDetalleCuentaContable,
                        obj.doxpc_icod_correlativo,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cecoc_icod_centro_costo,
                        obj.anac_icod_analitica,
                        obj.cdxpc_nmonto_cuenta,
                        obj.cdxpc_vglosa,
                        obj.intUsuario,
                        obj.strPc,
                        obj.cdxpc_flag_estado,
                        obj.pdxpc_icod_correlativo,
                        obj.prep_icod_presupuesto,
                        obj.prepd_icod_detalle
                        );
                }

                return Convert.ToInt64(IdDetalleCuentaContable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDXPDetCtaContable(EDocPorPagarDetalleCuentaContable obj)
        {
            try
            {
                if (obj.cecoc_icod_centro_costo == 0)
                {
                    obj.cecoc_icod_centro_costo = null;
                }
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SIGT_DOC_X_PAGAR_CUENTA_CONTABLE_MODIFICAR(
                        obj.cdxpc_icod_correlativo,
                        obj.doxpc_icod_correlativo,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cecoc_icod_centro_costo,
                        obj.anac_icod_analitica,
                        obj.cdxpc_nmonto_cuenta,
                        obj.cdxpc_vglosa,
                        obj.intUsuario,
                        obj.strPc,
                        obj.cdxpc_flag_estado,
                        obj.prep_icod_presupuesto,
                        obj.prepd_icod_detalle
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDXPDetCtaContable(List<EDocPorPagarDetalleCuentaContable> lstDelete, EFacturaCompra obj)
        {
            TesoreriaData objTesoreriaData = new TesoreriaData();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    lstDelete.ForEach(x =>
                    {
                        /**En caso de que exista pagos se eliminan los pagos**/
                        if (Convert.ToInt64(x.pdxpc_icod_correlativo) > 0)
                        {
                            EDocPorPagarPago obePago = new EDocPorPagarPago();
                            obePago.pdxpc_icod_correlativo = Convert.ToInt64(x.pdxpc_icod_correlativo);
                            obePago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo_dxp);
                            //obePago.strPc = obj.strPc;
                            //obePago.intUsuario = obj.intUsuario;
                            obePago.saldoDxP = Convert.ToDecimal(x.doxpc_nmonto_total_saldo);
                            obePago.pagoDxP = Convert.ToDecimal(x.doxpc_nmonto_total_pagado);
                            new CuentasPorPagarData().eliminarDocPorPagarPago(obePago);
                        }
                        /**Se eliminan los datos del detalle**/
                        dc.SIGT_DOC_X_PAGAR_CUENTA_CONTABLE_ELIMINAR(x.cdxpc_icod_correlativo, x.intUsuario, x.strPc);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDXPDetCtaContableBC(List<EDocPorPagarDetalleCuentaContable> lstDelete, EBoletaCompra obj)
        {
            TesoreriaData objTesoreriaData = new TesoreriaData();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    lstDelete.ForEach(x =>
                    {
                        /**En caso de que exista pagos se eliminan los pagos**/
                        if (Convert.ToInt64(x.pdxpc_icod_correlativo) > 0)
                        {
                            EDocPorPagarPago obePago = new EDocPorPagarPago();
                            obePago.pdxpc_icod_correlativo = Convert.ToInt64(x.pdxpc_icod_correlativo);
                            obePago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo_dxp);
                            //obePago.strPc = obj.strPc;
                            //obePago.intUsuario = obj.intUsuario;
                            obePago.saldoDxP = Convert.ToDecimal(x.doxpc_nmonto_total_saldo);
                            obePago.pagoDxP = Convert.ToDecimal(x.doxpc_nmonto_total_pagado);
                            new CuentasPorPagarData().eliminarDocPorPagarPago(obePago);
                        }
                        /**Se eliminan los datos del detalle**/
                        dc.SIGT_DOC_X_PAGAR_CUENTA_CONTABLE_ELIMINAR(x.cdxpc_icod_correlativo, x.intUsuario, x.strPc);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 

        public bool VerificarExistenPagos(long? doxcc_icod_correlativo, Int32? tdocc_icod_tipo_doc)
        {
            bool? resultado = false;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SIGTS_DOC_X_PAGAR_VERIFICAR_EXISTEN_PAGOS(doxcc_icod_correlativo, tdocc_icod_tipo_doc, ref resultado);
                }
                return Convert.ToBoolean(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public EDocXCobrarDocxPagarCanje ListaDatosCanjexIcodDxpPago(long icod_pago)
        {
            List<EDocXCobrarDocxPagarCanje> lista = new List<EDocXCobrarDocxPagarCanje>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_CANJE_DXP_DXC_LISTAR_X_ICOD_DXP_PAGO(icod_pago);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrarDocxPagarCanje()
                        {
                            //DATOS DXC
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo),
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            tipo_doc_dxc = item.tipo_doc_dxc,
                            tipo_doc_abr_dxc = item.tipo_doc_abr_dxc,
                            clase_documento_dxc = item.clase_documento_dxc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            tipo_moneda_dxc = item.tipo_moneda_dxc,
                            simboloMoneda_dxc = (item.tipo_moneda_dxc == 1) ? "S/." : "$",
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,

                            //DATOS DXP
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            tipo_doc_dxp = item.tipo_doc_dxp,
                            tipo_doc_abr_dxp = item.tipo_doc_abr_dxp,
                            clase_documento_dxp = item.clase_documento_dxp,
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tipo_moneda_dxp = item.tipo_moneda_dxp,
                            simboloMoneda_dxp = (item.tipo_moneda_dxp == 1) ? "S/." : "$",
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            voucher_cont_dxp = item.voucher_cont_dxp,

                            //DATOS DXC PAGO
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo,
                            pdxcc_nmonto_cobro = item.pdxcc_nmonto_cobro,

                            //DATOS DXP PAGO
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            pdxpc_nmonto_pago = item.pdxpc_nmonto_pago,

                            //DATOS DEL CANJE
                            //canjec_icod_correlativo = item.canjec_icod_correlativo,
                            //tipo_moneda_canje = item.tipo_moneda_canje,
                            //canjec_nmonto_pago = item.canjec_nmonto_pago,
                            //canjec_vobservacion = item.canjec_vobservacion,
                            //canjec_nmonto_tipo_cambio = item.canjec_nmonto_tipo_cambio,
                            //fecha_pago = item.canjec_sfecha_pago
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista[0];
        }
        public List<EDxPDatosAdicionales> ListaDxPDatosAdicionalesXIcod(long icod)
        {
            List<EDxPDatosAdicionales> lista = new List<EDxPDatosAdicionales>();
            try
            {
                //using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                //{
                //    var query = dc.SIGT_DOC_X_PAGAR_DATOS_ADICIONALES_LISTAR_X_ICOD(icod);
                //    foreach (var item in query)
                //    {
                //        lista.Add(new EDxPDatosAdicionales()
                //        {
                //            condicion = item.condicion,
                //            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                //            doxpc_tipo_compra = item.doxpc_tipo_compra,
                //            tipo_compra_descripcion = item.tipo_compra_descripcion,
                //            doxpc_tipo_comprobante = item.doxpc_tipo_comprobante,
                //            tipo_comprobante_codigo = item.tipo_comprobante_codigo,
                //            tipo_comprobante_descripcion = item.tipo_comprobante_descripcion,
                //            proc_tipo_persona = item.proc_tipo_persona,
                //            tipo_persona_descripcion = item.tipo_persona_descripcion,
                //            doxpc_tipo_documento = item.doxpc_tipo_documento,
                //            tipo_documento_descripcion = item.tipo_documento_descripcion,
                //            proc_vnombre = item.proc_vnombre,
                //            proc_vpaterno = item.proc_vpaterno,
                //            proc_vmaterno = item.proc_vmaterno,
                //            proc_vnombrecompleto = item.proc_vnombrecompleto,
                //            doxpc_tipo_destino = item.doxpc_tipo_destino,
                //            tipo_destino_descripcion = item.tipo_destino_descripcion,
                //            doxpc_ind_detraccion = item.doxpc_ind_detraccion,
                //            doxpc_cod_tasa_detrac = item.doxpc_cod_tasa_detrac,
                //            doxpc_ind_retencion = item.doxpc_ind_retencion,
                //            doxpc_num_serie = item.doxpc_num_serie,
                //            doxpc_cod_aduana = item.doxpc_cod_aduana,
                //            doxpc_anio_aduana = item.doxpc_anio_aduana,
                //            doxpc_corre_aduana = item.doxpc_corre_aduana,
                //            doxpc_num_doc_domiciliado = item.doxpc_num_doc_domiciliado,
                //            doxpc_tipo_comprobante_referencia = item.doxpc_tipo_comprobante_ref,
                //            tipo_comprobante_codigo_ref = item.tipo_comprobante_codigo_ref,
                //            tipo_comprobante_descripcion_ref = item.tipo_comprobante_descripcion_ref,
                //            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                //            doxpc_numdoc_tipo = item.doxpc_numdoc_tipo,
                //            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                //            doxpc_nmonto_destino_gravado = item.doxpc_nmonto_destino_gravado,
                //            doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado,
                //            doxpc_nmonto_destino_mixto = item.doxpc_nmonto_destino_mixto,
                //            doxpc_nmonto_nogravado = item.doxpc_nmonto_nogravado,
                //            doxpc_vnro_doc_referencia = item.doxpc_vnro_doc_referencia,
                //            doxpc_num_serie_referencia = item.doxpc_num_serie_referencia,
                //            doxpc_num_comprobante_referencia = item.doxpc_num_comprobante_referencia,
                //            doxpc_sfecha_emision_referencia = item.doxpc_sfecha_emision_referencia,
                //            doxpc_nmonto_referencial_cif = item.doxpc_nmonto_referencial_cif,
                //            doxpc_nporcentaje_igv = item.doxpc_nporcentaje_igv
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListaDxPPendXFecha(DateTime fecha, int prov_icod, int analitica_icod)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGT_DOC_X_PAGAR_LISTAR_PEND_X_FECHA(fecha, prov_icod, analitica_icod);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            clase_viid_correlativo = string.Format("{0:00}", item.tdocd_iid_codigo_doc_det),
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento,
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarDxPDatosAdicionales(EDxPDatosAdicionales obe)
        {
            try
            {
                //using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                //{
                //    dc.SIGT_DOC_X_PAGAR_DATOS_ADICIONALES_INSERTAR(
                //        obe.doxpc_icod_correlativo,
                //        obe.doxpc_tipo_compra,
                //        obe.doxpc_tipo_comprobante,
                //        obe.proc_tipo_persona,
                //        obe.doxpc_tipo_documento,
                //        obe.proc_vnombre,
                //        obe.proc_vpaterno,
                //        obe.proc_vmaterno,
                //        obe.doxpc_tipo_destino,
                //        obe.doxpc_ind_detraccion,
                //        obe.doxpc_cod_tasa_detrac,
                //        obe.doxpc_ind_retencion,
                //        obe.doxpc_num_serie,
                //        obe.doxpc_cod_aduana,
                //        obe.doxpc_anio_aduana,
                //        obe.doxpc_corre_aduana,
                //        obe.doxpc_num_doc_domiciliado,
                //        obe.doxpc_tipo_comprobante_referencia,
                //        obe.doxpc_num_serie_referencia,
                //        obe.doxpc_num_comprobante_referencia,
                //        obe.doxpc_sfecha_emision_referencia
                //    );

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarMontoPagadoSaldoAdelanto(long IdDocumentoPorPagar, int IdTipoMoneda)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SIGTS_DOC_X_PAGAR_ACTUALIZAR_MONTO_PAGADO_SALDO_ADELANTOS(IdDocumentoPorPagar, IdTipoMoneda);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMontoPagadoSaldoNotaCredito(long IdDocumentoPorPagar, int IdTipoMoneda)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SIGTS_DOC_X_PAGAR_ACTUALIZAR_MONTO_PAGADO_SALDO_NOTA_CREDITO(IdDocumentoPorPagar, IdTipoMoneda);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProveedor> ListarProveedoresSaldos(int año, int doxpc_iid_tipo_documento)
        {
            List<EProveedor> lista = new List<EProveedor>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_PROVEEDORES_SALDOS_LISTAR(año, doxpc_iid_tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EProveedor()
                        {
                            iid_icod_proveedor = (int)item.proc_icod_proveedor,
                            vcod_proveedor = item.proc_vcod_proveedor,
                            vnombrecompleto = item.proc_vnombrecompleto,
                            Situacion = item.Situacion,
                            doxpc_nmonto_total_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxpc_nmonto_total_saldo_dolares = item.doxcc_nmonto_saldo_dolares
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProveedor> ListarGarantiaProveedoresSaldos(int año, int doxpc_iid_tipo_documento)
        {
            List<EProveedor> lista = new List<EProveedor>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_GARANTIA_PROVEEDORES_SALDOS_LISTAR(año, doxpc_iid_tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EProveedor()
                        {
                            iid_icod_proveedor = (int)item.proc_icod_proveedor,
                            vcod_proveedor = item.proc_vcod_proveedor,
                            vnombrecompleto = item.proc_vnombrecompleto,
                            Situacion = item.Situacion,
                            doxpc_nmonto_total_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxpc_nmonto_total_saldo_dolares = item.doxcc_nmonto_saldo_dolares
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarProveedor(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_BUSCAR_X_PROVEEDOR_TODA_SITUACION(proc_icod_proveedor, doxcc_ianio, tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            CodigoClaseDocumento = Convert.ToInt32(item.CodigoClaseDocumento),
                            tablc_iid_situacion_documento = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            Situacion = item.Situacion,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ConsultarDocumentos(int doxcc_ianio)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_BUSCAR_X_PROVEEDOR_TODA_SITUACION_CONSULTAR(doxcc_ianio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            CodigoClaseDocumento = Convert.ToInt32(item.CodigoClaseDocumento),
                            tablc_iid_situacion_documento = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            Situacion = item.Situacion,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                            Diferencia = Convert.ToDecimal(item.Diferencia),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes)

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarGarantiaProveedor(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_BUSCAR_X_GARANTIA_PROVEEDOR_TODA_SITUACION(proc_icod_proveedor, doxcc_ianio, tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            CodigoClaseDocumento = Convert.ToInt32(item.CodigoClaseDocumento),
                            tablc_iid_situacion_documento = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            Situacion = item.Situacion,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDocumentosProveedor(int doxcc_ianio, int icod_tipo_documento)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DOCUMENTOS_PROVEEDOR(doxcc_ianio, icod_tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,

                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            idTipodocuemnto = Convert.ToInt32(item.idTipodocuemnto),
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = (item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            doxpc_sfecha_emision_referencia = Convert.ToDateTime(item.doxpc_sfecha_emision_referencia),
                            ValorVenta = Convert.ToDecimal(item.ValorVenta),
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            DocPago = item.DocPago,
                            FechaPago = Convert.ToDateTime(item.FechaPago),
                            MonedaPago = item.MonedaPago,
                            MontoPago = Convert.ToDecimal(item.MontoPago)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDocumentosGarantiaProveedor(int doxcc_ianio, int icod_tipo_documento)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DOCUMENTOS_GARANTIA_PROVEEDOR(doxcc_ianio, icod_tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,

                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            idTipodocuemnto = Convert.ToInt32(item.idTipodocuemnto),
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = (item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            doxpc_sfecha_emision_referencia = Convert.ToDateTime(item.doxpc_sfecha_emision_referencia),
                            ValorVenta = Convert.ToDecimal(item.ValorVenta),
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDetalleProveedores(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_PROVEEDORES(proc_icod_proveedor, doxcc_ianio, tipo_documento);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            doxpc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            Valorcompra = Convert.ToDecimal(item.ValorVenta),
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public DataTable EstadoCuentaDetallePagoProveedor(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            DataTable dtResultado = null;

            using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
            {
                var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_PAGO_PROVEEDOR(proc_icod_proveedor, doxcc_ianio, tipo_documento);
                Convertir convierte = new Convertir();
                dtResultado = new DataTable();
                dtResultado = convierte.ConvertirADataTable(query);
                return dtResultado;
            }
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarFechaVencimiento(int doxcc_ianio, DateTime sfechainicio, DateTime sfechaFinal)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_X_FECHA_VENCIMIENTO(doxcc_ianio, sfechainicio, sfechaFinal);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            CodigoClaseDocumento = Convert.ToInt32(item.CodigoClaseDocumento),
                            tablc_iid_situacion_documento = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            Situacion = item.Situacion,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            proc_vcod_proveedor = item.proc_vcod_proveedor
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProveedor> ListarProveedoresSaldosAUnaFecha(int año, DateTime sfecha)
        {
            List<EProveedor> lista = new List<EProveedor>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_PROVEEDORES_SALDOS_LISTAR_A_UNA_FECHA(año, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new EProveedor()
                        {
                            iid_icod_proveedor = (int)item.proc_icod_proveedor,
                            vcod_proveedor = item.proc_vcod_proveedor,
                            vnombrecompleto = item.proc_vnombrecompleto,
                            Situacion = item.Situacion,
                            doxpc_nmonto_total_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxpc_nmonto_total_saldo_dolares = item.doxcc_nmonto_saldo_dolares
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocxPagarPagoAdelanto> ListarPagoAdelantoXIdAdelantoAUnaFecha(long doxcc_icod_correlativo_adelanto, int anio, DateTime sfecha)
        {
            List<EDocxPagarPagoAdelanto> lista = new List<EDocxPagarPagoAdelanto>();
            try
            {
                //using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                //{
                //    var query = dc.SIGT_DOC_X_PAGAR_ADELANTO_PAGO_LISTAR_X_ICOD_ADELANTO_A_UNA_FECHA(doxcc_icod_correlativo_adelanto, anio, sfecha);
                //    foreach (var item in query)
                //    {
                //        lista.Add(new EDocxPagarPagoAdelanto()
                //        {
                //            adpap_icod_correlativo = item.adpap_icod_correlativo,
                //            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                //            doxpc_icod_correlativo_adelanto = item.doxpc_icod_correlativo_adelanto,
                //            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                //            AbreviaturaAdelanto = item.AbreviaturaAdelanto,
                //            tdocc_iid_correlativo = item.tdocc_iid_correlativo,
                //            adpap_vnumero_doc_adelanto = item.adpap_vnumero_doc_adelanto,
                //            SaldoDXCAdelanto = item.SaldoDXCAdelanto,
                //            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                //            Moneda = item.Moneda,
                //            SimboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                //            adpap_nmonto_pago = item.adpap_nmonto_pago,
                //            adpap_nmonto_tipo_cambio = item.adpap_nmonto_tipo_cambio,
                //            adpap_vdescripcion = item.adpap_vdescripcion,
                //            adpap_sfecha_pago = item.adpap_sfecha_pago,
                //            adpap_iorigen = item.adpap_iorigen
                //            //pdxpc_icod_correlativo = item.pdxpc_icod_correlativo
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarProveedorAUnaFecha(int proc_icod_proveedor, int doxcc_ianio, DateTime sfecha)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_BUSCAR_X_PROVEEDOR_TODA_SITUACION_A_UNA_FECHA(doxcc_ianio, sfecha, proc_icod_proveedor);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            CodigoClaseDocumento = Convert.ToInt32(item.CodigoClaseDocumento),
                            tablc_iid_situacion_documento = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            Situacion = item.Situacion,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            //doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDetalleProveedoresAUnaFecha(int proc_icod_proveedor, int doxcc_ianio, DateTime sfecha)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_PROVEEDORES_A_UNA_FECHA(doxcc_ianio, sfecha, proc_icod_proveedor);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            doxpc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxpc_sfecha_doc = item.doxpc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            //doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            Valorcompra = Convert.ToDecimal(item.ValorVenta),
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public DataTable EstadoCuentaDetallePagoProveedorAunaFecha(int proc_icod_proveedor, int doxcc_ianio, DateTime sfecha)
        {
            DataTable dtResultado = null;

            using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
            {
                var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_PAGO_PROVEEDOR_A_UNA_FECHA(doxcc_ianio, sfecha, proc_icod_proveedor);
                Convertir convierte = new Convertir();
                dtResultado = new DataTable();
                dtResultado = convierte.ConvertirADataTable(query);
                return dtResultado;
            }
        }
        public List<EDocPorPagar> EstadoCuentaDocumentosProveedorAUnaFecha(int doxcc_ianio, DateTime sfecha)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DOCUMENTOS_PROVEEDOR_A_UNA_FECHA(doxcc_ianio, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            idTipodocuemnto = Convert.ToInt32(item.idTipodocuemnto),
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = (item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            doxpc_sfecha_emision_referencia = Convert.ToDateTime(item.doxpc_sfecha_emision_referencia),
                            ValorVenta = Convert.ToDecimal(item.ValorVenta),
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarCuenta(int doxcc_ianio, int mes)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>(); ;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_BUSCAR_DOCUMENTO_CUENTA(doxcc_ianio, mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            Abreviatura = item.numero_doc_comple,
                            tdodc_iid_correlativo = Convert.ToInt32(item.tdodc_iid_correlativo),
                            CodigoClaseDocumento = Convert.ToInt32(item.CodigoClaseDocumento),
                            tablc_iid_situacion_documento = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            Situacion = item.Situacion,
                            doxpc_vnumero_doc = item.doxpc_vnumero_doc,
                            doxpc_sfecha_doc = Convert.ToDateTime(item.doxpc_sfecha_doc),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxpc_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento),
                            doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado,
                            doxpc_nmonto_total_saldo = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                            doxpc_nmonto_tipo_cambio = Convert.ToDecimal(item.doxpc_nmonto_tipo_cambio),
                            //doxpc_vdescrip_transaccion = item.doxpc_vdescrip_transaccion,
                            doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            icod_cuenta_contable = item.ID_cuenta_contable,
                            cuenta_contable = item.cuenta_contable,
                            proc_vcod_proveedor = item.proc_vcod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            vdescripcion_cuenta_contable = item.vdescripcion_cuenta_contable,
                            SALDO_SOLES = item.SALDO_SOLES,
                            PAGADO_SOLES = item.PAGADO_SOLES,
                            TOTAL_SOLES = item.TOTAL_SOLES,
                            SALDO_DOLARES = item.SALDO_DOLARES,
                            PAGADO_DOLARES = item.PAGADO_DOLARES,
                            TOTAL_DOLARES = item.TOTAL_DOLARES,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void CierreDocumentoXPagar(int año, int iUSUARIO)
        {

            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGEV_DOC_X_PAGAR_CIERRE(año, iUSUARIO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Letra Por Pagar - Cabecera

        public List<ELetraPorPagar> listarLetraPorPagar(int intEjercicio, int intPeriodo)
        {
            List<ELetraPorPagar> Lista = new List<ELetraPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LETRA_X_PAGAR_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELetraPorPagar()
                        {
                            lexpc_icod_correlativo = item.lexpc_icod_correlativo,
                            lexpc_icorrelativo = item.lexpc_icorrelativo,
                            anioc_iid_anio = item.anioc_iid_anio,
                            mesec_iid_mes = item.mesec_iid_mes,
                            lexpc_vnumero_letra = item.lexpc_vnumero_letra,
                            lexpc_vnumero_letra_proveedor = item.lexpc_vnumero_letra_proveedor,
                            lexpc_inumero_renovacion = item.lexpc_inumero_renovacion,
                            lexpc_sfecha_letra = item.lexpc_sfecha_letra,
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            lexpc_vaval = item.lexpc_vaval,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            lexpc_nmonto_total = item.lexpc_nmonto_total,
                            lexpc_nmonto_pagado = item.lexpc_nmonto_pagado,
                            lexpc_nmonto_tipo_cambio = item.lexpc_nmonto_tipo_cambio,
                            lexpc_sfecha_vencimiento = item.lexpc_sfecha_vencimiento,
                            tablc_iid_situacion_letra = item.tablc_iid_situacion_letra,
                            tablc_iid_condicion_letra = item.tablc_iid_condicion_letra,
                            efinc_icod_entidad_financiera = item.efinc_icod_entidad_financiera,
                            lexpc_vnumero_ubd = item.lexpc_vnumero_ubd,
                            lexpc_vobservaciones = item.lexpc_vobservaciones,
                            tablc_iid_ubicacion_letra = item.tablc_iid_ubicacion_letra,
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            lexpc_icod_correlativo_renovacion = item.lexpc_icod_correlativo_renovacion,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            strSituacion = item.strSituacion,
                            strCondicion = item.strCondicion,
                            strUbicacion = item.strUbicacion,
                            strMoneda = item.strMoneda,
                            strDesProveedor = item.strProveedor,
                            fitec_vruta = item.fitec_vruta
                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ELetraPorPagar> getLetraPorPagarCab(int intLxp)
        {
            List<ELetraPorPagar> Lista = new List<ELetraPorPagar>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GET_LETRA_X_PAGAR_CAB(intLxp);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELetraPorPagar()
                        {
                            lexpc_icod_correlativo = item.lexpc_icod_correlativo,
                            lexpc_icorrelativo = item.lexpc_icorrelativo,
                            anioc_iid_anio = item.anioc_iid_anio,
                            mesec_iid_mes = item.mesec_iid_mes,
                            lexpc_vnumero_letra = item.lexpc_vnumero_letra,
                            lexpc_inumero_renovacion = item.lexpc_inumero_renovacion,
                            lexpc_sfecha_letra = item.lexpc_sfecha_letra,
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            lexpc_vaval = item.lexpc_vaval,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            lexpc_nmonto_total = item.lexpc_nmonto_total,
                            lexpc_nmonto_pagado = item.lexpc_nmonto_pagado,
                            lexpc_nmonto_tipo_cambio = item.lexpc_nmonto_tipo_cambio,
                            lexpc_sfecha_vencimiento = item.lexpc_sfecha_vencimiento,
                            tablc_iid_situacion_letra = item.tablc_iid_situacion_letra,
                            tablc_iid_condicion_letra = item.tablc_iid_condicion_letra,
                            efinc_icod_entidad_financiera = item.efinc_icod_entidad_financiera,
                            lexpc_vnumero_ubd = item.lexpc_vnumero_ubd,
                            lexpc_vobservaciones = item.lexpc_vobservaciones,
                            tablc_iid_ubicacion_letra = item.tablc_iid_ubicacion_letra,
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            lexpc_icod_correlativo_renovacion = item.lexpc_icod_correlativo_renovacion,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            strSituacion = item.strSituacion,
                            strCondicion = item.strCondicion,
                            strUbicacion = item.strUbicacion,
                            strMoneda = item.strMoneda,
                            strDesProveedor = item.strProveedor
                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EGarantiaProveedores> getGarantiaPorPagarCab(int intLxp)
        {
            List<EGarantiaProveedores> Lista = new List<EGarantiaProveedores>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GET_GARANTIA_X_PAGAR_CAB(intLxp);
                    foreach (var item in query)
                    {
                        Lista.Add(new EGarantiaProveedores()
                        {
                            garp_icod_garantia = item.garp_icod_garantia,
                            garap_vnumero_garantia = item.garap_vnumero_garantia,
                            garp_sfecha_garantia = Convert.ToDateTime(item.garp_sfecha_garantia),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            garp_nmonto = Convert.ToDecimal(item.garp_nmonto),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            fcoc_icod_doc = Convert.ToInt32(item.fcoc_icod_doc),
                            Situacion = item.strSituacion,
                            NomProv = item.strProveedor,
                            Moneda = item.strMoneda,
                            NumDXP = item.NumDXP,
                            ClaseDXP = Convert.ToInt32(item.ClaseDXP),
                            CentroCosto = item.CentroCosto,
                            DesCC = item.DesCC

                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarLetraPorPagar(ELetraPorPagar oBe)
        {
            int? intIcod = 0;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_PAGAR_INSERTAR(
                        ref intIcod,
                        oBe.lexpc_icorrelativo,
                        oBe.anioc_iid_anio,
                        oBe.mesec_iid_mes,
                        oBe.lexpc_vnumero_letra,
                        oBe.lexpc_vnumero_letra_proveedor,
                        oBe.lexpc_inumero_renovacion,
                        oBe.lexpc_sfecha_letra,
                        oBe.proc_icod_proveedor,
                        oBe.lexpc_vaval,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lexpc_nmonto_total,
                        oBe.lexpc_nmonto_pagado,
                        oBe.lexpc_nmonto_tipo_cambio,
                        oBe.lexpc_sfecha_vencimiento,
                        oBe.tablc_iid_situacion_letra,
                        oBe.tablc_iid_condicion_letra,
                        oBe.efinc_icod_entidad_financiera,
                        oBe.lexpc_vnumero_ubd,
                        oBe.lexpc_vobservaciones,
                        oBe.tablc_iid_ubicacion_letra,
                        oBe.doxpc_icod_correlativo,
                        oBe.lexpc_icod_correlativo_renovacion,
                        oBe.vcocc_iid_voucher_contable,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fitec_vruta
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLetraPorPagar(ELetraPorPagar oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_PAGAR_MODIFICAR(
                        oBe.lexpc_icod_correlativo,
                        oBe.lexpc_icorrelativo,
                        oBe.anioc_iid_anio,
                        oBe.mesec_iid_mes,
                        oBe.lexpc_vnumero_letra,
                        oBe.lexpc_vnumero_letra_proveedor,
                        oBe.lexpc_inumero_renovacion,
                        oBe.lexpc_sfecha_letra,
                        oBe.proc_icod_proveedor,
                        oBe.lexpc_vaval,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lexpc_nmonto_total,
                        oBe.lexpc_nmonto_pagado,
                        oBe.lexpc_nmonto_tipo_cambio,
                        oBe.lexpc_sfecha_vencimiento,
                        oBe.tablc_iid_situacion_letra,
                        oBe.tablc_iid_condicion_letra,
                        oBe.efinc_icod_entidad_financiera,
                        oBe.lexpc_vnumero_ubd,
                        oBe.lexpc_vobservaciones,
                        oBe.tablc_iid_ubicacion_letra,
                        oBe.doxpc_icod_correlativo,
                        oBe.lexpc_icod_correlativo_renovacion,
                        oBe.vcocc_iid_voucher_contable,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fitec_vruta
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarLetraPorPagar(ELetraPorPagar oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_PAGAR_ELIMINAR(
                        oBe.lexpc_icod_correlativo,
                        oBe.doxpc_icod_correlativo,
                        oBe.vcocc_iid_voucher_contable,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Letra Por Pagar - Detalle
        public List<ELetraPorPagarDet> listarLetraPorPagarDet(int intLXP)
        {
            List<ELetraPorPagarDet> Lista = new List<ELetraPorPagarDet>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LETRA_X_PAGAR_DET_LISTAR(intLXP);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELetraPorPagarDet()
                        {
                            lxppc_icod_correlativo = item.lxppc_icod_correlativo,
                            lexpc_icod_correlativo = item.lexpc_icod_correlativo,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            lxppc_nmonto_pago = item.lxppc_nmonto_pago,
                            lxppc_nmonto_tipo_cambio = Convert.ToDecimal(item.lxppc_nmonto_tipo_cambio),
                            lxppc_sfecha_doc = item.lxppc_sfecha_doc,
                            lxppc_sfecha_pago = item.lxppc_sfecha_pago,
                            lxppc_vconcepto = item.lxppc_vconcepto,
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_iid_clase_doc = item.tdocc_iid_clase_doc,
                            pdxpc_vnumero_doc = item.pdxpc_vnumero_doc,
                            tablc_iid_tipo_cuenta_debe_haber = item.tablc_iid_tipo_cuenta_debe_haber,
                            ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            anac_icod_analitica = item.anac_icod_analitica,
                            lxppc_isituacion = item.lxppc_isituacion,
                            lxppc_tipo_pago = item.lxppc_tipo_pago,
                            strDesCuenta = item.strDesCuenta,
                            strCodCCosto = item.strCodCCosto,
                            strDesCCosto = item.strDesCCosto,
                            strCodAnalitica = String.Format("{0:00}", item.strCodAnalitica),
                            strCodSubAnalitica = item.strCodSubAnalitica,
                            strTipoDoc = item.strTipoDoc,
                            strDebeHaber = item.strTipoCuenta,
                            dblMontoDoc = Convert.ToDecimal(item.dblMontoDoc),
                            dblSaldoDoc = Convert.ToDecimal(item.dblSalDoc)
                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarLetraPorPagarDet(ELetraPorPagarDet oBe)
        {

            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_PAGAR_DET_INSERTAR(
                        oBe.lexpc_icod_correlativo,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lxppc_nmonto_pago,
                        oBe.lxppc_nmonto_tipo_cambio,
                        oBe.lxppc_sfecha_doc,
                        oBe.lxppc_sfecha_pago,
                        oBe.lxppc_vconcepto,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tdocc_iid_clase_doc,
                        oBe.pdxpc_vnumero_doc,
                        oBe.tablc_iid_tipo_cuenta_debe_haber,
                        oBe.ctacc_iid_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.anac_icod_analitica,
                        oBe.lxppc_isituacion,
                        oBe.lxppc_tipo_pago,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLetraPorPagarDet(ELetraPorPagarDet oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_PAGAR_DET_MODIFICAR(
                        oBe.lxppc_icod_correlativo,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lxppc_nmonto_pago,
                        oBe.lxppc_nmonto_tipo_cambio,
                        oBe.lxppc_sfecha_doc,
                        oBe.lxppc_sfecha_pago,
                        oBe.lxppc_vconcepto,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tdocc_iid_clase_doc,
                        oBe.pdxpc_vnumero_doc,
                        oBe.tablc_iid_tipo_cuenta_debe_haber,
                        oBe.ctacc_iid_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.anac_icod_analitica,
                        oBe.lxppc_isituacion,
                        oBe.lxppc_tipo_pago,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarLetraPorPagarDet(ELetraPorPagarDet oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_PAGAR_DET_ELIMINAR(
                        oBe.lxppc_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Liquidacion Gastos
        public List<ELiquidacionGastos> listarLiquidacionGastos(int intEjercicio, int intPeriodo)
        {
            List<ELiquidacionGastos> Lista = new List<ELiquidacionGastos>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LIQUIDACION_GASTOS_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELiquidacionGastos()
                        {
                            lqgc_icod_correlativo = item.lqgc_icod_correlativo,
                            lqgc_icorrelativo = Convert.ToInt32(item.lqgc_icorrelativo),
                            anioc_iid_anio = Convert.ToInt32(item.anioc_iid_anio),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            lqgc_vnumero_liq_gasto = item.lqgc_vnumero_liq_gasto,
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            lqgc_nmonto_total = Convert.ToDecimal(item.lqgc_nmonto_total),
                            lqgc_nmonto_pagado = Convert.ToDecimal(item.lqgc_nmonto_pagado),
                            lqgc_nmonto_tipo_cambio = Convert.ToDecimal(item.lqgc_nmonto_tipo_cambio),
                            lqgc_sfecha_vencimiento = Convert.ToDateTime(item.lqgc_sfecha_vencimiento),
                            tablc_iid_sit_liq_gasto = Convert.ToInt32(item.tablc_iid_sit_liq_gasto),
                            lqgc_vconcepto = item.lqgc_vconcepto,
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            strSituacion = item.strSituacion,
                            strMoneda = item.strMoneda,
                            strDesProveedor = item.strProveedor
                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarLiquidacionGastos(ELiquidacionGastos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LIQUIDACION_GASTOS_INSERTAR(
                        ref intIcod,
                        oBe.lqgc_icorrelativo,
                        oBe.anioc_iid_anio,
                        oBe.mesec_iid_mes,
                        oBe.lqgc_vnumero_liq_gasto,
                        oBe.lqgc_sfecha_liq_gasto,
                        oBe.proc_icod_proveedor,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lqgc_nmonto_total,
                        oBe.lqgc_nmonto_pagado,
                        oBe.lqgc_nmonto_tipo_cambio,
                        oBe.lqgc_sfecha_vencimiento,
                        oBe.tablc_iid_sit_liq_gasto,
                        oBe.lqgc_vconcepto,
                        oBe.doxpc_icod_correlativo,
                        oBe.vcocc_iid_voucher_contable,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLiquidacionGastos(ELiquidacionGastos oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LIQUIDACION_GASTOS_MODIFICAR(
                        oBe.lqgc_icod_correlativo,
                        oBe.lqgc_icorrelativo,
                        oBe.anioc_iid_anio,
                        oBe.mesec_iid_mes,
                        oBe.lqgc_vnumero_liq_gasto,
                        oBe.lqgc_sfecha_liq_gasto,
                        oBe.proc_icod_proveedor,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lqgc_nmonto_total,
                        oBe.lqgc_nmonto_pagado,
                        oBe.lqgc_nmonto_tipo_cambio,
                        oBe.lqgc_sfecha_vencimiento,
                        oBe.tablc_iid_sit_liq_gasto,
                        oBe.lqgc_vconcepto,
                        oBe.doxpc_icod_correlativo,
                        oBe.vcocc_iid_voucher_contable,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarLiquidacionGastos(ELiquidacionGastos oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LIQUIDACION_GASTOS_ELIMINAR(
                        oBe.lqgc_icod_correlativo,
                        oBe.doxpc_icod_correlativo,
                        oBe.vcocc_iid_voucher_contable,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Liquidacion Gastos Detalle
        public List<ELiquidacionGastosDet> listarLiquidacionGastosDet(int intLXP)
        {
            List<ELiquidacionGastosDet> Lista = new List<ELiquidacionGastosDet>();
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LIQUIDACION_GASTOS_DET_LISTAR(intLXP);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELiquidacionGastosDet()
                        {
                            lqgd_icod_correlativo = item.lqgd_icod_correlativo,
                            lqgc_icod_correlativo = Convert.ToInt32(item.lqgc_icod_correlativo),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            lqgd_nmonto_pago = Convert.ToDecimal(item.lqgd_nmonto_pago),
                            lqgd_nmonto_tipo_cambio = Convert.ToDecimal(item.lqgd_nmonto_tipo_cambio),
                            lqgd_sfecha_doc = item.lqgd_sfecha_doc,
                            lqgd_sfecha_pago = item.lqgd_sfecha_pago,
                            lqgd_vconcepto = item.lqgd_vconcepto,
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            pdxpc_icod_correlativo = item.pdxpc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_iid_clase_doc = item.tdocc_iid_clase_doc,
                            pdxpc_vnumero_doc = item.pdxpc_vnumero_doc,
                            tablc_iid_tipo_cuenta_debe_haber = item.tablc_iid_tipo_cuenta_debe_haber,
                            ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            anac_icod_analitica = item.anac_icod_analitica,
                            lqgd_tipo_pago = item.lqgd_tipo_pago,
                            strDesCuenta = item.strDesCuenta,
                            strCodCCosto = item.strCodCCosto,
                            strDesCCosto = item.strDesCCosto,
                            strCodAnalitica = String.Format("{0:00}", item.strCodAnalitica),
                            strCodSubAnalitica = item.strCodSubAnalitica,
                            strTipoDoc = item.strTipoDoc,
                            strDebeHaber = item.strTipoCuenta,
                            dblMontoDoc = Convert.ToDecimal(item.dblMontoDoc),
                            dblSaldoDoc = Convert.ToDecimal(item.dblSalDoc)
                        });
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void insertarLiquidacionGastosDet(ELiquidacionGastosDet oBe)
        {

            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LIQUIDACION_GASTOS_DET_INSERTAR(
                        oBe.lqgc_icod_correlativo,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lqgd_nmonto_pago,
                        oBe.lqgd_nmonto_tipo_cambio,
                        oBe.lqgd_sfecha_doc,
                        oBe.lqgd_sfecha_pago,
                        oBe.lqgd_vconcepto,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tdocc_iid_clase_doc,
                        oBe.pdxpc_vnumero_doc,
                        oBe.tablc_iid_tipo_cuenta_debe_haber,
                        oBe.ctacc_iid_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.anac_icod_analitica,
                        oBe.lqgd_tipo_pago,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLiquidacionGastosDet(ELiquidacionGastosDet oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LIQUIDACION_GASTOS_DET_MODIFICAR(
                        oBe.lqgd_icod_correlativo,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lqgd_nmonto_pago,
                        oBe.lqgd_nmonto_tipo_cambio,
                        oBe.lqgd_sfecha_doc,
                        oBe.lqgd_sfecha_pago,
                        oBe.lqgd_vconcepto,
                        oBe.doxpc_icod_correlativo,
                        oBe.pdxpc_icod_correlativo,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tdocc_iid_clase_doc,
                        oBe.pdxpc_vnumero_doc,
                        oBe.tablc_iid_tipo_cuenta_debe_haber,
                        oBe.ctacc_iid_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.anac_icod_analitica,
                        oBe.lqgd_tipo_pago,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarLiquidacionGastosDet(ELiquidacionGastosDet oBe)
        {
            try
            {
                using (CuentasPorPagarDataContext dc = new CuentasPorPagarDataContext(Helper.conexion()))
                {
                    dc.SGE_LIQUIDACION_GASTOS_DET_ELIMINAR(
                        oBe.lqgd_icod_correlativo,
                        oBe.intUsuario,
                        oBe.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


    }
}
