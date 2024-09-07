using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BDocXCobrarDocxPagarCanje
    {
        CuentasPorCobrarData objDocXCobrarData = new CuentasPorCobrarData();

        public void InsertarCanjeDXCconDXP(EDocPorPagarPago canje, int opcionVCO)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.InsertarCanjeDXCconDXP(canje, opcionVCO);

                    tx.Complete();
                }
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.ActualizarCanjeDXCconDXP(canje, opcionVCO);
                    tx.Complete();
                }
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objDocXCobrarData.EliminarCanjeDXCconDXP(canje);
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
