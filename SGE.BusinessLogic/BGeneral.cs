using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace SGE.BusinessLogic
{
    public class BGeneral
    {
        #region Tabla Registro
        /*Los ID´s de los Tipos de Tabla se pueden ver en la Clase Parámetros*/
        public List<ETablaRegistro> listarTablaRegistro(int intTipoTabla)
        {
            List<ETablaRegistro> lista = null;
            try
            {
                lista = new GeneralData().listarTablaRegistro(intTipoTabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        #endregion
        #region Carpeta de Fotos
        public void crearPictureFolder()
        {
            if (Directory.Exists(@"C:\WindowsServicenFiles") == false)
            {
                Directory.CreateDirectory(@"C:\WindowsServicenFiles");
            }
            else
            {
                string strSubDirectory = @"Imagenes";
                DirectoryInfo strDirectory = new DirectoryInfo("C:\\WindowsServicenFiles");
                DirectoryInfo dirInfo = strDirectory.CreateSubdirectory(strSubDirectory);

            }
        }
        #endregion
        #region tipo de Cambio Mensual
        public List<ETipoCambioMensual> ListarTipoCambioMensual()
        {
            List<ETipoCambioMensual> lista = null;
            try
            {
                lista = (new GeneralData()).ListarTipoCambioMensual();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void InsertarTipoCambioMensual(ETipoCambioMensual obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new GeneralData().InsertarTipoCambioMensual(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarTipoCambioMensual(ETipoCambioMensual obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new GeneralData().ActualizarTipoCambioMensual(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EliminarTipoCambioMensual(ETipoCambioMensual obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new GeneralData().EliminarTipoCambioMensual(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Equipo_Ingresar(string strNombreEquipo, string strIdCpu)
        {
            new GeneralData().Equipo_Ingresar(strNombreEquipo, strIdCpu);
        }
        #endregion
    }
}
