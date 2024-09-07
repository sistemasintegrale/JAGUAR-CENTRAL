using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BPlanillas
    {

        PlanillasData objtPlanillasData = new PlanillasData();
        #region Empresa
        public List<EEmpresa> listarEmpresa()
        {
            List<EEmpresa> lista = null;
            try
            {
                lista = new PlanillasData().ListarEmpresa();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarEmpresa(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Cargo
        public List<ECargo> listarCargo()
        {
            List<ECargo> lista = null;
            try
            {
                lista = new PlanillasData().ListarCargo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarCargo(ECargo oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarCargo(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarCargo(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarCargo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Areas
        public List<EAreas> listarAreas()
        {
            List<EAreas> lista = null;
            try
            {
                lista = new PlanillasData().ListarAreas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarAreas(EAreas oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarAreas(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarAreas(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarAreas(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FondosPensiones
        public List<EFondosPensiones> listarFondosPensiones()
        {
            List<EFondosPensiones> lista = null;
            try
            {
                lista = new PlanillasData().ListarFondosPensiones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarFondosPensiones(EFondosPensiones oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarFondosPensiones(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarFondosPensiones(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarFondosPensiones(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FondosPensionesConceptos

        public List<EFondosPensionesConceptos> listarFondosPensionesConceptos(int codFondosPensiones)
        {
            List<EFondosPensionesConceptos> lista = null;
            try
            {
                lista = new PlanillasData().ListarFondosPensionesConceptos(codFondosPensiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarFondosPensionesConceptos(EFondosPensionesConceptos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarFondosPensionesConceptos(oBe);
                    //new PlanillasData().modificarPorcentajeFondoFijo(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarFondosPensionesConceptos(oBe);
                    //new PlanillasData().modificarPorcentajeFondoFijo(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarFondosPensionesConceptos(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPorcentajeFondoFijo(CodPension);

                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarPorcentajeFijo(codPension, usuario, pc);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FondosPensionesConceptosMixtas

        public List<EFondosPensionesMixtas> listarFondosPensionesMixtas(int codFondosPensiones)
        {
            List<EFondosPensionesMixtas> lista = null;
            try
            {
                lista = new PlanillasData().ListarFondosPensionesMixtas(codFondosPensiones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarFondosPensionesMixtas(EFondosPensionesMixtas oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarFondosPensionesMixtas(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarFondosPensionesMixtas(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarFondosPensionesMixtas(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPorcentajeFondoMixto(CodPension);

                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarPorcentajeMixto(codPension, usuario, pc);

                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region TablaPensiones

        public List<ETablaPlanilla> listarTablaPlanilla()
        {
            List<ETablaPlanilla> lista = null;
            try
            {
                lista = new PlanillasData().ListarTablaPlanilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarTablaPlanilla(ETablaPlanilla oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarTablaPlanilla(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarTablaPlanilla(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarTablaPlanilla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion

        #region TablaPensionesDetalle

        public List<ETablaPlanillaDetalle> listarTablaPlanillaDetalle(int codTablaPlanilla)
        {
            List<ETablaPlanillaDetalle> lista = null;
            try
            {
                lista = new PlanillasData().ListarTablePlanillaDetalle(codTablaPlanilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarTablaPlanillaDetalle(ETablaPlanillaDetalle oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarTablaPlanillaDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarTablaPlanillaDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarTablaPlanillaDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarDetalleTablaPlanilla(cod, usuario, pc);
                    tx.Complete();
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
            List<EPersonal> lista = null;
            try
            {
                lista = new PlanillasData().listarPersonal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPersonal(EPersonal oBe, List<EPersonalCCostos> lstPersonalCC, List<EPersonal> lstcontrato, List<EArchivos> lstArchivos)
        {
            int intIcod = 0;

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarPersonal(oBe);
                    lstPersonalCC.ForEach(x =>
                    {
                        x.perc_icod_personal = intIcod;
                        insertarPersonalCCostos(x);


                    });
                    lstcontrato.ForEach(c =>
                    {
                        c.perc_icod_personal = intIcod;
                        new PlanillasData().insertarPersonal_contratacion(c);
                    });
                    foreach (var obd in lstArchivos)
                    {
                        obd.arch_iid_personal = intIcod;
                        new PlanillasData().insertarArchivos(obd);
                    }
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPersonal(EPersonal oBe, List<EPersonalCCostos> lstPersonalCC, List<EPersonalCCostos> lstPersonalCCDelete, List<EPersonal> lstpersonalContrato, List<EPersonal> lstPersonalcontratoDelete, List<EArchivos> lstArchivos)
        {
            int intIcod = 0;

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPersonal(oBe);
                    lstPersonalCCDelete.ForEach(x =>
                    {

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarPersonalCCostos(x);
                    });
                    lstPersonalcontratoDelete.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        x.pctd_icod_persona_contratacion = x.pctd_icod_persona_contratacion;
                        new PlanillasData().eliminarPersonal_contratacion(x);
                    });
                    lstArchivos.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        new PlanillasData().eliminarArchivos(x);
                    });
                    lstPersonalCC.ForEach(x =>
                    {
                        if (x.intTpoOperacion == 1)
                        {
                            x.perc_icod_personal = oBe.perc_icod_personal;
                            insertarPersonalCCostos(x);

                        }

                        else if (x.intTpoOperacion == 2)
                        {
                            x.perc_icod_personal = oBe.perc_icod_personal;
                            modificarPersonalCCostos(x);

                        }
                    });

                    lstpersonalContrato.ForEach(d =>
                    {
                        if (d.intTpoOperacion == 1)
                        {
                            d.perc_icod_personal = oBe.perc_icod_personal;

                            new PlanillasData().insertarPersonal_contratacion(d);
                        }
                        else if (d.intTpoOperacion == 2)
                        {
                            d.perc_icod_personal = oBe.perc_icod_personal;
                            new PlanillasData().modificarPersonal_contratacion(d);
                        }

                    });
                    foreach (var obd in lstArchivos)
                    {
                        if (obd.intTipoOperacion == 1)
                        {
                            obd.arch_iid_personal = oBe.perc_icod_personal;
                            new PlanillasData().insertarArchivos(obd);
                        }
                        else if (obd.intTipoOperacion == 2)
                        {
                            obd.arch_iid_personal = oBe.perc_icod_personal;
                            new PlanillasData().modificarArchivos(obd);
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
        public void eliminarPersonal(EPersonal oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarPersonal(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EPersonalCCostos> listarCCostos()
        {
            List<EPersonalCCostos> lista = null;
            try
            {
                lista = new PlanillasData().listarCCostos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EPersonalCCostos> listarPersonalCCostos(int cod)
        {
            List<EPersonalCCostos> lista = null;
            try
            {
                lista = new PlanillasData().listarPersonalCCostos(cod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPersonalCCostos(EPersonalCCostos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarPersonalCCostos(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPersonalCCostos(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarPersonalCCostos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int insertarCCostos(EPersonalCCostos oBe, List<EPersonalCCostos> lstPersonalCC)
        {
            int intIcod = 0;

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //intIcod = new PlanillasData().insertarPersonal(oBe);
                    lstPersonalCC.ForEach(x =>
                    {
                        //x.perc_icod_personal = intIcod;
                        insertarPersonalCCostos(x);


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
        public void modificarCCostos(EPersonalCCostos oBe, List<EPersonalCCostos> lstPersonalCC, List<EPersonalCCostos> lstPersonalCCDelete)
        {
            int intIcod = 0;

            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //new PlanillasData().modificarPersonal(oBe);
                    lstPersonalCCDelete.ForEach(x =>
                    {

                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarPersonalCCostos(x);
                    });


                    lstPersonalCC.ForEach(x =>
                    {
                        if (x.intTpoOperacion == 1)
                        {
                            x.perc_icod_personal = oBe.perc_icod_personal;
                            insertarPersonalCCostos(x);

                        }

                        else if (x.intTpoOperacion == 2)
                        {
                            x.perc_icod_personal = oBe.perc_icod_personal;
                            modificarPersonalCCostos(x);

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
        public List<EPersonalCCostos> listarPersonalControlCCostos()
        {
            List<EPersonalCCostos> lista = null;
            try
            {
                lista = new PlanillasData().listarPersonalControlCCostos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region ConceptosIngresos
        public List<EConceptosIngresos> listarIngreso()
        {
            List<EConceptosIngresos> lista = null;
            try
            {
                lista = new PlanillasData().listarIngresos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarIngreso(EConceptosIngresos oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarIngresos(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarIngreso(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarIngreso(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ConceptosDescuento
        public List<EConceptoDescuento> listarConceptoDescuento()
        {
            List<EConceptoDescuento> lista = null;
            try
            {
                lista = new PlanillasData().listarConceptoDescuento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarConceptoDescuento(EConceptoDescuento oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarConceptoDescuento(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarConceptoDescuento(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarConceptoDescuento(oBe);
                    tx.Complete();
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
            List<EConceptoAportacion> lista = null;
            try
            {
                lista = new PlanillasData().listarConceptoAportacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarConceptoAportacion(EConceptoAportacion oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarConceptoAportacion(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarConceptoAportacion(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarConceptoAportacion(oBe);
                    tx.Complete();
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
            List<ECargo> lista = null;
            try
            {
                lista = new PlanillasData().listarComboCargo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }


        public List<EAreas> listarComboArea()
        {
            List<EAreas> lista = null;
            try
            {
                lista = new PlanillasData().listarComboArea();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ETablaPlanillaDetalle> listarComboTablaPlanillaDetalle(int num)
        {
            List<ETablaPlanillaDetalle> lista = null;
            try
            {
                lista = new PlanillasData().listarComboTablaPlanillaDetalle(num);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region lista de datos de contratacion
        public List<EPersonal> listarPersonal_contratacion(int cod)
        {
            List<EPersonal> lista = null;
            try
            {
                lista = new PlanillasData().listarPersonal_contratacion(cod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Archivos
        public List<EArchivos> listarArchivos(int cod)
        {
            List<EArchivos> lista = null;
            try
            {
                lista = new PlanillasData().listarArchivos(cod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion

        #region Planilla Personal
        public List<EPlanillaPersonal> listarPlanillaPersonal()
        {
            List<EPlanillaPersonal> lista = null;
            try
            {
                lista = new PlanillasData().listarPlanillaPersonal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPlanillaPersonal(EPlanillaPersonal oBe, List<EPlanillaPersonalDetalle> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarPlanillaPersonal(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        x.planc_icod_planilla_personal = intIcod;
                        insertarPlanillaPersonalDetalle(x);
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
        public void modificarPlanillaPersonal(EPlanillaPersonal oBe, List<EPlanillaPersonalDetalle> lstDetalle, List<EPlanillaPersonalDetalle> lstDetalleEliminados)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPlanillaPersonal(oBe);
                    foreach (var objdetalle in lstDetalle)
                    {
                        if (objdetalle.operacion == 1)
                        {

                            objdetalle.planc_icod_planilla_personal = oBe.planc_icod_planilla_personal;
                            insertarPlanillaPersonalDetalle(objdetalle);

                        }
                        else
                        {
                            objdetalle.planc_icod_planilla_personal = oBe.planc_icod_planilla_personal;
                            modificarPlanillaPersonalDetalle(objdetalle);
                        }

                    }
                    foreach (var objElim in lstDetalleEliminados)
                    {
                        if (objElim.pland_icod_planilla_personal_det != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        eliminarPlanillaPersonalDetalle(objElim);
                    }
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lstDet = new BPlanillas().listarPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal);
                    lstDet.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarPlanillaPersonalDetalle(x);
                    });
                    new PlanillasData().eliminarPlanillaPersonal(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarPlanillaPersonalSituacion(EPlanillaPersonal oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPlanillaPersonal(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Planilla Personal Detalle
        public List<EPlanillaPersonalDetalle> listarPlanillaPersonalDetalle(int planc_icod_planilla_personal)
        {
            List<EPlanillaPersonalDetalle> lista = null;
            try
            {
                lista = new PlanillasData().listarPlanillaPersonalDetalle(planc_icod_planilla_personal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarPlanillaPersonalDetalle(EPlanillaPersonalDetalle oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarPlanillaPersonalDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarPlanillaPersonalDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarPlanillaPersonalDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Provision Planilla Personal
        public List<EProvisionPlanillaPersonal> listarProvisionPlanillaPersonal()
        {
            List<EProvisionPlanillaPersonal> lista = null;
            try
            {
                lista = new PlanillasData().listarProvisionPlanillaPersonal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarProvisionPlanillaPersonal(EProvisionPlanillaPersonal oBe, List<EProvisionPlanillaPersonalDetalle> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarProvisionPlanillaPersonal(oBe);
                    lstDetalle.ForEach(x =>
                    {
                        x.planc_icod_planilla_personal = intIcod;
                        insertarProvisionPlanillaPersonalDetalle(x);
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
        public void modificarProvisionPlanillaPersonal(EProvisionPlanillaPersonal oBe, List<EProvisionPlanillaPersonalDetalle> lstDetalle, List<EProvisionPlanillaPersonalDetalle> lstDetalleEliminados)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarProvisionPlanillaPersonal(oBe);
                    foreach (var objdetalle in lstDetalle)
                    {
                        if (objdetalle.operacion == 1)
                        {

                            objdetalle.planc_icod_planilla_personal = oBe.planc_icod_planilla_personal;
                            insertarProvisionPlanillaPersonalDetalle(objdetalle);

                        }

                    }
                    foreach (var objElim in lstDetalleEliminados)
                    {
                        if (objElim.pland_icod_planilla_personal_det != null)
                        {
                            //new AlmacenData().EliminarKardex(Convert.ToInt64(objElim.kardc_iid_correlativo));
                            //new AlmacenData().ActualizarStockProductoCantidad(Convert.ToInt32(obj.almac_icod_almacen), Convert.ToInt32(objElim.prdc_icod_producto), 0, Convert.ToInt32(objElim.ocod_ncantidad));
                        }
                        eliminarProvisionPlanillaPersonalDetalle(objElim);
                    }
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var lstDet = new BPlanillas().listarProvisionPlanillaPersonalDetalle(oBe.planc_icod_planilla_personal);
                    lstDet.ForEach(x =>
                    {
                        x.intUsuario = oBe.intUsuario;
                        x.strPc = oBe.strPc;
                        eliminarProvisionPlanillaPersonalDetalle(x);
                    });
                    new PlanillasData().eliminarProvisionPlanillaPersonal(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProvisionPlanillaPersonalSituacion(EProvisionPlanillaPersonal oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarProvisionPlanillaPersonal(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Provision Planilla Personal Detalle
        public List<EProvisionPlanillaPersonalDetalle> listarProvisionPlanillaPersonalDetalle(int planc_icod_planilla_personal)
        {
            List<EProvisionPlanillaPersonalDetalle> lista = null;
            try
            {
                lista = new PlanillasData().listarProvisionPlanillaPersonalDetalle(planc_icod_planilla_personal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarProvisionPlanillaPersonalDetalle(EProvisionPlanillaPersonalDetalle oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarProvisionPlanillaPersonalDetalle(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarProvisionPlanillaPersonalDetalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarProvisionPlanillaPersonalDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EProvisionPlanillaPersonalDetalle> listarProvisionPlanillaPersonalDetalle_Gratif()
        {
            List<EProvisionPlanillaPersonalDetalle> lista = null;
            try
            {
                lista = new PlanillasData().listarProvisionPlanillaPersonalDetalle_Gratif();
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
            List<EInicial_Prov_planilla> lista = null;
            try
            {
                lista = new PlanillasData().ListarInicial_Prov_Planilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarInicial_Prov_Planilla(EInicial_Prov_planilla oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarInicial_Prov_Planilla(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarInicial_Prov_Planilla(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarInicial_Prov_Planilla(oBe);
                    tx.Complete();
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
            List<EInicial_Prov_planilla_Det> lista = null;
            try
            {
                lista = new PlanillasData().ListarInicial_Prov_Planilla_Detalle(cod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int InsertarInicial_Prov_Planilla_Detalle(EInicial_Prov_planilla_Det oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarInicial_Prov_Planilla_Detalle(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarInicial_Prov_Planilla_Detalle(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarInicial_Prov_Planilla_Detalle(oBe);
                    tx.Complete();
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
            List<EParametroPlanilla> lista = null;
            try
            {
                lista = new PlanillasData().listarParametroPlanilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarParametroPlanilla(EParametroPlanilla oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().insertarParametroPlanilla(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarParametroPlanilla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VACACIONES
        public List<EVacaciones> listarVacaciones()
        {
            List<EVacaciones> lista = null;
            try
            {
                lista = new PlanillasData().ListarVacaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int insertarVacaciones(EVacaciones oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new PlanillasData().InsertarVacaciones(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().modificarVacaciones(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new PlanillasData().eliminarVacaciones(oBe);
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
