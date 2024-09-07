using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BTesoreria
    {
        TesoreriaData objTesoreriaData = new TesoreriaData();
        CuentasPorPagarData objDocumentoPorPagarData = new CuentasPorPagarData();
        CuentasPorCobrarData objDocumentoPorCobrarData = new CuentasPorCobrarData();
        #region Bancos
        public List<EBanco> listarBancos()
        {
            List<EBanco> lista = null;
            try
            {
                lista = (new TesoreriaData()).listarBancos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int getBancoCuenta(int intMovimientoBanco)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new TesoreriaData().getBancoCuenta(intMovimientoBanco);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ActualizarMontoDXPPagadoSaldo(int IcodDXP, int Moneda)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().ActualizarMontoDXPPagadoSaldo(IcodDXP, Moneda);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarBanco(EBanco oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new TesoreriaData().insertarBanco(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBanco(EBanco oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().modificarBanco(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBanco(EBanco oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().eliminarBanco(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Cuentas
        public List<EBancoCuenta> listarBancoCuentas(int? bcoc_icod_banco)
        {
            List<EBancoCuenta> lista = null;
            try
            {
                lista = new TesoreriaData().listarBancoCuentas(bcoc_icod_banco);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarBancoCuenta(EBancoCuenta oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    EAnaliticaDetalle Obe = new EAnaliticaDetalle();
                    Obe.anad_iid_analitica = oBe.strCodAnalitica;
                    Obe.anad_vdescripcion = String.Format("{0}-{1}", oBe.strBanco, oBe.bcod_vnumero_cuenta);
                    Obe.anad_situacion = true;
                    Obe.tarec_icorrelativo_tipo_analitica = 1;//Tipo de Analitica BANCOS
                    Obe.anad_origen = 2;
                    /**/
                    oBe.anad_icod_analitica = new BContabilidad().insertarAnaliticaDetalle(Obe);
                    intIcod = new TesoreriaData().insertarBancoCuenta(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarBancoCuenta(EBancoCuenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    EAnaliticaDetalle Obe = new EAnaliticaDetalle();
                    Obe.anad_iid_analitica = oBe.strCodAnalitica;
                    Obe.anad_vdescripcion = String.Format("{0}-{1}", oBe.strBanco, oBe.bcod_vnumero_cuenta);
                    Obe.anad_situacion = true;
                    Obe.tarec_icorrelativo_tipo_analitica = 1;//Tipo de Analitica BANCOS
                    Obe.anad_origen = 2;
                    /**/
                    new BContabilidad().modificarAnaliticaDetalle(Obe);
                    /**/
                    new TesoreriaData().modificarBancoCuenta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarBancoCuenta(EBancoCuenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().eliminarBancoCuenta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool verificarBancoCuentaMovimientos(int intCtaBancaria)
        {
            bool flag = false;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    flag = new TesoreriaData().verificarBancoCuentaMovimientos(intCtaBancaria);
                    tx.Complete();
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarSaldoInicialBancoCuenta(EBancoCuenta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().ActualizarSaldoInicialBancoCuenta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EBancoCuenta> listarSaldoInicialBancoCuenta(int intEjercicio)
        {
            List<EBancoCuenta> lista = null;
            try
            {
                lista = new TesoreriaData().listarSaldoInicialBancoCuenta(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Bancos Movimientos
        public List<ELibroBancos> ListarMovimientoCuentasMovimientos(DateTime FechaI, DateTime FechaF, int Periodo, int icod_cuenta)
        {
            List<ELibroBancos> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarMovimientoCuentasMovimientos(FechaI, FechaF, Periodo, icod_cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ELibroBancos> ListarMovimientoCuentasDetalle(DateTime FechaI, DateTime FechaF, int Periodo)
        {
            List<ELibroBancos> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarMovimientoCuentasDetalle(FechaI, FechaF, Periodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EEntidadFinancieraCuenta> ListarResumenMovimientoCuentas(DateTime FechaInicio, DateTime FechaFinal, int Periodo)
        {
            List<EEntidadFinancieraCuenta> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarResumenMovimientoCuentas(FechaInicio, FechaFinal, Periodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarMovimientoCuentasCheques(int Periodo, int IdCuenta)
        {
            List<ELibroBancos> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarMovimientoCuentasCheques(Periodo, IdCuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarEstadoCuenta(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarEstadoCuenta(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> listarMovimientosSinConciliar(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = null;
            try
            {
                lista = (new TesoreriaData()).listarMovimientosSinConciliar(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarLibroBancos(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                lista = (new TesoreriaData()).ListarLibroBancos(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarLibroBancosSaldoAnterior(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                lista = (new TesoreriaData()).ListarLibroBancosSaldoAnterior(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarLibroBancosSaldoDisponible(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                lista = (new TesoreriaData()).ListarLibroBancosSaldoDisponible(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ELibroBancos> ListarLibroBancosCompras(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                lista = (new TesoreriaData()).ListarLibroBancosCompras(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarLibroBancosSaldoAnteriorCompras(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                lista = (new TesoreriaData()).ListarLibroBancosSaldoAnteriorCompras(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancos> ListarLibroBancosSaldoDisponibleCompras(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                lista = (new TesoreriaData()).ListarLibroBancosSaldoDisponibleCompras(Periodo, Mes, IdCuentaBancaria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public List<ELibroBancosDetalle> ListarEntidadFinancieraDetalle(int Code)
        {
            List<ELibroBancosDetalle> lista = new List<ELibroBancosDetalle>();
            try
            {
                lista = (new TesoreriaData()).ListarEntidadFinancieraDetalle(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancosDetalle> ListarEntidadFinancieraDetalleADPNCP_Cliente(int Code)
        {
            List<ELibroBancosDetalle> lista = new List<ELibroBancosDetalle>();
            try
            {
                lista = (new TesoreriaData()).ListarEntidadFinancieraDetalleADPNCP_Cliente(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<ELibroBancosDetalle> ListarEntidadFinancieraDetalleADPNCP(int Code)
        {
            List<ELibroBancosDetalle> lista = new List<ELibroBancosDetalle>();
            try
            {
                lista = (new TesoreriaData()).ListarEntidadFinancieraDetalleADPNCP(Code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EBancoCuenta> listarTransferencia(int intMov)
        {
            List<EBancoCuenta> lista = null;
            try
            {
                lista = (new TesoreriaData()).listarTransferencia(intMov);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTransferencia(ELibroBancos oBe1, ELibroBancos oBe2)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    oBe1.iid_correlativo = objTesoreriaData.InsertarMovimientoBancos(oBe1);
                    oBe2.iid_correlativo = objTesoreriaData.InsertarMovimientoBancos(oBe2);
                    objTesoreriaData.transferenciaID(oBe1.iid_correlativo, oBe2.iid_correlativo);
                    objTesoreriaData.transferenciaID(oBe2.iid_correlativo, oBe1.iid_correlativo);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oBe1.iid_correlativo;
        }
        public void modificarTransferencia(ELibroBancos oBe1, ELibroBancos oBe2)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objTesoreriaData.ActualizarLibroBancos(oBe1);
                    objTesoreriaData.ActualizarLibroBancos(oBe2);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int insertarLibroBancos(ELibroBancos objLibroBancos, List<ELibroBancosDetalle> ListaLibroDetalle,
            EAdelantoProveedor objAdelantoProveedor, EDocPorPagar objDXP, EAdelantoCliente objAdelantoCliente,
            EDocXCobrar objDXC, List<EImagenCheque> lstImagenCheque)
        {
            int icod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int IdEntidadFinancieraMovimieto = objTesoreriaData.InsertarMovimientoBancos(objLibroBancos);
                    objLibroBancos.icod_correlativo = IdEntidadFinancieraMovimieto;
                    icod = IdEntidadFinancieraMovimieto;
                    if (lstImagenCheque != null)
                    {
                        if (lstImagenCheque.Count > 0)
                        {
                            lstImagenCheque.ForEach(x =>
                            {
                                x.mobac_icod_correlativo = IdEntidadFinancieraMovimieto;
                                new BAdministracionSistema().insertarImagenCheque(x);
                            });
                        }
                    }


                    #region Movimiento Varios
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoVarios)
                    {
                        //Insertar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            item.mobdc_iid_anio = objLibroBancos.iid_anio;
                            item.mobdc_iid_mes_periodo = objLibroBancos.iid_mes;
                            item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                            objTesoreriaData.InsertarLibroBancosDetalle(item);
                        }
                        //CrearVoucherContableBancosVarios(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Cuentas Por Pagar
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                    {
                        //Insertar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (Convert.ToInt32(item.iid_cuenta_contable) > 0)
                            {
                                item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                objTesoreriaData.InsertarLibroBancosDetalle(item);
                            }
                            else
                            {
                                //Insertar Documento Por Pagar Pago
                                EDocPorPagarPago obj_DXP_pago = new EDocPorPagarPago();
                                obj_DXP_pago.doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                obj_DXP_pago.tdocc_icod_tipo_doc = objLibroBancos.ii_tipo_doc; //tipo doc de la cabecera
                                obj_DXP_pago.pdxpc_vnumero_doc = objLibroBancos.vnro_documento;
                                obj_DXP_pago.pdxpc_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                obj_DXP_pago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                obj_DXP_pago.pdxpc_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                obj_DXP_pago.pdxpc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                obj_DXP_pago.pdxpc_vobservacion = objLibroBancos.vglosa;
                                obj_DXP_pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                obj_DXP_pago.pdxpc_vorigen = "B";
                                obj_DXP_pago.doxcc_icod_correlativo = null;
                                obj_DXP_pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                obj_DXP_pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                obj_DXP_pago.anac_icod_analitica = item.icod_analitica;
                                obj_DXP_pago.intUsuario = item.iusuario_crea;
                                obj_DXP_pago.strPc = item.vpc_crea;
                                obj_DXP_pago.pdxpc_mes = objLibroBancos.iid_mes;
                                obj_DXP_pago.pdxpc_flag_estado = true;
                                obj_DXP_pago.anio = Parametros.intEjercicio;
                                long IdDocumentoPorPagarPago = new CuentasPorPagarData().insertarDocPorPagarPago(obj_DXP_pago);

                                //Insertar Libro Bancos Detalle
                                item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                item.docxp_icod_pago = IdDocumentoPorPagarPago;
                                objTesoreriaData.InsertarLibroBancosDetalle(item);

                                //Actualizar Monto Pagado Saldo
                                objTesoreriaData.ActualizarMontoDXPPagadoSaldo(obj_DXP_pago.doxpc_icod_correlativo, obj_DXP_pago.tablc_iid_tipo_moneda);

                            }
                        }
                        // CrearVoucherContableBancosCtaPor_Pagar_Cobrar(objLibroBancos, ListaLibroDetalle, objLibroBancos.iid_motivo_mov_banco);
                    }
                    #endregion
                    #region Adenlanto a Proveedores
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoAdelantosProveedores)
                    {
                        //Insertamos en Documentos Por Pagar
                        long? doxpc_icod_correlativo = 0;
                        doxpc_icod_correlativo = new CuentasPorPagarData().insertarDocPorPagar(objDXP);

                        //Insertamos en Adelanto Proveedor
                        int IdAdelantoProveedor = 0;
                        objAdelantoProveedor.doxpc_icod_correlativo = doxpc_icod_correlativo;
                        objAdelantoProveedor.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                        IdAdelantoProveedor = objTesoreriaData.insertarAdelantoProveedor(objAdelantoProveedor);

                        //Actualizamos el correlativo Numeracion Documento
                        objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoProveedor);

                    }
                    #endregion
                    #region Adelanto a Cliente
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoAdelantosClientes)
                    {

                        //Insertamos en Documentos Por Cobrar
                        long? doxcc_icod_correlativo = 0;
                        doxcc_icod_correlativo = new CuentasPorCobrarData().insertarDocumentoXCobrar(objDXC);
                        if (doxcc_icod_correlativo == 0)
                        {
                            throw new Exception("El número del Documento por Cobrar ya fue utilizado, intente con un número de Adelanto superior");
                        }
                        //Insertamos en Adelanto Cliente
                        objAdelantoCliente.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                        objAdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(doxcc_icod_correlativo);
                        int IdAdelantoCliente = 0;
                        IdAdelantoCliente = objTesoreriaData.insertarAdelantoCliente(objAdelantoCliente);

                        //Actualizamos el correlativo Numeracion Documento
                        objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoCliente);
                        //CrearVoucherContableBancosAdelantoCliente(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Cuentas por Cobrar
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                    {
                        //Insertar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (Convert.ToInt32(item.iid_cuenta_contable) > 0)
                            {
                                item.mobdc_iid_anio = Parametros.intEjercicio;
                                item.mobdc_iid_mes_periodo = Convert.ToDateTime(item.doxcc_sfecha_doc).Month;
                                item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                objTesoreriaData.InsertarLibroBancosDetalle(item);
                            }
                            else
                            {
                                if (item.tdocc_icod_tipo_doc != 83)
                                {
                                    #region Dxc pago
                                    //Insertar Documento Por Cobrar Pago
                                    EDocXCobrarPago obj_DXC_Pago = new EDocXCobrarPago();
                                    obj_DXC_Pago.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo); //IdDocumentoPorCobrar
                                    obj_DXC_Pago.tdocc_icod_tipo_doc = objLibroBancos.ii_tipo_doc;//item.tdocc_icod_tipo_doc;
                                    obj_DXC_Pago.pdxcc_vnumero_doc = objLibroBancos.vnro_documento;
                                    obj_DXC_Pago.pdxcc_sfecha_cobro = objLibroBancos.dfecha_movimiento;
                                    obj_DXC_Pago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                    obj_DXC_Pago.pdxcc_nmonto_cobro = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_DXC_Pago.pdxcc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    obj_DXC_Pago.pdxcc_vobservacion = objLibroBancos.vglosa;
                                    obj_DXC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                    obj_DXC_Pago.cliec_icod_cliente = item.mobdc_icod_cliente;
                                    obj_DXC_Pago.ctacc_iid_cuenta_contable = Convert.ToInt32(item.iid_cuenta_contable);
                                    obj_DXC_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                    obj_DXC_Pago.anac_icod_analitica = item.tablc_icod_tipo_analitica;
                                    obj_DXC_Pago.anac_icod_analitica_det = item.icod_analitica;
                                    obj_DXC_Pago.pdxcc_vorigen = "B";
                                    obj_DXC_Pago.intUsuario = item.iusuario_crea;
                                    obj_DXC_Pago.strPc = item.vpc_crea;
                                    obj_DXC_Pago.pdxcc_flag_estado = true;
                                    obj_DXC_Pago.anio = Parametros.intEjercicio;
                                    long IdDocumentoPorCobrarPago = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(obj_DXC_Pago);

                                    item.docxc_icod_pago = IdDocumentoPorCobrarPago;

                                    //Actualizar Monto Pagado Saldo
                                    objTesoreriaData.ActualizarMontoDXCPagadoSaldo(obj_DXC_Pago.doxcc_icod_correlativo, obj_DXC_Pago.tablc_iid_tipo_moneda);
                                    #endregion
                                }
                                else
                                {

                                    #region GRABAR DOC X COBRAR
                                    //Datos Documento Por Cobrar
                                    EDocXCobrar obj_DXCx = new EDocXCobrar();
                                    obj_DXCx.doxcc_icod_correlativo = 0;
                                    obj_DXCx.doxcc_vnumero_doc = item.vnumero_doc;
                                    obj_DXCx.anio = Parametros.intEjercicio;
                                    obj_DXCx.mesec_iid_mes = Convert.ToInt16(Convert.ToDateTime(item.doxcc_sfecha_doc).Month);
                                    obj_DXCx.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                                    obj_DXCx.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                                    obj_DXCx.doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc);
                                    obj_DXCx.cliec_icod_cliente = Convert.ToInt32(item.mobdc_icod_cliente);
                                    //obj_DXC.cliec_vnombre_cliente = item.anac_vdescripcion;
                                    obj_DXCx.tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                                    obj_DXCx.tablc_iid_tipo_pago = 1;
                                    obj_DXCx.doxcc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    obj_DXCx.doxcc_vdescrip_transaccion = item.vglosa;
                                    obj_DXCx.doxcc_nmonto_afecto = 0;
                                    obj_DXCx.doxcc_nmonto_inafecto = 0;
                                    obj_DXCx.doxcc_nporcentaje_igv = 0;
                                    obj_DXCx.doxcc_nmonto_impuesto = 0;
                                    obj_DXCx.doxcc_nmonto_total = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_DXCx.doxcc_nmonto_saldo = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_DXCx.doxcc_nmonto_pagado = 0;
                                    obj_DXCx.doxcc_sfecha_vencimiento_doc = DateTime.Now;
                                    obj_DXCx.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                                    obj_DXCx.doxcc_vobservaciones = objLibroBancos.cliec_vnombre_cliente;
                                    obj_DXCx.doxc_bind_cuenta_corriente = false;
                                    obj_DXCx.doxcc_sfecha_entrega = null;
                                    obj_DXCx.doxcc_bind_impresion_nogerencia = false;
                                    obj_DXCx.doxc_bind_situacion_legal = false;
                                    obj_DXCx.doxc_bind_cierre_cuenta_corriente = false;
                                    obj_DXCx.intUsuario = item.iusuario_crea;
                                    obj_DXCx.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                                    obj_DXCx.doxcc_tipo_comprobante_referencia = 0;
                                    obj_DXCx.doxcc_num_serie_referencia = "";
                                    obj_DXCx.doxcc_num_comprobante_referencia = "";
                                    obj_DXCx.doxcc_sfecha_emision_referencia = null;
                                    //obj_DXC.docxc_icod_documento = IdAdelantoCliente;
                                    obj_DXCx.doxcc_flag_estado = true;
                                    obj_DXCx.doxcc_origen = "B";

                                    //Insertamos en Documentos Por Cobrar
                                    item.doxcc_icod_correlativo_ADC = new CuentasPorCobrarData().insertarDocumentoXCobrar(obj_DXCx);

                                    if (item.doxcc_icod_correlativo_ADC == 0)
                                    {
                                        throw new ArgumentException("El número del adelanto ya existe, intente con un número superior");
                                    }

                                    #region Adelanto Cliente
                                    //Proceso para guardar adelanto cliente
                                    //Datos Adelanto Cliente

                                    EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();
                                    objE_AdelantoCliente.icod_correlativo = 0;//tenemos que traer el ID del adelanto
                                    objE_AdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo_ADC);
                                    objE_AdelantoCliente.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                    objE_AdelantoCliente.icod_cliente = Convert.ToInt32(item.mobdc_icod_cliente);
                                    objE_AdelantoCliente.iid_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                                    objE_AdelantoCliente.vnumero_adelanto = item.vnumero_doc;
                                    objE_AdelantoCliente.vnumero_documento = item.vnumero_doc;
                                    objE_AdelantoCliente.sfecha_adelanto = Convert.ToDateTime(item.doxcc_sfecha_doc);
                                    objE_AdelantoCliente.iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                                    objE_AdelantoCliente.nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    objE_AdelantoCliente.nmonto_adelanto = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                    objE_AdelantoCliente.nmonto_pagado = 0;
                                    objE_AdelantoCliente.vobservacion = item.vglosa;
                                    objE_AdelantoCliente.nsituacion_adelanto_cliente = Parametros.intSitClienteGenerado;
                                    objE_AdelantoCliente.iusuario_crea = item.iusuario_crea;
                                    objE_AdelantoCliente.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                                    objE_AdelantoCliente.flag_estado = true;

                                    #endregion
                                    //Actualizamos el correlativo Numeracion Documento
                                    objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoCliente);

                                    //Insertamos en Adelanto Cliente
                                    objE_AdelantoCliente.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                    objE_AdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(Convert.ToInt64(item.doxcc_icod_correlativo_ADC));
                                    int IdAdelantoCliente = 0;
                                    IdAdelantoCliente = objTesoreriaData.insertarAdelantoCliente(objE_AdelantoCliente);

                                    ////Actualizamos el correlativo Numeracion Documento
                                    //objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoCliente);
                                    #endregion

                                    item.docxc_icod_pago = null;
                                }

                                //Insertar Libro Bancos Detalle
                                item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                item.mobdc_iid_anio = Parametros.intEjercicio;
                                item.mobdc_iid_mes_periodo = Convert.ToDateTime(item.doxcc_sfecha_doc).Month;
                                objTesoreriaData.InsertarLibroBancosDetalle(item);
                            }
                        }
                        //CrearVoucherContableBancosCtaPor_Pagar_Cobrar(objLibroBancos, ListaLibroDetalle, objLibroBancos.iid_motivo_mov_banco);
                    }
                    #endregion
                    #region Pago Adelantado a Clientes
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoClientes)
                    {
                        //Insertar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (item.iid_cuenta_contable > 0)
                            {
                                item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                objTesoreriaData.InsertarLibroBancosDetalle(item);
                            }
                            else
                            {
                                if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)
                                {
                                    //Insertar Adelanto Pago
                                    EAdelantoPago obj_ADC_Pago = new EAdelantoPago();
                                    obj_ADC_Pago.doxcc_icod_correlativo_pago = Convert.ToInt64(item.doxcc_icod_correlativo);
                                    obj_ADC_Pago.doxcc_icod_correlativo_adelanto = Convert.ToInt64(item.doxcc_icod_correlativo);/*Convert.ToInt64(item.docxc_icod_documento);*/
                                    obj_ADC_Pago.tdocc_icod_tipo_doc = objLibroBancos.ii_tipo_doc;/* item.tdocc_icod_tipo_doc;*/
                                    obj_ADC_Pago.tdocc_iid_correlativo_pago = 0;/* Convert.ToInt32(item.tdodc_iid_correlativo);*/
                                    obj_ADC_Pago.cliec_icod_cliente = Convert.ToInt32(objLibroBancos.cliec_icod_cliente);
                                    obj_ADC_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                                    obj_ADC_Pago.adpac_nmonto_pago = item.tablc_iid_tipo_moneda == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_ADC_Pago.adpac_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    obj_ADC_Pago.adpac_vdescripcion = objLibroBancos.vglosa;
                                    obj_ADC_Pago.adpac_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                    obj_ADC_Pago.adpac_iorigen = "B";
                                    obj_ADC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                    obj_ADC_Pago.adpac_iusuario_crea = item.iusuario_crea;
                                    obj_ADC_Pago.adpac_vpc_crea = item.vpc_crea;
                                    obj_ADC_Pago.adpac_flag_estado = true;
                                    obj_ADC_Pago.mobac_icod_correlativo = IdEntidadFinancieraMovimieto;
                                    long IdAdelantoPago = new CuentasPorCobrarData().insertarAdelantoPago(obj_ADC_Pago);

                                    //Insertar Libro Bancos Detalle
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    item.adclie_icod_pago = IdAdelantoPago;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);

                                    //Actualizar Monto Pagado Saldo
                                    objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(obj_ADC_Pago.doxcc_icod_correlativo_adelanto, obj_ADC_Pago.tablc_iid_tipo_moneda);
                                }

                                if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                                {
                                    //Insertar Nota Credito Pago
                                    ENotaCreditoPago obj_NCP_Pago = new ENotaCreditoPago();
                                    obj_NCP_Pago.doxcc_icod_correlativo_pago = Convert.ToInt64(item.doxcc_icod_correlativo);
                                    obj_NCP_Pago.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(item.doxcc_icod_correlativo);/* Convert.ToInt64(item.docxc_icod_documento);*/
                                    obj_NCP_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(objLibroBancos.ii_tipo_doc);/* Convert.ToInt32(item.tdocc_icod_tipo_doc);*/
                                    //obj_NCP_Pago.tdocc_iid_correlativo = 0;/*item.tdodc_iid_correlativo;*/
                                    //obj_NCP_Pago.ncpac_vnumero_doc_nota_credito = objLibroBancos.vnro_documento;
                                    obj_NCP_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                                    obj_NCP_Pago.ncpac_nmonto_pago = item.tablc_iid_tipo_moneda == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_NCP_Pago.ncpac_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    obj_NCP_Pago.ncpac_vdescripcion = objLibroBancos.vglosa;
                                    obj_NCP_Pago.ncpac_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                    obj_NCP_Pago.ncpac_iorigen = "B";
                                    obj_NCP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                    obj_NCP_Pago.pdxcc_icod_correlativo = null;
                                    obj_NCP_Pago.ncpac_iusuario_crea = item.iusuario_crea;
                                    obj_NCP_Pago.ncpac_vpc_crea = item.vpc_crea;
                                    obj_NCP_Pago.ncpac_flag_estado = true;
                                    obj_NCP_Pago.mobac_icod_correlativo = IdEntidadFinancieraMovimieto;
                                    long IdNotaCreditoPago = new CuentasPorCobrarData().insertarNCPago(obj_NCP_Pago);

                                    //Insertar Libro Bancos Detalle
                                    item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                    item.ncclie_icod_pago = IdNotaCreditoPago;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);

                                    //Actualizar Nota Credito Pagado Saldo
                                    objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(obj_NCP_Pago.doxcc_icod_correlativo_nota_credito, obj_NCP_Pago.tablc_iid_tipo_moneda);
                                }
                            }
                        }
                        //CrearVoucherContableBancosPagoAdelNCCliente(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Pago Adelantado a Proveedores
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoProveedores)
                    {
                        //Insertar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (item.iid_cuenta_contable > 0)
                            {
                                item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                objTesoreriaData.InsertarLibroBancosDetalle(item);
                            }
                            else
                            {
                                if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor)
                                {
                                    //Insertar Doc x Pagar Adelanto Pago
                                    EDocxPagarPagoAdelanto obj_ADP_Pago = new EDocxPagarPagoAdelanto();
                                    obj_ADP_Pago.doxpc_icod_correlativo_pago = Convert.ToInt64(item.doxpc_icod_correlativo);
                                    obj_ADP_Pago.doxpc_icod_correlativo_adelanto = Convert.ToInt64(item.doxpc_icod_correlativo);/* Convert.ToInt64(item.doxpc_icod_documento);*/
                                    obj_ADP_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(objLibroBancos.ii_tipo_doc);/* Convert.ToInt32(item.tdocc_icod_tipo_doc);*/
                                    //obj_ADP_Pago.tdocc_iid_correlativo = 0;/* item.tdodc_iid_correlativo;*/
                                    //obj_ADP_Pago.adpap_vnumero_doc_adelanto = objLibroBancos.vnro_documento;
                                    obj_ADP_Pago.id_tipo_moneda_adelanto = item.tablc_iid_tipo_moneda;
                                    obj_ADP_Pago.adpap_nmonto_pago = (item.tablc_iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_ADP_Pago.adpap_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    obj_ADP_Pago.adpap_vdescripcion = objLibroBancos.vglosa;
                                    obj_ADP_Pago.adpap_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                    obj_ADP_Pago.adpap_iorigen = "B";
                                    obj_ADP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;

                                    obj_ADP_Pago.pdxpc_icod_correlativo = null;
                                    obj_ADP_Pago.adpap_iusuario_crea = item.iusuario_crea;
                                    obj_ADP_Pago.adpap_vpc_crea = item.vpc_crea;
                                    obj_ADP_Pago.adpap_flag_estado = true;
                                    obj_ADP_Pago.mobac_icod_correlativo = IdEntidadFinancieraMovimieto;
                                    obj_ADP_Pago.anio = Parametros.intEjercicio;
                                    long Id_ADP_Pago = new CuentasPorPagarData().insertarAdelantoPago(obj_ADP_Pago);

                                    //Insertar Libro Bancos Detalle
                                    item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                    item.adprov_icod_pago = Id_ADP_Pago;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);

                                    //Actualizar Monto Pagado Saldo
                                    objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(obj_ADP_Pago.doxpc_icod_correlativo_adelanto, item.tablc_iid_tipo_moneda);

                                }

                                if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                                {
                                    //Insertar Doc x Pagar Nota Credito Pago
                                    EDocPorPagarNotaCredito obj_NCP_Pago = new EDocPorPagarNotaCredito();
                                    obj_NCP_Pago.doxpc_icod_correlativo_pago = Convert.ToInt64(item.doxpc_icod_correlativo);
                                    obj_NCP_Pago.doxpc_icod_correlativo_nota_credito = Convert.ToInt64(item.doxpc_icod_correlativo); ;/* Convert.ToInt64(item.doxpc_icod_documento);*/
                                    obj_NCP_Pago.tipo_documento_nota_credito = Convert.ToInt32(objLibroBancos.ii_tipo_doc);/* Convert.ToInt32(item.tdocc_icod_tipo_doc);*/
                                    obj_NCP_Pago.idd_correlativo_nota_credito = 0;/* item.tdodc_iid_correlativo;*/
                                    //obj_NCP_Pago.ncpap_vnumero_doc_nota_credito = objLibroBancos.vnro_documento;
                                    obj_NCP_Pago.iid_moneda_nota_credito = objLibroBancos.iid_tipo_moneda;
                                    obj_NCP_Pago.ncpap_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                    obj_NCP_Pago.ncpap_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    obj_NCP_Pago.ncpap_vdescripcion = objLibroBancos.vglosa;
                                    obj_NCP_Pago.ncpap_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                    obj_NCP_Pago.ncpap_iorigen = "B";
                                    obj_NCP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                    //obj_NCP_Pago.ncpap_isituacion = Parametros.intSitDocCobrarGenerado;
                                    obj_NCP_Pago.pdxpc_icod_correlativo = null;
                                    obj_NCP_Pago.ncpac_iusuario_crea = item.iusuario_crea;
                                    obj_NCP_Pago.ncpac_vpc_crea = item.vpc_crea;
                                    obj_NCP_Pago.ncpap_flag_estado = true;
                                    obj_NCP_Pago.anio = Parametros.intEjercicio;
                                    long Id_NCP_Pago = new CuentasPorPagarData().insertarNotaCreditoPago(obj_NCP_Pago);

                                    //Insertar Libro Bancos Detalle
                                    item.icod_correlativo_cabecera = IdEntidadFinancieraMovimieto;
                                    item.ncprov_icod_pago = Id_NCP_Pago;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);

                                    ////Actualizar Nota Credito Pagado Saldo
                                    objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoProveedor(obj_NCP_Pago.doxpc_icod_correlativo_nota_credito, obj_NCP_Pago.iid_moneda_nota_credito);

                                }
                            }
                        }
                        //CrearVoucherContableBancosPagoAdelNCProveedor(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Transferencia
                    //intMotivoTransferencia
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return icod;
        }
        public void actualizarLibroBancos(ELibroBancos objLibroBancos, List<ELibroBancosDetalle> ListaLibroDetalle,
            EAdelantoProveedor objAdelantoProveedor, EDocPorPagar objDocumentoPorPagar,
            EAdelantoCliente objAdelantoCliente, EDocXCobrar objDocumentoPorCobrar,
            List<ELibroBancosDetalle> objDetListDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objTesoreriaData.ActualizarLibroBancos(objLibroBancos);
                    #region Movimiento Varios
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoVarios)
                    {
                        //Actualizar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in objDetListDelete)
                        {
                            objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                        }
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            item.mobdc_iid_anio = objLibroBancos.iid_anio;
                            item.mobdc_iid_mes_periodo = objLibroBancos.iid_mes;
                            if (item.TipOper == 1)
                            {
                                objTesoreriaData.InsertarLibroBancosDetalle(item);
                            }
                            if (item.TipOper == 2)
                            {

                                item.vpc_modifica = objLibroBancos.vpc_modifica;
                                objTesoreriaData.ActualizarLibroBancosDetalle(item);
                            }
                        }
                        //CrearVoucherContableBancosVarios(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Cuentas por Pagar
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                    {
                        #region 1.- ELIMINAR LO QUE ESTA EN LA LISTA DE ELIMINACIÓN

                        foreach (ELibroBancosDetalle item in objDetListDelete)
                        {
                            if (item.icod_correlativo != 0)
                            {
                                if (Convert.ToInt64(item.docxp_icod_pago) > 0)
                                {
                                    EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                                    oBeDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_pago);
                                    oBeDXPPago.intUsuario = objLibroBancos.iusuario_modifica;
                                    oBeDXPPago.strPc = objLibroBancos.vpc_modifica;
                                    new CuentasPorPagarData().eliminarDocPorPagarPago(oBeDXPPago);
                                    objTesoreriaData.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(item.doxpc_icod_correlativo), item.tablc_iid_tipo_moneda);
                                }
                                objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                            }
                        }
                        #endregion
                        #region 2.- ACTUALIZAR LOS DATOS DEL DETALLE
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            #region 2.1.- INGRESANDO NUEVOS REGISTROS
                            if (item.TipOper == 1)
                            {
                                int Icod_Libro_Detalle = 0;
                                int Icod_Retencion_Cab = 0;

                                if (item.iid_cuenta_contable > 0)
                                {
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    //Insertar Documento Por Pagar Pago
                                    EDocPorPagarPago objDXP_Pago = new EDocPorPagarPago();
                                    objDXP_Pago.doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                    objDXP_Pago.tdocc_icod_tipo_doc = objLibroBancos.ii_tipo_doc; //tipo doc de la cabecera
                                    objDXP_Pago.pdxpc_vnumero_doc = objLibroBancos.vnro_documento;
                                    objDXP_Pago.pdxpc_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                    objDXP_Pago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                    objDXP_Pago.pdxpc_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                    objDXP_Pago.pdxpc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    objDXP_Pago.pdxpc_vobservacion = objLibroBancos.vglosa;
                                    objDXP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                    objDXP_Pago.pdxpc_vorigen = "B";
                                    objDXP_Pago.doxcc_icod_correlativo = null;
                                    objDXP_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                    objDXP_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                    objDXP_Pago.anac_icod_analitica = item.icod_analitica;
                                    objDXP_Pago.intUsuario = item.iusuario_crea;
                                    objDXP_Pago.strPc = item.vpc_crea;
                                    objDXP_Pago.pdxpc_mes = objLibroBancos.iid_mes;
                                    objDXP_Pago.pdxpc_flag_estado = true;
                                    objDXP_Pago.anio = Parametros.intEjercicio;
                                    long IdDocumentoPorPagarPago = new CuentasPorPagarData().insertarDocPorPagarPago(objDXP_Pago);

                                    //Insertar Libro Bancos Detalle
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    item.docxp_icod_pago = IdDocumentoPorPagarPago;
                                    Icod_Libro_Detalle = objTesoreriaData.InsertarLibroBancosDetalle(item);
                                    item.icod_correlativo = Icod_Libro_Detalle;

                                    //Actualizar Monto Pagado Saldo
                                    objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objDXP_Pago.doxpc_icod_correlativo, objDXP_Pago.tablc_iid_tipo_moneda);
                                }

                            }
                            #endregion
                            #region 2.2.- ACTUAZALIZANDO REGISTROS
                            if (item.TipOper == 2)
                            {
                                if (Convert.ToInt32(item.iid_cuenta_contable) > 0)
                                {
                                    objTesoreriaData.ActualizarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    //Actualizar Documento Por Pagar Pago
                                    EDocPorPagarPago objDXP_Pago = new EDocPorPagarPago();//
                                    //objDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(item.tdodc_iid_correlativo);
                                    objDXP_Pago.pdxpc_icod_correlativo = Convert.ToInt32(item.docxp_icod_pago);
                                    //objDocumentoPorPagarPago.pdxpc_icod_correlativo = Convert.ToInt64(item.pdxpc_icod_correlativo);
                                    objDXP_Pago.doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo); //IdDocumentoPorPagar
                                    objDXP_Pago.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                                    objDXP_Pago.pdxpc_vnumero_doc = objLibroBancos.vnro_documento;
                                    objDXP_Pago.pdxpc_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                    objDXP_Pago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                    objDXP_Pago.pdxpc_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                    objDXP_Pago.pdxpc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                    objDXP_Pago.pdxpc_vobservacion = objLibroBancos.vglosa;
                                    objDXP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                    objDXP_Pago.pdxpc_vorigen = "B";
                                    objDXP_Pago.doxcc_icod_correlativo = null;
                                    objDXP_Pago.ctacc_iid_cuenta_contable = item.iid_cuenta_contable;
                                    objDXP_Pago.cecoc_icod_centro_costo = item.icod_centro_costo;
                                    objDXP_Pago.anac_icod_analitica = item.icod_analitica;
                                    objDXP_Pago.intUsuario = item.iusuario_modifica;
                                    objDXP_Pago.strPc = item.vpc_modifica;
                                    objDXP_Pago.pdxpc_mes = objLibroBancos.iid_mes;
                                    objDXP_Pago.anio = Parametros.intEjercicio;
                                    objDXP_Pago.pdxpc_flag_estado = true;
                                    new CuentasPorPagarData().modificarDocPorPagarPago(objDXP_Pago);

                                    //Actualizar Libro Bancos Detalle
                                    objTesoreriaData.ActualizarLibroBancosDetalle(item);

                                    //Actualizar Monto Pagado Saldo
                                    objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objDXP_Pago.doxpc_icod_correlativo, objDXP_Pago.tablc_iid_tipo_moneda);

                                }
                            }
                            #endregion
                        }

                        #endregion

                    }
                    #endregion
                    #region Cuentas por Cobrar
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                    {
                        foreach (ELibroBancosDetalle item in objDetListDelete)
                        {
                            if (Convert.ToInt32(item.iid_cuenta_contable) == 0 && Convert.ToInt32(item.tdocc_icod_tipo_doc) != 83)
                            {
                                EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                                objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt64(item.docxc_icod_pago);
                                objE_DocPorCobrarPago.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo);
                                objE_DocPorCobrarPago.intUsuario = item.iusuario_modifica;
                                objE_DocPorCobrarPago.strPc = item.vpc_modifica;
                                new CuentasPorCobrarData().EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);
                                objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objE_DocPorCobrarPago.doxcc_icod_correlativo, 1/*el tipo de moneda no es relevante*/);
                            }
                            else
                            {
                                if (item.tdocc_icod_tipo_doc == 83)
                                {
                                    EDocXCobrar _beDXC = new EDocXCobrar();
                                    _beDXC.doxcc_icod_correlativo = item.doxcc_icod_correlativo_ADC;
                                    _beDXC.intUsuario = item.iusuario_crea;
                                    _beDXC.strPc = item.vpc_crea;
                                    new CuentasPorCobrarData().EliminarDocumentoXCobrar(_beDXC);

                                    EAdelantoCliente _BEADC = new EAdelantoCliente();
                                    _BEADC.icod_correlativo = item.adci_icod_correlativo;
                                    _BEADC.iusuario_modifica = item.iusuario_crea;
                                    _BEADC.vpc_modifica = item.vpc_crea;
                                    objTesoreriaData.eliminarAdelantoCliente(_BEADC);
                                }
                            }
                            objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);


                        }
                        //Actualizar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (item.TipOper == 1)//NUEVO
                            {
                                if (Convert.ToInt32(item.iid_cuenta_contable) > 0)
                                {
                                    item.mobdc_iid_anio = Parametros.intEjercicio;
                                    item.mobdc_iid_mes_periodo = Convert.ToDateTime(objLibroBancos.dfecha_movimiento).Month;
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    if (item.tdocc_icod_tipo_doc != 83)
                                    {
                                        #region Pago
                                        //Insertar Documento Por Cobrar Pago
                                        EDocXCobrarPago objDocPorCobrarPago = new EDocXCobrarPago();
                                        objDocPorCobrarPago.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo); //IdDocumentoPorCobrar
                                        objDocPorCobrarPago.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                                        objDocPorCobrarPago.pdxcc_vnumero_doc = objLibroBancos.vnro_documento;
                                        objDocPorCobrarPago.pdxcc_sfecha_cobro = objLibroBancos.dfecha_movimiento;
                                        objDocPorCobrarPago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                        objDocPorCobrarPago.pdxcc_nmonto_cobro = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                        objDocPorCobrarPago.pdxcc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        objDocPorCobrarPago.pdxcc_vobservacion = objLibroBancos.vglosa;
                                        objDocPorCobrarPago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        objDocPorCobrarPago.cliec_icod_cliente = objLibroBancos.cliec_icod_cliente;

                                        objDocPorCobrarPago.ctacc_iid_cuenta_contable = null;
                                        objDocPorCobrarPago.cecoc_icod_centro_costo = null;
                                        objDocPorCobrarPago.anac_icod_analitica = null;
                                        objDocPorCobrarPago.pdxcc_vorigen = "B";
                                        objDocPorCobrarPago.intUsuario = item.iusuario_crea;
                                        objDocPorCobrarPago.strPc = item.vpc_crea;
                                        objDocPorCobrarPago.pdxcc_flag_estado = true;
                                        objDocPorCobrarPago.anio = Parametros.intEjercicio;
                                        long IdDocumentoPorCobrarPago = new CuentasPorCobrarData().InsertarPagoDirectoDocumentoXCobrar(objDocPorCobrarPago);
                                        item.docxc_icod_pago = IdDocumentoPorCobrarPago;

                                        //Actualizar Monto Pagado Saldo
                                        objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objDocPorCobrarPago.doxcc_icod_correlativo, objDocPorCobrarPago.tablc_iid_tipo_moneda);
                                        #endregion
                                    }
                                    else
                                    {
                                        #region GRABAR DOC X COBRAR
                                        //Datos Documento Por Cobrar
                                        EDocXCobrar obj_DXCx = new EDocXCobrar();
                                        obj_DXCx.doxcc_icod_correlativo = 0;
                                        obj_DXCx.doxcc_vnumero_doc = item.vnumero_doc;
                                        obj_DXCx.anio = Parametros.intEjercicio;
                                        obj_DXCx.mesec_iid_mes = Convert.ToInt16(Convert.ToDateTime(item.doxcc_sfecha_doc).Month);
                                        obj_DXCx.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                                        obj_DXCx.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                                        obj_DXCx.doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc);
                                        obj_DXCx.cliec_icod_cliente = Convert.ToInt32(item.mobdc_icod_cliente);
                                        //obj_DXC.cliec_vnombre_cliente = item.anac_vdescripcion;
                                        obj_DXCx.tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                                        obj_DXCx.tablc_iid_tipo_pago = 1;
                                        obj_DXCx.doxcc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_DXCx.doxcc_vdescrip_transaccion = item.vglosa;
                                        obj_DXCx.doxcc_nmonto_afecto = 0;
                                        obj_DXCx.doxcc_nmonto_inafecto = 0;
                                        obj_DXCx.doxcc_nporcentaje_igv = 0;
                                        obj_DXCx.doxcc_nmonto_impuesto = 0;
                                        obj_DXCx.doxcc_nmonto_total = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_DXCx.doxcc_nmonto_saldo = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_DXCx.doxcc_nmonto_pagado = 0;
                                        obj_DXCx.doxcc_sfecha_vencimiento_doc = DateTime.Now;
                                        obj_DXCx.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                                        obj_DXCx.doxcc_vobservaciones = objLibroBancos.cliec_vnombre_cliente;
                                        obj_DXCx.doxc_bind_cuenta_corriente = false;
                                        obj_DXCx.doxcc_sfecha_entrega = null;
                                        obj_DXCx.doxcc_bind_impresion_nogerencia = false;
                                        obj_DXCx.doxc_bind_situacion_legal = false;
                                        obj_DXCx.doxc_bind_cierre_cuenta_corriente = false;
                                        obj_DXCx.intUsuario = item.iusuario_crea;
                                        obj_DXCx.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                                        obj_DXCx.doxcc_tipo_comprobante_referencia = 0;
                                        obj_DXCx.doxcc_num_serie_referencia = "";
                                        obj_DXCx.doxcc_num_comprobante_referencia = "";
                                        obj_DXCx.doxcc_sfecha_emision_referencia = null;
                                        //obj_DXC.docxc_icod_documento = IdAdelantoCliente;
                                        obj_DXCx.doxcc_flag_estado = true;
                                        obj_DXCx.doxcc_origen = "B";

                                        //Insertamos en Documentos Por Cobrar
                                        item.doxcc_icod_correlativo_ADC = new CuentasPorCobrarData().insertarDocumentoXCobrar(obj_DXCx);


                                        #region Adelanto Cliente
                                        //Proceso para guardar adelanto cliente
                                        //Datos Adelanto Cliente

                                        EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();
                                        objE_AdelantoCliente.icod_correlativo = 0;//tenemos que traer el ID del adelanto
                                        objE_AdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo_ADC);
                                        objE_AdelantoCliente.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                        objE_AdelantoCliente.icod_cliente = Convert.ToInt32(item.mobdc_icod_cliente);
                                        objE_AdelantoCliente.iid_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                                        objE_AdelantoCliente.vnumero_adelanto = item.vnumero_doc;
                                        objE_AdelantoCliente.vnumero_documento = "";
                                        objE_AdelantoCliente.sfecha_adelanto = Convert.ToDateTime(item.doxcc_sfecha_doc);
                                        objE_AdelantoCliente.iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                                        objE_AdelantoCliente.nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        objE_AdelantoCliente.nmonto_adelanto = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        objE_AdelantoCliente.nmonto_pagado = 0;
                                        objE_AdelantoCliente.vobservacion = item.vglosa;
                                        objE_AdelantoCliente.nsituacion_adelanto_cliente = Parametros.intSitClienteGenerado;
                                        objE_AdelantoCliente.iusuario_crea = item.iusuario_crea;
                                        objE_AdelantoCliente.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                                        objE_AdelantoCliente.flag_estado = true;



                                        #endregion

                                        //Insertamos en Adelanto Cliente
                                        objE_AdelantoCliente.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                        objE_AdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(Convert.ToInt64(item.doxcc_icod_correlativo_ADC));

                                        int IdAdelantoCliente = 0;
                                        IdAdelantoCliente = objTesoreriaData.insertarAdelantoCliente(objE_AdelantoCliente);
                                        //Actualizamos el correlativo Numeracion Documento
                                        objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoCliente);

                                        ////Actualizamos el correlativo Numeracion Documento
                                        //objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoCliente);
                                        #endregion
                                        item.docxc_icod_pago = null;
                                    }
                                    //Insertar Libro Bancos Detalle
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    item.mobdc_iid_anio = Parametros.intEjercicio;
                                    item.mobdc_iid_mes_periodo = Convert.ToDateTime(item.doxcc_sfecha_doc).Month;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);
                                }

                            }
                            else
                            {
                                if (item.TipOper == 2)//POR MODIFICAR
                                {
                                    if (Convert.ToInt32(item.iid_cuenta_contable) > 0)
                                    {
                                        objTesoreriaData.ActualizarLibroBancosDetalle(item);
                                    }
                                    else
                                    {
                                        if (item.tdocc_icod_tipo_doc != 83)
                                        {
                                            #region actualizar Pago
                                            EDocXCobrarPago objDocPorCobrarPago = new EDocXCobrarPago();
                                            objDocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt64(item.docxc_icod_pago);
                                            objDocPorCobrarPago.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo); //IdDocumentoPorCobrar
                                            objDocPorCobrarPago.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                                            objDocPorCobrarPago.pdxcc_vnumero_doc = objLibroBancos.vnro_documento;
                                            objDocPorCobrarPago.pdxcc_sfecha_cobro = objLibroBancos.dfecha_movimiento;
                                            objDocPorCobrarPago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                            objDocPorCobrarPago.pdxcc_nmonto_cobro = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                            objDocPorCobrarPago.pdxcc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                            objDocPorCobrarPago.pdxcc_vobservacion = objLibroBancos.vglosa;
                                            objDocPorCobrarPago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                            objDocPorCobrarPago.cliec_icod_cliente = objLibroBancos.cliec_icod_cliente;
                                            objDocPorCobrarPago.ctacc_iid_cuenta_contable = null;
                                            objDocPorCobrarPago.cecoc_icod_centro_costo = null;
                                            objDocPorCobrarPago.anac_icod_analitica = null;
                                            objDocPorCobrarPago.pdxcc_vorigen = "B";
                                            objDocPorCobrarPago.intUsuario = item.iusuario_crea;
                                            objDocPorCobrarPago.strPc = item.vpc_crea;
                                            objDocPorCobrarPago.pdxcc_flag_estado = true;
                                            new CuentasPorCobrarData().ActualizarPagoDirectoDocumentoXCobrar(objDocPorCobrarPago);
                                            //Actualizar Monto Pagado Saldo
                                            objTesoreriaData.ActualizarMontoDXCPagadoSaldo(objDocPorCobrarPago.doxcc_icod_correlativo, objDocPorCobrarPago.tablc_iid_tipo_moneda);
                                            #endregion
                                        }
                                        else
                                        {
                                            #region GRABAR DOC X COBRAR
                                            //Datos Documento Por Cobrar
                                            EDocXCobrar obj_DXCx = new EDocXCobrar();
                                            obj_DXCx.doxcc_icod_correlativo = item.doxcc_icod_correlativo_ADC;
                                            obj_DXCx.doxcc_vnumero_doc = item.vnumero_doc;
                                            obj_DXCx.anio = Parametros.intEjercicio;
                                            obj_DXCx.mesec_iid_mes = Convert.ToInt16(Convert.ToDateTime(item.doxcc_sfecha_doc).Month);
                                            obj_DXCx.tdocc_icod_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                                            obj_DXCx.tdodc_iid_correlativo = Parametros.intClaseTipoDocAdelantoCliente;
                                            obj_DXCx.doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc);
                                            obj_DXCx.cliec_icod_cliente = Convert.ToInt32(item.mobdc_icod_cliente);
                                            //obj_DXC.cliec_vnombre_cliente = item.anac_vdescripcion;
                                            obj_DXCx.tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                                            obj_DXCx.tablc_iid_tipo_pago = 1;
                                            obj_DXCx.doxcc_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                            obj_DXCx.doxcc_vdescrip_transaccion = item.vglosa;
                                            obj_DXCx.doxcc_nmonto_afecto = 0;
                                            obj_DXCx.doxcc_nmonto_inafecto = 0;
                                            obj_DXCx.doxcc_nporcentaje_igv = 0;
                                            obj_DXCx.doxcc_nmonto_impuesto = 0;
                                            obj_DXCx.doxcc_nmonto_total = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                            obj_DXCx.doxcc_nmonto_saldo = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                            obj_DXCx.doxcc_nmonto_pagado = 0;
                                            obj_DXCx.doxcc_sfecha_vencimiento_doc = DateTime.Now;
                                            obj_DXCx.tablc_iid_situacion_documento = Parametros.intSitDocCobrarGenerado;
                                            obj_DXCx.doxcc_vobservaciones = objLibroBancos.cliec_vnombre_cliente;
                                            obj_DXCx.doxc_bind_cuenta_corriente = false;
                                            obj_DXCx.doxcc_sfecha_entrega = null;
                                            obj_DXCx.doxcc_bind_impresion_nogerencia = false;
                                            obj_DXCx.doxc_bind_situacion_legal = false;
                                            obj_DXCx.doxc_bind_cierre_cuenta_corriente = false;
                                            obj_DXCx.intUsuario = item.iusuario_crea;
                                            obj_DXCx.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                                            obj_DXCx.doxcc_tipo_comprobante_referencia = 0;
                                            obj_DXCx.doxcc_num_serie_referencia = "";
                                            obj_DXCx.doxcc_num_comprobante_referencia = "";
                                            obj_DXCx.doxcc_sfecha_emision_referencia = null;
                                            //obj_DXC.docxc_icod_documento = IdAdelantoCliente;
                                            obj_DXCx.doxcc_flag_estado = true;
                                            obj_DXCx.doxcc_origen = "B";

                                            //Insertamos en Documentos Por Cobrar
                                            new CuentasPorCobrarData().modificarDocumentoXCobrar(obj_DXCx);


                                            #region Adelanto Cliente
                                            //Proceso para guardar adelanto cliente
                                            //Datos Adelanto Cliente

                                            EAdelantoCliente objE_AdelantoCliente = new EAdelantoCliente();
                                            objE_AdelantoCliente.icod_correlativo = item.adci_icod_correlativo;//tenemos que traer el ID del adelanto
                                            objE_AdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo_ADC);
                                            objE_AdelantoCliente.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                            objE_AdelantoCliente.icod_cliente = Convert.ToInt32(item.mobdc_icod_cliente);
                                            objE_AdelantoCliente.iid_tipo_doc = Parametros.intTipoDocAdelantoCliente;
                                            objE_AdelantoCliente.vnumero_adelanto = item.vnumero_doc;
                                            objE_AdelantoCliente.vnumero_documento = "";
                                            objE_AdelantoCliente.sfecha_adelanto = Convert.ToDateTime(item.doxcc_sfecha_doc);
                                            objE_AdelantoCliente.iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda);
                                            objE_AdelantoCliente.nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                            objE_AdelantoCliente.nmonto_adelanto = Convert.ToInt32(item.tablc_iid_tipo_moneda) == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                            objE_AdelantoCliente.nmonto_pagado = 0;
                                            objE_AdelantoCliente.vobservacion = item.vglosa;
                                            objE_AdelantoCliente.nsituacion_adelanto_cliente = Parametros.intSitClienteGenerado;
                                            objE_AdelantoCliente.iusuario_crea = item.iusuario_crea;
                                            objE_AdelantoCliente.vpc_crea = WindowsIdentity.GetCurrent().Name.ToString();
                                            objE_AdelantoCliente.flag_estado = true;

                                            #endregion

                                            //Insertamos en Adelanto Cliente
                                            objE_AdelantoCliente.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                            objE_AdelantoCliente.doxcc_icod_correlativo = Convert.ToInt64(Convert.ToInt64(item.doxcc_icod_correlativo_ADC));
                                            objTesoreriaData.modificarAdelantoCliente(objE_AdelantoCliente);

                                            ////Actualizamos el correlativo Numeracion Documento
                                            //objTesoreriaData.ActualizarNumero(objLibroBancos.iid_anio, Parametros.intTipoDocAdelantoCliente);
                                            #endregion
                                        }
                                        //Actualizar Libro Bancos Detalle
                                        objTesoreriaData.ActualizarLibroBancosDetalle(item);


                                    }
                                }
                            }
                        }
                        //CrearVoucherContableBancosCtaPor_Pagar_Cobrar(objLibroBancos, ListaLibroDetalle, objLibroBancos.iid_motivo_mov_banco);
                    }
                    #endregion
                    #region Adelanto a Proveedores
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoAdelantosProveedores)
                    {
                        //Actualizar Adelanto Proveedor
                        objTesoreriaData.modificarAdelantoProveedor(objAdelantoProveedor);
                        //Actualizar Documento x Pagar
                        new CuentasPorPagarData().modificarDocPorPagar(objDocumentoPorPagar);
                    }
                    #endregion
                    #region Adelanto a Clientes
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoAdelantosClientes)
                    {
                        //Actualizar Adelanto Proveedor
                        objTesoreriaData.modificarAdelantoCliente(objAdelantoCliente);
                        //Actualizar Documento x Cobrar
                        new CuentasPorCobrarData().modificarDocumentoXCobrar(objDocumentoPorCobrar);

                        //CrearVoucherContableBancosAdelantoCliente(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Pago Adelantado a Clientes
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoClientes)
                    {
                        foreach (ELibroBancosDetalle item in objDetListDelete)
                        {
                            if (item.iid_cuenta_contable > 0)
                                objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                            else
                            {
                                item.iusuario_modifica = objLibroBancos.iusuario_modifica;
                                item.vpc_modifica = objLibroBancos.vpc_modifica;
                                new BCuentasPorCobrar().EliminarAdelantoPagoNotaCredito(item);
                                objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                            }
                        }
                        //Actualizar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (item.TipOper == 1)
                            {
                                if (item.iid_cuenta_contable > 0)
                                {
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)
                                    {
                                        //Insertar Adelanto Pago
                                        EAdelantoPago obj_ADC_Pago = new EAdelantoPago();
                                        obj_ADC_Pago.doxcc_icod_correlativo_pago = Convert.ToInt64(item.doxcc_icod_correlativo);
                                        obj_ADC_Pago.doxcc_icod_correlativo_adelanto = Convert.ToInt64(item.doxcc_icod_correlativo);/*Convert.ToInt64(item.docxc_icod_documento);*/
                                        obj_ADC_Pago.tdocc_icod_tipo_doc = objLibroBancos.ii_tipo_doc;/* item.tdocc_icod_tipo_doc;*/
                                        obj_ADC_Pago.tdocc_iid_correlativo_pago = 0;/* Convert.ToInt32(item.tdodc_iid_correlativo);*/
                                        obj_ADC_Pago.cliec_icod_cliente = Convert.ToInt32(objLibroBancos.cliec_icod_cliente);
                                        obj_ADC_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                                        obj_ADC_Pago.adpac_nmonto_pago = item.tablc_iid_tipo_moneda == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_ADC_Pago.adpac_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_ADC_Pago.adpac_vdescripcion = objLibroBancos.vglosa;
                                        obj_ADC_Pago.adpac_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_ADC_Pago.adpac_iorigen = "B";
                                        obj_ADC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        obj_ADC_Pago.adpac_iusuario_crea = item.iusuario_crea;
                                        obj_ADC_Pago.adpac_vpc_crea = item.vpc_crea;
                                        obj_ADC_Pago.adpac_flag_estado = true;
                                        obj_ADC_Pago.mobac_icod_correlativo = objLibroBancos.icod_correlativo;
                                        long IdAdelantoPago = new CuentasPorCobrarData().insertarAdelantoPago(obj_ADC_Pago);

                                        //Insertar Libro Bancos Detalle
                                        item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                        item.adclie_icod_pago = IdAdelantoPago;
                                        objTesoreriaData.InsertarLibroBancosDetalle(item);

                                        //Actualizar Monto Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(obj_ADC_Pago.doxcc_icod_correlativo_adelanto, obj_ADC_Pago.tablc_iid_tipo_moneda);
                                    }

                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                                    {
                                        //Insertar Nota Credito Pago
                                        ENotaCreditoPago obj_NCC_Pago = new ENotaCreditoPago();
                                        obj_NCC_Pago.doxcc_icod_correlativo_pago = Convert.ToInt64(item.doxcc_icod_correlativo);
                                        obj_NCC_Pago.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(item.doxcc_icod_correlativo);
                                        obj_NCC_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(objLibroBancos.ii_tipo_doc); /*Convert.ToInt32(item.tdocc_icod_tipo_doc);*/
                                        //obj_NCC_Pago.tdocc_iid_correlativo = 0;/* item.tdodc_iid_correlativo;*/
                                        //obj_NCC_Pago.ncpac_vnumero_doc_nota_credito = objLibroBancos.vnro_documento;
                                        obj_NCC_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                                        obj_NCC_Pago.ncpac_nmonto_pago = item.tablc_iid_tipo_moneda == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_NCC_Pago.ncpac_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_NCC_Pago.ncpac_vdescripcion = objLibroBancos.vglosa;
                                        obj_NCC_Pago.ncpac_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_NCC_Pago.ncpac_iorigen = "B";
                                        obj_NCC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;

                                        obj_NCC_Pago.pdxcc_icod_correlativo = null;
                                        obj_NCC_Pago.ncpac_iusuario_crea = item.iusuario_crea;
                                        obj_NCC_Pago.ncpac_vpc_crea = item.vpc_crea;
                                        obj_NCC_Pago.ncpac_flag_estado = true;
                                        long IdNotaCreditoPago = new CuentasPorCobrarData().insertarNCPago(obj_NCC_Pago);

                                        //Insertar Libro Bancos Detalle
                                        item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                        item.ncclie_icod_pago = IdNotaCreditoPago;
                                        objTesoreriaData.InsertarLibroBancosDetalle(item);

                                        //Actualizar Nota Credito Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(obj_NCC_Pago.doxcc_icod_correlativo_nota_credito, obj_NCC_Pago.tablc_iid_tipo_moneda);

                                    }
                                }

                            }
                            else if (item.TipOper == 2)
                            {
                                if (item.iid_cuenta_contable > 0)
                                {
                                    objTesoreriaData.ActualizarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)
                                    {
                                        //Actualizar Adelanto Pago
                                        EAdelantoPago obj_ADC_Pago = new EAdelantoPago();
                                        obj_ADC_Pago.adpac_icod_correlativo = Convert.ToInt64(item.adclie_icod_pago); //
                                        obj_ADC_Pago.doxcc_icod_correlativo_pago = Convert.ToInt64(item.doxcc_icod_correlativo);
                                        obj_ADC_Pago.doxcc_icod_correlativo_adelanto = Convert.ToInt64(item.doxcc_icod_correlativo); /* Convert.ToInt64(item.docxc_icod_documento); //*/
                                        obj_ADC_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(objLibroBancos.ii_tipo_doc);/* item.tdocc_icod_tipo_doc;*/
                                        obj_ADC_Pago.cliec_icod_cliente = Convert.ToInt32(objLibroBancos.cliec_icod_cliente);
                                        obj_ADC_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                                        obj_ADC_Pago.adpac_nmonto_pago = item.tablc_iid_tipo_moneda == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_ADC_Pago.adpac_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_ADC_Pago.adpac_vdescripcion = objLibroBancos.vglosa;
                                        obj_ADC_Pago.adpac_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_ADC_Pago.adpac_iorigen = "B";
                                        obj_ADC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        obj_ADC_Pago.adpac_iusuario_crea = item.iusuario_crea;
                                        obj_ADC_Pago.adpac_vpc_crea = item.vpc_crea;
                                        obj_ADC_Pago.adpac_flag_estado = true;
                                        new CuentasPorCobrarData().modificarAdelantoPago(obj_ADC_Pago);

                                        //Actualizar Libro Bancos Detalle
                                        objTesoreriaData.ActualizarLibroBancosDetalle(item);

                                        //Actualizar Monto Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(obj_ADC_Pago.doxcc_icod_correlativo_adelanto, obj_ADC_Pago.tablc_iid_tipo_moneda);

                                    }

                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                                    {
                                        //Actualizar Nota Credito Pago
                                        ENotaCreditoPago obj_NCC_Pago = new ENotaCreditoPago();
                                        obj_NCC_Pago.ncpac_icod_correlativo = Convert.ToInt64(item.ncclie_icod_pago); //
                                        obj_NCC_Pago.doxcc_icod_correlativo_pago = Convert.ToInt64(item.doxcc_icod_correlativo);
                                        obj_NCC_Pago.doxcc_icod_correlativo_nota_credito = Convert.ToInt64(item.doxcc_icod_correlativo);/* Convert.ToInt64(item.docxc_icod_documento); //*/
                                        obj_NCC_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(objLibroBancos.ii_tipo_doc);/*Convert.ToInt32(item.tdocc_icod_tipo_doc);*/
                                        //obj_NCC_Pago.tdocc_iid_correlativo = 0;/*item.tdodc_iid_correlativo;*/
                                        //obj_NCC_Pago.ncpac_vnumero_doc_nota_credito = objLibroBancos.vnro_documento;
                                        obj_NCC_Pago.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                        obj_NCC_Pago.ncpac_nmonto_pago = item.tablc_iid_tipo_moneda == 3 ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_NCC_Pago.ncpac_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_NCC_Pago.ncpac_vdescripcion = objLibroBancos.vglosa;
                                        obj_NCC_Pago.ncpac_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_NCC_Pago.ncpac_iorigen = "B";
                                        obj_NCC_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;

                                        obj_NCC_Pago.pdxcc_icod_correlativo = null;
                                        obj_NCC_Pago.ncpac_iusuario_crea = item.iusuario_crea;
                                        obj_NCC_Pago.ncpac_vpc_crea = item.vpc_crea;
                                        obj_NCC_Pago.ncpac_flag_estado = true;
                                        new CuentasPorCobrarData().modificarNCPago(obj_NCC_Pago);

                                        //Actualizar Libro Bancos Detalle
                                        objTesoreriaData.ActualizarLibroBancosDetalle(item);

                                        //Actualizar Nota Credito Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(obj_NCC_Pago.doxcc_icod_correlativo_nota_credito, obj_NCC_Pago.tablc_iid_tipo_moneda);

                                    }
                                }
                            }
                        }
                        //CrearVoucherContableBancosPagoAdelNCCliente(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    #region Pago Adelantado a Proveedores
                    if (objLibroBancos.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoProveedores)
                    {
                        //eliminando
                        foreach (ELibroBancosDetalle item in objDetListDelete)
                        {
                            if (item.iid_cuenta_contable > 0)
                                objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                            else
                            {
                                item.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                                new BCuentasPorPagar().EliminarAdelantoPagoNotaCreditoProveedor(item);
                                objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                            }
                            //else if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                            //{
                            //     item.tablc_iid_tipo_moneda = objLibroBancos.iid_tipo_moneda;
                            //    new BCuentasPorCobrar().EliminarAdelantoPagoNotaCredito(item);
                            //    objTesoreriaData.EliminarLibroBancosDetalle(item.icod_correlativo);
                            //}
                        }
                        //Actualizar Entidad Financiera Movimiento Detalle
                        foreach (ELibroBancosDetalle item in ListaLibroDetalle)
                        {
                            if (item.TipOper == 1)
                            {
                                if (item.iid_cuenta_contable > 0)
                                {
                                    item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                    objTesoreriaData.InsertarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor)
                                    {
                                        EDocxPagarPagoAdelanto obj_ADP_Pago = new EDocxPagarPagoAdelanto();
                                        obj_ADP_Pago.doxpc_icod_correlativo_pago = Convert.ToInt64(item.doxpc_icod_correlativo);
                                        obj_ADP_Pago.doxpc_icod_correlativo_adelanto = Convert.ToInt64(item.doxpc_icod_correlativo); ;/* Convert.ToInt64(item.doxpc_icod_documento);*/
                                        obj_ADP_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc);
                                        obj_ADP_Pago.id_tipo_moneda_adelanto = item.tablc_iid_tipo_moneda;
                                        obj_ADP_Pago.adpap_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_ADP_Pago.adpap_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_ADP_Pago.adpap_vdescripcion = objLibroBancos.vglosa;
                                        obj_ADP_Pago.adpap_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_ADP_Pago.adpap_iorigen = "B";
                                        obj_ADP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        obj_ADP_Pago.pdxpc_icod_correlativo = null;
                                        obj_ADP_Pago.adpap_iusuario_crea = item.iusuario_crea;
                                        obj_ADP_Pago.adpap_vpc_crea = item.vpc_crea;
                                        obj_ADP_Pago.adpap_flag_estado = true;
                                        obj_ADP_Pago.mobac_icod_correlativo = objLibroBancos.icod_correlativo;
                                        obj_ADP_Pago.anio = Parametros.intEjercicio;
                                        long Id_ADP_Pago = new CuentasPorPagarData().insertarAdelantoPago(obj_ADP_Pago);

                                        //Insertar Libro Bancos Detalle
                                        item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                        item.adprov_icod_pago = Id_ADP_Pago;

                                        objTesoreriaData.InsertarLibroBancosDetalle(item);

                                        //Actualizar Monto Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(obj_ADP_Pago.doxpc_icod_correlativo_adelanto, item.tablc_iid_tipo_moneda);

                                    }

                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                                    {
                                        //Insertar Doc x Pagar Nota Credito Pago
                                        EDocPorPagarNotaCredito obj_NCP_Pago = new EDocPorPagarNotaCredito();
                                        obj_NCP_Pago.doxpc_icod_correlativo_pago = Convert.ToInt64(item.doxpc_icod_correlativo);
                                        obj_NCP_Pago.doxpc_icod_correlativo_nota_credito = Convert.ToInt64(item.doxpc_icod_correlativo); /*Convert.ToInt64(item.doxpc_icod_documento);*/
                                        obj_NCP_Pago.tipo_documento_nota_credito = Convert.ToInt32(item.tdocc_icod_tipo_doc);
                                        obj_NCP_Pago.idd_correlativo_nota_credito = item.tdodc_iid_correlativo;
                                        //obj_NCP_Pago.ncpap_vnumero_doc_nota_credito = objLibroBancos.vnro_documento;
                                        obj_NCP_Pago.iid_moneda_nota_credito = objLibroBancos.iid_tipo_moneda;
                                        obj_NCP_Pago.ncpap_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_NCP_Pago.ncpap_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_NCP_Pago.ncpap_vdescripcion = objLibroBancos.vglosa;
                                        obj_NCP_Pago.ncpap_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_NCP_Pago.ncpap_iorigen = "B";
                                        obj_NCP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        //obj_NCP_Pago.ncpap_isituacion = Parametros.intSitDocCobrarGenerado;
                                        obj_NCP_Pago.pdxpc_icod_correlativo = null;
                                        obj_NCP_Pago.ncpac_iusuario_crea = item.iusuario_crea;
                                        obj_NCP_Pago.ncpac_vpc_crea = item.vpc_crea;
                                        obj_NCP_Pago.ncpap_flag_estado = true;
                                        obj_NCP_Pago.anio = Parametros.intEjercicio;
                                        obj_NCP_Pago.mobac_icod_correlativo = objLibroBancos.icod_correlativo;
                                        long Id_NCP_Pago = new CuentasPorPagarData().insertarNotaCreditoPago(obj_NCP_Pago);

                                        //Insertar Libro Bancos Detalle
                                        item.icod_correlativo_cabecera = objLibroBancos.icod_correlativo;
                                        item.ncprov_icod_pago = Id_NCP_Pago;
                                        objTesoreriaData.InsertarLibroBancosDetalle(item);

                                        ////Actualizar Nota Credito Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoProveedor(obj_NCP_Pago.doxpc_icod_correlativo_nota_credito, obj_NCP_Pago.iid_moneda_nota_credito);
                                    }
                                }

                            }
                            else if (item.TipOper == 2)
                            {
                                if (item.iid_cuenta_contable > 0)
                                {
                                    objTesoreriaData.ActualizarLibroBancosDetalle(item);
                                }
                                else
                                {
                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor)
                                    {
                                        //Actualizar Doc x Pagar Adelanto Pago
                                        EDocxPagarPagoAdelanto obj_ADP_Pago = new EDocxPagarPagoAdelanto();
                                        obj_ADP_Pago.adpap_icod_correlativo = Convert.ToInt64(item.adprov_icod_pago);
                                        obj_ADP_Pago.doxpc_icod_correlativo_pago = Convert.ToInt64(item.doxpc_icod_correlativo);
                                        obj_ADP_Pago.doxpc_icod_correlativo_adelanto = Convert.ToInt64(item.doxpc_icod_correlativo); /*Convert.ToInt64(item.doxpc_icod_documento);*/
                                        obj_ADP_Pago.tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc);
                                        //obj_ADP_Pago.tdocc_iid_correlativo = item.tdodc_iid_correlativo;
                                        //obj_ADP_Pago.adpap_vnumero_doc_adelanto = objLibroBancos.vnro_documento;
                                        obj_ADP_Pago.cliec_icod_cliente = Convert.ToInt32(objLibroBancos.cliec_icod_cliente);
                                        obj_ADP_Pago.adpap_nmonto_pago = (item.tablc_iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_ADP_Pago.adpap_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_ADP_Pago.adpap_vdescripcion = objLibroBancos.vglosa;
                                        obj_ADP_Pago.adpap_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_ADP_Pago.adpap_iorigen = "B";
                                        obj_ADP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        obj_ADP_Pago.pdxpc_icod_correlativo = null;
                                        obj_ADP_Pago.adpap_iusuario_modifica = item.iusuario_modifica;
                                        obj_ADP_Pago.adpap_vpc_modifica = item.vpc_modifica;
                                        obj_ADP_Pago.adpap_flag_estado = true;
                                        obj_ADP_Pago.mobac_icod_correlativo = objLibroBancos.icod_correlativo;
                                        obj_ADP_Pago.anio = Parametros.intEjercicio;
                                        new CuentasPorPagarData().modificarAdelantoPago(obj_ADP_Pago);

                                        //Actualizar Libro Bancos Detalle
                                        objTesoreriaData.ActualizarLibroBancosDetalle(item);

                                        //Actualizar Monto Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(obj_ADP_Pago.doxpc_icod_correlativo_adelanto, 0);
                                    }

                                    if (item.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                                    {
                                        //Actualizar Doc x Pagar Nota Credito Pago
                                        EDocPorPagarNotaCredito obj_NCP_Pago = new EDocPorPagarNotaCredito();
                                        obj_NCP_Pago.ncpap_icod_correlativo = Convert.ToInt64(item.ncprov_icod_pago);
                                        obj_NCP_Pago.doxpc_icod_correlativo_pago = Convert.ToInt64(item.doxpc_icod_correlativo);
                                        obj_NCP_Pago.doxpc_icod_correlativo_nota_credito = Convert.ToInt64(item.doxpc_icod_correlativo); /*Convert.ToInt64(item.doxpc_icod_documento);*/
                                        obj_NCP_Pago.tipo_documento_nota_credito = Convert.ToInt32(item.tdocc_icod_tipo_doc);
                                        obj_NCP_Pago.idd_correlativo_nota_credito = item.tdodc_iid_correlativo;
                                        //obj_NCP_Pago.ncpap_vnumero_doc_nota_credito = objLibroBancos.vnro_documento;
                                        obj_NCP_Pago.iid_moneda_nota_credito = objLibroBancos.iid_tipo_moneda;
                                        obj_NCP_Pago.ncpap_nmonto_pago = (objLibroBancos.iid_tipo_moneda == 3) ? item.mto_mov_soles : item.mto_mov_dolar;
                                        obj_NCP_Pago.ncpap_nmonto_tipo_cambio = objLibroBancos.nmonto_tipo_cambio;
                                        obj_NCP_Pago.ncpap_vdescripcion = objLibroBancos.vglosa;
                                        obj_NCP_Pago.ncpap_sfecha_pago = objLibroBancos.dfecha_movimiento;
                                        obj_NCP_Pago.ncpap_iorigen = "B";
                                        obj_NCP_Pago.efctc_icod_enti_financiera_cuenta = objLibroBancos.icod_enti_financiera_cuenta;
                                        //obj_NCP_Pago.ncpap_isituacion = Parametros.intSitDocCobrarGenerado;
                                        obj_NCP_Pago.pdxpc_icod_correlativo = null;
                                        obj_NCP_Pago.ncpac_iusuario_crea = item.iusuario_crea;
                                        obj_NCP_Pago.ncpac_vpc_crea = item.vpc_crea;
                                        obj_NCP_Pago.ncpap_flag_estado = true;
                                        obj_NCP_Pago.anio = Parametros.intEjercicio;
                                        obj_NCP_Pago.mobac_icod_correlativo = objLibroBancos.icod_correlativo;
                                        new CuentasPorPagarData().modificarNotaCreditoPago(obj_NCP_Pago);

                                        ////Actualizar Libro Bancos Detalle
                                        objTesoreriaData.ActualizarLibroBancosDetalle(item);

                                        ////Actualizar Nota Credito Pagado Saldo
                                        objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoProveedor(obj_NCP_Pago.doxpc_icod_correlativo_pago, obj_NCP_Pago.iid_moneda_nota_credito);
                                    }
                                }
                            }
                        }
                        //CrearVoucherContableBancosPagoAdelNCProveedor(objLibroBancos, ListaLibroDetalle);
                    }
                    #endregion
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarLibroBancos(ELibroBancos obj_LibroBanco)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                #region Motivo : Varios
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoVarios)
                {
                    objTesoreriaData.EliminarLibroBancos(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Cuentas por Pagar
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                {
                    /*Listamos el detalle antes de eliminar la cabecera*/
                    List<ELibroBancosDetalle> ListaDetalle = (new TesoreriaData()).ListarEntidadFinancieraDetalle(obj_LibroBanco.icod_correlativo);
                    //Eliminar el movimiento de la cuenta bancaria
                    objTesoreriaData.EliminarLibroBancos(obj_LibroBanco.icod_correlativo);

                    foreach (ELibroBancosDetalle item in ListaDetalle)
                    {
                        //Eliminar el documento por pagar pago, como tambien el detalle
                        EDocPorPagarPago obj_DXP_Pago = new EDocPorPagarPago();
                        obj_DXP_Pago.pdxpc_icod_correlativo = Convert.ToInt32(item.docxp_icod_pago);
                        obj_DXP_Pago.doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo);
                        obj_DXP_Pago.intUsuario = obj_LibroBanco.iusuario_modifica;
                        obj_DXP_Pago.strPc = obj_LibroBanco.vpc_modifica;
                        obj_DXP_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                        new CuentasPorPagarData().eliminarDocPorPagarPago(obj_DXP_Pago);
                        //Actualizar Monto Pagado Saldo
                        objTesoreriaData.ActualizarMontoDXPPagadoSaldo(obj_DXP_Pago.doxpc_icod_correlativo, obj_DXP_Pago.tablc_iid_tipo_moneda);
                    }
                }
                #endregion
                #region Motivo : Cuentas por Cobrar
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                {
                    //Listamos el detalle de movimiento de la cuenta.
                    List<ELibroBancosDetalle> ListaDetalle = null;
                    ListaDetalle = (new TesoreriaData()).ListarEntidadFinancieraDetalle(obj_LibroBanco.icod_correlativo);
                    foreach (ELibroBancosDetalle item in ListaDetalle)
                    {
                        if (item.tdocc_icod_tipo_doc != 83)
                        {
                            //Eliminar el documento por cobrar pago, como tambien el detalle
                            EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                            objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt32(item.docxc_icod_pago);
                            objDocumentoPorCobrarData.EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);

                            //Actualizar monto pagado saldo
                            objDocumentoPorCobrarData.ActualizarMontoPagadoSaldo(Convert.ToInt64(item.doxcc_icod_correlativo), item.tablc_iid_tipo_moneda);
                        }
                        else
                        {
                            if (item.tdocc_icod_tipo_doc == 83)
                            {
                                EDocXCobrar _beDXC = new EDocXCobrar();
                                _beDXC.doxcc_icod_correlativo = item.doxcc_icod_correlativo_ADC;
                                _beDXC.intUsuario = item.iusuario_crea;
                                _beDXC.strPc = item.vpc_crea;
                                new CuentasPorCobrarData().EliminarDocumentoXCobrar(_beDXC);

                                EAdelantoCliente _BEADC = new EAdelantoCliente();
                                _BEADC.icod_correlativo = item.adci_icod_correlativo;
                                _BEADC.iusuario_modifica = item.iusuario_crea;
                                _BEADC.vpc_modifica = item.vpc_crea;
                                objTesoreriaData.eliminarAdelantoCliente(_BEADC);
                            }
                        }

                    }

                    //Eliminar el movimiento de la cuenta bancaria
                    objTesoreriaData.EliminarLibroBancos(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Adelanto Cliente
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoAdelantosClientes)
                {
                    objTesoreriaData.EliminarLibroBancosAdelantoCliente(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Adelanto Proveedores
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoAdelantosProveedores)
                {
                    objTesoreriaData.EliminarLibroBancosAdelantoProveedor(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Pago Adelanto NC Proveedores
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoProveedores)
                {
                    var lstDetalle = ListarEntidadFinancieraDetalleADPNCP(obj_LibroBanco.icod_correlativo);
                    lstDetalle.ForEach(x =>
                    {
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor)
                        {
                            //Eliminar Doc X Pagar Adelanto Pago
                            EDocxPagarPagoAdelanto objE_DocxPagarPagoAdelanto = new EDocxPagarPagoAdelanto();
                            objE_DocxPagarPagoAdelanto.adpap_icod_correlativo = Convert.ToInt32(x.adprov_icod_pago);
                            objE_DocxPagarPagoAdelanto.adpap_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            objDocumentoPorPagarData.eliminarAdelantoPago(objE_DocxPagarPagoAdelanto);

                            ////Actualizar Monto Pagado Saldo
                            //objDocumentoPorPagarData.ActualizarMontoPagadoSaldoAdelanto(Convert.ToInt64(x.doxpc_icod_correlativo), 0);

                            //Actualizar Monto Pagado Saldo
                            objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(Convert.ToInt64(x.doxpc_icod_correlativo), 0);

                        }
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                        {
                            //Eliminar Dox X Pagar Nota Credito Pago
                            EDocPorPagarNotaCredito objE_DocPorPagarNotaCredito = new EDocPorPagarNotaCredito();
                            //objE_DocPorPagarNotaCredito.ncpap_icod_correlativo = Convert.ToInt32(x.ncprov_icod_pago);
                            //objE_DocPorPagarNotaCredito.ncpap_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            //objDocumentoPorPagarData.eliminarNotaCreditoPago(objE_DocPorPagarNotaCredito);

                            //Actualizar Nota Credito Pagado Saldo
                            objDocumentoPorPagarData.ActualizarMontoPagadoSaldoNotaCredito(Convert.ToInt64(x.doxpc_icod_correlativo), 0);

                        }

                    });
                    //Eliminar el movimiento de la cuenta bancaria
                    objTesoreriaData.EliminarLibroBancos(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Pago Adelanto NC Clientes
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoClientes)
                {
                    var lstDetalle = ListarEntidadFinancieraDetalleADPNCP_Cliente(obj_LibroBanco.icod_correlativo);
                    lstDetalle.ForEach(x =>
                    {
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)
                        {
                            //Eliminar Adelanto Pago
                            EAdelantoPago objE_AdelantoPago = new EAdelantoPago();
                            objE_AdelantoPago.adpac_icod_correlativo = Convert.ToInt32(x.adclie_icod_pago);
                            objE_AdelantoPago.adpac_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            objDocumentoPorCobrarData.eliminarAdelantoPago(objE_AdelantoPago);
                            //Actualizar Monto Pagado Saldo
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.doxcc_icod_correlativo), 0);
                        }
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                        {
                            //Eliminar Nota Credito Pago
                            ENotaCreditoPago objE_NotaCreditoPago = new ENotaCreditoPago();
                            objE_NotaCreditoPago.ncpac_icod_correlativo = Convert.ToInt32(x.ncclie_icod_pago);
                            objE_NotaCreditoPago.ncpac_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            objDocumentoPorCobrarData.eliminarNCPago(objE_NotaCreditoPago);

                            //Actualizar Nota Credito Pagado Saldo
                            objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(Convert.ToInt64(x.doxcc_icod_correlativo), 0);

                        }
                    });
                    //Eliminar el movimiento de la cuenta bancaria
                    objTesoreriaData.EliminarLibroBancos(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Transferencia
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoTransferenciaCuentas)
                {
                    objTesoreriaData.EliminarLibroBancos(obj_LibroBanco.icod_correlativo);
                    objTesoreriaData.EliminarLibroBancos(Convert.ToInt32(obj_LibroBanco.id_transferencia));
                }
                #endregion
                tx.Complete();
            }
        }

        public void anularLibroBancos(ELibroBancos obj_LibroBanco)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                #region Motivo : Varios
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoVarios)
                {
                    objTesoreriaData.AnularLibroBancosVarios(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Cuentas por Pagar
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorPagar)
                {
                    //Eliminar el movimiento de la cuenta bancaria
                    objTesoreriaData.AnularLibroBancos(obj_LibroBanco.icod_correlativo);
                    //Listamos el detalle de movimiento de la cuenta.
                    List<ELibroBancosDetalle> ListaDetalle = (new TesoreriaData()).ListarEntidadFinancieraDetalle(obj_LibroBanco.icod_correlativo);

                    foreach (ELibroBancosDetalle item in ListaDetalle)
                    {
                        //Eliminar el documento por pagar pago, como tambien el detalle
                        EDocPorPagarPago obj_DXP_Pago = new EDocPorPagarPago();
                        obj_DXP_Pago.pdxpc_icod_correlativo = Convert.ToInt32(item.docxp_icod_pago);
                        obj_DXP_Pago.doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo);
                        obj_DXP_Pago.intUsuario = obj_LibroBanco.iusuario_modifica;
                        obj_DXP_Pago.strPc = obj_LibroBanco.vpc_modifica;
                        obj_DXP_Pago.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                        new CuentasPorPagarData().eliminarDocPorPagarPago(obj_DXP_Pago);
                        //Actualizar Monto Pagado Saldo
                        objTesoreriaData.ActualizarMontoDXPPagadoSaldo(obj_DXP_Pago.doxpc_icod_correlativo, obj_DXP_Pago.tablc_iid_tipo_moneda);
                    }


                }
                #endregion
                #region Motivo : Cuentas por Cobrar
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoCuentasPorCobrar)
                {
                    //Anular el movimiento de la cuenta bancaria
                    objTesoreriaData.AnularLibroBancos(obj_LibroBanco.icod_correlativo);

                    //Listamos el detalle de movimiento de la cuenta.
                    List<ELibroBancosDetalle> ListaDetalle = null;
                    ListaDetalle = (new TesoreriaData()).ListarEntidadFinancieraDetalle(obj_LibroBanco.icod_correlativo);
                    foreach (ELibroBancosDetalle item in ListaDetalle)
                    {
                        if (item.tdocc_icod_tipo_doc != 83)
                        {
                            //Eliminar el documento por cobrar pago, como tambien el detalle
                            EDocXCobrarPago objE_DocPorCobrarPago = new EDocXCobrarPago();
                            objE_DocPorCobrarPago.pdxcc_icod_correlativo = Convert.ToInt32(item.docxc_icod_pago);
                            objDocumentoPorCobrarData.EliminarPagoDirectoDocumentoXCobrar(objE_DocPorCobrarPago);

                            //Actualizar monto pagado saldo
                            objDocumentoPorCobrarData.ActualizarMontoPagadoSaldo(Convert.ToInt64(item.doxcc_icod_correlativo), item.tablc_iid_tipo_moneda);
                        }
                        else
                        {
                            if (item.tdocc_icod_tipo_doc == 83)
                            {
                                EDocXCobrar _beDXC = new EDocXCobrar();
                                _beDXC.doxcc_icod_correlativo = item.doxcc_icod_correlativo_ADC;
                                _beDXC.intUsuario = item.iusuario_crea;
                                _beDXC.strPc = item.vpc_crea;
                                new CuentasPorCobrarData().EliminarDocumentoXCobrar(_beDXC);

                                EAdelantoCliente _BEADC = new EAdelantoCliente();
                                _BEADC.icod_correlativo = item.adci_icod_correlativo;
                                _BEADC.iusuario_modifica = item.iusuario_crea;
                                _BEADC.vpc_modifica = item.vpc_crea;
                                objTesoreriaData.eliminarAdelantoCliente(_BEADC);
                            }
                        }
                    }
                }
                #endregion
                #region Motivo : Adelanto Proveedores
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoAdelantosProveedores)
                {
                    objTesoreriaData.AnularLibroBancosAdelantoProveedor(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Adelanto Cliente
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoAdelantosClientes)
                {
                    objTesoreriaData.AnularLibroBancosAdelantoCliente(obj_LibroBanco.icod_correlativo);
                }
                #endregion
                #region Motivo : Pago Adelanto Clientes
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoClientes)
                {
                    //Anular el movimiento de la cuenta bancaria
                    objTesoreriaData.AnularLibroBancos(obj_LibroBanco.icod_correlativo);

                    //Listamos el detalle de movimiento de la cuenta.

                    var lstDetalle = (new TesoreriaData()).ListarEntidadFinancieraDetalleADPNCP_Cliente(obj_LibroBanco.icod_correlativo);
                    lstDetalle.ForEach(x =>
                    {
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente)
                        {

                            EAdelantoPago objE_AdelantoPago = new EAdelantoPago();
                            objE_AdelantoPago.adpac_icod_correlativo = Convert.ToInt32(x.adclie_icod_pago);
                            objE_AdelantoPago.adpac_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            objDocumentoPorCobrarData.eliminarAdelantoPago(objE_AdelantoPago);

                            //Actualizar Monto Pagado Saldo
                            objTesoreriaData.ActualizarMontoPagadoAdelantoCliente(Convert.ToInt64(x.doxcc_icod_correlativo), 0);
                        }
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente)
                        {
                            //Eliminar Nota Credito Pago
                            ENotaCreditoPago objE_NotaCreditoPago = new ENotaCreditoPago();
                            objE_NotaCreditoPago.ncpac_icod_correlativo = Convert.ToInt32(x.ncclie_icod_pago);
                            objE_NotaCreditoPago.ncpac_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            objDocumentoPorCobrarData.eliminarNCPago(objE_NotaCreditoPago);

                            //Actualizar Nota Credito Pagado Saldo
                            objTesoreriaData.ActualizarMontoPagadoSaldoNotaCreditoCliente(objE_NotaCreditoPago.doxcc_icod_correlativo_nota_credito, objE_NotaCreditoPago.tablc_iid_tipo_moneda);
                        }
                    });
                }
                #endregion
                #region Motivo : Pago Adelanto Proveedores
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoPagoAdelantadoProveedores)
                {
                    //Anular el movimiento de la cuenta bancaria
                    objTesoreriaData.AnularLibroBancos(obj_LibroBanco.icod_correlativo);

                    //Listamos el detalle de movimiento de la cuenta.
                    //
                    var lstDetalle = ListarEntidadFinancieraDetalleADPNCP(obj_LibroBanco.icod_correlativo);
                    lstDetalle.ForEach(x =>
                    {
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor)
                        {
                            //Eliminar Doc X Pagar Adelanto Pago
                            EDocxPagarPagoAdelanto objE_DocxPagarPagoAdelanto = new EDocxPagarPagoAdelanto();
                            objE_DocxPagarPagoAdelanto.adpap_icod_correlativo = Convert.ToInt32(x.adprov_icod_pago);
                            objE_DocxPagarPagoAdelanto.adpap_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            objDocumentoPorPagarData.eliminarAdelantoPago(objE_DocxPagarPagoAdelanto);

                            //Actualizar Monto Pagado Saldo
                            objTesoreriaData.ActualizarMontoPagadoSaldoAdelantoProveedor(Convert.ToInt64(x.doxpc_icod_correlativo), 0);
                            ////Actualizar Monto Pagado Saldo
                            //objDocumentoPorPagarData.ActualizarMontoPagadoSaldoAdelanto(Convert.ToInt64(x.doxpc_icod_correlativo), 0);

                        }
                        if (x.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor)
                        {
                            //Eliminar Dox X Pagar Nota Credito Pago
                            EDocPorPagarNotaCredito objE_DocPorPagarNotaCredito = new EDocPorPagarNotaCredito();
                            //objE_DocPorPagarNotaCredito.ncpap_icod_correlativo = Convert.ToInt32(x.ncprov_icod_pago);
                            //objE_DocPorPagarNotaCredito.ncpap_vpc_modifica = obj_LibroBanco.vpc_modifica;
                            //objDocumentoPorPagarData.eliminarNotaCreditoPago(objE_DocPorPagarNotaCredito);

                            ////Actualizar Nota Credito Pagado Saldo
                            //objDocumentoPorPagarData.ActualizarMontoPagadoSaldoNotaCredito(Convert.ToInt64(x.doxpc_icod_correlativo), 0);

                        }
                    });
                }
                #endregion
                #region Motivo : Transferencia
                if (obj_LibroBanco.iid_motivo_mov_banco == Parametros.intMotivoTransferenciaCuentas)
                {
                    objTesoreriaData.AnularLibroBancosVarios(obj_LibroBanco.icod_correlativo);
                    objTesoreriaData.AnularLibroBancosVarios(Convert.ToInt32(obj_LibroBanco.id_transferencia));
                }
                #endregion
                tx.Complete();
            }
        }


        public void conciliarLibroBancos(int IdLibroBanco, bool FlagConcilia, int IdUsuario, string PC)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objTesoreriaData.ConciliarLibroBancos(IdLibroBanco, FlagConcilia, IdUsuario, PC);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region caja
        public List<EConceptoMovCaja> ListarConceptoCaja()
        {
            List<EConceptoMovCaja> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarConceptoCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarMovCaja(EConceptoMovCaja EConceptoMovCaja)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objTesoreriaData.InsertarMovCaja(EConceptoMovCaja);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarMovCaja(EConceptoMovCaja EConceptoMovCaja)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objTesoreriaData.ActualizarMovCaja(EConceptoMovCaja);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarMovCaja(EConceptoMovCaja oBE)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objTesoreriaData.EliminarMovCaja(oBE);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EConceptoCaja> ListarConceptoCajaEfectivo()
        {
            List<EConceptoCaja> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarConceptoCajaEfectivo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarCajaEfectivo(EConceptoCaja EConceptoMovCaja)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objTesoreriaData.InsertarCajaEfectivo(EConceptoMovCaja);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCajaEfectivo(EConceptoCaja EConceptoMovCaja)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objTesoreriaData.ActualizarCajaEfectivo(EConceptoMovCaja);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarCajaEfectivo(EConceptoCaja oBE)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objTesoreriaData.EliminarCajaEfectivo(oBE);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //*****////
        public List<ECajaChica> ListarCajaChica()
        {
            List<ECajaChica> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarCajaChica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarCajaChica(ECajaChica objCajaChica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objTesoreriaData.InsertarCajaChica(objCajaChica);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarCajaChica(ECajaChica oBE)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    objTesoreriaData.EliminarCajaChica(oBE);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCajaChica(ECajaChica objCajaChica)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    objTesoreriaData.ActualizarCajaChica(objCajaChica);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void insertarLiquidacionCaja(ELiquidacionCaja oBe, List<ELiquidacionCajaDet> lstDet)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    int IdLiquidacion = objTesoreriaData.insertarLiquidacionCaja(oBe);
                    oBe.lqcc_icod_liquid_cja = IdLiquidacion;
                    int? intNullVal = null;

                    List<ECuentaContable> lstPlanCuentas = new BContabilidad().listarCuentaContable();

                    foreach (ELiquidacionCajaDet item in lstDet)
                    {
                        item.lqcc_icod_liquid_caja = IdLiquidacion;
                        if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaCContable)
                        {
                            objTesoreriaData.insertarLiquidacionCajaDetalle(item);
                        }
                        else if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaPagoProvision)
                        {
                            EDocPorPagarPago objDcxp = new EDocPorPagarPago();
                            objDcxp.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo); //IdDocumentoPorPagar
                            objDcxp.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                            objDcxp.pdxpc_vnumero_doc = item.lq_nro_doc_pago;
                            objDcxp.pdxpc_sfecha_pago = oBe.lqcc_sfecha_liquid;
                            objDcxp.tablc_iid_tipo_moneda = oBe.lqcc_iid_tipo_moneda;
                            objDcxp.pdxpc_nmonto_pago = item.lqcd_nmonto_pago;
                            objDcxp.pdxpc_nmonto_tipo_cambio = item.lqcd_ntipo_cambio_pago;
                            objDcxp.pdxpc_vobservacion = item.lqcd_vdescripcion_movim;
                            objDcxp.efctc_icod_enti_financiera_cuenta = null;
                            objDcxp.pdxpc_vorigen = "C";
                            objDcxp.doxcc_icod_correlativo = null;
                            objDcxp.ctacc_iid_cuenta_contable = null;
                            objDcxp.cecoc_icod_centro_costo = null;
                            objDcxp.anac_icod_analitica = null;
                            objDcxp.intUsuario = item.intUsuario;
                            objDcxp.strPc = item.strPc;
                            objDcxp.anio = oBe.lqcc_iid_anio;
                            objDcxp.pdxpc_mes = oBe.lqcc_iid_mes;
                            objDcxp.pdxpc_flag_estado = true;
                            long IdPago = new CuentasPorPagarData().insertarDocPorPagarPago(objDcxp); //INSERCIÓN DE PAGO DE DOC X PGR.

                            item.docxp_icod_pago = Convert.ToInt32(IdPago);
                            objTesoreriaData.insertarLiquidacionCajaDetalle(item); //INSERCIÓN DEL DET DE LA LQD.
                            objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objDcxp.doxpc_icod_correlativo, objDcxp.tablc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                        }
                        else if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaGenProvision)
                        {
                            /********SE DEBE GENERAR EL DOC. X PAGAR CON SU RESPECTIA CUENTA********************************************/
                            EDocPorPagar oBeDXP = crearDocPorPagarParaLiquidacioCaja(oBe, item);
                            item.docxp_icod_correlativo = objDocumentoPorPagarData.insertarDocPorPagar(oBeDXP);
                            /********DETALLE DE DXP CTA. CONTABLE**************/
                            EDocPorPagarDetalleCuentaContable oBeDXPCtaCtbl = new EDocPorPagarDetalleCuentaContable();
                            oBeDXPCtaCtbl.pdxpc_nmonto_pago_dxp = item.lqcd_nmonto_pago;
                            oBeDXPCtaCtbl.pdxpc_nmonto_pago_dxp = 0;
                            oBeDXPCtaCtbl.doxpc_vnumero_doc = item.lqcd_vnumero_doc;
                            oBeDXPCtaCtbl.tdocc_vabreviatura_tipo_doc = item.tip_doc_abreviatura;
                            oBeDXPCtaCtbl.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo);
                            oBeDXPCtaCtbl.ctacc_iid_cuenta_contable = Convert.ToInt32(item.lqcd_iid_cuenta_contable);
                            if (oBeDXPCtaCtbl.ctacc_iid_cuenta_contable == 0)
                                throw new ArgumentException(String.Format("El CONCEPTO de Liquidación de Caja ingresado, NO contiene un NRO. DE CUENTA CONTABLE asignado para la creación del DOC. POR PAGAR", item.lqcd_vnumero_doc));

                            oBeDXPCtaCtbl.cdxpc_nmonto_cuenta = item.lqcd_nmonto_pago;
                            oBeDXPCtaCtbl.cdxpc_vglosa = item.lqcd_vdescripcion_movim;
                            oBeDXPCtaCtbl.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                            oBeDXPCtaCtbl.intUsuario = oBe.intUsuario;
                            oBeDXPCtaCtbl.strPc = oBe.strPc;
                            oBeDXPCtaCtbl.cdxpc_flag_estado = true; //estado del detalle
                            oBeDXPCtaCtbl.TipOper = 1;
                            ECuentaContable oBeCta = lstPlanCuentas.Where(x => x.ctacc_icod_cuenta_contable == item.lqcd_iid_cuenta_contable).ToList()[0];
                            oBeDXPCtaCtbl.anac_icod_analitica = (Convert.ToInt32(oBeCta.tablc_iid_tipo_analitica) != 0) ? Convert.ToInt32(item.lqcd_iid_analitica) : intNullVal;
                            oBeDXPCtaCtbl.cecoc_icod_centro_costo = (oBeCta.ctacc_iccosto) ? Convert.ToInt32(item.lqcd_iid_centro_costo) : intNullVal;
                            objDocumentoPorPagarData.insertarDXPDetCtaContable(oBeDXPCtaCtbl);
                            /********SE DE DEBE REALIZAR EL PAGO DEL DOC. X PAGAR CREADO********************************************/
                            EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                            oBeDXPPago.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo);
                            oBeDXPPago.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                            oBeDXPPago.pdxpc_vnumero_doc = item.lq_nro_doc_pago;
                            oBeDXPPago.pdxpc_sfecha_pago = oBe.lqcc_sfecha_liquid;
                            oBeDXPPago.tablc_iid_tipo_moneda = oBe.lqcc_iid_tipo_moneda;
                            oBeDXPPago.pdxpc_nmonto_pago = item.lqcd_nmonto_pago;
                            oBeDXPPago.pdxpc_nmonto_tipo_cambio = item.lqcd_ntipo_cambio_pago;
                            oBeDXPPago.pdxpc_vobservacion = item.lqcd_vdescripcion_movim;
                            oBeDXPPago.efctc_icod_enti_financiera_cuenta = null;
                            oBeDXPPago.pdxpc_vorigen = "C";
                            oBeDXPPago.doxcc_icod_correlativo = null;
                            oBeDXPPago.ctacc_iid_cuenta_contable = null;
                            oBeDXPPago.cecoc_icod_centro_costo = null;
                            oBeDXPPago.anac_icod_analitica = null;
                            oBeDXPPago.intUsuario = item.intUsuario;
                            oBeDXPPago.strPc = item.strPc;
                            oBeDXPPago.anio = oBe.lqcc_iid_anio;
                            oBeDXPPago.pdxpc_mes = oBe.lqcc_iid_mes;
                            oBeDXPPago.pdxpc_flag_estado = true;

                            long IdPago = new CuentasPorPagarData().insertarDocPorPagarPago(oBeDXPPago); //INSERCIÓN DE PAGO DE DOC X PGR.

                            item.docxp_icod_pago = Convert.ToInt32(IdPago);
                            objTesoreriaData.insertarLiquidacionCajaDetalle(item); //INSERCIÓN DEL DET DE LA LQD.
                            objTesoreriaData.ActualizarMontoDXPPagadoSaldo(oBeDXPPago.doxpc_icod_correlativo, oBeDXPPago.tablc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                        }
                    }
                    tx.Complete();
                }
                //CrearVoucherContableLiquidacionCaja(objLiquidacion, oListLiq, 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EDocPorPagar crearDocPorPagarParaLiquidacioCaja(ELiquidacionCaja oBeCab, ELiquidacionCajaDet oBeDet)
        {
            EDocPorPagar objDXP = new EDocPorPagar();
            try
            {
                objDXP.doxpc_icod_correlativo = Convert.ToInt64(oBeDet.docxp_icod_correlativo);
                objDXP.anio = Parametros.intEjercicio;
                objDXP.mesec_iid_mes = oBeCab.lqcc_iid_mes;
                objDXP.tdocc_icod_tipo_doc = Convert.ToInt32(oBeDet.lq_icod_tipo_doc_pago);
                objDXP.tdodc_iid_correlativo = Convert.ToInt32(oBeDet.lq_icod_clase_doc_pago);

                objDXP.doxpc_iid_correlativo = 0;
                objDXP.doxpc_vnumero_doc = oBeDet.lqcd_vnumero_doc;
                objDXP.doxpc_sfecha_doc = oBeDet.lqcd_sfecha_liquid;
                objDXP.doxpc_sfecha_vencimiento_doc = oBeDet.lqcd_sfecha_liquid;
                objDXP.proc_icod_proveedor = Convert.ToInt32(oBeDet.lqcd_iid_proveedor);
                objDXP.tablc_iid_tipo_moneda = oBeCab.lqcc_iid_tipo_moneda;
                objDXP.doxpc_nmonto_tipo_cambio = oBeCab.lqcc_ntipo_cambio;
                if (objDXP.doxpc_nmonto_tipo_cambio == 0)
                    throw new ArgumentException("No se econtró tipo de cambio para la fecha del documento, favor de registrar tipo de cambio");
                objDXP.doxpc_vdescrip_transaccion = oBeDet.lqcd_vdescripcion_movim;
                objDXP.doxpc_nmonto_destino_mixto = oBeDet.lqcd_nmonto_dest_mixto;
                objDXP.doxpc_nmonto_destino_nogravado = 0;
                objDXP.doxpc_nmonto_nogravado = oBeDet.lqcd_nmonto_inafecto;
                objDXP.doxpc_nmonto_referencial_cif = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nmonto_destino_gravado = oBeDet.lqcd_nmonto_afecto;
                objDXP.doxpc_nmonto_imp_destino_gravado = (oBeDet.lqcd_nmonto_afecto > 0) ? Convert.ToDecimal(oBeDet.lqcd_nmonto_igv) : 0;
                objDXP.doxpc_nmonto_imp_destino_mixto = (oBeDet.lqcd_nmonto_dest_mixto > 0) ? Convert.ToDecimal(oBeDet.lqcd_nmonto_igv) : 0;
                objDXP.doxpc_nmonto_imp_destino_nogravado = 0;
                objDXP.doxpc_nmonto_total_pagado = 0;
                objDXP.doxpc_nmonto_total_documento = Convert.ToDecimal(oBeDet.lqcd_nmonto_pago);
                objDXP.doxpc_nmonto_total_saldo = Convert.ToDecimal(oBeDet.lqcd_nmonto_pago);
                objDXP.doxpc_nporcentaje_igv = Convert.ToDecimal(oBeDet.lqcd_nporcent_igv);
                objDXP.tablc_iid_situacion_documento = Parametros.intSitDocGenerado;
                objDXP.doxpc_tipo_comprobante_referencia = 0;
                objDXP.doxpc_num_serie_referencia = "";
                objDXP.doxpc_num_comprobante_referencia = "";
                objDXP.doxpc_sfecha_emision_referencia = null;
                objDXP.doxpc_nporcentaje_isc = 0;
                objDXP.doxpc_nmonto_isc = 0;
                objDXP.doxpc_nmonto_retenido = 0;
                objDXP.doxpc_nmonto_retencion_rh = 0;
                objDXP.doxpc_nmonto_servicio_no_domic = 0;
                objDXP.doxpc_nporcentaje_imp_renta = 0;
                //objE_DocXPagar.doxpc_vnro_deposito_detraccion = txtDetraccion.Text;
                objDXP.doxpc_sfec_deposito_detraccion = null;
                //objDXP.doxpc_icod_documento = oBeDet.facc_icod_doc;
                objDXP.intUsuario = oBeCab.intUsuario;
                objDXP.strPc = oBeCab.strPc;
                objDXP.doxpc_origen = Parametros.origenLiquidacionCaja;
                objDXP.doxpc_flag_estado = true;
                return objDXP;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void modificarLiquidacionCaja(ELiquidacionCaja oBe, List<ELiquidacionCajaDet> lstDet, List<ELiquidacionCajaDet> lstDelete)
        {
            int? intNullVal = null;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<ECuentaContable> lstPlanCuentas = new BContabilidad().listarCuentaContable();
                    objTesoreriaData.modificarLiquidacionCaja(oBe);
                    #region Lista de Eliminación
                    foreach (ELiquidacionCajaDet item in lstDelete)
                    {
                        objTesoreriaData.eliminarLiquidacionCajaDetalle(item);
                        if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaPagoProvision)
                        {
                            item.intUsuario = oBe.intUsuario;
                            item.strPc = oBe.strPc;
                            objTesoreriaData.EliminarLiquidacionCajaDetalleDocXPagarPago(item);
                            objTesoreriaData.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(item.docxp_icod_correlativo), oBe.lqcc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                        }
                        if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaGenProvision)
                        {
                            /*ELIMINAMOS EL DOC. POR PAGAR*/
                            EDocPorPagar oBeDXP = new EDocPorPagar();
                            oBeDXP.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo);
                            oBeDXP.intUsuario = oBe.intUsuario;
                            oBeDXP.strPc = oBe.strPc;
                            objDocumentoPorPagarData.eliminarDocPorPagar(oBeDXP);

                            var lstDXPDet = objDocumentoPorPagarData.listarDXPDetCtaContable(Convert.ToInt64(item.docxp_icod_correlativo));
                            objDocumentoPorPagarData.eliminarDXPDetCtaContable(lstDXPDet, null);

                            /*ELIMINAMOS EL PAGO REALIZADO*/
                            item.intUsuario = oBe.intUsuario;
                            item.strPc = oBe.strPc;
                            objTesoreriaData.EliminarLiquidacionCajaDetalleDocXPagarPago(item);
                            objTesoreriaData.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(item.docxp_icod_correlativo), oBe.lqcc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                        }
                    }
                    #endregion
                    foreach (ELiquidacionCajaDet item in lstDet)
                    {
                        if (item.operacion == 1)
                        {
                            item.lqcc_icod_liquid_caja = oBe.lqcc_icod_liquid_cja;

                            if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaCContable)
                            {
                                objTesoreriaData.insertarLiquidacionCajaDetalle(item);
                            }
                            else if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaPagoProvision)
                            {
                                #region INSERTAR EL PAGO PROVISION
                                EDocPorPagarPago objDcxp = new EDocPorPagarPago();
                                objDcxp.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo); //IdDocumentoPorPagar
                                objDcxp.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                                objDcxp.pdxpc_vnumero_doc = item.lq_nro_doc_pago;
                                objDcxp.pdxpc_sfecha_pago = oBe.lqcc_sfecha_liquid;
                                objDcxp.tablc_iid_tipo_moneda = oBe.lqcc_iid_tipo_moneda;
                                objDcxp.pdxpc_nmonto_pago = item.lqcd_nmonto_pago;
                                objDcxp.pdxpc_nmonto_tipo_cambio = item.lqcd_ntipo_cambio_pago;
                                objDcxp.pdxpc_vobservacion = item.lqcd_vdescripcion_movim;
                                objDcxp.efctc_icod_enti_financiera_cuenta = null;
                                objDcxp.pdxpc_vorigen = "C";
                                objDcxp.doxcc_icod_correlativo = null;
                                objDcxp.ctacc_iid_cuenta_contable = null;
                                objDcxp.cecoc_icod_centro_costo = null;
                                objDcxp.anac_icod_analitica = null;
                                objDcxp.intUsuario = item.intUsuario;
                                objDcxp.strPc = item.strPc;
                                objDcxp.anio = oBe.lqcc_iid_anio;
                                objDcxp.pdxpc_mes = oBe.lqcc_iid_mes;
                                objDcxp.pdxpc_flag_estado = true;
                                long IdPago = new CuentasPorPagarData().insertarDocPorPagarPago(objDcxp); //INSERCIÓN DE PAGO DE DOC X PGR.

                                item.docxp_icod_pago = Convert.ToInt32(IdPago);
                                objTesoreriaData.insertarLiquidacionCajaDetalle(item); //INSERCIÓN DEL DET DE LA LQD.
                                objTesoreriaData.ActualizarMontoDXPPagadoSaldo(objDcxp.doxpc_icod_correlativo, objDcxp.tablc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                                #endregion
                            }
                            else if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaGenProvision)
                            {
                                #region INSERTAR EL DOC POR PAGAR
                                /********SE DEBE GENERAR EL DOC. X PAGAR CON SU RESPECTIA CUENTA********************************************/
                                EDocPorPagar oBeDXP = crearDocPorPagarParaLiquidacioCaja(oBe, item);
                                item.docxp_icod_correlativo = objDocumentoPorPagarData.insertarDocPorPagar(oBeDXP);
                                #endregion
                                #region INSERTAR EL DET. CTA CONTABLE DEL DOC POR PAGAR
                                /********DETALLE DE DXP CTA. CONTABLE**************/
                                EDocPorPagarDetalleCuentaContable oBeDXPCtaCtbl = new EDocPorPagarDetalleCuentaContable();
                                oBeDXPCtaCtbl.pdxpc_nmonto_pago_dxp = item.lqcd_nmonto_pago;
                                oBeDXPCtaCtbl.pdxpc_nmonto_pago_dxp = 0;
                                oBeDXPCtaCtbl.doxpc_vnumero_doc = item.lqcd_vnumero_doc;
                                oBeDXPCtaCtbl.tdocc_vabreviatura_tipo_doc = item.tip_doc_abreviatura;
                                oBeDXPCtaCtbl.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo);
                                oBeDXPCtaCtbl.ctacc_iid_cuenta_contable = Convert.ToInt32(item.lqcd_iid_cuenta_contable);
                                if (oBeDXPCtaCtbl.ctacc_iid_cuenta_contable == 0)
                                    throw new ArgumentException(String.Format("El CONCEPTO de Liquidación de Caja ingresado, NO contiene un NRO. DE CUENTA CONTABLE asignado para la creación del DOC. POR PAGAR", item.lqcd_vnumero_doc));
                                oBeDXPCtaCtbl.cdxpc_nmonto_cuenta = item.lqcd_nmonto_pago;
                                oBeDXPCtaCtbl.cdxpc_vglosa = item.lqcd_vdescripcion_movim;
                                oBeDXPCtaCtbl.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                                oBeDXPCtaCtbl.intUsuario = oBe.intUsuario;
                                oBeDXPCtaCtbl.strPc = oBe.strPc;
                                oBeDXPCtaCtbl.cdxpc_flag_estado = true; //estado del detalle
                                oBeDXPCtaCtbl.TipOper = 1;
                                ECuentaContable oBeCta = lstPlanCuentas.Where(x => x.ctacc_icod_cuenta_contable == item.lqcd_iid_cuenta_contable).ToList()[0];
                                oBeDXPCtaCtbl.anac_icod_analitica = (Convert.ToInt32(oBeCta.tablc_iid_tipo_analitica) != 0) ? Convert.ToInt32(item.lqcd_iid_analitica) : intNullVal;
                                oBeDXPCtaCtbl.cecoc_icod_centro_costo = (oBeCta.ctacc_iccosto) ? Convert.ToInt32(item.lqcd_iid_centro_costo) : intNullVal;
                                objDocumentoPorPagarData.insertarDXPDetCtaContable(oBeDXPCtaCtbl);
                                #endregion
                                #region INSERTAR EL PAGO CORRESPONDIENTE
                                /********SE DE DEBE REALIZAR EL PAGO DEL DOC. X PAGAR CREADO********************************************/
                                EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                                oBeDXPPago.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo);
                                oBeDXPPago.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                                oBeDXPPago.pdxpc_vnumero_doc = item.lq_nro_doc_pago;
                                oBeDXPPago.pdxpc_sfecha_pago = oBe.lqcc_sfecha_liquid;
                                oBeDXPPago.tablc_iid_tipo_moneda = oBe.lqcc_iid_tipo_moneda;
                                oBeDXPPago.pdxpc_nmonto_pago = item.lqcd_nmonto_pago;
                                oBeDXPPago.pdxpc_nmonto_tipo_cambio = item.lqcd_ntipo_cambio_pago;
                                oBeDXPPago.pdxpc_vobservacion = item.lqcd_vdescripcion_movim;
                                oBeDXPPago.efctc_icod_enti_financiera_cuenta = null;
                                oBeDXPPago.pdxpc_vorigen = "C";
                                oBeDXPPago.doxcc_icod_correlativo = null;
                                oBeDXPPago.ctacc_iid_cuenta_contable = null;
                                oBeDXPPago.cecoc_icod_centro_costo = null;
                                oBeDXPPago.anac_icod_analitica = null;
                                oBeDXPPago.intUsuario = item.intUsuario;
                                oBeDXPPago.strPc = item.strPc;
                                oBeDXPPago.anio = oBe.lqcc_iid_anio;
                                oBeDXPPago.pdxpc_mes = oBe.lqcc_iid_mes;
                                oBeDXPPago.pdxpc_flag_estado = true;

                                long IdPago = new CuentasPorPagarData().insertarDocPorPagarPago(oBeDXPPago); //INSERCIÓN DE PAGO DE DOC X PGR.

                                item.docxp_icod_pago = Convert.ToInt32(IdPago);
                                objTesoreriaData.insertarLiquidacionCajaDetalle(item); //INSERCIÓN DEL DET DE LA LQD.
                                objTesoreriaData.ActualizarMontoDXPPagadoSaldo(oBeDXPPago.doxpc_icod_correlativo, oBeDXPPago.tablc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                                #endregion
                            }
                        }
                        else if (item.operacion == 2)
                        {
                            objTesoreriaData.modificarLiquidacionCajaDetalle(item);
                            if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaPagoProvision)
                            {
                                #region PAGO DE PROVISION
                                EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                                oBeDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_pago);
                                oBeDXPPago.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo); //IdDocumentoPorPagar
                                oBeDXPPago.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                                oBeDXPPago.pdxpc_vnumero_doc = item.lq_nro_doc_pago;
                                oBeDXPPago.pdxpc_sfecha_pago = item.lqcd_sfecha_liquid;
                                oBeDXPPago.tablc_iid_tipo_moneda = oBe.lqcc_iid_tipo_moneda;
                                oBeDXPPago.pdxpc_nmonto_pago = item.lqcd_nmonto_pago;
                                oBeDXPPago.pdxpc_nmonto_tipo_cambio = item.lqcd_ntipo_cambio_pago;
                                oBeDXPPago.pdxpc_vobservacion = item.lqcd_vdescripcion_movim;
                                oBeDXPPago.pdxpc_vorigen = "C";
                                oBeDXPPago.intUsuario = item.intUsuario;
                                oBeDXPPago.strPc = item.strPc;
                                oBeDXPPago.anio = oBe.lqcc_iid_anio;
                                oBeDXPPago.pdxpc_mes = oBe.lqcc_iid_mes;
                                oBeDXPPago.pdxpc_flag_estado = true;
                                new CuentasPorPagarData().modificarDocPorPagarPago(oBeDXPPago);

                                objTesoreriaData.ActualizarMontoDXPPagadoSaldo(oBeDXPPago.doxpc_icod_correlativo, oBeDXPPago.tablc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                                #endregion
                            }
                            if (item.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaGenProvision)
                            {
                                /***********************************/
                                EDocPorPagar oBeDXP = crearDocPorPagarParaLiquidacioCaja(oBe, item);
                                objDocumentoPorPagarData.modificarDocPorPagar(oBeDXP);
                                /***********************************/
                                var lstDXPDet = objDocumentoPorPagarData.listarDXPDetCtaContable(Convert.ToInt64(item.docxp_icod_correlativo));
                                objDocumentoPorPagarData.eliminarDXPDetCtaContable(lstDXPDet, null);
                                /***********************************/
                                EDocPorPagarDetalleCuentaContable oBeDXPCtaCtbl = new EDocPorPagarDetalleCuentaContable();
                                oBeDXPCtaCtbl.pdxpc_nmonto_pago_dxp = item.lqcd_nmonto_pago;
                                oBeDXPCtaCtbl.pdxpc_nmonto_pago_dxp = 0;
                                oBeDXPCtaCtbl.doxpc_vnumero_doc = item.lqcd_vnumero_doc;
                                oBeDXPCtaCtbl.tdocc_vabreviatura_tipo_doc = item.tip_doc_abreviatura;
                                oBeDXPCtaCtbl.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo);
                                oBeDXPCtaCtbl.ctacc_iid_cuenta_contable = Convert.ToInt32(item.lqcd_iid_cuenta_contable);
                                if (oBeDXPCtaCtbl.ctacc_iid_cuenta_contable == 0)
                                    throw new ArgumentException(String.Format("El CONCEPTO de Liquidación de Caja ingresado, NO contiene un NRO. DE CUENTA CONTABLE asignado para la creación del DOC. POR PAGAR", item.lqcd_vnumero_doc));
                                oBeDXPCtaCtbl.cdxpc_nmonto_cuenta = item.lqcd_nmonto_pago;
                                oBeDXPCtaCtbl.cdxpc_vglosa = item.lqcd_vdescripcion_movim;
                                oBeDXPCtaCtbl.cdxpc_isituacion = 1;// (1) habilitado (2) inhabilitado-eliminado
                                oBeDXPCtaCtbl.intUsuario = oBe.intUsuario;
                                oBeDXPCtaCtbl.strPc = oBe.strPc;
                                oBeDXPCtaCtbl.cdxpc_flag_estado = true; //estado del detalle
                                oBeDXPCtaCtbl.TipOper = 1;
                                ECuentaContable oBeCta = lstPlanCuentas.Where(x => x.ctacc_icod_cuenta_contable == item.lqcd_iid_cuenta_contable).ToList()[0];
                                oBeDXPCtaCtbl.anac_icod_analitica = (Convert.ToInt32(oBeCta.tablc_iid_tipo_analitica) != 0) ? Convert.ToInt32(item.lqcd_iid_analitica) : intNullVal;
                                oBeDXPCtaCtbl.cecoc_icod_centro_costo = (oBeCta.ctacc_iccosto) ? Convert.ToInt32(item.lqcd_iid_centro_costo) : intNullVal;
                                objDocumentoPorPagarData.insertarDXPDetCtaContable(oBeDXPCtaCtbl);
                                /***********************************/
                                EDocPorPagarPago oBeDXPPago = new EDocPorPagarPago();
                                oBeDXPPago.pdxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_pago);
                                oBeDXPPago.doxpc_icod_correlativo = Convert.ToInt64(item.docxp_icod_correlativo); //IdDocumentoPorPagar
                                oBeDXPPago.tdocc_icod_tipo_doc = Parametros.intTipoDocLiquidacionCaja;
                                oBeDXPPago.pdxpc_vnumero_doc = item.lq_nro_doc_pago;
                                oBeDXPPago.pdxpc_sfecha_pago = item.lqcd_sfecha_liquid;
                                oBeDXPPago.tablc_iid_tipo_moneda = oBe.lqcc_iid_tipo_moneda;
                                oBeDXPPago.pdxpc_nmonto_pago = item.lqcd_nmonto_pago;
                                oBeDXPPago.pdxpc_nmonto_tipo_cambio = item.lqcd_ntipo_cambio_pago;
                                oBeDXPPago.pdxpc_vobservacion = item.lqcd_vdescripcion_movim;
                                oBeDXPPago.pdxpc_vorigen = "C";
                                oBeDXPPago.intUsuario = item.intUsuario;
                                oBeDXPPago.strPc = item.strPc;
                                oBeDXPPago.anio = oBe.lqcc_iid_anio;
                                oBeDXPPago.pdxpc_mes = oBe.lqcc_iid_mes;
                                oBeDXPPago.pdxpc_flag_estado = true;
                                new CuentasPorPagarData().modificarDocPorPagarPago(oBeDXPPago);
                                /***********************************/
                                objTesoreriaData.ActualizarMontoDXPPagadoSaldo(oBeDXPPago.doxpc_icod_correlativo, oBeDXPPago.tablc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                            }
                        }
                    }
                    tx.Complete();
                }
                //CrearVoucherContableLiquidacionCaja(objLiquidacion, oListLiq, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<ELiquidacionCaja> listarLiquidacionCaja(int intEjercicio, int intMes)
        {
            List<ELiquidacionCaja> lista = null;
            try
            {
                lista = (new TesoreriaData()).listarLiquidacionCaja(intEjercicio, intMes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void anularLiquidacionCaja(ELiquidacionCaja oBe)
        {
            try
            {
                objTesoreriaData.AnularLiquidacionCaja(oBe);
                var lstDet = (new TesoreriaData()).listarLiquidacionCajaDetalle(oBe.lqcc_icod_liquid_cja);
                lstDet.ForEach(x =>
                {
                    objTesoreriaData.eliminarLiquidacionCajaDetalle(x);
                    if (x.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaPagoProvision)
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objTesoreriaData.EliminarLiquidacionCajaDetalleDocXPagarPago(x);
                        objTesoreriaData.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.docxp_icod_correlativo), oBe.lqcc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                    }
                    if (x.lqcd_vtipo_movimiento == Parametros.strTipoLiqCajaGenProvision)
                    {
                        /*ELIMINAMOS EL DOC. POR PAGAR*/
                        EDocPorPagar oBeDXP = new EDocPorPagar();
                        oBeDXP.doxpc_icod_correlativo = Convert.ToInt64(x.docxp_icod_correlativo);
                        oBeDXP.intUsuario = oBe.intUsuario;
                        oBeDXP.strPc = oBe.strPc;
                        objDocumentoPorPagarData.eliminarDocPorPagar(oBeDXP);

                        var lstDXPDet = objDocumentoPorPagarData.listarDXPDetCtaContable(Convert.ToInt64(x.docxp_icod_correlativo));
                        objDocumentoPorPagarData.eliminarDXPDetCtaContable(lstDXPDet, null);

                        /*ELIMINAMOS EL PAGO REALIZADO*/
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        objTesoreriaData.EliminarLiquidacionCajaDetalleDocXPagarPago(x);
                        objTesoreriaData.ActualizarMontoDXPPagadoSaldo(Convert.ToInt64(x.docxp_icod_correlativo), oBe.lqcc_iid_tipo_moneda); //ACTUALIACIÓN DEL DOC X PGR.
                    }
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<ELiquidacionCajaDet> ListarLiquidacionCajaDetalle(int icod_liquid_caja)
        {
            List<ELiquidacionCajaDet> lista = null;
            try
            {
                lista = (new TesoreriaData()).listarLiquidacionCajaDetalle(icod_liquid_caja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int GetCorrelativoLiquidacion(int AnioEjercicio, int icod_caja_liq)
        {
            int correlativo;
            try
            {
                correlativo = (new TesoreriaData()).GetCorrelativoLiquidacion(AnioEjercicio, icod_caja_liq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return correlativo;
        }
        #endregion
        public List<EProvisionPlanillaPersonalDetalle> ListarProvisionPlanillaDetalle(int planc_icod_planilla_personal)
        {
            List<EProvisionPlanillaPersonalDetalle> lista = null;
            try
            {
                lista = (new TesoreriaData()).ListarProvisionPlanillaDetalle(planc_icod_planilla_personal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EPlanillaPersonalDetalle> listarPlanillaPersonalDetalle(int planc_icod_planilla_personal)
        {
            List<EPlanillaPersonalDetalle> lista = null;
            try
            {
                lista = new TesoreriaData().listarPlanillaPersonalDetalle(planc_icod_planilla_personal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ELibroBancos> ListarLibroBancosVCO2(int mobac_icod_correlativo)
        {
            List<ELibroBancos> lista = null;
            try
            {
                lista = new TesoreriaData().ListarLibroBancosVCO2(mobac_icod_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #region Cajas Chicas
        public List<ECajasChicas> listarCajasChicas()
        {
            List<ECajasChicas> lista = null;
            try
            {
                lista = new TesoreriaData().listarCajasChicas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarCajasChicas(ECajasChicas oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new TesoreriaData().insertarCajasChicas(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCajasChicas(ECajasChicas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().modificarCajasChicas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCajasChicas(ECajasChicas oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().eliminarCajasChicas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Cajas Correlativo
        public List<ECajaCorrelativo> listarCajasCorrelativo()
        {
            List<ECajaCorrelativo> lista = null;
            try
            {
                lista = new TesoreriaData().listarCajasCorrelativo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarCajasCorrelativo(ECajaCorrelativo oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new TesoreriaData().insertarCajasCorrelativo(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCajasCorrelativo(ECajaCorrelativo oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().modificarCajasCorrelativo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCajasCorrelativo(ECajaCorrelativo oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new TesoreriaData().eliminarCajasCorrelativo(oBe);
                    tx.Complete();
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
