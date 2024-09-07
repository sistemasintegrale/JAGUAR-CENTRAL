using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.DataAccess
{
    public class TesoreriaData
    {
        #region Bancos
        public List<EBanco> listarBancos()
        {
            List<EBanco> lista = new List<EBanco>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_BANCO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EBanco()
                        {
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcoc_iid_banco = item.bcoc_iid_banco,
                            bcoc_iid_tipo_banco = item.bcoc_iid_tipo_banco,
                            bcoc_vnombre_banco = item.bcoc_vnombre_banco,
                            bcoc_iid_situacion_banco = Convert.ToInt32(item.bcoc_iid_situacion_banco),
                            bcoc_flag_estado = Convert.ToBoolean(item.bcoc_flag_estado),
                            strSituacion = item.strSituacion,
                            bcoc_icod_pvt = Convert.ToInt32(item.bcoc_icod_pvt),
                            bcoc_flag_indicador = Convert.ToBoolean(item.bcoc_flag_indicador)
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
        public int insertarBanco(EBanco oBe)
        {
            int? intIcod = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCO_INSERTAR(
                        ref intIcod,
                        oBe.bcoc_iid_banco,
                        oBe.bcoc_iid_tipo_banco,
                        oBe.bcoc_vnombre_banco,
                        oBe.bcoc_iid_situacion_banco,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.bcoc_flag_estado,
                        oBe.bcoc_icod_pvt,
                        oBe.bcoc_flag_indicador
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCO_MODIFICAR(
                        oBe.bcoc_icod_banco,
                        oBe.bcoc_iid_banco,
                        oBe.bcoc_iid_tipo_banco,
                        oBe.bcoc_vnombre_banco,
                        oBe.bcoc_iid_situacion_banco,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.bcoc_icod_pvt,
                        oBe.bcoc_flag_indicador);
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCO_ELIMINAR(
                        oBe.bcoc_icod_banco,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Banco Cuentas
        public List<EBancoCuenta> listarBancoCuentas(int? bcoc_icod_banco)
        {
            List<EBancoCuenta> lista = new List<EBancoCuenta>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_BANCOS_CUENTAS_LISTAR(bcoc_icod_banco);
                    foreach (var item in query)
                    {
                        lista.Add(new EBancoCuenta()
                        {
                            bcod_icod_banco_cuenta = item.bcod_icod_banco_cuenta,
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcod_vnumero_cuenta = item.bcod_vnumero_cuenta,
                            tablc_iid_tipo_cuenta_ef = Convert.ToInt32(item.tablc_iid_tipo_cuenta_ef),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            bcod_iid_situacion_cuenta = Convert.ToInt32(item.bcod_iid_situacion_cuenta),
                            bcod_iicod_banco_cuenta = Convert.ToInt32(item.bcod_iicod_banco_cuenta),
                            tarec_iid_tabla_registro = item.tarec_iid_tabla_registro,
                            bcod_flag_estado = Convert.ToBoolean(item.bcod_flag_estado),
                            strTipoCuenta = item.strTipoCuenta,
                            strMoneda = item.strMoneda,
                            strSituacion = item.strSituacion,
                            anad_icod_analitica = Convert.ToInt32(item.anad_icod_analitica),
                            strCodAnalitica = item.anad_iid_analitica,
                            ctacc_icod_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            tablc_iid_tipo_analitica = item.tablc_iid_tipo_analitica,
                            strCodCCosto = item.strCodCCosto,
                            strDesCCosto = item.strDesCCosto,
                            strCodCtaContable = item.strCodCtaContable,
                            strDesCtaContable = item.strDesCtaContable,
                            strTipoAnalitica = item.strTipoAnalitica

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
        public int insertarBancoCuenta(EBancoCuenta oBe)
        {
            int? intIcod = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCOS_CUENTAS_INSERTAR(
                        ref intIcod,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_vnumero_cuenta,
                        oBe.tablc_iid_tipo_cuenta_ef,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.bcod_iid_situacion_cuenta,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.bcod_iicod_banco_cuenta,
                        oBe.tarec_iid_tabla_registro,
                        oBe.bcod_flag_estado,
                        oBe.anad_icod_analitica,
                        oBe.ctacc_icod_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.tablc_iid_tipo_analitica
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCOS_CUENTAS_MODIFICAR(
                        oBe.bcod_icod_banco_cuenta,
                        oBe.bcod_vnumero_cuenta,
                        oBe.bcod_iid_situacion_cuenta,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.ctacc_icod_cuenta_contable,
                        oBe.cecoc_icod_centro_costo,
                        oBe.tablc_iid_tipo_analitica,
                        oBe.tablc_iid_tipo_moneda);
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCOS_CUENTAS_ELIMINAR(
                        oBe.bcod_icod_banco_cuenta,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool verificarBancoCuentaMovimientos(int intCtaBancaria)
        {
            bool? flag = false;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_VERIFICAR_MOVIMIENTOS_CTA_BANCARIA(
                        intCtaBancaria,
                        ref flag
                        );
                }
                return Convert.ToBoolean(flag);
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_BANCOS_CUENTAS_SALDO_INICIAL(
                        0,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.tablc_iid_tipo_moneda,
                        oBe.bcod_monto_apertura,
                        oBe.intAnio,
                        oBe.intMes,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.bcod_fecha_apertura,
                        oBe.intMotivo,
                        oBe.intTipoDocumento
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EBancoCuenta> listarSaldoInicialBancoCuenta(int intEjercicio)
        {
            List<EBancoCuenta> lista = new List<EBancoCuenta>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_BANCOS_CUENTAS_SALDO_INICIAL_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EBancoCuenta()
                        {
                            bcod_icod_banco_cuenta = item.bcod_icod_banco_cuenta,
                            bcoc_icod_banco = item.bcoc_icod_banco,
                            bcod_vnumero_cuenta = item.bcod_vnumero_cuenta,
                            strBanco = item.bcoc_vnombre_banco,
                            tablc_iid_tipo_cuenta_ef = Convert.ToInt32(item.tablc_iid_tipo_cuenta_ef),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            bcod_iid_situacion_cuenta = Convert.ToInt32(item.bcod_iid_situacion_cuenta),
                            bcod_iicod_banco_cuenta = Convert.ToInt32(item.bcod_iicod_banco_cuenta),
                            tarec_iid_tabla_registro = item.tarec_iid_tabla_registro,
                            strMoneda = item.Moneda,
                            strSituacion = item.Situacion,
                            anad_icod_analitica = Convert.ToInt32(item.anad_icod_analitica),
                            strTipoCuenta = item.Tip,
                            strMotivo = item.strMotivo,
                            bcod_monto_apertura = item.dblApertura,
                            bcod_fecha_apertura = item.bcod_fecha_apertura
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
        public int getBancoCuenta(int intMovimientoBanco)
        {
            int? intIcod = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_GET_CTA_BANCARIA_ICOD
                        (intMovimientoBanco, ref intIcod);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Movimiento Bancos
        public List<ELibroBancos> ListarMovimientoCuentasMovimientos(DateTime FechaI, DateTime FechaF, int Periodo, int icod_cuenta)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_CUENTAS_MOVIMIENTOS(FechaI, FechaF, Periodo, icod_cuenta);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = Convert.ToInt32(item.mobac_icod_correlativo),

                            dfecha_crea = item.mobac_dfecha_movimiento,
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            Dia = Convert.ToDateTime(item.mobac_dfecha_movimiento).Day,
                            //mov = (item.Mov == 1) ? "R" : (item.Mov == 2) ? "M" : (item.Mov == 3) ? "E" : (item.Mov == 4) ? "A" : (item.Mov == 5) ? "C" : "",
                            icod_entidad_financiera = Convert.ToInt32(item.bcoc_icod_banco),
                            iid_entidad_financiera = item.bcoc_iid_banco,
                            Descripcion_Banco = item.bcoc_vnombre_banco,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            Numero_cuenta = item.efctc_vnumero_cuenta,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            iid_correlativo = Convert.ToInt32(item.bcoc_icod_banco.ToString() + item.efctc_icod_enti_financiera_cuenta.ToString()),
                            TipoDocumento = item.tdocc_vabreviatura_tipo_doc,
                            vnro_documento = item.mobac_vnro_documento,
                            documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.mobac_vnro_documento,
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            sflag_conciliacion = (Convert.ToBoolean(item.mobac_cflag_conciliacion) == true) ? "*" : "",
                            vglosa = item.mobac_vglosa,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            TipoMoneda = item.moneda_vdescripcion,
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            nmonto_movimiento = Convert.ToDecimal(item.mobac_nmonto_movimiento),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.mobac_nmonto_tipo_cambio),
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            iid_situacion_movimiento_banco = Convert.ToInt32(item.tablc_iid_situacion_movimiento_banco),
                            Situacion = item.situacion_vdescripcion,
                            iid_motivo_mov_banco = Convert.ToInt32(item.tablc_iid_motivo_mov_banco),
                            MotivoBanco = item.motivo_vdescripcion,
                            inumero_orden = item.mobac_inumero_orden,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,
                            vnro_retencion = item.retencion_vnumero
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

        public List<ELibroBancos> ListarMovimientoCuentasDetalle(DateTime FechaI, DateTime FechaF, int Periodo)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_CUENTAS_DETALLE(FechaI, FechaF, Periodo);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = Convert.ToInt32(item.mobac_icod_correlativo),

                            dfecha_crea = item.mobac_dfecha_movimiento,
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            Dia = Convert.ToDateTime(item.mobac_dfecha_movimiento).Day,
                            //mov = (item.Mov == 1) ? "R" : (item.Mov == 2) ? "M" : (item.Mov == 3) ? "E" : (item.Mov == 4) ? "A" : (item.Mov == 5) ? "C" : "",
                            icod_entidad_financiera = Convert.ToInt32(item.bcoc_icod_banco),
                            iid_entidad_financiera = item.bcoc_iid_banco,
                            Descripcion_Banco = item.bcoc_vnombre_banco,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            Numero_cuenta = item.efctc_vnumero_cuenta,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            iid_correlativo = Convert.ToInt32(item.bcoc_icod_banco.ToString() + item.efctc_icod_enti_financiera_cuenta.ToString()),
                            TipoDocumento = item.tdocc_vabreviatura_tipo_doc,
                            vnro_documento = item.mobac_vnro_documento,
                            documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.mobac_vnro_documento,
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            sflag_conciliacion = (Convert.ToBoolean(item.mobac_cflag_conciliacion) == true) ? "*" : "",
                            vglosa = item.mobac_vglosa,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            TipoMoneda = item.moneda_vdescripcion,
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            nmonto_movimiento = Convert.ToDecimal(item.mobac_nmonto_movimiento),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.mobac_nmonto_tipo_cambio),
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            iid_situacion_movimiento_banco = Convert.ToInt32(item.tablc_iid_situacion_movimiento_banco),
                            Situacion = item.situacion_vdescripcion,
                            iid_motivo_mov_banco = Convert.ToInt32(item.tablc_iid_motivo_mov_banco),
                            MotivoBanco = item.motivo_vdescripcion,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,
                            vnro_retencion = item.retencion_vnumero
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
        public List<EEntidadFinancieraCuenta> ListarResumenMovimientoCuentas(DateTime FechaInicio, DateTime FechaFinal, int Periodo)
        {
            List<EEntidadFinancieraCuenta> lista = new List<EEntidadFinancieraCuenta>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_CUENTAS_RESUMEN(FechaInicio, FechaFinal, Periodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EEntidadFinancieraCuenta()
                        {
                            id_entidad_financiera = Convert.ToInt32(item.temp_efinc_icod_entidad_financiera),
                            vdes_entidad_financiera = item.temp_efinc_vnombre_entidad_financiera,
                            id_entidad_Financiera_cuenta = Convert.ToInt32(item.temp_efctc_icod_enti_financiera_cuenta),
                            Descripcion = item.temp_efctc_vnumero_cuenta,
                            iid_tipo_moneda = Convert.ToInt32(item.temp_tablc_iid_tipo_moneda),
                            Moneda = item.temp_vdes_moneda,
                            CuentasMoneda = (Convert.ToInt32(item.temp_tablc_iid_tipo_moneda) == 1) ? "CUENTA M.N." : "CUENTA M.E.",
                            mto_saldo_anterior = Convert.ToDecimal(item.temp_nmonto_saldo_anterior),
                            mto_abono_acumulado = Convert.ToDecimal(item.temp_nmonto_abono),
                            mto_cargo_acumulado = Convert.ToDecimal(item.temp_nmonto_cargo),
                            mto_saldo_libro = Convert.ToDecimal(item.temp_nmonto_saldo_libro),
                            mto_saldo_disponible = Convert.ToDecimal(item.temp_nmonto_saldo_disponible)
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
        public List<ELibroBancos> ListarMovimientoCuentasCheques(int Periodo, int IdCuenta)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_MOV_CHEQUES(Periodo, IdCuenta);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = Convert.ToInt32(item.mobac_icod_correlativo),
                            dfecha_crea = item.mobac_dfecha_movimiento,
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            Mes = item.mes,
                            Dia = Convert.ToDateTime(item.mobac_dfecha_movimiento).Day,
                            mov = (item.Mov == 1) ? "R" : (item.Mov == 2) ? "M" : (item.Mov == 3) ? "E" : (item.Mov == 4) ? "A" : (item.Mov == 5) ? "C" : "",
                            icod_entidad_financiera = Convert.ToInt32(item.bcoc_icod_banco),
                            iid_entidad_financiera = item.bcoc_iid_banco,
                            Descripcion_Banco = item.bcoc_vnombre_banco,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            Numero_cuenta = item.efctc_vnumero_cuenta,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            TipoDocumento = item.tdocc_vabreviatura_tipo_doc,
                            vnro_documento = item.mobac_vnro_documento,
                            documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.mobac_vnro_documento,
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            sflag_conciliacion = (Convert.ToBoolean(item.mobac_cflag_conciliacion) == true) ? "*" : "",
                            vglosa = item.mobac_vglosa,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            TipoMoneda = item.moneda_vdescripcion,
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            nmonto_movimiento = Convert.ToDecimal(item.mobac_nmonto_movimiento),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.mobac_nmonto_tipo_cambio),
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            iid_situacion_movimiento_banco = Convert.ToInt32(item.tablc_iid_situacion_movimiento_banco),
                            Situacion = item.situacion_vdescripcion,
                            iid_motivo_mov_banco = Convert.ToInt32(item.tablc_iid_motivo_mov_banco),
                            MotivoBanco = item.motivo_vdescripcion,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,
                            vnro_retencion = item.retencion_vnumero
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
        public List<ELibroBancos> ListarEstadoCuenta(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ESTADO_CUENTAS_REPORTE(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = Convert.ToInt32(item.mobac_icod_correlativo),
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            tdocc_vdescripcion = item.tdocc_vdescripcion,
                            vnro_documento = item.mobac_vnro_documento,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            vglosa = item.mobac_vglosa,
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            Abono = item.mobac_nmonto_movimiento_abono,
                            Cargo = item.mobac_nmonto_movimiento_cargo,
                            mobac_nmonto_saldo = item.mobac_nmonto_saldo,
                            mobac_nmonto_disponible = item.mobac_nmonto_disponible,
                            mobac_nmonto_abono_ant = item.mobac_nmonto_abono_ant,
                            mobac_nmonto_cargo_ant = item.mobac_nmonto_cargo_ant,
                            mobac_nmonto_saldo_ant = item.mobac_nmonto_saldo_ant,
                            mobac_nmonto_disponible_ant = item.mobac_nmonto_disponible_ant,
                            Dia = Convert.ToDateTime(item.mobac_dfecha_movimiento).Day,
                            sflag_conciliacion = (item.mobac_cflag_conciliacion == true) ? "*" : "",
                            nmonto_movimiento = (item.mobac_nmonto_movimiento_abono == null) ? Convert.ToDecimal(item.mobac_nmonto_movimiento_cargo) : Convert.ToDecimal(item.mobac_nmonto_movimiento_abono)
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

        public List<ELibroBancos> listarMovimientosSinConciliar(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_CONCILIACION_LISTAR(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = Convert.ToInt32(item.mobac_icod_correlativo),
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            tdocc_vdescripcion = item.tdocc_vabreviatura_tipo_doc + " - " + item.mobac_vnro_documento,
                            vnro_documento = item.mobac_vnro_documento,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            vglosa = item.mobac_vglosa,
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            nmonto_movimiento = Convert.ToDecimal(item.mobac_nmonto_movimiento),
                            Abono = item.Abono,
                            Cargo = item.Cargo
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
        public List<ELibroBancos> ListarLibroBancos(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = item.mobac_icod_correlativo,
                            Dia = item.dia,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            TipoDocumento = item.TipoDocumento,
                            TipoDocAbreviado = item.TipoDocAbrev,
                            vnro_documento = item.mobac_vnro_documento,
                            cflag_conciliacion = item.mobac_cflag_conciliacion,
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            vglosa = item.mobac_vglosa,
                            iid_situacion_movimiento_banco = item.tablc_iid_situacion_movimiento_banco,
                            Situacion = item.Situacion,
                            iid_motivo_mov_banco = item.tablc_iid_motivo_mov_banco,
                            MotivoBanco = item.MotivoBanco,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,

                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            iid_correlativo = item.mobac_iid_correlativo,
                            iid_anio = item.anioc_iid_anio,
                            iid_mes = item.mesec_iid_mes,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            sfecha_cheque = Convert.ToDateTime(item.mobac_sfecha_cheque),
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            nmonto_tipo_cambio = item.mobac_nmonto_tipo_cambio,
                            nmonto_movimiento = item.mobac_nmonto_movimiento,
                            nmonto_saldo_banco = item.mobac_nmonto_saldo_banco,
                            inumero_orden = item.mobac_inumero_orden,
                            iid_origen_mov_banco = Convert.ToInt32(item.tablc_iid_origen_mov_banco),

                            SaldoAnterior = item.SaldoAnterior,
                            SaldoLibro = item.SaldoLibro,
                            SaldoDisponible = item.SaldoDisponible,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            anac_icod_analitica_cliente = Convert.ToInt32(item.anac_icod_analitica_cliente),
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            sflag_conciliacion = (item.mobac_cflag_conciliacion == true) ? "*" : "",
                            cflag_pase = Convert.ToBoolean(item.mobac_cflag_pase),
                            id_transferencia = item.id_transferencia,
                            mobac_sfecha_diferida = item.mobac_sfecha_diferida,
                            mobac_origen_regitro = item.mobac_origen_regitro
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

        public List<ELibroBancos> ListarLibroBancosCompras(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_COMPRAS(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = item.mobac_icod_correlativo,
                            Dia = item.dia,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            TipoDocumento = item.TipoDocumento,
                            TipoDocAbreviado = item.TipoDocAbrev,
                            vnro_documento = item.mobac_vnro_documento,
                            cflag_conciliacion = item.mobac_cflag_conciliacion,
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            vglosa = item.mobac_vglosa,
                            iid_situacion_movimiento_banco = item.tablc_iid_situacion_movimiento_banco,
                            Situacion = item.Situacion,
                            iid_motivo_mov_banco = item.tablc_iid_motivo_mov_banco,
                            MotivoBanco = item.MotivoBanco,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,

                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            iid_correlativo = item.mobac_iid_correlativo,
                            iid_anio = item.anioc_iid_anio,
                            iid_mes = item.mesec_iid_mes,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            sfecha_cheque = Convert.ToDateTime(item.mobac_sfecha_cheque),
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            nmonto_tipo_cambio = item.mobac_nmonto_tipo_cambio,
                            nmonto_movimiento = item.mobac_nmonto_movimiento,
                            nmonto_saldo_banco = item.mobac_nmonto_saldo_banco,
                            inumero_orden = item.mobac_inumero_orden,
                            iid_origen_mov_banco = Convert.ToInt32(item.tablc_iid_origen_mov_banco),

                            SaldoAnterior = item.SaldoAnterior,
                            SaldoLibro = item.SaldoLibro,
                            SaldoDisponible = item.SaldoDisponible,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            anac_icod_analitica_cliente = Convert.ToInt32(item.anac_icod_analitica_cliente),
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            sflag_conciliacion = (item.mobac_cflag_conciliacion == true) ? "*" : "",
                            cflag_pase = Convert.ToBoolean(item.mobac_cflag_pase),
                            id_transferencia = item.id_transferencia,
                            mobac_sfecha_diferida = item.mobac_sfecha_diferida,
                            mobac_origen_regitro = item.mobac_origen_regitro
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

        public List<ELibroBancos> ListarLibroBancosVCO(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_VCO(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = item.mobac_icod_correlativo,
                            Dia = item.dia,
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            TipoDocumento = item.TipoDocumento,
                            TipoDocAbreviado = item.TipoDocAbrev,
                            vnro_documento = item.mobac_vnro_documento,
                            cflag_conciliacion = item.mobac_cflag_conciliacion,
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            vglosa = item.mobac_vglosa,
                            iid_situacion_movimiento_banco = item.tablc_iid_situacion_movimiento_banco,
                            Situacion = item.Situacion,
                            iid_motivo_mov_banco = item.tablc_iid_motivo_mov_banco,
                            MotivoBanco = item.MotivoBanco,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,
                            //vnro_retencion = item.mobac_icod_comprobante_retencion,
                            //vnro_deposito_detraccion = item.mobac_vnro_deposito_detraccion,
                            //vnro_letra_rechazada = item.mobac_vnro_letra_rechazada,
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            iid_correlativo = item.mobac_iid_correlativo,
                            iid_anio = item.anioc_iid_anio,
                            iid_mes = item.mesec_iid_mes,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            sfecha_cheque = Convert.ToDateTime(item.mobac_sfecha_cheque),
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            nmonto_tipo_cambio = item.mobac_nmonto_tipo_cambio,
                            nmonto_movimiento = item.mobac_nmonto_movimiento,
                            nmonto_saldo_banco = item.mobac_nmonto_saldo_banco,
                            inumero_orden = item.mobac_inumero_orden,
                            iid_origen_mov_banco = Convert.ToInt32(item.tablc_iid_origen_mov_banco),
                            //icod_comprobante_retencion = Convert.ToInt32(item.mobac_icod_comprobante_retencion),
                            SaldoAnterior = item.SaldoAnterior,
                            SaldoLibro = item.SaldoLibro,
                            SaldoDisponible = item.SaldoDisponible,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            anac_icod_analitica_cliente = Convert.ToInt32(item.anac_icod_analitica_cliente),
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            sflag_conciliacion = (item.mobac_cflag_conciliacion == true) ? "*" : "",
                            sfecha_crea = Convert.ToDateTime(item.mobac_sfecha_crea),
                            cflag_pase = Convert.ToBoolean(item.mobac_cflag_pase),
                            id_transferencia = item.id_transferencia,
                            mobac_sfecha_diferida = item.mobac_sfecha_diferida,
                            mobac_origen_regitro = item.mobac_origen_regitro
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
        public List<ELibroBancos> ListarLibroBancosVCO2(int mobac_icod_correlativo)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_VCO2(mobac_icod_correlativo);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            icod_correlativo = item.mobac_icod_correlativo,
                            Dia = Convert.ToInt32(item.Dia),
                            ii_tipo_doc = item.tdocc_iid_tipo_doc,
                            TipoDocumento = item.TipoDocumento,
                            TipoDocAbreviado = item.TipoDocAbrev,
                            vnro_documento = item.mobac_vnro_documento,
                            cflag_conciliacion = item.mobac_cflag_conciliacion,
                            Abono = item.Abono,
                            Cargo = item.Cargo,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            vglosa = item.mobac_vglosa,
                            iid_situacion_movimiento_banco = item.tablc_iid_situacion_movimiento_banco,
                            Situacion = item.Situacion,
                            iid_motivo_mov_banco = item.tablc_iid_motivo_mov_banco,
                            MotivoBanco = item.MotivoBanco,
                            vnro_planilla_liquidacion = item.mobac_vnro_planilla_liquidacion,
                            //vnro_retencion = item.mobac_icod_comprobante_retencion,
                            //vnro_deposito_detraccion = item.mobac_vnro_deposito_detraccion,
                            //vnro_letra_rechazada = item.mobac_vnro_letra_rechazada,
                            dfecha_movimiento = item.mobac_dfecha_movimiento,
                            iid_correlativo = item.mobac_iid_correlativo,
                            iid_anio = item.anioc_iid_anio,
                            iid_mes = item.mesec_iid_mes,
                            icod_enti_financiera_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            sfecha_cheque = Convert.ToDateTime(item.mobac_sfecha_cheque),
                            cflag_tipo_movimiento = item.mobac_cflag_tipo_movimiento.ToString(),
                            iid_tipo_moneda = item.tablc_iid_tipo_moneda,
                            nmonto_tipo_cambio = item.mobac_nmonto_tipo_cambio,
                            nmonto_movimiento = item.mobac_nmonto_movimiento,
                            nmonto_saldo_banco = item.mobac_nmonto_saldo_banco,
                            inumero_orden = item.mobac_inumero_orden,
                            iid_origen_mov_banco = Convert.ToInt32(item.tablc_iid_origen_mov_banco),
                            //icod_comprobante_retencion = Convert.ToInt32(item.mobac_icod_comprobante_retencion),
                            //SaldoAnterior = item.SaldoAnterior,
                            SaldoLibro = item.SaldoLibro,
                            //SaldoDisponible = item.SaldoDisponible,
                            anac_icod_analitica = Convert.ToInt32(item.anac_icod_analitica),
                            proc_icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            anac_icod_analitica_cliente = Convert.ToInt32(item.anac_icod_analitica_cliente),
                            cliec_icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            cliec_vnombre_cliente = item.cliec_vnombre_cliente,
                            sflag_conciliacion = (item.mobac_cflag_conciliacion == true) ? "*" : "",
                            sfecha_crea = Convert.ToDateTime(item.mobac_sfecha_crea),
                            cflag_pase = Convert.ToBoolean(item.mobac_cflag_pase),
                            id_transferencia = item.id_transferencia,
                            mobac_sfecha_diferida = item.mobac_sfecha_diferida,
                            mobac_origen_regitro = item.mobac_origen_regitro
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
        public List<ELibroBancos> ListarLibroBancosSaldoAnterior(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_SALDO_ANTERIOR(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            SaldoAnterior = Convert.ToDecimal(item.SaldoAnterior)
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
        public List<ELibroBancos> ListarLibroBancosSaldoDisponible(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_SALDO_DISPONIBLE(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            SaldoDisponible = Convert.ToDecimal(item.SaldoDisponible)
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

        public List<ELibroBancos> ListarLibroBancosSaldoAnteriorCompras(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_SALDO_ANTERIOR_COMPRAS(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            SaldoAnterior = Convert.ToDecimal(item.SaldoAnterior)
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
        public List<ELibroBancos> ListarLibroBancosSaldoDisponibleCompras(int Periodo, int Mes, int IdCuentaBancaria)
        {
            List<ELibroBancos> lista = new List<ELibroBancos>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIBRO_BANCOS_LISTAR_SALDO_DISPONIBLE_COMPRAS(Periodo, Mes, IdCuentaBancaria);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancos()
                        {
                            SaldoDisponible = Convert.ToDecimal(item.SaldoDisponible)
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

        public List<ELibroBancosDetalle> ListarEntidadFinancieraDetalle(int intIcod)
        {
            List<ELibroBancosDetalle> lista = new List<ELibroBancosDetalle>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_DETALLE_LISTAR(intIcod);
                    foreach (var item in query)
                    {

                        ELibroBancosDetalle _BE = new ELibroBancosDetalle();
                        _BE.icod_correlativo = item.mobdc_icod_correlativo;
                        _BE.icod_correlativo_cabecera = Convert.ToInt32(item.mobac_icod_correlativo);
                        _BE.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                        _BE.tdodc_iid_correlativo = item.tdodc_iid_correlativo_clase;
                        _BE.tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc;
                        _BE.vnumero_doc = item.mobac_vnumero_doc;
                        _BE.doxcc_sfecha_doc = item.doxcc_sfecha_doc;
                        _BE.doxpc_sfecha_doc = item.doxpc_sfecha_doc;
                        _BE.mobdc_icod_proveedor = item.mobdc_icod_proveedor;
                        _BE.mobdc_icod_cliente = item.mobdc_icod_cliente;
                        _BE.tablc_icod_tipo_analitica = item.tablc_icod_tipo_analitica;
                        _BE.tarec_vdescripcion = item.tarec_vdescripcion;
                        _BE.icod_analitica = item.anac_icod_analitica;
                        _BE.anac_iid_analitica = item.anad_iid_analitica;
                        _BE.anac_vdescripcion = item.anad_vdescripcion;
                        _BE.iid_cuenta_contable = item.ctacc_iid_cuenta_contable;
                        _BE.NumeroCuentaContable = item.ctacc_vnumero_cuenta_contable;
                        _BE.DescripcionCuentaContable = item.ctacc_vnombre_descripcion_larga;
                        _BE.mto_mov_soles = Convert.ToDecimal(item.mobdc_mto_mov_soles);
                        _BE.mto_mov_dolar = Convert.ToDecimal(item.mobdc_mto_mov_dolar);
                        _BE.mto_retenido_soles = Convert.ToDecimal(item.mobdc_mto_retenido_soles);
                        _BE.mto_retenido_dolar = Convert.ToDecimal(item.mobdc_mto_retenido_dolar);
                        _BE.mto_detalle_soles = Convert.ToDecimal(item.mobdc_mto_detalle_soles);
                        _BE.mto_detalle_dolar = Convert.ToDecimal(item.mobdc_mto_detalle_dolar);
                        _BE.vglosa = item.mobdc_vglosa;
                        _BE.icod_centro_costo = item.cecoc_icod_centro_costo;
                        _BE.CodigoCentroCosto = item.cecoc_ccodigo_centro_costo;
                        _BE.DescripcionCentroCosto = item.cecoc_vdescripcion;

                        _BE.docxp_icod_pago = item.pdxpc_icod_correlativo;
                        _BE.docxc_icod_pago = item.pdxcc_icod_correlativo;

                        _BE.adclie_icod_pago = item.adpac_icod_correlativo;
                        _BE.ncclie_icod_pago = item.ncpac_icod_correlativo;
                        _BE.adprov_icod_pago = item.adpap_icod_correlativo;
                        _BE.ncprov_icod_pago = item.ncpap_icod_correlativo;
                        _BE.doxpc_icod_correlativo = Convert.ToInt64(item.doxpc_icod_correlativo);
                        _BE.doxcc_icod_correlativo = Convert.ToInt64(item.doxcc_icod_correlativo);
                        _BE.docxp_nmonto_total_documento = Convert.ToDecimal(item.doxpc_nmonto_total_documento);
                        _BE.docxc_nmonto_total_documento = Convert.ToDecimal(item.doxcc_nmonto_total);
                        _BE.mobdc_vcta_bco_nacion = item.mobdc_vcta_bco_nacion;
                        _BE.mobdc_iid_anio = item.mobdc_iid_anio;
                        _BE.mobdc_iid_mes_periodo = item.mobdc_iid_mes_periodo;
                        _BE.vdes_analisis =
                        (item.tablc_icod_tipo_analitica != null) ? string.Format("{0:00}", item.tablc_icod_tipo_analitica) + "." + item.anad_iid_analitica : "";
                        _BE.mnto = (Convert.ToDecimal(item.mobdc_mto_mov_soles) != 0) ? Convert.ToDecimal(item.mobdc_mto_mov_soles) : Convert.ToDecimal(item.mobdc_mto_mov_dolar);
                        _BE.MonedaDXC = Convert.ToInt32(item.MonedaDXC);
                        _BE.MonedaDXP = Convert.ToInt32(item.MonedaDXP);
                        _BE.mobdc_sfecha = Convert.ToDateTime(item.mobdc_sfecha);
                        _BE.mobdc_icod_concepto_caja = Convert.ToInt32(item.mobdc_icod_concepto_caja);
                        _BE.concepto_abreviatura = item.concepto_abreviatura;
                        lista.Add(_BE);
                    }
                }
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_DETALLE_LISTAR_ADP_NCP(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancosDetalle()
                        {
                            icod_correlativo = item.mobdc_icod_correlativo,
                            icod_correlativo_cabecera = Convert.ToInt32(item.mobac_icod_correlativo),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo_clase,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            vnumero_doc = item.mobac_vnumero_doc,
                            doxpc_sfecha_doc = (item.ncre_sfecha_doc == null) ? item.adpr_sfecha_adelanto : item.ncre_sfecha_doc,
                            mobdc_icod_proveedor = item.mobdc_icod_proveedor,
                            mobdc_icod_cliente = item.mobdc_icod_cliente,
                            tablc_icod_tipo_analitica = item.tablc_icod_tipo_analitica,
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            icod_analitica = item.anac_icod_analitica,
                            anac_iid_analitica = item.anac_iid_analitica,
                            anac_vdescripcion = item.anac_vdescripcion,
                            iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            NumeroCuentaContable = item.ctacc_vnumero_cuenta_contable,
                            DescripcionCuentaContable = item.ctacc_vnombre_descripcion_larga,
                            mto_mov_soles = Convert.ToDecimal(item.mobdc_mto_mov_soles),
                            mto_mov_dolar = Convert.ToDecimal(item.mobdc_mto_mov_dolar),
                            mto_retenido_soles = Convert.ToDecimal(item.mobdc_mto_retenido_soles),
                            mto_retenido_dolar = Convert.ToDecimal(item.mobdc_mto_retenido_dolar),
                            mto_detalle_soles = Convert.ToDecimal(item.mobdc_mto_detalle_soles),
                            mto_detalle_dolar = Convert.ToDecimal(item.mobdc_mto_detalle_dolar),
                            vglosa = item.mobdc_vglosa,
                            icod_centro_costo = item.cecoc_icod_centro_costo,
                            CodigoCentroCosto = item.cecoc_ccodigo_centro_costo,
                            DescripcionCentroCosto = item.cecoc_vdescripcion,
                            docxp_icod_pago = item.pdxpc_icod_correlativo,
                            docxc_icod_pago = item.pdxcc_icod_correlativo,
                            adclie_icod_pago = item.adpac_icod_correlativo,
                            ncclie_icod_pago = item.ncpac_icod_correlativo,
                            adprov_icod_pago = item.adpap_icod_correlativo,
                            ncprov_icod_pago = item.ncpap_icod_correlativo,
                            doxpc_icod_correlativo = (item.doxpc_icod_correlativo_adp == null) ? item.doxpc_icod_correlativo_ncp : item.doxpc_icod_correlativo_adp,
                            mobdc_vcta_bco_nacion = item.mobdc_vcta_bco_nacion,
                            //mobdc_iid_tab_sunat_det_bien = item.mobdc_iid_tab_sunat_det_bien,
                            //mobdc_iid_tab_sunat_det_oper = item.mobdc_iid_tab_sunat_det_oper,
                            mobdc_iid_anio = item.mobdc_iid_anio,
                            mobdc_iid_mes_periodo = item.mobdc_iid_mes_periodo,
                            vdes_analisis =
                            (item.tablc_icod_tipo_analitica != null) ? string.Format("{0:00}", item.tablc_icod_tipo_analitica) + "." + item.anac_iid_analitica : "",
                            mnto = (Convert.ToDecimal(item.mobdc_mto_mov_soles) != 0) ? Convert.ToDecimal(item.mobdc_mto_mov_soles) : Convert.ToDecimal(item.mobdc_mto_mov_dolar)
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
        public List<ELibroBancosDetalle> ListarEntidadFinancieraDetalleADPNCP_Cliente(int Code)
        {
            List<ELibroBancosDetalle> lista = new List<ELibroBancosDetalle>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ENTIDAD_FINANCIERA_DETALLE_LISTAR_ADP_NCP_CLIE(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new ELibroBancosDetalle()
                        {
                            icod_correlativo = item.mobdc_icod_correlativo,
                            icod_correlativo_cabecera = Convert.ToInt32(item.mobac_icod_correlativo),
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdodc_iid_correlativo = item.tdodc_iid_correlativo_clase,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            vnumero_doc = item.mobac_vnumero_doc,
                            doxcc_sfecha_doc = (item.ncrec_sfecha_documento == null) ? item.adci_sfecha_adelanto : item.ncrec_sfecha_documento,
                            mobdc_icod_proveedor = item.mobdc_icod_proveedor,
                            mobdc_icod_cliente = item.mobdc_icod_cliente,
                            tablc_icod_tipo_analitica = item.tablc_icod_tipo_analitica,
                            tarec_vdescripcion = item.tarec_vdescripcion,
                            icod_analitica = item.anac_icod_analitica,
                            anac_iid_analitica = item.anac_iid_analitica,
                            anac_vdescripcion = item.anac_vdescripcion,
                            iid_cuenta_contable = item.ctacc_iid_cuenta_contable,
                            NumeroCuentaContable = item.ctacc_vnumero_cuenta_contable,
                            DescripcionCuentaContable = item.ctacc_vnombre_descripcion_larga,
                            mto_mov_soles = Convert.ToDecimal(item.mobdc_mto_mov_soles),
                            mto_mov_dolar = Convert.ToDecimal(item.mobdc_mto_mov_dolar),
                            mto_retenido_soles = Convert.ToDecimal(item.mobdc_mto_retenido_soles),
                            mto_retenido_dolar = Convert.ToDecimal(item.mobdc_mto_retenido_dolar),
                            mto_detalle_soles = Convert.ToDecimal(item.mobdc_mto_detalle_soles),
                            mto_detalle_dolar = Convert.ToDecimal(item.mobdc_mto_detalle_dolar),
                            vglosa = item.mobdc_vglosa,
                            icod_centro_costo = item.cecoc_icod_centro_costo,
                            CodigoCentroCosto = item.cecoc_ccodigo_centro_costo,
                            DescripcionCentroCosto = item.cecoc_vdescripcion,
                            docxp_icod_pago = item.pdxpc_icod_correlativo,
                            docxc_icod_pago = item.pdxcc_icod_correlativo,
                            adclie_icod_pago = item.adpac_icod_correlativo,

                            ncclie_icod_pago = item.ncpac_icod_correlativo,
                            adprov_icod_pago = item.adpap_icod_correlativo,
                            ncprov_icod_pago = item.ncpap_icod_correlativo,
                            doxcc_icod_correlativo = (item.doxcc_icod_correlativo_adc == null) ? item.doxcc_icod_correlativo_ncc : item.doxcc_icod_correlativo_adc,
                            docxc_icod_documento = (item.adci_icod_correlativo == null) ? item.ncrec_icod_credito : item.adci_icod_correlativo,
                            docxc_nmonto_total_documento = (item.adci_nmonto_adelanto == null) ? Convert.ToDecimal(item.ncrec_nmonto_total) : Convert.ToDecimal(item.adci_nmonto_adelanto),
                            mobdc_vcta_bco_nacion = item.mobdc_vcta_bco_nacion,

                            mobdc_iid_anio = item.mobdc_iid_anio,
                            mobdc_iid_mes_periodo = item.mobdc_iid_mes_periodo,
                            vdes_analisis =
                            (item.tablc_icod_tipo_analitica != null) ? string.Format("{0:00}", item.tablc_icod_tipo_analitica) + "." + item.anac_iid_analitica : "",
                            mnto = (Convert.ToDecimal(item.mobdc_mto_mov_soles) != 0) ? Convert.ToDecimal(item.mobdc_mto_mov_soles) : Convert.ToDecimal(item.mobdc_mto_mov_dolar)
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
        public void ConciliarLibroBancos(int IdLibroBanco, bool FlagConcilia, int IdUsuario, string PC)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_LIBRO_BANCOS_CONCILIAR(IdLibroBanco, FlagConcilia, IdUsuario, PC);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void transferenciaID(int id_1, int id_2)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_ENTIDAD_FINANCIERA_TRANSFERENCIA(id_1, id_2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EBancoCuenta> listarTransferencia(int intMov)
        {
            List<EBancoCuenta> lista = new List<EBancoCuenta>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ENTIDAD_FINANCIERA_CUENTA_BANCO(intMov);
                    foreach (var item in query)
                    {
                        lista.Add(new EBancoCuenta()
                        {
                            bcod_icod_banco_cuenta = item.efctc_icod_enti_financiera_cuenta,
                            bcoc_icod_banco = item.bcoc_icod_banco
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

        public int InsertarMovimientoBancos(ELibroBancos objCab)
        {
            int IdComprobante = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    int? IntId = 0;
                    dc.SGET_ENTIDAD_FINANCIERA_MOVIMIENTOS_INSERTAR(
                                     ref IntId,
                                     objCab.iid_anio,
                                     objCab.iid_mes,
                                     objCab.icod_enti_financiera_cuenta,
                                     objCab.ii_tipo_doc,
                                     Convert.ToChar(objCab.cflag_tipo_movimiento),
                                     objCab.iid_motivo_mov_banco,
                                     objCab.vdescripcion_beneficiario,
                                     objCab.iid_tipo_moneda,
                                     objCab.nmonto_tipo_cambio,
                                     objCab.nmonto_movimiento,
                                     objCab.cflag_conciliacion,
                                     objCab.nmonto_saldo_banco,
                                     objCab.iid_situacion_movimiento_banco,
                                     objCab.inumero_orden,
                                     objCab.iid_origen_mov_banco,
                                     objCab.vnro_planilla_liquidacion,
                                     objCab.iusuario_crea,
                                     objCab.vpc_crea,
                                     objCab.dfecha_movimiento,
                                     objCab.vnro_documento,
                                     objCab.vglosa,
                                     objCab.mobac_flag_estado,
                                     objCab.proc_icod_proveedor,
                                     objCab.cliec_icod_cliente,
                                     objCab.cflag_pase,
                                     objCab.mobac_sfecha_diferida,
                                     objCab.mobac_origen_regitro
                                     );
                    IdComprobante = Convert.ToInt32(IntId);
                }
                return IdComprobante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertarLibroBancosDetalle(ELibroBancosDetalle ob)
        {
            try
            {

                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    int? Id = 0;
                    dc.SGET_ENTIDAD_FINANCIERA_MOVIMIENTO_DETALLE_INSERTAR(
                         ref Id,
                         ob.icod_correlativo_cabecera,
                         ob.tdocc_icod_tipo_doc,
                         ob.tdodc_iid_correlativo,
                         ob.vnumero_doc,
                         ob.mobdc_icod_proveedor,
                         ob.mobdc_icod_cliente,
                         ob.iid_cuenta_contable,
                         ob.tablc_icod_tipo_analitica,
                         ob.icod_analitica,
                         ob.icod_centro_costo,
                         ob.mto_mov_soles,
                         ob.mto_mov_dolar,
                         ob.mto_retenido_soles,
                         ob.mto_retenido_dolar,
                         ob.mto_detalle_soles,
                         ob.mto_detalle_dolar,
                         ob.vglosa,
                         ob.docxc_icod_pago,
                         ob.adclie_icod_pago,
                         ob.ncclie_icod_pago,
                         ob.docxp_icod_pago,
                         ob.adprov_icod_pago,
                         ob.ncprov_icod_pago,
                         ob.mobdc_vcta_bco_nacion,
                         ob.mobdc_iid_anio,
                         ob.mobdc_iid_mes_periodo,
                         ob.iusuario_crea,
                         ob.vpc_crea,
                         ob.mobdc_flag_estado,
                         ob.mobdc_sfecha,
                         ob.mobdc_icod_concepto_caja
                     );
                    return Convert.ToInt32(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarMontoDXPPagadoSaldo(long IdDocumentoPorPagar, int IdTipoMoneda)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_DOC_X_PAGAR_ACTUALIZAR_MONTO_PAGADO_SALDO(IdDocumentoPorPagar, IdTipoMoneda);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMontoDXCPagadoSaldo(long IdDocumentoPorCobrar, int IdTipoMoneda)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_DOC_X_COBRAR_ACTUALIZAR_MONTO_PAGADO_SALDO(
                        IdDocumentoPorCobrar,
                        IdTipoMoneda
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMontoPagadoAdelantoCliente(long IdDocumentoPorCobrar, int IdTipoMoneda)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_DOC_X_COBRAR_ACTUALIZAR_MONTO_PAGADO_ADELANTOS(IdDocumentoPorCobrar, IdTipoMoneda);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMontoPagadoSaldoNotaCreditoCliente(long IdDocumentoPorPagar, int IdTipoMoneda)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_DOC_X_COBRAR_ACTUALIZAR_MONTO_PAGADO_SALDO_NOTA_CREDITO(IdDocumentoPorPagar, IdTipoMoneda);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMontoPagadoSaldoAdelantoProveedor(long IdDocumentoPorPagar, int IdTipoMoneda)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_DOC_X_PAGAR_ACTUALIZAR_MONTO_PAGADO_SALDO_ADELANTOS(IdDocumentoPorPagar, IdTipoMoneda);
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ActualizarMontoPagadoSaldoNotaCreditoProveedor(long IdDocumentoPorPagar, int IdTipoMoneda)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_DOC_X_PAGAR_ACTUALIZAR_MONTO_PAGADO_SALDO_NOTA_CREDITO(IdDocumentoPorPagar, IdTipoMoneda);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Adelanto Proveedor
        public List<EAdelantoProveedor> ListarAdelantoProveedor(int IdLibroBanco)
        {
            List<EAdelantoProveedor> lista = new List<EAdelantoProveedor>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ADELANTO_PROVEEDOR_LISTAR(IdLibroBanco);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoProveedor()
                        {
                            icod_correlativo_cabecera = item.mobac_icod_correlativo,
                            iid_anio = Convert.ToInt32(item.anioc_iid_anio),
                            iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            ii_tipo_doc = Convert.ToInt32(item.tdocc_iid_tipo_doc),
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.mobac_nmonto_tipo_cambio),
                            nmonto_movimiento = item.mobac_nmonto_movimiento,
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            sfecha_adelanto = Convert.ToDateTime(item.mobac_dfecha_movimiento),
                            vnro_documento = item.mobac_vnro_documento,
                            vglosa = item.mobac_vglosa,
                            iid_situacion_movimiento_banco = Convert.ToInt32(item.tablc_iid_situacion_movimiento_banco),
                            icod_correlativo = Convert.ToInt32(item.adpr_icod_correlativo),
                            vnumero_adelanto = item.adpr_vnumero_adelanto,
                            icod_proveedor = Convert.ToInt32(item.proc_icod_proveedor),
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            nmonto_canjeado = Convert.ToDecimal(item.adpr_nmonto_canjeado),
                            vobservacion = item.adpr_vobservacion,
                            nsituacion_adelanto_proveedor = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo)
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
        public int insertarAdelantoProveedor(EAdelantoProveedor ob)
        {
            try
            {
                int? IdAdelantoProveedor = 0;

                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_ADELANTO_PROVEEDOR_INSERTAR(ref
                        IdAdelantoProveedor,
                        ob.icod_correlativo_cabecera,
                        ob.vnumero_adelanto,
                        ob.icod_proveedor,
                        ob.iid_tipo_doc,
                        ob.vnumero_documento,
                        ob.sfecha_adelanto,
                        ob.iid_tipo_moneda,
                        ob.nmonto_tipo_cambio,
                        ob.nmonto_adelanto,
                        ob.nmonto_canjeado,
                        ob.vobservacion,
                        ob.doxpc_icod_correlativo,
                        ob.iusuario_crea,
                        ob.vpc_crea,
                        ob.flag_estado
                     );
                }

                return Convert.ToInt32(IdAdelantoProveedor);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarAdelantoProveedor(EAdelantoProveedor ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_ADELANTO_PROVEEDOR_MODICAR(
                        ob.icod_correlativo,
                        ob.icod_correlativo_cabecera,
                        ob.icod_proveedor,
                        ob.iid_tipo_doc,
                        ob.vnumero_documento,
                        ob.sfecha_adelanto,
                        ob.nmonto_tipo_cambio,
                        ob.nmonto_adelanto,
                        ob.nmonto_canjeado,
                        ob.vobservacion,
                        ob.iusuario_modifica,
                        ob.vpc_modifica,
                        ob.iid_tipo_moneda
                     );

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        public void ActualizarNumero(int Anio, int IdDocumento)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_NUMERACION_DOCUMENTO_ACTUALIZAR_NUMERO(
                     Anio,
                     IdDocumento
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Adelanto Clientes
        public List<EAdelantoCliente> ListarAdelantoCliente(int IdLibroBanco)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_ADELANTO_CLIENTE_LISTAR(IdLibroBanco);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoCliente()
                        {
                            icod_correlativo_cabecera = item.mobac_icod_correlativo,
                            iid_anio = Convert.ToInt32(item.anioc_iid_anio),
                            iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            ii_tipo_doc = Convert.ToInt32(item.tdocc_iid_tipo_doc),
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.mobac_nmonto_tipo_cambio),
                            nmonto_movimiento = item.mobac_nmonto_movimiento,
                            cflag_conciliacion = Convert.ToBoolean(item.mobac_cflag_conciliacion),
                            sfecha_adelanto = Convert.ToDateTime(item.mobac_dfecha_movimiento),
                            vnro_documento = item.mobac_vnro_documento,
                            vglosa = item.mobac_vglosa,
                            iid_situacion_movimiento_banco = Convert.ToInt32(item.tablc_iid_situacion_movimiento_banco),
                            vnumero_adelanto = item.adci_vnumero_adelanto,
                            icod_cliente = Convert.ToInt32(item.cliec_icod_cliente),
                            vnombrecliente = item.cliec_vnombre_cliente,
                            nmonto_adelanto = Convert.ToDecimal(item.adci_nmonto_adelanto),
                            nmonto_pagado = Convert.ToDecimal(item.adci_nmonto_pagado),
                            vobservacion = item.adci_vobservacion,
                            nsituacion_adelanto_cliente = Convert.ToInt32(item.tablc_iid_situacion_documento),
                            doxcc_icod_correlativo = Convert.ToInt32(item.doxcc_icod_correlativo)
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

        public int insertarAdelantoCliente(EAdelantoCliente ob)
        {
            try
            {
                int? IdAdelantoCliente = 0;

                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_ADELANTO_CLIENTE_INSERTAR(ref
                        IdAdelantoCliente,
                        ob.icod_correlativo_cabecera,
                        ob.vnumero_adelanto,
                        ob.icod_cliente,
                        ob.iid_tipo_doc,
                        ob.vnumero_documento,
                        ob.sfecha_adelanto,
                        ob.iid_tipo_moneda,
                        ob.nmonto_tipo_cambio,
                        ob.nmonto_adelanto,
                        ob.nmonto_pagado,
                        ob.vobservacion,
                        ob.doxcc_icod_correlativo,
                        ob.iusuario_crea,
                        ob.vpc_crea,
                        ob.flag_estado
                     );
                }

                return Convert.ToInt32(IdAdelantoCliente);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void modificarAdelantoCliente(EAdelantoCliente ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_ADELANTO_CLIENTE_MODICAR(
                        ob.icod_correlativo,
                        ob.vnumero_adelanto,
                        ob.icod_cliente,
                        ob.vnumero_documento,
                        ob.sfecha_adelanto,
                        ob.nmonto_tipo_cambio,
                        ob.nmonto_adelanto,
                        ob.nmonto_pagado,
                        ob.vobservacion,
                        ob.iusuario_modifica,
                        ob.vpc_modifica
                     );

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void eliminarAdelantoCliente(EAdelantoCliente ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_ADELANTO_CLIENTE_ELIMINAR(
                        ob.icod_correlativo,
                        ob.iusuario_modifica,
                        ob.vpc_modifica
                     );

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        public void ActualizarLibroBancos(ELibroBancos ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_LIBRO_BANCOS_MODIFICAR(
                     ob.icod_correlativo,
                     ob.iid_mes,
                     ob.icod_enti_financiera_cuenta,
                     ob.iid_motivo_mov_banco,
                     ob.vdescripcion_beneficiario,
                     ob.nmonto_tipo_cambio,
                     ob.nmonto_movimiento,
                     ob.nmonto_saldo_banco,
                     ob.inumero_orden,
                     ob.vnro_planilla_liquidacion,
                     ob.iusuario_modifica,
                     ob.vpc_modifica,
                     ob.dfecha_movimiento,
                     ob.vglosa,
                     ob.mobac_sfecha_diferida,
                     ob.ii_tipo_doc
                     );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarLibroBancosDetalle(int IdLibroBancoDetalle)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_LIBRO_BANCOS_DETALLE_ELIMINAR(IdLibroBancoDetalle);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarLibroBancosDetalle(ELibroBancosDetalle ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_LIBRO_BANCOS_DETALLE_MODIFICAR(
                         ob.icod_correlativo,
                         ob.iid_correlativo,
                         ob.vnumero_doc,
                         ob.icod_analitica,
                         ob.iid_cuenta_contable,
                         ob.mto_mov_soles,
                         ob.mto_mov_dolar,
                         ob.mto_retenido_soles,
                         ob.mto_retenido_dolar,
                         ob.mto_detalle_soles,
                         ob.mto_detalle_dolar,
                         ob.vglosa,
                         ob.iusuario_modifica,
                         ob.vpc_modifica,
                         ob.mobdc_sfecha,
                         ob.mobdc_icod_concepto_caja


                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





        public void EliminarLibroBancos(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_LIBRO_BANCOS_ELIMINAR(IdLibroBanco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularLibroBancosVarios(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGCT_LIBRO_BANCOS_ANULAR_VARIOS(IdLibroBanco);
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
            List<EConceptoMovCaja> lista = new List<EConceptoMovCaja>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_CONCEPTO_MOVIMIENTO_CAJA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptoMovCaja()
                        {
                            icod_concepto_caja = item.cmvcc_icod_concepto_caja,
                            ccod_concep_mov = item.cmvcc_ccod_concep_mov,
                            vdescripcion = item.cmvcc_vdescripcion,
                            iid_correlativo = item.tdodc_iid_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,

                            tdodc_iid_codigo_doc_det = (Convert.ToInt32(item.tdocd_iid_codigo_doc_det) == 0) ? "" : String.Format("{0:00}", Convert.ToInt32(item.tdocd_iid_codigo_doc_det)),

                            doc_vdes = (item.tdocd_iid_codigo_doc_det != null) ? item.tdocc_vabreviatura_tipo_doc + "-" + item.tdocd_iid_codigo_doc_det.ToString() : "",
                            iid_cuenta_contable = Convert.ToInt64(item.ctacc_iid_cuenta_contable),
                            tipo_analitica = (Convert.ToInt32(item.tipo_analitica) == 0) ? "" : item.tipo_analitica.ToString(),
                            ccosto_flag = Convert.ToBoolean(item.centro_costo),
                            cuenta_vdes = item.cuenta_vdes,
                            cuenta = (int?)item.cuenta,
                            cuenta_ambos = (item.ctacc_iid_cuenta_contable == null) ? item.cuenta.ToString() : item.ctacc_iid_cuenta_contable.ToString(),
                            iid_situacion_cuenta = Convert.ToInt32(item.cmvcc_iid_situacion_cuenta),
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
        public void InsertarMovCaja(EConceptoMovCaja ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_CONCEPTO_MOVIMIENTO_CAJA_INSERTAR(
                            ob.ccod_concep_mov,
                            ob.vdescripcion,
                            ob.iid_correlativo,
                            ob.iid_cuenta_contable,
                            ob.iid_situacion_cuenta,
                            ob.iusuario_crea,
                            ob.vpc_crea
                            );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarMovCaja(EConceptoMovCaja ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_CONCEPTO_MOVIMIENTO_CAJA_ACTUALIZAR(
                            ob.icod_concepto_caja,
                            ob.vdescripcion,
                            ob.iid_correlativo,
                            ob.iid_cuenta_contable,
                            ob.iid_situacion_cuenta,
                            ob.iusuario_modifica,
                            ob.vpc_modifica
                        );

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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_CONCEPTO_MOVIMIENTO_CAJA_ELIMINAR(oBE.icod_concepto_caja);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        ////////*************///////////////

        public List<EConceptoCaja> ListarConceptoCajaEfectivo()
        {
            List<EConceptoCaja> lista = new List<EConceptoCaja>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_CONCEPTO_CAJA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptoCaja()
                        {
                            icod_concepto_caja = item.cmvcc_icod_concepto_caja,
                            ccod_concep_mov = item.cmvcc_ccod_concep_mov,
                            vdescripcion = item.cmvcc_vdescripcion,
                            iid_correlativo = item.tdodc_iid_correlativo,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,

                            tdodc_iid_codigo_doc_det = (Convert.ToInt32(item.tdocd_iid_codigo_doc_det) == 0) ? "" : String.Format("{0:00}", Convert.ToInt32(item.tdocd_iid_codigo_doc_det)),

                            doc_vdes = (item.tdocd_iid_codigo_doc_det != null) ? item.tdocc_vabreviatura_tipo_doc + "-" + item.tdocd_iid_codigo_doc_det.ToString() : "",
                            iid_cuenta_contable = Convert.ToInt64(item.ctacc_iid_cuenta_contable),
                            tipo_analitica = (Convert.ToInt32(item.tipo_analitica) == 0) ? "" : item.tipo_analitica.ToString(),
                            ccosto_flag = Convert.ToBoolean(item.centro_costo),
                            cuenta_vdes = item.cuenta_vdes,
                            cuenta = item.cuenta,
                            cuenta_ambos = (item.ctacc_iid_cuenta_contable == null) ? item.cuenta.ToString() : item.ctacc_iid_cuenta_contable.ToString(),
                            iid_situacion_cuenta = Convert.ToInt32(item.cmvcc_iid_situacion_cuenta),
                            cmvcc_icod_tipo = Convert.ToInt32(item.cmvcc_icod_tipo),
                            strTipo = item.strTipo
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
        public void InsertarCajaEfectivo(EConceptoCaja ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_CONCEPTO_CAJA_INSERTAR(
                            ob.ccod_concep_mov,
                            ob.vdescripcion,
                            ob.iid_correlativo,
                            ob.iid_cuenta_contable,
                            ob.iid_situacion_cuenta,
                            ob.cmvcc_icod_tipo,
                            ob.iusuario_crea,
                            ob.vpc_crea
                            );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCajaEfectivo(EConceptoCaja ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_CONCEPTO_CAJA_ACTUALIZAR(
                            ob.icod_concepto_caja,
                            ob.vdescripcion,
                            ob.iid_correlativo,
                            ob.iid_cuenta_contable,
                            ob.iid_situacion_cuenta,
                            ob.cmvcc_icod_tipo,
                            ob.iusuario_modifica,
                            ob.vpc_modifica
                        );

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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_CONCEPTO_CAJA_ELIMINAR(oBE.icod_concepto_caja);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public List<ECajaChica> ListarCajaChica()
        {
            List<ECajaChica> lista = new List<ECajaChica>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_CAJA_CHICA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECajaChica()
                        {
                            icod_caja_liquida = item.cjalc_icod_caja_liquida,
                            vnro_caja_liquida = item.cjalc_vnro_caja_liquida.ToString(),
                            vdescrip_caja_liquida = item.cjalc_vdescrip_caja_liquida.ToString(),
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            iid_cuenta_contable = Convert.ToInt32(item.ctacc_iid_cuenta_contable),
                            vnumero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            vdescripcion_cuenta_contable = item.ctacc_nombre_descripcion,
                            icod_analitica = item.anac_icod_analitica,
                            tblc_tipo_analitica = item.tarec_icorrelativo_tipo_analitica,
                            anac_iid_analitica = item.anad_iid_analitica,
                            anac_vdescripcion = item.anad_vdescripcion,
                            analisis = (item.anac_icod_analitica != null) ? string.Format("{0:00}", item.tarec_icorrelativo_tipo_analitica) + "-" + item.anad_iid_analitica : "",
                            vnom_responsable = item.cjalc_vnom_responsable.ToString(),
                            iid_situacion_cuenta = Convert.ToInt32(item.cjalc_iid_situacion_cuenta),
                            viid_caja_chica = String.Format("{0:00}", item.cjalc_iid_caja_liquida),
                            Moneda = item.Moneda.ToString(),
                            Situacion = item.Situacion.ToString(),
                            id_correlative_caja_chica = Convert.ToInt32(item.cjalc_iid_caja_liquida)
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
        public void InsertarCajaChica(ECajaChica ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGET_CAJA_CHICA_INSERTAR(
                        ob.vnro_caja_liquida,
                        ob.vdescrip_caja_liquida,
                        ob.iid_tipo_moneda,
                        ob.iid_cuenta_contable,
                        ob.icod_analitica,
                        ob.vnom_responsable,
                        ob.iid_situacion_cuenta,
                        ob.iusuario_crea,
                        ob.vpc_crea,
                        ob.id_correlative_caja_chica
                        );
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ActualizarCajaChica(ECajaChica ob)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_CAJA_CHICA_ACTUALIZAR(
                        ob.icod_caja_liquida,
                        ob.vdescrip_caja_liquida,
                        ob.vnom_responsable,
                        ob.iusuario_modifica,
                        ob.vpc_modifica,
                        ob.iid_cuenta_contable,
                        ob.icod_analitica,
                        ob.iid_situacion_cuenta
                        );
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SGET_CAJA_CHICA_ELIMINAR(oBE.icod_caja_liquida);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<ELiquidacionCaja> listarLiquidacionCaja(int intEjercicio, int intMes)
        {
            List<ELiquidacionCaja> lista = new List<ELiquidacionCaja>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIQUIDACION_CAJA_LISTAR(intEjercicio, intMes);
                    foreach (var item in query)
                    {
                        lista.Add(new ELiquidacionCaja()
                        {
                            lqcc_icod_liquid_cja = item.lqcc_icod_liquid_cja,
                            lqcc_iid_anio = Convert.ToInt32(item.lqcc_iid_anio),
                            lqcc_iid_mes = Convert.ToInt32(item.lqcc_iid_mes),
                            lqcc_icod_caja_liquida = Convert.ToInt32(item.lqcc_icod_caja_liquida),
                            lqcc_inro_liquid_caja = Convert.ToInt32(item.lqcc_inro_liquid_caja),
                            lqcc_sfecha_liquid = Convert.ToDateTime(item.lqcc_sfecha_liquid),
                            lqcc_vconcepto = item.lqcc_vconcepto,
                            lqcc_iid_tipo_moneda = Convert.ToInt32(item.lqcc_iid_tipo_moneda),
                            lqcc_nmonto_total = Convert.ToDecimal(item.lqcc_nmonto_total),
                            lqcc_nmonto_detalle = Convert.ToDecimal(item.lqcc_nmonto_detalle),
                            lqcc_iid_situacion_liq = Convert.ToInt32(item.lqcc_iid_situacion_liq),
                            lqcc_ntipo_cambio = Convert.ToDecimal(item.lqcc_ntipo_cambio),
                            //*//
                            caja_nro = item.caja_nro,
                            caja_decripcion = item.caja_decripcion,
                            moneda = item.moneda,
                            situacion = item.situacion,
                            caja_iid_cuenta_contable = Convert.ToInt32(item.ctacc_iid_cuenta_contable),
                            caja_tipo_analitica = item.tarec_icorrelativo,
                            caja_icod_analitica = item.anac_icod_analitica,
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable,
                            strCodAnalitica = item.anad_iid_analitica
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
        public void AnularLiquidacionCaja(ELiquidacionCaja objLiqCaja)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIQUIDACION_CAJA_ANULAR(
                        objLiqCaja.lqcc_icod_liquid_cja,
                        objLiqCaja.intUsuario,
                        objLiqCaja.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int GetCorrelativoLiquidacion(int intEjercicio, int intCaja)
        {
            int correlativo = new int();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIQUIDACION_CAJA_GET_CORRELATIVO(intEjercicio, intCaja);
                    foreach (var item in query)
                    {
                        correlativo = Convert.ToInt32(item.correlativo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return correlativo;
        }
        public int insertarLiquidacionCaja(ELiquidacionCaja objLiquidacion)
        {
            int IdLiquidacion = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    int? IntId = 0;
                    dc.SIGTT_LIQUIDACION_CAJA_INSERTAR(
                                     ref IntId,
                                     objLiquidacion.lqcc_iid_anio
                                   , objLiquidacion.lqcc_iid_mes
                                   , objLiquidacion.lqcc_icod_caja_liquida
                                   , objLiquidacion.lqcc_inro_liquid_caja
                                   , objLiquidacion.lqcc_sfecha_liquid
                                   , objLiquidacion.lqcc_vconcepto
                                   , objLiquidacion.lqcc_iid_tipo_moneda
                                   , objLiquidacion.lqcc_nmonto_total
                                   , objLiquidacion.lqcc_nmonto_detalle
                                   , objLiquidacion.lqcc_iid_situacion_liq
                                   , objLiquidacion.lqcc_ntipo_cambio
                                   , objLiquidacion.intUsuario
                                   , objLiquidacion.strPc
                                   , Convert.ToBoolean(objLiquidacion.lqcc_flag_estado)
                                     );
                    IdLiquidacion = Convert.ToInt32(IntId);
                }
                return IdLiquidacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLiquidacionCaja(ELiquidacionCaja objLiquidacion)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGTT_LIQUIDACION_CAJA_MODIFICAR(
                                     objLiquidacion.lqcc_icod_liquid_cja
                                   , objLiquidacion.lqcc_vconcepto
                                   , objLiquidacion.lqcc_nmonto_total
                                   , objLiquidacion.lqcc_nmonto_detalle
                                   , objLiquidacion.lqcc_iid_situacion_liq
                                   , objLiquidacion.intUsuario
                                   , objLiquidacion.strPc
                                     );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Liquidación Caja Detalle
        public List<ELiquidacionCajaDet> listarLiquidacionCajaDetalle(int intLiquidacion)
        {
            List<ELiquidacionCajaDet> lista = new List<ELiquidacionCajaDet>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGET_LIQUIDACION_CAJA_DETALLE_LISTAR(intLiquidacion);
                    foreach (var item in query)
                    {
                        lista.Add(new ELiquidacionCajaDet()
                        {
                            lqcd_icod_deta_liquid = item.lqcd_icod_deta_liquid,
                            lqcc_icod_liquid_caja = item.lqcc_icod_liquid_caja,
                            lqcd_inro_item = Convert.ToInt32(item.lqcd_inro_item),
                            lqcd_sfecha_liquid = Convert.ToDateTime(item.lqcd_sfecha_liquid),
                            lqcd_icod_concepto_caja = Convert.ToInt32(item.lqcd_icod_concepto_caja),
                            lqcd_vdescripcion_movim = item.lqcd_vdescripcion_movim,
                            lqcd_nmonto_afecto = Convert.ToDecimal(item.lqcd_nmonto_afecto),
                            lqcd_nmonto_inafecto = Convert.ToDecimal(item.lqcd_nmonto_inafecto),
                            lqcd_nmonto_dest_mixto = Convert.ToDecimal(item.lqcd_nmonto_dest_mixto),
                            lqcd_nporcent_igv = Convert.ToDecimal(item.lqcd_nporcent_igv),
                            lqcd_nmonto_igv = Convert.ToDecimal(item.lqcd_nmonto_igv),
                            lqcd_nmonto_pago = Convert.ToDecimal(item.lqcd_nmonto_pago),
                            lqcd_iid_cuenta_contable = item.lqcd_iid_cuenta_contable,
                            lqcd_iid_centro_costo = item.lqcd_iid_centro_costo,
                            lqcd_iid_tipo_analitica = item.lqcd_iid_tipo_analitica,
                            lqcd_iid_analitica = item.lqcd_iid_analitica,
                            lqcd_iid_proveedor = item.lqcd_iid_proveedor,
                            lqcd_iid_tipo_doc = item.lqcd_iid_tipo_doc,
                            lqcd_iid_clase_tipo_doc = item.lqcd_iid_clase_tipo_doc,
                            lqcd_vnumero_doc = item.lqcd_vnumero_doc,
                            docxp_icod_pago = item.docxp_icod_pago,
                            lqcd_ntipo_cambio_pago = Convert.ToDecimal(item.lqcd_ntipo_cambio_pago),
                            lqcd_nporc_rta_cuarta = Convert.ToDecimal(item.lqcd_nporc_rta_cuarta),
                            lqcd_nmonto_rta_cuarta = Convert.ToDecimal(item.lqcd_nmonto_rta_cuarta),
                            lqcd_flag_estado = Convert.ToInt32(item.lqcd_flag_estado),
                            //**//
                            concepto_abreviatura = item.concepto_abreviatura,
                            numero_cuenta_contable = item.numero_cuenta_contable,
                            cuenta_descripcion = item.cuenta_descripcion,
                            codigo_ccosto = item.codigo_ccosto,
                            ccosto_descripcion = item.ccosto_descripcion,
                            iid_analitica = item.iid_analitica,
                            analisis = (item.lqcd_iid_tipo_analitica != null) ? string.Format("{0:00}", item.lqcd_iid_tipo_analitica) + "." + item.iid_analitica : "",
                            analitica_descripcion = item.analitica_descripcion,
                            codigo_provedor = item.codigo_provedor,
                            proveedor_nombre = item.proveedor_nombre,
                            tip_doc_abreviatura = item.tip_doc_abreviatura,
                            docxp_icod_correlativo = item.docxp_icod_correlativo,
                            docxp_mto_total = item.docxp_mto_total,
                            docxp_mto_pagado = item.docxp_mto_pagado,
                            docxp_mto_saldo = item.docxp_mto_saldo,
                            lqcd_vtipo_movimiento = item.lqcd_vtipo_movimiento,
                            MonedaDXP = Convert.ToInt32(item.MonedaDXP)
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
        public void insertarLiquidacionCajaDetalle(ELiquidacionCajaDet oBe)
        {

            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    dc.SIGTT_LIQUIDACION_CAJA_DETALLE_INSERTAR(
                                     oBe.lqcc_icod_liquid_caja
                                   , oBe.lqcd_inro_item
                                   , oBe.lqcd_sfecha_liquid
                                   , oBe.lqcd_icod_concepto_caja
                                   , oBe.lqcd_vdescripcion_movim
                                   , oBe.lqcd_nmonto_afecto
                                   , oBe.lqcd_nmonto_inafecto
                                   , oBe.lqcd_nmonto_dest_mixto
                                   , oBe.lqcd_nporcent_igv
                                   , oBe.lqcd_nmonto_igv
                                   , oBe.lqcd_nmonto_pago
                                   , oBe.lqcd_iid_cuenta_contable
                                   , oBe.lqcd_iid_centro_costo
                                   , oBe.lqcd_iid_tipo_analitica
                                   , oBe.lqcd_iid_analitica
                                   , oBe.lqcd_iid_proveedor
                                   , oBe.lqcd_iid_tipo_doc
                                   , oBe.lqcd_iid_clase_tipo_doc
                                   , oBe.lqcd_vnumero_doc
                                   , oBe.docxp_icod_pago
                                   , oBe.lqcd_ntipo_cambio_pago
                                   , oBe.lqcd_nporc_rta_cuarta
                                   , oBe.lqcd_nmonto_rta_cuarta
                                   , oBe.intUsuario
                                   , oBe.strPc
                                   , Convert.ToBoolean(oBe.lqcd_flag_estado)
                                   , oBe.docxp_icod_correlativo
                                   , oBe.lqcd_vtipo_movimiento
                                     );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarLiquidacionCajaDetalle(ELiquidacionCajaDet oBe)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGTT_LIQUIDACION_CAJA_DETALLE_MODIFICAR(
                          oBe.lqcd_icod_deta_liquid
                        , oBe.lqcd_inro_item
                        , oBe.lqcd_sfecha_liquid
                        , oBe.lqcd_icod_concepto_caja
                        , oBe.lqcd_vdescripcion_movim
                        , oBe.lqcd_nmonto_pago
                        , oBe.lqcd_iid_cuenta_contable
                        , oBe.lqcd_iid_centro_costo
                        , oBe.lqcd_iid_tipo_analitica
                        , oBe.lqcd_iid_analitica
                        , oBe.intUsuario
                        , oBe.strPc
                        , oBe.docxp_icod_correlativo
                        , oBe.lqcd_vtipo_movimiento);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarLiquidacionCajaDetalle(ELiquidacionCajaDet objLiqDet)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGTT_LIQUIDACION_CAJA_DETALLE_ELIMINAR(
                          objLiqDet.lqcd_icod_deta_liquid
                        , objLiqDet.intUsuario
                        , objLiqDet.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public void EliminarLiquidacionCajaDetalleDocXPagarPago(ELiquidacionCajaDet objLiqDet)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGTT_LIQUIDACION_CAJA_DETALLE_DOC_X_PAGAR_PAGO_ELIMINAR(
                          objLiqDet.docxp_icod_pago
                        , objLiqDet.intUsuario
                        , objLiqDet.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public List<EDocPorPagar> ListarEDocPorPagarNoPendientes(int intAnio, int intProveedor)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGT_DOC_X_PAGAR_LISTAR_NO_PENDIENTES(intAnio, intProveedor);
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
                            vSituacion = (item.tablc_iid_situacion_documento == 1) ? "Pen" : "Can",
                            vMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "$.",
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
        public List<EDocPorPagar> ListarEDocPorPagarTodosPorProveedor(int intAnio, int intProveedor)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGT_DOC_X_PAGAR_LISTAR_NO_TODOS_POR_PROVEEDOR(intAnio, intProveedor);
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
                            vSituacion = (item.tablc_iid_situacion_documento == 1) ? "Pen" : "Can",
                            vMoneda = (item.tablc_iid_tipo_moneda == 1) ? "S/." : "$.",
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

        public List<EDocPorPagar> ListarDocumentoAdelantoNotaCreditoPorCobrarProveedor(int intProveedor, int intEjercicio)
        {
            List<EDocPorPagar> lista = new List<EDocPorPagar>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_LISTAR_ADELANTOS_NOTA_CREDITO_POR_PROVEEDOR(intProveedor, intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EDocPorPagar()
                        {
                            doxpc_icod_correlativo = item.doxpc_icod_correlativo,
                            doxpc_icod_documento = Convert.ToInt32(item.doxpc_icod_documento),
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
        public List<EAdelantoProveedor> ListarAdelantoProveedorTodo(int ianio)
        {
            List<EAdelantoProveedor> lista = new List<EAdelantoProveedor>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGCT_ADELANTO_PROVEEDOR_LISTAR_TODO(ianio);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoProveedor()
                        {
                            icod_correlativo_cabecera = Convert.ToInt32(item.mobac_icod_correlativo),
                            vnumero_documento = (item.adpr_vnumero_adelanto),
                            Situacion = item.Situacion,
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            vnro_documento = item.adpr_vnumero_documento,
                            moneda = item.moneda,
                            nmonto_adelanto = Convert.ToDecimal(item.adpr_nmonto_adelanto),
                            nmonto_canjeado = Convert.ToDecimal(item.adpr_nmonto_canjeado),
                            nmonto_saldo = Convert.ToDecimal(item.nmonto_saldo),
                            sfecha_adelanto = Convert.ToDateTime(item.adpr_sfecha_adelanto),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.adpr_nmonto_tipo_cambio),
                            vobservacion = item.adpr_vobservacion,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            efctc_vnumero_cuenta = item.efctc_vnumero_cuenta,
                            doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo),
                            SimboloMoneda = item.SimboloMoneda,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda)
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


        public void EliminarLibroBancosAdelantoProveedor(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGCT_LIBRO_BANCOS_ELIMINAR_ADELANTO_PROVEEDOR(IdLibroBanco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularLibroBancos(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGCT_LIBRO_BANCOS_ANULAR(IdLibroBanco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularLibroBancosAdelantoProveedor(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGCT_LIBRO_BANCOS_ANULAR_ADELANTO_PROVEEDOR(IdLibroBanco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void EliminarLibroBancosAdelantoCliente(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGCT_LIBRO_BANCOS_ELIMINAR_ADELANTO_CLIENTE(IdLibroBanco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void AnularLibroBancosAdelantoCliente(int IdLibroBanco)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SIGCT_LIBRO_BANCOS_ANULAR_ADELANTO_CLIENTE(IdLibroBanco);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EAdelantoCliente> ListarAdelantoClienteTodo(int intEjercicio)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGCT_ADELANTO_CLIENTE_LISTAR_TODO(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoCliente()
                        {
                            icod_correlativo_cabecera = item.adci_icod_correlativo,
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            vnumero_adelanto = item.adci_vnumero_adelanto,
                            Situacion = item.Situacion,
                            icod_cliente = item.cliec_icod_cliente,
                            vnombrecliente = item.cliec_vnombre_cliente,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            vnro_documento = item.adci_vnumero_documento,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = item.SimboloMoneda,
                            moneda = item.moneda,
                            nmonto_adelanto = Convert.ToDecimal(item.adci_nmonto_adelanto),
                            nmonto_pagado = Convert.ToDecimal(item.adci_nmonto_pagado),
                            nmonto_saldo = Convert.ToDecimal(item.nmonto_saldo),
                            adci_sfecha_adelanto = Convert.ToDateTime(item.adci_sfecha_adelanto),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.adci_nmonto_tipo_cambio),
                            vobservacion = item.adci_vobservacion,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            efctc_vnumero_cuenta = item.bcod_vnumero_cuenta
                            //doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo)
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
        public List<EAdelantoCliente> getAdelantoClienteCab(int intAdelantoCliente)
        {
            List<EAdelantoCliente> lista = new List<EAdelantoCliente>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ADELANTO_CLIENTE_GET_CAB(intAdelantoCliente);
                    foreach (var item in query)
                    {
                        lista.Add(new EAdelantoCliente()
                        {
                            icod_correlativo_cabecera = item.adci_icod_correlativo,
                            mobac_icod_correlativo = item.mobac_icod_correlativo,
                            vnumero_adelanto = item.adci_vnumero_adelanto,
                            sfecha_adelanto = Convert.ToDateTime(item.adci_sfecha_adelanto),
                            Situacion = item.Situacion,
                            icod_cliente = item.cliec_icod_cliente,
                            vnombrecliente = item.cliec_vnombre_cliente,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            vnro_documento = item.adci_vnumero_documento,
                            iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            SimboloMoneda = item.SimboloMoneda,
                            moneda = item.moneda,
                            nmonto_adelanto = Convert.ToDecimal(item.adci_nmonto_adelanto),
                            nmonto_pagado = Convert.ToDecimal(item.adci_nmonto_pagado),
                            nmonto_saldo = Convert.ToDecimal(item.nmonto_saldo),
                            adci_sfecha_adelanto = Convert.ToDateTime(item.adci_sfecha_adelanto),
                            nmonto_tipo_cambio = Convert.ToDecimal(item.adci_nmonto_tipo_cambio),
                            vobservacion = item.adci_vobservacion,
                            vdescripcion_beneficiario = item.mobac_vdescripcion_beneficiario,
                            efctc_vnumero_cuenta = item.bcod_vnumero_cuenta
                            //doxpc_icod_correlativo = Convert.ToInt32(item.doxpc_icod_correlativo)
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
        public List<EDocPorPagarPago> ListarPagoDocumentoXPagarXIdDocXPagarAunaFecha(long doxcc_icod_correlativo, int Anio, DateTime sfecha)
        {
            List<EDocPorPagarPago> lista = new List<EDocPorPagarPago>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGTS_DOC_X_PAGAR_PAGO_LISTAR_X_ICOD_DXC_A_UNA_FECHA(doxcc_icod_correlativo, Anio, sfecha);
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
                            cecoc_ccodigo_centro_costo = item.cecoc_vcodigo_centro_costo,
                            anac_icod_analitica = item.anad_icod_analitica,
                            anac_viid_analitica = item.anad_iid_analitica,
                            anac_vdescripcion = item.anad_vdescripcion,
                            TipoAnalitica = item.TipoAnalitica,
                            Moneda = (item.tablc_iid_tipo_moneda == 3) ? "S/" : "US$",
                            vcocc_iid_voucher_contable = item.vcocc_iid_voucher_contable
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

        public List<EProvisionPlanillaPersonal> listarProvisionPlanilla(int intEjercicio, int intMes)
        {
            List<EProvisionPlanillaPersonal> lista = new List<EProvisionPlanillaPersonal>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PROVISION_PLANILLA_PERSONAL_CONTA_LISTAR(intEjercicio, intMes);
                    foreach (var item in query)
                    {
                        lista.Add(new EProvisionPlanillaPersonal()
                        {
                            planc_icod_planilla_personal = item.planc_icod_planilla_personal,
                            planc_iid_planilla_personal = Convert.ToInt32(item.planc_iid_planilla_personal),
                            NumPlanilla = string.Format("{0:00000}", item.planc_iid_planilla_personal),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            Mes = item.Mes,
                            planc_iid_anio = Convert.ToInt32(item.planc_iid_anio),
                            planc_vdescripcion = item.planc_vdescripcion,
                            tablc_iid_situacion_planilla = Convert.ToInt32(item.tablc_iid_situacion_planilla),
                            Situacion = item.Situacion,
                            planc_iid_tipo_planilla = Convert.ToInt32(item.planc_iid_tipo_planilla),
                            Tipo = item.Tipo,
                            planc_sfecha = Convert.ToDateTime(item.planc_sfecha)
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
        public List<EProvisionPlanillaPersonalDetalle> ListarProvisionPlanillaDetalle(int planc_icod_planilla_personal)
        {
            List<EProvisionPlanillaPersonalDetalle> lista = new List<EProvisionPlanillaPersonalDetalle>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PROVISION_PLANILLA_PERSONAL_DETALLE_CONTA_LISTAR(planc_icod_planilla_personal);
                    foreach (var item in query)
                    {
                        lista.Add(new EProvisionPlanillaPersonalDetalle()
                        {
                            pland_icod_planilla_personal_det = item.pland_icod_planilla_personal_det,
                            pland_iid_planilla_personal_det = Convert.ToInt32(item.pland_iid_planilla_personal_det),
                            planc_icod_planilla_personal = Convert.ToInt32(item.planc_icod_planilla_personal),
                            pland_ape_nom = item.pland_ape_nom,
                            pland_num_doc = item.pland_num_doc,
                            pland_cussp = item.pland_cussp,
                            pland_rem_basica = Convert.ToDecimal(item.pland_rem_basica),
                            pland_icod_personal = Convert.ToInt32(item.pland_icod_personal),
                            pland_beps = item.pland_beps,
                            pland_sfecha_incio = item.pland_sfecha_incio,
                            pland_sfecha_cese = item.pland_sfecha_cese,
                            pland_basignacion_familiar = item.pland_basignacion_familiar,
                            prpc_nasignacion_familiar = item.prpc_nasignacion_familiar,
                            prpc_ngratificacion_essalud = item.prpc_ngratificacion_essalud,
                            prpc_ngratificacion_eps = item.prpc_ngratificacion_eps,
                            pland_nasignacion_familiar = item.pland_nasignacion_familiar,
                            pland_nrem_computable = item.pland_nrem_computable,
                            pland_nmonto_gratificacion = item.pland_nmonto_gratificacion,
                            pland_nbonificacion_mes = item.pland_nbonificacion_mes,
                            strEPS = Convert.ToBoolean(item.pland_beps) == true ? "EPS" : "ESSALUD",
                            //****CTS***//
                            pland_ncts_gratificacion = item.pland_ncts_gratificacion,
                            pland_nctssexto_gratificacion = item.pland_nctssexto_gratificacion,
                            pland_ncts_comision = item.pland_ncts_comision,
                            pland_nctssexto_comision = item.pland_nctssexto_comision,
                            pland_ncts_total = item.pland_ncts_total,
                            pland_icts_meses = item.pland_icts_meses,
                            pland_icts_dias = item.pland_icts_dias,
                            pland_ncts_meses_monto = item.pland_ncts_meses_monto,
                            pland_ncts_dias_monto = item.pland_ncts_dias_monto,
                            pland_ncts_por_mes = item.pland_ncts_por_mes,
                            pland_nctsprovision_acumulada = item.pland_nctsprovision_acumulada,
                            pland_nctsprovision_mes = item.pland_nctsprovision_mes,
                            pland_ncts_horas_extras = item.pland_ncts_horas_extras,
                            iid_analitica = Convert.ToInt32(item.iid_analitica),
                            analitica_descripcion = item.analitica_descripcion,
                            anad_icod_analitica = Convert.ToInt32(item.anad_icod_analitica),
                            pland_vac_prov_tot_mensual = item.pland_vac_prov_tot_mensual
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

        public List<EPlanillaPersonal> listarPlanillaConta(int intEjercicio, int intMes)
        {
            List<EPlanillaPersonal> lista = new List<EPlanillaPersonal>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PLANILLA_PERSONAL_CONTA_LISTAR(intEjercicio, intMes);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaPersonal()
                        {
                            planc_icod_planilla_personal = item.planc_icod_planilla_personal,
                            planc_iid_planilla_personal = Convert.ToInt32(item.planc_iid_planilla_personal),
                            NumPlanilla = string.Format("{0:00000}", item.planc_iid_planilla_personal),
                            mesec_iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            Mes = item.Mes,
                            planc_iid_anio = Convert.ToInt32(item.planc_iid_anio),
                            planc_vdescripcion = item.planc_vdescripcion,
                            tablc_iid_situacion_planilla = Convert.ToInt32(item.tablc_iid_situacion_planilla),
                            Situacion = item.Situacion,
                            planc_iid_tipo_planilla = Convert.ToInt32(item.planc_iid_tipo_planilla),
                            Tipo = item.Tipo,
                            planc_sfecha = Convert.ToDateTime(item.planc_sfecha)


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
        public List<EPlanillaPersonalDetalle> listarPlanillaPersonalDetalle(int planc_icod_planilla_personal)
        {
            List<EPlanillaPersonalDetalle> lista = new List<EPlanillaPersonalDetalle>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PLANILLA_PERSONAL_DETALLE_CONTA_LISTAR(planc_icod_planilla_personal);
                    foreach (var item in query)
                    {
                        lista.Add(new EPlanillaPersonalDetalle()
                        {
                            pland_icod_planilla_personal_det = item.pland_icod_planilla_personal_det,
                            pland_iid_planilla_personal_det = Convert.ToInt32(item.pland_iid_planilla_personal_det),
                            planc_icod_planilla_personal = Convert.ToInt32(item.planc_icod_planilla_personal),
                            pland_ape_nom = item.pland_ape_nom,
                            pland_num_doc = item.pland_num_doc,
                            pland_cussp = item.pland_cussp,
                            pland_sueldo_basico = Convert.ToDecimal(item.pland_sueldo_basico),
                            pland_rem_basica = Convert.ToDecimal(item.pland_rem_basica),
                            pland_icod_personal = Convert.ToInt32(item.pland_icod_personal),
                            pland_sfecha_incio = item.pland_sfecha_incio,
                            pland_sfecha_cese = item.pland_sfecha_cese,
                            pland_nasignacion_familiar = item.pland_nasignacion_familiar,
                            pland_nrem_computable = item.pland_nrem_computable,
                            /**Remuneraciones**/
                            pland_reg_pension = item.pland_reg_pension,
                            pland_comision = item.pland_comision,
                            pland_cargo = item.pland_cargo,
                            pland_hijo = item.pland_hijo,
                            pland_dias = item.pland_dias,
                            pland_faltas = item.pland_faltas,
                            pland_vacaciones = item.pland_vacaciones,
                            pland_descanso_medico = item.pland_descanso_medico,
                            pland_dias_subsidios = item.pland_dias_subsidios,
                            pland_dias_efectivos = item.pland_dias_efectivos,
                            pland_nmonto_vacaciones = item.pland_nmonto_vacaciones,
                            pland_nhoras_25 = item.pland_nhoras_25,
                            pland_nhoras_35 = item.pland_nhoras_35,
                            pland_nferiado_descanso = item.pland_nferiado_descanso,
                            pland_notros_ingresos = item.pland_notros_ingresos,
                            pland_nsubsidios_essalud = item.pland_nsubsidios_essalud,
                            pland_ncomision_venta = item.pland_ncomision_venta,
                            pland_ncomision_eventual = item.pland_ncomision_eventual,
                            pland_nasignacion_transporte = item.pland_nasignacion_transporte,
                            pland_nvales_alimentos = item.pland_nvales_alimentos,
                            pland_nadelanto_sueldo = item.pland_nadelanto_sueldo,
                            pland_ngratif_afecto = item.pland_ngratif_afecto,
                            pland_nbonif_afecto = item.pland_nbonif_afecto,
                            pland_nvacaciones_truncas = item.pland_nvacaciones_truncas,
                            pland_ngratif_no_afecto = item.pland_ngratif_no_afecto,
                            pland_nbonif_no_afecto = item.pland_nbonif_no_afecto,
                            pland_nCTS = item.pland_nCTS,
                            pland_nutilidades = item.pland_nutilidades,
                            pland_nremun_bruta = item.pland_nremun_bruta,
                            pland_nremun_computable_neta = item.pland_nremun_computable_neta,
                            pland_ninasistencias = item.pland_ninasistencias,
                            pland_ntardanzas = item.pland_ntardanzas,
                            pland_npago_utilid = item.pland_npago_utilid,
                            str_reg_pension = Convert.ToInt32(item.pland_reg_pension) == 6384 ? "AFP" : "ONP",
                            str_comision = Convert.ToInt32(item.pland_comision) == 6386 ? "FIJA" : Convert.ToInt32(item.pland_comision) == 6387 ? "MIXTA" : "",
                            str_hijo = Convert.ToInt32(item.pland_hijo) == 1 ? "SI" : "NO",
                            str_cargo = item.str_cargo,
                            pland_desc_onp = item.pland_desc_onp,
                            pland_desc_fondo = item.pland_desc_fondo,
                            pland_desc_comision = item.pland_desc_comision,
                            pland_desc_seguro = item.pland_desc_seguro,
                            pland_desc_tot_afp = item.pland_desc_tot_afp,
                            pland_desc_renta5 = item.pland_desc_renta5,
                            pland_desc_adelanto = item.pland_desc_adelanto,
                            pland_desc_prestamo = item.pland_desc_prestamo,
                            pland_desc_eps = item.pland_desc_eps,
                            pland_desc_otros_desc_no_afect = item.pland_desc_otros_desc_no_afect,
                            pland_desc_retenc_judicial = item.pland_desc_retenc_judicial,
                            pland_desc_otros_desc_afect = item.pland_desc_otros_desc_afect,
                            pland_desc_total_desc = item.pland_desc_total_desc,
                            pland_aport_essalud9 = item.pland_aport_essalud9,
                            pland_aport_eps_pacifico = item.pland_aport_eps_pacifico,
                            pland_aport_essalud = item.pland_aport_essalud,
                            pland_total_neto_pagar = item.pland_total_neto_pagar,
                            str_fondo_pension = item.str_fondo_pension,
                            pland_nutilidad_convencional = item.pland_nutilidad_convencional,
                            pland_npago_utilidad_convencional = item.pland_npago_utilidad_convencional
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


        #region Cajas Chicas
        public List<ECajasChicas> listarCajasChicas()
        {
            List<ECajasChicas> lista = new List<ECajasChicas>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CAJAS_CHICAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECajasChicas()
                        {
                            cc_icod_caja_chica = item.cc_icod_caja_chica,
                            cc_vdescricpcion = item.cc_vdescricpcion,
                            ctacc_icod_cuenta_contable = Convert.ToInt64(item.ctacc_icod_cuenta_contable),
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable
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
        public int insertarCajasChicas(ECajasChicas oBe)
        {
            int? intIcod = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJAS_CHICAS_INSERTAR(
                        ref intIcod,
                        oBe.cc_vdescricpcion,
                        oBe.ctacc_icod_cuenta_contable,
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
        public void modificarCajasChicas(ECajasChicas oBe)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJAS_CHICAS_MODIFICAR(
                        oBe.cc_icod_caja_chica,
                        oBe.cc_vdescricpcion,
                        oBe.ctacc_icod_cuenta_contable,
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
        public void eliminarCajasChicas(ECajasChicas oBe)
        {
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJAS_CHICAS_ELIMINAR(
                        oBe.cc_icod_caja_chica,
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

        #region Cajas Correlativo
        public List<ECajaCorrelativo> listarCajasCorrelativo()
        {
            List<ECajaCorrelativo> lista = new List<ECajaCorrelativo>();
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_CAJAS_CORRELATIVOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECajaCorrelativo()
                        {
                            cc_icod_caja_correlativo = item.cc_icod_caja_correlativo,
                            bcoc_icod_banco = Convert.ToInt32(item.bcoc_icod_banco),
                            bcod_icod_banco_cuenta = Convert.ToInt32(item.bcod_icod_banco_cuenta),
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            cc_inumero_correlativo = Convert.ToInt32(item.cc_inumero_correlativo),
                            DesBanco = item.DesBanco,
                            DesBancoCuenta = item.DesBancoCuenta,
                            TipoDoc = item.TipoDoc,
                            cc_icod_tipo = Convert.ToInt32(item.cc_icod_tipo),
                            strTipo = item.strTipo
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
        public int insertarCajasCorrelativo(ECajaCorrelativo oBe)
        {
            int? intIcod = 0;
            try
            {
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJAS_CORRELATIVOS_INSERTAR(
                        ref intIcod,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.cc_inumero_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cc_icod_tipo
                        );
                }
                return Convert.ToInt32(intIcod);
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJAS_CORRELATIVOS_MODIFICAR(
                        oBe.cc_icod_caja_correlativo,
                        oBe.bcoc_icod_banco,
                        oBe.bcod_icod_banco_cuenta,
                        oBe.tdocc_icod_tipo_doc,
                        oBe.cc_inumero_correlativo,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.cc_icod_tipo
                        );
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
                using (TesoreriaDataContext dc = new TesoreriaDataContext(Helper.conexion()))
                {
                    dc.SGE_CAJAS_CORRELATIVOS_ELIMINAR(
                        oBe.cc_icod_caja_correlativo,
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
