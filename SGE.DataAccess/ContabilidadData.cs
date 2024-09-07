using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SGE.DataAccess
{
    public class ContabilidadData
    {
        #region Año Ejercicio
        public List<EAnioEjercicio> listarAnioEjercicio()
        {
            List<EAnioEjercicio> lista = new List<EAnioEjercicio>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_ANIO_EJERCICIO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAnioEjercicio()
                        {
                            anioc_icod_anio_ejercicio = item.anioc_icod_anio_ejercicio,
                            anioc_iid_anio_ejercicio = item.anioc_iid_anio_ejercicio,
                            anioc_iactivo = item.anioc_iactivo,
                            strEstado = item.strEstado
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
        public int insertarAnioEjercicio(EAnioEjercicio obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_ANIO_EJERCICIO_INSERTAR(
                        ref intIcod,
                        obj.anioc_iid_anio_ejercicio,
                        obj.anioc_iactivo);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAnioEjercicio(EAnioEjercicio obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_ANIO_EJERCICIO_MODIFICAR(
                        obj.anioc_icod_anio_ejercicio,
                        obj.anioc_iactivo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAnioEjercicio(EAnioEjercicio obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_ANIO_EJERCICIO_ELIMINAR(
                        obj.anioc_icod_anio_ejercicio);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region Centro de Costo
        public List<ECentroCosto> listarCentroCosto()
        {
            List<ECentroCosto> lista = new List<ECentroCosto>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_CENTRO_COSTO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECentroCosto()
                        {
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            cecoc_vcodigo_centro_costo = item.cecoc_vcodigo_centro_costo,
                            cecoc_vdescripcion = item.cecoc_vdescripcion,
                            cecoc_situacion_centro_costo = Convert.ToBoolean(item.cecoc_situacion_centro_costo),
                            cecoc_flag_estado = Convert.ToBoolean(item.cecoc_flag_estado),
                            strEstado = (Convert.ToBoolean(item.cecoc_situacion_centro_costo)) ? "Activo" : "Inactivo",
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto)
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
        public List<ECentroCosto> listarCentroCostoProyectos()
        {
            List<ECentroCosto> lista = new List<ECentroCosto>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_CENTRO_COSTO_LISTAR_PROYECTOS();
                    foreach (var item in query)
                    {
                        lista.Add(new ECentroCosto()
                        {
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            cecoc_vcodigo_centro_costo = item.cecoc_vcodigo_centro_costo,
                            cecoc_vdescripcion = item.cecoc_vdescripcion,
                            cecoc_situacion_centro_costo = Convert.ToBoolean(item.cecoc_situacion_centro_costo),
                            cecoc_flag_estado = Convert.ToBoolean(item.cecoc_flag_estado),
                            strEstado = (Convert.ToBoolean(item.cecoc_situacion_centro_costo)) ? "Activo" : "Inactivo",
                            pryc_icod_proyecto = Convert.ToInt32(item.pryc_icod_proyecto)
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
        public int insertarCentroCosto(ECentroCosto obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_CENTRO_COSTO_INSERTAR(
                        ref intIcod,
                        obj.cecoc_vcodigo_centro_costo,
                        obj.cecoc_vdescripcion,
                        obj.cecoc_situacion_centro_costo,
                        obj.intUsuario,
                        obj.pryc_icod_proyecto
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCentroCosto(ECentroCosto obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_CENTRO_COSTO_MODIFICAR(
                        obj.cecoc_icod_centro_costo,
                        obj.cecoc_vdescripcion,
                        obj.cecoc_situacion_centro_costo,
                        obj.intUsuario,
                        obj.pryc_icod_proyecto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCentroCosto(ECentroCosto obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_CENTRO_COSTO_ELIMINAR(
                        obj.cecoc_icod_centro_costo,
                        obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Analíticas
        public List<EAnaliticaDetalle> listarAnaliticaDetalle(int intTipoAnalitica)
        {
            List<EAnaliticaDetalle> lista = new List<EAnaliticaDetalle>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_ANALITICA_DETALLE_LISTAR(intTipoAnalitica);
                    foreach (var item in query)
                    {
                        lista.Add(new EAnaliticaDetalle()
                        {
                            anad_icod_analitica = item.anad_icod_analitica,
                            anad_iid_analitica = item.anad_iid_analitica,
                            anad_vdescripcion = item.anad_vdescripcion,
                            tarec_icorrelativo_tipo_analitica = item.tarec_icorrelativo_tipo_analitica,
                            anad_nombre = item.anad_nombre,
                            anad_apepaterno = item.anad_apepaterno,
                            anad_apematerno = item.anad_apematerno,
                            tarec_icorrelativo_tipo_persona = item.tarec_icorrelativo_tipo_persona,
                            anad_origen = item.anad_origen,
                            anad_situacion = Convert.ToBoolean(item.anad_situacion),
                            strEstado = (Convert.ToBoolean(item.anad_situacion)) ? "ACTIVO" : "INACTIVO",
                            strTipoAnalitica = item.strTipoAnalitica,
                            strTipoPersona = item.strTipoPersona,
                            id_entidad = item.id_entidad
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
        public List<EAnaliticaDetalle> listarAnaliticaDetalleTodo()
        {
            List<EAnaliticaDetalle> lista = new List<EAnaliticaDetalle>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_ANALITICA_DETALLE_LISTAR_TODO();
                    foreach (var item in query)
                    {
                        lista.Add(new EAnaliticaDetalle()
                        {
                            anad_icod_analitica = item.anad_icod_analitica,
                            anad_iid_analitica = item.anad_iid_analitica,
                            anad_vdescripcion = item.anad_vdescripcion,
                            tarec_icorrelativo_tipo_analitica = Convert.ToInt32(item.tarec_icorrelativo_tipo_analitica),
                            anad_nombre = item.anad_nombre,
                            anad_apepaterno = item.anad_apepaterno,
                            anad_apematerno = item.anad_apematerno,
                            tarec_icorrelativo_tipo_persona = item.tarec_icorrelativo_tipo_persona,
                            anad_origen = item.anad_origen,
                            anad_situacion = Convert.ToBoolean(item.anad_situacion),
                            strEstado = (Convert.ToBoolean(item.anad_situacion)) ? "ACTIVO" : "INACTIVO",
                            strTipoAnalitica = item.strTipoAnalitica,
                            strTipoPersona = item.strTipoPersona,
                            id_entidad = item.id_entidad
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
        public int insertarAnaliticaDetalle(EAnaliticaDetalle obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_ANALITICA_DETALLE_INSERTAR(
                        ref intIcod,
                        obj.anad_iid_analitica,
                        obj.anad_vdescripcion,
                        obj.tarec_icorrelativo_tipo_analitica,
                        obj.anad_nombre,
                        obj.anad_apepaterno,
                        obj.anad_apematerno,
                        obj.tarec_icorrelativo_tipo_persona,
                        obj.intUsuario,
                        obj.anad_origen,
                        obj.anad_situacion
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAnaliticaDetalle(EAnaliticaDetalle obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_ANALITICA_DETALLE_MODIFICAR(
                        obj.anad_icod_analitica,
                        obj.anad_vdescripcion,
                        obj.anad_nombre,
                        obj.anad_apepaterno,
                        obj.anad_apematerno,
                        obj.tarec_icorrelativo_tipo_persona,
                        obj.anad_situacion,
                        obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAnaliticaDetalle(EAnaliticaDetalle obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_ANALITICA_DETALLE_ELIMINAR(
                        obj.anad_icod_analitica,
                        obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Parámetro Contable
        public List<EParametroContable> listarParametroContable()
        {
            List<EParametroContable> lista = new List<EParametroContable>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_PARAMETRO_CONTABLE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EParametroContable()
                        {
                            parac_iid_parametro = item.parac_iid_parametro,
                            parac_vnombre = item.parac_vnombre,
                            parac_vdescripcion = item.parac_vdescripcion,
                            parac_nvalor_numerico = item.parac_nvalor_numerico,
                            parac_vvalor_texto = item.parac_vvalor_texto,
                            parac_cflag_visible = item.parac_cflag_visible,
                            parac_id_sd_ingbco = item.parac_id_sd_ingbco,
                            parac_id_sd_egrbco = item.parac_id_sd_egrbco,
                            parac_id_sd_cjachic = item.parac_id_sd_cjachic,
                            parac_id_sd_cosvta = item.parac_id_sd_cosvta,
                            parac_id_sd_docxpag = item.parac_id_sd_docxpag,
                            parac_id_sd_docxcob = item.parac_id_sd_docxcob,
                            parac_id_sd_apert = item.parac_id_sd_apert,
                            parac_id_sd_cieanual = item.parac_id_sd_cieanual,
                            parac_id_cta_gdifc_mn = item.parac_id_cta_gdifc_mn,
                            parac_id_cta_gdifc_me = item.parac_id_cta_gdifc_me,
                            parac_id_cta_pdifc_mn = item.parac_id_cta_pdifc_mn,
                            parac_id_cta_pdifc_me = item.parac_id_cta_pdifc_me,
                            parac_id_sd_difc_mn = item.parac_id_sd_difc_mn,
                            parac_id_sd_difc_me = item.parac_id_sd_difc_me,
                            parac_nro_comp_difc_mn = item.parac_nro_comp_difc_mn,
                            parac_nro_comp_difc_me = item.parac_nro_comp_difc_me,
                            parac_id_ccosto_difc = item.parac_id_ccosto_difc,
                            parac_id_cta_gredo_mn = item.parac_id_cta_gredo_mn,
                            parac_id_cta_gredo_me = item.parac_id_cta_gredo_me,
                            parac_id_cta_predo_mn = item.parac_id_cta_predo_mn,
                            parac_id_cta_predo_me = item.parac_id_cta_predo_me,
                            parac_id_cta_retencion = item.parac_id_cta_retencion,
                            parac_id_cta_planilla = item.parac_id_cta_planilla,
                            parac_id_ccos_base = item.parac_id_ccos_base,
                            tablc_iid_modulo = item.tablc_iid_modulo,
                            parac_cestado = item.parac_cestado,
                            parac_vmascara = item.parac_vmascara,
                            strCodeCCostoDifCambio = item.strCodeCCostoDifCambio,
                            strDesCCostoDifCambio = item.strDesCCostoDifCambio,
                            strCodeCCostoBase = item.strCodeCCostoBase,
                            strDesCCostoBase = item.strDesCCostoBase,
                            ctacc_icod_cta_ctbl_serv_propio = item.ctacc_icod_cta_ctbl_serv_propio,
                            ctacc_icod_cta_ctbl_serv_externo = item.ctacc_icod_cta_ctbl_serv_externo,
                            strCodCuentaServPropio = item.strCodCuentaServPropio,
                            strDesCuentaServPropio = item.strDesCuentaServPropio,
                            strCodCuentaServExterno = item.strCodCuentaServExterno,
                            strDesCuentaServExterno = item.strDesCuentaServExterno,
                            Porcentaje_de_Retencion = Convert.ToDecimal(item.Porcentaje_de_Retencion),
                            parac_id_cta_4ta_cat = item.parac_id_cta_4ta_cat,
                            strCod4taCategoria = item.strCod4taCategoria,
                            parac_Porcentaje_Retencion_ventas = Convert.ToDecimal(item.parac_Porcentaje_Retencion_ventas),
                            parac_id_sd_planillas = Convert.ToInt32(item.parac_id_sd_planillas)
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
        public void insertarParametroContable(EParametroContable obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_PARAMETRO_CONTABLE_INSERTAR(
                        obj.parac_vnombre,
                        obj.parac_vdescripcion,
                        obj.parac_nvalor_numerico,
                        obj.parac_vvalor_texto,
                        obj.parac_cflag_visible,
                        obj.parac_id_sd_ingbco,
                        obj.parac_id_sd_egrbco,
                        obj.parac_id_sd_cjachic,
                        obj.parac_id_sd_cosvta,
                        obj.parac_id_sd_docxpag,
                        obj.parac_id_sd_docxcob,
                        obj.parac_id_sd_apert,
                        obj.parac_id_sd_cieanual,
                        obj.parac_id_cta_gdifc_mn,
                        obj.parac_id_cta_gdifc_me,
                        obj.parac_id_cta_pdifc_mn,
                        obj.parac_id_cta_pdifc_me,
                        obj.parac_id_sd_difc_mn,
                        obj.parac_id_sd_difc_me,
                        obj.parac_nro_comp_difc_mn,
                        obj.parac_nro_comp_difc_me,
                        obj.parac_id_ccosto_difc,
                        obj.parac_id_cta_gredo_mn,
                        obj.parac_id_cta_gredo_me,
                        obj.parac_id_cta_predo_mn,
                        obj.parac_id_cta_predo_me,
                        obj.parac_id_cta_retencion,
                        obj.parac_id_cta_planilla,
                        obj.parac_id_ccos_base,
                        obj.tablc_iid_modulo,
                        obj.parac_cestado,
                        obj.strPc,
                        obj.intUsuario,
                        obj.parac_vmascara,
                        obj.ctacc_icod_cta_ctbl_serv_propio,
                        obj.ctacc_icod_cta_ctbl_serv_externo,
                        obj.Porcentaje_de_Retencion,
                        obj.parac_id_cta_4ta_cat,
                        obj.parac_Porcentaje_Retencion_ventas,
                        obj.parac_id_sd_planillas
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarParamentroContable(EParametroContable obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_PARAMETRO_CONTABLE_MODIFICAR(
                        obj.parac_iid_parametro,
                        obj.parac_vnombre,
                        obj.parac_vdescripcion,
                        obj.parac_nvalor_numerico,
                        obj.parac_vvalor_texto,
                        obj.parac_cflag_visible,
                        obj.parac_id_sd_ingbco,
                        obj.parac_id_sd_egrbco,
                        obj.parac_id_sd_cjachic,
                        obj.parac_id_sd_cosvta,
                        obj.parac_id_sd_docxpag,
                        obj.parac_id_sd_docxcob,
                        obj.parac_id_sd_apert,
                        obj.parac_id_sd_cieanual,
                        obj.parac_id_cta_gdifc_mn,
                        obj.parac_id_cta_gdifc_me,
                        obj.parac_id_cta_pdifc_mn,
                        obj.parac_id_cta_pdifc_me,
                        obj.parac_id_sd_difc_mn,
                        obj.parac_id_sd_difc_me,
                        obj.parac_nro_comp_difc_mn,
                        obj.parac_nro_comp_difc_me,
                        obj.parac_id_ccosto_difc,
                        obj.parac_id_cta_gredo_mn,
                        obj.parac_id_cta_gredo_me,
                        obj.parac_id_cta_predo_mn,
                        obj.parac_id_cta_predo_me,
                        obj.parac_id_cta_retencion,
                        obj.parac_id_cta_planilla,
                        obj.parac_id_ccos_base,
                        obj.tablc_iid_modulo,
                        obj.parac_cestado,
                        obj.strPc,
                        obj.intUsuario,
                        obj.parac_vmascara,
                        obj.ctacc_icod_cta_ctbl_serv_propio,
                        obj.ctacc_icod_cta_ctbl_serv_externo,
                        obj.Porcentaje_de_Retencion,
                        obj.parac_id_cta_4ta_cat,
                        obj.parac_Porcentaje_Retencion_ventas,
                        obj.parac_id_sd_planillas
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Cuentas Contables
        public List<ECuentaContable> listarCuentaContable()
        {
            List<ECuentaContable> lista = new List<ECuentaContable>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGECONTA_CUENTA_CONTABLE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECuentaContable()
                        {
                            anioc_iid_anio = item.anioc_iid_anio,
                            ctacc_icod_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_iid_cuenta_padre = item.ctacc_iid_cuenta_padre,
                            ctacc_ = item.ctacc_,
                            ctacc_nivel_cuenta = Convert.ToInt32(item.ctacc_nivel_cuenta),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ctacc_cflag_relacion = item.ctacc_cflag_relacion,
                            tablc_iid_tipo_analitica = item.tablc_iid_tipo_analitica,
                            tablc_iid_clasificacion = item.tablc_iid_clasificacion,
                            tablc_iid_elemento = item.tablc_iid_elemento,
                            tablc_iid_tipo_cuenta = item.tablc_iid_tipo_cuenta,
                            tablc_iid_tipo_saldo = item.tablc_iid_tipo_saldo,
                            ctacc_icod_cuenta_debe_auto = item.ctacc_icod_cuenta_debe_auto,
                            ctacc_icod_cuenta_haber_auto = item.ctacc_icod_cuenta_haber_auto,
                            ctacc_flag_estado = Convert.ToBoolean(item.ctacc_flag_estado),
                            ctacc_iccosto = Convert.ToBoolean(item.ctacc_iccosto),
                            strEstado = (Convert.ToBoolean(item.ctacc_flag_estado) == true) ? "Activo" : "Inactivo",
                            strTipoMoneda = item.strTipoMoneda,
                            strTipoCuenta = item.strTipoCuenta,
                            strTipoAnalitica = item.strTipoAnalitica,
                            strFlagCCosto = (Convert.ToBoolean(item.ctacc_iccosto)) ? "Si" : "No"
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
        public List<ECuentaContable> listarCuentaContableImpresion(string strCuentaInicio, string strCuentaFin)
        {
            List<ECuentaContable> lista = new List<ECuentaContable>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_IMPRESION_CUENTA_CONTABLE(strCuentaInicio, strCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new ECuentaContable()
                        {
                            anioc_iid_anio = item.anioc_iid_anio,
                            ctacc_icod_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_iid_cuenta_padre = item.ctacc_iid_cuenta_padre,
                            ctacc_ = item.ctacc_,
                            ctacc_nivel_cuenta = Convert.ToInt32(item.ctacc_nivel_cuenta),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            ctacc_cflag_relacion = item.ctacc_cflag_relacion,
                            tablc_iid_tipo_analitica = item.tablc_iid_tipo_analitica,
                            tablc_iid_clasificacion = item.tablc_iid_clasificacion,
                            tablc_iid_elemento = item.tablc_iid_elemento,
                            tablc_iid_tipo_cuenta = item.tablc_iid_tipo_cuenta,
                            tablc_iid_tipo_saldo = item.tablc_iid_tipo_saldo,
                            /*------------------------------------------------------------------*/
                            ctacc_icod_cuenta_debe_auto = item.ctacc_icod_cuenta_debe_auto,
                            strNumeroCuentaDebeAuto = item.strNumeroCuentaDebeAuto,
                            strDesCuentaDebeAuto = item.strDesCuentaDebeAuto,
                            /*------------------------------------------------------------------*/
                            ctacc_icod_cuenta_haber_auto = item.ctacc_icod_cuenta_haber_auto,
                            strNumeroCuentaHaberAuto = item.strNumeroCuentaHaberAuto,
                            strDesCuentaHaberAuto = item.strDesCuentaHaberAuto,
                            /*------------------------------------------------------------------*/
                            strTipoCuenta = item.strTipoCuenta,
                            ctacc_iccosto = Convert.ToBoolean(item.ctacc_iccosto)
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
        public int insertarCuentaContable(ECuentaContable obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_CUENTA_CONTABLE_INSERTAR(
                        obj.anioc_iid_anio,
                        obj.ctacc_icod_cuenta_contable,
                        obj.ctacc_numero_cuenta_contable,
                        obj.ctacc_nombre_descripcion,
                        obj.ctacc_iid_cuenta_padre,
                        obj.ctacc_,
                        obj.ctacc_nivel_cuenta,
                        obj.tablc_iid_tipo_moneda,
                        obj.ctacc_cflag_relacion,
                        obj.tablc_iid_tipo_analitica,
                        obj.tablc_iid_clasificacion,
                        obj.tablc_iid_elemento,
                        obj.tablc_iid_tipo_cuenta,
                        obj.tablc_iid_tipo_saldo,
                        obj.ctacc_icod_cuenta_debe_auto,
                        obj.ctacc_icod_cuenta_haber_auto,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ctacc_flag_estado,
                        obj.ctacc_iccosto
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarCuentaContable(ECuentaContable obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_CUENTA_CONTABLE_MODIFICAR(
                        obj.anioc_iid_anio,
                        obj.ctacc_icod_cuenta_contable,
                        obj.ctacc_numero_cuenta_contable,
                        obj.ctacc_nombre_descripcion,
                        obj.ctacc_iid_cuenta_padre,
                        obj.ctacc_,
                        obj.ctacc_nivel_cuenta,
                        obj.tablc_iid_tipo_moneda,
                        obj.ctacc_cflag_relacion,
                        obj.tablc_iid_tipo_analitica,
                        obj.tablc_iid_clasificacion,
                        obj.tablc_iid_elemento,
                        obj.tablc_iid_tipo_cuenta,
                        obj.tablc_iid_tipo_saldo,
                        obj.ctacc_icod_cuenta_debe_auto,
                        obj.ctacc_icod_cuenta_haber_auto,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ctacc_flag_estado,
                        obj.ctacc_iccosto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarCuentaContable(ECuentaContable obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_CUENTA_CONTABLE_ELIMINAR(
                        obj.ctacc_icod_cuenta_contable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Sub Diarios
        public List<EComprobanteDetalle> ListarComprobanteDetallexSubdiario(int intEjercicio, int intMes, int subInicial, int subFinal)
        {
            List<EComprobanteDetalle> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EComprobanteDetalle>();
                    var query = dc.SIGT_VOUCHER_CONTABLE_DET_LISTAR_X_SUBDIARIO(intEjercicio, intMes, subInicial, subFinal);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprobanteDetalle()
                        {
                            iid_det_correlat = item.vcocd_icod_det,
                            fec_cab = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            viid_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            iid_voucher_contable = Convert.ToInt32(item.vcocc_icod_vcontable),
                            vglosa_linea = item.vcocd_vglosa_linea,
                            vid_subdiario = string.Format("{0:00}", item.subdi_icod_subdiario),
                            iid_subdiario_vnum_voucher = string.Format("{0:00}", item.subdi_icod_subdiario) + "." + item.vcocc_numero_vcontable,
                            ctacc_vnombre_descripcion_larga = item.ctacc_nombre_descripcion
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
        public List<EComprobanteDetalle> ListarComprobanteDetalle(int Code)
        {
            List<EComprobanteDetalle> lista = new List<EComprobanteDetalle>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SIGT_VOUCHER_CONTABLE_DET_LISTAR(Code);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprobanteDetalle()
                        {
                            iid_det_correlat = item.vcocd_icod_det,
                            nro_item_det = item.vcocd_nro_item_det,
                            icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            vnumero_documento = item.vcocd_numero_doc,
                            tipo_numero_documento = item.tdocc_vabreviatura_tipo_doc + "-" + item.vcocd_numero_doc,
                            iid_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            viid_cuenta_contable = item.ctacc_vnumero_cuenta_contable,
                            vdescripcion_cuenta_contable = item.ctacc_vnombre_descripcion_larga,
                            icod_centro_costo = item.cecoc_icod_centro_costo,
                            iid_tipo_relacion = item.tablc_iid_tipo_analitica,
                            iid_relacion = item.anad_icod_analitica,
                            vglosa_linea = item.vcocd_vglosa_linea,
                            nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            //corigen_registro_item = Convert.ToChar(item.tarec_icorrelativo_origen_vcontable),
                            vcode_centro_costo = item.centrocosto,
                            cecoc_vdescripcion = item.cecoc_vdescripcion,
                            viid_tipo_relacion = (item.tipoanalitica == 0) ? "" : String.Format("{0:00}", item.tipoanalitica),
                            viid_relacion = item.analitica,
                            anac_vdescripcion = item.anac_vdescripcion,
                            vicod_centro_costo = item.identificador,
                            iid_voucher_contable = item.vcocc_icod_vcontable,
                            iid_cautomatica_debe = item.ctacc_icod_cuenta_debe_auto,
                            iid_cautomatica_haber = item.ctacc_icod_cuenta_haber_auto,
                            tipocambio = item.vcocd_tipo_cambio.ToString(),
                            vdescripcion_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            ctacc_iid_cuenta_contable_ref = item.ctacc_iid_cuenta_contable_ref,
                            vdes_Analisis = (item.tipoanalitica != 0) ? string.Format("{0:00}", item.tablc_iid_tipo_analitica) + "." + item.analitica : ""
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
        public List<EComprobante> ListarComprobantesxSubdiario(int intEjercicio, int intMes, int subInicial, int subFinal)
        {
            List<EComprobante> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EComprobante>();
                    var query = dc.SIGT_VOUCHER_CONTABLE_CAB_LISTAR_X_SUBDIARIO(intEjercicio, intMes, subInicial, subFinal);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprobante()
                        {
                            iid_anio = item.anioc_iid_anio,
                            iid_mes = item.mesec_iid_mes,
                            iid_voucher_contable = item.vcocc_icod_vcontable,
                            subdi_icod_subdiario = item.subdi_icod_subdiario,
                            sfec_nota_contable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vglosa = item.vcocc_vglosa,
                            vobservacion = item.vcocc_observacion,
                            vnumero_voucher_contable = item.vcocc_numero_vcontable,
                            iid_subdiario_vnum_voucher = string.Format("{0:00}", item.subdi_icod_subdiario) + "." + item.vcocc_numero_vcontable,
                            iid_situacion_voucher_contable = item.tarec_icorrelativo_situacion_vcontable,
                            iid_origen_voucher_contable = item.tarec_icorrelativo_origen_vcontable,
                            tablc_iid_moneda = item.tablc_iid_moneda,
                            tablc_iid_tipo_cambio = Convert.ToInt32(item.vcocc_tipo_cambio),

                            nmto_tot_debe_sol = item.vcocc_nmto_tot_debe_sol,
                            nmto_tot_haber_sol = item.vcocc_nmto_tot_haber_sol,
                            nmto_tot_debe_dol = item.vcocc_nmto_tot_debe_dol,
                            nmto_tot_haber_dol = item.vcocc_nmto_tot_haber_dol,
                            idf_SubDiario = item.subdi_icod_subdiario,
                            subdiario_des = item.subdiario_des,
                            iid_dia = Convert.ToDateTime(item.vcocc_fecha_vcontable).Day,
                            vsituacion_voucher = item.Estado,
                            tipocambio = Convert.ToString(item.tipocambio),
                            //vmoneda = item.tipomoneda,
                            vmoneda = (item.tablc_iid_moneda == 1) ? "S/." : (item.tablc_iid_moneda == 2) ? "US$" : "HIS",
                            Movimiento = Convert.ToInt32(item.movimientos),

                            tbl_origen = item.tbl_origen,
                            tbl_reg_icod = item.tbl_origen_icod
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
        /*lista para expotar al TXT Gerson */
        public List<EComprobante> ListarComprobantesxSubdiario_TXT(int intEjercicio, int intMes)
        {
            List<EComprobante> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EComprobante>();
                    var query = dc.SIGT_VOUCHER_CONTABLE_CAB_LISTAR_X_SUBDIARIO_TXT(intEjercicio, intMes);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprobante()
                        {
                            iid_anio = Convert.ToInt32(item.anioc_iid_anio),
                            iid_mes = Convert.ToInt32(item.mesec_iid_mes),
                            iid_voucher_contable = Convert.ToInt32(item.vcocc_icod_vcontable),
                            subdi_icod_subdiario = Convert.ToInt32(item.subdi_icod_subdiario),
                            sfec_nota_contable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            tablc_iid_moneda = item.tablc_iid_moneda,

                            iid_situacion_voucher_contable = item.tarec_icorrelativo_situacion_vcontable,
                            iid_origen_voucher_contable = item.tarec_icorrelativo_origen_vcontable,
                            tablc_iid_tipo_cambio = Convert.ToInt32(item.vcocc_tipo_cambio),
                            iid_dia = Convert.ToDateTime(item.vcocc_fecha_vcontable).Day,
                            vcocd_nro_item_det = item.vcocd_nro_item_det,
                            ctacc_icod_cuenta_contable = item.ctacc_icod_cuenta_contable,

                            /*txt*/
                            vcocd_AnioDocu = item.anio_txt,
                            vnumero_voucher_contable = item.tipoaMC_txt,
                            vcocd_numero_doc__show = item.abre_txt,
                            vcocd_numero_doc = item.abre_nex_txt,
                            vcocd_moneda = item.moneda_txt,
                            vcocd_formato_txt = item.form_txt,
                            tdocc_coa = item.tdocc_coa_txt,
                            vglosa = item.vglosa_txt,
                            nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_Vperido_fech = item.fecha_txt,
                            anad_icod_analitica = Convert.ToInt32(item.anad_icod_analitica),
                            anad_iid_analitica = item.anad_iid_analitica,
                            tarec_icorrelativo_tipo_analitica = Convert.ToInt32(item.tarec_icorrelativo_tipo_analitica),
                            doxpc_codigo_aduana = item.doxpc_codigo_aduana

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


        public List<ESubDiario> listarSubDiario()
        {
            List<ESubDiario> lista = new List<ESubDiario>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_SUBDIARIO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ESubDiario()
                        {
                            subdi_icod_subdiario = item.subdi_icod_subdiario,
                            subdi_vdescripcion = item.subdi_vdescripcion,
                            subdi_iactivo = Convert.ToBoolean(item.subdi_iactivo),
                            strEstado = (Convert.ToBoolean(item.subdi_iactivo)) ? "Activo" : "Inactivo"
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
        public int insertarSubDiario(ESubDiario obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_SUBDIARIO_INSERTAR(
                        obj.subdi_icod_subdiario,
                        obj.subdi_vdescripcion,
                        obj.subdi_iactivo,
                        obj.intUsuario
                        );
                    intIcod = obj.subdi_icod_subdiario;
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarSubDiario(ESubDiario obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_SUBDIARIO_MODIFICAR(
                        obj.subdi_icod_subdiario,
                        obj.subdi_vdescripcion,
                        obj.subdi_iactivo,
                        obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarSubDiario(ESubDiario obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_SUBDIARIO_ELIMINAR(
                        obj.subdi_icod_subdiario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Comprobantes
        /*Cabecera*/

        public int UltimoCorrelativoVoucherContableCab()
        {
            int? CORRELATIVO = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_CAB_ULTIMO_CORRELATVO(
                        ref CORRELATIVO
                        );
                }
                return Convert.ToInt32(CORRELATIVO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EVoucherContableCab> listarVoucherContableCab(int intEjercicio, int intPeriodo)
        {
            List<EVoucherContableCab> lista = new List<EVoucherContableCab>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGECONTA_VOUCHER_CONTABLE_CAB_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableCab()
                        {
                            anioc_iid_anio = item.anioc_iid_anio,
                            mesec_iid_mes = item.mesec_iid_mes,
                            vcocc_icod_vcontable = item.vcocc_icod_vcontable,
                            subdi_icod_subdiario = item.subdi_icod_subdiario,
                            vcocc_numero_vcontable = item.vcocc_numero_vcontable,
                            vcocc_fecha_vcontable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocc_glosa = item.vcocc_glosa,
                            vcocc_observacion = item.vcocc_observacion,
                            tarec_icorrelativo_situacion_vcontable = Convert.ToInt32(item.tarec_icorrelativo_situacion_vcontable),
                            tarec_icorrelativo_origen_vcontable = Convert.ToInt32(item.tarec_icorrelativo_origen_vcontable),
                            tablc_iid_moneda = Convert.ToInt32(item.tablc_iid_moneda),
                            strTipoMoneda2 = item.tablc_iid_moneda == 3 ? "S/." : "US$",
                            vcocc_tipo_cambio = Convert.ToDecimal(item.vcocc_tipo_cambio),
                            vcocc_nmto_tot_debe_sol = Convert.ToDecimal(item.vcocc_nmto_tot_debe_sol),
                            vcocc_nmto_tot_haber_sol = Convert.ToDecimal(item.vcocc_nmto_tot_haber_sol),
                            vcocc_nmto_tot_debe_dol = Convert.ToDecimal(item.vcocc_nmto_tot_debe_dol),
                            vcocc_nmto_tot_haber_dol = Convert.ToDecimal(item.vcocc_nmto_tot_haber_dol),
                            tbl_origen = item.tbl_origen,
                            tbl_origen_icod = item.tbl_origen_icod,
                            strTipoMoneda = item.strTipoMoneda,
                            strVcoSituacion = item.strVcoSituacion,
                            strNroVco = String.Format("{0:00}.{1:00000}", item.subdi_icod_subdiario, item.vcocc_numero_vcontable),
                            intMovimientos = Convert.ToInt32(item.intMovimientos),
                            strSubdiario = item.strSubdiario
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
        public List<EVoucherContableCab> getVoucherContableCab(string strOrigen, int intIcod)
        {
            List<EVoucherContableCab> lista = new List<EVoucherContableCab>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_GET_VOUCHER_CONTABLE_CAB(strOrigen, intIcod);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableCab()
                        {
                            anioc_iid_anio = item.anioc_iid_anio,
                            mesec_iid_mes = item.mesec_iid_mes,
                            vcocc_icod_vcontable = item.vcocc_icod_vcontable,
                            subdi_icod_subdiario = item.subdi_icod_subdiario,
                            vcocc_numero_vcontable = item.vcocc_numero_vcontable,
                            vcocc_fecha_vcontable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocc_glosa = item.vcocc_glosa,
                            vcocc_observacion = item.vcocc_observacion,
                            tarec_icorrelativo_situacion_vcontable = Convert.ToInt32(item.tarec_icorrelativo_situacion_vcontable),
                            tarec_icorrelativo_origen_vcontable = Convert.ToInt32(item.tarec_icorrelativo_origen_vcontable),
                            tablc_iid_moneda = Convert.ToInt32(item.tablc_iid_moneda),
                            vcocc_tipo_cambio = Convert.ToDecimal(item.vcocc_tipo_cambio),
                            vcocc_nmto_tot_debe_sol = Convert.ToDecimal(item.vcocc_nmto_tot_debe_sol),
                            vcocc_nmto_tot_haber_sol = Convert.ToDecimal(item.vcocc_nmto_tot_haber_sol),
                            vcocc_nmto_tot_debe_dol = Convert.ToDecimal(item.vcocc_nmto_tot_debe_dol),
                            vcocc_nmto_tot_haber_dol = Convert.ToDecimal(item.vcocc_nmto_tot_haber_dol),
                            tbl_origen = item.tbl_origen,
                            tbl_origen_icod = item.tbl_origen_icod,
                            strTipoMoneda = item.strTipoMoneda,
                            strVcoSituacion = item.strVcoSituacion,
                            strNroVco = String.Format("{0:00}.{1:00000}", item.subdi_icod_subdiario, item.vcocc_numero_vcontable),
                            intMovimientos = Convert.ToInt32(item.intMovimientos),
                            strSubdiario = item.strSubdiario
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
        public int insertarVoucherContableCab(EVoucherContableCab obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_CAB_INSERTAR(
                        obj.anioc_iid_anio,
                        obj.mesec_iid_mes,
                        ref intIcod,
                        obj.subdi_icod_subdiario,
                        obj.vcocc_numero_vcontable,
                        obj.vcocc_fecha_vcontable,
                        obj.vcocc_glosa,
                        obj.vcocc_observacion,
                        obj.tarec_icorrelativo_situacion_vcontable,
                        obj.tarec_icorrelativo_origen_vcontable,
                        obj.tablc_iid_moneda,
                        obj.vcocc_tipo_cambio,
                        obj.vcocc_nmto_tot_debe_sol,
                        obj.vcocc_nmto_tot_haber_sol,
                        obj.vcocc_nmto_tot_debe_dol,
                        obj.vcocc_nmto_tot_haber_dol,
                        obj.doxpc_viid_correlativo,
                        obj.intUsuario,
                        obj.strPc,
                        obj.tbl_origen,
                        obj.tbl_origen_icod
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVoucherContableCab(EVoucherContableCab obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_CAB_MODIFICAR(
                        obj.vcocc_icod_vcontable,
                        obj.vcocc_glosa,
                        obj.vcocc_observacion,
                        obj.tarec_icorrelativo_situacion_vcontable,
                        obj.vcocc_nmto_tot_debe_sol,
                        obj.vcocc_nmto_tot_haber_sol,
                        obj.vcocc_nmto_tot_debe_dol,
                        obj.vcocc_nmto_tot_haber_dol,
                        obj.intUsuario,
                        obj.strPc,
                        obj.tbl_origen,
                        obj.tbl_origen_icod);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVoucherContableCab(EVoucherContableCab obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_CAB_ELIMINAR(
                        obj.vcocc_icod_vcontable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVoucherContableCabPorOrigen(int intEjercicio, int intPeriodo, string strOrigen)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_POR_ORIGEN_ELIMINAR(intEjercicio, intPeriodo, strOrigen);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVoucherContableDet> listarVoucherContableDet(int intIcod)
        {
            List<EVoucherContableDet> lista = new List<EVoucherContableDet>(); ;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGECONTA_VOUCHER_CONTABLE_DET_LISTAR(intIcod);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            vcocd_icod_det = item.vcocd_icod_det,
                            vcocc_icod_vcontable = item.vcocc_icod_vcontable,
                            vcocd_nro_item_det = item.vcocd_nro_item_det,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            vcocd_numero_doc = item.vcocd_numero_doc,
                            ctacc_icod_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            cecoc_icod_centro_costo = item.cecoc_icod_centro_costo,
                            tablc_iid_tipo_analitica = item.tablc_iid_tipo_analitica,
                            anad_icod_analitica = item.anad_icod_analitica,
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            tablc_iid_moneda = Convert.ToInt32(item.tablc_iid_moneda),
                            vcocd_tipo_cambio = Convert.ToDecimal(item.vcocd_tipo_cambio),
                            vcocd_nmto_tot_debe_sol = Convert.ToDecimal(item.vcocd_nmto_tot_debe_sol),
                            vcocd_nmto_tot_haber_sol = Convert.ToDecimal(item.vcocd_nmto_tot_haber_sol),
                            vcocd_nmto_tot_debe_dol = Convert.ToDecimal(item.vcocd_nmto_tot_debe_dol),
                            vcocd_nmto_tot_haber_dol = Convert.ToDecimal(item.vcocd_nmto_tot_haber_dol),
                            ctacc_iid_cuenta_contable_ref = item.ctacc_iid_cuenta_contable_ref,
                            strTipNroDocumento = String.Format("{0}-{1}", item.strTipoDoc, item.vcocd_numero_doc),
                            strTipoDoc = item.strTipoDoc,
                            strNroCuenta = item.strNroCuenta,
                            strDesCuenta = item.strDesCuenta,
                            strCodCCosto = item.strCodCCosto,
                            strDesCCosto = item.strDesCCosto,
                            strTipoAnalitica = item.strTipoAnalitica,
                            strCodAnaliica = item.strCodAnaliica,
                            strDesAnalitica = item.strDesAnalitica,
                            strAnalisis = (item.tablc_iid_tipo_analitica != null) ? String.Format("{0:00}.{1}", item.tablc_iid_tipo_analitica, item.strCodAnaliica) : ""
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
        public void insertarVoucherContableDet(EVoucherContableDet obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_DET_INSERTAR(
                        obj.vcocc_icod_vcontable,
                        obj.vcocd_nro_item_det,
                        obj.tdocc_icod_tipo_doc,
                        obj.vcocd_numero_doc,
                        obj.ctacc_icod_cuenta_contable,
                        obj.cecoc_icod_centro_costo,
                        obj.tablc_iid_tipo_analitica,
                        obj.anad_icod_analitica,
                        obj.vcocd_vglosa_linea,
                        obj.tablc_iid_moneda,
                        obj.vcocd_tipo_cambio,
                        obj.vcocd_nmto_tot_debe_sol,
                        obj.vcocd_nmto_tot_haber_sol,
                        obj.vcocd_nmto_tot_debe_dol,
                        obj.vcocd_nmto_tot_haber_dol,
                        obj.intUsuario,
                        obj.strPc,
                        obj.ctacc_iid_cuenta_contable_ref,
                        obj.tarec_icorrelativo_origen_vcontable
                        //obj.vcocd_vnumero_serie,
                        //obj.vcocd_vnumero_correlativo
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVoucherContableDet(EVoucherContableDet obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_DET_MODIFICAR(
                        obj.vcocd_icod_det,
                        obj.tdocc_icod_tipo_doc,
                        obj.vcocd_numero_doc,
                        obj.vcocd_vglosa_linea,
                        obj.vcocd_nmto_tot_debe_sol,
                        obj.vcocd_nmto_tot_haber_sol,
                        obj.vcocd_nmto_tot_debe_dol,
                        obj.vcocd_nmto_tot_haber_dol,
                        obj.intUsuario,
                        obj.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVoucherContableDet(EVoucherContableDet obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_DET_ELIMINAR(
                        obj.vcocd_icod_det);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*Porcesos*/
        public void actualizarSituacionVoucher(int intIcod)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_ACTUALIZAR_SITUACION(intIcod);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void redondearVoucher(int intIcod, int intCaso, int intUsuario, string strPC)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGECONTA_VOUCHER_CONTABLE_REDONDEO(intIcod, intCaso, intUsuario, strPC);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EVoucherContableCab> eliminarRedondeoVoucher(int intEjercicio, int intMes)
        {
            List<EVoucherContableCab> lista = new List<EVoucherContableCab>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGECONTA_VOUCHER_CONTABLE_REDONDEO_ELIMINAR(intEjercicio, intMes);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableCab()
                        {
                            vcocc_icod_vcontable = Convert.ToInt32(item.iid_voucher_contable)
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

        public List<EComprobante> ListarComprobantesNoCuadrados(int intEjercicio)
        {
            List<EComprobante> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EComprobante>();
                    var query = dc.SGE_VOUCHER_CONTABLE_NO_CUADRADOS_CAB_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new EComprobante()
                        {
                            iid_anio = item.anioc_iid_anio,
                            iid_mes = item.mesec_iid_mes,
                            iid_voucher_contable = item.vcocc_icod_vcontable,
                            subdi_icod_subdiario = item.subdi_icod_subdiario,
                            sfec_nota_contable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vglosa = item.vcocc_vglosa,
                            vobservacion = item.vcocc_observacion,
                            vnumero_voucher_contable = item.vcocc_numero_vcontable,
                            iid_situacion_voucher_contable = item.tarec_icorrelativo_situacion_vcontable,
                            iid_origen_voucher_contable = item.tarec_icorrelativo_origen_vcontable,
                            tablc_iid_moneda = item.tablc_iid_moneda,
                            //tablc_iid_tipo_cambio = item.vcocc_tipo_cambio,
                            decimal_tipoCambio = Convert.ToDecimal(item.vcocc_tipo_cambio),
                            nmto_tot_debe_sol = item.vcocc_nmto_tot_debe_sol,
                            nmto_tot_haber_sol = item.vcocc_nmto_tot_haber_sol,
                            nmto_tot_debe_dol = item.vcocc_nmto_tot_debe_dol,
                            nmto_tot_haber_dol = item.vcocc_nmto_tot_haber_dol,
                            subdiario_des = item.subdiario_des,
                            //iid_dia = Convert.ToInt32(item.dia),
                            vsituacion_voucher = item.Estado,
                            tipocambio = Convert.ToString(item.tipocambio),
                            vmoneda = item.tipomoneda,
                            Movimiento = Convert.ToInt32(item.movimientos),
                            strNroVco = String.Format("{0:00}.{1:00000}", item.subdi_icod_subdiario, item.vcocc_numero_vcontable)
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
        #endregion
        #region Diferencia de Cambio
        public List<EVoucherContableDet> listarDiferenciaCambio(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGECONTA_DIFERENCIA_CAMBIO_CUENTAS(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anad_iid_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            strCodCCosto = item.cecoc_vcodigo_centro_costo,
                            dblCuentaSaldoAntSol = Convert.ToDecimal(item.mto_sal_anterior_sol),
                            dblCuentaSaldoAntDol = Convert.ToDecimal(item.mto_sal_anterior_dol),
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anad_iid_analitica : "",
                            strAnalCCosto = (item.anad_vdescripcion != null) ? item.anad_vdescripcion : (item.cecoc_vdescripcion != null) ? item.cecoc_vdescripcion : ""
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

        public List<EVoucherContableDet> listarDiferenciaCambio_3(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGECONTA_DIFERENCIA_CAMBIO_CUENTAS_3(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anad_iid_analitica,
                            //cecoc_icod_centro_costo = item.icod_centro_costo,
                            //strCodCCosto = item.cecoc_vcodigo_centro_costo,
                            dblCuentaSaldoAntSol = Convert.ToDecimal(item.mto_sal_anterior_sol),
                            dblCuentaSaldoAntDol = Convert.ToDecimal(item.mto_sal_anterior_dol),
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anad_iid_analitica : "",
                            //strAnalCCosto = (item.anad_vdescripcion != null) ? item.anad_vdescripcion : (item.cecoc_vdescripcion != null) ? item.cecoc_vdescripcion : ""
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
        public List<EVoucherContableDet> listarDiferenciaCambio_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGECONTA_DIFERENCIA_CAMBIO_CUENTAS_2(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            vcocd_nmto_tot_debe_sol = Convert.ToDecimal(item.vcocd_nmto_tot_debe_sol),
                            vcocd_nmto_tot_haber_sol = Convert.ToDecimal(item.vcocd_nmto_tot_haber_sol),
                            vcocd_nmto_tot_debe_dol = Convert.ToDecimal(item.vcocd_nmto_tot_debe_dol),
                            vcocd_nmto_tot_haber_dol = Convert.ToDecimal(item.vcocd_nmto_tot_haber_dol),
                            vcocd_numero_doc = item.vcodc_vnumero_documento,
                            dtFechaVContable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = Convert.ToDecimal(item.vcocc_tipo_cambio),
                            strNroVContable = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det)
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
        public List<EVoucherContableDet> listarDiferenciaCambio_4(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGECONTA_DIFERENCIA_CAMBIO_CUENTAS_4(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            vcocd_nmto_tot_debe_sol = Convert.ToDecimal(item.vcocd_nmto_tot_debe_sol),
                            vcocd_nmto_tot_haber_sol = Convert.ToDecimal(item.vcocd_nmto_tot_haber_sol),
                            vcocd_nmto_tot_debe_dol = Convert.ToDecimal(item.vcocd_nmto_tot_debe_dol),
                            vcocd_nmto_tot_haber_dol = Convert.ToDecimal(item.vcocd_nmto_tot_haber_dol),
                            vcocd_numero_doc = item.vcodc_vnumero_documento,
                            dtFechaVContable = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = Convert.ToDecimal(item.vcocc_tipo_cambio),
                            strNroVContable = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det)
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
        #endregion
        #region Mayores
        public List<EVoucherContableDet> listarMayorAuxiliarMensual(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_MENSUAL(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intOpcion);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anad_iid_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            strCodCCosto = item.cecoc_Vcodigo_centro_costo,
                            ctacc_iid_cuenta_contable_acumulado_sol = item.mto_sal_anterior_sol,
                            ctacc_iid_cuenta_contable_acumulado_dol = item.mto_sal_anterior_dol,
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anad_iid_analitica : "",
                            anac_cecoc_vdescripcion = (item.anad_vdescripcion != null) ? item.anad_vdescripcion : (item.cecoc_vdescripcion != null) ? item.cecoc_vdescripcion : ""
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
        public List<EVoucherContableDet> listarMayorAuxiliarMensual_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_MENSUAL_02(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intOpcion);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            vcocd_nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            vcocd_numero_doc = item.vcocd_numero_doc,
                            fec_cab = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = item.vcocc_tipo_cambio,
                            iid_subdiario_vnum_voucher = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det)
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
        public List<EVoucherContableDet> listarMayorAuxiliarDetallado(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_DETALLADO(intEjercicio, intMes, intCuentaInicio, intCuentaFin, intOpcion);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            ctacc_iid_cuenta_contable_acumulado_sol = item.mto_sal_anterior_sol,
                            ctacc_iid_cuenta_contable_acumulado_dol = item.mto_sal_anterior_dol
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
        public List<EVoucherContableDet> listarBalanceComprobacion(int intEjercicio, int intMes, int intMoneda, int intDigitos, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_BALANCE_COMPROBACION_LISTAR(intEjercicio, intMes, intMoneda, intDigitos, intOpcion);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            ctacc_iid_cuenta_contable_acumulado_sol = item.mto_sal_anterior_sol,
                            cta_padre = item.cta_iid__padre,
                            cta_vdespadre = item.cta_vdes__padre
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
        public List<EVoucherContableDet> listarBalanceComprobacion_2(int intEjercicio, int intMes, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_BALANCE_COMPROBACION_LISTAR_2(intEjercicio, intMes, intOpcion);
                    foreach (var item in query)
                    {

                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anad_iid_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            strCodCCosto = item.cecoc_vcodigo_centro_costo,
                            vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            vcocd_nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            vcocd_numero_doc = item.vcocd_numero_doc,
                            fec_cab = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = item.vcocc_tipo_cambio,
                            iid_subdiario_vnum_voucher = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det),
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anad_iid_analitica : ""
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
        public List<EVoucherContableDet> listarMayorCCostoMensual(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin,
            string intCCostoInicio, string intCCostoFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_CCOSTO_MENSUAL(intEjercicio, intMes, intOpcion, intCuentaInicio, intCuentaFin, intCCostoInicio, intCCostoFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            strCodCCosto = item.cecoc_vcodigo_centro_costo,
                            ctacc_iid_cuenta_contable_acumulado_sol = item.mto_sal_anterior_sol,
                            ctacc_iid_cuenta_contable_acumulado_dol = item.mto_sal_anterior_dol,
                            anac_cecoc_vdescripcion = item.cecoc_vdescripcion
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
        public List<EVoucherContableDet> listarMayorCCostoMensual_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin,
            string intCCostoInicio, string intCCostoFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_CCOSTO_MENSUAL_2(intEjercicio, intMes, intOpcion, intCuentaInicio, intCuentaFin, intCCostoInicio, intCCostoFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anac_iid_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            strCodCCosto = item.cecoc_vcodigo_centro_costo,
                            vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            vcocd_nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            vcocd_numero_doc = item.vcocd_numero_doc,
                            fec_cab = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = item.vcocc_tipo_cambio,
                            iid_subdiario_vnum_voucher = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det),
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anac_iid_analitica : ""
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

        public List<EVoucherContableDet> listarMayorCCostoMensual_Balance(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin,
           string intCCostoInicio, string intCCostoFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_CCOSTO_MENSUAL_BALANCE(intEjercicio, intMes, intOpcion, intCuentaInicio, intCuentaFin, intCCostoInicio, intCCostoFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anac_iid_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            strCodCCosto = item.cecoc_vcodigo_centro_costo,
                            vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            vcocd_nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            vcocd_numero_doc = item.vcocd_numero_doc,
                            fec_cab = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = item.vcocc_tipo_cambio,
                            iid_subdiario_vnum_voucher = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det),
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anac_iid_analitica : ""
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

        public List<EVoucherContableDet> listarMayorAnaliticaMensual(int intEjercicio, int intMes,
     int? intTipoAnalitica, string strAnaliticaInicio, string strAnaliticaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 90000;
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_ANALITICAS_MENSUAL(intEjercicio, intMes, intOpcion, intTipoAnalitica, strAnaliticaInicio, strAnaliticaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anad_iid_analitica,
                            ctacc_iid_cuenta_contable_acumulado_sol = item.mto_sal_anterior_sol,
                            ctacc_iid_cuenta_contable_acumulado_dol = item.mto_sal_anterior_dol,
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anad_iid_analitica : "",
                            anac_cecoc_vdescripcion = item.anad_vdescripcion,
                            tablc_iid_tipo_moneda_CB = Convert.ToInt32(item.tablc_iid_tipo_moneda)
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
        public List<EVoucherContableDet> listarMayorAnaliticaMensual_2(int intEjercicio, int intMes,
           int? intTipoAnalitica, string strAnaliticaInicio, string strAnaliticaFin, int intOpcion)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.CommandTimeout = 90000;
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_ANALITICAS_MENSUAL_2(intEjercicio, intMes, intOpcion, intTipoAnalitica, strAnaliticaInicio, strAnaliticaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            tablc_iid_tipo_analitica = item.iid_tipo_analitica,
                            anad_icod_analitica = item.icod_analitica,
                            strCodAnaliica = item.anad_iid_analitica,
                            cecoc_icod_centro_costo = item.icod_centro_costo,
                            anac_cecoc_vdescripcion = item.anad_vdescripcion,
                            vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            vcocd_nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            vcocd_numero_doc = item.vcocd_numero_doc,
                            fec_cab = Convert.ToDateTime(item.vcocc_fecha_vcontable),
                            vcocd_vglosa_linea = item.vcocd_vglosa_linea,
                            strMonedaVContable = item.vmoneda,
                            vcocd_tipo_cambio = item.vcocc_tipo_cambio,
                            iid_subdiario_vnum_voucher = item.vnumero_voucher,
                            vcocd_nro_item_det = Convert.ToInt32(item.vcocd_nro_item_det),
                            tablc_iid_tipo_moneda_CB = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            strAnalisis = (item.iid_tipo_analitica != 0) ? String.Format("{0:00}", item.iid_tipo_analitica) + "." + item.anad_iid_analitica : ""

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
        public List<EVoucherContableDet> listarMayorAuxiliarResumen(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_RESUMEN(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = item.icod_cuenta_contable,
                            strNroCuenta = item.ctacc_numero_cuenta_contable,
                            strDesCuenta = item.ctacc_nombre_descripcion,
                            id_mes = item.iid_mes
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
        public List<EVoucherContableDet> listarMayorAuxiliarResumen_2(int intEjercicio, int intMes, int? intCuentaInicio, int? intCuentaFin)
        {
            List<EVoucherContableDet> lista = null;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    lista = new List<EVoucherContableDet>();
                    var query = dc.SGE_MAYO_AUXILIAR_RESUMEN_2(intEjercicio, intMes, intCuentaInicio, intCuentaFin);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableDet()
                        {
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            vcocd_nmto_tot_debe_sol = item.vcocd_nmto_tot_debe_sol,
                            vcocd_nmto_tot_haber_sol = item.vcocd_nmto_tot_haber_sol,
                            vcocd_nmto_tot_debe_dol = item.vcocd_nmto_tot_debe_dol,
                            vcocd_nmto_tot_haber_dol = item.vcocd_nmto_tot_haber_dol,
                            id_mes = item.mesec_iid_mes,
                            id_subdiario = item.subdi_icod_subdiario
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

        #endregion
        public int getVoucherContableCabCorrelativo(int intEjercicio, int intPeriodo, int intSubdiario)
        {
            int? intCorrelativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_VOUCHER_CAB_GET_CORRELATIVO(
                        intEjercicio,
                        intPeriodo,
                        intSubdiario,
                        ref intCorrelativo
                        );
                }
                return Convert.ToInt32(intCorrelativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal getTipoCambioPorFecha(DateTime dtFecha)
        {
            decimal? dblTC = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_GET_TIPO_CAMBIO_POR_FECHA(
                        ref dblTC,
                        dtFecha
                        );
                }
                return Convert.ToDecimal(dblTC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal getTipoCambioPorMes(int dtFecha)
        {
            decimal? dblTC = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGET_GET_TIPO_CAMBIO_MENSUAL_LISTAR(
                        ref dblTC,
                        dtFecha
                        );
                }
                return Convert.ToDecimal(dblTC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Registro de Compras
        public List<ERegistroCompras> ListarRegistroDeComprasGeneral(int anio, int mes)
        {
            int cont = 0;

            List<ERegistroCompras> lista = new List<ERegistroCompras>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_REGISTRO_COMPRAS_LISTAR_GENERAL(anio, mes);
                    foreach (var item in query)
                    {
                        ERegistroCompras _BE = new ERegistroCompras();
                        cont = cont + 1;
                        _BE.doxpc_icod_correlativo = item.doxpc_icod_correlativo;
                        _BE.str_doxpc_icod_correlativo = string.Format("{0:000000}", item.doxpc_icod_correlativo);
                        _BE.doxpc_viid_correlativo = string.Format("{0:00000}", item.doxpc_iid_correlativo); //código pero con formato string para mostrarlo en pantalla
                        _BE.tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc;
                        _BE.tdodc_iid_correlativo = item.tdocd_iid_correlativo;
                        _BE.doxpc_vnumero_doc = (item.doxpc_vnumero_doc.Contains("") || item.doxpc_numdoc_tipo == 2) ? item.doxpc_vnumero_doc : (item.doxpc_vnumero_doc.Substring(0, 4) + item.doxpc_vnumero_doc.Substring(4));
                        //doxpc_vnumero_doc = item.doxpc_vnumero_doc 
                        _BE.doxpc_sfecha_doc = item.doxpc_sfecha_doc;
                        _BE.proc_vcod_proveedor = item.proc_vcod_proveedor;
                        _BE.proc_vnombrecompleto = item.proc_vnombrecompleto;
                        _BE.simboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/ ." : "US$";
                        _BE.doxpc_nmonto_tipo_cambio = item.doxpc_nmonto_tipo_cambio;

                        //montos
                        _BE.doxpc_nmonto_destino_gravado = item.doxpc_nmonto_destino_gravado;
                        _BE.doxpc_nmonto_destino_mixto = item.doxpc_nmonto_destino_mixto;
                        _BE.doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado;
                        _BE.doxpc_nmonto_nogravado = item.doxpc_nmonto_nogravado;
                        _BE.doxpc_nmonto_referencial_cif = item.doxpc_nmonto_referencial_cif;
                        _BE.doxpc_nmonto_servicio_no_domic = item.doxpc_nmonto_servicio_no_domic;
                        _BE.valor_compra = item.valor_compra;
                        _BE.doxpc_nmonto_imp_destino_gravado = item.doxpc_nmonto_imp_destino_gravado;
                        _BE.doxpc_nmonto_imp_destino_mixto = item.doxpc_nmonto_imp_destino_mixto;
                        _BE.doxpc_nmonto_imp_destino_nogravado = item.doxpc_nmonto_imp_destino_nogravado;
                        _BE.doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento;
                        _BE.doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado;
                        _BE.doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo;
                        //montos para el reporte

                        _BE.situacion_documento = item.situacion_documento;
                        _BE.doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc;
                        _BE.doxpc_vnro_deposito_detraccion = (item.doxpc_vnro_deposito_detraccion != null) ? item.doxpc_vnro_deposito_detraccion : null;
                        _BE.doxpc_sfec_deposito_detraccion = (!string.IsNullOrWhiteSpace(item.doxpc_vnro_deposito_detraccion)) ? item.doxpc_sfec_deposito_detraccion : null;


                        _BE.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                        _BE.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                        _BE.tdocc_vcodigo_tipo_doc_sunat = item.tdocc_vcodigo_tipo_doc_sunat;
                        _BE.tip_doc_proveedor = item.tip_doc_proveedor.ToString();
                        _BE.num_doc_proveedor = item.num_doc_proveedor;
                        _BE.tdocc_vdescripcion = item.tdocc_vdescripcion;

                        //nota de crédito
                        _BE.nc_dxc_tipodoc = item.nc_dxc_numdoc;
                        //nc_dxc_numdoc = item.nc_dxc_numdoc,

                        _BE.doxpc_tipo_comprobante_referencia = item.doxpc_tipo_comprobante_referencia;
                        _BE.doxpc_num_serie_referencia = item.doxpc_num_serie_referencia;
                        _BE.doxpc_num_comprobante_referencia = item.doxpc_num_comprobante_referencia;
                        _BE.doxpc_sfecha_emision_referencia = item.doxpc_sfecha_emision_referencia;
                        _BE.doxpc_vnumero_serio = item.doxpc_vnumero_serio;
                        _BE.doxpc_vnumero_correlativo = item.doxpc_vnumero_correlativo;
                        _BE.doxpc_numdoc_tipo = item.doxpc_numdoc_tipo;
                        _BE.vcocc_numero_vcontable = item.vcocc_numero_vcontable;
                        _BE.subdi_icod_subdiario = Convert.ToInt32(item.subdi_icod_subdiario);
                        _BE.CUO = anio.ToString() + string.Format("{0:00}", mes) + string.Format("{0:00}", _BE.subdi_icod_subdiario) + string.Format("{0:0000}", Convert.ToInt32(item.doxpc_iid_correlativo));
                        _BE.ViddAdquisicion = item.ViddAdquisicion.ToString();
                        _BE.doxpc_iid_correlativo = Convert.ToInt32(item.doxpc_iid_correlativo);
                        /*DUA*/
                        _BE.doxpc_codigo_aduana = item.doxpc_codigo_aduana;
                        _BE.doxpc_anio = item.doxpc_anio;
                        _BE.doxpc_numero_declaracion = item.doxpc_numero_declaracion;
                        _BE.MontoDUA = Convert.ToDecimal(item.MontoDUA);
                        lista.Add(_BE);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                cont = cont;

            }
            return lista;
        }
        public List<ERegistroCompras> ListarRegistroDeCompras(int anio, int mes)
        {
            int cont = 0;

            List<ERegistroCompras> lista = new List<ERegistroCompras>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_REGISTRO_COMPRAS_LISTAR(anio, mes);
                    foreach (var item in query)
                    {
                        ERegistroCompras _BE = new ERegistroCompras();
                        cont = cont + 1;
                        _BE.doxpc_icod_correlativo = item.doxpc_icod_correlativo;
                        _BE.str_doxpc_icod_correlativo = string.Format("{0:000000}", item.doxpc_icod_correlativo);
                        _BE.doxpc_viid_correlativo = string.Format("{0:00000}", item.doxpc_iid_correlativo); //código pero con formato string para mostrarlo en pantalla
                        _BE.tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc;
                        _BE.tdodc_iid_correlativo = item.tdocd_iid_correlativo;
                        _BE.doxpc_vnumero_doc = (item.doxpc_vnumero_doc.Contains("") || item.doxpc_numdoc_tipo == 2) ? item.doxpc_vnumero_doc : (item.doxpc_vnumero_doc.Substring(0, 4) + item.doxpc_vnumero_doc.Substring(4));
                        //doxpc_vnumero_doc = item.doxpc_vnumero_doc 
                        _BE.doxpc_sfecha_doc = item.doxpc_sfecha_doc;
                        _BE.proc_vcod_proveedor = item.proc_vcod_proveedor;
                        _BE.proc_vnombrecompleto = item.proc_vnombrecompleto;
                        _BE.simboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/ ." : "US$";
                        _BE.doxpc_nmonto_tipo_cambio = item.doxpc_nmonto_tipo_cambio;

                        //montos
                        _BE.doxpc_nmonto_destino_gravado = item.doxpc_nmonto_destino_gravado;
                        _BE.doxpc_nmonto_destino_mixto = item.doxpc_nmonto_destino_mixto;
                        _BE.doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado;
                        _BE.doxpc_nmonto_nogravado = item.doxpc_nmonto_nogravado;
                        _BE.doxpc_nmonto_referencial_cif = item.doxpc_nmonto_referencial_cif;
                        _BE.doxpc_nmonto_servicio_no_domic = item.doxpc_nmonto_servicio_no_domic;
                        _BE.valor_compra = item.valor_compra;
                        _BE.doxpc_nmonto_imp_destino_gravado = item.doxpc_nmonto_imp_destino_gravado;
                        _BE.doxpc_nmonto_imp_destino_mixto = item.doxpc_nmonto_imp_destino_mixto;
                        _BE.doxpc_nmonto_imp_destino_nogravado = item.doxpc_nmonto_imp_destino_nogravado;
                        _BE.doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento;
                        _BE.doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado;
                        _BE.doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo;
                        //montos para el reporte

                        _BE.situacion_documento = item.situacion_documento;
                        _BE.doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc;
                        _BE.doxpc_vnro_deposito_detraccion = (item.doxpc_vnro_deposito_detraccion != null) ? item.doxpc_vnro_deposito_detraccion : null;
                        _BE.doxpc_sfec_deposito_detraccion = (!string.IsNullOrWhiteSpace(item.doxpc_vnro_deposito_detraccion)) ? item.doxpc_sfec_deposito_detraccion : null;


                        _BE.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                        _BE.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                        _BE.tdocc_vcodigo_tipo_doc_sunat = item.tdocc_vcodigo_tipo_doc_sunat;
                        _BE.tip_doc_proveedor = item.tip_doc_proveedor.ToString();
                        _BE.num_doc_proveedor = item.num_doc_proveedor;
                        _BE.tdocc_vdescripcion = item.tdocc_vdescripcion;

                        //nota de crédito
                        _BE.nc_dxc_tipodoc = item.nc_dxc_numdoc;
                        //nc_dxc_numdoc = item.nc_dxc_numdoc,

                        _BE.doxpc_tipo_comprobante_referencia = item.doxpc_tipo_comprobante_referencia;
                        _BE.doxpc_num_serie_referencia = item.doxpc_num_serie_referencia;
                        _BE.doxpc_num_comprobante_referencia = item.doxpc_num_comprobante_referencia;
                        _BE.doxpc_sfecha_emision_referencia = item.doxpc_sfecha_emision_referencia;
                        _BE.doxpc_vnumero_serio = item.doxpc_vnumero_serio;
                        _BE.doxpc_vnumero_correlativo = item.doxpc_vnumero_correlativo;
                        _BE.doxpc_numdoc_tipo = item.doxpc_numdoc_tipo;
                        _BE.vcocc_numero_vcontable = item.vcocc_numero_vcontable;
                        _BE.subdi_icod_subdiario = Convert.ToInt32(item.subdi_icod_subdiario);
                        _BE.CUO = anio.ToString() + string.Format("{0:00}", mes) + string.Format("{0:00}", _BE.subdi_icod_subdiario) + string.Format("{0:0000}", Convert.ToInt32(item.doxpc_iid_correlativo));
                        _BE.ViddAdquisicion = item.ViddAdquisicion.ToString();
                        _BE.doxpc_iid_correlativo = Convert.ToInt32(item.doxpc_iid_correlativo);
                        /*DUA*/
                        _BE.doxpc_codigo_aduana = item.doxpc_codigo_aduana;
                        _BE.doxpc_anio = item.doxpc_anio;
                        _BE.doxpc_numero_declaracion = item.doxpc_numero_declaracion;
                        _BE.MontoDUA = Convert.ToDecimal(item.MontoDUA);
                        lista.Add(_BE);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                cont = cont;

            }
            return lista;
        }
        public List<ERegistroCompras> ListarRegistroDeComprasNoDomic(int anio, int mes)
        {
            int cont = 0;

            List<ERegistroCompras> lista = new List<ERegistroCompras>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_REGISTRO_COMPRAS_NO_DOMIC_LISTAR(anio, mes);
                    foreach (var item in query)
                    {
                        ERegistroCompras _BE = new ERegistroCompras();
                        cont = cont + 1;
                        _BE.doxpc_icod_correlativo = item.doxpc_icod_correlativo;
                        _BE.str_doxpc_icod_correlativo = string.Format("{0:000000}", item.doxpc_icod_correlativo);
                        _BE.doxpc_viid_correlativo = string.Format("{0:00000}", item.doxpc_iid_correlativo); //código pero con formato string para mostrarlo en pantalla
                        _BE.tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc;
                        _BE.tdodc_iid_correlativo = item.tdocd_iid_correlativo;
                        _BE.doxpc_vnumero_doc = (item.doxpc_vnumero_doc.Contains("") || item.doxpc_numdoc_tipo == 2) ? item.doxpc_vnumero_doc : (item.doxpc_vnumero_doc.Substring(0, 4) + item.doxpc_vnumero_doc.Substring(4));
                        //doxpc_vnumero_doc = item.doxpc_vnumero_doc 
                        _BE.doxpc_sfecha_doc = item.doxpc_sfecha_doc;
                        _BE.proc_vcod_proveedor = item.proc_vcod_proveedor;
                        _BE.proc_vnombrecompleto = item.proc_vnombrecompleto;
                        _BE.simboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/ ." : "US$";
                        _BE.doxpc_nmonto_tipo_cambio = item.doxpc_nmonto_tipo_cambio;

                        //montos
                        _BE.doxpc_nmonto_destino_gravado = item.doxpc_nmonto_destino_gravado;
                        _BE.doxpc_nmonto_destino_mixto = item.doxpc_nmonto_destino_mixto;
                        _BE.doxpc_nmonto_destino_nogravado = item.doxpc_nmonto_destino_nogravado;
                        _BE.doxpc_nmonto_nogravado = item.doxpc_nmonto_nogravado;
                        _BE.doxpc_nmonto_referencial_cif = item.doxpc_nmonto_referencial_cif;
                        _BE.doxpc_nmonto_servicio_no_domic = item.doxpc_nmonto_servicio_no_domic;
                        _BE.valor_compra = item.valor_compra;
                        _BE.doxpc_nmonto_imp_destino_gravado = item.doxpc_nmonto_imp_destino_gravado;
                        _BE.doxpc_nmonto_imp_destino_mixto = item.doxpc_nmonto_imp_destino_mixto;
                        _BE.doxpc_nmonto_imp_destino_nogravado = item.doxpc_nmonto_imp_destino_nogravado;
                        _BE.doxpc_nmonto_total_documento = item.doxpc_nmonto_total_documento;
                        _BE.doxpc_nmonto_total_pagado = item.doxpc_nmonto_total_pagado;
                        _BE.doxpc_nmonto_total_saldo = item.doxpc_nmonto_total_saldo;
                        //montos para el reporte

                        _BE.situacion_documento = item.situacion_documento;
                        _BE.doxpc_sfecha_vencimiento_doc = item.doxpc_sfecha_vencimiento_doc;
                        _BE.doxpc_vnro_deposito_detraccion = (item.doxpc_vnro_deposito_detraccion != null) ? item.doxpc_vnro_deposito_detraccion : null;
                        _BE.doxpc_sfec_deposito_detraccion = (!string.IsNullOrWhiteSpace(item.doxpc_vnro_deposito_detraccion)) ? item.doxpc_sfec_deposito_detraccion : null;


                        _BE.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                        _BE.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                        _BE.tdocc_vcodigo_tipo_doc_sunat = item.tdocc_vcodigo_tipo_doc_sunat;
                        _BE.tip_doc_proveedor = item.tip_doc_proveedor.ToString();
                        _BE.num_doc_proveedor = item.num_doc_proveedor;
                        _BE.tdocc_vdescripcion = item.tdocc_vdescripcion;

                        //nota de crédito
                        _BE.nc_dxc_tipodoc = item.nc_dxc_numdoc;
                        //nc_dxc_numdoc = item.nc_dxc_numdoc,

                        _BE.doxpc_tipo_comprobante_referencia = item.doxpc_tipo_comprobante_referencia;
                        _BE.doxpc_num_serie_referencia = item.doxpc_num_serie_referencia;
                        _BE.doxpc_num_comprobante_referencia = item.doxpc_num_comprobante_referencia;
                        _BE.doxpc_sfecha_emision_referencia = item.doxpc_sfecha_emision_referencia;
                        _BE.doxpc_vnumero_serio = item.doxpc_vnumero_serio;
                        _BE.doxpc_vnumero_correlativo = item.doxpc_vnumero_correlativo;
                        _BE.doxpc_numdoc_tipo = item.doxpc_numdoc_tipo;
                        _BE.vcocc_numero_vcontable = item.vcocc_numero_vcontable;
                        _BE.subdi_icod_subdiario = Convert.ToInt32(item.subdi_icod_subdiario);
                        _BE.CUO = anio.ToString() + string.Format("{0:00}", mes) + string.Format("{0:00}", _BE.subdi_icod_subdiario) + string.Format("{0:0000}", Convert.ToInt32(item.doxpc_iid_correlativo));
                        _BE.ViddAdquisicion = item.ViddAdquisicion.ToString();
                        _BE.doxpc_iid_correlativo = Convert.ToInt32(item.doxpc_iid_correlativo);
                        _BE.doxpc_codigo_aduana = item.doxpc_codigo_aduana;
                        _BE.doxpc_anio = item.doxpc_anio;
                        _BE.doxpc_numero_declaracion = item.doxpc_numero_declaracion;
                        _BE.proc_pais_nodomic = Convert.ToInt32(item.proc_pais_nodomic);
                        _BE.proc_vdireccion = item.proc_vdireccion;
                        _BE.proc_vdni = item.proc_vdni;
                        lista.Add(_BE);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                cont = cont;

            }
            return lista;
        }
        #endregion
        #region Registro de Ventas
        public List<ERegistroVentas> ListarRegistroDeVentas(int anio, int mes)
        {
            List<ERegistroVentas> lista = new List<ERegistroVentas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_REGISTRO_VENTAS_LISTAR(anio, mes);
                    foreach (var item in query)
                    {

                        ERegistroVentas _be = new ERegistroVentas();
                        _be.doxcc_icod_correlativo = item.doxcc_icod_correlativo;
                        _be.str_doxcc_icod_correlativo = string.Format("{0:000000}", item.doxcc_icod_correlativo);
                        _be.tdocc_vcodigo_tipo_doc_sunat = item.tdocc_vcodigo_tipo_doc_sunat;
                        _be.tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc;
                        //doxcc_vnumero_doc = (item.doxcc_vnumero_doc.Contains("-")) ? item.doxcc_vnumero_doc : (item.doxcc_vnumero_doc.Substring(0, 3) + "-" + item.doxcc_vnumero_doc.Substring(3)),
                        _be.doxcc_vnumero_doc = item.doxcc_vnumero_doc;
                        _be.doxcc_sfecha_doc = item.doxcc_sfecha_doc;
                        _be.str_doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yy");
                        _be.cliec_icod_cliente = item.cliec_icod_cliente;
                        _be.cliec_vcod_cliente = item.cliec_vcod_cliente;
                        _be.cliec_vnombre_cliente = item.cliec_vnombre_cliente;
                        _be.tablc_iid_tipo_moneda = item.tablc_iid_tipo_moneda;
                        _be.simboloMoneda = (item.tablc_iid_tipo_moneda == 3) ? "S/ ." : "US$";
                        _be.doxcc_nmonto_tipo_cambio = item.doxcc_nmonto_tipo_cambio;
                        _be.doxcc_nporcentaje_igv = item.doxcc_nporcentaje_igv;
                        //montos
                        _be.doxcc_nmonto_afecto = item.doxcc_nmonto_afecto;
                        _be.doxcc_nmonto_inafecto = item.doxcc_nmonto_inafecto;
                        _be.valor_venta = item.valor_venta;
                        _be.doxcc_nmonto_impuesto = item.doxcc_nmonto_impuesto;
                        _be.doxcc_nmonto_total = item.doxcc_nmonto_total;

                        _be.tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc;
                        _be.doxcc_sfecha_vencimiento_doc = item.doxcc_sfecha_vencimiento_doc;
                        _be.str_doxcc_sfecha_vencimiento_doc = Convert.ToDateTime(item.doxcc_sfecha_vencimiento_doc).ToString("dd/MM/yy");

                        _be.num_doc_cliente = item.num_doc_cliente;

                        if (item.num_doc_cliente == "99999999999")
                        {
                            _be.tip_doc_cliente = "0";
                        }
                        else
                        {
                            _be.tip_doc_cliente = item.tip_doc_cliente.ToString();
                        }
                        _be.nc_dxc_tipodoc = item.nc_dxc_numdoc;
                        _be.doxcc_tipo_comprobante_referencia = item.doxcc_tipo_comprobante_referencia;
                        _be.doxcc_num_serie_referencia = item.doxcc_num_serie_referencia;
                        _be.doxcc_num_comprobante_referencia = item.doxcc_num_comprobante_referencia;
                        _be.doxcc_sfecha_emision_referencia = item.doxcc_sfecha_emision_referencia;
                        _be.tablc_iid_situacion_documento = item.tablc_iid_situacion_documento;
                        _be.tdocc_vdescripcion = item.tdocc_vdescripcion;
                        _be.vcocc_numero_vcontable = item.vcocc_numero_vcontable;
                        _be.subdi_icod_subdiario = Convert.ToInt32(item.subdi_icod_subdiario);
                        _be.CUO = string.Format("{0:00}", _be.subdi_icod_subdiario) + string.Format("{0:0000}", Convert.ToInt32(item.vcocc_numero_vcontable));
                        lista.Add(_be);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Almacen Contable

        public List<EAlmacenContable> listarAlmacenes()
        {
            List<EAlmacenContable> lista = new List<EAlmacenContable>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_ALMACEN_CONTABLE_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAlmacenContable()
                        {
                            almcc_icod_almacen = item.almcc_icod_almacen,
                            almcc_iid_almacen = Convert.ToInt32(item.almcc_iid_almacen),
                            almcc_vabreviatura = item.almcc_vabreviatura,
                            almcc_vdescripcion = item.almcc_vdescripcion
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
        public int insertarAlmacen(EAlmacenContable obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEA_ALMACEN_CONTABLE_INSERTAR(
                        ref intIcod,
                        obj.almcc_iid_almacen,
                        obj.almcc_vabreviatura,
                        obj.almcc_vdescripcion,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarAlmacen(EAlmacenContable obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEA_ALMACEN_CONTABLE_MODIFICAR(
                        obj.almcc_icod_almacen,
                        obj.almcc_iid_almacen,
                        obj.almcc_vabreviatura,
                        obj.almcc_vdescripcion,
                        obj.intUsuario,
                        obj.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAlmacen(EAlmacenContable obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEA_ALMACEN_CONTABLE_ELIMINAR(
                        obj.almcc_icod_almacen,
                        obj.intUsuario,
                        obj.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Clasificacion de Producto

        public List<EClasificacionProducto> listarClasificacionProducto()
        {
            List<EClasificacionProducto> lista = new List<EClasificacionProducto>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_CLASIFICACION_PRODUCTO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EClasificacionProducto()
                        {
                            clasc_icod_clasificacion = item.clasc_icod_clasificacion,
                            clasc_iid_clasificacion = Convert.ToInt32(item.clasc_iid_clasificacion),
                            clasc_vdescripcion = item.clasc_vdescripcion,
                            ctacc_icod_cuenta_contable_producto = item.ctacc_icod_cuenta_contable_producto,
                            ctacc_icod_cuenta_contable_importacion = item.ctacc_icod_cuenta_contable_importacion,
                            ctacc_icod_cuenta_contable_compra = item.ctacc_icod_cuenta_contable_compra,
                            ctacc_icod_cuenta_contable_costos = item.ctacc_icod_cuenta_contable_costos,
                            almcc_icod_almacen = Convert.ToInt32(item.almcc_icod_almacen),
                            strDesAlmacenCtbl = item.strDesAlmacenCtbl,
                            strCuentaProducto = item.strCuentaProducto,
                            strCuentaImportacion = item.strCuentaImportacion,
                            strCuentaCompra = item.strCuentaCompra,
                            strCuentaCostos = item.strCuentaCostos,

                            strDesCuentaProducto = item.strDesCuentaProducto,
                            strDesCuentaImportacion = item.strDesCuentaImportacion,
                            strDesCuentaCompra = item.strDesCuentaCompra,
                            strDesCuentaCostos = item.strDesCuentaCostos
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
        public int insertarClasificacionProducto(EClasificacionProducto obj)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_CLASIFICACION_PRODUCTO_INSERTAR(
                        ref intIcod,
                        obj.clasc_iid_clasificacion,
                        obj.clasc_vdescripcion,
                        obj.ctacc_icod_cuenta_contable_producto,
                        obj.ctacc_icod_cuenta_contable_importacion,
                        obj.ctacc_icod_cuenta_contable_compra,
                        obj.ctacc_icod_cuenta_contable_costos,
                        obj.intUsuario,
                        obj.strPc,
                        obj.almcc_icod_almacen
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarClasificacionProducto(EClasificacionProducto obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_CLASIFICACION_PRODUCTO_MODIFICAR(
                        obj.clasc_icod_clasificacion,
                        obj.clasc_iid_clasificacion,
                        obj.clasc_vdescripcion,
                        obj.ctacc_icod_cuenta_contable_producto,
                        obj.ctacc_icod_cuenta_contable_importacion,
                        obj.ctacc_icod_cuenta_contable_compra,
                        obj.ctacc_icod_cuenta_contable_costos,
                        obj.intUsuario,
                        obj.strPc,
                        obj.almcc_icod_almacen);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarClasificacionProducto(EClasificacionProducto obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_CLASIFICACION_PRODUCTO_ELIMINAR(
                        obj.clasc_icod_clasificacion,
                        obj.intUsuario,
                        obj.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Costos de Venta
        public List<EGuiaRemision> listarCostoVentaCab(int intEjercicio, int intPeriodo)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DESPACHO_CAB_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            intCorrelativo = item.id,
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            remic_sfecha_remision = item.remic_sfecha_remision,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            almac_icod_almacen_ingreso = Convert.ToInt32(item.almac_icod_almacen_ingreso),
                            intEjercicio = Convert.ToInt32(item.remic_ianio),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            dcmlTipoCambio = Convert.ToDecimal(item.tipoCambio),
                            strTipoVenta = item.tipoVenta,
                            intTipoDoc = Convert.ToInt32(item.icod_tipo_doc),
                            favc_vobservacion = item.vglosa,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo)
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
        public List<EGuiaRemision> listarDespachoVentaCab(int intEjercicio, int intPeriodo)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DESPACHO_VENTAS_CAB_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            intCorrelativo = item.id,
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            remic_sfecha_remision = item.remic_sfecha_remision,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            almac_icod_almacen_ingreso = Convert.ToInt32(item.almac_icod_almacen_ingreso),
                            intEjercicio = Convert.ToInt32(item.remic_ianio),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            dcmlTipoCambio = Convert.ToDecimal(item.tipoCambio),
                            strTipoVenta = item.tipoVenta,
                            intTipoDoc = Convert.ToInt32(item.icod_tipo_doc),
                            favc_vobservacion = item.vglosa,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo)
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
        public List<ECostoVenta> listarDespachoVentaDet(EGuiaRemision oBe)
        {
            List<ECostoVenta> lista = new List<ECostoVenta>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DESPACHO_VENTA_DET(oBe.remic_icod_remision, oBe.intEjercicio, oBe.remic_sfecha_remision);
                    foreach (var item in query)
                    {
                        lista.Add(new ECostoVenta()
                        {
                            dfacc_ncantidad_producto = Convert.ToDecimal(item.dremc_ncantidad_producto),
                            pcp = Convert.ToDecimal(item.dcmlPcp),
                            iid_cuenta_costos = item.intCuentaCostos,
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            iid_cuenta_producto = item.intCuentaProducto,
                            descripcion = item.strDesProducto
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
        public List<EGuiaRemision> listarDespachoTransferenciaCab(int intEjercicio, int intPeriodo)
        {
            List<EGuiaRemision> lista = new List<EGuiaRemision>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DESPACHO_TRANSFERENCIA_CAB_LISTAR(intEjercicio, intPeriodo);
                    foreach (var item in query)
                    {
                        lista.Add(new EGuiaRemision()
                        {
                            intCorrelativo = item.id,
                            remic_icod_remision = item.remic_icod_remision,
                            remic_vnumero_remision = item.remic_vnumero_remision,
                            remic_sfecha_remision = item.remic_sfecha_remision,
                            cliec_icod_cliente = item.cliec_icod_cliente,
                            almac_icod_almacen = Convert.ToInt32(item.almac_icod_almacen),
                            almac_icod_almacen_ingreso = Convert.ToInt32(item.almac_icod_almacen_ingreso),
                            intEjercicio = Convert.ToInt32(item.remic_ianio),
                            tablc_iid_tipo_moneda = Convert.ToInt32(item.tablc_iid_tipo_moneda),
                            dcmlTipoCambio = Convert.ToDecimal(item.tipoCambio),
                            strTipoVenta = item.tipoVenta,
                            intTipoDoc = Convert.ToInt32(item.icod_tipo_doc),
                            favc_vobservacion = item.vglosa,
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo),
                            tablc_iid_motivo = Convert.ToInt32(item.tablc_iid_motivo)
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
        public List<ECostoVenta> listarDespachoTransferenciaDet(EGuiaRemision oBe)
        {
            List<ECostoVenta> lista = new List<ECostoVenta>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_DESPACHO_TRANSFERENCIA_DET(oBe.remic_icod_remision, oBe.intEjercicio, oBe.remic_sfecha_remision);
                    foreach (var item in query)
                    {
                        lista.Add(new ECostoVenta()
                        {
                            dfacc_ncantidad_producto = Convert.ToDecimal(item.dremc_ncantidad_producto),
                            pcp = Convert.ToDecimal(item.dcmlPcp),
                            iid_cuenta_costos = item.intCuentaCostos,
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            iid_cuenta_producto = item.intCuentaProducto,
                            descripcion = item.strDesProducto
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
        public List<ECostoVenta> listarCostoVentaDet(EGuiaRemision oBe)
        {
            List<ECostoVenta> lista = new List<ECostoVenta>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_COSTO_VENTA_DET(oBe.remic_icod_remision, oBe.intEjercicio, oBe.remic_sfecha_remision);
                    foreach (var item in query)
                    {
                        lista.Add(new ECostoVenta()
                        {
                            dfacc_ncantidad_producto = Convert.ToDecimal(item.dremc_ncantidad_producto),
                            pcp = Convert.ToDecimal(item.dcmlPcp),
                            iid_cuenta_costos = item.intCuentaCostos,
                            ctacc_icod_cuenta_contable = Convert.ToInt32(item.ctacc_icod_cuenta_contable),
                            iid_cuenta_producto = item.intCuentaProducto,
                            descripcion = item.strDesProducto
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
        public List<ECostoVenta> listarCostoVentaNcvDet(EFacturaCab oBe)
        {
            List<ECostoVenta> lista = new List<ECostoVenta>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_COSTO_VENTA_NCV_DET(oBe.favc_icod_factura, oBe.intEjercicio, oBe.favc_sfecha_factura);
                    foreach (var item in query)
                    {
                        lista.Add(new ECostoVenta()
                        {
                            dfacc_ncantidad_producto = Convert.ToDecimal(item.dfacc_ncantidad_producto),
                            pcp = Convert.ToDecimal(item.dcmlPcp),
                            iid_cuenta_costos = item.intCuentaCostos,
                            iid_cuenta_producto = item.intCuentaProducto,
                            iid_centro_costos = item.intCentroCosto,
                            codigo_centro_costos = item.strCodCentroCosto,
                            descripcion = item.strDesProducto
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

        #endregion


        public void InsetarMasivoComprobanteDet(DataTable tbl)
        {
            try
            {
                using (var conn = new SqlConnection(Helper.conexion()))
                {
                    using (var cmd = new SqlCommand("INSERCION_MASIVA_VOUCHER_CONTABLE_DET", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VCD", tbl);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsetarMasivoComprobanteCab(DataTable tbl)
        {
            try
            {
                using (var conn = new SqlConnection(Helper.conexion()))
                {
                    using (var cmd = new SqlCommand("INSERCION_MASIVA_VOUCHER_CONTABLE_CAB", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VCC", tbl);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region ActivosFijos
        #region Localidades
        public List<ELocalidades> listarLocalidades()
        {
            List<ELocalidades> lista = new List<ELocalidades>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_LOCALIDADES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ELocalidades()
                        {
                            lafc_icod_localidades = item.lafc_icod_localidades,
                            lafc_iid_localidades = item.lafc_iid_localidades,
                            lafc_vdescripcion = item.lafc_vdescripcion,
                            lafc_flag_estado = item.lafc_flag_estado
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

        public int InsertarLocalidades(ELocalidades oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEP_LOCALIDADES_INSERTAR(
                        ref intIcod,
                        oBe.lafc_iid_localidades,
                        oBe.lafc_vdescripcion,
                        oBe.lafc_flag_estado,
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

        public void modificarLocalidades(ELocalidades oBe)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEP_LOCALIDADES_MODIFICAR(
                        oBe.lafc_icod_localidades,
                        oBe.lafc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarLocalidades(ELocalidades oBe)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEP_LOCALIDADES_ELIMINAR(
                        oBe.lafc_icod_localidades,
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
        #region Registro Activo Fijo
        public List<EActivoFijo> listarActivoFijo()
        {
            List<EActivoFijo> lista = new List<EActivoFijo>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_ACTIVO_FIJO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EActivoFijo()
                        {
                            acfc_icod_activo_fijo = item.acfc_icod_activo_fijo,
                            acfc_iid_activo_fijo = item.acfc_iid_activo_fijo,
                            tarec_iid_clasificacion_af = item.tarec_iid_clasificacion_af,
                            tarec_iid_situacion_af = item.tarec_iid_situacion_af,
                            acfc_vdescripcion = item.acfc_vdescripcion,
                            acfc_vmarca = item.acfc_vmarca,
                            acfc_vmodelo = item.acfc_vmodelo,
                            acfc_vserie = item.acfc_vserie,
                            acfc_vcaracterist = item.acfc_vcaracterist,
                            acfc_icantidad = item.acfc_icantidad,
                            tarec_iid_tip_moneda = item.tarec_iid_tip_moneda,
                            acfc_ncosto_act = item.acfc_ncosto_act,
                            acfc_ntotal_deprec = item.acfc_ntotal_deprec,
                            acfc_vcodigo_invent = item.acfc_vcodigo_invent,
                            lafc_icod_localidad = item.lafc_icod_localidad,
                            acfc_sfech_adqui = Convert.ToDateTime(item.acfc_sfech_adqui),
                            acfc_ncosto_adqui = item.acfc_ncosto_adqui,
                            acfc_ianio_vida = item.acfc_ianio_vida,
                            acfc_nporct_deprec = item.acfc_nporct_deprec,
                            acfc_sfech_alta = Convert.ToDateTime(item.acfc_sfech_alta),
                            acfc_sfecha_inic_uso = Convert.ToDateTime(item.acfc_sfecha_inic_uso),
                            ccoc_icod_centro_costo = item.ccoc_icod_centro_costo,
                            ctacc_icod_cuenta_contable = item.ctacc_icod_cuenta_contable,
                            proc_icod_proveedor = item.proc_icod_proveedor,
                            acfc_sfecha_baja = Convert.ToDateTime(item.acfc_sfecha_baja),
                            acfc_vmotivo = item.acfc_vmotivo,
                            acfc_vfoto = item.acfc_vfoto,
                            ccoc_numero_centro_costo = item.ccoc_numero_centro_costo,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            acfc_flag_estado = item.acfc_flag_estado,
                            lafc_vdescripcion = item.lafc_vdescripcion,
                            proc_vnombrecompleto = item.proc_vnombrecompleto,
                            NClasificacion = item.NClasificacion,
                            NSituacion = item.NSituacion,
                            NMoneda = item.NMoneda

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

        public int InsertarActivoFijo(EActivoFijo oBe)
        {
            int? intIcod = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEP_ACTIVO_FIJO_INSERTAR(
                        ref intIcod,
                       oBe.acfc_iid_activo_fijo,
                       oBe.tarec_iid_clasificacion_af,
                       oBe.tarec_iid_situacion_af,
                       oBe.acfc_vdescripcion,
                       oBe.acfc_vmarca,
                       oBe.acfc_vmodelo,
                       oBe.acfc_vserie,
                       oBe.acfc_vcaracterist,
                       oBe.acfc_icantidad,
                       oBe.tarec_iid_tip_moneda,
                       oBe.acfc_ncosto_act,
                       oBe.acfc_ntotal_deprec,
                       oBe.acfc_vcodigo_invent,
                       oBe.lafc_icod_localidad,
                       oBe.acfc_sfech_adqui,
                       oBe.acfc_ncosto_adqui,
                       oBe.acfc_ianio_vida,
                       oBe.acfc_nporct_deprec,
                       oBe.acfc_sfech_alta,
                       oBe.acfc_sfecha_inic_uso,
                       oBe.ccoc_icod_centro_costo,
                       oBe.ctacc_icod_cuenta_contable,
                       oBe.proc_icod_proveedor,
                       oBe.acfc_sfecha_baja,
                       oBe.acfc_vmotivo,
                       oBe.ccoc_numero_centro_costo,
                       oBe.ctacc_numero_cuenta_contable,
                       oBe.intUsuario,
                        oBe.strPc,
                       oBe.acfc_flag_estado,
                       oBe.lafc_vdescripcion,
                       oBe.proc_vnombrecompleto,
                       oBe.acfc_vfoto


                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarActivoFijo(EActivoFijo oBe)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEP_ACTIVO_FIJO_MODIFICAR(

                       oBe.acfc_icod_activo_fijo,
                       oBe.tarec_iid_clasificacion_af,
                       oBe.tarec_iid_situacion_af,
                       oBe.acfc_vdescripcion,
                       oBe.acfc_vmarca,
                       oBe.acfc_vmodelo,
                       oBe.acfc_vserie,
                       oBe.acfc_vcaracterist,
                       oBe.acfc_icantidad,
                       oBe.tarec_iid_tip_moneda,
                       oBe.acfc_ncosto_act,
                       oBe.acfc_ntotal_deprec,
                       oBe.acfc_vcodigo_invent,
                       oBe.lafc_icod_localidad,
                       oBe.acfc_sfech_adqui,
                       oBe.acfc_ncosto_adqui,
                       oBe.acfc_ianio_vida,
                       oBe.acfc_nporct_deprec,
                       oBe.acfc_sfech_alta,
                       oBe.acfc_sfecha_inic_uso,
                       oBe.ccoc_icod_centro_costo,
                       oBe.ctacc_icod_cuenta_contable,
                       oBe.proc_icod_proveedor,
                       oBe.acfc_sfecha_baja,
                       oBe.acfc_vmotivo,
                       oBe.ccoc_numero_centro_costo,
                       oBe.ctacc_numero_cuenta_contable,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.lafc_vdescripcion,
                        oBe.proc_vnombrecompleto,
                        oBe.acfc_vfoto
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarActivoFijo(EActivoFijo oBe)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGEP_LOCALIDADES_ELIMINAR(
                        oBe.acfc_icod_activo_fijo,
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

        #endregion

        #region Estado Gancias y Perdidas

        public List<EEstadoGanPer> ListarEstadoGanPer()
        {
            List<EEstadoGanPer> Lista = new List<EEstadoGanPer>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ESTADO_GAN_PER_LISTAR();
                    foreach (var item in query)
                    {
                        Lista.Add(new EEstadoGanPer()
                        {
                            egpc_icod_estado_gan_per = item.egpc_icod_estado_gan_per,
                            egpc_vlinea = item.egpc_vlinea,
                            tablc_icod_linea_registro = Convert.ToInt32(item.tablc_icod_linea_registro),
                            egpc_vconcepto = item.egpc_vconcepto,
                            tablc_icod_signo_monto = Convert.ToInt32(item.tablc_icod_signo_monto),
                            tablc_icod_tipo_total = Convert.ToInt32(item.tablc_icod_tipo_total),
                            DesLinea = item.DesLinea,
                            Signo = item.Signo,
                            Total = item.Total
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public int InsertarEstadoGanPer(EEstadoGanPer obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.egpc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.egpc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }

        public void ModificarEstadoGanPer(EEstadoGanPer obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_MODIFICAR(
                        obj.egpc_icod_estado_gan_per,
                        obj.egpc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.egpc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
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

        public void EliminarEstadoGanPer(EEstadoGanPer obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_ELIMINAR(
                        obj.egpc_icod_estado_gan_per,
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

        #endregion
        #region Estado Gancias y Perdidas Cuentas

        public List<EEstadoGanPerCtas> ListarEstadoGanPerCtasxIcodPosFin(int icod_pos_finan)
        {
            List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ESTADO_GAN_PER_CTAS_LISTAR_X_ICOD_EST(icod_pos_finan);
                    foreach (var item in query)
                    {
                        Lista.Add(new EEstadoGanPerCtas()
                        {
                            egpd_icod_ctas_estado_gan_per = item.egpd_icod_ctas_estado_gan_per,
                            egpd_icod_estado_gan_per = Convert.ToInt32(item.egpd_icod_estado_gan_per),
                            egpd_iid_cuenta_contable = Convert.ToInt32(item.egpd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public List<EEstadoGanPerCtas> ListarEstadoGanPerCtasxIcodPosFinMontos(int icod_pos_finan, int cecoc_icod_centro_costo, int vcocc_fecha_vcontable, int indicador)
        {
            List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ESTADO_GAN_PER_CTAS_LISTAR_X_ICOD_EST_MONTOS(icod_pos_finan, cecoc_icod_centro_costo, vcocc_fecha_vcontable, indicador);
                    foreach (var item in query)
                    {
                        Lista.Add(new EEstadoGanPerCtas()
                        {
                            egpd_icod_ctas_estado_gan_per = item.egpd_icod_ctas_estado_gan_per,
                            egpd_icod_estado_gan_per = Convert.ToInt32(item.egpd_icod_estado_gan_per),
                            egpd_iid_cuenta_contable = Convert.ToInt32(item.egpd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            MontosCC = Convert.ToDecimal(item.MontosCC)
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }


        public int InsertarPEstadoGanPerCtas(EEstadoGanPerCtas obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_CTAS_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.egpd_icod_estado_gan_per,
                        obj.egpd_iid_cuenta_contable,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }

        public void EliminarEstadoGanPerCtas(EEstadoGanPerCtas obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_CTAS_ELIMINAR(
                        obj.egpd_icod_ctas_estado_gan_per,
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

        #endregion

        #region Balnce
        public List<EBalance> ListarBalance()
        {
            List<EBalance> Lista = new List<EBalance>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_BALANCE_LISTAR();
                    foreach (var item in query)
                    {
                        Lista.Add(new EBalance()
                        {
                            blgc_icod_balance = item.blgc_icod_balance,
                            blgc_vlinea = item.blgc_vlinea,
                            tablc_icod_linea_registro = Convert.ToInt32(item.tablc_icod_linea_registro),
                            blgc_vconcepto = item.blgc_vconcepto,
                            tablc_icod_signo_monto = Convert.ToInt32(item.tablc_icod_signo_monto),
                            tablc_icod_tipo_total = Convert.ToInt32(item.tablc_icod_tipo_total),
                            DesLinea = item.DesLinea,
                            Signo = item.Signo,
                            Total = item.Total
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarBalance(EBalance obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_BALANCE_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.blgc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.blgc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }
        public void ModificarBalance(EBalance obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_BALANCE_MODIFICAR(
                        obj.blgc_icod_balance,
                        obj.blgc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.blgc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
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
        public void EliminarBalance(EBalance obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_BALANCE_ELIMINAR(
                        obj.blgc_icod_balance,
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
        #endregion
        #region Balance Cuentas
        public List<EBalanceCtas> ListarBalanceCtasxIcodPosFin(int icod_pos_finan)
        {
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_BALANCE_CTAS_LISTAR_X_ICOD(icod_pos_finan);
                    foreach (var item in query)
                    {
                        Lista.Add(new EBalanceCtas()
                        {
                            blgd_icod_ctas_balance = item.blgd_icod_ctas_balance,
                            blgc_icod_balance = Convert.ToInt32(item.blgc_icod_balance),
                            blgd_iid_cuenta_contable = Convert.ToInt32(item.blgd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public List<EBalanceCtas> ListarBalanceCtasxIcodPosFinMontos(int icod_pos_finan, int vcocc_fecha_vcontable)
        {
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_BALANCE_CTAS_LISTAR_X_ICOD_MONTOS(icod_pos_finan, vcocc_fecha_vcontable);
                    foreach (var item in query)
                    {
                        Lista.Add(new EBalanceCtas()
                        {
                            blgd_icod_ctas_balance = item.blgd_icod_ctas_balance,
                            blgc_icod_balance = Convert.ToInt32(item.blgc_icod_balance),
                            blgd_iid_cuenta_contable = Convert.ToInt32(item.blgd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            MontosCC = Convert.ToDecimal(item.MontosCC)
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public List<EBalanceCtas> ListarBalanceCtasxFechaContable(int vcocc_fecha_vcontable)
        {
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_BALANCE_CTAS_LISTAR_X_Fecha_Contable(vcocc_fecha_vcontable);
                    foreach (var item in query)
                    {
                        Lista.Add(new EBalanceCtas()
                        {
                            blgd_icod_ctas_balance = item.blgd_icod_ctas_balance,
                            blgc_icod_balance = Convert.ToInt32(item.blgc_icod_balance),
                            blgd_iid_cuenta_contable = Convert.ToInt32(item.blgd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            MontosCC = Convert.ToDecimal(item.MontosCC)
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarBalanceCtas(EBalanceCtas obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_BALANCE_CTAS_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.blgc_icod_balance,
                        obj.blgd_iid_cuenta_contable,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }
        public void EliminarBalanceCtas(EBalanceCtas obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_BALANCE_CTAS_ELIMINAR(
                        obj.blgd_icod_ctas_balance,
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
        #endregion

        #region centro de costo
        //grid
        public DataTable listarCentroCostoDinamico(int id_ger_per, int anio, int id_mes)
        {
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Helper.conexion()))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand
                    {
                        CommandText = "SGE_CENTRO_COSTO_DINAMICO",
                        CommandType = CommandType.StoredProcedure,
                        Connection = cn
                    };
                    SqlParameter param = cmd.Parameters.AddWithValue("@id_gn_per", id_ger_per);
                    param.Direction = ParameterDirection.Input;
                    SqlParameter param1 = cmd.Parameters.AddWithValue("@id_anio", anio);
                    param1.Direction = ParameterDirection.Input;
                    SqlParameter param2 = cmd.Parameters.AddWithValue("@id_mes", id_mes);
                    param2.Direction = ParameterDirection.Input;

                    SqlDataAdapter d = new SqlDataAdapter(cmd);
                    d.Fill(t);
                }
                return t;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable listarCentroCostoDinamico_plantilla(int id_costo, int anio, int id_mes)
        {
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Helper.conexion()))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand
                    {
                        CommandText = "SGE_CENTRO_COSTO_DINAMICO_PLANTILLA",
                        CommandType = CommandType.StoredProcedure,
                        Connection = cn
                    };
                    SqlParameter param = cmd.Parameters.AddWithValue("@id_centroC", id_costo);
                    param.Direction = ParameterDirection.Input;
                    SqlParameter param1 = cmd.Parameters.AddWithValue("@id_anio", anio);
                    param1.Direction = ParameterDirection.Input;
                    SqlParameter param2 = cmd.Parameters.AddWithValue("@id_mes", id_mes);
                    param2.Direction = ParameterDirection.Input;

                    SqlDataAdapter d = new SqlDataAdapter(cmd);
                    d.Fill(t);
                }
                return t;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable listarCentroCostoDinamico_Detalle_excel(int anio, int id_mes)
        {
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Helper.conexion()))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand
                    {
                        CommandText = "SGE_CENTRO_COSTO_DINAMICO_Exportar",
                        CommandType = CommandType.StoredProcedure,
                        Connection = cn
                    };

                    SqlParameter param1 = cmd.Parameters.AddWithValue("@id_anio", anio);
                    param1.Direction = ParameterDirection.Input;
                    SqlParameter param2 = cmd.Parameters.AddWithValue("@id_mes", id_mes);
                    param2.Direction = ParameterDirection.Input;

                    SqlDataAdapter d = new SqlDataAdapter(cmd);
                    d.Fill(t);
                }
                return t;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable listarCentroCostoDinamico_plantilla_SinMovimiento()
        {
            try
            {
                DataTable t = new DataTable();
                using (SqlConnection cn = new SqlConnection(Helper.conexion()))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand
                    {
                        CommandText = "SGE_LISTAR_PLANTILLA",
                        CommandType = CommandType.StoredProcedure,
                        Connection = cn
                    };

                    SqlDataAdapter d = new SqlDataAdapter(cmd);
                    d.Fill(t);
                }
                return t;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Estado Gancias y Perdidas Funcion
        public List<EEstadoGanPerFuncion> ListarEstadoGanPerFuncion()
        {
            List<EEstadoGanPerFuncion> Lista = new List<EEstadoGanPerFuncion>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ESTADO_GAN_PER_FUNCION_LISTAR();
                    foreach (var item in query)
                    {
                        Lista.Add(new EEstadoGanPerFuncion()
                        {
                            egpfc_icod_estado_gan_per_funcion = item.egpfc_icod_estado_gan_per_funcion,
                            egpfc_vlinea = item.egpfc_vlinea,
                            tablc_icod_linea_registro = Convert.ToInt32(item.tablc_icod_linea_registro),
                            egpfc_vconcepto = item.egpfc_vconcepto,
                            tablc_icod_signo_monto = Convert.ToInt32(item.tablc_icod_signo_monto),
                            tablc_icod_tipo_total = Convert.ToInt32(item.tablc_icod_tipo_total),
                            DesLinea = item.DesLinea,
                            Signo = item.Signo,
                            Total = item.Total
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarEstadoGanPerFuncion(EEstadoGanPerFuncion obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_FUNCION_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.egpfc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.egpfc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }
        public void ModificarEstadoGanPerFuncion(EEstadoGanPerFuncion obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_FUNCION_MODIFICAR(
                        obj.egpfc_icod_estado_gan_per_funcion,
                        obj.egpfc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.egpfc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
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
        public void EliminarEstadoGanPerFuncion(EEstadoGanPerFuncion obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_FUNCION_ELIMINAR(
                        obj.egpfc_icod_estado_gan_per_funcion,
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
        #endregion
        #region Estado Gancias y Perdidas Cuentas Funcion

        public List<EEstadoGanPerCtasFuncion> ListarEstadoGanPerCtasxIcodPosFinFuncion(int icod_pos_finan)
        {
            List<EEstadoGanPerCtasFuncion> Lista = new List<EEstadoGanPerCtasFuncion>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ESTADO_GAN_PER_CTAS_FUNCION_LISTAR_X_ICOD_EST(icod_pos_finan);
                    foreach (var item in query)
                    {
                        Lista.Add(new EEstadoGanPerCtasFuncion()
                        {
                            egpfd_icod_ctas_estado_gan_per_funcion = item.egpfd_icod_ctas_estado_gan_per_funcion,
                            egpfc_icod_estado_gan_per_funcion = Convert.ToInt32(item.egfc_icod_estado_gan_per_funcion),
                            egpfd_iid_cuenta_contable = Convert.ToInt32(item.egpfd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public List<EEstadoGanPerCtasFuncion> ListarEstadoGanPerCtasxFechaContable(int cecoc_icod_centro_costo, int vcocc_fecha_vcontable, int indicador)
        {
            List<EEstadoGanPerCtasFuncion> Lista = new List<EEstadoGanPerCtasFuncion>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_ESTADO_GAN_PER_CTAS_LISTAR_X_FECHA_CONTABLE(cecoc_icod_centro_costo, vcocc_fecha_vcontable, indicador);
                    foreach (var item in query)
                    {
                        Lista.Add(new EEstadoGanPerCtasFuncion()
                        {
                            egpfd_icod_ctas_estado_gan_per_funcion = item.egpfd_icod_ctas_estado_gan_per_funcion,
                            egpfc_icod_estado_gan_per_funcion = Convert.ToInt32(item.egfc_icod_estado_gan_per_funcion),
                            egpfd_iid_cuenta_contable = Convert.ToInt32(item.egpfd_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            MontosCC = Convert.ToDecimal(item.MontosCC)

                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }

        public int InsertarPEstadoGanPerCtasFuncion(EEstadoGanPerCtasFuncion obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_CTAS_FUNCION_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.egpfc_icod_estado_gan_per_funcion,
                        obj.egpfd_iid_cuenta_contable,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }

        public void EliminarEstadoGanPerCtasFuncion(EEstadoGanPerCtasFuncion obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_ESTADO_GAN_PER_CTAS_FUNCION_ELIMINAR(
                        obj.egpfd_icod_ctas_estado_gan_per_funcion,
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

        #endregion

        #region Inventario Resultado
        public List<EInventarioResultado> ListarInventarioResultado()
        {
            List<EInventarioResultado> Lista = new List<EInventarioResultado>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_INVENTARIO_RESULTADO_LISTAR();
                    foreach (var item in query)
                    {
                        Lista.Add(new EInventarioResultado()
                        {
                            irc_icod_inventario_resultado = item.irc_icod_inventario_resultado,
                            irc_vlinea = item.irc_vlinea,
                            tablc_icod_linea_registro = Convert.ToInt32(item.tablc_icod_linea_registro),
                            irc_vconcepto = item.irc_vconcepto,
                            tablc_icod_signo_monto = Convert.ToInt32(item.tablc_icod_signo_monto),
                            tablc_icod_tipo_total = Convert.ToInt32(item.tablc_icod_tipo_total),
                            DesLinea = item.DesLinea,
                            Signo = item.Signo,
                            Total = item.Total
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarInventarioResultado(EInventarioResultado obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_INVENTARIO_RESULTADO_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.irc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.irc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }
        public void ModificarInventarioResultado(EInventarioResultado obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_INVENTARIO_RESULTADO_MODIFICAR(
                        obj.irc_icod_inventario_resultado,
                        obj.irc_vlinea,
                        obj.tablc_icod_linea_registro,
                        obj.irc_vconcepto,
                        obj.tablc_icod_signo_monto,
                        obj.tablc_icod_tipo_total,
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
        public void EliminarInventarioResultado(EInventarioResultado obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_INVENTARIO_RESULTADO_ELIMINAR(
                        obj.irc_icod_inventario_resultado,
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
        #endregion
        #region Inventario Resultado Cuentas
        public List<EInventarioResultadoCtas> ListarInventarioResultadoCtasxIcodPosFin(int icod_pos_finan)
        {
            List<EInventarioResultadoCtas> Lista = new List<EInventarioResultadoCtas>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_INVENTARIO_RESULTADO_CTAS_LISTAR_X_ICOD(icod_pos_finan);
                    foreach (var item in query)
                    {
                        Lista.Add(new EInventarioResultadoCtas()
                        {
                            ird_icod_ctas_inventario_resultado = item.ird_icod_ctas_inventario_resultado,
                            irc_icod_inventario_resultado = Convert.ToInt32(item.irc_icod_inventario_resultado),
                            ird_iid_cuenta_contable = Convert.ToInt32(item.ird_iid_cuenta_contable),
                            ctacc_nombre_descripcion = item.ctacc_nombre_descripcion,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable
                        }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Lista;
        }
        public int InsertarInventarioResultadoCtas(EInventarioResultadoCtas obj)
        {
            int? Cab_icod_correlativo = 0;
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_INVENTARIO_RESULTADO_CTAS_INSERTAR(
                        ref Cab_icod_correlativo,
                        obj.irc_icod_inventario_resultado,
                        obj.ird_iid_cuenta_contable,
                        obj.intUsuario,
                        obj.strPc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Convert.ToInt32(Cab_icod_correlativo);
        }
        public void EliminarInventarioResultadoCtas(EInventarioResultadoCtas obj)
        {
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    dc.SGE_INVENTARIO_RESULTADO_CTAS_ELIMINAR(
                        obj.ird_icod_ctas_inventario_resultado,
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
        #endregion

        public List<EVoucherContableCab> VerificarAnalitica(int analitica)
        {
            List<EVoucherContableCab> lista = new List<EVoucherContableCab>();
            try
            {
                using (ContabilidadDataContext dc = new ContabilidadDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_VERFICAR_ANALITICA(analitica);
                    foreach (var item in query)
                    {
                        lista.Add(new EVoucherContableCab()
                        {
                            vcocc_icod_vcontable = item.vcocc_icod_vcontable
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
    }
}
