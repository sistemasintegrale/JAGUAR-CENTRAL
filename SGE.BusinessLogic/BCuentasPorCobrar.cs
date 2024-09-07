using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BCuentasPorCobrar
    {
        CuentasPorCobrarData objDocXCobrarData = new CuentasPorCobrarData();
        public List<EDocXCobrar> ListarDocPorCobrarxCliente(int? IdCliente, int ANIO)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarDocPorCobrarxCliente(IdCliente, ANIO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> ListarDocumentoAdelantoNotaCreditoPorCobrarCliente(int IdCliente)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarDocumentoAdelantoNotaCreditoPorCobrarCliente(IdCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int getSituacionDocPorCobrar(Int64 int_dxc)
        {
            try
            {
                return objDocXCobrarData.getSituacionDocPorCobrar(int_dxc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EAdelantoCliente> ListarAdelantoClietneXAñoTodos(int año)
        {
            List<EAdelantoCliente> lista = null;
            try
            {
                lista = (new VentasData()).ListarAdelantoClientexAñoTodos(año);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public EAdelantoCliente ListarAdelantoCliente(int IdLibroBanco)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>();
            EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();
            try
            {
                lista = (new TesoreriaData()).ListarAdelantoCliente(IdLibroBanco);
                if (lista.Count > 0)
                {
                    objE_AdelantoCliente.icod_correlativo_cabecera = lista[0].icod_correlativo_cabecera;
                    objE_AdelantoCliente.iid_anio = lista[0].iid_anio;
                    objE_AdelantoCliente.iid_mes = lista[0].iid_mes;
                    objE_AdelantoCliente.ii_tipo_doc = lista[0].ii_tipo_doc;
                    objE_AdelantoCliente.vdescripcion_beneficiario = lista[0].vdescripcion_beneficiario;
                    objE_AdelantoCliente.iid_tipo_moneda = lista[0].iid_tipo_moneda;
                    objE_AdelantoCliente.nmonto_tipo_cambio = lista[0].nmonto_tipo_cambio;
                    objE_AdelantoCliente.nmonto_movimiento = lista[0].nmonto_movimiento;
                    objE_AdelantoCliente.cflag_conciliacion = lista[0].cflag_conciliacion;
                    objE_AdelantoCliente.sfecha_adelanto = lista[0].sfecha_adelanto;
                    objE_AdelantoCliente.vnro_documento = lista[0].vnro_documento;
                    objE_AdelantoCliente.vglosa = lista[0].vglosa;
                    objE_AdelantoCliente.iid_situacion_movimiento_banco = lista[0].iid_situacion_movimiento_banco;
                    objE_AdelantoCliente.icod_correlativo = lista[0].icod_correlativo;
                    objE_AdelantoCliente.vnumero_adelanto = lista[0].vnumero_adelanto;
                    objE_AdelantoCliente.icod_cliente = lista[0].icod_cliente;
                    objE_AdelantoCliente.vnombrecliente = lista[0].vnombrecliente;
                    objE_AdelantoCliente.nmonto_pagado = lista[0].nmonto_pagado;
                    objE_AdelantoCliente.vobservacion = lista[0].vobservacion;
                    objE_AdelantoCliente.nsituacion_adelanto_cliente = lista[0].nsituacion_adelanto_cliente;
                    objE_AdelantoCliente.doxcc_icod_correlativo = lista[0].doxcc_icod_correlativo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objE_AdelantoCliente;
        }


        public void EliminarAdelantoPagoNotaCredito(ELibroBancosDetalle obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (obj.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)
                    {
                        //Eliminar Adelanto Pago
                        EAdelantoPago obj_ADC_Pago = new EAdelantoPago();
                        obj_ADC_Pago.adpac_icod_correlativo = Convert.ToInt32(obj.adclie_icod_pago);
                        obj_ADC_Pago.adpac_vpc_modifica = obj.vpc_modifica;
                        objDocXCobrarData.eliminarAdelantoPago(obj_ADC_Pago);

                        //Actualizar Monto Pagado Saldo
                        new TesoreriaData().ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(obj.doxcc_icod_correlativo), obj.tablc_iid_tipo_moneda);

                    }

                    if (obj.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                    {
                        //Eliminar Nota Credito Pago
                        ENotaCreditoPago obj_NCC_Pago = new ENotaCreditoPago();
                        obj_NCC_Pago.ncpac_icod_correlativo = Convert.ToInt32(obj.ncclie_icod_pago);
                        obj_NCC_Pago.ncpac_vpc_modifica = obj.vpc_modifica;
                        objDocXCobrarData.eliminarNCPago(obj_NCC_Pago);

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
        #region Diferentes listados de Dxc
        public List<EDocXCobrar> listarDxc(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.listarDxc(intEjercicio, intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> listarDxcPendientes(int intEjercicio)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.listarDxcPendientes(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> listarDxcConPagos(int intEjercicio, int? intCliente)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.listarDxcConPagos(intEjercicio, intCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> listarDxcConPagosSuministros(int intEjercicio, int? intCliente)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.listarDxcConPagosSuministros(intEjercicio, intCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> listarIcodsDxcConPagos(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.listarIcodsDxcConPagos(intEjercicio, intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> listarDxcOrigenDifPlanilla(int intEjercicio, int intPeriodo)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.listarDxcOrigenDifPlanilla(intEjercicio, intPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        public List<EDocXCobrarCuentaContable> BuscarDocumentoXCobrarCuentaContable(long doxcc_icod_correlativo)
        {
            List<EDocXCobrarCuentaContable> lista = null;
            try
            {
                lista = objDocXCobrarData.BuscarDocumentoXCobrarCuentaContable(doxcc_icod_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarDocumentoXCobrar(EDocXCobrar objDocXCobrar, List<EDocXCobrarCuentaContable> Lista)
        {

            EDocXCobrarCuentaContable x = new EDocXCobrarCuentaContable();
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.insertarDxc(objDocXCobrar, Lista);
                    /*--------------------------------------------------------*/
                    if (Lista.Count == 0)
                    {
                        if (objDocXCobrar.ctacc_icod_cuenta_gastos_nac != null)
                        {
                            objDocXCobrarData.insertarDxcDetalle(objDocXCobrar, Lista);
                        }
                    }
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarDocumentoXCobrar(EDocXCobrar obj, List<EDocXCobrarCuentaContable> Lista, List<EDocXCobrarCuentaContable> ListaEliminados)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.modificarDxc(obj, Lista, ListaEliminados);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool VerificarExistenPagos(long doxcc_icod_correlativo, Int32 tdocc_icod_tipo_doc)
        {
            try
            {
                return objDocXCobrarData.VerificarExistenPagos(doxcc_icod_correlativo, tdocc_icod_tipo_doc);
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.eliminarDxc(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.AnularDocumentoXCobrar(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<EDocXCobrarPago> ListarPagoDirectoDocumentoXCobrar(long doxcc_icod_correlativo)
        {
            List<EDocXCobrarPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarPagoDirectoDocumentoXCobrar(doxcc_icod_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarPagoDirectoDocumentoXCobrar(EDocXCobrarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    obj.pdxcc_icod_correlativo = objDocXCobrarData.InsertarPagoDirectoDocumentoXCobrar(obj);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj.doxcc_icod_correlativo, 0);
                    tx.Complete();
                }
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.ActualizarPagoDirectoDocumentoXCobrar(obj);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj.doxcc_icod_correlativo, 0);
                    //CrearVoucherContableDXC_PagoDirecto(obj);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.EliminarPagoDirectoDocumentoXCobrar(obj);
                    new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj.doxcc_icod_correlativo, 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EAdelantoPago> ListarAdelantoPago(long doxcc_icod_correlativo_dxc_pago, long doxcc_icod_correlativo_dxc_adelanto)
        {
            List<EAdelantoPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarAdelantoPago(doxcc_icod_correlativo_dxc_pago, doxcc_icod_correlativo_dxc_adelanto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarPagoAdelanto(EAdelantoPago obj, EDocXCobrarPago objDXCPago)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    long? IdDocumentoPorCobrarPago = null;

                    if (objDXCPago != null)
                    {
                        IdDocumentoPorCobrarPago = objDocXCobrarData.InsertarPagoDirectoDocumentoXCobrar(objDXCPago);
                        obj.pdxcc_icod_correlativo = IdDocumentoPorCobrarPago;
                    }

                    objDocXCobrarData.insertarAdelantoPago(obj);

                    TesoreriaData objTesoreriaData = new TesoreriaData();
                    objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(obj.doxcc_icod_correlativo_adelanto, 0);
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(obj.doxcc_icod_correlativo_pago, 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPagoAdelanto(EAdelantoPago objAD, EDocXCobrarPago objPago, EDocXCobrar objDocXCobrar)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.modificarAdelantoPago(objAD);

                    objPago.pdxcc_icod_correlativo = Convert.ToInt64(objAD.pdxcc_icod_correlativo);
                    objDocXCobrarData.ActualizarPagoDirectoDocumentoXCobrar(objPago);
                    TesoreriaData objTesoreriaData = new TesoreriaData();
                    objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(objAD.doxcc_icod_correlativo_adelanto, 0);
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objAD.doxcc_icod_correlativo_pago, 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPagoAdelanto(EAdelantoPago objAD, EDocXCobrarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.eliminarAdelantoPago(objAD);

                    obj.pdxcc_icod_correlativo = Convert.ToInt64(objAD.pdxcc_icod_correlativo);
                    objDocXCobrarData.EliminarPagoDirectoDocumentoXCobrar(obj);

                    TesoreriaData objTesoreriaData = new TesoreriaData();
                    objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(objAD.doxcc_icod_correlativo_adelanto, 0);
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objAD.doxcc_icod_correlativo_pago, 0);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ENotaCreditoPago> ListarPagoNotaCredito(long doxcc_icod_correlativo_pago, long doxcc_icod_correlativo_nota_credito, int indTodo, int anio)
        {
            List<ENotaCreditoPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarPagoNotaCredito(doxcc_icod_correlativo_pago, doxcc_icod_correlativo_nota_credito, indTodo, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ENotaCreditoPago> ListarPagoNotaCredito2(long doxcc_icod_correlativo_nota_credito, int anio)
        {
            List<ENotaCreditoPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarPagoNotaCredito2(doxcc_icod_correlativo_nota_credito, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarPagoNotaCredito(ENotaCreditoPago obj, EDocXCobrarPago objDXCPago, EDocXCobrar obj_DXC, EDocXCobrar obj_NC)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    long? IdDocumentoPorCobrarPago = null;

                    if (objDXCPago != null)
                    {
                        IdDocumentoPorCobrarPago = objDocXCobrarData.InsertarPagoDirectoDocumentoXCobrar(objDXCPago);
                    }
                    objDXCPago.pdxcc_icod_correlativo = Convert.ToInt64(IdDocumentoPorCobrarPago);
                    obj.pdxcc_icod_correlativo = IdDocumentoPorCobrarPago;
                    objDocXCobrarData.insertarNCPago(obj);
                    /**/
                    TesoreriaData objTesoreriaData = new TesoreriaData();
                    objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(obj.doxcc_icod_correlativo_nota_credito, 0);
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(obj.doxcc_icod_correlativo_pago, 0);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarPagoNotaCredito(ENotaCreditoPago objNC, EDocXCobrarPago objPago, EDocXCobrar objDocXCobrar)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.modificarNCPago(objNC);

                    objPago.pdxcc_icod_correlativo = Convert.ToInt64(objPago.pdxcc_icod_correlativo);
                    objDocXCobrarData.ActualizarPagoDirectoDocumentoXCobrar(objPago);
                    /**/
                    TesoreriaData objTesoreriaData = new TesoreriaData();
                    objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(objNC.doxcc_icod_correlativo_nota_credito, 0);
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objNC.doxcc_icod_correlativo_pago, 0);
                    /**/
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarPagoNotaCredito(ENotaCreditoPago objNC, EDocXCobrarPago obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.eliminarNCPago(objNC);
                    objDocXCobrarData.EliminarPagoDirectoDocumentoXCobrar(obj);
                    /**/
                    TesoreriaData objTesoreriaData = new TesoreriaData();
                    objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(objNC.doxcc_icod_correlativo_nota_credito, 0);
                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objNC.doxcc_icod_correlativo_pago, 0);
                    /**/
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ECliente> ListarDocPorCobrarSaldos(int año)
        {
            List<ECliente> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarDocPorCobrarSaldos(año);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECliente> ListarDocPorCobrarSaldosGarantia(int año)
        {
            List<ECliente> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarDocPorCobrarSaldosGarantia(año);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> BuscarDocumentosXCobrarCliente(int cliec_icod_cliente, int doxcc_ianio)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.BuscarDocumentosXCobrarCliente(cliec_icod_cliente, doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> BuscarDocumentosXCobrarClienteGarantia(int cliec_icod_cliente, int doxcc_ianio)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.BuscarDocumentosXCobrarClienteGarantia(cliec_icod_cliente, doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> EstadoCuentaDetalleCliente(int cliec_icod_cliente, int doxcc_ianio)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.EstadoCuentaDetalleCliente(cliec_icod_cliente, doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public DataTable EstadoCuentaDetallePagoCliente(int cliec_icod_cliente, int doxcc_ianio)
        {
            DataTable dt = null;
            try
            {
                dt = objDocXCobrarData.EstadoCuentaDetallePagoCliente(cliec_icod_cliente, doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public List<EDocXCobrar> EstadoCuentaDocumentos(int doxcc_ianio)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.EstadoCuentaDocumentos(doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECliente> ListarClientesSaldosCobranzaDudosa(int año)
        {
            List<ECliente> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarClientesSaldosCobranzaDudosa(año);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECliente> ListarClientesSaldosAUnaFecha(DateTime sfecha, int anio)
        {
            List<ECliente> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarClientesSaldosAUnaFecha(anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> BuscarDocumentosXCobrarClienteAUnaFecha(int cliec_icod_cliente, int doxcc_ianio, DateTime sfecha)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.BuscarDocumentosXCobrarClienteAUnaFecha(cliec_icod_cliente, doxcc_ianio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EAdelantoPago> ListarPagoAdelantoXIdDocXCobrarAUnaFecha(long doxcc_icod_correlativo_dxc, int anio, DateTime sfecha)
        {
            List<EAdelantoPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarPagoAdelantoXIdDocXCobrarAUnaFecha(doxcc_icod_correlativo_dxc, anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ENotaCreditoPago> ListarPagoNotaCreditoXIdDocXCobrarAUnaFecha(long doxcc_icod_correlativo_dxc, int anio, DateTime sfecha)
        {
            List<ENotaCreditoPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarPagoNotaCreditoXIdDocXCobrarAUnaFecha(doxcc_icod_correlativo_dxc, anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrarPago> ListarPagoDocumentoXCobrarXIdDocXCobrarAUnaFecha(long doxcc_icod_correlativo, int anio, DateTime sfecha)
        {
            List<EDocXCobrarPago> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarPagoDocumentoXCobrarXIdDocXCobrarAUnaFecha(doxcc_icod_correlativo, anio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EDocXCobrar> EstadoCuentaDetalleClienteAUnaFecha(int cliec_icod_cliente, int doxcc_ianio, DateTime sfecha)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.EstadoCuentaDetalleClienteAUnaFecha(cliec_icod_cliente, doxcc_ianio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public DataTable EstadoCuentaDetallePagoClienteAunaFecha(int cliec_icod_cliente, DateTime sfecha, int doxcc_ianio)
        {
            DataTable dt = null;
            try
            {
                dt = objDocXCobrarData.EstadoCuentaDetallePagoClienteAunaFecha(cliec_icod_cliente, sfecha, doxcc_ianio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public List<EDocXCobrar> EstadoCuentaDocumentosAUnaFecha(int doxcc_ianio, DateTime sfecha)
        {
            List<EDocXCobrar> lista = null;
            try
            {
                lista = objDocXCobrarData.EstadoCuentaDocumentosAUnaFecha(doxcc_ianio, sfecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ECliente> ListarClientesSaldosCobranzaDudosaAUnaFecha(int año, DateTime sfecha)
        {
            List<ECliente> lista = null;
            try
            {
                lista = objDocXCobrarData.ListarClientesSaldosCobranzaDudosaAUnaFecha(año, sfecha);
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
                Lista = objDocXCobrarData.ListarNotaCreditoClienteTodo(anio);
                foreach (ENotaCreditoCliente item in Lista)
                {
                    item.str_doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yyyy");
                    item.str_doxcc_nmonto_total = Convert.ToDecimal(item.doxcc_nmonto_total).ToString("N2");
                    item.str_doxcc_nmonto_pagado = Convert.ToDecimal(item.doxcc_nmonto_pagado).ToString("N2");
                    item.str_doxcc_nmonto_saldo = Convert.ToDecimal(item.doxcc_nmonto_saldo).ToString("N2");
                }
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EAdelantoCliente> ListarAdelantoClienteTodo(int anio)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>();
            try
            {
                lista = (new TesoreriaData()).ListarAdelantoClienteTodo(anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        #region Letras Por Cobrar

        public List<ELetraPorCobrar> listarLetraPorCobrar(int intEjercicio, int intPeriodo)
        {
            List<ELetraPorCobrar> Lista = new List<ELetraPorCobrar>();
            try
            {
                Lista = objDocXCobrarData.listarLetraPorCobrar(intEjercicio, intPeriodo);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarLetraPorCobrar(ELetraPorCobrar oBe, List<ELetraPorCobrarDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objDocXCobrarData.insertarLetraPorCobrar(oBe);
                    oBe.lexcc_icod_correlativo = intIcod;
                    /********************************************************/
                    #region Doc. Por Cobrar
                    EDocXCobrar obj = new EDocXCobrar();
                    obj.mesec_iid_mes = Convert.ToInt16(oBe.lexcc_sfecha_letra.Month);
                    obj.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                    obj.tdodc_iid_correlativo = Parametros.intClaseTipoDocLetraCliente;
                    obj.doxcc_vnumero_doc = oBe.lexcc_vnumero_letra;
                    obj.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    obj.cliec_vnombre_cliente = oBe.strDesCliente;
                    obj.doxcc_sfecha_doc = oBe.lexcc_sfecha_letra;
                    obj.doxcc_sfecha_vencimiento_doc = oBe.lexcc_sfecha_vencimiento;
                    obj.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj.doxcc_nmonto_tipo_cambio = oBe.lexcc_nmonto_tipo_cambio;
                    obj.tablc_iid_tipo_pago = 3;
                    obj.doxcc_vdescrip_transaccion = oBe.lexcc_vobservaciones;
                    obj.doxcc_nmonto_afecto = 0;
                    obj.doxcc_nmonto_inafecto = oBe.lexcc_nmonto_total;
                    obj.doxcc_nporcentaje_igv = 0;
                    obj.doxcc_nmonto_impuesto = 0;
                    obj.doxcc_nmonto_total = oBe.lexcc_nmonto_total;
                    obj.doxcc_nmonto_saldo = oBe.lexcc_nmonto_total;
                    obj.doxcc_nmonto_pagado = 0;
                    obj.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj.doxcc_vobservaciones = oBe.lexcc_vobservaciones;
                    obj.doxc_bind_cuenta_corriente = false;
                    obj.doxcc_sfecha_entrega = null;
                    obj.doxcc_bind_impresion_nogerencia = false;
                    obj.doxc_bind_situacion_legal = false;
                    obj.doxc_bind_cierre_cuenta_corriente = false;
                    obj.intUsuario = oBe.intUsuario;
                    obj.strPc = oBe.strPc;
                    obj.doxcc_tipo_comprobante_referencia = 0;
                    obj.doxcc_num_serie_referencia = "";
                    obj.doxcc_num_comprobante_referencia = "";
                    obj.doxcc_sfecha_emision_referencia = null;
                    obj.docxc_icod_documento = oBe.lexcc_icod_correlativo;
                    obj.anio = oBe.anioc_iid_anio;
                    obj.doxcc_flag_estado = true;
                    obj.doxcc_origen = Parametros.origenLetraPorCobrar;
                    /**/
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    oBe.doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDxc(obj, Lista);
                    objDocXCobrarData.modificarLetraPorCobrar(oBe);
                    /**/
                    #endregion
                    /********************************************************/

                    lstDetalle.ForEach(x =>
                    {
                        x.lexcc_icod_correlativo = intIcod;
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            #region Pago de DXC
                            EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                            obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.doxcc_icod_correlativo);
                            obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                            obj_DXC_Pago.pdxcc_vnumero_doc = oBe.lexcc_vnumero_letra;
                            obj_DXC_Pago.pdxcc_sfecha_cobro = x.lxcpc_sfecha_pago;
                            obj_DXC_Pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                            obj_DXC_Pago.pdxcc_nmonto_cobro = x.lxcpc_nmonto_pago;
                            obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = x.lxcpc_nmonto_tipo_cambio;
                            obj_DXC_Pago.pdxcc_vobservacion = x.lxcpc_vconcepto;
                            //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                            obj_DXC_Pago.cliec_icod_cliente = oBe.cliec_icod_cliente;
                            //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                            //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                            //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                            //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                            obj_DXC_Pago.pdxcc_vorigen = Parametros.origenLetraPorCobrar;
                            obj_DXC_Pago.intUsuario = x.intUsuario;
                            obj_DXC_Pago.strPc = x.strPc;
                            obj_DXC_Pago.pdxcc_flag_estado = true;
                            x.pdxcc_icod_correlativo = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                            #endregion
                        }
                        objDocXCobrarData.insertarLetraPorCobrarDet(x);

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

        public void modificarLetraPorCobrarUbiCon(ELetraPorCobrar oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.modificarLetraPorCobrar(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLetraPorCobrar(ELetraPorCobrar oBe, List<ELetraPorCobrarDet> lstDetalle, List<ELetraPorCobrarDet> lstDelete)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.modificarLetraPorCobrar(oBe);
                    /************************************************************************/
                    #region Doc. Por Cobrar
                    EDocXCobrar obj = new EDocXCobrar();
                    obj.mesec_iid_mes = Convert.ToInt16(oBe.lexcc_sfecha_letra.Month);
                    obj.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                    obj.tdodc_iid_correlativo = Parametros.intClaseTipoDocLetraCliente;
                    obj.doxcc_vnumero_doc = oBe.lexcc_vnumero_letra;
                    obj.cliec_icod_cliente = oBe.cliec_icod_cliente;
                    obj.cliec_vnombre_cliente = oBe.strDesCliente;
                    obj.doxcc_sfecha_doc = oBe.lexcc_sfecha_letra;
                    obj.doxcc_sfecha_vencimiento_doc = oBe.lexcc_sfecha_vencimiento;
                    obj.tablc_iid_tipo_moneda = oBe.tablc_iid_tipo_moneda;
                    obj.doxcc_nmonto_tipo_cambio = oBe.lexcc_nmonto_tipo_cambio;
                    obj.tablc_iid_tipo_pago = 3;
                    obj.doxcc_vdescrip_transaccion = oBe.lexcc_vobservaciones;
                    obj.doxcc_nmonto_afecto = 0;
                    obj.doxcc_nmonto_inafecto = oBe.lexcc_nmonto_total;
                    obj.doxcc_nporcentaje_igv = 0;
                    obj.doxcc_nmonto_impuesto = 0;
                    obj.doxcc_nmonto_total = oBe.lexcc_nmonto_total;
                    obj.doxcc_nmonto_saldo = oBe.lexcc_nmonto_total;
                    obj.doxcc_nmonto_pagado = 0;
                    obj.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                    obj.doxcc_vobservaciones = oBe.lexcc_vobservaciones;
                    obj.doxc_bind_cuenta_corriente = false;
                    obj.doxcc_sfecha_entrega = null;
                    obj.doxcc_bind_impresion_nogerencia = false;

                    obj.intUsuario = oBe.intUsuario;
                    obj.strPc = oBe.strPc;
                    obj.doxcc_tipo_comprobante_referencia = 0;
                    obj.doxcc_num_serie_referencia = "";
                    obj.doxcc_num_comprobante_referencia = "";
                    obj.doxcc_sfecha_emision_referencia = null;
                    obj.docxc_icod_documento = oBe.lexcc_icod_correlativo;
                    obj.anio = oBe.anioc_iid_anio;
                    obj.doxcc_flag_estado = true;
                    obj.doxcc_origen = Parametros.origenLetraPorCobrar;

                    /**/
                    List<EDocXCobrarCuentaContable> Lista = new List<EDocXCobrarCuentaContable>();
                    new CuentasPorCobrarData().modificarDxc(obj, Lista, Lista);
                    /**/
                    #endregion
                    /************************************************************************/
                    lstDelete.ForEach(x =>
                    {
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                            objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt64(x.pdxcc_icod_correlativo);
                            objE_DocPorCobrarPago.doxcc_icod_correlativo = Convert.ToInt64(x.doxcc_icod_correlativo);
                            objE_DocPorCobrarPago.intUsuario = x.intUsuario;
                            objE_DocPorCobrarPago.strPc = x.strPc;
                            new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objE_DocPorCobrarPago.doxcc_icod_correlativo, 1/*el tipo de moneda no es relevante*/);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objDocXCobrarData.eliminarLetraPorCobrarDet(x);

                    });
                    /************************************************************************/
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                            {
                                #region Pago de DXC
                                EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                                obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.doxcc_icod_correlativo);
                                obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                                obj_DXC_Pago.pdxcc_vnumero_doc = oBe.lexcc_vnumero_letra;
                                obj_DXC_Pago.pdxcc_sfecha_cobro = x.lxcpc_sfecha_pago;
                                obj_DXC_Pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                                obj_DXC_Pago.pdxcc_nmonto_cobro = x.lxcpc_nmonto_pago;
                                obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = x.lxcpc_nmonto_tipo_cambio;
                                obj_DXC_Pago.pdxcc_vobservacion = x.lxcpc_vconcepto;
                                //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                obj_DXC_Pago.cliec_icod_cliente = oBe.cliec_icod_cliente;
                                //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                                //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                                obj_DXC_Pago.pdxcc_vorigen = Parametros.origenLetraPorCobrar;
                                obj_DXC_Pago.intUsuario = x.intUsuario;
                                obj_DXC_Pago.strPc = x.strPc;
                                obj_DXC_Pago.pdxcc_flag_estado = true;
                                x.pdxcc_icod_correlativo = objDocXCobrarData.InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                                new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                                #endregion
                            }
                            x.lexcc_icod_correlativo = oBe.lexcc_icod_correlativo;
                            objDocXCobrarData.insertarLetraPorCobrarDet(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                            {
                                #region Pago de DXC
                                EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                                obj_DXC_Pago.pdxcc_icod_correlativo = Convert.ToInt64(x.pdxcc_icod_correlativo);
                                obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(x.doxcc_icod_correlativo);
                                obj_DXC_Pago.tdocc_icod_tipo_doc = Parametros.intTipoDocLetraCliente;
                                obj_DXC_Pago.pdxcc_vnumero_doc = oBe.lexcc_vnumero_letra;
                                obj_DXC_Pago.pdxcc_sfecha_cobro = x.lxcpc_sfecha_pago;
                                obj_DXC_Pago.tablc_iid_tipo_moneda = x.tablc_iid_tipo_moneda;
                                obj_DXC_Pago.pdxcc_nmonto_cobro = x.lxcpc_nmonto_pago;
                                obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = x.lxcpc_nmonto_tipo_cambio;
                                obj_DXC_Pago.pdxcc_vobservacion = x.lxcpc_vconcepto;
                                //obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                obj_DXC_Pago.cliec_icod_cliente = oBe.cliec_icod_cliente;
                                //obj_DXC_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                //obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                //obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                                //obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                                obj_DXC_Pago.pdxcc_vorigen = Parametros.origenLetraPorCobrar;
                                obj_DXC_Pago.intUsuario = x.intUsuario;
                                obj_DXC_Pago.strPc = x.strPc;
                                obj_DXC_Pago.pdxcc_flag_estado = true;
                                objDocXCobrarData.ActualizarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);
                                new TesoreriaData().ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                                #endregion
                            }
                            x.intUsuario = oBe.intUsuario;
                            x.strPc = oBe.strPc;
                            objDocXCobrarData.modificarLetraPorCobrarDet(x);
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
        public void eliminarLetraPorCobrar(ELetraPorCobrar oBe)
        {

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.eliminarLetraPorCobrar(oBe);
                    /*******************************************************/
                    EDocXCobrar oBeDXC = new EDocXCobrar();
                    oBeDXC.doxcc_icod_correlativo = oBe.doxcc_icod_correlativo;
                    oBeDXC.intUsuario = oBe.intUsuario;
                    oBeDXC.strPc = oBe.strPc;
                    objDocXCobrarData.eliminarDxc(oBeDXC);
                    /*******************************************************/
                    var lst = objDocXCobrarData.listarLetraPorCobrarDet(oBe.lexcc_icod_correlativo);
                    lst.ForEach(x =>
                    {
                        if (Convert.ToInt32(x.tdocc_icod_tipo_doc) > 0)
                        {
                            EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                            objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt64(x.pdxcc_icod_correlativo);
                            objE_DocPorCobrarPago.doxcc_icod_correlativo = Convert.ToInt64(x.doxcc_icod_correlativo);
                            objE_DocPorCobrarPago.intUsuario = x.intUsuario;
                            objE_DocPorCobrarPago.strPc = x.strPc;
                            new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);
                            new TesoreriaData().ActualizarMontoDXCPagadoSaldo(objE_DocPorCobrarPago.doxcc_icod_correlativo, 1/*el tipo de moneda no es relevante*/);
                        }
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objDocXCobrarData.eliminarLetraPorCobrarDet(x);
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

        public List<ELetraPorCobrarDet> listarLetraPorCobrarDet(int intLXC)
        {
            List<ELetraPorCobrarDet> Lista = new List<ELetraPorCobrarDet>();
            try
            {
                Lista = objDocXCobrarData.listarLetraPorCobrarDet(intLXC);
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        public EAnioEjercicio VerificarExistenciaAnoSiguiente(int anioc_inumero_anho)
        {
            EAnioEjercicio obj = null;
            try
            {
                obj = objDocXCobrarData.VerificarExistenciaAnoSiguiente(anioc_inumero_anho);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }
        public void CierreDocumentoXCobrar(int anioc_inumero_anho, int iusuario)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.CierreDocumentoXCobrar(anioc_inumero_anho, iusuario);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new CuentasPorCobrarData().ActualizarTipoCambio(opcion);
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
