using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.DataAccess
{
    public class PlanillasData
    {
        #region Empresa

        public List<EEmpresa> ListarEmpresa()
        {
            List<EEmpresa> lista = new List<EEmpresa>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEA_EMPRESA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EEmpresa()
                        {
                            cia_icod_empresa = item.cia_icod_empresa,
                            cia_vrazon_social = item.cia_vrazon_social,
                            cia_vruc = item.cia_vruc,
                            cia_vdireccion_empr = item.cia_vdireccion_empr,
                            cia_vtelefonos = item.cia_vtelefonos,
                            cia_vregistro_patronal = item.cia_vregistro_patronal,
                            cia_vrepresentante_legal = item.cia_vrepresentante_legal,
                            cia_vpagina_web = item.cia_vpagina_web

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

        public void modificarEmpresa(EEmpresa oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEA_EMPRESA_MODIFICAR(
                        oBe.cia_icod_empresa,
                        oBe.cia_vrazon_social,
                        oBe.cia_vruc,
                        oBe.cia_vdireccion_empr,
                        oBe.cia_vtelefonos,
                        oBe.cia_vregistro_patronal,
                        oBe.cia_vrepresentante_legal,
                        oBe.cia_vpagina_web,
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

        #region Cargos

        public List<ECargo> ListarCargo()
        {
            List<ECargo> lista = new List<ECargo>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_CARGO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ECargo()
                        {
                            carg_icod_cargo = item.carg_icod_cargo,
                            carg_vdescripcion = item.carg_vdescripcion,
                            carg_vabreviado = item.carg_vabreviado


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

        public int InsertarCargo(ECargo oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CARGO_INSERTAR(
                        ref intIcod,
                        oBe.carg_vdescripcion,
                        oBe.carg_vabreviado,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.carg_sflag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarCargo(ECargo oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CARGO_MODIFICAR(
                        oBe.carg_icod_cargo,
                        oBe.carg_vdescripcion,
                        oBe.carg_vabreviado,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarCargo(ECargo oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CARGO_ELIMINAR(
                        oBe.carg_icod_cargo,
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

        #region Areas

        public List<EAreas> ListarAreas()
        {
            List<EAreas> lista = new List<EAreas>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_AREAS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EAreas()
                        {
                            arec_icod_cargo = item.arec_icod_cargo,
                            arec_vdescripcion = item.arec_vdescripcion,
                            arec_vabreviado = item.arec_vabreviado


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

        public int InsertarAreas(EAreas oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_AREAS_INSERTAR(
                        ref intIcod,
                        oBe.arec_vdescripcion,
                        oBe.arec_vabreviado,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.arec_sflag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarAreas(EAreas oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_AREAS_MODIFICAR(
                        oBe.arec_icod_cargo,
                        oBe.arec_vdescripcion,
                        oBe.arec_vabreviado,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarAreas(EAreas oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_AREAS_ELIMINAR(
                        oBe.arec_icod_cargo,
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

        #region FondosPensiones
        public List<EFondosPensiones> ListarFondosPensiones()
        {
            List<EFondosPensiones> lista = new List<EFondosPensiones>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_FONDOS_PENSIONES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EFondosPensiones()
                        {
                            fdpc_icod_fondo_pension = item.fdpc_icod_fondo_pension,
                            fdpc_iid_vcodigo_fondo = item.fdpc_iid_vcodigo_fondo,
                            fdpc_vdescripcion = item.fdpc_vdescripcion,
                            fdpc_nporcentaje_fijo = item.fdpc_nporcentaje_fijo,
                            fdpc_nporcentaje_mixto = item.fdpc_nporcentaje_mixto,
                            tablc_iid_tipo_fondo_pensiones = item.tablc_iid_tipo_fondo_pensiones,
                            fdpc_situacion = item.fdpc_situacion


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
        public int InsertarFondosPensiones(EFondosPensiones oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_INSERTAR(
                        ref intIcod,
                        oBe.fdpc_iid_vcodigo_fondo,
                        oBe.fdpc_vdescripcion,
                        oBe.fdpc_nporcentaje_fijo,
                        oBe.fdpc_nporcentaje_mixto,
                        oBe.tablc_iid_tipo_fondo_pensiones,
                        oBe.fdpc_situacion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fdpc_flag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarFondosPensiones(EFondosPensiones oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_MODIFICAR(
                        oBe.fdpc_icod_fondo_pension,
                        oBe.fdpc_vdescripcion,
                        oBe.fdpc_nporcentaje_fijo,
                        oBe.fdpc_nporcentaje_mixto,
                        oBe.tablc_iid_tipo_fondo_pensiones,
                        oBe.fdpc_situacion,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarFondosPensiones(EFondosPensiones oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_ELIMINAR(
                        oBe.fdpc_icod_fondo_pension,
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

        #region FondosPensionesConceptos

        public List<EFondosPensionesConceptos> ListarFondosPensionesConceptos(int codFondosPensiones)
        {
            List<EFondosPensionesConceptos> lista = new List<EFondosPensionesConceptos>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_FONDOS_PENSIONES_CONCEPTOS_LISTAR(codFondosPensiones);
                    foreach (var item in query)
                    {
                        lista.Add(new EFondosPensionesConceptos()
                        {
                            fdpd_icod_fondo_pension_concepto = item.fdpd_icod_fondo_pension_concepto,
                            //fdpc_icod_fondo_pension = item.fdpc_icod_fondo_pension,
                            fdpd_iid_vcodigo_fondo_concepto = item.fdpd_iid_vcodigo_fondo_concepto,
                            fdpd_vdescripcion_concepto = item.fdpd_vdescripcion_concepto,
                            fdpd_nporcentaje_concepto = item.fdpd_nporcentaje_concepto,
                            fdpd_ntope_concpeto = item.fdpd_ntope_concpeto,
                            fdpc_icod_fondo_pension = item.fdpc_icod_fondo_pension

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

        public int InsertarFondosPensionesConceptos(EFondosPensionesConceptos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_CONCEPTOS_INSERTAR(
                        ref intIcod,
                        oBe.fdpc_icod_fondo_pension,
                        oBe.fdpd_iid_vcodigo_fondo_concepto,
                        oBe.fdpd_vdescripcion_concepto,
                        oBe.fdpd_nporcentaje_concepto,
                        oBe.fdpd_ntope_concpeto,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fdpd_flag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarFondosPensionesConceptos(EFondosPensionesConceptos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_CONCEPTOS_MODIFICAR(
                        oBe.fdpd_icod_fondo_pension_concepto,
                        oBe.fdpd_vdescripcion_concepto,
                        oBe.fdpd_nporcentaje_concepto,
                        oBe.fdpd_ntope_concpeto,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarFondosPensionesConceptos(EFondosPensionesConceptos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_CONCEPTO_ELIMINAR(
                        oBe.fdpd_icod_fondo_pension_concepto,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarPorcentajeFondoFijo(int CodPension)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_ACTUALIZAR_FONDOS_PENSIONES_FIJO(CodPension);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPorcentajeFijo(int codPension, int usuario, string pc)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CONCEPTOS_FIJOS_ELIMINAR(

                        codPension,
                        usuario,
                        pc
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FondosPensionesConceptasMixtas

        public List<EFondosPensionesMixtas> ListarFondosPensionesMixtas(int codFondosPensiones)
        {
            List<EFondosPensionesMixtas> lista = new List<EFondosPensionesMixtas>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_FONDOS_PENSIONES_CONCEPTOS_MIXTOS_LISTAR(codFondosPensiones);
                    foreach (var item in query)
                    {
                        lista.Add(new EFondosPensionesMixtas()
                        {
                            fdpd2_icod_fp_concepto_mixto = item.fdpd2_icod_fp_concepto_mixto,
                            fdpd2_iid_vcodigo_fp_concepto_mixto = item.fdpd2_iid_vcodigo_fp_concepto_mixto,
                            fdpd2_vdescripcion_concepto_mixto = item.fdpd2_vdescripcion_concepto_mixto,
                            fdpd2_nporcentaje_concepto_mixto = Convert.ToDecimal(item.fdpd2_nporcentaje_concepto_mixto),
                            fdpd2_ntope_concepto_mixto = Convert.ToDecimal(item.fdpd2_ntope_concepto_mixto),
                            fdpc_icod_fondo_pension = item.fdpc_icod_fondo_pension

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

        public int InsertarFondosPensionesMixtas(EFondosPensionesMixtas oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_CONCEPTOS_MIXTAS_INSERTAR(
                        ref intIcod,
                        oBe.fdpc_icod_fondo_pension,
                        oBe.fdpd2_iid_vcodigo_fp_concepto_mixto,
                        oBe.fdpd2_vdescripcion_concepto_mixto,
                        oBe.fdpd2_nporcentaje_concepto_mixto,
                        oBe.fdpd2_ntope_concepto_mixto,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.fdpd2_flag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarFondosPensionesMixtas(EFondosPensionesMixtas oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_CONCEPTOS_MIXTAS_MODIFICAR(
                        oBe.fdpd2_icod_fp_concepto_mixto,
                        oBe.fdpd2_vdescripcion_concepto_mixto,
                        oBe.fdpd2_nporcentaje_concepto_mixto,
                        oBe.fdpd2_ntope_concepto_mixto,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarFondosPensionesMixtas(EFondosPensionesMixtas oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_FONDOS_PENSIONES_CONCEPTO_MIXTAS_ELIMINAR(
                        oBe.fdpd2_icod_fp_concepto_mixto,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarPorcentajeFondoMixto(int CodPension)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_ACTUALIZAR_FONDOS_PENSIONES_MIXTO(CodPension);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarPorcentajeMixto(int codPension, int usuario, string pc)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CONCEPTOS_MIXTOS_ELIMINAR(
                        codPension,
                       usuario,
                      pc

                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region TablaPlanilla

        public List<ETablaPlanilla> ListarTablaPlanilla()
        {
            List<ETablaPlanilla> lista = new List<ETablaPlanilla>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_TABLA_PLANILLA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaPlanilla()
                        {
                            tbpc_icod_tabla_planilla = item.tbpc_icod_tabla_planilla,
                            tbpc_iid_vcodigo_tabla_planilla = item.tbpc_iid_vcodigo_tabla_planilla,
                            tbpc_vdescripcion = item.tbpc_vdescripcion,


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

        public int InsertarTablaPlanilla(ETablaPlanilla oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_TABLA_PLANILLA_INSERTAR(
                        ref intIcod,
                        oBe.tbpc_iid_vcodigo_tabla_planilla,
                        oBe.tbpc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.tbpc_flag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarTablaPlanilla(ETablaPlanilla oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_TABLA_PLANILLA_MODIFICAR(
                        oBe.tbpc_icod_tabla_planilla,
                        oBe.tbpc_vdescripcion,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarTablaPlanilla(ETablaPlanilla oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_TABLA_PLANILLA_ELIMINAR(
                        oBe.tbpc_icod_tabla_planilla,
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


        #region TablaPlanillaDetalle

        public List<ETablaPlanillaDetalle> ListarTablePlanillaDetalle(int codTablaPlanilla)
        {
            List<ETablaPlanillaDetalle> lista = new List<ETablaPlanillaDetalle>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_TABLA_PLANILLA_DETALLE_LISTAR(codTablaPlanilla);
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaPlanillaDetalle()
                        {
                            tbpd_icod_tabla_planilla_detalle = item.tbpd_icod_tabla_planilla_detalle,
                            //fdpc_icod_fondo_pension = item.fdpc_icod_fondo_pension,
                            tbpd_iid_vcodigo_tabla_planilla_detalle = item.tbpd_iid_vcodigo_tabla_planilla_detalle,
                            tbpd_vabreviado_detalle = item.tbpd_vabreviado_detalle,
                            tbpd_vdescripcion_detalle = item.tbpd_vdescripcion_detalle,
                            tbpd_votros_datos = item.tbpd_votros_datos

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

        public int InsertarTablaPlanillaDetalle(ETablaPlanillaDetalle oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_TABLA_PLANILLA_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.tbpc_icod_tabla_planilla,
                        oBe.tbpd_iid_vcodigo_tabla_planilla_detalle,
                        oBe.tbpd_vdescripcion_detalle,
                        oBe.tbpd_vabreviado_detalle,
                        oBe.tbpd_votros_datos,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.tbpd_flag_estado

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarTablaPlanillaDetalle(ETablaPlanillaDetalle oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_TABLA_PLANILLA_DETALLE_MODIFICAR(
                        oBe.tbpd_icod_tabla_planilla_detalle,
                        oBe.tbpd_vdescripcion_detalle,
                        oBe.tbpd_vabreviado_detalle,
                        oBe.tbpd_votros_datos,
                        oBe.intUsuario,
                        oBe.strPc);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarTablaPlanillaDetalle(ETablaPlanillaDetalle oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_TABLA_PLANILLA_DETALLE_ELIMINAR(
                        oBe.tbpd_icod_tabla_planilla_detalle,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarDetalleTablaPlanilla(int cod, int usuario, string pc)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_DETALLE_TABLA_ELIMINAR(
                        cod,
                        usuario,
                        pc
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DatosPersonal
        public List<EPersonal> listarPersonal()
        {
            List<EPersonal> lista = new List<EPersonal>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EPersonal>();
                    var query = dc.SGEP_PERSONAL_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPersonal()
                        {
                            perc_icod_personal = Convert.ToInt32(item.perc_icod_personal),
                            perc_iid_personal = item.perc_iid_personal,
                            perc_sfecha_registro = item.perc_sfecha_registro,

                            tbpd_icod_tip_doc = item.tbpd_icod_tip_doc,
                            perc_vnum_doc = item.perc_vnum_doc,
                            tbpd_icod_pais_emi_doc = item.tbpd_icod_pais_emi_doc,
                            perc_sfecha_nacimiento = item.perc_sfecha_nacimiento,
                            perc_vapellido_pat = item.perc_vapellido_pat,
                            perc_vapellido_mat = item.perc_vapellido_mat,
                            perc_vnombres = item.perc_vnombres,
                            perc_icod_sexo = item.perc_icod_sexo,
                            tbpd_icod_nacionalidad = item.tbpd_icod_nacionalidad,
                            tbpd_icod_telf_ldn = item.tbpd_icod_telf_ldn,
                            perc_vnum_telf_ldn = item.perc_vnum_telf_ldn,
                            perc_vcorreo = item.perc_vcorreo,
                            tbpd_icod_tip_via = item.tbpd_icod_tip_via,
                            perc_vnomb_via = item.perc_vnomb_via,
                            perc_vnum_via = item.perc_vnum_via,
                            perc_vdepartamento = item.perc_vdepartamento,
                            perc_vinterior = item.perc_vinterior,
                            perc_vmanzana = item.perc_vmanzana,
                            perc_vlote = item.perc_vlote,
                            perc_vkilometro = item.perc_vkilometro,
                            perc_vblock = item.perc_vblock,
                            perc_vetapa = item.perc_vetapa,
                            tbpd_icod_tip_zona = item.tbpd_icod_tip_zona,
                            perc_vnomb_zona = item.perc_vnomb_zona,
                            perc_vreferencia = item.perc_vreferencia,
                            tbpd_icod_ubi_geo = item.tbpd_icod_ubi_geo,
                            tarec_icod_est_civil = item.tarec_icod_est_civil,//--modif


                            tablc_iid_tipo_cargo = item.tablc_iid_tipo_cargo,
                            tablc_iid_tipo_area = item.tablc_iid_tipo_area,
                            perc_vruc = item.perc_vruc,
                            perc_vnum_seguro = item.perc_vnum_seguro,
                            perc_icod_tip_fdo_pension = item.perc_icod_tip_fdo_pension,
                            perc_sfech_inicio = item.perc_sfech_inicio,
                            perc_icod_afp = item.perc_icod_afp,
                            perc_icod_tip_comision = item.perc_icod_tip_comision,
                            perc_vcuspp = item.perc_vcuspp,
                            perc_beps = item.perc_beps,
                            perc_icod_tip_personal = item.perc_icod_tip_personal,
                            perc_nmont_basico = item.perc_nmont_basico,
                            tarec_icod_moneda = item.tarec_icod_moneda,
                            perc_brta_5ta = item.perc_brta_5ta,
                            perc_nmont_ant_afecto = item.perc_nmont_ant_afecto,
                            perc_nmont_retenido = item.perc_nmont_retenido,
                            perc_basig_familiar = item.perc_basig_familiar,
                            perc_sfech_cese = item.perc_sfech_cese,


                            perc_iid_situacion_perso = Convert.ToInt32(item.perc_iid_situacion_perso),
                            strCargo = item.strCargo,
                            strArea = item.strArea,
                            strEstado = (Convert.ToInt32(item.perc_iid_situacion_perso) == 1) ? "Activo" : "Inactivo",
                            ApellNomb = item.ApellNomb,
                            perc_hora_inical_LV = item.perc_hora_inicio_lv,
                            perc_hora_final_LV = item.perc_hora_final_lv,
                            perc_hora_inical_S = item.perc_hora_inicio_s,
                            perc_hora_final_S = item.perc_hora_final_s,
                            perc_hora_total_LV = item.perc_hora_total_lv,
                            perc_hora_total_S = item.perc_hora_total_s,
                            perc_icod_tip_contrato = item.perc_icod_tip_contrato,
                            perc_icod_banc_haber = item.perc_icod_banc_haber,
                            perc_vbanc_haber = item.perc_vbanc_haber,
                            perc_icod_banc_cts = item.perc_icod_banc_cts,
                            perc_vbanc_cts = item.perc_vbanc_cts,
                            strEPS = Convert.ToInt32(item.perc_beps) == 0 ? "ESSALUD" : "EPS",
                            perc_sfecha_cese = item.perc_sfecha_cese,
                            perc_icod_motiv_cese = item.perc_icod_motiv_cese,
                            perc_retenc_judicial = item.perc_retenc_judicial,
                            NumAnalitica = item.NumAnalitica,
                            perc_nasig_transporte = item.perc_nasig_transporte

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
        public int insertarPersonal(EPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGEP_PERSONAL_INSERTAR(
                    ref intIcod,
                    oBe.perc_iid_personal,
                    oBe.perc_sfecha_registro,
                    oBe.tbpd_icod_tip_doc,
                    oBe.perc_vnum_doc,
                    oBe.tbpd_icod_pais_emi_doc,
                    oBe.perc_sfecha_nacimiento,
                    oBe.perc_vapellido_pat,
                    oBe.perc_vapellido_mat,
                    oBe.perc_vnombres,
                    oBe.perc_icod_sexo,
                    oBe.tbpd_icod_nacionalidad,
                    oBe.tbpd_icod_telf_ldn,
                    oBe.perc_vnum_telf_ldn,
                    oBe.perc_vcorreo,
                    oBe.tbpd_icod_tip_via,
                    oBe.perc_vnomb_via,
                    oBe.perc_vnum_via,
                    oBe.perc_vdepartamento,
                    oBe.perc_vinterior,
                    oBe.perc_vmanzana,
                    oBe.perc_vlote,
                    oBe.perc_vkilometro,
                    oBe.perc_vblock,
                    oBe.perc_vetapa,
                    oBe.tbpd_icod_tip_zona,
                    oBe.perc_vnomb_zona,
                    oBe.perc_vreferencia,
                    oBe.tbpd_icod_ubi_geo,
                    oBe.tarec_icod_est_civil,


                    oBe.tablc_iid_tipo_cargo,
                    oBe.tablc_iid_tipo_area,
                    oBe.perc_vruc,
                    oBe.perc_vnum_seguro,
                    oBe.perc_icod_tip_fdo_pension,
                    oBe.perc_sfech_inicio,
                    oBe.perc_icod_afp,
                    oBe.perc_icod_tip_comision,
                    oBe.perc_vcuspp,
                    oBe.perc_beps,
                    oBe.perc_icod_tip_personal,
                    oBe.perc_nmont_basico,
                    oBe.tarec_icod_moneda,
                    oBe.perc_brta_5ta,
                    oBe.perc_nmont_ant_afecto,
                    oBe.perc_nmont_retenido,
                    oBe.perc_basig_familiar,
                    oBe.perc_sfech_cese,

                    oBe.perc_iid_situacion_perso,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.perc_hora_inical_LV,
                    oBe.perc_hora_final_LV,
                    oBe.perc_hora_inical_S,
                    oBe.perc_hora_final_S,
                    oBe.perc_hora_total_LV,
                    oBe.perc_hora_total_S,
                     oBe.perc_icod_tip_contrato,
                    oBe.perc_icod_banc_haber,
                    oBe.perc_vbanc_haber,
                    oBe.perc_icod_banc_cts,
                    oBe.perc_vbanc_cts,
                     oBe.perc_sfecha_cese,
                    oBe.perc_icod_motiv_cese,
                    oBe.perc_retenc_judicial,
                    oBe.perc_nasig_transporte
                    );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPersonal(EPersonal oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_PERSONAL_MODIFICAR(
                    oBe.perc_icod_personal,
                    oBe.perc_iid_personal,
                    oBe.perc_sfecha_registro,

                    oBe.tbpd_icod_tip_doc,
                    oBe.perc_vnum_doc,
                    oBe.tbpd_icod_pais_emi_doc,
                    oBe.perc_sfecha_nacimiento,
                    oBe.perc_vapellido_pat,
                    oBe.perc_vapellido_mat,
                    oBe.perc_vnombres,
                    oBe.perc_icod_sexo,
                    oBe.tbpd_icod_nacionalidad,
                    oBe.tbpd_icod_telf_ldn,
                    oBe.perc_vnum_telf_ldn,
                    oBe.perc_vcorreo,
                    oBe.tbpd_icod_tip_via,
                    oBe.perc_vnomb_via,
                    oBe.perc_vnum_via,
                    oBe.perc_vdepartamento,
                    oBe.perc_vinterior,
                    oBe.perc_vmanzana,
                    oBe.perc_vlote,
                    oBe.perc_vkilometro,
                    oBe.perc_vblock,
                    oBe.perc_vetapa,
                    oBe.tbpd_icod_tip_zona,
                    oBe.perc_vnomb_zona,
                    oBe.perc_vreferencia,
                    oBe.tbpd_icod_ubi_geo,
                    oBe.tarec_icod_est_civil,

                    oBe.tablc_iid_tipo_cargo,
                    oBe.tablc_iid_tipo_area,
                    oBe.perc_vruc,
                    oBe.perc_vnum_seguro,
                    oBe.perc_icod_tip_fdo_pension,
                    oBe.perc_sfech_inicio,
                    oBe.perc_icod_afp,
                    oBe.perc_icod_tip_comision,
                    oBe.perc_vcuspp,
                    oBe.perc_beps,
                    oBe.perc_icod_tip_personal,
                    oBe.perc_nmont_basico,
                    oBe.tarec_icod_moneda,
                    oBe.perc_brta_5ta,
                    oBe.perc_nmont_ant_afecto,
                    oBe.perc_nmont_retenido,
                    oBe.perc_basig_familiar,
                    oBe.perc_sfech_cese,

                    oBe.perc_iid_situacion_perso,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.perc_hora_inical_LV,
                    oBe.perc_hora_final_LV,
                    oBe.perc_hora_inical_S,
                    oBe.perc_hora_final_S,
                    oBe.perc_hora_total_LV,
                    oBe.perc_hora_total_S,
                    oBe.perc_icod_tip_contrato,
                    oBe.perc_icod_banc_haber,
                    oBe.perc_vbanc_haber,
                    oBe.perc_icod_banc_cts,
                    oBe.perc_vbanc_cts,
                    oBe.perc_sfecha_cese,
                    oBe.perc_icod_motiv_cese,
                    oBe.perc_retenc_judicial,
                    oBe.anac_icod_analitica,
                    oBe.perc_nasig_transporte
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPersonal(EPersonal oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_PERSONAL_ELIMINAR(
                        oBe.perc_icod_personal,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EPersonalCCostos> listarCCostos()
        {
            List<EPersonalCCostos> lista = new List<EPersonalCCostos>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EPersonalCCostos>();
                    var query = dc.SGEP_CCOSTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPersonalCCostos()
                        {
                            pccd_icod_ccosto = Convert.ToInt32(item.pccd_icod_ccosto),
                            perc_icod_personal = Convert.ToInt32(item.perc_icod_personal),
                            perc_vnum_doc = item.perc_vnum_doc,
                            pccd_sfecha = Convert.ToDateTime(item.pccd_sfecha),
                            ccoc_numero_centro_costo = item.ccoc_numero_centro_costo,
                            ccoc_vdescripcion_ccosto = item.ccoc_vdescripcion_ccosto,
                            pccd_flag_estado = item.pccd_flag_estado,
                            pccd_imes = Convert.ToInt32(item.pccd_imes),
                            pccd_iaño = Convert.ToInt32(item.pccd_iaño),
                            strMes = item.strMes,
                            pccd_nmonto_vacaciones = Convert.ToDecimal(item.pccd_nmonto_vacaciones),
                            pccd_nmonto_gratificaciones = Convert.ToDecimal(item.pccd_nmonto_gratificaciones),
                            pccd_nmonto_cts = Convert.ToDecimal(item.pccd_nmonto_cts),
                            cecoc_icod_centro_costo = Convert.ToInt32(item.cecoc_icod_centro_costo)

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

        public List<EPersonalCCostos> listarPersonalCCostos(int cod)
        {
            List<EPersonalCCostos> lista = new List<EPersonalCCostos>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EPersonalCCostos>();
                    var query = dc.SGEP_PERSONAL_CCOSTOS_LISTAR(cod);
                    foreach (var item in query)
                    {
                        lista.Add(new EPersonalCCostos()
                        {
                            pccd_icod_ccosto = Convert.ToInt32(item.pccd_icod_ccosto),
                            perc_icod_personal = Convert.ToInt32(item.perc_icod_personal),
                            perc_vnum_doc = item.perc_vnum_doc,
                            pccd_sfecha = Convert.ToDateTime(item.pccd_sfecha),
                            ccoc_numero_centro_costo = item.ccoc_numero_centro_costo,
                            ccoc_vdescripcion_ccosto = item.ccoc_vdescripcion_ccosto,
                            pccd_flag_estado = item.pccd_flag_estado,
                            pccd_imes = Convert.ToInt32(item.pccd_imes),
                            pccd_iaño = Convert.ToInt32(item.pccd_iaño),
                            strMes = item.strMes,
                            pccd_nmonto_vacaciones = Convert.ToDecimal(item.pccd_nmonto_vacaciones),
                            pccd_nmonto_gratificaciones = Convert.ToDecimal(item.pccd_nmonto_gratificaciones),
                            pccd_nmonto_cts = Convert.ToDecimal(item.pccd_nmonto_cts)
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
        public int insertarPersonalCCostos(EPersonalCCostos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGEP_PERSONAL_CCOSTOS_INSERTAR(
                    ref intIcod,
                    oBe.perc_icod_personal,
                    oBe.perc_vnum_doc,
                    oBe.pccd_sfecha,
                    oBe.ccoc_numero_centro_costo,
                    oBe.ccoc_vdescripcion_ccosto,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.pccd_flag_estado,
                    oBe.pccd_imes,
                    oBe.pccd_iaño,
                    oBe.pccd_nmonto_vacaciones,
                    oBe.pccd_nmonto_gratificaciones,
                    oBe.pccd_nmonto_cts
                    );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPersonalCCostos(EPersonalCCostos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_PERSONAL_CCOSTOS_MODIFICAR(
                    oBe.pccd_icod_ccosto,
                    oBe.perc_icod_personal,
                    oBe.perc_vnum_doc,
                    oBe.pccd_sfecha,
                    oBe.ccoc_numero_centro_costo,
                    oBe.ccoc_vdescripcion_ccosto,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.pccd_imes,
                    oBe.pccd_iaño,
                    oBe.pccd_nmonto_vacaciones,
                    oBe.pccd_nmonto_gratificaciones,
                    oBe.pccd_nmonto_cts
                    );
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPersonalCCostos(EPersonalCCostos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_PERSONAL_CCOSTOS_ELIMINAR(
                        oBe.pccd_icod_ccosto,
                        oBe.intUsuario,
                        oBe.strPc);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EPersonalCCostos> listarPersonalControlCCostos()
        {
            List<EPersonalCCostos> lista = new List<EPersonalCCostos>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EPersonalCCostos>();
                    var query = dc.SGEP_PERSONAL_CONTROL_CCOSTOS_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EPersonalCCostos()
                        {
                            pccd_icod_ccosto = Convert.ToInt32(item.pccd_icod_ccosto),
                            perc_icod_personal = Convert.ToInt32(item.perc_icod_personal),
                            perc_vnum_doc = item.perc_vnum_doc,
                            pccd_sfecha = Convert.ToDateTime(item.pccd_sfecha),
                            ccoc_numero_centro_costo = item.ccoc_numero_centro_costo,
                            ccoc_vdescripcion_ccosto = item.ccoc_vdescripcion_ccosto,
                            pccd_flag_estado = item.pccd_flag_estado,
                            pccd_imes = Convert.ToInt32(item.pccd_imes),
                            pccd_iaño = Convert.ToInt32(item.pccd_iaño),
                            strMes = item.strMes

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

        #region ConceptosIngresos
        public List<EConceptosIngresos> listarIngresos()
        {
            List<EConceptosIngresos> lista = new List<EConceptosIngresos>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EConceptosIngresos>();
                    var query = dc.SGEP_INGRESO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptosIngresos()
                        {
                            cipc_icod_concepto_ingreso_plan = item.cipc_icod_concepto_ingreso_plan,
                            tbpc_icod_tipo_planilla = Convert.ToInt32(item.tbpc_icod_tipo_planilla),
                            tbpc_icod_situacion_concepto_plan = Convert.ToInt32(item.tbpc_icod_situacion_concepto_plan),
                            cipc_iid_concepto_ing_plan = item.cipc_iid_concepto_ing_plan,
                            cipc_vdescripcion = item.cipc_vdescripcion,
                            cipc_bafecto_renta_quinta = item.cipc_bafecto_renta_quinta,
                            tbpc_icod_tipo_concepto_planilla = Convert.ToInt32(item.tbpc_icod_tipo_concepto_planilla),
                            tbpc_icod_tipo_accion_planilla = Convert.ToInt32(item.tbpc_icod_tipo_accion_planilla),
                            cipc_nmonto_calculo_planilla = Convert.ToDecimal(item.cipc_nmonto_calculo_planilla),
                            cipc_nmonto_porcentaje_planilla = Convert.ToDecimal(item.cipc_nmonto_porcentaje_planilla),
                            tbpc_icod_tipo_calculo_planilla = Convert.ToInt32(item.tbpc_icod_tipo_calculo_planilla),
                            ctcc_icod_cuenta_contable_debe = Convert.ToInt32(item.ctcc_icod_cuenta_contable_debe),
                            ctcc_icod_cuenta_contable_haber = Convert.ToInt32(item.ctcc_icod_cuenta_contable_haber),
                            cipc_flag_estado = item.cipc_flag_estado,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_numero_cuenta_contable_haber = item.ctacc_numero_cuenta_contable_haber,
                            StrTipoPlanilla = item.TipoPlanilla,
                            StrSituacion = item.Situacion,
                            StrTipo = item.Tipo,
                            StrAccion = item.Accion,
                            StrTipoCalculo = item.TipoCalculo

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
        public int insertarIngresos(EConceptosIngresos oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGEP_INGRESO_INSERTAR(
                    ref intIcod,
                    oBe.tbpc_icod_tipo_planilla,
                    oBe.tbpc_icod_situacion_concepto_plan,
                    oBe.cipc_iid_concepto_ing_plan,
                    oBe.cipc_vdescripcion,
                    oBe.cipc_bafecto_renta_quinta,
                    oBe.tbpc_icod_tipo_concepto_planilla,
                    oBe.tbpc_icod_tipo_accion_planilla,
                    oBe.cipc_nmonto_calculo_planilla,
                    oBe.cipc_nmonto_porcentaje_planilla,
                    oBe.tbpc_icod_tipo_calculo_planilla,
                    oBe.ctcc_icod_cuenta_contable_debe,
                    oBe.ctcc_icod_cuenta_contable_haber,
                    oBe.cipc_flag_estado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ctacc_numero_cuenta_contable,
                    oBe.ctacc_numero_cuenta_contable_haber);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarIngreso(EConceptosIngresos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INGRESO_MODIFICAR(
                    oBe.cipc_icod_concepto_ingreso_plan,
                    oBe.tbpc_icod_tipo_planilla,
                    oBe.tbpc_icod_situacion_concepto_plan,
                    oBe.cipc_vdescripcion,
                    oBe.cipc_bafecto_renta_quinta,
                    oBe.tbpc_icod_tipo_concepto_planilla,
                    oBe.tbpc_icod_tipo_accion_planilla,
                    oBe.cipc_nmonto_calculo_planilla,
                    oBe.cipc_nmonto_porcentaje_planilla,
                    oBe.tbpc_icod_tipo_calculo_planilla,
                    oBe.ctcc_icod_cuenta_contable_debe,
                    oBe.ctcc_icod_cuenta_contable_haber,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ctacc_numero_cuenta_contable,
                    oBe.ctacc_numero_cuenta_contable_haber);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarIngreso(EConceptosIngresos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INGRESO_ELIMINAR(
                        oBe.cipc_icod_concepto_ingreso_plan,
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

        #region ConceptosDescuentos
        public List<EConceptoDescuento> listarConceptoDescuento()
        {
            List<EConceptoDescuento> lista = new List<EConceptoDescuento>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EConceptoDescuento>();
                    var query = dc.SGEP_CONCEPTO_DESCUENTO_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptoDescuento()
                        {
                            cdpc_icod_concepto_ingreso_plan = item.cdpc_icod_concepto_descuento_plan,
                            tbpc_icod_tipo_planilla = Convert.ToInt32(item.tbpc_icod_tipo_planilla),
                            tbpc_icod_situacion_concepto_plan = Convert.ToInt32(item.tbpc_icod_situacion_concepto_plan),
                            cdpc_iid_concepto_ing_plan = item.cdpc_iid_concepto_desc_plan,
                            cdpc_vdescripcion = item.cdpc_vdescripcion,
                            tbpc_icod_tipo_accion_planilla = Convert.ToInt32(item.tbpc_icod_tipo_accion_planilla),
                            cdpc_nmonto_calculo_planilla = Convert.ToDecimal(item.cdpc_nmonto_calculo_planilla),
                            cdpc_nmonto_porcentaje_planilla = Convert.ToDecimal(item.cdpc_nmonto_porcentaje_planilla),
                            tbpc_icod_tipo_calculo_planilla = Convert.ToInt32(item.tbpc_icod_tipo_calculo_planilla),
                            ctcc_icod_cuenta_contable_debe = Convert.ToInt32(item.ctcc_icod_cuenta_contable_debe),
                            ctcc_icod_cuenta_contable_haber = Convert.ToInt32(item.ctcc_icod_cuenta_contable_haber),
                            cipc_flag_estado = item.cdpc_flag_estado,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_numero_cuenta_contable_haber = item.ctacc_numero_cuenta_contable_haber,
                            StrTipoPlanilla = item.TipoPlanilla,
                            StrSituacion = item.Situacion,
                            StrAccion = item.Accion,
                            StrTipoCalculo = item.TipoCalculo

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
        public int insertarConceptoDescuento(EConceptoDescuento oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGEP_CONCEPTO_DESCUENTO_INSERTAR(
                    ref intIcod,
                    oBe.tbpc_icod_tipo_planilla,
                    oBe.tbpc_icod_situacion_concepto_plan,
                    oBe.cdpc_iid_concepto_ing_plan,
                    oBe.cdpc_vdescripcion,
                    oBe.tbpc_icod_tipo_accion_planilla,
                    oBe.cdpc_nmonto_calculo_planilla,
                    oBe.cdpc_nmonto_porcentaje_planilla,
                    oBe.tbpc_icod_tipo_calculo_planilla,
                    oBe.ctcc_icod_cuenta_contable_debe,
                    oBe.ctcc_icod_cuenta_contable_haber,
                    oBe.cipc_flag_estado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ctacc_numero_cuenta_contable,
                    oBe.ctacc_numero_cuenta_contable_haber);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarConceptoDescuento(EConceptoDescuento oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CONCEPTO_DESCUENTO_MODIFICAR(
                    oBe.cdpc_icod_concepto_ingreso_plan,
                    oBe.tbpc_icod_tipo_planilla,
                    oBe.tbpc_icod_situacion_concepto_plan,
                    oBe.cdpc_vdescripcion,
                    oBe.tbpc_icod_tipo_accion_planilla,
                    oBe.cdpc_nmonto_calculo_planilla,
                    oBe.cdpc_nmonto_porcentaje_planilla,
                    oBe.tbpc_icod_tipo_calculo_planilla,
                    oBe.ctcc_icod_cuenta_contable_debe,
                    oBe.ctcc_icod_cuenta_contable_haber,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ctacc_numero_cuenta_contable,
                    oBe.ctacc_numero_cuenta_contable_haber);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarConceptoDescuento(EConceptoDescuento oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CONCEPTO_DESCUENTO_ELIMINAR(
                        oBe.cdpc_icod_concepto_ingreso_plan,
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

        #region ConceptosAportaciones
        public List<EConceptoAportacion> listarConceptoAportacion()
        {
            List<EConceptoAportacion> lista = new List<EConceptoAportacion>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EConceptoAportacion>();
                    var query = dc.SGEP_CONCEPTO_APORTACION_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EConceptoAportacion()
                        {
                            capc_icod_concepto_aportaciones_plan = item.capc_icod_concepto_aportaciones_plan,
                            tbpc_icod_tipo_planilla = Convert.ToInt32(item.tbpc_icod_tipo_planilla),
                            tbpc_icod_situacion_concepto_plan = Convert.ToInt32(item.tbpc_icod_situacion_concepto_plan),
                            capc_iid_concepto_apor_plan = item.capc_iid_concepto_apor_plan,
                            capc_vdescripcion = item.capc_vdescripcion,
                            tbpc_icod_tipo_accion_planilla = Convert.ToInt32(item.tbpc_icod_tipo_accion_planilla),
                            capc_nmonto_calculo_planilla = Convert.ToDecimal(item.capc_nmonto_calculo_planilla),
                            capc_nmonto_porcentaje_planilla = Convert.ToDecimal(item.capc_nmonto_porcentaje_planilla),
                            tbpc_icod_tipo_calculo_planilla = Convert.ToInt32(item.tbpc_icod_tipo_calculo_planilla),
                            ctcc_icod_cuenta_contable_debe = Convert.ToInt32(item.ctcc_icod_cuenta_contable_debe),
                            ctcc_icod_cuenta_contable_haber = Convert.ToInt32(item.ctcc_icod_cuenta_contable_haber),
                            capc_flag_estado = item.capc_flag_estado,
                            ctacc_numero_cuenta_contable = item.ctacc_numero_cuenta_contable,
                            ctacc_numero_cuenta_contable_haber = item.ctacc_numero_cuenta_contable_haber,
                            StrTipoPlanilla = item.TipoPlanilla,
                            StrSituacion = item.Situacion,
                            StrAccion = item.Accion,
                            StrTipoCalculo = item.TipoCalculo

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

        public int insertarConceptoAportacion(EConceptoAportacion oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGEP_CONCEPTO_APORTACION_INSERTAR(
                    ref intIcod,
                    oBe.tbpc_icod_tipo_planilla,
                    oBe.tbpc_icod_situacion_concepto_plan,
                    oBe.capc_iid_concepto_apor_plan,
                    oBe.capc_vdescripcion,
                    oBe.tbpc_icod_tipo_accion_planilla,
                    oBe.capc_nmonto_calculo_planilla,
                    oBe.capc_nmonto_porcentaje_planilla,
                    oBe.tbpc_icod_tipo_calculo_planilla,
                    oBe.ctcc_icod_cuenta_contable_debe,
                    oBe.ctcc_icod_cuenta_contable_haber,
                    oBe.capc_flag_estado,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ctacc_numero_cuenta_contable,
                    oBe.ctacc_numero_cuenta_contable_haber);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void modificarConceptoAportacion(EConceptoAportacion oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CONCEPTO_APORTACION_MODIFICAR(
                    oBe.capc_icod_concepto_aportaciones_plan,
                    oBe.tbpc_icod_tipo_planilla,
                    oBe.tbpc_icod_situacion_concepto_plan,
                    oBe.capc_vdescripcion,
                    oBe.tbpc_icod_tipo_accion_planilla,
                    oBe.capc_nmonto_calculo_planilla,
                    oBe.capc_nmonto_porcentaje_planilla,
                    oBe.tbpc_icod_tipo_calculo_planilla,
                    oBe.ctcc_icod_cuenta_contable_debe,
                    oBe.ctcc_icod_cuenta_contable_haber,
                    oBe.intUsuario,
                    oBe.strPc,
                    oBe.ctacc_numero_cuenta_contable,
                    oBe.ctacc_numero_cuenta_contable_haber);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void eliminarConceptoAportacion(EConceptoAportacion oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_CONCEPTO_APORTACION_ELIMINAR(
                        oBe.capc_icod_concepto_aportaciones_plan,
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

        #region ListarCombos

        public List<ECargo> listarComboCargo()
        {
            List<ECargo> lista = new List<ECargo>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_LISTAR_COMBO_CARGO();
                    foreach (var item in query)
                    {
                        lista.Add(new ECargo()
                        {
                            carg_icod_cargo = item.carg_icod_cargo,
                            carg_vdescripcion = item.carg_vdescripcion,
                            carg_vabreviado = item.carg_vabreviado

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

        public List<EAreas> listarComboArea()
        {
            List<EAreas> lista = new List<EAreas>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_LISTAR_COMBO_AREA();
                    foreach (var item in query)
                    {
                        lista.Add(new EAreas()
                        {
                            arec_icod_cargo = item.arec_icod_cargo,
                            arec_vdescripcion = item.arec_vdescripcion,
                            arec_vabreviado = item.arec_vabreviado

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

        public List<ETablaPlanillaDetalle> listarComboTablaPlanillaDetalle(int num)
        {
            List<ETablaPlanillaDetalle> lista = new List<ETablaPlanillaDetalle>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    var query = dc.SGEP_LISTAR_COMBO_TABLA_PLANILLA_DETALLE(num);
                    foreach (var item in query)
                    {
                        lista.Add(new ETablaPlanillaDetalle()
                        {
                            tbpc_icod_tabla_planilla = Convert.ToInt32(item.tbpc_icod_tabla_planilla),
                            tbpd_icod_tabla_planilla_detalle = item.tbpd_icod_tabla_planilla_detalle,
                            tbpd_vdescripcion_detalle = item.tbpd_vdescripcion_detalle

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

        #region personal datos de contratacion
        public List<EPersonal> listarPersonal_contratacion(int id_personal)
        {
            List<EPersonal> lista = new List<EPersonal>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EPersonal>();
                    var query = dc.SGE_PERSONAL_CONTRATACIONES_LISTAR(id_personal);
                    foreach (var item in query)
                    {
                        lista.Add(new EPersonal()
                        {
                            pctd_icod_persona_contratacion = item.pctd_icod_persona_contratacion,
                            pctd_sfecha_ini_contrato = item.pctd_sfecha_ini_contrato,
                            pctd_sfecha_fin_contrato = item.pctd_sfecha_fin_contrato,
                            pctd_sfecha_ini_actividad = item.pctd_sfecha_ini_actividad,
                            pctd_sfecha_fin_actividad = item.pctd_sfecha_fin_actividad,
                            pctd_sfecha_cese = item.pctd_sfecha_cese,
                            tbpd_icod_motivo_cese = item.tbpd_icod_motivo_cese,
                            strMotivo = item.strMotivo

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

        public int insertarPersonal_contratacion(EPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGE_PERSONAL_CONTRACION_INSERTAR(
                    oBe.perc_icod_personal,
                    oBe.pctd_sfecha_ini_contrato,
                    oBe.pctd_sfecha_fin_contrato,
                    oBe.pctd_sfecha_ini_actividad,
                    oBe.pctd_sfecha_fin_actividad,
                    oBe.pctd_sfecha_cese,
                    oBe.tbpd_icod_motivo_cese,
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


        public int modificarPersonal_contratacion(EPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGE_PERSONAL_CONTRACION_MODIFICAR(
                    oBe.pctd_icod_persona_contratacion,
                    oBe.perc_icod_personal,
                    oBe.pctd_sfecha_ini_contrato,
                    oBe.pctd_sfecha_fin_contrato,
                    oBe.pctd_sfecha_ini_actividad,
                    oBe.pctd_sfecha_fin_actividad,
                    oBe.pctd_sfecha_cese,
                    oBe.tbpd_icod_motivo_cese,
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

        public int eliminarPersonal_contratacion(EPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGE_PERSONAL_CONTRATACION_ELIMINAR(
                    oBe.pctd_icod_persona_contratacion,
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
        #endregion

        #region Archivos
        public List<EArchivos> listarArchivos(int codigo)
        {
            List<EArchivos> lista = new List<EArchivos>();

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    lista = new List<EArchivos>();
                    var query = dc.SGE_ARCHIVOS_PERSONAL_LISTAR(codigo);
                    foreach (var item in query)
                    {
                        lista.Add(new EArchivos()
                        {
                            arch_icod_archivos = Convert.ToInt32(item.arch_icod_archivos),
                            arch_iid_correlativo = Convert.ToInt32(item.arch_iid_correlativo),
                            arch_iid_orden_compra_local = Convert.ToInt32(item.arch_iid_personal),
                            arch_vdescripcion = item.arch_vdescripcion,
                            arch_vruta = item.arch_vruta,
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
        public int insertarArchivos(EArchivos oBe)
        {
            int? intIcod = 0;

            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    dc.SGE_ARCHIVOS_PERSONAL_INSERTAR(
                    ref intIcod,
                    oBe.arch_iid_correlativo,
                    oBe.arch_iid_personal,
                    oBe.arch_vdescripcion,
                    oBe.arch_vruta,
                    oBe.intUsuario,
                    oBe.strPc,
                    true);
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarArchivos(EArchivos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_ARCHIVOS_PERSONAL_MODIFICAR(
                    oBe.arch_icod_archivos,
                    oBe.arch_iid_correlativo,
                    oBe.arch_iid_personal,
                    oBe.arch_vdescripcion,
                    oBe.arch_vruta,
                    oBe.intUsuario,
                    oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarArchivos(EArchivos oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_ARCHIVOS_PERSONAL_ELIMINAR(
                        oBe.arch_icod_archivos,
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

        #region Plantilla Personal
        public List<EPlanillaPersonal> listarPlanillaPersonal()
        {
            List<EPlanillaPersonal> lista = new List<EPlanillaPersonal>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PLANILLA_PERSONAL_LISTAR();
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
        public int insertarPlanillaPersonal(EPlanillaPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_PERSONAL_INSERTAR(
                        ref intIcod,
                        oBe.planc_iid_planilla_personal,
                        oBe.mesec_iid_mes,
                        oBe.planc_iid_anio,
                        oBe.planc_vdescripcion,
                        oBe.tablc_iid_situacion_planilla,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.planc_iid_tipo_planilla,
                        oBe.planc_sfecha
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaPersonal(EPlanillaPersonal oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_PERSONAL_MODIFICAR(
                        oBe.planc_icod_planilla_personal,
                        oBe.planc_iid_planilla_personal,
                        oBe.mesec_iid_mes,
                        oBe.planc_iid_anio,
                        oBe.planc_vdescripcion,
                        oBe.tablc_iid_situacion_planilla,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.planc_sfecha);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlanillaPersonal(EPlanillaPersonal oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_PERSONAL_ELIMINAR(
                        oBe.planc_icod_planilla_personal,
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

        #region Plantilla Personal Detalle
        public List<EPlanillaPersonalDetalle> listarPlanillaPersonalDetalle(int planc_icod_planilla_personal)
        {
            List<EPlanillaPersonalDetalle> lista = new List<EPlanillaPersonalDetalle>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PLANILLA_PERSONAL_DETALLE_LISTAR(planc_icod_planilla_personal);
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
                            pland_npago_utilidad_convencional = item.pland_npago_utilidad_convencional,
                            vccn_nvacaciones = item.vccn_nvacaciones,
                            vccn_nfondo = item.vccn_nfondo,
                            vccn_ncomision = item.vccn_ncomision,
                            vccn_nseguro = item.vccn_nseguro,
                            vccn_ntotal_afp = item.vccn_ntotal_afp,
                            vccn_nopn = item.vccn_nopn,
                            vccn_nrenta5 = item.vccn_nrenta5,
                            vccn_notros_desc = item.vccn_notros_desc,
                            vccn_nessalud = item.vccn_nessalud,
                            vccn_nvacaciones_neto = item.vccn_nvacaciones_neto,
                            rmcn_remun_computable = item.rmcn_remun_computable,
                            rmcn_fondo = item.rmcn_fondo,
                            rmcn_comision = item.rmcn_comision,
                            rmcn_seguro = item.rmcn_seguro,
                            rmcn_total_afp = item.rmcn_total_afp,
                            rmcn_onp = item.rmcn_onp,
                            rmcn_rta_5ta = item.rmcn_rta_5ta,
                            rmcn_otros_dstos = item.rmcn_otros_dstos,
                            rmcn_reten_judicial = item.rmcn_reten_judicial,
                            rmcn_essalud = item.rmcn_essalud,
                            rmcn_remun_neto = item.rmcn_remun_neto,
                            pland_desc_aporte_c_prov = item.pland_desc_aporte_c_prov,
                            pland_desc_aporte_s_prov = item.pland_desc_aporte_s_prov,
                            rmcn_aporte_c_prov = item.rmcn_aporte_c_prov,
                            rmcn_aporte_s_prov = item.rmcn_aporte_s_prov
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
        public int insertarPlanillaPersonalDetalle(EPlanillaPersonalDetalle oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_PERSONAL_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.pland_iid_planilla_personal_det,
                        oBe.planc_icod_planilla_personal,
                        oBe.pland_ape_nom,
                        oBe.pland_num_doc,
                        oBe.pland_cussp,
                        oBe.pland_sueldo_basico,
                        oBe.pland_rem_basica,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pland_flag_estado,
                        oBe.pland_icod_personal,
                        oBe.pland_sfecha_incio,
                        oBe.pland_sfecha_cese,
                        oBe.pland_nasignacion_familiar,
                        oBe.pland_nrem_computable,
                        //****Remuneraciones***//
                        oBe.pland_reg_pension,
                        oBe.pland_comision,
                        oBe.pland_cargo,
                        oBe.pland_hijo,
                        oBe.pland_dias,
                        oBe.pland_faltas,
                        oBe.pland_vacaciones,
                        oBe.pland_descanso_medico,
                        oBe.pland_dias_subsidios,
                        oBe.pland_dias_efectivos,
                        oBe.pland_nmonto_vacaciones,
                        oBe.pland_nhoras_25,
                        oBe.pland_nhoras_35,
                        oBe.pland_nferiado_descanso,
                        oBe.pland_notros_ingresos,
                        oBe.pland_nsubsidios_essalud,
                        oBe.pland_ncomision_venta,
                        oBe.pland_ncomision_eventual,
                        oBe.pland_nasignacion_transporte,
                        oBe.pland_nvales_alimentos,
                        oBe.pland_nadelanto_sueldo,
                        oBe.pland_ngratif_afecto,
                        oBe.pland_nbonif_afecto,
                        oBe.pland_nvacaciones_truncas,
                        oBe.pland_ngratif_no_afecto,
                        oBe.pland_nbonif_no_afecto,
                        oBe.pland_nCTS,
                        oBe.pland_nutilidades,
                        oBe.pland_nremun_bruta,
                        oBe.pland_nremun_computable_neta,
                        oBe.pland_ninasistencias,
                        oBe.pland_ntardanzas,
                        oBe.pland_npago_utilid,
                        oBe.pland_desc_onp,
                        oBe.pland_desc_fondo,
                        oBe.pland_desc_comision,
                        oBe.pland_desc_seguro,
                        oBe.pland_desc_tot_afp,
                        oBe.pland_desc_renta5,
                        oBe.pland_desc_adelanto,
                        oBe.pland_desc_prestamo,
                        oBe.pland_desc_eps,
                        oBe.pland_desc_otros_desc_no_afect,
                        oBe.pland_desc_retenc_judicial,
                        oBe.pland_desc_otros_desc_afect,
                        oBe.pland_desc_total_desc,
                        oBe.pland_aport_essalud9,
                        oBe.pland_aport_eps_pacifico,
                        oBe.pland_aport_essalud,
                        oBe.pland_total_neto_pagar,
                        oBe.pland_icod_fondo_pension,
                        oBe.pland_nutilidad_convencional,
                        oBe.pland_npago_utilidad_convencional,
                        oBe.vccn_nvacaciones,
                        oBe.vccn_nfondo,
                        oBe.vccn_ncomision,
                        oBe.vccn_nseguro,
                        oBe.vccn_ntotal_afp,
                        oBe.vccn_nopn,
                        oBe.vccn_nrenta5,
                        oBe.vccn_notros_desc,
                        oBe.vccn_nessalud,
                        oBe.vccn_nvacaciones_neto,
                        oBe.rmcn_remun_computable,
                        oBe.rmcn_fondo,
                        oBe.rmcn_comision,
                        oBe.rmcn_seguro,
                        oBe.rmcn_total_afp,
                        oBe.rmcn_onp,
                        oBe.rmcn_rta_5ta,
                        oBe.rmcn_otros_dstos,
                        oBe.rmcn_reten_judicial,
                        oBe.rmcn_essalud,
                        oBe.rmcn_remun_neto,
                        oBe.pland_desc_aporte_c_prov,
                        oBe.pland_desc_aporte_s_prov,
                        oBe.rmcn_aporte_c_prov,
                        oBe.rmcn_aporte_s_prov

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaPersonalDetalle(EPlanillaPersonalDetalle oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_PERSONAL_DETALLE_MODIFICAR(
                        oBe.pland_icod_planilla_personal_det,
                        oBe.pland_ape_nom,
                        oBe.pland_num_doc,
                        oBe.pland_cussp,
                        oBe.pland_sueldo_basico,
                        oBe.pland_rem_basica,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pland_nrem_computable,
//****CTS***//
oBe.pland_reg_pension,
oBe.pland_comision,
oBe.pland_cargo,
oBe.pland_hijo,
oBe.pland_dias,
oBe.pland_faltas,
oBe.pland_vacaciones,
oBe.pland_descanso_medico,
oBe.pland_dias_subsidios,
oBe.pland_dias_efectivos,
oBe.pland_nmonto_vacaciones,
oBe.pland_nhoras_25,
oBe.pland_nhoras_35,
oBe.pland_nferiado_descanso,
oBe.pland_notros_ingresos,
oBe.pland_nsubsidios_essalud,
oBe.pland_ncomision_venta,
oBe.pland_ncomision_eventual,
oBe.pland_nasignacion_transporte,
oBe.pland_nvales_alimentos,
oBe.pland_nadelanto_sueldo,
oBe.pland_ngratif_afecto,
oBe.pland_nbonif_afecto,
oBe.pland_nvacaciones_truncas,
oBe.pland_ngratif_no_afecto,
oBe.pland_nbonif_no_afecto,
oBe.pland_nCTS,
oBe.pland_nutilidades,
oBe.pland_nremun_bruta,
oBe.pland_nremun_computable_neta,
oBe.pland_ninasistencias,
oBe.pland_ntardanzas,
oBe.pland_npago_utilid,
oBe.pland_desc_onp,
oBe.pland_desc_fondo,
oBe.pland_desc_comision,
oBe.pland_desc_seguro,
oBe.pland_desc_tot_afp,
oBe.pland_desc_renta5,
oBe.pland_desc_adelanto,
oBe.pland_desc_prestamo,
oBe.pland_desc_eps,
oBe.pland_desc_otros_desc_no_afect,
oBe.pland_desc_retenc_judicial,
oBe.pland_desc_otros_desc_afect,
oBe.pland_desc_total_desc,
oBe.pland_aport_essalud9,
oBe.pland_aport_eps_pacifico,
oBe.pland_aport_essalud,
oBe.pland_total_neto_pagar,
oBe.pland_icod_fondo_pension,
oBe.pland_nutilidad_convencional,
oBe.pland_npago_utilidad_convencional,
oBe.pland_nasignacion_familiar,
oBe.vccn_nvacaciones,
oBe.vccn_nfondo,
oBe.vccn_ncomision,
oBe.vccn_nseguro,
oBe.vccn_ntotal_afp,
oBe.vccn_nopn,
oBe.vccn_nrenta5,
oBe.vccn_notros_desc,
oBe.vccn_nessalud,
oBe.vccn_nvacaciones_neto,
oBe.rmcn_remun_computable,
oBe.rmcn_fondo,
oBe.rmcn_comision,
oBe.rmcn_seguro,
oBe.rmcn_total_afp,
oBe.rmcn_onp,
oBe.rmcn_rta_5ta,
oBe.rmcn_otros_dstos,
oBe.rmcn_reten_judicial,
oBe.rmcn_essalud,
oBe.rmcn_remun_neto,
oBe.pland_desc_aporte_c_prov,
oBe.pland_desc_aporte_s_prov,
oBe.rmcn_aporte_c_prov,
oBe.rmcn_aporte_s_prov

                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarPlanillaPersonalDetalle(EPlanillaPersonalDetalle oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PLANILLA_PERSONAL_DETALLE_ELIMINAR(
                        oBe.pland_icod_planilla_personal_det,
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

        #region Provision Plantilla Personal
        public List<EProvisionPlanillaPersonal> listarProvisionPlanillaPersonal()
        {
            List<EProvisionPlanillaPersonal> lista = new List<EProvisionPlanillaPersonal>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PROVISION_PLANILLA_PERSONAL_LISTAR();
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
        public int insertarProvisionPlanillaPersonal(EProvisionPlanillaPersonal oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVISION_PLANILLA_PERSONAL_INSERTAR(
                        ref intIcod,
                        oBe.planc_iid_planilla_personal,
                        oBe.mesec_iid_mes,
                        oBe.planc_iid_anio,
                        oBe.planc_vdescripcion,
                        oBe.tablc_iid_situacion_planilla,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.planc_iid_tipo_planilla,
                        oBe.planc_sfecha
                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProvisionPlanillaPersonal(EProvisionPlanillaPersonal oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVISION_PLANILLA_PERSONAL_MODIFICAR(
                        oBe.planc_icod_planilla_personal,
                        oBe.planc_iid_planilla_personal,
                        oBe.mesec_iid_mes,
                        oBe.planc_iid_anio,
                        oBe.planc_vdescripcion,
                        oBe.tablc_iid_situacion_planilla,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.planc_sfecha);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProvisionPlanillaPersonal(EProvisionPlanillaPersonal oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVISION_PLANILLA_PERSONAL_ELIMINAR(
                        oBe.planc_icod_planilla_personal,
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

        #region Provision Plantilla Personal Detalle
        public List<EProvisionPlanillaPersonalDetalle> listarProvisionPlanillaPersonalDetalle(int planc_icod_planilla_personal)
        {
            List<EProvisionPlanillaPersonalDetalle> lista = new List<EProvisionPlanillaPersonalDetalle>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PROVISION_PLANILLA_PERSONAL_DETALLE_LISTAR(planc_icod_planilla_personal);
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
                            //****VACACIONES***//
                            pland_vac_dias_Prov_mensual = item.pland_vac_dias_Prov_mensual,
                            pland_vac_dias_acumulado = item.pland_vac_dias_acumulado,
                            pland_vac_provision_mes = item.pland_vac_provision_mes,
                            pland_vac_ajuste_mes = item.pland_vac_ajuste_mes,
                            pland_vac_prov_tot_mensual = item.pland_vac_prov_tot_mensual,
                            strCargo = item.strCargo

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
        public int insertarProvisionPlanillaPersonalDetalle(EProvisionPlanillaPersonalDetalle oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVISION_PLANILLA_PERSONAL_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.pland_iid_planilla_personal_det,
                        oBe.planc_icod_planilla_personal,
                        oBe.pland_ape_nom,
                        oBe.pland_num_doc,
                        oBe.pland_cussp,
                        oBe.pland_rem_basica,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pland_flag_estado,
                        oBe.pland_icod_personal,
                        oBe.pland_beps,
                        oBe.pland_sfecha_incio,
                        oBe.pland_sfecha_cese,
                        oBe.pland_basignacion_familiar,
                        oBe.prpc_nasignacion_familiar,
                        oBe.prpc_ngratificacion_essalud,
                        oBe.prpc_ngratificacion_eps,
                        oBe.pland_nasignacion_familiar,
                        oBe.pland_nrem_computable,
                        oBe.pland_nmonto_gratificacion,
                        oBe.pland_nbonificacion_mes,
                        //****CTS***//
                        oBe.pland_ncts_gratificacion,
                        oBe.pland_nctssexto_gratificacion,
                        oBe.pland_ncts_comision,
                        oBe.pland_nctssexto_comision,
                        oBe.pland_ncts_total,
                        oBe.pland_icts_meses,
                        oBe.pland_icts_dias,
                        oBe.pland_ncts_meses_monto,
                        oBe.pland_ncts_dias_monto,
                        oBe.pland_ncts_por_mes,
                        oBe.pland_nctsprovision_acumulada,
                        oBe.pland_nctsprovision_mes,
                        oBe.pland_ncts_horas_extras,
                        oBe.pland_vac_dias_Prov_mensual,
                        oBe.pland_vac_dias_acumulado,
                        oBe.pland_vac_provision_mes,
                        oBe.pland_vac_ajuste_mes,
                        oBe.pland_vac_prov_tot_mensual

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProvisionPlanillaPersonalDetalle(EProvisionPlanillaPersonalDetalle oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVISION_PLANILLA_PERSONAL_DETALLE_MODIFICAR(
                        oBe.pland_icod_planilla_personal_det,
                        oBe.pland_ape_nom,
                        oBe.pland_num_doc,
                        oBe.pland_cussp,
                        oBe.pland_rem_basica,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.pland_nrem_computable,
                        oBe.pland_nmonto_gratificacion,
                        oBe.pland_nbonificacion_mes,
                        //****CTS***//
                        oBe.pland_ncts_gratificacion,
                        oBe.pland_nctssexto_gratificacion,
                        oBe.pland_ncts_comision,
                        oBe.pland_nctssexto_comision,
                        oBe.pland_ncts_total,
                        oBe.pland_icts_meses,
                        oBe.pland_icts_dias,
                        oBe.pland_ncts_meses_monto,
                        oBe.pland_ncts_dias_monto,
                        oBe.pland_ncts_por_mes,
                        oBe.pland_nctsprovision_acumulada,
                        oBe.pland_nctsprovision_mes,
                        oBe.pland_ncts_horas_extras,
                        oBe.pland_vac_dias_Prov_mensual,
                        oBe.pland_vac_dias_acumulado,
                        oBe.pland_vac_provision_mes,
                        oBe.pland_vac_ajuste_mes,
                        oBe.pland_vac_prov_tot_mensual
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProvisionPlanillaPersonalDetalle(EProvisionPlanillaPersonalDetalle oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGE_PROVISION_PLANILLA_PERSONAL_DETALLE_ELIMINAR(
                        oBe.pland_icod_planilla_personal_det,
                        oBe.intUsuario,
                        oBe.strPc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProvisionPlanillaPersonalDetalle> listarProvisionPlanillaPersonalDetalle_Gratif()
        {
            List<EProvisionPlanillaPersonalDetalle> lista = new List<EProvisionPlanillaPersonalDetalle>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGE_PROVISION_PLANILLA_PERSONAL_DETALLE_LISTAR_GRATIF();
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
                            pland_ncts_horas_extras = item.pland_ncts_horas_extras

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

        #region Inicial Provision Planilla

        public List<EInicial_Prov_planilla> ListarInicial_Prov_Planilla()
        {
            List<EInicial_Prov_planilla> lista = new List<EInicial_Prov_planilla>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_INICIAL_PROVISION_PLANILLA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EInicial_Prov_planilla()
                        {
                            ippc_icod_inicial_provision_planilla = item.ippc_icod_inicial_provision_planilla,
                            ippc_iid_inicial_provision_planilla = item.ippc_iid_inicial_provision_planilla,
                            ippc_vdescripcion = item.ippc_vdescripcion


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

        public int InsertarInicial_Prov_Planilla(EInicial_Prov_planilla oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INICIAL_PROVISION_PLANILLA_INSERTAR(
                        ref intIcod,
                         oBe.ippc_iid_inicial_provision_planilla,
                         oBe.ippc_vdescripcion,
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

        public void modificarInicial_Prov_Planilla(EInicial_Prov_planilla oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INICIAL_PROVISION_PLANILLA_MODIFICAR(
                        oBe.ippc_icod_inicial_provision_planilla,
                        oBe.ippc_vdescripcion,
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

        public void eliminarInicial_Prov_Planilla(EInicial_Prov_planilla oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INICIAL_PROVISION_PLANILLA_ELIMINAR(
                        oBe.ippc_icod_inicial_provision_planilla,
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

        #region Inicial Provision Planilla Detalle

        public List<EInicial_Prov_planilla_Det> ListarInicial_Prov_Planilla_Detalle(int cod)
        {
            List<EInicial_Prov_planilla_Det> lista = new List<EInicial_Prov_planilla_Det>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_INICIAL_PROVISION_PLANILLA_DETALLE_LISTAR().Where(x => Convert.ToInt32(x.ippc_icod_inicial_provision_planilla) == cod).ToList();
                    foreach (var item in query)
                    {
                        lista.Add(new EInicial_Prov_planilla_Det()
                        {
                            ippd_icod_inicial_provision_planilla_detalle = item.ippd_icod_inicial_provision_planilla_detalle,
                            ippc_icod_inicial_provision_planilla = item.ippc_icod_inicial_provision_planilla,
                            ippd_iid_inicial_provision_planilla_detalle = item.ippd_iid_inicial_provision_planilla_detalle,
                            perc_icod_personal = item.perc_icod_personal,
                            ippd_ninicial = item.ippd_ninicial,
                            ippd_sflag_estado = item.ippd_sflag_estado,
                            strIdPersonal = item.strIdPersonal,
                            strNomyApell = item.strNomyApell

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

        public int InsertarInicial_Prov_Planilla_Detalle(EInicial_Prov_planilla_Det oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INICIAL_PROVISION_PLANILLA_DETALLE_INSERTAR(
                        ref intIcod,
                        oBe.ippc_icod_inicial_provision_planilla,
                        oBe.ippd_iid_inicial_provision_planilla_detalle,
                        oBe.perc_icod_personal,
                        oBe.ippd_ninicial,
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

        public void modificarInicial_Prov_Planilla_Detalle(EInicial_Prov_planilla_Det oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INICIAL_PROVISION_PLANILLA_DETALLE_MODIFICAR(
                        oBe.ippd_icod_inicial_provision_planilla_detalle,
                        oBe.ippd_ninicial,
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

        public void eliminarInicial_Prov_Planilla_Detalle(EInicial_Prov_planilla_Det oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_INICIAL_PROVISION_PLANILLA_DETALLE_ELIMINAR(
                        oBe.ippd_icod_inicial_provision_planilla_detalle,
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

        #region Parametro Planilla
        public List<EParametroPlanilla> listarParametroPlanilla()
        {
            List<EParametroPlanilla> lista = new List<EParametroPlanilla>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_PARAMETRO_PLANILLA_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EParametroPlanilla()
                        {
                            prpc_nasignacion_familiar = item.prpc_nasignacion_familiar,
                            prpc_ngratificacion_essalud = item.prpc_ngratificacion_essalud,
                            prpc_ngratificacion_eps = item.prpc_ngratificacion_eps,
                            prpc_id_cta_remuneracion = Convert.ToInt32(item.prpc_id_cta_remuneracion),
                            CuentaRemuneracion = item.CuentaRemuneracion,
                            prpc_id_cta_vacaciones = Convert.ToInt32(item.prpc_id_cta_vacaciones),
                            CuentaVacaciones = item.CuentaVacaciones,
                            prpc_id_cta_gratificaciones = Convert.ToInt32(item.prpc_id_cta_gratificaciones),
                            CuentaGratificaciones = item.CuentaGratificaciones,
                            prpc_id_cta_cts = Convert.ToInt32(item.prpc_id_cta_cts),
                            CuentaCTS = item.CuentaCTS,
                            prpc_nporc_essalud = item.prpc_nporc_essalud,
                            prpc_nporc_eps_pacifico = item.prpc_nporc_eps_pacifico,
                            prpc_nporc_eps_essalud = item.prpc_nporc_eps_essalud,
                            prpc_nsueldo_minimo = item.prpc_nsueldo_minimo,
                            /*Cuentas Destino*/
                            prpc_id_cta_destino_vacaciones = Convert.ToInt32(item.prpc_id_cta_destino_vacaciones),
                            CuentaDestinoVacaciones = item.CuentaDestinoVacaciones,
                            prpc_id_cta_destino_gratificaciones = Convert.ToInt32(item.prpc_id_cta_destino_gratificaciones),
                            CuentaDestinoGratificaciones = item.CuentaDestinoGratificaciones,
                            prpc_id_cta_destino_cts = Convert.ToInt32(item.prpc_id_cta_destino_cts),
                            CuentaDestinoCTS = item.CuentaDestinoCTS,
                            prpc_ndias_trabajo = item.prpc_ndias_trabajo
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
        public int insertarParametroPlanilla(EParametroPlanilla oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_PARAMETRO_PLANILLA_INSERTAR(
                        oBe.prpc_nasignacion_familiar,
                        oBe.prpc_ngratificacion_essalud,
                        oBe.prpc_ngratificacion_eps,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.prpc_id_cta_remuneracion,
                        oBe.prpc_id_cta_vacaciones,
                        oBe.prpc_id_cta_gratificaciones,
                        oBe.prpc_id_cta_cts,
                         oBe.prpc_nporc_essalud,
                        oBe.prpc_nporc_eps_pacifico,
                        oBe.prpc_nporc_eps_essalud,
                        oBe.prpc_nsueldo_minimo,
                        oBe.prpc_id_cta_destino_vacaciones,
                        oBe.prpc_id_cta_destino_gratificaciones,
                        oBe.prpc_id_cta_destino_cts,
                        oBe.prpc_ndias_trabajo

                        );
                }
                return Convert.ToInt32(intIcod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarParametroPlanilla(EParametroPlanilla oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_PARAMETRO_PLANILLA_MODIFICAR(
                        oBe.prpc_nasignacion_familiar,
                        oBe.prpc_ngratificacion_essalud,
                        oBe.prpc_ngratificacion_eps,
                        oBe.intUsuario,
                        oBe.strPc,
                        oBe.prpc_id_cta_remuneracion,
                        oBe.prpc_id_cta_vacaciones,
                        oBe.prpc_id_cta_gratificaciones,
                        oBe.prpc_id_cta_cts,
                         oBe.prpc_nporc_essalud,
                        oBe.prpc_nporc_eps_pacifico,
                        oBe.prpc_nporc_eps_essalud,
                        oBe.prpc_nsueldo_minimo,
                        oBe.prpc_id_cta_destino_vacaciones,
                        oBe.prpc_id_cta_destino_gratificaciones,
                        oBe.prpc_id_cta_destino_cts,
                        oBe.prpc_ndias_trabajo
                        );

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VACACIONES

        public List<EVacaciones> ListarVacaciones()
        {
            List<EVacaciones> lista = new List<EVacaciones>();
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {

                    var query = dc.SGEP_VACACIONES_LISTAR();
                    foreach (var item in query)
                    {
                        lista.Add(new EVacaciones()
                        {
                            vacd_icod_vacaciones = item.vacd_icod_vacaciones,
                            vacd_iid_vacaciones = item.vacd_iid_vacaciones,
                            vacd_icod_personal = item.vacd_icod_personal,
                            vacd_ndias = item.vacd_ndias,
                            vacd_sfecha_ini = item.vacd_sfecha_ini,
                            vacd_sfecha_fin = item.vacd_sfecha_fin,
                            vacd_mes = item.vacd_mes,
                            vacd_año = item.vacd_año,
                            strMes = item.strMes

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

        public int InsertarVacaciones(EVacaciones oBe)
        {
            int? intIcod = 0;
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_VACACIONES_INSERTAR(
                        ref intIcod,
                        oBe.vacd_iid_vacaciones,
                        oBe.vacd_icod_personal,
                        oBe.vacd_ndias,
                        oBe.vacd_sfecha_ini,
                        oBe.vacd_sfecha_fin,
                        oBe.vacd_mes,
                        oBe.vacd_año,
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

        public void modificarVacaciones(EVacaciones oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_VACACIONES_MODIFICAR(
                       oBe.vacd_icod_vacaciones,
                        oBe.vacd_ndias,
                        oBe.vacd_sfecha_ini,
                        oBe.vacd_sfecha_fin,
                        oBe.vacd_mes,
                        oBe.vacd_año,
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

        public void eliminarVacaciones(EVacaciones oBe)
        {
            try
            {
                using (PlanillasDataContext dc = new PlanillasDataContext(Helper.conexion()))
                {
                    dc.SGEP_VACACIONES_ELIMINAR(
                        oBe.vacd_icod_vacaciones,
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
    }
}

