using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Ventas.Reporte;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    public partial class frm01DocumentosEmitidos : DevExpress.XtraEditors.XtraForm
    {
        private List<EFacturaVentaElectronica> lstfacturaElectronica = new List<EFacturaVentaElectronica>();
        private int xposition;
        private List<string> mensajeRespuesta = new List<string>();
        private int intIndicador = 0;
        List<EParametro> lstParametro = new List<EParametro>();
        public string CorrelativoTXT;
        public frm01DocumentosEmitidos()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            if (chkTodos.Checked == true)
            {
                cargar();
                lkpEstadoSunat.Properties.DataSource = 0;
            }
            else
            {
                cargaEstadoSunat();
                filtroBuscarGeneral();
            }
        }

        private void cargar()
        {
            grdFacturaElectronica.DataSource = listaFacturaElectronica().Where(x => x.EstadoFacturacion != 0);
            grdFacturaElectronica.RefreshDataSource();
            viewFacturaElectronica.Focus();

        }
        //private void cargaLkpTipoDoc()
        //{
        //    List<string> listaTD = new List<string>();
        //    listaTD = lstfacturaElectronica.OrderBy(ord => ord.StrTipoDoc).Select(sel => sel.StrTipoDoc).Distinct().ToList();
        //    listaTD.Add("TODOS");
        //    lkpTipoDoc.Properties.DataSource = listaTD;
        //    lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        //}
        public class TipoEnvioSunat
        {
            public int intCodigo { get; set; }
            public string strTipo { get; set; }
        }
        private void cargaEstadoSunat()
        {
            List<TipoEstadoSunat> lst = new List<TipoEstadoSunat>();
            lst.Add(new TipoEstadoSunat { intCodigo = 1, strTipo = "ENVIADO SUNAT" });
            lst.Add(new TipoEstadoSunat { intCodigo = 2, strTipo = "APROBADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 3, strTipo = "RECHAZADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 4, strTipo = "PENDIENTES POR ENVIAR" });
            //lst.Add(new TipoEstadoSunat { intCodigo = 0, strTipo = "PENDIENTES POR ENVIAR" });
            BSControls.LoaderLook(lkpEstadoSunat, lst, "strTipo", "intCodigo", true);
            lkpEstadoSunat.ItemIndex = lst.Count - 1;
        }
        public class TipoEstadoSunat
        {
            public int intCodigo { get; set; }
            public string strTipo { get; set; }
        }
        #region Marca
        private void Agregar(int cod)
        {
            cargar();
            viewFacturaElectronica.FocusedRowHandle = lstfacturaElectronica.Count - 1;
            viewFacturaElectronica.Focus();
        }

        private void reload(int cod)
        {
            cargar();
            viewFacturaElectronica.FocusedRowHandle = xposition;
            viewFacturaElectronica.Focus();
        }


        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
        }
        private void imprimir()
        {
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            Obe = (EFacturaVentaElectronica)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            if (Obe != null)
            {
                mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);
                rptFacturaElectronico rptFcatura = new rptFacturaElectronico();
                rptBoletaElectronico rptBoleta = new rptBoletaElectronico();
                rptNotaCreditoElectronico rptNotaCredito = new rptNotaCreditoElectronico();
                if (Obe.tipoDocumento == "01")
                {
                    List<EFacturaCab> lstFV = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_icod_factura == Obe.doc_icod_documento).ToList();
                    if (lstFV.Count > 0)
                    {
                        Obe.strTelefonoCliente = lstFV[0].strTelefonoCliente;
                    }
                    Obe.cuotas = new List<ECuotasFactura>();
                    Obe.cuotas = new BVentas().CuotasFacturaListar(lstFV[0].doxcc_icod_correlativo);
                    Obe.PorcRetension = Convert.ToDecimal(lstFV[0].facv_nporc_retencion);
                    Obe.MontoRetension = Convert.ToDecimal(lstFV[0].facv_nmonto_retencion);
                    rptFcatura.cargar(Obe, mlisdet);
                    rptFcatura.ShowPreview();
                }
                if (Obe.tipoDocumento == "03")
                {
                    List<EBoletaCab> lstBV = new BVentas().listarBoletaCab(Parametros.intEjercicio).Where(x => x.bovc_icod_boleta == Obe.doc_icod_documento).ToList();
                    if (lstBV.Count > 0)
                    {
                        Obe.strTelefonoCliente = lstBV[0].strTelefonoCliente;
                    }
                    rptBoleta.cargar(Obe, mlisdet);
                    rptBoleta.ShowPreview();
                }
                if (Obe.tipoDocumento == "07")
                {
                    List<ENotaCredito> lstNC = new BVentas().listarNotaCreditoClienteCab(Parametros.intEjercicio).Where(x => x.ncrec_icod_credito == Obe.doc_icod_documento).ToList();
                    if (lstNC.Count > 0)
                    {

                    }

                    rptNotaCredito.cargar(Obe, mlisdet);
                    rptNotaCredito.ShowPreview();
                }
            }
        }


        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intIndicador = 1;//Indicador 1 = Generar
            enableLoading(true);
            EnviarSunat();

        }

        private async void EnviarSunat()
        {
            List<EFacturaVentaDetalleElectronico> mlisdet = new List<EFacturaVentaDetalleElectronico>();
            EFacturaVentaElectronica Obe = new EFacturaVentaElectronica();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            var HH = lstfacturaElectronica.Where(p => p.indicador_check /*&& p.EstadoEnvioInt != 2*/ && p.EstadoFacturacion != 2).ToList();
            if (lstfacturaElectronica.Where(p => p.indicador_check /*&& p.EstadoEnvioInt != 2*/ && p.EstadoFacturacion != 2).Any())
            {
                mensajeRespuesta = new List<string>();
                foreach (var item in lstfacturaElectronica.Where(p => p.indicador_check /*&& p.EstadoEnvioInt != 2*/ && p.EstadoFacturacion != 2))
                {
                    if (item.indicador_check)
                    {
                        Obe = lstfacturaElectronica.Where(p => p.IdCabecera == item.IdCabecera).FirstOrDefault();
                        mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera).Where(x => x.ValorVentaItem != Convert.ToDecimal(0.01)).ToList();

                        if (Obe.tipoDocumento == "01" || Obe.tipoDocumento == "03")
                        {
                            response = await EviarFacturaElectronica(Obe, mlisdet);
                        }
                        else if (Obe.tipoDocumento == "07" || Obe.tipoDocumento == "08")
                        {
                            response = await EviarNotaCredito(Obe, mlisdet);
                        }
                        //Mensaje de respuesta 

                        if (response.Exito)
                        {
                            mensajeRespuesta.Add($"{Obe.idDocumento};Se envio correctamente.");
                        }
                        else
                        {
                            mensajeRespuesta.Add($"{Obe.idDocumento};Ocurrio un errror, para mas detalle filtar por la opcion rechazados.");
                        }

                        response.IdCabezera = Obe.IdCabecera;
                        int idResponse = new BVentas().insertarFacturaElectronicaResponse(response);
                        if (idResponse == 0)
                        {
                            new BVentas().modificarFacturaElectronicaResponse(item.IdCabecera);
                            new BVentas().insertarFacturaElectronicaResponse(response);
                        }
                        if (response.Exito)
                        {
                            new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.enviadoSunat);
                            int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                            if (codigoRespuesta <= 0)
                            {
                                new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.aprobado);
                            }
                        }
                        else
                        {
                            int estadoSunat = 0;

                            int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                            if (codigoRespuesta >= (int)EstadoDocumento.rangoExcepcionMin &&
                                codigoRespuesta <= (int)EstadoDocumento.rangoExcepcionMax)
                            {
                                /*Vuelve al estado de pendientes por enviar para ser modificado*/
                                estadoSunat = (int)EstadoDocumento.rechazado;
                            }
                            else if (codigoRespuesta >= (int)EstadoDocumento.rangoRechazadoMin &&
                                codigoRespuesta <= (int)EstadoDocumento.rangoRechazadoMax)
                            {
                                estadoSunat = (int)EstadoDocumento.rechazado;
                            }
                            else if (codigoRespuesta >= (int)EstadoDocumento.rangoObservadoMax)
                            {
                                /*Vuelve al estado de pendientes por enviar para ser modificado*/
                                estadoSunat = (int)EstadoDocumento.rechazado;
                            }

                            new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, estadoSunat);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione registro(s)", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            backgroundWorker1.RunWorkerAsync();
        }

        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;
            enviarDocumentoToolStripMenuItem.Enabled = !flag;
            mnuMarca.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.imprimir();
        }


        #endregion Marca


        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.imprimir();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            intIndicador = 1;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (intIndicador == 1)
                {
                    enableLoading(false);
                    grdFacturaElectronica.DataSource = listaFacturaElectronica().Where(x => x.EstadoFacturacion != 0);
                    viewFacturaElectronica.RefreshData();
                    grdFacturaElectronica.RefreshDataSource();
                    if (chkTodos.Checked == true)
                    {
                        cargar();
                        lkpEstadoSunat.Properties.DataSource = 0;
                    }
                    else
                    {
                        cargaEstadoSunat();
                        filtroBuscarGeneral();
                    }
                }
                RespuestaSunat form = new RespuestaSunat();
                form.ListaMensajeRespuesta = this.mensajeRespuesta;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Infomarción del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        }

        private List<EFacturaVentaElectronica> listaFacturaElectronica()
        {
            DateTime FechaActual;
            List<EParametro> lstParamatro = new BAdministracionSistema().listarParametro();
            lstfacturaElectronica = new List<EFacturaVentaElectronica>();
            lstfacturaElectronica = new BVentas().listarfacturaVentaElectronicaFecha(lstParamatro[0].pm_sfecha_inicio).Where(x => x.EstadoFacturacion != 0).ToList();
            foreach (var item in lstfacturaElectronica)
            {
                FechaActual = DateTime.Now;
                TimeSpan ts = FechaActual - Convert.ToDateTime(item.FEmisionPresentacion);
                int Dias = ts.Days;
                item.Dias = Dias;
                if (item.EstadoFacturacion == (int)EstadoDocumento.aprobado)
                {
                    item.indicador_check = true;
                    item.Dias = 0;
                }
                if (item.EstadoFacturacion == (int)EstadoDocumento.enviadoSunat)
                {
                    item.indicador_check = true;
                    item.Dias = 0;
                }
            }

            return lstfacturaElectronica;
        }


        private void filtroBuscarGeneral()
        {
            List<EFacturaVentaElectronica> listaTempEE = new List<EFacturaVentaElectronica>();
            listaTempEE = lstfacturaElectronica.Where(ob => /*ob.EstadoEnvio == lkpEstadoEnvio.Text &&*/ ob.EstadoSunat == lkpEstadoSunat.Text).ToList();
            grdFacturaElectronica.DataSource = listaTempEE;

            if (lkpEstadoSunat.Text == "ENVIADO" /*&& lkpEstadoEnvio.Text == "APROBADO"*/)
            {
                enviarDocumentoToolStripMenuItem.Enabled = false;
            }
            else
            {
                enviarDocumentoToolStripMenuItem.Enabled = true;
            }
            //}
        }



        private void lkpEstadoSunat_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpEstadoSunat.ContainsFocus)
                filtroBuscarGeneral();
        }

        private void repositoryItemCheckEdit1_Click(object sender, EventArgs e)
        {
            EFacturaVentaElectronica Obe = (EFacturaVentaElectronica)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            Obe.indicador_check = true;
        }

        private void viewFacturaElectronica_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DateTime FechaActual;
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string FechaEmision = View.GetRowCellDisplayText(e.RowHandle, View.Columns["FEmisionPresentacion"]);
                string strEnvioSunat = View.GetRowCellDisplayText(e.RowHandle, View.Columns["EstadoSunat"]);
                if (String.Compare(strEnvioSunat, "APROBADO") == 0)
                {
                    e.Appearance.BackColor = Color.LightGreen;

                }
                if (strEnvioSunat == "APROBADO")
                {
                    enviarDocumentoToolStripMenuItem.Enabled = false;
                }
                else
                {
                    enviarDocumentoToolStripMenuItem.Enabled = true;
                    FechaActual = DateTime.Now;
                    TimeSpan ts = FechaActual - Convert.ToDateTime(FechaEmision);
                    int Diferencia = ts.Days;

                    if (Diferencia > 3)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                    }
                }


            }
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked == true)
            {
                cargar();
                lkpEstadoSunat.Properties.DataSource = 0;
            }
            else
            {
                cargaEstadoSunat();
                filtroBuscarGeneral();
            }
        }

        #region Metodos de documentos electronicos

        //Facturacion
        public async Task<EFacturaElectronicaResponse> EviarFacturaElectronica(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
            List<EFacturaCab> lstFV = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_icod_factura == Obe.doc_icod_documento).ToList();
            Obe.cuotas = new List<ECuotasFactura>();
            Obe.cuotas = new BVentas().CuotasFacturaListar(lstFV[0].doxcc_icod_correlativo);
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.DatosGuiaTransportista = null;
            objdocumento.DatoAdicionales = new List<DatoAdicional>();
            objdocumento.Relacionados = new List<DocumentoRelacionado>();
            objdocumento.OtrosDocumentosRelacionados = new List<DocumentoRelacionado>();
            objdocumento.Discrepancias = new List<Discrepancia>();

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);

            EFacturaElectronicaResponse response = await model.FacturaElectronica(objdocumento);

            return response;
        }


        public async Task<EFacturaElectronicaResponse> EviarNotaCredito(EFacturaVentaElectronica Obe, List<EFacturaVentaDetalleElectronico> mlisdet)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
            EFacturaElectronicaResponse response = null;
            DocumentoElectronico objdocumento = new DocumentoElectronico();
            objdocumento.DatosGuiaTransportista = null;
            objdocumento.DatoAdicionales = new List<DatoAdicional>();
            objdocumento.Relacionados = new List<DocumentoRelacionado>();
            objdocumento.OtrosDocumentosRelacionados = new List<DocumentoRelacionado>();


            objdocumento = FacturaElectronicaDto.ModelDtoNC(Obe, mlisdet);
            objdocumento.Discrepancias.Add(new Discrepancia
            {
                CodMotivoNota = Obe.CodMotivoNota.Trim(),
                //CodMotivoNota = "06",
                DescripMotivoNota = Obe.DescripMotivoNota,
                NroReferencia = Obe.NroDocqModifica.Remove(4, 8) + '-' + Obe.NroDocqModifica.Remove(0, 4),


                Descripcion = "",
                Tipo = ""
            });
            objdocumento.Relacionados.Add(new DocumentoRelacionado
            {
                NroDocqModifica = Obe.NroDocqModifica.Remove(4, 8) + '-' + Obe.NroDocqModifica.Remove(0, 4),
                TipoDocqModifica = Obe.TipoDocqModifica,

                NroDocumento = "",
                TipoDocumento = ""
            });
            if (objdocumento.TipoDocumento == "07")
            {
                response = await model.NotaCreditoElectronica(objdocumento);
            }
            else
            {
                response = await model.NotaDebitoElectronica(objdocumento);
            }

            return response;
        }
        #endregion

        private void descagarCDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Obe = (EFacturaVentaElectronica)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            if (Obe is null)
                return;
            if (string.IsNullOrEmpty(Obe.TramaZipCdr))
                return;
            var parametros = new BAdministracionSistema().listarParametro().First();
            string nombre = $"{parametros.Ruc}-{Obe.tipoDocumento}-{Obe.idDocumento}";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = nombre;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes($"{saveFileDialog.FileName}.zip", Convert.FromBase64String(Obe.TramaZipCdr));
                XtraMessageBox.Show("Se descargó el CDR", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void grdFacturaElectronica_Click(object sender, EventArgs e)
        {

        }
    }
}