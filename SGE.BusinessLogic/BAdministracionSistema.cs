using ICSharpCode.SharpZipLib.Zip;
using SGE.DataAccess;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;


namespace SGE.BusinessLogic
{
    public class BAdministracionSistema
    {
        AdministracionSistemaData objAdministracioData = new AdministracionSistemaData();
        CentralData objCentralData = new CentralData();
        #region Usuario
        public List<EUsuario> listarUsuarios()
        {
            List<EUsuario> lista = null;
            try
            {
                lista = objAdministracioData.listarUsuarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarUsuario(EUsuario oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarUsuario(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarUsuario(EUsuario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarUsuario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarUsuario(EUsuario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarUsuario(oBe);
                    tx.Complete();
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
            List<EFormulario> lista = null;
            try
            {
                lista = objAdministracioData.listarAccesosNoPermitidos(intUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public List<EFormulario> listarAccesosPermitidos(int intUsuario)
        {
            List<EFormulario> lista = null;
            try
            {
                lista = objAdministracioData.listarAccesosPermitidos(intUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarAccesoUsuario(EFormulario oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarAccesoUsuario(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarAccesoUsuario(EFormulario oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarAccesoUsuario(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ControlEquipos Equipo_Obtner_Datos(string text, string idCpu)
        {
            return objAdministracioData.Equipo_Obtner_Datos(text, idCpu);
        }
        #endregion
        #region Tipo Documento
        public List<ETipoDocumento> listarTipoDocumento()
        {
            List<ETipoDocumento> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public List<ControlVersiones> Listar_Versiones()
        {
            return objAdministracioData.Listar_Versiones();
        }

        public List<ETipoDocumento> listarTipoDocumentoPorModulo(int IdModulo)
        {
            List<ETipoDocumento> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumentoPorModulo(IdModulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoDocumento(ETipoDocumento oBe, List<EModulo> lstModulo)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoDocumento(oBe);
                    /*----------------------------------------------------------------*/
                    oBe.tdocc_icod_tipo_doc = intIcod;
                    /*----------------------------------------------------------------*/
                    objAdministracioData.insertarTipoDocumentoDetalle(oBe, lstModulo);
                    /*----------------------------------------------------------------*/
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Equipo_Modificar(ControlEquipos objEquipo)
        {
            var task = new Task(() => objAdministracioData.Equipo_Modificar(objEquipo));
            task.Start();
            await task;
        }

        public void modificarTipoDocumento(ETipoDocumento oBe, List<EModulo> lstModulo)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoDocumento(oBe);
                    /*----------------------------------------------------------------*/
                    objAdministracioData.eliminarTipoDocumentoDetalle(oBe);
                    /*----------------------------------------------------------------*/
                    objAdministracioData.insertarTipoDocumentoDetalle(oBe, lstModulo);
                    /*----------------------------------------------------------------*/
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoDocumento(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tipo Documento Detalle
        public List<ETipoDocumentoDetalle> listarTipoDocumentoDetalle(ETipoDocumento oBe)
        {
            List<ETipoDocumentoDetalle> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumentoDetalle(oBe);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        #endregion
        #region Tipo Documento Detalle Cuenta
        public List<ETipoDocumentoDetalleCta> listarTipoDocumentoDetCta(int intTipoDoc)
        {
            List<ETipoDocumentoDetalleCta> lista = null;
            try
            {
                lista = objAdministracioData.listarTipoDocumentoDetCta(intTipoDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoDocumentoDetCta(ETipoDocumentoDetalleCta oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoDocumentoDetCta(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTipoDocumentoDetCta(ETipoDocumentoDetalleCta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoDocumentoDetCta(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTipoDocumentoDetCta(ETipoDocumentoDetalleCta oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoDocumentoDetCta(oBe);
                    tx.Complete();
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
            try
            {
                lista = objAdministracioData.listarModulo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarModulo(EModulo oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarModulo(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarModulo(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarModulo(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Parametro
        public void modificarCorrelativoOR(EParametro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    (new AdministracionSistemaData()).modificarCorrelativoOR(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EParametro> listarParametro()
        {
            List<EParametro> lista = null;
            try
            {
                lista = objAdministracioData.listarParametro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarParametro(EParametro obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarParametro(obj);
                    tx.Complete();
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
            List<ETabla> lista = null;
            try
            {
                lista = objAdministracioData.listarTabla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTabla(ETabla oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTabla(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTabla(ETabla oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTabla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTabla(ETabla oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTabla(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Tabla Registro
        public int insertarTablaRegistro(ETablaRegistro oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTablaRegistro(oBe);
                    tx.Complete();
                }
                return intIcod;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void modificarTablaRegistro(ETablaRegistro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTablaRegistro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void eliminarTablaRegistro(ETablaRegistro oBe)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTablaRegistro(oBe);
                    tx.Complete();
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
                lista = objAdministracioData.listarTipoCambio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoCambio(ETipoCambio oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoCambio(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoCambio(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoCambio(oBe);
                    tx.Complete();
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
                lista = objAdministracioData.listarTipoCambioEuro(intEjercicio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarTipoCambioEuro(ETipoCambioEuro oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarTipoCambioEuro(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarTipoCambioEuro(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarTipoCambioEuro(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Update
        public string GetCorrelativoDocumentoBancos(int anio, int mes, int cuenta, int tipo_doc)
        {
            string nro = "";
            try
            {
                //nro = (new TesoreriaData()).GetCorrelativoDocumentoBancos(anio, mes, cuenta, tipo_doc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nro;
        }
        public void ActualizarCorrelativoDocumentoBancos(int anio, int mes, int cuenta, int tipo_doc, string nro_doc)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    //objTesoreriaData.ActualizarCorrelativoDocumentoBancos(anio, mes, cuenta, tipo_doc, nro_doc);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ETipoDocumento> getCorrelativoTipoDoc(int intTipoDoc, string strNroSerie)
        {
            try
            {
                var lst = new AdministracionSistemaData().getCorrelativoTipoDocumento(intTipoDoc, strNroSerie);
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AdministracionSistemaData().updateCorrelativoTipoDocumentoPVT(intTipoDocumento, intCorrelativo, intOpcion);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AdministracionSistemaData().updateCorrelativoTipoDocumentoSeries(serie, intCorrelativo);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    new AdministracionSistemaData().updateCorrelativoDocumentoSunat(icodParametro, intCorrelativo);
                    tx.Complete();
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoDocumento(serie, Tipo_docuemnto);
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCLMercaderia(serie);
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCL(serie);
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoRCS(serie);
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCS(serie);
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoOCI(serie);
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
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoProyecto(serie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        public int ObtenerCorrelativoTablaRegistro(int serie)
        {
            int NumDocumento = 0;
            try
            {
                NumDocumento = (new AdministracionSistemaData()).ObtenerCorrelativoTablaRegistro(serie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NumDocumento;
        }
        #endregion
        #region EnvioInformacion
        public void crearArchivoTransferencia(string ruta, List<ESendInfo> listSend, Object jonathan)
        {
            //jonathan. = 1;
            //jonathan.Properties.PercentView = true;
            //jonathan.Properties.Maximum = 10;
            //jonathan.Properties.Minimum = 0;
            //jonathan.GetType

            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {

                string activeDir = ruta;
                string newPath = System.IO.Path.Combine(activeDir, "mySubDir");
                System.IO.Directory.CreateDirectory(newPath);

                Random rA = new Random();
                FastZip zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
                zip.Password = "5384250";
                foreach (ESendInfo obj in listSend)
                {
                    if (obj.estado == true)
                    {
                        string direcc;
                        Random r = new Random();
                        int aleat = r.Next(100);
                        direcc = newPath + "\\" + aleat.ToString() + obj.trans_nombre_archivo;
                        new AdministracionSistemaData().crearArchivoTransferencia(direcc, obj.trans_nombre_tabla);
                    }
                }
                zip.CreateEmptyDirectories = true;
                zip.CreateZip(ruta + "\\" + rA.Next(1000000, 9999999) + ".zip", newPath + "\\", true, "\\.txt");
                Directory.Delete(newPath, true);
            }
        }

        public List<ESendInfo> ListarSendInfo()
        {
            List<ESendInfo> lista = null;
            try
            {
                lista = objAdministracioData.ListarSendInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void crearArchivo(List<EMenu> lista)
        {
            string ruta = "D:\\Edgar";
            StreamWriter fs = File.CreateText(ruta + "\\Formularios.txt");
            foreach (EMenu obj in lista)
            {
                fs.Write(fs.NewLine + obj.FullNameSpace + " % " + obj.FullDescription + " % " + obj.Description);
            }
            fs.Close();
        }
        #endregion
        #region Parametro Produccion
        public void modificarCorrelativoOP(EParametroProduccion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    (new AdministracionSistemaData()).modificarCorrelativoOP(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EParametroProduccion> listarParametroProduccion()
        {
            List<EParametroProduccion> lista = null;
            try
            {
                lista = objAdministracioData.listarParametroProduccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void modificarParametroProduccion(EParametroProduccion obj)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarParametroProduccion(obj);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Imagen Cheque
        public List<EImagenCheque> listarImagenesCheque(int mobac_icod_correlativo)
        {
            List<EImagenCheque> lista = null;
            try
            {
                lista = objAdministracioData.listarImagenCheque(mobac_icod_correlativo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public int insertarImagenCheque(EImagenCheque oBe)
        {
            int intIcod = 0;
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    intIcod = objAdministracioData.insertarImagenCheque(oBe);
                    tx.Complete();
                }
                return intIcod;
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.modificarImagenCheque(oBe);
                    tx.Complete();
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
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    objAdministracioData.eliminarImagenCheque(oBe);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<ComparacionKardexStock> ComparacionKardexStockListar(int anio)
        {
            return objAdministracioData.ComparacionKardexStockListar(anio);
        }

        public void ActualizarStock(DataTable tabla)
        {
            objCentralData.InsertMasivo(tabla, "SGE_ACTULIZAR_STOCK_PVT_CON_KARDEX_CALCULO");
        }
    }
}
