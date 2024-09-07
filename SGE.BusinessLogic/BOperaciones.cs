using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BOperaciones
    {
        #region Personal
        public List<EVendedor> listarVendedor()
        {
            List<EVendedor> lista = null;
            try
            {
                lista = new OperacionesData().listarVendedor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarVendedor(EVendedor oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new OperacionesData().insertarVendedor(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarVendedor(EVendedor oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().modificarVendedor(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarVendedor(EVendedor oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().eliminarVendedor(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Proforma Cab
        public List<EProforma> listarProforma(int intEjercicio)
        {
            List<EProforma> lista = new List<EProforma>();
            try
            {
                lista = new OperacionesData().listarProforma(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarProforma(EProforma oBe, List<EProformaDet> lstDetalle)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new OperacionesData().insertarProforma(oBe);
                    oBe.prfc_icod_proforma = intIcod;
                    lstDetalle.ForEach(x =>
                    {
                        x.prfc_icod_proforma = intIcod;
                        insertarProformaDetalle(x);
                    });
                    //if (oBe.prfc_recepcion != "" || oBe.prfc_recomendacion != "")
                    new OperacionesData().insertarRecepRecom(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProforma(EProforma oBe, List<EProformaDet> lstDetalle, List<EProformaDet> lstDelete)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().modificarProforma(oBe);
                    lstDelete.ForEach(x =>
                    {
                        eliminarProformaDetalle(x);
                    });
                    lstDetalle.ForEach(x =>
                    {
                        if (x.intTipoOperacion == 1)
                        {
                            x.prfc_icod_proforma = oBe.prfc_icod_proforma;
                            insertarProformaDetalle(x);
                        }
                        else if (x.intTipoOperacion == 2)
                        {
                            modificarProformaDetalle(x);
                        }
                    });
                    //if (oBe.prfc_recepcion != "" || oBe.prfc_recomendacion != "")
                    new OperacionesData().insertarRecepRecom(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProforma(EProforma oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().eliminarProforma(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Proforma Det
        public List<EProformaDet> listarProformaDetalle(int intProforma)
        {
            List<EProformaDet> lista = new List<EProformaDet>();
            try
            {
                lista = new OperacionesData().listarProformaDetalle(intProforma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void insertarProformaDetalle(EProformaDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().insertarProformaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProformaDetalle(EProformaDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().modificarProformaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProformaDetalle(EProformaDet oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().eliminarProformaDetalle(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Productividad
        //
        public List<EProductividad> listarProductividad(DateTime? dtFecha_Inicio, DateTime? dtFecha_Fin, int intEjercicio)
        {
            List<EProductividad> lista = new List<EProductividad>();
            try
            {
                lista = new OperacionesData().listarProductividad(dtFecha_Inicio, dtFecha_Fin, intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<EProductividadDet> listarProductividadDetalle(int intPersonal, DateTime? dtFecha_Inicio, DateTime? dtFecha_Fin, int intEjercicio)
        {
            List<EProductividadDet> lista = new List<EProductividadDet>();
            try
            {
                lista = new OperacionesData().listarProductividadDetalle(intPersonal, dtFecha_Inicio, dtFecha_Fin, intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Proyectos
        public int insertarProyectos(EPersonal oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = new OperacionesData().insertarProyectos(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarProyectos(EPersonal oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().modificarProyectos(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarProyectos(EPersonal oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new OperacionesData().eliminarProyectos(oBe);
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
