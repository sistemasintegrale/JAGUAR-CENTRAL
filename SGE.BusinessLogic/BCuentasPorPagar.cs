using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BCuentasPorPagar
    {
        CuentasPorPagarData objCuentasPorPagarData = new CuentasPorPagarData();
        TesoreriaData objTesoreriaData = new TesoreriaData();
        public int getSituacionDocPorPagar(Int64 intDXP)
        {
            try
            {
                return new CuentasPorPagarData().getSituacionDocPorPagar(intDXP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocPorPagar> ListarDocumentoPorPagarProveedor(int intProveedor, int intEjercicio)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).ListarDocumentoPorPagarProveedor(intProveedor, intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListarDocumentoPorPagarTodo(int intEjercicio)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).ListarDocumentoPorPagarTodo(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListarDocumentoAdelantoNotaCreditoPorCobrarProveedor(int intProveedor, int intEjercicio)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarDocumentoAdelantoNotaCreditoPorCobrarProveedor(intProveedor, intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EAdelantoProveedor> ListarAdelantoProveedoresCorrelativo(int intEjercicio)
        {
            List<EAdelantoProveedor> lista = null;
            try
            {
                lista = (new VentasData()).ListarAdelantoProveedoresCorrelativo(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public EAdelantoProveedor ListarAdelantoProveedor(int IdLibroBanco)
        {
            List<EAdelantoProveedor> lista = null;
            EAdelantoProveedor objE_AdelantoProveedor = new EAdelantoProveedor();
            try
            {
                lista = (new TesoreriaData()).ListarAdelantoProveedor(IdLibroBanco);
                if (lista.Count > 0)
                {
                    objE_AdelantoProveedor.icod_correlativo_cabecera = lista[0].icod_correlativo_cabecera;
                    objE_AdelantoProveedor.iid_anio = lista[0].iid_anio;
                    objE_AdelantoProveedor.iid_mes = lista[0].iid_mes;
                    objE_AdelantoProveedor.ii_tipo_doc = lista[0].ii_tipo_doc;
                    objE_AdelantoProveedor.vdescripcion_beneficiario = lista[0].vdescripcion_beneficiario;
                    objE_AdelantoProveedor.iid_tipo_moneda = lista[0].iid_tipo_moneda;
                    objE_AdelantoProveedor.nmonto_tipo_cambio = lista[0].nmonto_tipo_cambio;
                    objE_AdelantoProveedor.nmonto_movimiento = lista[0].nmonto_movimiento;
                    objE_AdelantoProveedor.cflag_conciliacion = lista[0].cflag_conciliacion;
                    objE_AdelantoProveedor.sfecha_adelanto = lista[0].sfecha_adelanto;
                    objE_AdelantoProveedor.vnro_documento = lista[0].vnro_documento;
                    objE_AdelantoProveedor.vglosa = lista[0].vglosa;
                    objE_AdelantoProveedor.iid_situacion_movimiento_banco = lista[0].iid_situacion_movimiento_banco;
                    objE_AdelantoProveedor.icod_correlativo = lista[0].icod_correlativo;
                    objE_AdelantoProveedor.vnumero_adelanto = lista[0].vnumero_adelanto;
                    objE_AdelantoProveedor.icod_proveedor = lista[0].icod_proveedor;
                    objE_AdelantoProveedor.proc_vnombrecompleto = lista[0].proc_vnombrecompleto;
                    objE_AdelantoProveedor.nmonto_canjeado = lista[0].nmonto_canjeado;
                    objE_AdelantoProveedor.vobservacion = lista[0].vobservacion;
                    objE_AdelantoProveedor.nsituacion_adelanto_proveedor = lista[0].nsituacion_adelanto_proveedor;
                    objE_AdelantoProveedor.doxpc_icod_correlativo = lista[0].doxpc_icod_correlativo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objE_AdelantoProveedor;
        }
        public long getCorrelativoDocPorPagar(int intEjercicio, int intPeriodo)
        {
            long lngCorrelativo = 0;
            try
            {
                lngCorrelativo = new CuentasPorPagarData().getCorrelativoDocPorPagar(intEjercicio, intPeriodo);
                return lngCorrelativo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocPorPagar> ListarEDocPorPagar(EDocPorPagar obj)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).listarDocPorPagar(obj);
                foreach (var item in lista)
                {
                    item.dxp_dcta_cuadra = (item.Valorcompra == item.cdxpc_suma_monto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public long InsertarEDocPorPagar(EDocPorPagar oBeDXP, List<EDocPorPagarDetalleCuentaContable> lstDetCtaContable, List<EDocPorPagarDetalleNacional> lstDetNacional, List<EDXPImportacion> lstDXPImportacion)
        {
            try
            {
                CuentasPorPagarData objDocumentoPorPagarData = new CuentasPorPagarData();
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertamos en la cabecera
                    long IdDocumentoXPagar = objDocumentoPorPagarData.insertarDocPorPagar(oBeDXP);
                    if (IdDocumentoXPagar == 0)
                    {
                        throw new Exception("El número del documento ya fue generado");
                    }
                    oBeDXP.doxpc_icod_correlativo = IdDocumentoXPagar;
                    //Insertamos en el detalle cuenta contable
                    if (lstDetCtaContable != null)
                    {
                        foreach (EDocPorPagarDetalleCuentaContable item in lstDetCtaContable)
                        {
                            item.doxpc_icod_correlativo = IdDocumentoXPagar;
                            int idAnalitica;
                            int.TryParse(item.IdTipoAnalitica, out idAnalitica);

                            //if (idAnalitica == 5)
                            //    item.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(new EDocPorPagarPago()
                            //    {
                            //        doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo_dxp),
                            //        tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc,
                            //        pdxpc_vnumero_doc = oBeDXP.doxpc_vnumero_doc,
                            //        pdxpc_sfecha_pago = oBeDXP.doxpc_sfecha_doc,
                            //        tablc_iid_tipo_moneda = oBeDXP.tablc_iid_tipo_moneda,
                            //        pdxpc_nmonto_pago = item.cdxpc_nmonto_cuenta,
                            //        pdxpc_nmonto_pago_dxp = item.pdxpc_nmonto_pago_dxp,
                            //        pdxpc_nmonto_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio,
                            //        pdxpc_vobservacion = item.cdxpc_vglosa,
                            //        pdxpc_vorigen = "L",
                            //        ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            //        cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            //        anac_icod_analitica = item.anac_icod_analitica,
                            //        intUsuario = item.intUsuario,
                            //        strPc = item.strPc,
                            //        pdxpc_mes = oBeDXP.mesec_iid_mes,
                            //        pdxpc_flag_estado = item.cdxpc_flag_estado,
                            //        doxpc_icod_correlativo_pago = item.doxpc_icod_correlativo
                            //    }
                            //);
                            objDocumentoPorPagarData.insertarDXPDetCtaContable(item);
                        }
                    }
                    if (lstDXPImportacion != null)
                    {
                        List<EImportacionConceptos> lstpreImportacionConcepto = new List<EImportacionConceptos>();
                        List<EDXPImportacion> lstDXPImpDet = new List<EDXPImportacion>();
                        lstDXPImportacion.ForEach(x =>
                        {
                            x.doxpc_icod_correlativo = IdDocumentoXPagar;
                            new BCompras().InsertarDXPImportacion(x);
                            lstpreImportacionConcepto = new BCompras().ListarImportacionConceptos(x.impc_icod_importacion);

                            lstpreImportacionConcepto.ForEach(xs =>
                            {
                                //int IcodImpoDet = 0;
                                //IcodImpoDet=Convert.ToInt32(xs.impd_icod_importacion_detalle);
                                lstDXPImpDet = new BCompras().listarDXPImpDet(Convert.ToInt32(xs.impd_icod_importacion_detalle));
                                decimal TotalSoles = 0;
                                decimal TotalSolesConvertir = 0;
                                decimal Suma = 0;
                                EDXPImportacion Obe1 = new EDXPImportacion();
                                List<EDXPImportacion> lisImpo = new List<EDXPImportacion>();
                                lstDXPImpDet.ForEach(xss =>
                                {
                                    if (x.tablc_iid_tipo_moneda == 4)
                                    {
                                        TotalSolesConvertir = x.dxpd2_nmonto_importacion * x.doxpc_nmonto_tipo_cambio;
                                        lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSolesConvertir });
                                    }
                                    else
                                    {
                                        TotalSoles = x.dxpd2_nmonto_importacion;
                                        lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSoles });
                                    }
                                    Suma = lisImpo.Sum(xsss => xsss.dxpd2_nmonto_importacion);
                                    new BCompras().modificarImportacionConceptosMontoSoles(Convert.ToInt32(xs.impd_icod_importacion_detalle), Suma);
                                });




                            });

                        });
                    }


                    tx.Complete();
                    return IdDocumentoXPagar;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarEDocPorPagar(EDocPorPagar oBeDXP, List<EDocPorPagarDetalleCuentaContable> lstDetCtaContable, List<EDocPorPagarDetalleNacional> lstDetNacional,
            List<EDocPorPagarDetalleCuentaContable> lstDeleteCtaContable, List<EDocPorPagarDetalleNacional> lstDeleteNacional, List<EDXPImportacion> DeletelstDXPImportacion,
            List<EDXPImportacion> lstDXPImportacion)
        {
            try
            {
                CuentasPorPagarData objDocumentoPorPagarData = new CuentasPorPagarData();
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizamos la cabecera
                    objDocumentoPorPagarData.modificarDocPorPagar(oBeDXP);

                    //Actualizamos el detalle de la cuenta contable
                    if (lstDetCtaContable != null)
                    {
                        foreach (EDocPorPagarDetalleCuentaContable item in lstDetCtaContable)
                        {
                            if (item.TipOper == 1)//operacion 1 : NUEVO
                            {
                                item.doxpc_icod_correlativo = oBeDXP.doxpc_icod_correlativo;
                                int idAnalitica; int.TryParse((item.IdTipoAnalitica == null) ? string.Empty : item.IdTipoAnalitica, out idAnalitica);
                                if (idAnalitica == 5)
                                    item.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(new EDocPorPagarPago()
                                    {
                                        doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo_dxp),
                                        tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc,
                                        pdxpc_vnumero_doc = oBeDXP.doxpc_vnumero_doc,
                                        pdxpc_sfecha_pago = oBeDXP.doxpc_sfecha_doc,
                                        tablc_iid_tipo_moneda = oBeDXP.tablc_iid_tipo_moneda,
                                        pdxpc_nmonto_pago = item.cdxpc_nmonto_cuenta,
                                        pdxpc_nmonto_pago_dxp = item.pdxpc_nmonto_pago_dxp,
                                        pdxpc_nmonto_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio,
                                        pdxpc_vobservacion = item.cdxpc_vglosa,
                                        pdxpc_vorigen = "L",
                                        ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                                        cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                                        anac_icod_analitica = item.anac_icod_analitica,
                                        intUsuario = item.intUsuario,
                                        strPc = item.strPc,
                                        pdxpc_mes = oBeDXP.mesec_iid_mes,
                                        pdxpc_flag_estado = item.cdxpc_flag_estado,
                                        doxpc_icod_correlativo_pago = item.doxpc_icod_correlativo
                                    }
                                    );
                                objDocumentoPorPagarData.insertarDXPDetCtaContable(item);
                            }
                            else if (item.TipOper == 2)//operacion 2 MODIFICAR
                            {
                                item.doxpc_icod_correlativo = Convert.ToInt64(oBeDXP.doxpc_icod_correlativo);
                                if (item.pdxpc_icod_correlativo != null)
                                {
                                    new CuentasPorPagarData().modificarDocPorPagarPago(new EDocPorPagarPago()
                                    {
                                        pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo),
                                        doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo_dxp),
                                        tdocc_icod_tipo_doc = oBeDXP.tdocc_icod_tipo_doc,
                                        pdxpc_vnumero_doc = oBeDXP.doxpc_vnumero_doc,
                                        pdxpc_sfecha_pago = oBeDXP.doxpc_sfecha_doc,
                                        tablc_iid_tipo_moneda = oBeDXP.tablc_iid_tipo_moneda,
                                        pdxpc_nmonto_pago = item.cdxpc_nmonto_cuenta,
                                        pdxpc_nmonto_pago_dxp = item.pdxpc_nmonto_pago_dxp,
                                        pdxpc_nmonto_tipo_cambio = oBeDXP.doxpc_nmonto_tipo_cambio,
                                        pdxpc_vobservacion = oBeDXP.doxpc_vdescrip_transaccion,
                                        pdxpc_vorigen = "L",
                                        ctacc_iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                                        cecoc_icod_centro_costo = (item.cecoc_icod_centro_costo == 0) ? null : item.cecoc_icod_centro_costo,
                                        anac_icod_analitica = (item.anac_icod_analitica == 0) ? null : item.anac_icod_analitica,
                                        intUsuario = item.intUsuario,
                                        strPc = item.strPc,
                                        pdxpc_mes = oBeDXP.mesec_iid_mes,
                                        pdxpc_flag_estado = item.cdxpc_flag_estado,
                                        saldoDxP = Convert.ToDecimal(item.doxpc_nmonto_total_saldo),
                                        pagoDxP = Convert.ToDecimal(item.doxpc_nmonto_total_pagado)
                                    }
                                    );
                                }
                                objDocumentoPorPagarData.modificarDXPDetCtaContable(item);
                            }
                        }
                    }
                    if (lstDeleteCtaContable != null && lstDeleteCtaContable.Count > 0)
                    {
                        objDocumentoPorPagarData.eliminarDXPDetCtaContable(lstDeleteCtaContable, null);
                    }
                    if (DeletelstDXPImportacion != null)
                    {
                        DeletelstDXPImportacion.ForEach(x =>
                        {
                            new BCompras().eliminarDXPImportacion(x);
                        });
                        lstDXPImportacion.ForEach(x =>
                        {
                            List<EImportacionConceptos> lstpreImportacionConcepto = new List<EImportacionConceptos>();
                            List<EDXPImportacion> lstDXPImpDet = new List<EDXPImportacion>();
                            x.doxpc_icod_correlativo = oBeDXP.doxpc_icod_correlativo;
                            new BCompras().InsertarDXPImportacion(x);

                            //int IcodImpoDet = 0;
                            //IcodImpoDet=Convert.ToInt32(xs.impd_icod_importacion_detalle);
                            lstDXPImpDet = new BCompras().listarDXPImpDet(Convert.ToInt32(x.impd_icod_importacion_detalle));
                            decimal TotalSoles = 0;
                            decimal TotalSolesConvertir = 0;
                            decimal Suma = 0;
                            EDXPImportacion Obe1 = new EDXPImportacion();
                            List<EDXPImportacion> lisImpo = new List<EDXPImportacion>();
                            lstDXPImpDet.ForEach(xss =>
                            {
                                if (x.tablc_iid_tipo_moneda == 4)
                                {
                                    TotalSolesConvertir = x.dxpd2_nmonto_importacion * x.doxpc_nmonto_tipo_cambio;
                                    lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSolesConvertir });
                                }
                                else
                                {
                                    TotalSoles = x.dxpd2_nmonto_importacion;
                                    lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSoles });
                                }
                                Suma = lisImpo.Sum(xsss => xsss.dxpd2_nmonto_importacion);
                                new BCompras().modificarImportacionConceptosDXPMontoSoles(Convert.ToInt32(x.impd_icod_importacion_detalle), Suma);
                            });





                        });
                    }
                    //new BCompras().eliminarDXPImportacion(DeletelstDXPImportacion);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long InsertarEDocPorPagarSI(EDocPorPagar oBeDXP)
        {
            try
            {
                CuentasPorPagarData objDocumentoPorPagarData = new CuentasPorPagarData();
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Insertamos en la cabecera
                    long IdDocumentoXPagar = objDocumentoPorPagarData.insertarDocPorPagar(oBeDXP);
                    tx.Complete();
                    return IdDocumentoXPagar;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarEDocPorPagarSI(EDocPorPagar oBeDXP)
        {
            try
            {
                CuentasPorPagarData objDocumentoPorPagarData = new CuentasPorPagarData();
                ComprasData objComprasData = new ComprasData();
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Actualizamos la cabecera
                    objDocumentoPorPagarData.modificarDocPorPagar(oBeDXP);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocPorPagarDetalleCuentaContable> listarDXPDetCtaContable(long intDXP)
        {
            List<EDocPorPagarDetalleCuentaContable> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).listarDXPDetCtaContable(intDXP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public bool VerificarExistenPagos(long doxcc_icod_correlativo, Int32 tdocc_icod_tipo_doc)
        {
            try
            {
                return (new CuentasPorPagarData()).VerificarExistenPagos(doxcc_icod_correlativo, tdocc_icod_tipo_doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Pagos DXP
        public List<EDocPorPagarPago> listarDxpPagos(long intDXP, int Anio)
        {
            List<EDocPorPagarPago> lista = null;
            try
            {
                lista = new CuentasPorPagarData().listarDxpPagos(intDXP, Anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarDxpPagoDirecto(EDocPorPagarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    obj.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(obj);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(obj.doxpc_icod_correlativo), 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarDxpPagoDirecto(EDocPorPagarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().modificarDocPorPagarPago(obj);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(obj.doxpc_icod_correlativo), 0);

                    //CrearVoucherContableDXP_PagoDirecto(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarDxpPagoDirecto(EDocPorPagarPago obj)
        //, List<EDocPorPagarPago> obeCanje)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //obeCanje.ForEach(x=>
                    //{
                    //    EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                    //    objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt64(x.doxcc_icod_correlativo);
                    //    objE_DocPorCobrarPago.intUsuario = x.intUsuario;
                    //    objE_DocPorCobrarPago.strPc = x.strPc;
                    //    objE_DocPorCobrarPago.doxcc_icod_correlativo = x.IcodDXC;
                    //    new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);
                    //    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objE_DocPorCobrarPago.doxcc_icod_correlativo, 0/*el tipo de moneda no es relevante*/);
                    //});


                    EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                    objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt64(obj.doxcc_icod_correlativo);
                    objE_DocPorCobrarPago.intUsuario = obj.intUsuario;
                    objE_DocPorCobrarPago.strPc = obj.strPc;
                    objE_DocPorCobrarPago.doxcc_icod_correlativo = obj.IcodDXC;
                    new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objE_DocPorCobrarPago.doxcc_icod_correlativo, 0/*el tipo de moneda no es relevante*/);



                    new CuentasPorPagarData().eliminarDocPorPagarPago(obj);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(obj.doxpc_icod_correlativo), 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<EDocxPagarPagoAdelanto> listarDxpPagosConAdelantos(long intDXP, long intDXA, int anio)
        {
            List<EDocxPagarPagoAdelanto> lista = null;
            try
            {
                lista = objCuentasPorPagarData.ListarAdelantoPago(intDXP, intDXA, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarPagoAdelanto(EDocxPagarPagoAdelanto obj, EDocPorPagarPago objDXPPago, EDocPorPagar oBeDXP)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    if (objDXPPago != null)
                    {
                        //---------------------------------------------------------
                        objDXPPago.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(objDXPPago);
                        //---------------------------------------------------------
                        objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objDXPPago.doxpc_icod_correlativo, 0);
                    }
                    //---------------------------------------------------------
                    obj.pdxpc_icod_correlativo = objDXPPago.pdxpc_icod_correlativo;
                    //---------------------------------------------------------
                    objCuentasPorPagarData.insertarAdelantoPago(obj);
                    //---------------------------------------------------------
                    objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(obj.doxpc_icod_correlativo_adelanto, 0);
                    /*EDGAR*/
                    //CrearVoucherContableCanjeAdelantos(obj_DXP, objDXPPago, obj);
                    /*EDGAR*/
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarAdelantoPago(EDocxPagarPagoAdelanto objAD, EDocPorPagarPago objPago, EDocPorPagar oBeDXP)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCuentasPorPagarData.modificarAdelantoPago(objAD);

                    new CuentasPorPagarData().modificarDocPorPagarPago(objPago);

                    //---------------------------------------------------------
                    objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objAD.doxpc_icod_correlativo_pago, 0);

                    //---------------------------------------------------------
                    objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(objAD.doxpc_icod_correlativo_adelanto, 0);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDocPorPagar> listarEDocPorPagarPendientes(int intEjercicio)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().ListarEDocPorPagarPendientes(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> listarEDocPorPagarNoPendientes(int intEjercicio, int intProveedor)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = objTesoreriaData.ListarEDocPorPagarNoPendientes(intEjercicio, intProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListarEDocPorPagarTodosPorProveedor(int intEjercicio, int intProveedor)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = objTesoreriaData.ListarEDocPorPagarTodosPorProveedor(intEjercicio, intProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void EliminarPagoAdelanto(EDocxPagarPagoAdelanto objAD, EDocPorPagarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCuentasPorPagarData.eliminarAdelantoPago(objAD);
                    //---------------------------------------------------------
                    new CuentasPorPagarData().eliminarDocPorPagarPago(obj);
                    //---------------------------------------------------------
                    objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objAD.doxpc_icod_correlativo_pago, 0);
                    //---------------------------------------------------------
                    objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(objAD.doxpc_icod_correlativo_adelanto, 0);
                    //---------------------------------------------------------
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarEDocPorPagar(EDocPorPagar obeDocPorPagar, List<EDocPorPagarDetalleCuentaContable> ListaEliminadosCtaCont, List<EDXPImportacion> DeletelstDXPImportacion)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCuentasPorPagarData.eliminarDocPorPagar(obeDocPorPagar);
                    /**/
                    EliminarDocPorPagarDetalleCuentaContable(ListaEliminadosCtaCont);
                    /**/

                    if (DeletelstDXPImportacion != null)
                    {
                        DeletelstDXPImportacion.ForEach(x =>
                    {
                        new BCompras().eliminarDXPImportacion(x);
                    });
                    }

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarDocPorPagarDetalleCuentaContable(List<EDocPorPagarDetalleCuentaContable> ListaEliminados)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (ListaEliminados != null && ListaEliminados.Count > 0)
                    {
                        objCuentasPorPagarData.eliminarDXPDetCtaContable(ListaEliminados, null);
                    }
                    tx.Complete();
                }
            }
            catch
            {

            }
        }
        public List<EDocPorPagarNotaCredito> listarDxpPagosConNc(long doxpc_icod_correlativo_pago, long doxpc_icod_correlativo_NC, int anio)
        {
            List<EDocPorPagarNotaCredito> lista = null;
            try
            {
                lista = objCuentasPorPagarData.listarDxpPagosNc(doxpc_icod_correlativo_pago, doxpc_icod_correlativo_NC, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagarNotaCredito> listarDxpPagosConNc2(int periodo)
        {
            List<EDocPorPagarNotaCredito> lista = null;
            try
            {
                lista = objCuentasPorPagarData.listarDxpPagosNc2(periodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarPagoNotaCredito(EDocPorPagarNotaCredito obj, EDocPorPagarPago objDXPPago, EDocPorPagar oBeDXP, EDocPorPagar obj_NC)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    if (objDXPPago != null)
                    {
                        objDXPPago.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(objDXPPago);
                        //actualizamos montos del documento y situacion
                        new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(objDXPPago.doxpc_icod_correlativo), 0);
                    }
                    obj.pdxpc_icod_correlativo = objDXPPago.pdxpc_icod_correlativo;
                    objCuentasPorPagarData.insertarNotaCreditoPago(obj);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoPagadoSaldoNotaCreditoProveedor(obj.doxpc_icod_correlativo_nota_credito, 0);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPagoNotaCredito(EDocPorPagarNotaCredito objNC, EDocPorPagarPago objPago, EDocPorPagar oBeDXP)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCuentasPorPagarData.modificarNotaCreditoPago(objNC);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoPagadoSaldoNotaCreditoProveedor(objNC.doxpc_icod_correlativo_nota_credito, 0);

                    objCuentasPorPagarData.modificarDocPorPagarPago(objPago);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(objPago.doxpc_icod_correlativo), 0);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EDocXCobrarDocxPagarCanje ListaDatosCanjexIcodDxpPago(long icod_pago)
        {
            EDocXCobrarDocxPagarCanje obj = null;
            try
            {
                obj = objCuentasPorPagarData.ListaDatosCanjexIcodDxpPago(icod_pago);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public void EliminarPagoNotaCredito(EDocPorPagarNotaCredito objNC, EDocPorPagarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objCuentasPorPagarData.eliminarNotaCreditoPago(objNC);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoPagadoSaldoNotaCreditoProveedor(objNC.doxpc_icod_correlativo_nota_credito, 0);

                    new CuentasPorPagarData().eliminarDocPorPagarPago(obj);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(objNC.doxpc_icod_correlativo_pago), 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EDxPDatosAdicionales> ListaDxPDatosAdicionalesXIcod(long icod)
        {
            List<EDxPDatosAdicionales> lista = null;
            try
            {
                lista = objCuentasPorPagarData.ListaDxPDatosAdicionalesXIcod(icod);

                if (lista[0].condicion == 0 && lista[0].tdocc_icod_tipo_doc != Parametros.intTipoDocTicketCintaMaqReg)
                {
                    if (lista[0].doxpc_numdoc_tipo != null)
                    {
                        if (lista[0].doxpc_numdoc_tipo == 1)
                        {
                            if (lista[0].doxpc_vnumero_doc.Contains("-"))
                            {
                                lista[0].doxpc_num_serie = lista[0].doxpc_vnumero_doc.Substring(0, 3);
                                lista[0].doxpc_num_doc_domiciliado = lista[0].doxpc_vnumero_doc.Substring(4);
                            }
                            else
                            {
                                lista[0].doxpc_num_serie = lista[0].doxpc_vnumero_doc.Substring(0, 3);
                                lista[0].doxpc_num_doc_domiciliado = lista[0].doxpc_vnumero_doc.Substring(3);
                            }
                        }
                        else
                            lista[0].doxpc_num_doc_domiciliado = lista[0].doxpc_vnumero_doc;
                    }
                    else
                    {
                        if (lista[0].tdocc_icod_tipo_doc != Parametros.intTipoDocLetraProveedor)
                        {
                            if (lista[0].doxpc_vnumero_doc.Contains("-"))
                            {
                                lista[0].doxpc_num_serie = lista[0].doxpc_vnumero_doc.Substring(0, 3);
                                lista[0].doxpc_num_doc_domiciliado = lista[0].doxpc_vnumero_doc.Substring(4);
                            }
                            else
                            {
                                lista[0].doxpc_num_serie = lista[0].doxpc_vnumero_doc.Substring(0, 3);
                                lista[0].doxpc_num_doc_domiciliado = lista[0].doxpc_vnumero_doc.Substring(3);
                            }
                        }
                        else
                            lista[0].doxpc_num_doc_domiciliado = lista[0].doxpc_vnumero_doc;
                    }
                }

                //DOCUMENTO REFERENCIA

                if (lista[0].doxpc_iid_tipo_doc_referencia != null)
                {
                    if (lista[0].doxpc_iid_tipo_doc_referencia != Parametros.intTipoDocLetraProveedor)
                    {
                        if (lista[0].doxpc_vnumero_doc.Contains("-"))
                        {
                            lista[0].doxpc_num_serie_referencia = lista[0].doxpc_vnro_doc_referencia.Substring(0, 3);
                            lista[0].doxpc_num_comprobante_referencia = lista[0].doxpc_vnro_doc_referencia.Substring(4);
                        }
                        else
                        {
                            lista[0].doxpc_num_serie_referencia = lista[0].doxpc_vnro_doc_referencia.Substring(0, 3);
                            lista[0].doxpc_num_comprobante_referencia = lista[0].doxpc_vnro_doc_referencia.Substring(3);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListaDxPPendXFecha(DateTime fecha, int prov_icod, int analitica_icod)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().ListaDxPPendXFecha(fecha, prov_icod, analitica_icod);
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //new CuentasPorPagarData.InsertarDxPDatosAdicionales(obe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EProveedor> ListarProveedoresSaldos(int anio, int doxpc_iid_tipo_documento)
        {
            List<EProveedor> lista = null;
            try
            {
                lista = new CuentasPorPagarData().ListarProveedoresSaldos(anio, doxpc_iid_tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EProveedor> ListarGarantiaProveedoresSaldos(int anio, int doxpc_iid_tipo_documento)
        {
            List<EProveedor> lista = null;
            try
            {
                lista = new CuentasPorPagarData().ListarGarantiaProveedoresSaldos(anio, doxpc_iid_tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarProveedor(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().BuscarDocumentosXPagarProveedor(proc_icod_proveedor, doxcc_ianio, tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ConsultarDocumentos(int doxcc_ianio)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().ConsultarDocumentos(doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarGarantiaProveedor(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().BuscarDocumentosXPagarGarantiaProveedor(proc_icod_proveedor, doxcc_ianio, tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocxPagarPagoAdelanto> ListarPagoAdelantoXIdAdelanto(long doxcc_icod_correlativo_pago, long doxcc_icod_correlativo_adelanto, int anio)
        {
            List<EDocxPagarPagoAdelanto> lista = null;
            try
            {
                lista = new CuentasPorPagarData().ListarAdelantoPago(doxcc_icod_correlativo_pago, doxcc_icod_correlativo_adelanto, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDocumentosProveedor(int anio, int icod_tipo_documento)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().EstadoCuentaDocumentosProveedor(anio, icod_tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDocumentosGarantiaProveedor(int anio, int icod_tipo_documento)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().EstadoCuentaDocumentosGarantiaProveedor(anio, icod_tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> EstadoCuentaDetalleProveedores(int proc_icod_proveedor, int anio, int tipo_documento)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().EstadoCuentaDetalleProveedores(proc_icod_proveedor, anio, tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public DataTable EstadoCuentaDetallePagoProveedor(int proc_icod_proveedor, int doxcc_ianio, int tipo_documento)
        {
            DataTable dt = null;
            try
            {
                dt = (new CuentasPorPagarData()).EstadoCuentaDetallePagoProveedor(proc_icod_proveedor, doxcc_ianio, tipo_documento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarFechaVencimiento(int doxcc_ianio, DateTime sfechaInicial, DateTime sfechaSalir)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).BuscarDocumentosXPagarFechaVencimiento(doxcc_ianio, sfechaInicial, sfechaSalir);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EProveedor> ListarProveedoresSaldosAUnaFecha(int anio, DateTime sfecha)
        {
            List<EProveedor> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).ListarProveedoresSaldosAUnaFecha(anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocxPagarPagoAdelanto> ListarPagoAdelantoXIdAdelantoAUnaFecha(long doxcc_icod_correlativo_adelanto, int anio, DateTime sfecha)
        {
            List<EDocxPagarPagoAdelanto> lista = null;
            try
            {
                lista = (new CuentasPorPagarData()).ListarPagoAdelantoXIdAdelantoAUnaFecha(doxcc_icod_correlativo_adelanto, anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagarPago> ListarPagoDocumentoXPagarXIdDocXPagarAunaFecha(long doxpc_icod_correlativo, int Anio, DateTime sfecha)
        {
            List<EDocPorPagarPago> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarPagoDocumentoXPagarXIdDocXPagarAunaFecha(doxpc_icod_correlativo, Anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarProveedorAUnaFecha(int proc_icod_proveedor, DateTime sfecha, int doxcc_ianio)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().BuscarDocumentosXPagarProveedorAUnaFecha(proc_icod_proveedor, doxcc_ianio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EDocPorPagar> EstadoCuentaDetalleProveedoresAUnaFecha(int proc_icod_proveedor, int anio, DateTime sfecha)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().EstadoCuentaDetalleProveedoresAUnaFecha(proc_icod_proveedor, anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public DataTable EstadoCuentaDetallePagoProveedorAunaFecha(int proc_icod_proveedor, int doxcc_ianio, DateTime sfecha)
        {
            DataTable dt = null;
            try
            {
                dt = (new CuentasPorPagarData()).EstadoCuentaDetallePagoProveedorAunaFecha(proc_icod_proveedor, doxcc_ianio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public List<EDocPorPagar> EstadoCuentaDocumentosProveedorAUnaFecha(int anio, DateTime sfecha)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().EstadoCuentaDocumentosProveedorAUnaFecha(anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> BuscarDocumentosXPagarCuenta(int doxcc_ianio, int mes)
        {
            List<EDocPorPagar> lista = null;
            try
            {
                lista = new CuentasPorPagarData().BuscarDocumentosXPagarCuenta(doxcc_ianio, mes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EAdelantoProveedor> ListarAdelantoProveedorTodo(int anio)
        {
            List<EAdelantoProveedor> lista = null;
            EAdelantoProveedor objE_AdelantoProveedor = new EAdelantoProveedor();
            try
            {
                lista = (new TesoreriaData()).ListarAdelantoProveedorTodo(anio);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocPorPagar> ListarEDocPorPagarRHO(EDocPorPagar obj)
        {
            List<EDocPorPagar> Lista = null;
            try
            {
                Lista = (new CuentasPorPagarData()).listarDocPorPagar(obj);
                foreach (EDocPorPagar item in Lista)
                {
                    item.doxpc_vnumero_doc = item.doxpc_vnumero_doc;
                    if (item.tablc_iid_tipo_moneda == Parametros.intTipoMonedaDolares)
                    {
                        item.doxpc_nmonto_destino_gravado = item.doxpc_nmonto_destino_gravado * item.doxpc_nmonto_tipo_cambio;
                        item.doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado * item.doxpc_nmonto_tipo_cambio;
                        item.doxpc_nmonto_retencion_rh = item.doxpc_nmonto_retencion_rh * item.doxpc_nmonto_tipo_cambio;
                    }
                    item.doxpc_nmonto_total_documento = ((item.doxpc_nmonto_destino_gravado != 0) ? item.doxpc_nmonto_destino_gravado : item.doxpc_nmonto_destino_nogravado) - item.doxpc_nmonto_retencion_rh;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        #region Letras Por Cobrar

        public List<ELetraPorPagar> listarLetraPorPagar(int intEjercicio, int intPeriodo)
        {
            List<ELetraPorPagar> Lista = new List<ELetraPorPagar>();
            try
            {
                Lista = new CuentasPorPagarData().listarLetraPorPagar(intEjercicio, intPeriodo);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarLetraPorPagar(ELetraPorPagar oBe, List<ELetraPorPagarDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new CuentasPorPagarData().insertarLetraPorPagar(oBe);
                    oBe.lexpc_icod_correlativo = intIcod;


                    if (!WebDavTest.Crearcarpeta(Path.Combine(Rutas.RutaImagenes, Rutas.RutaLetras), oBe.lexpc_icod_correlativo.ToString()))
                        throw new Exception("Error al Guardar Imagenes en la Nube");
                    if (oBe.imagen != null)
                        if (!WebDavTest.Put($"{Rutas.RutaImagenes}{Rutas.RutaLetras}{oBe.lexpc_icod_correlativo}/", $"{oBe.lexpc_icod_correlativo}.png", oBe.imagen))
                            throw new Exception("Error al Guardar Imagenes en la Nube");
                    /********************************************************/
                    #region Doc. Por Pagar
                    EDocPorPagar objDXP = new EDocPorPagar();
                    objDXP.anio = oBe.lexpc_sfecha_letra.Year;
                    objDXP.mesec_iid_mes = oBe.mesec_iid_mes;
                    objDXP.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocLetraProveedor;
                    objDXP.doxpc_iid_correlativo = 0;
                    objDXP.doxpc_vnumero_doc = oBe.lexpc_vnumero_letra;
                    objDXP.doxpc_sfecha_doc = oBe.lexpc_sfecha_letra;
                    objDXP.doxpc_sfecha_vencimiento_doc = oBe.lexpc_sfecha_vencimiento;
                    objDXP.proc_icod_proveedor = oBe.proc_icod_proveedor;
                    objDXP.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXP.doxpc_nmonto_tipo_cambio = oBe.lexpc_nmonto_tipo_cambio;
                    objDXP.doxpc_vdescrip_transaccion = oBe.lexpc_vobservaciones;
                    objDXP.doxpc_nmonto_destino_gravado = 0;
                    objDXP.doxpc_nmonto_destino_mixto = 0;
                    objDXP.doxpc_nmonto_destino_nogravado = 0;
                    objDXP.doxpc_nmonto_nogravado = 0;
                    objDXP.doxpc_nmonto_referencial_cif = 0;
                    objDXP.doxpc_nmonto_servicio_no_domic = 0;
                    objDXP.doxpc_nmonto_imp_destino_gravado = 0;
                    objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                    objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                    objDXP.doxpc_nmonto_total_pagado = 0;
                    objDXP.doxpc_nmonto_total_documento = oBe.lexpc_nmonto_total;
                    objDXP.doxpc_nmonto_total_saldo = oBe.lexpc_nmonto_total;
                    objDXP.doxpc_nporcentaje_igv = 0;
                    objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXP.doxpc_tipo_comprobante_referencia = 0;
                    objDXP.doxpc_num_serie_referencia = "";
                    objDXP.doxpc_num_comprobante_referencia = "";
                    objDXP.doxpc_sfecha_emision_referencia = null;
                    objDXP.doxpc_nporcentaje_imp_renta = 0;
                    objDXP.doxpc_nporcentaje_isc = 0;
                    objDXP.doxpc_nmonto_isc = 0;
                    objDXP.doxpc_nmonto_retencion_rh = 0;
                    objDXP.intUsuario = oBe.intUsuario;
                    objDXP.strPc = oBe.strPc;
                    objDXP.doxpc_origen = Parametros.origenLetraPorPagar;
                    objDXP.doxpc_flag_estado = true;
                    objDXP.doxpc_icod_documento = oBe.lexpc_icod_correlativo;
                    /**/
                    oBe.doxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagar(objDXP);
                    new CuentasPorPagarData().modificarLetraPorPagar(oBe);
                    /**/
                    #endregion
                    /********************************************************/

                    lstDetalle.ForEach(x =>
                    {
                        x.lexpc_icod_correlativo = intIcod;
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            #region Pago de DXC
                            EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                            obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo);
                            obj_DXP_pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                            obj_DXP_pago.pdxpc_vnumero_doc = oBe.lexpc_vnumero_letra;
                            obj_DXP_pago.pdxpc_sfecha_pago = x.lxppc_sfecha_pago;
                            obj_DXP_pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                            obj_DXP_pago.pdxpc_nmonto_pago = x.lxppc_nmonto_pago;
                            obj_DXP_pago.pdxpc_nmonto_tipo_cambio = x.lxppc_nmonto_tipo_cambio;
                            obj_DXP_pago.pdxpc_vobservacion = x.lxppc_vconcepto;
                            //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                            obj_DXP_pago.pdxpc_vorigen = "L";
                            obj_DXP_pago.doxcc_icod_correlativo = null;
                            //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                            obj_DXP_pago.intUsuario = x.intUsuario;
                            obj_DXP_pago.strPc = x.strPc;
                            obj_DXP_pago.pdxpc_mes = oBe.mesec_iid_mes;
                            obj_DXP_pago.anio = oBe.anioc_iid_anio;
                            obj_DXP_pago.pdxpc_flag_estado = true;
                            x.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(obj_DXP_pago);
                            new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), obj_DXP_pago.tablc_iid_tipo_moneda);
                            #endregion
                        }
                        new CuentasPorPagarData().insertarLetraPorPagarDet(x);

                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaLetras}{oBe.lexpc_icod_correlativo}");
                throw ex;
            }
        }
        public void modificarLetraPorPagarUbiCon(ELetraPorPagar oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().modificarLetraPorPagar(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async void modificarLetraPorPagar(ELetraPorPagar oBe, List<ELetraPorPagarDet> lstDetalle, List<ELetraPorPagarDet> lstDelete)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().modificarLetraPorPagar(oBe);
                    /************************************************************************/
                    #region Doc. Por Pagar
                    EDocPorPagar objDXP = new EDocPorPagar();
                    objDXP.anio = oBe.lexpc_sfecha_letra.Year;
                    objDXP.mesec_iid_mes = oBe.mesec_iid_mes;
                    objDXP.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocLetraProveedor;
                    objDXP.doxpc_iid_correlativo = 0;
                    objDXP.doxpc_vnumero_doc = oBe.lexpc_vnumero_letra;
                    objDXP.doxpc_sfecha_doc = oBe.lexpc_sfecha_letra;
                    objDXP.doxpc_sfecha_vencimiento_doc = oBe.lexpc_sfecha_vencimiento;
                    objDXP.proc_icod_proveedor = oBe.proc_icod_proveedor;
                    objDXP.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXP.doxpc_nmonto_tipo_cambio = oBe.lexpc_nmonto_tipo_cambio;
                    objDXP.doxpc_vdescrip_transaccion = oBe.lexpc_vobservaciones;
                    //objDXP.doxpc_nmonto_destino_gravado = obj.fcoc_nmonto_destino_gravado;
                    //objDXP.doxpc_nmonto_destino_mixto = obj.fcoc_nmonto_destino_mixto;
                    //objDXP.doxpc_nmonto_destino_nogravado = obj.fcoc_nmonto_destino_nogravado;
                    //objDXP.doxpc_nmonto_nogravado = obj.fcoc_nmonto_nogravado;
                    //objDXP.doxpc_nmonto_referencial_cif = 0;
                    //objDXP.doxpc_nmonto_servicio_no_domic = 0;
                    //objDXP.doxpc_nmonto_imp_destino_gravado = obj.fcoc_nmonto_imp_destino_gravado;
                    //objDXP.doxpc_nmonto_imp_destino_mixto = obj.fcoc_nmonto_imp_destino_mixto;
                    //objDXP.doxpc_nmonto_imp_destino_nogravado = obj.fcoc_nmonto_imp_destino_nogravado;
                    objDXP.doxpc_nmonto_total_pagado = 0;
                    objDXP.doxpc_nmonto_total_documento = oBe.lexpc_nmonto_total;
                    objDXP.doxpc_nmonto_total_saldo = oBe.lexpc_nmonto_total;
                    objDXP.doxpc_nporcentaje_igv = null;
                    objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXP.doxpc_tipo_comprobante_referencia = 0;
                    objDXP.doxpc_num_serie_referencia = "";
                    objDXP.doxpc_num_comprobante_referencia = "";
                    objDXP.doxpc_sfecha_emision_referencia = null;
                    objDXP.doxpc_nporcentaje_isc = 0;
                    objDXP.doxpc_nmonto_isc = 0;
                    //objDXP.doxpc_vnro_deposito_detraccion = obj.fcoc_vnro_depo_detraccion;
                    //objDXP.doxpc_sfec_deposito_detraccion = obj.fcoc_sfecha_depo_detraccion;
                    //objDXP.doxpc_icod_documento = obj.fcoc_icod_doc;
                    objDXP.intUsuario = oBe.intUsuario;
                    objDXP.strPc = oBe.strPc;
                    objDXP.doxpc_origen = Parametros.origenLetraPorPagar;
                    objDXP.doxpc_flag_estado = true;
                    objDXP.doxpc_icod_documento = oBe.lexpc_icod_correlativo;
                    /**/

                    new CuentasPorPagarData().modificarDocPorPagar(objDXP);
                    /**/
                    #endregion
                    /************************************************************************/
                    lstDelete.ForEach(x =>
                    {
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            EDocPorPagarPago objE_DocPorCobrarPago = new EDocPorPagarPago();
                            objE_DocPorCobrarPago.pdxpc_icod_correlativo = Convert.ToInt64(x.pdxpc_icod_correlativo);
                            objE_DocPorCobrarPago.intUsuario = oBe.intUsuario;
                            objE_DocPorCobrarPago.strPc = oBe.strPc;
                            new CuentasPorPagarData().eliminarDocPorPagarPago(objE_DocPorCobrarPago);
                            new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), x.tablc_iid_tipo_moneda);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new CuentasPorPagarData().eliminarLetraPorPagarDet(x);

                    });
                    /************************************************************************/
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                            {
                                #region Pago de DXC
                                EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                                obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                obj_DXP_pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor; //tipo doc de la cabecera
                                obj_DXP_pago.pdxpc_vnumero_doc = oBe.lexpc_vnumero_letra;
                                obj_DXP_pago.pdxpc_sfecha_pago = x.lxppc_sfecha_pago;
                                obj_DXP_pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                                obj_DXP_pago.pdxpc_nmonto_pago = x.lxppc_nmonto_pago;
                                obj_DXP_pago.pdxpc_nmonto_tipo_cambio = x.lxppc_nmonto_tipo_cambio;
                                obj_DXP_pago.pdxpc_vobservacion = x.lxppc_vconcepto;
                                //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                                obj_DXP_pago.pdxpc_vorigen = "L";
                                obj_DXP_pago.doxcc_icod_correlativo = null;
                                //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                                obj_DXP_pago.intUsuario = x.intUsuario;
                                obj_DXP_pago.strPc = x.strPc;
                                obj_DXP_pago.pdxpc_mes = oBe.mesec_iid_mes;
                                obj_DXP_pago.anio = oBe.anioc_iid_anio;
                                obj_DXP_pago.pdxpc_flag_estado = true;
                                x.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(obj_DXP_pago);
                                new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), obj_DXP_pago.tablc_iid_tipo_moneda);
                                #endregion
                            }
                            x.lexpc_icod_correlativo = oBe.lexpc_icod_correlativo;
                            new CuentasPorPagarData().insertarLetraPorPagarDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                            {
                                #region Pago de DXC
                                EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                                obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                obj_DXP_pago.pdxpc_icod_correlativo = Convert.ToInt32(x.pdxpc_icod_correlativo);
                                obj_DXP_pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor; //tipo doc de la cabecera
                                obj_DXP_pago.pdxpc_vnumero_doc = oBe.lexpc_vnumero_letra;
                                obj_DXP_pago.pdxpc_sfecha_pago = x.lxppc_sfecha_pago;
                                obj_DXP_pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                                obj_DXP_pago.pdxpc_nmonto_pago = x.lxppc_nmonto_pago;
                                obj_DXP_pago.pdxpc_nmonto_tipo_cambio = x.lxppc_nmonto_tipo_cambio;
                                obj_DXP_pago.pdxpc_vobservacion = x.lxppc_vconcepto;
                                //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                                obj_DXP_pago.pdxpc_vorigen = "L";
                                obj_DXP_pago.doxcc_icod_correlativo = null;
                                //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                                obj_DXP_pago.intUsuario = x.intUsuario;
                                obj_DXP_pago.strPc = x.strPc;
                                obj_DXP_pago.pdxpc_mes = oBe.mesec_iid_mes;
                                obj_DXP_pago.anio = oBe.anioc_iid_anio;
                                obj_DXP_pago.pdxpc_flag_estado = true;

                                new CuentasPorPagarData().modificarDocPorPagarPago(obj_DXP_pago);
                                new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), obj_DXP_pago.tablc_iid_tipo_moneda);
                                #endregion
                            }
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;

                            new CuentasPorPagarData().modificarLetraPorPagarDet(x);
                        }

                    });
                    try { await WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaLetras}{oBe.lexpc_icod_correlativo}/{oBe.lexpc_icod_correlativo}.png"); } catch { };
                    if (oBe.imagen != null)
                        if (!WebDavTest.Put($"{Rutas.RutaImagenes}{Rutas.RutaLetras}{oBe.lexpc_icod_correlativo}/", $"{oBe.lexpc_icod_correlativo}.png", oBe.imagen))
                            throw new Exception("Error al Guardar Imagenes en la Nube");

                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().eliminarLetraPorPagar(oBe);
                    /*******************************************************/
                    EDocPorPagar oBeDXC = new EDocPorPagar();
                    oBeDXC.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                    oBeDXC.intUsuario = oBe.intUsuario;
                    oBeDXC.strPc = oBe.strPc;
                    new CuentasPorPagarData().eliminarDocPorPagar(oBeDXC);
                    /*******************************************************/
                    var lst = new CuentasPorPagarData().listarLetraPorPagarDet(oBe.lexpc_icod_correlativo);
                    lst.ForEach(x =>
                    {
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                            oBeDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(x.pdxpc_icod_correlativo);
                            oBeDXPPago.intUsuario = oBe.intUsuario;
                            oBeDXPPago.strPc = oBe.strPc;
                            new CuentasPorPagarData().eliminarDocPorPagarPago(oBeDXPPago);
                            new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), x.tablc_iid_tipo_moneda);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new CuentasPorPagarData().eliminarLetraPorPagarDet(x);
                        WebDavTest.EliminarArchivo($"{Rutas.RutaImagenes}{Rutas.RutaLetras}{oBe.lexpc_icod_correlativo}");
                    });
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Letras Por Cobrar Detalle

        public List<ELetraPorPagarDet> listarLetraPorPagarDet(int intLXP)
        {
            List<ELetraPorPagarDet> Lista = new List<ELetraPorPagarDet>();
            try
            {
                Lista = new CuentasPorPagarData().listarLetraPorPagarDet(intLXP);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion
        public void CierreDocumentoXPagar(int anio, int iusuario)
        {
            try
            {
                new CuentasPorPagarData().CierreDocumentoXPagar(anio, iusuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Liquidacion Gastos
        public List<ELiquidacionGastos> listarLiquidacionGastos(int intEjercicio, int intPeriodo)
        {
            List<ELiquidacionGastos> Lista = new List<ELiquidacionGastos>();
            try
            {
                Lista = new CuentasPorPagarData().listarLiquidacionGastos(intEjercicio, intPeriodo);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarLiquidacionGastos(ELiquidacionGastos oBe, List<ELiquidacionGastosDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new CuentasPorPagarData().insertarLiquidacionGastos(oBe);
                    oBe.lqgc_icod_correlativo = intIcod;
                    /********************************************************/
                    #region Doc. Por Pagar
                    EDocPorPagar objDXP = new EDocPorPagar();
                    objDXP.anio = oBe.lqgc_sfecha_liq_gasto.Year;
                    objDXP.mesec_iid_mes = oBe.lqgc_sfecha_liq_gasto.Month;
                    objDXP.tdocc_icod_tipo_doc = 116;
                    objDXP.tdodc_iid_correlativo = 86;
                    objDXP.doxpc_iid_correlativo = 0;
                    objDXP.doxpc_vnumero_doc = oBe.lqgc_vnumero_liq_gasto;
                    objDXP.doxpc_sfecha_doc = oBe.lqgc_sfecha_liq_gasto;
                    objDXP.doxpc_sfecha_vencimiento_doc = oBe.lqgc_sfecha_vencimiento;
                    objDXP.proc_icod_proveedor = oBe.proc_icod_proveedor;
                    objDXP.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXP.doxpc_nmonto_tipo_cambio = oBe.lqgc_nmonto_tipo_cambio;
                    objDXP.doxpc_vdescrip_transaccion = oBe.lqgc_vconcepto;
                    objDXP.doxpc_nmonto_destino_mixto = 0;
                    objDXP.doxpc_nmonto_destino_nogravado = 0;
                    objDXP.doxpc_nmonto_nogravado = 0;
                    objDXP.doxpc_nmonto_referencial_cif = 0;
                    objDXP.doxpc_nmonto_servicio_no_domic = 0;
                    objDXP.doxpc_nmonto_imp_destino_gravado = 0;
                    objDXP.doxpc_nmonto_imp_destino_mixto = 0;
                    objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                    objDXP.doxpc_nmonto_total_pagado = 0;
                    objDXP.doxpc_nmonto_total_documento = oBe.lqgc_nmonto_total;
                    objDXP.doxpc_nmonto_total_saldo = oBe.lqgc_nmonto_total;
                    objDXP.doxpc_nporcentaje_igv = 0;
                    objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXP.doxpc_tipo_comprobante_referencia = 0;
                    objDXP.doxpc_num_serie_referencia = "";
                    objDXP.doxpc_num_comprobante_referencia = "";
                    objDXP.doxpc_sfecha_emision_referencia = null;
                    objDXP.doxpc_nporcentaje_imp_renta = 0;
                    objDXP.doxpc_nporcentaje_isc = 0;
                    objDXP.doxpc_nmonto_isc = 0;
                    objDXP.doxpc_nmonto_retencion_rh = 0;
                    objDXP.intUsuario = oBe.intUsuario;
                    objDXP.strPc = oBe.strPc;
                    objDXP.doxpc_origen = Parametros.origenLetraPorPagar;
                    objDXP.doxpc_flag_estado = true;
                    //objDXP.doxpc_icod_documento = oBe.lexpc_icod_correlativo;
                    /**/
                    oBe.doxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagar(objDXP);
                    new CuentasPorPagarData().modificarLiquidacionGastos(oBe);
                    /**/
                    #endregion
                    /********************************************************/

                    lstDetalle.ForEach(x =>
                    {
                        x.lqgc_icod_correlativo = intIcod;
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            #region Pago de DXC
                            EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                            obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo);
                            obj_DXP_pago.tdocc_icod_tipo_doc = 116;
                            obj_DXP_pago.pdxpc_vnumero_doc = oBe.lqgc_vnumero_liq_gasto;
                            obj_DXP_pago.pdxpc_sfecha_pago = x.lqgd_sfecha_pago;
                            obj_DXP_pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                            obj_DXP_pago.pdxpc_nmonto_pago = x.lqgd_nmonto_pago;
                            obj_DXP_pago.pdxpc_nmonto_tipo_cambio = x.lqgd_nmonto_tipo_cambio;
                            obj_DXP_pago.pdxpc_vobservacion = x.lqgd_vconcepto;
                            //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                            obj_DXP_pago.pdxpc_vorigen = "L";
                            obj_DXP_pago.doxcc_icod_correlativo = null;
                            //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                            obj_DXP_pago.intUsuario = x.intUsuario;
                            obj_DXP_pago.strPc = x.strPc;
                            obj_DXP_pago.pdxpc_mes = oBe.mesec_iid_mes;
                            obj_DXP_pago.pdxpc_flag_estado = true;
                            x.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(obj_DXP_pago);
                            new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), obj_DXP_pago.tablc_iid_tipo_moneda);
                            #endregion
                        }
                        new CuentasPorPagarData().insertarLiquidacionGastosDet(x);

                    });
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLiquidacionGastos(ELiquidacionGastos oBe, List<ELiquidacionGastosDet> lstDetalle, List<ELiquidacionGastosDet> lstDelete)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().modificarLiquidacionGastos(oBe);
                    /************************************************************************/
                    #region Doc. Por Pagar
                    EDocPorPagar objDXP = new EDocPorPagar();
                    objDXP.anio = oBe.lqgc_sfecha_liq_gasto.Year;
                    objDXP.mesec_iid_mes = oBe.lqgc_sfecha_liq_gasto.Month;
                    objDXP.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor;
                    objDXP.tdodc_iid_correlativo = Parametros.intClaseTipoDocLetraProveedor;
                    objDXP.doxpc_iid_correlativo = 0;
                    objDXP.doxpc_vnumero_doc = oBe.lqgc_vnumero_liq_gasto;
                    objDXP.doxpc_sfecha_doc = oBe.lqgc_sfecha_liq_gasto;
                    objDXP.doxpc_sfecha_vencimiento_doc = oBe.lqgc_sfecha_vencimiento;
                    objDXP.proc_icod_proveedor = oBe.proc_icod_proveedor;
                    objDXP.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    objDXP.doxpc_nmonto_tipo_cambio = oBe.lqgc_nmonto_tipo_cambio;
                    objDXP.doxpc_vdescrip_transaccion = oBe.lqgc_vconcepto;
                    //objDXP.doxpc_nmonto_destino_gravado = obj.fcoc_nmonto_destino_gravado;
                    //objDXP.doxpc_nmonto_destino_mixto = obj.fcoc_nmonto_destino_mixto;
                    //objDXP.doxpc_nmonto_destino_nogravado = obj.fcoc_nmonto_destino_nogravado;
                    //objDXP.doxpc_nmonto_nogravado = obj.fcoc_nmonto_nogravado;
                    //objDXP.doxpc_nmonto_referencial_cif = 0;
                    //objDXP.doxpc_nmonto_servicio_no_domic = 0;
                    //objDXP.doxpc_nmonto_imp_destino_gravado = obj.fcoc_nmonto_imp_destino_gravado;
                    //objDXP.doxpc_nmonto_imp_destino_mixto = obj.fcoc_nmonto_imp_destino_mixto;
                    //objDXP.doxpc_nmonto_imp_destino_nogravado = obj.fcoc_nmonto_imp_destino_nogravado;
                    objDXP.doxpc_nmonto_total_pagado = 0;
                    objDXP.doxpc_nmonto_total_documento = oBe.lqgc_nmonto_total;
                    objDXP.doxpc_nmonto_total_saldo = oBe.lqgc_nmonto_total;
                    objDXP.doxpc_nporcentaje_igv = null;
                    objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                    objDXP.doxpc_tipo_comprobante_referencia = 0;
                    objDXP.doxpc_num_serie_referencia = "";
                    objDXP.doxpc_num_comprobante_referencia = "";
                    objDXP.doxpc_sfecha_emision_referencia = null;
                    objDXP.doxpc_nporcentaje_isc = 0;
                    objDXP.doxpc_nmonto_isc = 0;
                    //objDXP.doxpc_vnro_deposito_detraccion = obj.fcoc_vnro_depo_detraccion;
                    //objDXP.doxpc_sfec_deposito_detraccion = obj.fcoc_sfecha_depo_detraccion;
                    //objDXP.doxpc_icod_documento = obj.fcoc_icod_doc;
                    objDXP.intUsuario = oBe.intUsuario;
                    objDXP.strPc = oBe.strPc;
                    objDXP.doxpc_origen = Parametros.origenLetraPorPagar;
                    objDXP.doxpc_flag_estado = true;

                    /**/

                    new CuentasPorPagarData().modificarDocPorPagar(objDXP);
                    /**/
                    #endregion
                    /************************************************************************/
                    lstDelete.ForEach(x =>
                    {
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            EDocPorPagarPago objE_DocPorCobrarPago = new EDocPorPagarPago();
                            objE_DocPorCobrarPago.pdxpc_icod_correlativo = Convert.ToInt64(x.pdxpc_icod_correlativo);
                            objE_DocPorCobrarPago.intUsuario = oBe.intUsuario;
                            objE_DocPorCobrarPago.strPc = oBe.strPc;
                            new CuentasPorPagarData().eliminarDocPorPagarPago(objE_DocPorCobrarPago);
                            new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), x.tablc_iid_tipo_moneda);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new CuentasPorPagarData().eliminarLiquidacionGastosDet(x);

                    });
                    /************************************************************************/
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                            {
                                #region Pago de DXC
                                EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                                obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                obj_DXP_pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor; //tipo doc de la cabecera
                                obj_DXP_pago.pdxpc_vnumero_doc = oBe.lqgc_vnumero_liq_gasto;
                                obj_DXP_pago.pdxpc_sfecha_pago = x.lqgd_sfecha_pago;
                                obj_DXP_pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                                obj_DXP_pago.pdxpc_nmonto_pago = x.lqgd_nmonto_pago;
                                obj_DXP_pago.pdxpc_nmonto_tipo_cambio = x.lqgd_nmonto_tipo_cambio;
                                obj_DXP_pago.pdxpc_vobservacion = x.lqgd_vconcepto;
                                //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                                obj_DXP_pago.pdxpc_vorigen = "L";
                                obj_DXP_pago.doxcc_icod_correlativo = null;
                                //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                                obj_DXP_pago.intUsuario = x.intUsuario;
                                obj_DXP_pago.strPc = x.strPc;
                                obj_DXP_pago.pdxpc_mes = oBe.mesec_iid_mes;
                                obj_DXP_pago.pdxpc_flag_estado = true;
                                x.pdxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagarPago(obj_DXP_pago);
                                new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), obj_DXP_pago.tablc_iid_tipo_moneda);
                                #endregion
                            }
                            x.lqgc_icod_correlativo = oBe.lqgc_icod_correlativo;
                            new CuentasPorPagarData().insertarLiquidacionGastosDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                            {
                                #region Pago de DXC
                                EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                                obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(x.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                obj_DXP_pago.pdxpc_icod_correlativo = Convert.ToInt32(x.pdxpc_icod_correlativo);
                                obj_DXP_pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraProveedor; //tipo doc de la cabecera
                                obj_DXP_pago.pdxpc_vnumero_doc = oBe.lqgc_vnumero_liq_gasto;
                                obj_DXP_pago.pdxpc_sfecha_pago = x.lqgd_sfecha_pago;
                                obj_DXP_pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                                obj_DXP_pago.pdxpc_nmonto_pago = x.lqgd_nmonto_pago;
                                obj_DXP_pago.pdxpc_nmonto_tipo_cambio = x.lqgd_nmonto_tipo_cambio;
                                obj_DXP_pago.pdxpc_vobservacion = x.lqgd_vconcepto;
                                //obj_DXP_pago.efctc_icod_enti_financiera_cuenta = x.
                                obj_DXP_pago.pdxpc_vorigen = "L";
                                obj_DXP_pago.doxcc_icod_correlativo = null;
                                //obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                //obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                //obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                                obj_DXP_pago.intUsuario = x.intUsuario;
                                obj_DXP_pago.strPc = x.strPc;
                                obj_DXP_pago.pdxpc_mes = oBe.mesec_iid_mes;
                                obj_DXP_pago.pdxpc_flag_estado = true;

                                new CuentasPorPagarData().modificarDocPorPagarPago(obj_DXP_pago);
                                new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), obj_DXP_pago.tablc_iid_tipo_moneda);
                                #endregion
                            }
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;

                            new CuentasPorPagarData().modificarLiquidacionGastosDet(x);
                        }

                    });

                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().eliminarLiquidacionGastos(oBe);
                    /*******************************************************/
                    EDocPorPagar oBeDXC = new EDocPorPagar();
                    oBeDXC.doxpc_icod_correlativo = oBe.doxpc_icod_correlativo;
                    oBeDXC.intUsuario = oBe.intUsuario;
                    oBeDXC.strPc = oBe.strPc;
                    new CuentasPorPagarData().eliminarDocPorPagar(oBeDXC);
                    /*******************************************************/
                    var lst = new CuentasPorPagarData().listarLiquidacionGastosDet(oBe.lqgc_icod_correlativo);
                    lst.ForEach(x =>
                    {
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                            oBeDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(x.pdxpc_icod_correlativo);
                            oBeDXPPago.intUsuario = oBe.intUsuario;
                            oBeDXPPago.strPc = oBe.strPc;
                            new CuentasPorPagarData().eliminarDocPorPagarPago(oBeDXPPago);
                            new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.doxpc_icod_correlativo), x.tablc_iid_tipo_moneda);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new CuentasPorPagarData().eliminarLiquidacionGastosDet(x);
                    });
                    tx.Complete();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<ELiquidacionGastosDet> listarLiquidacionGastosDet(int intLXP)
        {
            List<ELiquidacionGastosDet> Lista = new List<ELiquidacionGastosDet>();
            try
            {
                Lista = new CuentasPorPagarData().listarLiquidacionGastosDet(intLXP);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarDxpPagoCanje(EDocPorPagarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorPagarData().eliminarDocPorPagarPago(obj);
                    //actualizamos montos del documento y situacion
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(obj.doxpc_icod_correlativo), 0);
                    tx.Complete();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarAdelantoPagoNotaCreditoProveedor(ELibroBancosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (obj.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor)
                    {
                        //Eliminar Adelanto Pago
                        EDocxPagarPagoAdelanto obj_ADC_Pago = new EDocxPagarPagoAdelanto();
                        obj_ADC_Pago.adpap_icod_correlativo = Convert.ToInt32(obj.ncclie_icod_pago);
                        obj_ADC_Pago.adpap_vpc_modifica = obj.vpc_modifica;
                        objCuentasPorPagarData.eliminarAdelantoPago(obj_ADC_Pago);

                        //Actualizar Monto Pagado Saldo
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(obj.doxcc_icod_correlativo), obj.tablc_iid_tipo_moneda);

                    }

                    if (obj.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                    {
                        //Eliminar Nota Credito Pago
                        EDocPorPagarNotaCredito obj_NCC_Pago = new EDocPorPagarNotaCredito();
                        obj_NCC_Pago.ncpap_icod_correlativo = Convert.ToInt32(obj.ncprov_icod_pago);
                        obj_NCC_Pago.ncpac_vpc_modifica = obj.vpc_modifica;
                        objCuentasPorPagarData.eliminarNotaCreditoPago(obj_NCC_Pago);

                        //Actualizar Nota Credito Pagado Saldo                        
                        new TesoreriaData().ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(obj.doxcc_icod_correlativo), obj.tablc_iid_tipo_moneda);

                    }


                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
