using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.DataAccess
{
    public class CuentasPorCobrarData
    {
        #region Dxc ...
        public List<ECliente> ListarDocPorCobrarSaldos(int año)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_LISTAR_SALDOS(año);
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = (int)item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            Situacion = item.Situacion,
                            doxcc_nmonto_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxcc_nmonto_saldo_dolares = item.doxcc_nmonto_saldo_dolares,
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro
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
        public List<ECliente> ListarDocPorCobrarSaldosGarantia(int año)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_LISTAR_SALDOS_GARANTIA(año);
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = (int)item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            Situacion = item.Situacion,
                            doxcc_nmonto_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxcc_nmonto_saldo_dolares = item.doxcc_nmonto_saldo_dolares,
                            giroc_icod_giro = Convert.ToInt32(item.giroc_icod_giro),
                            giroc_vnombre_giro = item.giroc_vnombre_giro
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
        public List<EDocXCobrar> ListarDocPorCobrarxCliente(int? IdCliente, int ANIO)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_LISTAR_X_CLIENTE(IdCliente, ANIO);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            //docxc_icod_documento = item.docxc_icod_documento,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            intAnaliticaCliente = Convert.ToInt32(item.anac_icod_analitica)
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
        public Int64 insertarDxc(EDocXCobrar oBe, List<EDocXCobrarCuentaContable> lstDxcCtaCble)
        {
            try
            {
                oBe.doxcc_icod_correlativo = Convert.ToInt64(insertarDocumentoXCobrar(oBe));
                if (oBe.doxcc_icod_correlativo == 0)
                {
                    throw new Exception("El tipo y número de documento ya fue utilizado, intente con un tipo o número de documento diferente");
                }
                lstDxcCtaCble.ForEach(x =>
                {
                    x.doxcc_icod_correlativo = oBe.doxcc_icod_correlativo;
                });
                insertarDxcCtaCble(lstDxcCtaCble);
                return oBe.doxcc_icod_correlativo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int64 insertarDxcDetalle(EDocXCobrar oBe, List<EDocXCobrarCuentaContable> lstDxcCtaCble)
        {
            EDocXCobrarCuentaContable x = new EDocXCobrarCuentaContable();
            try
            {
                x.doxcc_icod_correlativo = oBe.doxcc_icod_correlativo;
                x.ctacc_iid_cuenta_contable = Convert.ToInt32(oBe.ctacc_icod_cuenta_gastos_nac);
                x.cecoc_icod_centro_costo = oBe.pryc_icod_proyecto;
                x.anac_icod_analitica = 2;
                x.ccdcc_nmonto = Convert.ToDecimal(oBe.doxcc_nmonto_total);
                x.ccdcc_vglosa = oBe.doxcc_vobservaciones;
                x.ccdcc_isituacion = 1;
                x.operacion = 1;
                lstDxcCtaCble.Add(x);
                lstDxcCtaCble.ForEach(xp =>
                {

                    xp.doxcc_icod_correlativo = oBe.doxcc_icod_correlativo;
                    xp.ctacc_iid_cuenta_contable = Convert.ToInt32(oBe.ctacc_icod_cuenta_gastos_nac);
                    xp.cecoc_icod_centro_costo = oBe.pryc_icod_proyecto;
                    xp.anac_icod_analitica = 2;
                    xp.ccdcc_nmonto = Convert.ToDecimal(oBe.doxcc_nmonto_total);
                    xp.ccdcc_vglosa = oBe.doxcc_vobservaciones;
                    xp.ccdcc_isituacion = 1;
                });
                insertarDxcCtaCble(lstDxcCtaCble);
                return oBe.doxcc_icod_correlativo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDxc(EDocXCobrar oBe, List<EDocXCobrarCuentaContable> lstDxcCtaCble, List<EDocXCobrarCuentaContable> lstDxcDeleteCtaCble)
        {
            try
            {
                modificarDocumentoXCobrar(oBe);
                if (lstDxcCtaCble.Where(val => val.operacion == 1).ToList().Count > 0)
                {
                    lstDxcCtaCble.ForEach(x => { x.doxcc_icod_correlativo = oBe.doxcc_icod_correlativo; });
                    insertarDxcCtaCble(lstDxcCtaCble);
                }
                if (lstDxcCtaCble.Where(val => val.operacion == 2).ToList().Count > 0)
                    modificarDxcCtaCble(lstDxcCtaCble);
                if (lstDxcDeleteCtaCble.Count > 0)
                    eliminarDxcCtaCble(lstDxcDeleteCtaCble);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDxc(EDocXCobrar oBe)
        {
            try
            {

                var lstDxcDeleteCtaCble = BuscarDocumentoXCobrarCuentaContable(oBe.doxcc_icod_correlativo);
                foreach (var item in lstDxcDeleteCtaCble)
                {
                    item.usuario = oBe.intUsuario;
                    item.pc = oBe.strPc;
                }
                eliminarDxcCtaCble(lstDxcDeleteCtaCble);

                EliminarDocumentoXCobrar(oBe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Documentos X Cobrar
        public List<EDocXCobrar> listarDxcPendientes(int intEjercicio)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_LISTAR_PENDIENTES(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            mesec_iid_mes = item.mesec_iid_mes,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                            DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            cliec_cruc = item.cliec_cruc,
                            DireccionCliente = item.DireccionCliente,
                            NumDocCliente = item.NumDocCliente,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            tablc_iid_tipo_pago = item.tablc_iid_tipo_pago,
                            FormaPago = item.FormaPago,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
                            doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                            doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,
                            ValorVenta = item.ValorVenta,
                            doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                            doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxc_bind_cuenta_corriente = item.doxc_bind_cuenta_corriente,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            doxcc_bind_impresion_nogerencia = item.doxcc_bind_impresion_nogerencia,
                            doxc_bind_situacion_legal = item.doxc_bind_situacion_legal,
                            doxc_bind_cierre_cuenta_corriente = item.doxc_bind_cierre_cuenta_corriente,
                            doxcc_tipo_comprobante_referencia = Convert.ToInt32(item.doxcc_tipo_comprobante_referencia),
                            doxcc_num_serie_referencia = item.doxcc_num_serie_referencia,
                            doxcc_num_comprobante_referencia = item.doxcc_num_comprobante_referencia,
                            doxcc_sfecha_emision_referencia = item.doxcc_sfecha_emision_referencia
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
        public List<EDocXCobrar> listarDxc(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            mesec_iid_mes = item.mesec_iid_mes,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                            DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "$.",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            tablc_iid_tipo_pago = item.tablc_iid_tipo_pago,
                            FormaPago = item.FormaPago,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
                            doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                            doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,
                            ValorVenta = item.ValorVenta,
                            doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                            doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vobservaciones = (!string.IsNullOrWhiteSpace(item.doxcc_vobservaciones)) ? item.doxcc_vobservaciones : item.doxcc_vdescrip_transaccion,
                            doxc_bind_cuenta_corriente = item.doxc_bind_cuenta_corriente,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            doxcc_bind_impresion_nogerencia = item.doxcc_bind_impresion_nogerencia,
                            doxc_bind_situacion_legal = item.doxc_bind_situacion_legal,
                            doxc_bind_cierre_cuenta_corriente = item.doxc_bind_cierre_cuenta_corriente,
                            doxcc_tipo_comprobante_referencia = Convert.ToInt32(item.doxcc_tipo_comprobante_referencia),
                            doxcc_num_serie_referencia = item.doxcc_num_serie_referencia,
                            doxcc_num_comprobante_referencia = item.doxcc_num_comprobante_referencia,
                            doxcc_sfecha_emision_referencia = item.doxcc_sfecha_emision_referencia,
                            docxc_icod_documento = Convert.ToInt32(item.docxc_icod_documento),
                            anio = Convert.ToInt32(item.doxcc_ianio),
                            doxcc_origen = item.doxcc_origen,
                            DescripcionOrigen = item.DescripcionOrigen,
                            tablc_iid_tipo_docxpagar = Convert.ToInt32(item.tablc_iid_tipo_docxpagar),
                            TipoDXC = item.TipoDXC,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            NomProyecto = item.NomProyecto,
                            CentroCossto = item.CentroCossto,
                            puvec_icod_punto_venta = Convert.ToInt32(item.puvec_icod_punto_venta)

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
        public int getSituacionDocPorCobrar(Int64 int_dxc)
        {
            int? id_situacion = 0;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_GET_SITUACION(ref id_situacion, int_dxc);
                }
                return Convert.ToInt32(id_situacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long insertarDocumentoXCobrar(EDocXCobrar obj)
        {
            long? doxcc_icod_correlativo = 0;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_INSERTAR(
                        ref doxcc_icod_correlativo,
                        obj.mesec_iid_mes,
                        obj.tdocc_icod_tipo_doc,
                        obj.tdodc_iid_correlativo,
                        obj.doxcc_vnumero_doc,
                        obj.cliec_icod_cliente,
                        obj.doxcc_sfecha_doc,
                        obj.doxcc_sfecha_vencimiento_doc,
                        obj.tablc_iid_tipo_moneda,
                        obj.doxcc_nmonto_tipo_cambio,
                        obj.tablc_iid_tipo_pago,
                        obj.doxcc_vdescrip_transaccion,
                        obj.doxcc_nmonto_afecto,
                        obj.doxcc_nmonto_inafecto,
                        obj.doxcc_nporcentaje_igv,
                        obj.doxcc_nmonto_impuesto,
                        obj.doxcc_nmonto_total,
                        obj.doxcc_nmonto_saldo,
                        obj.doxcc_nmonto_pagado,
                        obj.tablc_iid_situacion_documento,
                        obj.doxcc_vobservaciones,
                        obj.doxc_bind_cuenta_corriente,
                        obj.doxcc_sfecha_entrega,
                        obj.doxcc_bind_impresion_nogerencia,
                        obj.doxc_bind_situacion_legal,
                        obj.doxc_bind_cierre_cuenta_corriente,
                        obj.intUsuario,
                        obj.strPc,
                        obj.doxcc_tipo_comprobante_referencia,
                        obj.doxcc_num_serie_referencia,
                        obj.doxcc_num_comprobante_referencia,
                        obj.doxcc_sfecha_emision_referencia,
                        obj.docxc_icod_documento,
                        obj.anio,
                        obj.doxcc_flag_estado,
                        obj.doxcc_origen,
                        obj.tablc_iid_tipo_docxpagar,
                        obj.pryc_icod_proyecto,
                        obj.puvec_icod_punto_venta
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt64(doxcc_icod_correlativo);
        }
        public void modificarDocumentoXCobrar(EDocXCobrar obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_ACTUALIZAR(
                        obj.doxcc_icod_correlativo,
                        obj.mesec_iid_mes,
                        obj.doxcc_sfecha_doc,
                        obj.doxcc_sfecha_vencimiento_doc,
                        obj.doxcc_nmonto_tipo_cambio,
                        obj.tablc_iid_tipo_pago,
                        obj.doxcc_vdescrip_transaccion,
                        obj.doxcc_nmonto_afecto,
                        obj.doxcc_nmonto_inafecto,
                        obj.doxcc_nporcentaje_igv,
                        obj.doxcc_nmonto_impuesto,
                        obj.doxcc_nmonto_total,
                        obj.doxcc_nmonto_saldo,
                        obj.doxcc_nmonto_pagado,
                        obj.doxcc_vobservaciones,
                        obj.doxc_bind_cuenta_corriente,
                        obj.doxcc_sfecha_entrega,
                        obj.doxcc_bind_impresion_nogerencia,
                        obj.doxc_bind_situacion_legal,
                        obj.doxc_bind_cierre_cuenta_corriente,
                        obj.intUsuario,
                        obj.strPc,
                        obj.doxcc_tipo_comprobante_referencia,
                        obj.doxcc_num_serie_referencia,
                        obj.doxcc_num_comprobante_referencia,
                        obj.doxcc_sfecha_emision_referencia,
                        obj.docxc_icod_documento,
                        obj.cliec_icod_cliente,
                        obj.tablc_iid_tipo_docxpagar,
                        obj.pryc_icod_proyecto,
                        obj.puvec_icod_punto_venta
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarDocumentoXCobrar(EDocXCobrar obj)
        {
            try
            {

                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_ELIMINAR(
                        obj.doxcc_icod_correlativo,
                        obj.intUsuario,
                        obj.strPc
                        );
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AnularDocumentoXCobrar(EDocXCobrar obj)
        {
            try
            {

                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_ANULAR(
                        obj.doxcc_icod_correlativo,
                        obj.intUsuario,
                        obj.strPc
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocumentoXCobrarDescripcion(long doxcc_icod_correlativo, string doxcc_vobservaciones)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_ACTUALIZAR_DESCRIPCION(
                        doxcc_icod_correlativo,
                        doxcc_vobservaciones
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDocumentoXPagarDescripcion(long doxcc_icod_correlativo, string doxcc_vobservaciones)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGECPC_DOC_X_PAGAR_ACTUALIZAR_DESCRIPCION(
                        doxcc_icod_correlativo,
                        doxcc_vobservaciones
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Adelanto Pago
        public List<EAdelantoPago> ListarAdelantoPago(long doxcc_icod_correlativo_dxc_pago, long doxcc_icod_correlativo_dxc_adelanto)
        {
            List<EAdelantoPago> lista = new List<EAdelantoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_ADELANTO_PAGO_LISTAR(doxcc_icod_correlativo_dxc_pago, doxcc_icod_correlativo_dxc_adelanto);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoPago()
                        {
                            adpac_icod_correlativo = item.adpac_icod_correlativo,
                            doxcc_icod_correlativo_pago = item.doxcc_icod_correlativo_pago,
                            doxcc_icod_correlativo_adelanto = item.doxcc_icod_correlativo_adelanto,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_iid_correlativo_pago = Convert.ToInt32(item.tdocc_iid_correlativo_pago),
                            AbreviaturaAdelanto = item.AbreviaturaAdelanto,
                            AbreviaturaPago = item.AbreviaturaPago,
                            vnumero_doc_adelanto = item.vnumero_doc_adelanto,
                            vnumero_doc_pago = item.vnumero_doc_pago,
                            SaldoDXCAdelanto = item.SaldoDXCAdelanto,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            adpac_nmonto_pago = item.adpac_nmonto_pago,
                            adpac_nmonto_tipo_cambio = item.adpac_nmonto_tipo_cambio,
                            adpac_vdescripcion = item.adpac_vdescripcion,
                            adpac_sfecha_pago = item.adpac_sfecha_pago,
                            adpac_iorigen = item.adpac_iorigen,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            tdocc_iid_correlativo_adelanto = item.tdocc_iid_correlativo_adelanto,
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo,
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            iid_tipo_moneda_pago = Convert.ToInt32(item.iid_tipo_moneda_pago)
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

        public List<EAdelantoPago> ListarAdelantoPago2(int mes)
        {
            List<EAdelantoPago> lista = new List<EAdelantoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_ADELANTO_PAGO_LISTAR2(mes);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoPago()
                        {
                            adpac_icod_correlativo = item.adpac_icod_correlativo,
                            doxcc_icod_correlativo_pago = item.doxcc_icod_correlativo_pago,
                            doxcc_icod_correlativo_adelanto = item.doxcc_icod_correlativo_adelanto,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_iid_correlativo_pago = Convert.ToInt32(item.tdocc_iid_correlativo_pago),
                            AbreviaturaAdelanto = item.AbreviaturaAdelanto,
                            AbreviaturaPago = item.AbreviaturaPago,
                            vnumero_doc_adelanto = item.vnumero_doc_adelanto,
                            vnumero_doc_pago = item.vnumero_doc_pago,
                            SaldoDXCAdelanto = item.SaldoDXCAdelanto,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            adpac_nmonto_pago = item.adpac_nmonto_pago,
                            adpac_nmonto_tipo_cambio = item.adpac_nmonto_tipo_cambio,
                            adpac_vdescripcion = item.adpac_vdescripcion,
                            adpac_sfecha_pago = item.adpac_sfecha_pago,
                            adpac_iorigen = item.adpac_iorigen,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            tdocc_iid_correlativo_adelanto = item.tdocc_iid_correlativo_adelanto,
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo,
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            iid_tipo_moneda_pago = Convert.ToInt32(item.iid_tipo_moneda_pago)
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

        public long insertarAdelantoPago(EAdelantoPago obj)
        {
            try
            {
                long? IdAdelantoPago = 0;
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {

                    dc.SGEDXC_DOC_X_COBRAR_ADELANTO_PAGO_INSERTAR(
                        ref IdAdelantoPago,
                        obj.doxcc_icod_correlativo_pago,
                        obj.doxcc_icod_correlativo_adelanto,
                        obj.tablc_iid_tipo_moneda,
                        obj.adpac_nmonto_pago,
                        obj.adpac_nmonto_tipo_cambio,
                        obj.adpac_vdescripcion,
                        obj.adpac_sfecha_pago,
                        obj.adpac_iorigen,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.pdxcc_icod_correlativo,
                        obj.adpac_iusuario_crea,
                        obj.adpac_vpc_crea,
                        obj.adpac_flag_estado,
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
        public void modificarAdelantoPago(EAdelantoPago obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_ADELANTO_PAGO_ACTUALIZAR(
                        obj.adpac_icod_correlativo,
                        obj.adpac_nmonto_pago,
                        obj.adpac_nmonto_tipo_cambio,
                        obj.adpac_vdescripcion,
                        obj.adpac_sfecha_pago,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.adpac_iusuario_modifica,
                        obj.adpac_vpc_modifica,
                        obj.mobac_icod_correlativo
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAdelantoPago(EAdelantoPago obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_ADELANTO_PAGO_ELIMINAR(
                        obj.adpac_icod_correlativo,
                        obj.adpac_vpc_modifica,
                        obj.adpac_iusuario_modifica);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region NC Pago
        //public List<EDxCPagoConNCredito> ListarDxCPagoConNCreditoXIcodNC(long cod)
        //{
        //    List<EDxCPagoConNCredito> Lista = new List<EDxCPagoConNCredito>();
        //    try
        //    {
        //        using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
        //        {
        //            var query = dc.SIGTS_DXC_PAGO_CON_NCREDITO_LISTAR_X_ICOD_NC(cod);
        //            foreach (var item in query)
        //            {
        //                Lista.Add(new EDxCPagoConNCredito()
        //                {
        //                    ncpac_icod_correlativo = item.ncpac_icod_correlativo,
        //                    tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
        //                    ncpac_vnumero_doc_nota_credito = item.ncpac_vnumero_doc_nota_credito,
        //                    ncpac_vdescripcion = item.ncpac_vdescripcion,
        //                    tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
        //                    simboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/. " : "US$",
        //                    ncpac_nmonto_pago = item.ncpac_nmonto_pago,
        //                    ncpac_sfecha_pago = item.ncpac_sfecha_pago,

        //                    doxcc_icod_correlativo_nota_credito = item.doxcc_icod_correlativo_nota_credito,
        //                });
        //            }
        //        }
        //        return Lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<ENotaCreditoPago> ListarPagoNotaCredito(long doxcc_icod_correlativo_pago, long doxcc_icod_correlativo_nota_credito, int indTodo, int anio)
        {
            List<ENotaCreditoPago> lista = new List<ENotaCreditoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_NOTA_CREDITO_PAGO_LISTAR(doxcc_icod_correlativo_pago, doxcc_icod_correlativo_nota_credito, indTodo, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoPago()
                        {
                            ncpac_icod_correlativo = item.ncpac_icod_correlativo,
                            doxcc_icod_correlativo_pago = item.doxcc_icod_correlativo_pago,
                            doxcc_icod_correlativo_nota_credito = item.doxcc_icod_correlativo_nota_credito,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            AbreviaturaNotaCredito = item.AbreviaturaNotaCredito,
                            AbreviaturaPago = item.AbreviaturaPago,
                            iid_correlativo_nota_credito = item.iid_correlativo_nota_credito,
                            iid_correlativo_pago = item.iid_correlativo_pago,
                            vnumero_documento_NC = item.vnumero_documento_NC,
                            vnumero_documento_pago = item.vnumero_documento_pago,
                            SaldoDXCNotaCredito = item.SaldoDXCNotaCredito,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            ncpac_nmonto_pago = item.ncpac_nmonto_pago,
                            ncpac_nmonto_tipo_cambio = item.ncpac_nmonto_tipo_cambio,
                            ncpac_vdescripcion = item.ncpac_vdescripcion,
                            ncpac_sfecha_pago = item.ncpac_sfecha_pago,
                            ncpac_iorigen = item.ncpac_iorigen,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            ncpac_iusuario_crea = item.ncpac_iusuario_crea,
                            ncpac_vpc_crea = item.ncpac_vpc_crea,
                            pdxcc_icod_correlativo = item.pdxpc_icod_correlativo,
                            intCliente = Convert.ToInt32(item.cliec_icod_cliente),
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            iid_tipo_moneda_pago = item.iid_tipo_moneda_pago
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
        public List<ENotaCreditoPago> ListarPagoNotaCredito2(long doxcc_icod_correlativo_nota_credito, int anio)
        {
            List<ENotaCreditoPago> lista = new List<ENotaCreditoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_NOTA_CREDITO_PAGO_LISTAR2(doxcc_icod_correlativo_nota_credito, anio);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoPago()
                        {
                            ncpac_icod_correlativo = item.ncpac_icod_correlativo,
                            doxcc_icod_correlativo_pago = item.doxcc_icod_correlativo_pago,
                            doxcc_icod_correlativo_nota_credito = item.doxcc_icod_correlativo_nota_credito,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            AbreviaturaNotaCredito = item.AbreviaturaNotaCredito,
                            AbreviaturaPago = item.AbreviaturaPago,
                            iid_correlativo_nota_credito = item.iid_correlativo_nota_credito,
                            iid_correlativo_pago = item.iid_correlativo_pago,
                            vnumero_documento_NC = item.vnumero_documento_NC,
                            vnumero_documento_pago = item.vnumero_documento_pago,
                            SaldoDXCNotaCredito = item.SaldoDXCNotaCredito,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            ncpac_nmonto_pago = item.ncpac_nmonto_pago,
                            ncpac_nmonto_tipo_cambio = item.ncpac_nmonto_tipo_cambio,
                            ncpac_vdescripcion = item.ncpac_vdescripcion,
                            ncpac_sfecha_pago = item.ncpac_sfecha_pago,
                            ncpac_iorigen = item.ncpac_iorigen,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            ncpac_iusuario_crea = item.ncpac_iusuario_crea,
                            ncpac_vpc_crea = item.ncpac_vpc_crea,
                            pdxcc_icod_correlativo = item.pdxpc_icod_correlativo,
                            intCliente = Convert.ToInt32(item.cliec_icod_cliente),
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            iid_tipo_moneda_pago = item.iid_tipo_moneda_pago
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
        public List<ENotaCreditoPago> ListarPagoNotaCreditoBancos(int mes)
        {
            List<ENotaCreditoPago> lista = new List<ENotaCreditoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_NOTA_CREDITO_PAGO_LISTAR_BANCOS(mes);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoPago()
                        {
                            ncpac_icod_correlativo = item.ncpac_icod_correlativo,
                            doxcc_icod_correlativo_pago = item.doxcc_icod_correlativo_pago,
                            doxcc_icod_correlativo_nota_credito = item.doxcc_icod_correlativo_nota_credito,
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            AbreviaturaNotaCredito = item.AbreviaturaNotaCredito,
                            AbreviaturaPago = item.AbreviaturaPago,
                            iid_correlativo_nota_credito = item.iid_correlativo_nota_credito,
                            iid_correlativo_pago = item.iid_correlativo_pago,
                            vnumero_documento_NC = item.vnumero_documento_NC,
                            vnumero_documento_pago = item.vnumero_documento_pago,
                            SaldoDXCNotaCredito = item.SaldoDXCNotaCredito,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            ncpac_nmonto_pago = item.ncpac_nmonto_pago,
                            ncpac_nmonto_tipo_cambio = item.ncpac_nmonto_tipo_cambio,
                            ncpac_vdescripcion = item.ncpac_vdescripcion,
                            ncpac_sfecha_pago = item.ncpac_sfecha_pago,
                            ncpac_iorigen = item.ncpac_iorigen,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            ncpac_iusuario_crea = item.ncpac_iusuario_crea,
                            ncpac_vpc_crea = item.ncpac_vpc_crea,
                            pdxcc_icod_correlativo = item.pdxpc_icod_correlativo,
                            intCliente = Convert.ToInt32(item.cliec_icod_cliente),
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            iid_tipo_moneda_pago = item.iid_tipo_moneda_pago,
                            TDEntidadFinanciera = Convert.ToInt32(item.TDEntidadFinanciera),
                            AbreviaturaEF = item.AbreviaturaEF,
                            NumDOCEF = item.NumDOCEF,
                            GlosaEF = item.GlosaEF,
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
        public long insertarNCPago(ENotaCreditoPago obj)
        {
            try
            {
                long? IdNotaCreditoPago = 0;
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_NOTA_CREDITO_PAGO_INSERTAR(
                        ref IdNotaCreditoPago,
                        obj.doxcc_icod_correlativo_pago,
                        obj.doxcc_icod_correlativo_nota_credito,
                        obj.tablc_iid_tipo_moneda,
                        obj.ncpac_nmonto_pago,
                        obj.ncpac_nmonto_tipo_cambio,
                        obj.ncpac_vdescripcion,
                        obj.ncpac_sfecha_pago,
                        obj.ncpac_iorigen,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.ncpac_iusuario_crea,
                        obj.ncpac_vpc_crea,
                        obj.pdxcc_icod_correlativo,
                        obj.ncpac_flag_estado,
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
        public void modificarNCPago(ENotaCreditoPago obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_NOTA_CREDITO_PAGO_ACTUALIZAR(
                        obj.ncpac_icod_correlativo,
                        obj.ncpac_nmonto_pago,
                        obj.ncpac_nmonto_tipo_cambio,
                        obj.ncpac_vdescripcion,
                        obj.ncpac_sfecha_pago,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.ncpac_iusuario_modifica,
                        obj.ncpac_vpc_modifica,
                        obj.mobac_icod_correlativo
                        );
                }
                //ActualizarPagoDocumentoXCobrar(obj2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarNCPago(ENotaCreditoPago obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_NOTA_CREDITO_PAGO_ELIMINAR(
                        obj.ncpac_icod_correlativo,
                        obj.ncpac_vpc_modifica,
                        obj.ncpac_iusuario_modifica);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /**/
        #region Pago Directo
        public List<EDocXCobrarPago> ListarPagoDirectoDocumentoXCobrar(long doxcc_icod_correlativo)
        {
            List<EDocXCobrarPago> lista = new List<EDocXCobrarPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEDXC_DOC_X_COBRAR_PAGO_DIRECTO_LISTAR(doxcc_icod_correlativo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrarPago()
                        {
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            pdxcc_vnumero_doc = item.pdxcc_vnumero_doc,
                            pdxcc_sfecha_cobro = item.pdxcc_sfecha_cobro,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            pdxcc_nmonto_cobro = item.pdxcc_nmonto_cobro,
                            pdxcc_nmonto_tipo_cambio = item.pdxcc_nmonto_tipo_cambio,
                            pdxcc_vobservacion = item.pdxcc_vobservacion,
                            efctc_icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            CuentaBancaria = item.CuentaBancaria,
                            EntidadFinanciera = item.EntidadFinanciera,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            CuentaContable = item.CuentaContable,
                            DescripcionCuentaContable = item.DescripcionCuentaContable,
                            IndicadorCosto = Convert.ToInt32(item.IndicadorCosto),
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            CentroCosto = item.CentroCosto,
                            CentroCostoDesc = item.CentroCostoDesc,
                            anac_icod_analitica = item.anac_icod_analitica,
                            anac_icod_analitica_det = item.anac_icod_analitica_det,
                            TipoAnalitica = string.Format("{0:00}", item.TipoAnalitica),
                            Analitica = item.Analitica,
                            pdxcc_vorigen = item.pdxcc_vorigen,
                            intTipoDoc = item.intTipoDoc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            id_tipo_moneda_dxc = Convert.ToInt32(item.id_tipo_moneda_dxc),
                            strNroDoc = item.strNroDoc,
                            anac_iid_anio = Convert.ToInt32(item.anac_iid_anio)
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
        public long InsertarPagoDirectoDocumentoXCobrar(EDocXCobrarPago obj)
        {
            try
            {
                long? IdDocumentoPorCobrarPago = 0;
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_PAGO_DIRECTO_INSERTAR(
                        ref IdDocumentoPorCobrarPago,
                        obj.doxcc_icod_correlativo,
                        obj.tdocc_icod_tipo_doc,
                        obj.pdxcc_vnumero_doc,
                        obj.pdxcc_sfecha_cobro,
                        obj.tablc_iid_tipo_moneda,
                        obj.pdxcc_nmonto_cobro,
                        obj.pdxcc_nmonto_cobro_dxc,
                        obj.pdxcc_nmonto_tipo_cambio,
                        obj.pdxcc_vobservacion,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cecoc_icod_centro_costo,
                        obj.anac_icod_analitica,
                        obj.anac_icod_analitica_det,
                        obj.pdxcc_vorigen,
                        obj.intUsuario,
                        obj.strPc,
                        obj.pdxcc_flag_estado,
                        obj.doxcc_icod_correlativo_pago,
                        obj.anac_iid_anio
                    );
                }

                return Convert.ToInt64(IdDocumentoPorCobrarPago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPagoDirectoDocumentoXCobrar(EDocXCobrarPago obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_PAGO_DIRECTO_ACTUALIZAR(
                        obj.pdxcc_icod_correlativo,
                        obj.doxcc_icod_correlativo,
                        obj.tdocc_icod_tipo_doc,
                        obj.pdxcc_vnumero_doc,
                        obj.pdxcc_sfecha_cobro,
                        obj.tablc_iid_tipo_moneda,
                        obj.pdxcc_nmonto_cobro,
                        obj.pdxcc_nmonto_cobro_dxc,
                        obj.pdxcc_nmonto_tipo_cambio,
                        obj.pdxcc_vobservacion,
                        obj.efctc_icod_enti_financiera_cuenta,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cecoc_icod_centro_costo,
                        obj.anac_icod_analitica,
                        obj.anac_icod_analitica_det,
                        obj.pdxcc_vorigen,
                        obj.intUsuario,
                        obj.strPc,
                        obj.pdxcc_flag_estado,
                        obj.saldoDxP,
                        obj.pagoDxP,
                        null,
                        null,
                        null,
                        obj.anac_iid_anio
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPagoDirectoDocumentoXCobrar(EDocXCobrarPago obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEDXC_DOC_X_COBRAR_PAGO_DIRECTO_ELIMINAR(
                        obj.pdxcc_icod_correlativo,
                        obj.strPc,
                        obj.intUsuario
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void InsertarCanjeDXCconDXP(EDocPorPagarPago canje, int opcionVCO)
        {
            try
            {
                TesoreriaData p = new TesoreriaData();
                EDocXCobrarPago objDXC = new EDocXCobrarPago();
                objDXC.doxcc_icod_correlativo_pago = InsertarPagoDirectoDocumentoXCobrar(
                    new EDocXCobrarPago()
                    {
                        doxcc_icod_correlativo = Convert.ToInt64(canje.doxcc_icod_correlativo),
                        tdocc_icod_tipo_doc = canje.tdocc_icod_tipo_doc,
                        pdxcc_vnumero_doc = canje.doxpc_vnumero_doc,
                        pdxcc_sfecha_cobro = canje.pdxpc_sfecha_pago,
                        tablc_iid_tipo_moneda = Convert.ToInt32(canje.tablc_iid_tipo_moneda),
                        pdxcc_nmonto_cobro = canje.pdxcc_nmonto_cobro,
                        pdxcc_nmonto_tipo_cambio = canje.pdxpc_nmonto_tipo_cambio,
                        pdxcc_vobservacion = canje.pdxpc_vobservacion,
                        cliec_icod_cliente = canje.cliec_icod_cliente,
                        pdxcc_vorigen = canje.pdxpc_vorigen,
                        intUsuario = canje.intUsuario,
                        strPc = canje.strPc,
                        pdxcc_flag_estado = canje.pdxpc_flag_estado,
                    }
                    );
                p.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(canje.doxcc_icod_correlativo), Convert.ToInt32(canje.tablc_iid_tipo_moneda));

                canje.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(
                    new EDocPorPagarPago()
                    {
                        doxpc_icod_correlativo = Convert.ToInt32(canje.doxpc_icod_correlativo),
                        tdocc_icod_tipo_doc = canje.IcodTD,
                        pdxpc_vnumero_doc = canje.NumDXC,
                        pdxpc_sfecha_pago = canje.pdxpc_sfecha_pago,
                        tablc_iid_tipo_moneda = Convert.ToInt32(canje.tablc_iid_tipo_moneda),
                        pdxpc_nmonto_pago = canje.pdxpc_nmonto_pago,
                        pdxpc_nmonto_pago_dxp = canje.pdxpc_nmonto_pago,
                        pdxpc_nmonto_tipo_cambio = canje.pdxpc_nmonto_tipo_cambio,
                        pdxpc_vobservacion = canje.pdxpc_vobservacion,
                        pdxpc_vorigen = canje.pdxpc_vorigen,
                        doxcc_icod_correlativo = objDXC.doxcc_icod_correlativo_pago,
                        intUsuario = canje.intUsuario,
                        strPc = canje.strPc,
                        pdxpc_mes = canje.pdxpc_mes,
                        anio = canje.anio,
                        pdxpc_flag_estado = canje.pdxpc_flag_estado
                    }
                    );
                //actualizamos montos del documento y situacion
                p.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(canje.doxpc_icod_correlativo), Convert.ToInt32(canje.tablc_iid_tipo_moneda));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarCanjeDXCconDXP(EDocPorPagarPago canje, int opcionVCO)
        {
            try
            {
                TesoreriaData p = new TesoreriaData();

                ActualizarPagoDirectoDocumentoXCobrar(new EDocXCobrarPago()
                {
                    pdxcc_icod_correlativo = Convert.ToInt64(canje.doxcc_icod_correlativo_pago),
                    doxcc_icod_correlativo = Convert.ToInt64(canje.IcodDXC),
                    tdocc_icod_tipo_doc = canje.tdocc_icod_tipo_doc,
                    pdxcc_vnumero_doc = canje.doxpc_vnumero_doc,
                    pdxcc_sfecha_cobro = canje.pdxpc_sfecha_pago,
                    tablc_iid_tipo_moneda = Convert.ToInt32(canje.MonedaDXC),
                    pdxcc_nmonto_cobro = canje.pdxcc_nmonto_cobro,
                    pdxcc_nmonto_tipo_cambio = canje.pdxpc_nmonto_tipo_cambio,
                    pdxcc_vobservacion = canje.pdxpc_vobservacion,
                    cliec_icod_cliente = canje.cliec_icod_cliente,
                    pdxcc_vorigen = canje.pdxpc_vorigen,
                    intUsuario = canje.intUsuario,
                    strPc = canje.strPc,
                    pdxcc_flag_estado = canje.pdxpc_flag_estado,
                }
                    );
                p.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(canje.doxcc_icod_correlativo), canje.MonedaDXC);


                new CuentasPorPagarData().modificarDocPorPagarPago(new EDocPorPagarPago()
                {
                    pdxpc_icod_correlativo = canje.pdxpc_icod_correlativo,
                    doxpc_icod_correlativo = Convert.ToInt64(canje.doxpc_icod_correlativo),
                    tdocc_icod_tipo_doc = canje.IcodTD,
                    pdxpc_vnumero_doc = canje.NumDXC,
                    pdxpc_sfecha_pago = canje.pdxpc_sfecha_pago,
                    tablc_iid_tipo_moneda = Convert.ToInt32(canje.tablc_iid_tipo_moneda),
                    pdxpc_nmonto_pago = canje.pdxpc_nmonto_pago,
                    pdxpc_nmonto_pago_dxp = canje.pdxpc_nmonto_pago,
                    pdxpc_nmonto_tipo_cambio = canje.pdxpc_nmonto_tipo_cambio,
                    pdxpc_vobservacion = canje.pdxpc_vobservacion,
                    pdxpc_vorigen = canje.pdxpc_vorigen,
                    doxcc_icod_correlativo = Convert.ToInt64(canje.doxcc_icod_correlativo_pago),
                    intUsuario = canje.intUsuario,
                    strPc = canje.strPc,
                    pdxpc_mes = canje.pdxpc_mes,
                    anio = canje.anio,
                    pdxpc_flag_estado = canje.pdxpc_flag_estado
                });
                //actualizamos montos del documento y situacion
                p.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(canje.doxpc_icod_correlativo), 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarCanjeDXCconDXP(EDocXCobrarDocxPagarCanje canje)
        {
            try
            {
                TesoreriaData p = new TesoreriaData();
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SIGTS_DOC_X_COBRAR_DOC_X_PAGAR_CANJE_ELIMINAR(
                        canje.canjec_icod_correlativo,
                        canje.intUsuario,
                        canje.strPc
                        );
                }

                EliminarPagoDirectoDocumentoXCobrar(new EDocXCobrarPago()
                {
                    pdxcc_icod_correlativo = Convert.ToInt64(canje.pdxcc_icod_correlativo),
                    doxcc_icod_correlativo = Convert.ToInt64(canje.doxcc_icod_correlativo),
                    strPc = canje.strPc,
                    intUsuario = canje.intUsuario,
                    saldoDxP = canje.doxcc_nmonto_saldo,
                    pagoDxP = canje.doxcc_nmonto_pagado,
                    tdocc_icod_tipo_doc = canje.tipo_doc_dxp,
                    doxcc_icod_correlativo_pago = Convert.ToInt64(canje.doxcc_icod_correlativo),
                    pdxcc_nmonto_cobro = canje.pdxcc_nmonto_cobro
                }
                    );
                //actualizamos montos del documento y situacion
                p.ActualizarMontoDXCPagadoSaldo(Convert.ToInt64(canje.doxcc_icod_correlativo), 0);

                new CuentasPorPagarData().eliminarDocPorPagarPago(new EDocPorPagarPago()
                {
                    pdxpc_icod_correlativo = canje.pdxpc_icod_correlativo,
                    doxpc_icod_correlativo = Convert.ToInt64(canje.doxpc_icod_correlativo),
                    strPc = canje.strPc,
                    intUsuario = canje.intUsuario,
                    saldoDxP = Convert.ToDecimal(canje.doxpc_nmonto_total_saldo),
                    pagoDxP = Convert.ToDecimal(canje.doxpc_nmonto_total_pagado),
                    tdocc_icod_tipo_doc = canje.tipo_doc_dxc,
                    doxpc_icod_correlativo_pago = Convert.ToInt64(canje.doxpc_icod_correlativo),
                    pdxpc_nmonto_pago = canje.pdxpc_nmonto_pago
                }
                    );
                //actualizamos montos del documento y situacion
                p.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(canje.doxpc_icod_correlativo), 0);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EDocXCobrar> listarDxcConPagos(int intEjercicio, int? intCliente)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DOC_X_COBRAR_CON_PAGOS_LISTAR(intEjercicio, intCliente);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            mesec_iid_mes = item.mesec_iid_mes,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                            DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            DireccionCliente = item.DireccionCliente,
                            NumDocCliente = item.NumDocCliente,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/" : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            tablc_iid_tipo_pago = item.tablc_iid_tipo_pago,
                            FormaPago = item.FormaPago,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
                            doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                            doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,
                            ValorVenta = item.ValorVenta,
                            doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                            doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxc_bind_cuenta_corriente = item.doxc_bind_cuenta_corriente,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            doxcc_bind_impresion_nogerencia = item.doxcc_bind_impresion_nogerencia,
                            doxc_bind_situacion_legal = item.doxc_bind_situacion_legal,
                            doxc_bind_cierre_cuenta_corriente = item.doxc_bind_cierre_cuenta_corriente,
                            doxcc_tipo_comprobante_referencia = Convert.ToInt32(item.doxcc_tipo_comprobante_referencia),
                            doxcc_num_serie_referencia = item.doxcc_num_serie_referencia,
                            doxcc_num_comprobante_referencia = item.doxcc_num_comprobante_referencia,
                            doxcc_sfecha_emision_referencia = item.doxcc_sfecha_emision_referencia
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
        public List<EDocXCobrar> listarDxcConPagosSuministros(int intEjercicio, int? intCliente)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DOC_X_COBRAR_CON_PAGOS_LISTAR_SUMINISTROS(intEjercicio, intCliente);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            mesec_iid_mes = item.mesec_iid_mes,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                            DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            DireccionCliente = item.DireccionCliente,
                            NumDocCliente = item.NumDocCliente,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/" : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            tablc_iid_tipo_pago = item.tablc_iid_tipo_pago,
                            FormaPago = item.FormaPago,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
                            doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                            doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,
                            ValorVenta = item.ValorVenta,
                            doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                            doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxc_bind_cuenta_corriente = item.doxc_bind_cuenta_corriente,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            doxcc_bind_impresion_nogerencia = item.doxcc_bind_impresion_nogerencia,
                            doxc_bind_situacion_legal = item.doxc_bind_situacion_legal,
                            doxc_bind_cierre_cuenta_corriente = item.doxc_bind_cierre_cuenta_corriente,
                            doxcc_tipo_comprobante_referencia = Convert.ToInt32(item.doxcc_tipo_comprobante_referencia),
                            doxcc_num_serie_referencia = item.doxcc_num_serie_referencia,
                            doxcc_num_comprobante_referencia = item.doxcc_num_comprobante_referencia,
                            doxcc_sfecha_emision_referencia = item.doxcc_sfecha_emision_referencia
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
        public void ActualizarMontoPagadoSaldo(long intIcodDXC, int intMoneda)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SIGTS_DOC_X_COBRAR_ACTUALIZAR_MONTO_PAGADO_SALDO(
                        intIcodDXC,
                        intMoneda
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EDocXCobrar getDXCDatos(Int64 intIcodDXC)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GET_DXC_DATOS(intIcodDXC);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            mesec_iid_mes = item.mesec_iid_mes,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                            DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            tablc_iid_tipo_pago = item.tablc_iid_tipo_pago,
                            FormaPago = item.FormaPago,
                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
                            doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                            doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,
                            ValorVenta = item.ValorVenta,
                            doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                            doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vobservaciones = (!string.IsNullOrWhiteSpace(item.doxcc_vobservaciones)) ? item.doxcc_vobservaciones : item.doxcc_vdescrip_transaccion,
                            doxc_bind_cuenta_corriente = item.doxc_bind_cuenta_corriente,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            doxcc_bind_impresion_nogerencia = item.doxcc_bind_impresion_nogerencia,
                            doxc_bind_situacion_legal = item.doxc_bind_situacion_legal,
                            doxc_bind_cierre_cuenta_corriente = item.doxc_bind_cierre_cuenta_corriente,
                            doxcc_tipo_comprobante_referencia = Convert.ToInt32(item.doxcc_tipo_comprobante_referencia),
                            doxcc_num_serie_referencia = item.doxcc_num_serie_referencia,
                            doxcc_num_comprobante_referencia = item.doxcc_num_comprobante_referencia,
                            anio = Convert.ToInt32(item.doxcc_ianio),
                            doxcc_origen = item.doxcc_origen
                        });
                    }
                }
                return lista[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EDocXCobrar> listarIcodsDxcConPagos(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                //En este listado se obtienen todos los icod´s de todos los dxc que tengan pagos directos, con adelantos o con notas de crédito
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DXC_PARA_VCO_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocXCobrar> listarRelacionPagosVco(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                //En este listado se obtienen todos los icod´s de todos los dxc que tengan pagos directos, con adelantos o con notas de crédito
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_VERIFICAR_PAGOS_PARA_VCO(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo
                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocXCobrar> listarDxcOrigenDifPlanilla(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            int counter = 0;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DXC_PARA_VCO_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        counter++;
                        if (counter == 46)
                        {
                            int d = 0;
                        }
                        lista.Add(new EDocXCobrar()
                        {

                            dcmlMontoServicios = 0,
                            //dcmlMontoMercaderia = Convert.ToDecimal(item.dcmlMontoMercaderia),
                            intCtaTotal = Convert.ToInt32(item.intCtaTotal),
                            intCtaIGV = Convert.ToInt32(item.intCtaIGV),
                            intCtaMercaderia = Convert.ToInt32(item.intCtaMercaderia),

                            intCtaServicios = Convert.ToInt32(item.intCtaServicios),
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            mesec_iid_mes = item.mesec_iid_mes,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                            DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,

                            doxcc_vdescrip_transaccion = item.doxcc_vdescrip_transaccion,
                            doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                            doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,

                            doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                            doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,

                            //doxcc_nporcentaje_isc = item.doxcc_nporcentaje_isc,
                            //doxcc_nmonto_isc = item.doxcc_nmonto_isc,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            doxcc_vobservaciones = (!string.IsNullOrWhiteSpace(item.doxcc_vobservaciones)) ? item.doxcc_vobservaciones : item.doxcc_vdescrip_transaccion,
                            //docxc_icod_documento = item.docxc_icod_documento,
                            anio = Convert.ToInt32(item.doxcc_ianio),
                            doxcc_origen = item.doxcc_origen,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            anac_iid_analitica = item.anad_iid_analitica,
                            docxc_icod_documento = Convert.ToInt32(item.docxc_icod_documento)

                        });
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                int error = counter;
                throw ex;
            }
        }
        public List<EDocXCobrar> listarDxcOrigenDifPlanillaV1(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    //var query = dc.SGE_DXC_PARA_VCO_LISTAR_V1(intEjercicio, intPeriodo);
                    //foreach (var item in query)
                    //{
                    //    lista.Add(new EDocXCobrar()
                    //    {
                    //        dcmlMontoServicios = Convert.ToDecimal(item.dcmlMontoServicios),
                    //        dcmlMontoMercaderia = Convert.ToDecimal(item.dcmlMontoMercaderia),
                    //        intCtaTotal = Convert.ToInt32(item.intCtaTotal),
                    //        intCtaIGV = Convert.ToInt32(item.intCtaIGV),
                    //        intCtaMercaderia = Convert.ToInt32(item.intCtaMercaderia),
                    //        intCtaServicios = Convert.ToInt32(item.intCtaServicios),
                    //        doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                    //        mesec_iid_mes = item.mesec_iid_mes,
                    //        tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                    //        Abreviatura = item.Abreviatura,
                    //        tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                    //        ClaseDocumento = string.Format("{0:00}", item.ClaseDocumento),
                    //        DescripcionClaseDocumento = item.DescripcionClaseDocumento,
                    //        doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                    //        cliec_icod_cliente = item.cliec_icod_cliente,
                    //        doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                    //        tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                    //        SimboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                    //        doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                    //        doxcc_nmonto_afecto = item.doxcc_nmonto_afecto,
                    //        doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto,
                    //        doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv,
                    //        doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto,
                    //        doxcc_nmonto_total = item.doxcc_nmonto_total,
                    //        doxcc_vobservaciones = item.doxcc_vobservaciones, 
                    //        //docxc_icod_documento = item.docxc_icod_documento,
                    //        anio = Convert.ToInt32(item.doxcc_ianio),
                    //        doxcc_origen = item.doxcc_origen,
                    //        anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                    //        anac_iid_analitica = item.anad_iid_analitica
                    //    });
                    //}
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocXCobrarCuentaContable> BuscarDocumentoXCobrarCuentaContable(long doxcc_icod_correlativo)
        {
            List<EDocXCobrarCuentaContable> lista = new List<EDocXCobrarCuentaContable>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_COBRAR_CUENTA_CONTABLE_BUSCAR(doxcc_icod_correlativo);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrarCuentaContable()
                        {
                            ccdcc_icod_correlativo = item.ccdcc_icod_correlativo,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            ctacc_iid_cuenta_contable = Convert.ToInt64(item.ctacc_iid_cuenta_contable),
                            CuentaContable = item.CuentaContable,
                            DescripcionCuentaContable = item.DescripcionCuentaContable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            CentroCosto = (item.cecoc_icod_centro_costo == null || item.cecoc_icod_centro_costo == 0) ? "  -  " : item.CentroCosto,
                            IndicadorCentroCosto = Convert.ToInt32(item.IndicadorCentroCosto),
                            anac_icod_analitica = item.anac_icod_analitica,
                            TipoAnalitica = (item.anac_icod_analitica == null || item.anac_icod_analitica == 0) ? "  -  " : string.Format("{0:00}", item.TipoAnalitica),
                            Analitica = (item.anac_icod_analitica == null || item.anac_icod_analitica == 0) ? "  -  " : item.Analitica,
                            ccdcc_nmonto = item.ccdcc_nmonto,
                            ccdcc_vglosa = item.ccdcc_vglosa,
                            ccdcc_isituacion = item.ccdcc_isituacion,
                            operacion = 4//indica que este ya había sido creado al momento de modificar
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

        #region Dxc Cta. Contable
        private void insertarDxcCtaCble(List<EDocXCobrarCuentaContable> Lista)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    foreach (EDocXCobrarCuentaContable obj in Lista)
                    {
                        if (obj.operacion == 1)
                        {
                            dc.SIGTS_DOC_X_COBRAR_CUENTA_CONTABLE_INSERTAR(
                                obj.doxcc_icod_correlativo,
                                obj.ctacc_iid_cuenta_contable,
                                obj.cecoc_icod_centro_costo,
                                obj.anac_icod_analitica,
                                obj.ccdcc_nmonto,
                                obj.ccdcc_vglosa,
                                obj.ccdcc_isituacion,
                                obj.usuario,
                                obj.pc
                                );
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void insertarDxcCtaCbleDetalle(EDocXCobrarCuentaContable obj)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {

                    dc.SIGTS_DOC_X_COBRAR_CUENTA_CONTABLE_INSERTAR(
                        obj.doxcc_icod_correlativo,
                        obj.ctacc_iid_cuenta_contable,
                        obj.cecoc_icod_centro_costo,
                        obj.anac_icod_analitica,
                        obj.ccdcc_nmonto,
                        obj.ccdcc_vglosa,
                        obj.ccdcc_isituacion,
                        obj.usuario,
                        obj.pc
                        );

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDxcCtaCble(List<EDocXCobrarCuentaContable> Lista)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    foreach (EDocXCobrarCuentaContable obj in Lista)
                    {
                        if (obj.operacion == 2)
                        {
                            dc.SIGTS_DOC_X_COBRAR_CUENTA_CONTABLE_ACTUALIZAR(
                                obj.ccdcc_icod_correlativo,
                                obj.ccdcc_nmonto,
                                obj.ccdcc_vglosa,
                                obj.usuario,
                                obj.pc,
                                obj.ctacc_iid_cuenta_contable,
                                obj.cecoc_icod_centro_costo,
                                obj.anac_icod_analitica
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDxcCtaCble(List<EDocXCobrarCuentaContable> ListaEliminados)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    foreach (EDocXCobrarCuentaContable obj in ListaEliminados)
                    {
                        dc.SIGTS_DOC_X_COBRAR_CUENTA_CONTABLE_ELIMINAR(obj.ccdcc_icod_correlativo, obj.pc, obj.usuario);
                    }

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
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SIGTS_DOC_X_COBRAR_VERIFICAR_EXISTEN_PAGOS(doxcc_icod_correlativo, tdocc_icod_tipo_doc, ref resultado);
                }
                return Convert.ToBoolean(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<EDocXCobrar> ListarDocumentoAdelantoNotaCreditoPorCobrarCliente(int IdCliente)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_COBRAR_LISTAR_ADELANTOS_NOTA_CREDITO_POR_CLIENTE(IdCliente);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            //docxc_icod_documento = item.docxc_icod_documento,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            MontoDolares = Convert.ToDecimal(item.MontoDolares),
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            SaldoDolares = Convert.ToDecimal(item.SaldoDolares)
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

        public List<EDocXCobrar> BuscarDocumentosXCobrarCliente(int cliec_icod_cliente, int doxcc_ianio)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_COBRAR_BUSCAR_X_CLIENTE_TODA_SITUACION(cliec_icod_cliente, doxcc_ianio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            CodigoClaseDocumento = item.CodigoClaseDocumento,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            mesec_iid_mes = item.mesec_iid_mes,
                            ValorVenta = item.ValorVenta
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
        public List<EDocXCobrar> BuscarDocumentosXCobrarClienteGarantia(int cliec_icod_cliente, int doxcc_ianio)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_COBRAR_BUSCAR_X_CLIENTE_TODA_SITUACION_GARANTIA(cliec_icod_cliente, doxcc_ianio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            CodigoClaseDocumento = item.CodigoClaseDocumento,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            mesec_iid_mes = item.mesec_iid_mes,
                            ValorVenta = item.ValorVenta
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
        public List<EDocXCobrar> EstadoCuentaDetalleCliente(int cliec_icod_cliente, int doxcc_ianio)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_CLIENTE(cliec_icod_cliente, doxcc_ianio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            ValorVenta = item.ValorVenta,
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
        public DataTable EstadoCuentaDetallePagoCliente(int cliec_icod_cliente, int doxcc_ianio)
        {
            DataTable dtResultado = null;

            using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
            {
                var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_PAGO_CLIENTE(cliec_icod_cliente, doxcc_ianio);
                Convertir convierte = new Convertir();
                dtResultado = new DataTable();
                dtResultado = convierte.ConvertirADataTable(query);
                return dtResultado;
            }
        }
        public List<EDocXCobrar> EstadoCuentaDocumentos(int doxcc_ianio)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DOCUMENTOS(doxcc_ianio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            idTipodocuemnto = Convert.ToInt32(item.idTipodocuemnto),
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            ValorVenta = item.ValorVenta,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            cliec_vcod_cliente = item.cliec_vcod_cliente
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
        public List<ECliente> ListarClientesSaldosCobranzaDudosa(int año)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_CLIENTES_SALDOS_COBRANZA_DUDOSA(año);
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = (int)item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            Situacion = item.Situacion,
                            doxcc_nmonto_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxcc_nmonto_saldo_dolares = item.doxcc_nmonto_saldo_dolares,
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro
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
        public List<ECliente> ListarClientesSaldosAUnaFecha(int Anio, DateTime sfecha)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_CLIENTES_SALDOS_LISTAR_A_UNA_FECHA(Anio, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = (int)item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            Situacion = item.Situacion,
                            doxcc_nmonto_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxcc_nmonto_saldo_dolares = item.doxcc_nmonto_saldo_dolares,
                            giroc_icod_giro = Convert.ToInt32(item.giroc_icod_giro),
                            giroc_vnombre_giro = item.giroc_vnombre_giro
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
        public List<EDocXCobrar> BuscarDocumentosXCobrarClienteAUnaFecha(int cliec_icod_cliente, int doxcc_ianio, DateTime sfecha)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_COBRAR_BUSCAR_X_CLIENTE_TODA_SITUACION_A_UNA_FECHA(doxcc_ianio, sfecha, cliec_icod_cliente);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo,
                            CodigoClaseDocumento = item.CodigoClaseDocumento,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento,
                            Situacion = item.Situacion,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            mesec_iid_mes = Convert.ToInt16(item.mesec_iid_mes),
                            ValorVenta = item.ValorVenta
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
        public List<EAdelantoPago> ListarPagoAdelantoXIdDocXCobrarAUnaFecha(long doxcc_icod_correlativo_dxc, int anio, DateTime sfecha)
        {
            List<EAdelantoPago> lista = new List<EAdelantoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_ADELANTO_PAGO_LISTAR_X_ICOD_DXC_A_UNA_FECHA(doxcc_icod_correlativo_dxc, anio, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoPago()
                        {
                            adpac_icod_correlativo = item.adpac_icod_correlativo,
                            //doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            doxcc_icod_correlativo_adelanto = item.doxcc_icod_correlativo_adelanto,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            //tdocc_iid_correlativo = Convert.ToInt32(item.tdocc_iid_correlativo),
                            AbreviaturaAdelanto = item.AbreviaturaAdelanto,
                            //adpac_vnumero_doc_adelanto = item.adpac_vnumero_doc_adelanto,
                            SaldoDXCAdelanto = item.SaldoDXCAdelanto,
                            //cliec_icod_cliente = item.cliec_icod_cliente,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            adpac_nmonto_pago = item.adpac_nmonto_pago,
                            adpac_nmonto_tipo_cambio = item.adpac_nmonto_tipo_cambio,
                            adpac_vdescripcion = item.adpac_vdescripcion,
                            adpac_sfecha_pago = item.adpac_sfecha_pago,
                            adpac_iorigen = item.adpac_iorigen,
                            efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            adpac_iusuario_crea = item.adpac_iusuario_crea,
                            adpac_vpc_crea = item.adpac_vpc_crea,
                            //adpac_isituacion = item.adpac_isituacion,
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo
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
        public List<ENotaCreditoPago> ListarPagoNotaCreditoXIdDocXCobrarAUnaFecha(long doxcc_icod_correlativo_dxc, int anio, DateTime sfecha)
        {
            List<ENotaCreditoPago> lista = new List<ENotaCreditoPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_NOTA_CREDITO_PAGO_LISTAR_X_ICOD_DXC_A_UNA_FECHA(doxcc_icod_correlativo_dxc, anio, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new ENotaCreditoPago()
                        {
                            //ncpac_icod_correlativo = item.ncpac_icod_correlativo,
                            //doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            //doxcc_icod_correlativo_nota_credito = item.doxcc_icod_correlativo_nota_credito,
                            //tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            //AbreviaturaNotaCredito = item.AbreviaturaNotaCredito,
                            //tdocc_iid_correlativo = item.tdocc_iid_correlativo,
                            //ncpac_vnumero_doc_nota_credito = item.ncpac_vnumero_doc_nota_credito,
                            //SaldoDXCNotaCredito = item.SaldoDXCNotaCredito,
                            //tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            //SimboloMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "US$",
                            //ncpac_nmonto_pago = item.ncpac_nmonto_pago,
                            //ncpac_nmonto_tipo_cambio = item.ncpac_nmonto_tipo_cambio,
                            //ncpac_vdescripcion = item.ncpac_vdescripcion,
                            //ncpac_sfecha_pago = item.ncpac_sfecha_pago,
                            //ncpac_iorigen = item.ncpac_iorigen,
                            //efctc_icod_enti_financiera_cuenta = Convert.ToInt32(item.efctc_icod_enti_financiera_cuenta),
                            //ncpac_iusuario_crea = item.ncpac_iusuario_crea,
                            //ncpac_vpc_crea = item.ncpac_vpc_crea,
                            //ncpac_isituacion = item.ncpac_isituacion
                            //pdxcc_icod_correlativo = item.pdxcc_icod_correlativo
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

        public List<EDocXCobrarPago> ListarPagoDocumentoXCobrarXIdDocXCobrarAUnaFecha(long doxcc_icod_correlativo, int anio, DateTime sfecha)
        {
            List<EDocXCobrarPago> lista = new List<EDocXCobrarPago>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_COBRAR_PAGO_LISTAR_X_ICOD_DXC_A_UNA_FECHA(doxcc_icod_correlativo, anio, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrarPago()
                        {
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            Abreviatura = item.Abreviatura,
                            pdxcc_vnumero_doc = item.pdxcc_vnumero_doc,
                            pdxcc_sfecha_cobro = item.pdxcc_sfecha_cobro,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            pdxcc_nmonto_cobro = item.pdxcc_nmonto_cobro,
                            pdxcc_nmonto_tipo_cambio = item.pdxcc_nmonto_tipo_cambio,
                            pdxcc_vobservacion = item.pdxcc_vobservacion,
                            efctc_icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            CuentaBancaria = item.CuentaBancaria,
                            EntidadFinanciera = item.EntidadFinanciera,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            CuentaContable = item.CuentaContable,
                            DescripcionCuentaContable = item.DescripcionCuentaContable,
                            IndicadorCosto = Convert.ToInt32(item.IndicadorCosto),
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            CentroCosto = item.CentroCosto,
                            CentroCostoDesc = item.CentroCostoDesc,
                            anac_icod_analitica = item.anac_icod_analitica,
                            anac_icod_analitica_det = item.anac_icod_analitica_det,
                            TipoAnalitica = string.Format("{0:00}", item.TipoAnalitica),
                            Analitica = item.Analitica,
                            pdxcc_vorigen = item.pdxcc_vorigen
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
        public List<EDocXCobrar> EstadoCuentaDetalleClienteAUnaFecha(int cliec_icod_cliente, int doxcc_ianio, DateTime sfecha)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_CLIENTE_A_UNA_FECHA(doxcc_ianio, sfecha, cliec_icod_cliente);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            ValorVenta = item.ValorVenta,
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

        public DataTable EstadoCuentaDetallePagoClienteAunaFecha(int cliec_icod_cliente, DateTime sfecha, int doxcc_ianio)
        {
            DataTable dtResultado = null;

            using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
            {
                var query = dc.SIGTS_RPT_ESTADO_CUENTA_DETALLE_PAGO_CLIENTE_A_UNA_FECHA(doxcc_ianio, sfecha, cliec_icod_cliente);
                Convertir convierte = new Convertir();
                dtResultado = new DataTable();
                dtResultado = convierte.ConvertirADataTable(query);
                return dtResultado;
            }
        }
        public List<EDocXCobrar> EstadoCuentaDocumentosAUnaFecha(int doxcc_ianio, DateTime sfecha)
        {
            List<EDocXCobrar> lista = new List<EDocXCobrar>(); ;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_ESTADO_CUENTA_DOCUMENTOS_A_UNA_FECHA(doxcc_ianio, sfecha, 0);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocXCobrar()
                        {
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            idTipodocuemnto = Convert.ToInt32(item.idTipodocuemnto),
                            doxcc_vnumero_doc = item.doxcc_vnumero_doc,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_vobservaciones = item.doxcc_vobservaciones,
                            doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc,
                            doxcc_sfecha_entrega = item.doxcc_sfecha_entrega,
                            ValorVenta = item.ValorVenta,
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
        public List<ECliente> ListarClientesSaldosCobranzaDudosaAUnaFecha(int año, DateTime sfecha)
        {
            List<ECliente> lista = new List<ECliente>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_RPT_CLIENTES_SALDOS_COBRANZA_DUDOSA_A_UNA_FECHA(año, sfecha);
                    foreach (var item in query)
                    {
                        lista.Add(new ECliente()
                        {
                            cliec_icod_cliente = (int)item.cliec_icod_cliente,
                            cliec_vcod_cliente = item.cliec_vcod_cliente,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            Situacion = item.Situacion,
                            doxcc_nmonto_saldo_soles = item.doxcc_nmonto_saldo_soles,
                            doxcc_nmonto_saldo_dolares = item.doxcc_nmonto_saldo_dolares,
                            giroc_icod_giro = item.giroc_icod_giro,
                            giroc_vnombre_giro = item.giroc_vnombre_giro
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
        public List<ENotaCreditoCliente> ListarNotaCreditoClienteTodo(int anio)
        {
            List<ENotaCreditoCliente> Lista = new List<ENotaCreditoCliente>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_NOTA_CREDITO_CLIENTE_LISTAR(anio);
                    foreach (var item in query)
                    {
                        Lista.Add(new ENotaCreditoCliente()
                        {
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            doxcc_vnumero_doc = (item.doxcc_vnumero_doc.Contains("-")) ? item.doxcc_vnumero_doc : (item.doxcc_vnumero_doc.Substring(0, 3) + "-" + item.doxcc_vnumero_doc.Substring(3)),
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            doxcc_sfecha_doc = item.doxcc_sfecha_doc,
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            simboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/." : "US$",
                            doxcc_nmonto_total = item.doxcc_nmonto_total,
                            doxcc_nmonto_pagado = item.doxcc_nmonto_pagado,
                            doxcc_nmonto_saldo = item.doxcc_nmonto_saldo,
                            doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            tdodc_descripcion = item.tdocd_descripcion,
                            tablc_iid_situacion_documento = item.tablc_iid_situacion_documento
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }


        #region Letra Por Cobrar - Cabecera

        public List<ELetraPorCobrar> listarLetraPorCobrar(int intEjercicio, int intPeriodo)
        {
            List<ELetraPorCobrar> Lista = new List<ELetraPorCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LETRA_X_COBRAR_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELetraPorCobrar()
                        {
                            lexcc_icod_correlativo = item.lexcc_icod_correlativo,
                            lexcc_icorrelativo = item.lexcc_icorrelativo,
                            anioc_iid_anio = item.anioc_iid_anio,
                            mesec_iid_mes = item.mesec_iid_mes,
                            lexcc_vnumero_letra = item.lexcc_vnumero_letra,
                            lexcc_inumero_renovacion = item.lexcc_inumero_renovacion,
                            lexcc_sfecha_letra = item.lexcc_sfecha_letra,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            lexcc_vaval = item.lexcc_vaval,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            lexcc_nmonto_total = item.lexcc_nmonto_total,
                            lexcc_nmonto_pagado = item.lexcc_nmonto_pagado,
                            lexcc_nmonto_tipo_cambio = item.lexcc_nmonto_tipo_cambio,
                            lexcc_sfecha_vencimiento = item.lexcc_sfecha_vencimiento,
                            tablc_iid_situacion_letra = item.tablc_iid_situacion_letra,
                            tablc_iid_condicion_letra = item.tablc_iid_condicion_letra,
                            efinc_icod_entidad_financiera = item.efinc_icod_entidad_financiera,
                            lexcc_vnumero_ubd = item.lexcc_vnumero_ubd,
                            lexcc_vobservaciones = item.lexcc_vobservaciones,
                            tablc_iid_ubicacion_letra = item.tablc_iid_ubicacion_letra,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            lexcc_icod_correlativo_renovacion = item.lexcc_icod_correlativo_renovacion,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            strSituacion = item.strSituacion,
                            strCondicion = item.strCondicion,
                            strUbicacion = item.strUbicacion,
                            strMoneda = item.strMoneda,
                            strDesCliente = item.strCliente
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
        public List<ELetraPorCobrar> getLetraPorCobrarCab(int intLxc)
        {
            List<ELetraPorCobrar> Lista = new List<ELetraPorCobrar>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_GET_LETRA_X_COBRAR_CAB(intLxc);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELetraPorCobrar()
                        {
                            lexcc_icod_correlativo = item.lexcc_icod_correlativo,
                            lexcc_icorrelativo = item.lexcc_icorrelativo,
                            anioc_iid_anio = item.anioc_iid_anio,
                            mesec_iid_mes = item.mesec_iid_mes,
                            lexcc_vnumero_letra = item.lexcc_vnumero_letra,
                            lexcc_inumero_renovacion = item.lexcc_inumero_renovacion,
                            lexcc_sfecha_letra = item.lexcc_sfecha_letra,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            lexcc_vaval = item.lexcc_vaval,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            lexcc_nmonto_total = item.lexcc_nmonto_total,
                            lexcc_nmonto_pagado = item.lexcc_nmonto_pagado,
                            lexcc_nmonto_tipo_cambio = item.lexcc_nmonto_tipo_cambio,
                            lexcc_sfecha_vencimiento = item.lexcc_sfecha_vencimiento,
                            tablc_iid_situacion_letra = item.tablc_iid_situacion_letra,
                            tablc_iid_condicion_letra = item.tablc_iid_condicion_letra,
                            efinc_icod_entidad_financiera = item.efinc_icod_entidad_financiera,
                            lexcc_vnumero_ubd = item.lexcc_vnumero_ubd,
                            lexcc_vobservaciones = item.lexcc_vobservaciones,
                            tablc_iid_ubicacion_letra = item.tablc_iid_ubicacion_letra,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            lexcc_icod_correlativo_renovacion = item.lexcc_icod_correlativo_renovacion,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            strSituacion = item.strSituacion,
                            strCondicion = item.strCondicion,
                            strUbicacion = item.strUbicacion,
                            strMoneda = item.strMoneda,
                            strDesCliente = item.strCliente
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
        public int insertarLetraPorCobrar(ELetraPorCobrar oBe)
        {
            int? intIcod = 0;
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_COBRAR_INSERTAR(
                        ref intIcod,
                        oBe.lexcc_icorrelativo,
                        oBe.anioc_iid_anio,
                        oBe.mesec_iid_mes,
                        oBe.lexcc_vnumero_letra,
                        oBe.lexcc_inumero_renovacion,
                        oBe.lexcc_sfecha_letra,
                        oBe.cliec_icod_cliente,
                        oBe.lexcc_vaval,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lexcc_nmonto_total,
                        oBe.lexcc_nmonto_pagado,
                        oBe.lexcc_nmonto_tipo_cambio,
                        oBe.lexcc_sfecha_vencimiento,
                        oBe.tablc_iid_situacion_letra,
                        oBe.tablc_iid_condicion_letra,
                        oBe.efinc_icod_entidad_financiera,
                        oBe.lexcc_vnumero_ubd,
                        oBe.lexcc_vobservaciones,
                        oBe.tablc_iid_ubicacion_letra,
                        oBe.doxcc_icod_correlativo,
                        oBe.lexcc_icod_correlativo_renovacion,
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
        public void modificarLetraPorCobrar(ELetraPorCobrar oBe)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_COBRAR_MODIFICAR(
                        oBe.lexcc_icod_correlativo,
                        oBe.lexcc_icorrelativo,
                        oBe.anioc_iid_anio,
                        oBe.mesec_iid_mes,
                        oBe.lexcc_vnumero_letra,
                        oBe.lexcc_inumero_renovacion,
                        oBe.lexcc_sfecha_letra,
                        oBe.cliec_icod_cliente,
                        oBe.lexcc_vaval,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lexcc_nmonto_total,
                        oBe.lexcc_nmonto_pagado,
                        oBe.lexcc_nmonto_tipo_cambio,
                        oBe.lexcc_sfecha_vencimiento,
                        oBe.tablc_iid_situacion_letra,
                        oBe.tablc_iid_condicion_letra,
                        oBe.efinc_icod_entidad_financiera,
                        oBe.lexcc_vnumero_ubd,
                        oBe.lexcc_vobservaciones,
                        oBe.tablc_iid_ubicacion_letra,
                        oBe.doxcc_icod_correlativo,
                        oBe.lexcc_icod_correlativo_renovacion,
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
        public void eliminarLetraPorCobrar(ELetraPorCobrar oBe)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_COBRAR_ELIMINAR(
                        oBe.lexcc_icod_correlativo,
                        oBe.doxcc_icod_correlativo,
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

        #region Letra Por Cobrar - Detalle
        public List<ELetraPorCobrarDet> listarLetraPorCobrarDet(int intLXC)
        {
            List<ELetraPorCobrarDet> Lista = new List<ELetraPorCobrarDet>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_LETRA_X_COBRAR_DET_LISTAR(intLXC);
                    foreach (var item in query)
                    {
                        Lista.Add(new ELetraPorCobrarDet()
                        {
                            lxcpc_icod_correlativo = item.lxcpc_icod_correlativo,
                            lexcc_icod_correlativo = item.lexcc_icod_correlativo,
                            tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            lxcpc_nmonto_pago = item.lxcpc_nmonto_pago,
                            lxcpc_nmonto_tipo_cambio = Convert.ToDecimal(item.lxcpc_nmonto_tipo_cambio),
                            lxcpc_sfecha_doc = item.lxcpc_sfecha_doc,
                            lxcpc_sfecha_pago = item.lxcpc_sfecha_pago,
                            lxcpc_vconcepto = item.lxcpc_vconcepto,
                            doxcc_icod_correlativo = item.doxcc_icod_correlativo,
                            pdxcc_icod_correlativo = item.pdxcc_icod_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_iid_clase_doc = item.tdocc_iid_clase_doc,
                            pdxcc_vnumero_doc = item.pdxcc_vnumero_doc,
                            tablc_iid_tipo_cuenta_debe_haber = item.tablc_iid_tipo_cuenta_debe_haber,
                            ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            anac_icod_analitica = item.anac_icod_analitica,
                            lxcpc_isituacion = item.lxcpc_isituacion,
                            lxcpc_tipo_pago = item.lxcpc_tipo_pago,
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
        public void insertarLetraPorCobrarDet(ELetraPorCobrarDet oBe)
        {

            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_COBRAR_DET_INSERTAR(
                        oBe.lexcc_icod_correlativo,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lxcpc_nmonto_pago,
                        oBe.lxcpc_nmonto_tipo_cambio,
                        oBe.lxcpc_sfecha_doc,
                        oBe.lxcpc_sfecha_pago,
                        oBe.lxcpc_vconcepto,
                        oBe.doxcc_icod_correlativo,
                        oBe.pdxcc_icod_correlativo,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tdocc_iid_clase_doc,
                        oBe.pdxcc_vnumero_doc,
                        oBe.tablc_iid_tipo_cuenta_debe_haber,
                        oBe.ctacc_iid_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.anac_icod_analitica,
                        oBe.lxcpc_isituacion,
                        oBe.lxcpc_tipo_pago,
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
        public void modificarLetraPorCobrarDet(ELetraPorCobrarDet oBe)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_COBRAR_DET_MODIFICAR(
                        oBe.lxcpc_icod_correlativo,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.lxcpc_nmonto_pago,
                        oBe.lxcpc_nmonto_tipo_cambio,
                        oBe.lxcpc_sfecha_doc,
                        oBe.lxcpc_sfecha_pago,
                        oBe.lxcpc_vconcepto,
                        oBe.doxcc_icod_correlativo,
                        oBe.pdxcc_icod_correlativo,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.tdocc_iid_clase_doc,
                        oBe.pdxcc_vnumero_doc,
                        oBe.tablc_iid_tipo_cuenta_debe_haber,
                        oBe.ctacc_iid_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.anac_icod_analitica,
                        oBe.lxcpc_isituacion,
                        oBe.lxcpc_tipo_pago,
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
        public void eliminarLetraPorCobrarDet(ELetraPorCobrarDet oBe)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_LETRA_X_COBRAR_DET_ELIMINAR(
                        oBe.lxcpc_icod_correlativo,
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

        public EAnioEjercicio VerificarExistenciaAnoSiguiente(int anioc_inumero_anho)
        {
            EAnioEjercicio obj = new EAnioEjercicio();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    var query = dc.SGEV_VERIFICAR_EXISTENCIA_AÑO_SIGUIENTE(anioc_inumero_anho);
                    foreach (var item in query)
                    {
                        obj.anioc_icod_anio_ejercicio = item.anioc_icod_anio_ejercicio;
                        obj.anioc_iid_anio_ejercicio = (int)item.anioc_iid_anio_ejercicio;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public void CierreDocumentoXCobrar(int año, int iUSUARIO)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGEV_DOC_X_COBRAR_CIERRE(año, iUSUARIO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarTipoCambio(string opcion)
        {
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {
                    dc.SGE_ACTUALIZAR_TIPO_CAMBIO(opcion);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EGarantiaClientes> getGarantiaClientesCab(int intLxc)
        {
            List<EGarantiaClientes> lista = new List<EGarantiaClientes>();
            try
            {
                using (CuentasPorCobrarDataContext dc = new CuentasPorCobrarDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_GET_GARANTIA_CLIENTES_CAB(intLxc);
                    foreach (var item in query)
                    {
                        lista.Add(new EGarantiaClientes()
                        {
                            garc_icod_garantia = item.garc_icod_garantia,
                            garc_vnumero_garantia = item.garc_vnumero_garantia,
                            garc_sfecha_garantia = Convert.ToDateTime(item.garc_sfecha_garantia),
                            tablc_iid_situacion = Convert.ToInt32(item.tablc_iid_situacion),
                            Situacion = item.Situacion,
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            NomClie = item.NomClie,
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto),
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            CentroCostos = item.CentroCostos,
                            DesProyecto = item.DesProyecto,
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            Moneda = item.Moneda,
                            garc_nmonto = Convert.ToDecimal(item.garc_nmonto),
                            favc_icod_factura = Convert.ToInt32(item.favc_icod_factura),
                            NumDoc = item.NumDoc,
                            intDXP = Convert.ToInt64(item.intDXP),
                            pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo),
                            ClaseFac = Convert.ToInt32(item.ClaseFactura),
                            NumDXC = item.NumDXC

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
    }
}
