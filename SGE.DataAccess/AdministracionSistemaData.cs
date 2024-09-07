using SGE.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace SGE.DataAccess
{
    public class AdministracionSistemaData
    {
        #region Usuario 
        public List<EUsuario> listarUsuarios()
        {
            List<EUsuario> lista = new List<EUsuario>(); ;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    var query = dc.SGES_USUARIO_LISTAR("ACCESSKEY");
                    foreach (var item in query)
                    {
                        lista.Add(new EUsuario()
                        {
                            usua_icod_usuario = item.usua_icod_usuario,
                            usua_codigo_usuario = item.usua_codigo_usuario.Trim(),
                            usua_nombre_usuario = item.usua_nombre_usuario.Trim(),
                            usua_password_usuario = item.usua_password_usuario,
                            usua_iactivo = Convert.ToBoolean(item.usua_iactivo),
                            strEstado = (Convert.ToBoolean(item.usua_iactivo)) ? "Activo" : "Inactivo",
                            usua_bflag_indicador = Convert.ToBoolean(item.usua_bflag_indicador),
                            usua_bpunto_venta = Convert.ToBoolean(item.usua_bpunto_venta),
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
        public int insertarUsuario(EUsuario obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGES_USUARIO_INSERTAR(
                        ref intIcod,
                        obj.usua_codigo_usuario,
                        obj.usua_nombre_usuario,
                        obj.usua_password_usuario,
                        obj.usua_iactivo,
                        "ACCESSKEY",
                        obj.intUsuario,
                        obj.usua_bflag_indicador,
                        obj.usua_bpunto_venta,
                        obj.puvec_icod_punto_venta
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarUsuario(EUsuario obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGES_USUARIO_MODIFICAR(
                        obj.usua_icod_usuario,
                        obj.usua_nombre_usuario,
                        obj.usua_password_usuario,
                        obj.usua_iactivo,
                        "ACCESSKEY",
                        obj.intUsuario,
                        obj.usua_bflag_indicador,
                        obj.usua_bpunto_venta,
                        obj.puvec_icod_punto_venta);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarUsuario(EUsuario obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGES_USUARIO_ELIMINAR(
                        obj.usua_icod_usuario,
                        obj.intUsuario);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Accesos de Usuario
        public List<EFormulario> listarAccesosNoPermitidos(int intUsuario)
        {
            List<EFormulario> lista = new List<EFormulario>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEAS_FORMULARIO_NO_PERMITIDO_LISTAR(intUsuario);
                    foreach (var item in query)
                    {

                        lista.Add(new EFormulario()
                        {
                            formc_icod_forms = item.formc_icod_forms,
                            moduc_icod_modulo = item.moduc_icod_modulo,
                            strModulo = item.moduc_vdescripcion,
                            formc_vnombre_forms = item.formc_vnombre_forms,
                            formc_vdescripcion = item.formc_vdescripcion,
                            flag = false
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

        public List<ControlVersiones> Listar_Versiones()
        {
            List<ControlVersiones> lista = new List<ControlVersiones>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.ACT_ACTUALIZACIONES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ControlVersiones()
                        {
                            cvr_icod_version = item.cvr_icod_version,
                            cvr_vversion = item.cvr_vversion,
                            cvr_sfecha_version = item.cvr_sfecha_version,
                            cvr_vurl = item.cvr_vurl,
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

        public ControlEquipos Equipo_Obtner_Datos(string text, string idCpu)
        {
            ControlEquipos obj = new ControlEquipos();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.ACT_EQUIPO_DATOS_OBTENER(text, idCpu);
                    foreach (var item in query)
                    {
                        obj.ceq_icod_equipo = item.ceq_icod_equipo;
                        obj.ceq_vnombre_equipo = item.ceq_vnombre_equipo;
                        obj.cvr_icod_version = item.cvr_icod_version;
                        obj.ceq_sfecha_actualizacion = item.ceq_sfecha_actualizacion;
                        obj.cvr_vversion = item.cvr_vversion;
                        obj.cvr_sfecha_version = item.cvr_sfecha_version;
                        obj.cep_vubicacion_actualizador = item.cep_vubicacion_actualizador;
                        obj.cep_vid_cpu = item.cep_vid_cpu;
                        obj.cep_bflag_acceso = item.cep_bflag_acceso;
                        obj.cep_vubicacion_actualizador = item.cep_vubicacion_actualizador;
                        obj.cep_id_pvt = Convert.ToInt32(item.cep_id_pvt);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return obj;
        }

        public void Equipo_Modificar(ControlEquipos objEquipo)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.ACT_EQUIPO_MODIFICAR(
                           objEquipo.ceq_icod_equipo,
                           objEquipo.ceq_vnombre_equipo,
                           objEquipo.cvr_icod_version,
                           objEquipo.ceq_sfecha_actualizacion,
                           objEquipo.cep_vubicacion_actualizador,
                           objEquipo.cep_id_pvt
                           );
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EFormulario> listarAccesosPermitidos(int intUsuario)
        {
            List<EFormulario> lista = new List<EFormulario>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEAS_FORMULARIO_PERMITIDO_LISTAR(intUsuario);
                    foreach (var item in query)
                    {

                        lista.Add(new EFormulario()
                        {
                            formc_icod_forms = item.formc_icod_forms,
                            moduc_icod_modulo = item.moduc_icod_modulo,
                            strModulo = item.moduc_vdescripcion,
                            formc_vnombre_forms = item.formc_vnombre_forms,
                            formc_vdescripcion = item.formc_vdescripcion
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
        public int insertarAccesoUsuario(EFormulario obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_ACCESO_USUARIO_INSERTAR(
                        ref intIcod,
                        obj.intUsuarioAcceso,
                        obj.formc_icod_forms,
                        obj.intUsuario
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAccesoUsuario(EFormulario obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_ACCESO_USUARIO_ELIMINAR(
                        obj.intUsuarioAcceso,
                        obj.formc_icod_forms);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tipo Documento
        public List<ETipoDocumento> listarTipoDocumentoPorModulo(int IdModulo)
        {
            List<ETipoDocumento> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoDocumento>();
                    var query = dc.SGEAS_TIPO_DOCUMENTO_MODULO_LISTAR(IdModulo);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumento()
                        {
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_iid_tipo_doc = Convert.ToInt32(item.tdocc_iid_tipo_doc),
                            tdocc_vdescripcion = item.tdocc_vdescripcion,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc
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
        public List<ETipoDocumento> listarTipoDocumento()
        {
            List<ETipoDocumento> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoDocumento>();
                    var query = dc.SGEAS_TIPO_DOCUMENTO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumento()
                        {
                            tdocc_icod_tipo_doc = Convert.ToInt32(item.tdocc_icod_tipo_doc),
                            tdocc_iid_tipo_doc = Convert.ToInt32(item.tdocc_iid_tipo_doc),
                            tdocc_vdescripcion = item.tdocc_vdescripcion,
                            tdocc_coa = item.tdocc_coa,
                            tdocc_nro_correlativo = item.tdocc_nro_correlativo,
                            tdocc_nro_correlativo_2 = item.tdocc_nro_correlativo_2,
                            tdocc_vabreviatura_tipo_doc = item.tdocc_vabreviatura_tipo_doc,
                            tdocc_nro_serie = item.tdocc_nro_serie,
                            tdocc_nro_serie_2 = item.tdocc_nro_serie_2,
                            strNroCorrelativo = (Convert.ToInt32(item.tdocc_nro_correlativo) != 0) ? String.Format("{0:00000000000}", Convert.ToInt32(item.tdocc_nro_correlativo)) : "",
                            strNroCorrelativo_2 = (Convert.ToInt32(item.tdocc_nro_correlativo_2) != 0) ? String.Format("{0:00000000000}", Convert.ToInt32(item.tdocc_nro_correlativo_2)) : ""
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
        public int insertarTipoDocumento(ETipoDocumento oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_TIPO_DOCUMENTO_INSERTAR(
                    ref intIcod,
                    oBe.tdocc_iid_tipo_doc,
                    oBe.tdocc_vabreviatura_tipo_doc,
                    oBe.tdocc_vdescripcion,
                    oBe.tdocc_coa,
                    oBe.tdocc_nro_correlativo,
                    oBe.intUsuario,
                    oBe.tdocc_nro_serie,
                    oBe.tdocc_nro_serie_2,
                    oBe.tdocc_nro_correlativo_2);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoDocumento(ETipoDocumento oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_MODIFICAR(
                    oBe.tdocc_icod_tipo_doc,
                    oBe.tdocc_vdescripcion,
                    oBe.tdocc_coa,
                    oBe.tdocc_nro_correlativo,
                    oBe.intUsuario,
                    oBe.tdocc_nro_serie,
                    oBe.tdocc_nro_serie_2,
                    oBe.tdocc_nro_correlativo_2);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoDocumento(ETipoDocumento oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_ELIMINAR(
                        oBe.tdocc_icod_tipo_doc,
                        oBe.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ETipoDocumento> getCorrelativoTipoDocumento(int intTipoDocumento, string strNroSerie)
        {
            int? intCorrelativo = 0;

            int? intCorrelativo2 = 0;
            string strNroSerie2 = "";
            List<ETipoDocumento> lst = new List<ETipoDocumento>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_GET_CORRELATIVO_TIPO_DOC(
                    ref intCorrelativo,
                    ref strNroSerie,
                    ref intCorrelativo2,
                    ref strNroSerie2,
                    intTipoDocumento);

                    lst.Add(new ETipoDocumento()
                    {
                        tdocc_nro_serie = strNroSerie,
                        tdocc_nro_correlativo = intCorrelativo,
                        tdocc_nro_serie_2 = strNroSerie2,
                        tdocc_nro_correlativo_2 = intCorrelativo2
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updateCorrelativoTipoDocumentoPVT(int intTipoDocumento, int intCorrelativo, int intOpcion)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_UPDATE_CORRELATIVO_PVT(
                    intTipoDocumento,
                    intCorrelativo,
                    intOpcion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCorrelativoTipoDocumentoSeries(string serie, int intCorrelativo)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_UPDATE_CORRELATIVO_SERIES(
                    serie,
                    intCorrelativo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCorrelativoDocumentoSunat(int icodParametro, int intCorrelativo)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_UPDATE_CORRELATIVO_DOC_SUNAT(
                    icodParametro,
                    intCorrelativo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void updateCorrelativoTipoDocumentoPVTMP(int intTipoDocumento, int intCorrelativo, int intOpcion)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_UPDATE_CORRELATIVO_PVT_MP(
                    intTipoDocumento,
                    intCorrelativo,
                    intOpcion);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ObtenerCorrelativoDocumento(string serie, int Tipo_docuemnto)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_DOCUMENTO(serie, Tipo_docuemnto);
                    foreach (var item in query)
                    {
                        NumDocumento = item.NumDocumento;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        public string ObtenerCorrelativoOCLMercaderia(int serie)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCL_MERCADERIA(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        public string ObtenerCorrelativoOCL(int serie)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCL(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        public string ObtenerCorrelativoRCS(int serie)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_RCS(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

        public string ObtenerCorrelativoOCS(int serie)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCS(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

        public string ObtenerCorrelativoOCI(int serie)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_OCI(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:00000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }

        public string ObtenerCorrelativoProyecto(int serie)
        {
            string NumDocumento = null;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEG_OBTENER_CORRELATIVO_PROYECTO(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = string.Format("{0:000000}", (Convert.ToInt32(item.NumDocumento)));
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        #endregion  
        #region Tipo Documento Detalle
        public List<ETipoDocumentoDetalle> listarTipoDocumentoDetalle(ETipoDocumento oBe)
        {
            List<ETipoDocumentoDetalle> lista = new List<ETipoDocumentoDetalle>();

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGEAS_TIPO_DOCUMENTO_DET_LISTAR(oBe.tdocc_icod_tipo_doc);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumentoDetalle()
                        {
                            moduc_icod_modulo = item.moduc_icod_modulo
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
        public void insertarTipoDocumentoDetalle(ETipoDocumento oBe, List<EModulo> lstModulo)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    foreach (EModulo x in lstModulo)
                    {
                        if (x.moduc_flag_estado == true)
                        {
                            dc.SGEAS_TIPO_DOCUMENTO_DET_INSERTAR(
                            oBe.tdocc_icod_tipo_doc,
                            x.moduc_icod_modulo,
                            oBe.intUsuario
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
        public void eliminarTipoDocumentoDetalle(ETipoDocumento oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_ELIMINAR(
                        oBe.tdocc_icod_tipo_doc
                        );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo Documento Clase
        public List<ETipoDocumentoDetalleCta> listarTipoDocumentoDetCta(int intTipoDoc)
        {
            List<ETipoDocumentoDetalleCta> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoDocumentoDetalleCta>();
                    var query = dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_LISTAR(intTipoDoc);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoDocumentoDetalleCta()
                        {
                            tdocd_iid_correlativo = item.tdocd_iid_correlativo,
                            tdocd_iid_codigo_doc_det = item.tdocd_iid_codigo_doc_det,
                            tdocd_descripcion = item.tdocd_descripcion,
                            tdocc_icod_tipo_doc = item.tdocc_icod_tipo_doc,
                            ctacc_icod_cuenta_contable_nac = item.ctacc_icod_cuenta_contable_nac,
                            ctacc_icod_cuenta_contable_extra = item.ctacc_icod_cuenta_contable_extra,
                            ctacc_icod_cuenta_matris_nac = item.ctacc_icod_cuenta_matris_nac,
                            ctacc_icod_cuenta_matris_extra = item.ctacc_icod_cuenta_matris_extra,
                            ctacc_icod_subcuenta_nac = item.ctacc_icod_subcuenta_nac,
                            ctacc_icod_subcuenta_extra = item.ctacc_icod_subcuenta_extra,
                            ctacc_icod_cuenta_asociada_nac = item.ctacc_icod_cuenta_asociada_nac,
                            ctacc_icod_cuenta_asociada_extra = item.ctacc_icod_cuenta_asociada_extra,
                            ctacc_icod_cuenta_igv_nac = item.ctacc_icod_cuenta_igv_nac,
                            ctacc_icod_cuenta_isc = item.ctacc_icod_cuenta_isc,
                            ctacc_icod_cuenta_gastos_nac = item.ctacc_icod_cuenta_gastos_nac,
                            ctacc_icod_cuenta_ivap = item.ctacc_icod_cuenta_ivap,
                            tdocd_iestado_registro = item.tdocd_iestado_registro,
                            tdocd_estado_coa = item.tdocd_estado_coa,
                            reg_compra = (item.tdocd_iestado_registro == 0) ? "*" : "",
                            reg_venta = (item.tdocd_iestado_registro == 1) ? "*" : "",
                            ctacc_icod_cuenta_servicios = item.ctacc_icod_cuenta_servicios
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
        public int insertarTipoDocumentoDetCta(ETipoDocumentoDetalleCta obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_INSERTAR(
                        ref intIcod,
                        obj.tdocd_iid_codigo_doc_det,
                        obj.tdocd_descripcion,
                        obj.tdocc_icod_tipo_doc,
                        obj.ctacc_icod_cuenta_contable_nac,
                        obj.ctacc_icod_cuenta_contable_extra,
                        obj.ctacc_icod_cuenta_matris_nac,
                        obj.ctacc_icod_cuenta_matris_extra,
                        obj.ctacc_icod_subcuenta_nac,
                        obj.ctacc_icod_subcuenta_extra,
                        obj.ctacc_icod_cuenta_asociada_nac,
                        obj.ctacc_icod_cuenta_asociada_extra,
                        obj.ctacc_icod_cuenta_igv_nac,
                        obj.ctacc_icod_cuenta_isc,
                        obj.ctacc_icod_cuenta_gastos_nac,
                        obj.ctacc_icod_cuenta_ivap,
                        obj.tdocd_iestado_registro,
                        obj.tdocd_estado_coa,
                        obj.intUsuario,
                        obj.ctacc_icod_cuenta_servicios
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoDocumentoDetCta(ETipoDocumentoDetalleCta obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_MODIFICAR(
                        obj.tdocd_iid_correlativo,
                        obj.tdocd_descripcion,
                        obj.tdocc_icod_tipo_doc,
                        obj.ctacc_icod_cuenta_contable_nac,
                        obj.ctacc_icod_cuenta_contable_extra,
                        obj.ctacc_icod_cuenta_matris_nac,
                        obj.ctacc_icod_cuenta_matris_extra,
                        obj.ctacc_icod_subcuenta_nac,
                        obj.ctacc_icod_subcuenta_extra,
                        obj.ctacc_icod_cuenta_asociada_nac,
                        obj.ctacc_icod_cuenta_asociada_extra,
                        obj.ctacc_icod_cuenta_igv_nac,
                        obj.ctacc_icod_cuenta_isc,
                        obj.ctacc_icod_cuenta_gastos_nac,
                        obj.ctacc_icod_cuenta_ivap,
                        obj.tdocd_iestado_registro,
                        obj.tdocd_estado_coa,
                        obj.intUsuario,
                        obj.ctacc_icod_cuenta_servicios);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoDocumentoDetCta(ETipoDocumentoDetalleCta obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_DOCUMENTO_DET_CTA_ELIMINAR(
                        obj.tdocd_iid_correlativo,
                        obj.tdocc_icod_tipo_doc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Módulo
        public List<EModulo> listarModulo()
        {
            List<EModulo> lista = null;
            int cont = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<EModulo>();
                    var query = dc.SGEAS_MODULO_LISTAR();
                    foreach (var item in query)
                    {
                        cont += 1;
                        lista.Add(new EModulo()
                        {
                            intCorrelativo = cont,
                            moduc_icod_modulo = item.moduc_icod_modulo,
                            moduc_vdescripcion = item.moduc_vdescripcion
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
        public int insertarModulo(EModulo oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_MODULO_INSERTAR(
                    ref intIcod,
                    oBe.moduc_vdescripcion,
                    oBe.intUsuario);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarModulo(EModulo oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_MODULO_MODIFICAR(
                    oBe.moduc_icod_modulo,
                    oBe.moduc_vdescripcion,
                    oBe.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarModulo(EModulo oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_MODULO_ELIMINAR(
                        oBe.moduc_icod_modulo,
                        oBe.intUsuario);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Parametro
        public void modificarCorrelativoOR(EParametro oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_PARAMETRO_MODIFICAR_CORRELATIVO_OR(
                        oBe.pm_icod_parametro,
                        oBe.pm_correlativo_OR);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EParametro> listarParametro()
        {
            List<EParametro> lista = new List<EParametro>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SIGAS_PARAMETRO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EParametro()
                        {
                            pm_icod_parametro = item.pm_icod_parametro,
                            pm_nigv_parametro = item.pm_nigv_parametro,
                            pm_ntope_parametro = item.pm_ntope_parametro,
                            pm_nuit_parametro = item.pm_nuit_parametro,
                            pm_ncategoria_parametro = item.pm_ncategoria_parametro,
                            pm_nivap_parametro = item.pm_nivap_parametro,
                            pm_nisc_parametro = item.pm_nisc_parametro,
                            pm_nombre_empresa = item.pm_nombre_empresa,
                            pm_direccion_empresa = item.pm_direccion_empresa,
                            pm_vruc = item.pm_vruc,
                            pm_correlativo_OR = Convert.ToInt64(item.pm_correlativo_OR),
                            pm_correlativo_doc_sunat = Convert.ToInt32(item.pm_correlativo_doc_sunat),
                            urlServicioFacturaElectronica = item.urlServicioFacturaElectronica,
                            urlServicioNotaCredito = item.urlServicioNotaCredito,
                            urlServicioNotaDebito = item.urlServicioNotaDebito,
                            Ruc = item.Ruc,
                            UsuarioSol = item.UsuarioSol,
                            ClaveSol = item.ClaveSol,
                            EndPointUrlPrueba = item.EndPointUrlPrueba,
                            EndPointUrlDesarrollo = item.EndPointUrlDesarrollo,
                            PasswordCertificado = item.PasswordCertificado,
                            CertificadoDigital = item.CertificadoDigital,
                            urlServicioEnviarDocumento = item.urlServicioEnviarDocumento,
                            urlServicioFirma = item.urlServicioFirma,
                            IdServiceValidacion = item.IdServiceValidacion,
                            pm_sfecha_inicio = Convert.ToDateTime(item.pm_sfecha_inicio),
                            DirecciónXML = item.DirecciónXML,
                            urlServicioEnvioResumen = item.urlServicioEnvioResumen,
                            urlServicoGenerarResumen = item.urlServicoGenerarResumen,
                            IdServiceValidacionResumen = item.IdServiceValidacionResumen,
                            urlServicioDocumentoBaja = item.urlServicioDocumentoBaja,
                            ServiceConsultaTiket = item.ServiceConsultaTiket,
                            pm_ntipo_cambio = Convert.ToDecimal(item.pm_ntipo_cambio),
                            pmprc_vruta_letra = item.pmprc_vruta_letra == null ? "" : item.pmprc_vruta_letra,
                            pmprc_vruta_cheque = item.pmprc_vruta_cheque == null ? "" : item.pmprc_vruta_cheque,
                            urlServicioGuiaRemisionDesarrollo = item.urlServicioGuiaRemisionDesarrollo,
                            pm_vruta_resumen = item.pm_vruta_resumen,
                            urlTokenGREdesarrollo = item.urlTokenGREdesarrollo,
                            usuarioGRE = item.usuarioGRE,
                            claveGRE = item.claveGRE,
                            urlServicioCDRGuiaRemisionPrueba = item.urlServicioCDRGuiaRemisionPrueba,
                            UsuarioSolGRE = item.UsuarioSolGRE,
                            ClaveSolGRE = item.ClaveSolGRE,
                            DireccionXmlGRE = item.DireccionXmlGRE,
                            UrlBaseServicioGRE = item.UrlBaseServicioGRE,
                            UrlBaseEnvioGRE = item.UrlBaseEnvioGRE,
                            pm_porc_retencion = item.pm_porc_retencion
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
        public void modificarParametro(EParametro oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_PARAMETRO_MODIFICAR_CENTRAL(
                        oBe.pm_icod_parametro,
                        oBe.pm_nigv_parametro,
                        oBe.pm_nuit_parametro,
                        oBe.pm_ncategoria_parametro,
                        oBe.pm_nombre_empresa,
                        oBe.pm_direccion_empresa,
                        oBe.pm_vruc,
                        oBe.intUsuario,
                        oBe.pm_correlativo_OR,
                        oBe.pm_correlativo_doc_sunat,
                        oBe.urlServicioFacturaElectronica,
                        oBe.urlServicioNotaCredito,
                        oBe.urlServicioNotaDebito,
                        oBe.Ruc,
                        oBe.UsuarioSol,
                        oBe.ClaveSol,
                        oBe.EndPointUrlPrueba,
                        oBe.EndPointUrlDesarrollo,
                        oBe.PasswordCertificado,
                        oBe.CertificadoDigital,
                        oBe.urlServicioEnviarDocumento,
                        oBe.urlServicioFirma,
                        oBe.IdServiceValidacion,
                        oBe.pm_sfecha_inicio,
                        oBe.DirecciónXML,
                        oBe.urlServicioEnvioResumen,
                        oBe.urlServicoGenerarResumen,
                        oBe.IdServiceValidacionResumen,
                        oBe.urlServicioDocumentoBaja,
                        oBe.ServiceConsultaTiket,
                        oBe.pm_ntipo_cambio,
                        oBe.pmprc_vruta_letra,
                        oBe.pmprc_vruta_cheque,
                        oBe.urlServicioGuiaRemisionDesarrollo,
                        oBe.pm_vruta_resumen,
                        oBe.urlTokenGREdesarrollo,
                        oBe.usuarioGRE,
                        oBe.claveGRE,
                        oBe.urlServicioCDRGuiaRemisionPrueba,
                        oBe.UsuarioSolGRE,
                        oBe.ClaveSolGRE,
                        oBe.DireccionXmlGRE,
                        oBe.UrlBaseServicioGRE,
                        oBe.UrlBaseEnvioGRE,
                        oBe.pm_porc_retencion
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tabla
        public List<ETabla> listarTabla()
        {
            List<ETabla> lista = new List<ETabla>(); ;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_TABLA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETabla()
                        {
                            tablc_iid_tipo_tabla = item.tablc_iid_tipo_tabla,
                            tablc_vdescripcion = item.tablc_vdescripcion.Trim(),
                            tablc_cestado = Convert.ToChar(item.tablc_cestado),
                            strEstado = (item.tablc_cestado == 'A') ? "Activo" : "Inactivo"
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
        public int insertarTabla(ETabla obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_INSERTAR(
                        ref intIcod,
                        obj.tablc_vdescripcion,
                        obj.tablc_cestado
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTabla(ETabla obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_MODIFICAR(
                        obj.tablc_iid_tipo_tabla,
                        obj.tablc_vdescripcion,
                        obj.tablc_cestado);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTabla(ETabla obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_ELIMINAR(
                        obj.tablc_iid_tipo_tabla);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tabla Registro

        public int insertarTablaRegistro(ETablaRegistro obj)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_REGISTRO_INSERTAR(
                        ref intIcod,
                        obj.tablc_iid_tipo_tabla,
                        obj.tarec_icorrelativo_registro,
                        obj.tarec_vdescripcion,
                        obj.tarec_vtipo_operacion,
                        obj.tarec_cestado,
                        obj.tarec_iid_tabla_registro_ingreso,
                        obj.tarec_iid_tabla_registro_salida
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaRegistro(ETablaRegistro obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_REGISTRO_MODIFICAR(
                        obj.tarec_iid_tabla_registro,
                        obj.tarec_vdescripcion,
                        obj.tarec_vtipo_operacion,
                        obj.tarec_cestado,
                        obj.tarec_iid_tabla_registro_ingreso,
                        obj.tarec_iid_tabla_registro_salida);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaRegistro(ETablaRegistro obj)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEA_TABLA_REGISTRO_ELIMINAR(
                        obj.tarec_iid_tabla_registro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Tipo Cambio
        public List<ETipoCambio> listarTipoCambio()
        {
            List<ETipoCambio> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoCambio>();
                    var query = dc.SGEAS_TIPO_CAMBIO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoCambio()
                        {
                            ticac_icod_tipo_cambio = item.ticac_icod_tipo_cambio,
                            ticac_fecha_tipo_cambio = Convert.ToDateTime(item.ticac_fecha_tipo_cambio),
                            ticac_tipo_cambio_compra = Convert.ToDecimal(item.ticac_tipo_cambio_compra),
                            ticac_tipo_cambio_venta = Convert.ToDecimal(item.ticac_tipo_cambio_venta)
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
        public int insertarTipoCambio(ETipoCambio oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_TIPO_CAMBIO_INSERTAR(
                    ref intIcod,
                    oBe.ticac_fecha_tipo_cambio,
                    oBe.ticac_tipo_cambio_compra,
                    oBe.ticac_tipo_cambio_venta);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoCambio(ETipoCambio oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_MODIFICAR(
                    oBe.ticac_icod_tipo_cambio,
                    oBe.ticac_fecha_tipo_cambio,
                    oBe.ticac_tipo_cambio_compra,
                    oBe.ticac_tipo_cambio_venta,
                    oBe.intUsuario,
                    oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoCambio(ETipoCambio oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_ELIMINAR(
                        oBe.ticac_icod_tipo_cambio,
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
        #region Tipo Cambio Euro
        public List<ETipoCambioEuro> listarTipoCambioEuro(int intEjercicio)
        {
            List<ETipoCambioEuro> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ETipoCambioEuro>();
                    var query = dc.SGEAS_TIPO_CAMBIO_EURO_LISTAR(intEjercicio);
                    foreach (var item in query)
                    {
                        lista.Add(new ETipoCambioEuro()
                        {
                            tceu_icod_tipo_cambio_euro = item.tceu_icod_tipo_cambio_euro,
                            tceu_sfecha_tipo_cambio_euro = Convert.ToDateTime(item.tceu_sfecha_tipo_cambio_euro),
                            tceu_tipo_cambio_euro_compra = Convert.ToDecimal(item.tceu_tipo_cambio_euro_compra),
                            tceu_tipo_cambio_euro_venta = Convert.ToDecimal(item.tceu_tipo_cambio_euro_venta)
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
        public int insertarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGEAS_TIPO_CAMBIO_EURO_INSERTAR(
                    ref intIcod,
                    oBe.tceu_sfecha_tipo_cambio_euro,
                    oBe.tceu_tipo_cambio_euro_compra,
                    oBe.tceu_tipo_cambio_euro_venta,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.tceu_flag_estado);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_EURO_MODIFICAR(
                    oBe.tceu_icod_tipo_cambio_euro,
                    oBe.tceu_tipo_cambio_euro_compra,
                    oBe.tceu_tipo_cambio_euro_venta,
                    oBe.intUsuario,
                    oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGEAS_TIPO_CAMBIO_EURO_ELIMINAR(
                        oBe.tceu_icod_tipo_cambio_euro,
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

        #region Envio de Informacion

        public void crearArchivoTransferencia(string ruta, string tabla)
        {

            StreamWriter fs = File.CreateText(ruta);
            //if (tabla == "SIGT_TABLA")
            //{
            //    #region SIGT_TABLA
            //    List<ETablaRegistro> mlisregistro = new List<ETablaRegistro>();
            //    List<ETabla> mlistTabla = new List<ETabla>();
            //    mlistTabla = ListarTablaTodo();
            //    mlisregistro = ListarTablaRegistroTodo();
            //    if (mlisregistro.Count > 0)
            //    {

            //        foreach (ETabla obj in mlistTabla)
            //        {
            //            fs.Write("SIGT_TABLA," + obj.tablc_iid_tipo_tabla + "," +
            //                     obj.tablc_vdescripcion + "," +
            //                     obj.tablc_cestado);
            //            fs.WriteLine();
            //        }
            //        fs.WriteLine();
            //        foreach (ETablaRegistro obj1 in mlisregistro)
            //        {
            //            fs.Write("SIGT_TABLA_REGISTRO," + obj1.tarec_iid_tabla_registro + "," +
            //                     obj1.tablc_iid_tipo_tabla + "," +
            //                     obj1.tarec_icorrelativo_registro + "," +
            //                     obj1.tarec_vdescripcion + "," +
            //                     obj1.tarec_nvalor_numerico + "," +
            //                     obj1.tarec_vvalor_texto + "," +
            //                     obj1.tarec_cestado + "," +
            //                     obj1.tarec_iusuario_crea + "," +
            //                     obj1.tarec_iusuario_modifica + "," +
            //                     obj1.tarec_sfecha_crea + "," +
            //                     obj1.tarec_sfecha_modifica + ",");
            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion
            //}
            //if (tabla == "SIGT_OPCIONES")
            //{
            //    #region SIGT_OPCIONES
            //    List<ETablaRegistro> mlisOpcionesDet = new List<ETablaRegistro>();
            //    List<ETabla> mlistOpciones = new List<ETabla>();
            //    mlistOpciones = ListarOpcionesTrans();
            //    mlisOpcionesDet = ListarOpcionesDetalleTrans();
            //    if (mlistOpciones.Count > 0)
            //    {

            //        foreach (ETabla obj in mlistOpciones)
            //        {
            //            fs.Write("SIGT_OPCIONES," + obj.tablc_iid_tipo_tabla + "," +
            //                     obj.tablc_vdescripcion + "," +
            //                     obj.tablc_cestado);
            //            fs.WriteLine();
            //        }
            //        fs.WriteLine();
            //        foreach (ETablaRegistro obj1 in mlisOpcionesDet)
            //        {
            //            fs.Write("SIGT_OPCIONES_REGISTRO," + obj1.tarec_iid_tabla_registro + "," +
            //                     obj1.tablc_iid_tipo_tabla + "," +
            //                     obj1.tarec_icorrelativo_registro + "," +
            //                     obj1.tarec_vdescripcion + "," +
            //                     obj1.tarec_nvalor_numerico + "," +
            //                     obj1.tarec_vvalor_texto + "," +
            //                     obj1.tarec_cestado);
            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion
            //}
            //if (tabla == "SIGT_ALMACEN")
            //{
            //    #region SIGT_ALMACEN
            //    List<EAlmacen> mlisAlmacen = new List<EAlmacen>();
            //    mlisAlmacen = ListarAlmacenTodo();
            //    if (mlisAlmacen.Count > 0)
            //    {
            //        foreach (EAlmacen objal in mlisAlmacen)
            //        {
            //            fs.Write("SIGT_ALMACEN," + objal.almac_icod_almacen + "," +
            //                     objal.almac_iid_almacen + "," +
            //                     objal.puvec_icod_punto_venta + "," +
            //                     objal.almac_vdescripcion + "," +
            //                     objal.almac_vdireccion + "," +
            //                     objal.almac_vrepresentante + "," +
            //                     objal.almac_iid_ubigeo + "," +
            //                     objal.almac_iusuario_crea + "," +
            //                     objal.almac_sfecha_crea + "," +
            //                     objal.almac_iusuario_modifica + "," +
            //                     objal.almac_sfecha_modifica + "," +
            //                     objal.almac_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_CAJA")
            //{
            //    #region PVT_CAJA
            //    List<ECaja> mlisCaja = new List<ECaja>();
            //    mlisCaja = ListarCajaTodo();
            //    if (mlisCaja.Count > 0)
            //    {
            //        foreach (ECaja objal in mlisCaja)
            //        {
            //            fs.Write("PVT_CAJA," + objal.cajac_icod_caja + "," +
            //                     objal.cajac_vcod_caja + "," +
            //                     objal.puvec_icod_punto_venta + "," +
            //                     objal.cajac_vdescripcion + "," +
            //                     objal.cajac_vNombreLocal + "," +
            //                     objal.cajac_vDirecLocal + "," +
            //                     objal.cajac_vNumeroSerie + "," +
            //                     objal.cajac_vSerieImpres + "," +
            //                     objal.cajac_inro_serie_factura + "," +
            //                     objal.cajac_iNumCorrelTck + "," +
            //                     objal.cajac_inro_serie_boleta + "," +
            //                     objal.cajac_inro_serie_nota_credito + "," +
            //                     objal.cajac_icorrelativo_factura + "," +
            //                     objal.cajac_icorrelativo_boleta + "," +
            //                     objal.cajac_icorrelativo_nota_credito + "," +
            //                     objal.cajac_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "SIGT_PRODUCTOS")
            //{
            //    #region SIGT_PRODUCTOS
            //    List<EProducto> mlisProducto = new List<EProducto>();
            //    mlisProducto = ListarProductosTodo();
            //    if (mlisProducto.Count > 0)
            //    {
            //        foreach (EProducto objal in mlisProducto)
            //        {
            //            fs.Write("SIGT_PRODUCTOS," + objal.pr_icod_producto + "," +
            //                     objal.pr_iid_producto + "," +
            //                     objal.pr_iid_marca + "," +
            //                     objal.pr_iid_modelo + "," +
            //                     objal.pr_iid_color + "," +
            //                     objal.pr_iid_talla + "," +
            //                     objal.pr_vcodigo_externo + "," +
            //                     objal.pr_vdescripcion_producto + "," +
            //                     objal.pr_vabreviatura_producto + "," +
            //                     objal.pr_isituacion_producto + "," +
            //                     objal.pr_iestado_producto + "," +
            //                     objal.pr_tarec_icorrelativo + "," +
            //                     objal.unidc_icod_unidad_medida);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_PUNTO_VENTA")
            //{
            //    #region PVT_PUNTO_VENTA
            //    List<EPuntoVenta> mlisPuntoVenta = new List<EPuntoVenta>();
            //    mlisPuntoVenta = ListarPuntoVentaTodo();
            //    if (mlisPuntoVenta.Count > 0)
            //    {
            //        foreach (EPuntoVenta objal in mlisPuntoVenta)
            //        {
            //            fs.Write("PVT_PUNTO_VENTA," + objal.puvec_icod_punto_venta + "," +
            //                     objal.puvec_vcod_punto_venta + "," +
            //                     objal.puvec_vdescripcion + "," +
            //                     objal.puvec_iactivo);
            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_LISTA_PRECIOS")
            //{
            //    #region PVT_LISTA_PRECIOS
            //    List<EListaPrecios> mliPRECIOS = new List<EListaPrecios>();
            //    List<EListaPreciosDetalle> mliPRECIOSDeta = new List<EListaPreciosDetalle>();
            //    mliPRECIOS = ListarListaPreciosTodo();
            //    mliPRECIOSDeta = ListarListaPrecioDetalleTodo();
            //    if (mliPRECIOS.Count > 0)
            //    {
            //        foreach (EListaPrecios obj in mliPRECIOS)
            //        {
            //            fs.Write("PVT_LISTA_PRECIOS," + obj.lprec_icod_precio + "," +
            //                     obj.pr_icod_producto + "," +
            //                     obj.pr_vcodigo_externo + "," +
            //                     obj.pr_vdescripcion_producto + "," +
            //                     obj.lprec_nprecio_costo + "," +
            //                     obj.lprec_nporc_utilidad + "," +
            //                     obj.lprec_nprecio_vtamin + "," +
            //                     obj.lprec_nprecio_vtamay + "," +
            //                     obj.lprec_nprecio_vtaotros + "," +
            //                     obj.lprec_bdetalle + "," +
            //                     obj.lprec_iactivo
            //                     );
            //            fs.WriteLine();
            //        }
            //        fs.WriteLine();
            //        foreach (EListaPreciosDetalle obj1 in mliPRECIOSDeta)
            //        {
            //            fs.Write("PVT_LISTA_PRECIOS_DET," + obj1.lpred_icod_det_precio + "," +
            //                     obj1.lprec_icod_precio + "," +
            //                     obj1.lpred_vrango_tallas + "," +
            //                     obj1.lpred_nprecio_vtamin + "," +
            //                     obj1.lpred_nprecio_vtamay + "," +
            //                     obj1.lpred_nprecio_vtaotros + "," +
            //                     obj1.lpred_iactivo);

            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion

            //}
            //if (tabla == "SIGT_MODELO")
            //{
            //    #region SIGT_MODELO
            //    List<EModelo> mlisMODELO = new List<EModelo>();
            //    mlisMODELO = ListarModeloTodoS();
            //    if (mlisMODELO.Count > 0)
            //    {
            //        foreach (EModelo objal in mlisMODELO)
            //        {
            //            fs.Write("SIGT_MODELO," + objal.mo_icod_modelo + "," +
            //                     objal.mo_iid_modelo + "," +
            //                     objal.pr_iid_tipo + "," +
            //                     objal.pr_iid_categoria + "," +
            //                     objal.pr_iid_linea + "," +
            //                     objal.pr_iid_capellada + "," +
            //                     objal.pr_iid_planta + "," +
            //                     objal.pr_iid_forro + "," +
            //                     objal.pr_iid_tipo_marca + "," +
            //                     objal.pr_iid_taco + "," +
            //                     objal.pr_iid_horma + "," +
            //                     objal.mo_vdescripcion + "," +
            //                     objal.tarec_icorrelativo + "," +
            //                     objal.mo_estado + "," +
            //                     objal.mo_ruta);
            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_UNIDAD_MEDIDA")
            //{
            //    #region PVT_UNIDAD_MEDIDA
            //    List<EUnidadMedida> mlisUniMe = new List<EUnidadMedida>();
            //    mlisUniMe = ListarUnidadMedidaTodo();
            //    if (mlisUniMe.Count > 0)
            //    {
            //        foreach (EUnidadMedida objal in mlisUniMe)
            //        {
            //            fs.Write("PVT_UNIDAD_MEDIDA," + objal.unidc_icod_unidad_medida + "," +
            //                     objal.unidc_iid_unidad_medida + "," +
            //                     objal.unidc_vabreviatura_unidad_medida + "," +
            //                     objal.unidc_vdescripcion + "," +
            //                     objal.unidc_iactivo);

            //            fs.WriteLine();
            //        }
            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_GIRO")
            //{
            //    #region PVT_GIRO
            //    List<EGiro> mlisGiro = new List<EGiro>();
            //    mlisGiro = ListarGiroTodo();
            //    if (mlisGiro.Count > 0)
            //    {
            //        foreach (EGiro objal in mlisGiro)
            //        {
            //            fs.Write("PVT_GIRO," + objal.giroc_icod_giro + "," +
            //                     objal.giroc_vcod_giro + "," +
            //                     objal.giroc_vnombre_giro + "," +
            //                     objal.giroc_iactivo
            //                    );
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion

            //}
            //if (tabla == "PVT_UBICACION")
            //{
            //    #region PVT_UBICACION
            //    List<EUbicacion> mlisUbica = new List<EUbicacion>();
            //    mlisUbica = ListarUbicacionTodo();
            //    if (mlisUbica.Count > 0)
            //    {
            //        foreach (EUbicacion objal in mlisUbica)
            //        {
            //            fs.Write("PVT_UBICACION," + objal.ubicc_icod_ubicacion + "," +
            //                     objal.tablc_iid_tipo_ubicacion + "," +
            //                     objal.ubicc_vcod_ubicacion + "," +
            //                     objal.ubicc_vnombre_ubicacion + "," +
            //                     objal.ubicc_icod_ubicacion_padre + "," +
            //                     objal.ubicc_iactivo
            //                  );
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion

            //}
            //if (tabla == "PVT_PERSONAL")
            //{
            //    #region PVT_PERSONAL
            //    List<EPersonal> mlisPersonal = new List<EPersonal>();
            //    mlisPersonal = ListarPersonalTodo();
            //    if (mlisPersonal.Count > 0)
            //    {
            //        foreach (EPersonal objal in mlisPersonal)
            //        {
            //            fs.Write("PVT_PERSONAL," + objal.persc_icod_personal + "," +
            //                     objal.persc_vcod_personal + "," +
            //                     objal.persc_vnombre_personal + "," +
            //                     objal.persc_vdireccion + "," +
            //                     objal.persc_vnumero_telefono + "," +
            //                     objal.persc_vnumero_dni + "," +
            //                     objal.persc_vcorreo + "," +
            //                     objal.tablc_iid_tipo_personal + "," +
            //                     objal.persc_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "PVT_TRANSPORTISTA")
            //{
            //    #region PVT_TRANSPORTISTA
            //    List<ETransportista> mlisTrans = new List<ETransportista>();
            //    mlisTrans = ListarTransportistaTodo();
            //    if (mlisTrans.Count > 0)
            //    {
            //        foreach (ETransportista objal in mlisTrans)
            //        {
            //            fs.Write("PVT_TRANSPORTISTA," + objal.tranc_icod_transportista + "," +
            //                     objal.tranc_iid_transportista + "," +
            //                     objal.tranc_vnombre_transportista + "," +
            //                     objal.tranc_vruc + "," +
            //                     objal.tranc_vdireccion + "," +
            //                     objal.tranc_vnumero_telefono + "," +
            //                     objal.tranc_vnumero_dni + "," +
            //                     objal.tranc_vnombre_conductor + "," +
            //                     objal.tranc_vnum_licencia_conducir + "," +
            //                     objal.tranc_vnum_placa + "," +
            //                     objal.tranc_vnum_matricula + "," +
            //                     objal.tranc_iactivo);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            //if (tabla == "SIGT_PROVEEDOR")
            //{
            //    #region PVT_TRANSPORTISTA
            //    List<EProveedor> mlisProvee = new List<EProveedor>();
            //    mlisProvee = ListarProveedorTodo();
            //    if (mlisProvee.Count > 0)
            //    {
            //        foreach (EProveedor objal in mlisProvee)
            //        {
            //            fs.Write("SIGT_PROVEEDOR," + objal.iid_icod_proveedor + "," +
            //                     objal.iid_proveedor + "," +
            //                     objal.vcod_proveedor + "," +
            //                     objal.vnombre + "," +
            //                     objal.vpaterno + "," +
            //                     objal.vmaterno + "," +
            //                     objal.vnombrecompleto + "," +
            //                     objal.iid_tipo_persona + "," +
            //                     objal.vcomercial + "," +
            //                     objal.vdireccion + "," +
            //                     objal.vtelefono + "," +
            //                     objal.vfax + "," +
            //                     objal.isituacion + "," +
            //                     objal.vrepresentante + "," +
            //                     objal.vruc + "," +
            //                     objal.vempresarelacionada + "," +
            //                     objal.iactivo + "," +
            //                     objal.vdni + "," +
            //                     objal.proc_vcorreo + "," +
            //                     objal.proc_sfecha + "," +
            //                     objal.proc_tipo_doc + "," +
            //                     objal.ubicc_icod_ubicacion + "," +
            //                     objal.anac_icod_analitica);
            //            fs.WriteLine();
            //        }

            //    }
            //    #endregion
            //}
            fs.Close();
        }

        public List<ESendInfo> ListarSendInfo()
        {
            List<ESendInfo> lista = null;

            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<ESendInfo>();
                    var query = dc.SGE_PVT_SEND_INFO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ESendInfo()
                        {
                            trans_icod_trans = Convert.ToInt32(item.trans_icod_trans),
                            trans_descripcion = item.trans_descripcion,
                            trans_nombre_tabla = item.trans_nombre_tabla,
                            trans_nombre_archivo = item.trans_nombre_archivo,
                            sfecha_crea = Convert.ToDateTime(item.sfecha_crea),
                            estado = true
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

        public int ObtenerCorrelativoTablaRegistro(int serie)
        {
            int NumDocumento = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_OBTENER_CORRELATIVO_TABLA_REGISTRO_PROD(serie);
                    foreach (var item in query)
                    {
                        NumDocumento = Convert.ToInt32(item.Num);
                        ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        #region Parametro Produccion
        public void modificarCorrelativoOP(EParametroProduccion oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PARAMETRO_PRODUCCION_MODIFICAR_CORRELATIVO_OP(
                        oBe.pmprc_icod_parametro_produccion,
                        oBe.pmprc_inumero_orden_pedido,
                        oBe.pmprc_inumero_ficha_tecnica,
                        oBe.pmprc_inumero_orden_produccion,
                        oBe.pmprc_inumero_nota_pedido
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EParametroProduccion> listarParametroProduccion()
        {
            List<EParametroProduccion> lista = new List<EParametroProduccion>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var query = dc.SGE_PVT_PARAMETRO_PRODUCCION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EParametroProduccion()
                        {
                            pmprc_icod_parametro_produccion = item.pmprc_icod_parametro_produccion,
                            pmprc_inumero_orden_pedido = Convert.ToInt32(item.pmprc_inumero_orden_pedido),
                            pmprc_inumero_ficha_tecnica = Convert.ToInt32(item.pmprc_inumero_ficha_tecnica),
                            pmprc_inumero_orden_produccion = Convert.ToInt32(item.pmprc_inumero_orden_produccion),
                            pmprc_inumero_nota_pedido = Convert.ToInt32(item.pmprc_inumero_nota_pedido),
                            pmprc_flag_estado = Convert.ToBoolean(item.pmprc_flag_estado),
                            pmprc_vruta = item.pmprc_vruta,
                            pmprc_vruta_base = item.pmprc_vruta_base,
                            pmprc_vficha_tecnica = item.pmprc_vficha_tecnica,
                            pmprc_vorden_pedido = item.pmprc_vorden_pedido,
                            pmprc_vorden_produccion = item.pmprc_vorden_produccion,
                            pmprc_vmodelo = item.pmprc_vmodelo
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
        public void modificarParametroProduccion(EParametroProduccion oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGE_PVT_PARAMETRO_PRODUCCION_MODIFICAR(
                        oBe.pmprc_icod_parametro_produccion,
                        oBe.pmprc_inumero_orden_pedido,
                        oBe.pmprc_inumero_ficha_tecnica,
                        oBe.pmprc_inumero_orden_produccion,
                        oBe.pmprc_inumero_nota_pedido,
                        oBe.intusuario,
                        oBe.vpc,
                        oBe.pmprc_vruta,
                        oBe.pmprc_vruta_base,
                        oBe.pmprc_vficha_tecnica,
                        oBe.pmprc_vorden_pedido,
                        oBe.pmprc_vorden_produccion,
                        oBe.pmprc_vmodelo

                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Imagen Cheque
        public List<EImagenCheque> listarImagenCheque(int mobac_icod_correlativo)
        {
            List<EImagenCheque> lista = null;
            int cont = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    lista = new List<EImagenCheque>();
                    var query = dc.SGE_IMAGEN_CHEQUE_LISTAR(mobac_icod_correlativo);
                    foreach (var item in query)
                    {
                        cont += 1;
                        lista.Add(new EImagenCheque()
                        {
                            ic_icod_imagen_cheque = item.ic_icod_imagen_cheque,
                            mobac_icod_correlativo = Convert.ToInt32(item.mobac_icod_correlativo),
                            ic_vruta = item.ic_vruta
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
        public int insertarImagenCheque(EImagenCheque oBe)
        {
            int? intIcod = 0;
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {

                    dc.SGE_IMAGEN_CHEQUE_INSERTAR(
                    ref intIcod,
                    oBe.mobac_icod_correlativo,
                    oBe.ic_vruta,
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
        public void modificarImagenCheque(EImagenCheque oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGE_IMAGEN_CHEQUE_MODIFICAR(
                    oBe.ic_icod_imagen_cheque,
                    oBe.mobac_icod_correlativo,
                    oBe.ic_vruta,
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
        public void eliminarImagenCheque(EImagenCheque oBe)
        {
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    dc.SGE_IMAGEN_CHEQUE_ELIMINAR(
                        oBe.ic_icod_imagen_cheque,
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

        public List<ComparacionKardexStock> ComparacionKardexStockListar(int anio)
        {
            var lista = new List<ComparacionKardexStock>();
            try
            {
                using (AdministracionSistemaDataContext dc = new AdministracionSistemaDataContext(Helper.conexion()))
                {
                    var collection = dc.SGE_VERIFICAR_KARDEX_STOCK(anio);
                    foreach (var item in collection)
                    {
                        lista.Add(new ComparacionKardexStock()
                        {
                            CantidadKardex = Convert.ToDecimal(item.CantidadKardex),
                            CantidadStock = item.CantidadStock,
                            CodigoProducto = item.CodigoProducto,
                            DescripcionAlmacen = item.DescripcionAlmacen,
                            DescripcionProducto = item.DescripcionProducto,
                            IdAlmacen = Convert.ToInt32(item.IdAlmacen),
                            IdProducto = item.IdProducto.Value
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

    }
}
