using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
//using OpenInvoicePeru.Comun.Dto.Modelos;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using SGE.WindowForms.Utilities;
using SGE.WindowForms.Ventas.Reporte;
//using SGE.WindowForms.Ventas.Reportes;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    public partial class frm02DocumentosAnulados : DevExpress.XtraEditors.XtraForm
    {
        private List<ESunatDocumentosBaja> lstfacturaElectronica = new List<ESunatDocumentosBaja>();
        private int xposition;
        private int intIndicador = 0;
        private List<string> mensajeRespuesta = new List<string>();
        List<EParametro> lstParametro = new List<EParametro>();
        public string CorrelativoTXT;
        public frm02DocumentosAnulados()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            //cargaLkpTipoDoc();
            //cargaEstadoEnvio();
            //cargaEstadoSunat();
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
            grdFacturaElectronica.DataSource = listaFacturaElectronica();
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
            //lst.Add(new TipoEstadoSunat { intCodigo = 1, strTipo = "ENVIADO SUNAT" });
            lst.Add(new TipoEstadoSunat { intCodigo = 2, strTipo = "ENVIADO" });
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
                if (Obe.tipoDocumento == "01")
                {
                    rptFcatura.cargar(Obe, mlisdet);
                    rptFcatura.ShowPreview();
                }
                if (Obe.tipoDocumento == "03")
                {
                    rptBoleta.cargar(Obe, mlisdet);
                    rptBoleta.ShowPreview();
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
            ESunatDocumentosBaja Obe = new ESunatDocumentosBaja();
            EFacturaElectronicaResponse response = new EFacturaElectronicaResponse();

            var HH = lstfacturaElectronica.Where(p => p.indicador_check /*&& p.EstadoEnvioInt != 2*/ && p.EstadoFacturacion != 2).ToList();
            if (lstfacturaElectronica.Where(p => p.indicador_check /*&& p.EstadoEnvioInt != 2*/ && p.EstadoFacturacion != 2).Any())
            {
                foreach (var item in lstfacturaElectronica.Where(p => p.indicador_check /*&& p.EstadoEnvioInt != 2*/ && p.EstadoFacturacion != 2))
                {
                    if (item.indicador_check)
                    {
                        Obe = lstfacturaElectronica.Where(p => p.IdCabecera == item.IdCabecera).FirstOrDefault();
                        mlisdet = new BVentas().listarfacturaVentaElectronicaDetalle(Obe.IdCabecera);

                        if (Obe.TipoDocumento == "01" || Obe.TipoDocumento == "03")
                        {
                            //response = await EviarFacturaElectronica(Obe, mlisdet);
                        }
                        else if (Obe.TipoDocumento == "07" || Obe.TipoDocumento == "08")
                        {
                            //response = await EviarNotaCredito(Obe, mlisdet);
                        }
                        
                        response.IdCabezera = Obe.IdCabecera;
                        int idResponse = new BVentas().insertarFacturaElectronicaResponse(response);
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
            SunatToolStripMenuItem.Enabled = !flag;
            mnuMarca.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.imprimir();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.imprimir();
        }

        #endregion Marca

        private void btnNuevo_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.imprimir();
        }

        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }

        private void BuscarCriterio()
        {
            //grdFacturaElectronica.DataSource = lstfacturaElectronica.Where(obj => obj.idDocumento.Contains(txtpresupuesto.Text) &&
            //                                    obj.nombreLegalReceptor.ToUpper().Contains(txtCliente.Text.ToUpper())).ToList();
            //if (lkpTipoDoc.Text == "TODOS" && txtpresupuesto.Text.Trim() == "" && txtCliente.Text.TrimStart().TrimEnd() == "")
            //{
            //    grdFacturaElectronica.DataSource = lstfacturaElectronica;
            //}
            //else
            //{
            //    List<EFacturaVentaElectronica> listaTempDxC = new List<EFacturaVentaElectronica>();
            //    if (lkpTipoDoc.Text != "TODOS")
            //        listaTempDxC = lstfacturaElectronica.Where(ob => ob.StrTipoDoc == lkpTipoDoc.Text).ToList();
            //    else
            //        listaTempDxC = lstfacturaElectronica;
            //    listaTempDxC = listaTempDxC.Where(ob => ob.idDocumento.Contains(txtpresupuesto.Text.TrimStart().TrimEnd())).ToList();
            //    listaTempDxC = listaTempDxC.Where(ob => ob.nombreLegalReceptor.Contains(txtCliente.Text.TrimStart().TrimEnd())).ToList();
            //    grdFacturaElectronica.DataSource = listaTempDxC;
            //}
        }

        private void confirmarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }



        private void grdFacturaElectronica_Click(object sender, EventArgs e)
        {
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
                    grdFacturaElectronica.DataSource = listaFacturaElectronica();
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

        private List<ESunatDocumentosBaja> listaFacturaElectronica()
        {
            DateTime FechaActual;
            List<EParametro> lstParametro = new BAdministracionSistema().listarParametro();
            lstfacturaElectronica = new List<ESunatDocumentosBaja>();
            lstfacturaElectronica = new BVentas().listarSunatDocumentosBajaCab(lstParametro[0].pm_sfecha_inicio);
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
            List<ESunatDocumentosBaja> listaTempEE = new List<ESunatDocumentosBaja>();
            listaTempEE = lstfacturaElectronica.Where(ob => /*ob.EstadoEnvio == lkpEstadoEnvio.Text &&*/ ob.EstadoSunat == lkpEstadoSunat.Text).ToList();
            grdFacturaElectronica.DataSource = listaTempEE;

            if (lkpEstadoSunat.Text == "ENVIADO")
            {
                SunatToolStripMenuItem.Enabled = false;
            }
            else
            {
                SunatToolStripMenuItem.Enabled = true;
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
            ESunatDocumentosBaja Obe = (ESunatDocumentosBaja)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            Obe.indicador_check = true;
            //if (Obe.EstadoSunat == "ENVIADO SUNAT")
            //{
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowEdit = false;
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowFocus = false;
            //}
            //else
            //{
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowEdit = true;
            //    viewFacturaElectronica.Columns[6].OptionsColumn.AllowFocus = true;
            //}
        }

        private void viewFacturaElectronica_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DateTime FechaActual;
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string FechaEmision = View.GetRowCellDisplayText(e.RowHandle, View.Columns["FEmisionPresentacion"]);
                string strEnvioSunat = View.GetRowCellDisplayText(e.RowHandle, View.Columns["EstadoSunat"]);
                if (/*String.Compare(strEnvio,"ENVIADO")==0 &&*/ String.Compare(strEnvioSunat, "ENVIADO") == 0)
                {
                    e.Appearance.BackColor = Color.LightGreen;

                }
                if (strEnvioSunat == "ENVIADO")
                {
                    SunatToolStripMenuItem.Enabled = false;
                }
                else
                {
                    SunatToolStripMenuItem.Enabled = true;
                    FechaActual = DateTime.Now;
                    TimeSpan ts = FechaActual - Convert.ToDateTime(FechaEmision);
                    int Diferencia = ts.Days;

                    if (Diferencia > 7)
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


            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);
            objdocumento.Discrepancias.Add(new Discrepancia
            {
                NroReferencia = Obe.numDocRef,
                Tipo = "01",
                Descripcion = "Ejemplo"
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

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            BuscarCriterio();
        }

        private  void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intIndicador = 1;//Indicador 1 = Generar
            enableLoading(true);
            AnularDocumentoElectronico();
        }


        private async void AnularDocumentoElectronico()
        {
            ESunatDocumentosBajaResponse response = new ESunatDocumentosBajaResponse();
            List<EDocumentoElectronicoBaja> objLista = new List<EDocumentoElectronicoBaja>();
            ESunatDocumentosBaja Obe = new ESunatDocumentosBaja();


            if (lstfacturaElectronica.Where(p => p.indicador_check && p.EstadoFacturacion != 2).Any())
            {
                foreach (var item in lstfacturaElectronica.Where(p => p.indicador_check && p.EstadoFacturacion != 2))
                {

                    if (item.indicador_check)
                    {
                        Obe = lstfacturaElectronica.Where(p => p.IdCabecera == item.IdCabecera).FirstOrDefault();

                        response = await EnviarBajaDocumentoElectronico(Obe);
                        mensajeRespuesta = new List<string>();
                        response.IdCabecera = item.IdCabecera;

                        if (response.Exito)
                        {
                            this.mensajeRespuesta.Add($"{item.Serie + item.Correlativo}; Se Envio correctamente.");

                        }
                        else
                        {
                            this.mensajeRespuesta.Add($"{item.Serie + item.Correlativo}; ocurrio un error.");
                        }
                        response.IdItems += $"{item.Serie + item.Correlativo},";


                        //int idResponse = new BVentas().insertarDocumentosBajaResponse(response);


                        //if (idResponse == 0)
                        //{
                        //    //new BVentas().modificarFacturaElectronicaResponse(item.IdCabecera);
                        //    //new BVentas().insertarDocumentosBajaResponse(response);
                        //}
                        if (response.Exito)
                        {
                            //new BVentas().actualizarFacturaElectronicaResponse(Obe.IdCabecera, (int)EstadoDocumento.enviadoSunat);
                            int codigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);
                            if (codigoRespuesta <= 0)
                            {
                                //new BVentas().actualizarDocumentosBajaResponse(Obe.IdCabecera, (int)EstadoDocumento.aprobado);
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
                            //new BVentas().actualizarDocumentosBajaResponse(Obe.IdCabecera, estadoSunat);
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

        public async Task<ESunatDocumentosBajaResponse> EnviarBajaDocumentoElectronico(ESunatDocumentosBaja Obe)
        {
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
            ComunicacionBaja objBaja = new ComunicacionBaja();

            objBaja = FacturaElectronicaDto.DocumentoElectronicoBaja(Obe);

            ESunatDocumentosBajaResponse response = await model.DocumentoElectronicoBaja(objBaja);

            return response;
        }

        //Envio de correo

        public void EnviarEmail()
        {
            string template = Convertir.CrearTemplateEmail();


            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(template,
                                        Encoding.UTF8,
                                        MediaTypeNames.Text.Html);

            var pathResource = string.Format(@"{0}Utilities\ImagenFirma\", AppDomain.CurrentDomain.BaseDirectory);

            string resourceId = "01-FIRMA";
            string urlResource = string.Format("{0}{1}.jpg", pathResource, resourceId);
            LinkedResource linkedResource = new LinkedResource(urlResource, MediaTypeNames.Image.Jpeg);
            linkedResource.ContentId = resourceId;
            htmlView.LinkedResources.Add(linkedResource);


            Attachment Attach_ = new Attachment(@"D:\Factura Electronica\20109714039-07-F005-00000006.pdf");

            Email.From("SIDEPERU@SIDE.somee.com")
                           .To("jfernandez-20@hotmail.com") //jferna
                           .CarbonCopy("jfernandez-20@hotmail.com")
                           .Subject("Ejemplo")
                           .AlternateView(htmlView)
                           // .Attach(Attach_)
                           //.UsingStringTemplateText("Mensaje de Prueba")
                           .Send();
        }

        private void enviarDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ESunatDocumentosBaja obe = (ESunatDocumentosBaja)viewFacturaElectronica.GetRow(viewFacturaElectronica.FocusedRowHandle);
            if (obe == null)
                return;
            lstParametro = new BAdministracionSistema().listarParametro();
            
            try
            {
                if (obe.EstadoFacturacion == 2)
                {
                    throw new ArgumentException("Documento Ya se Encuentra Enviado");
                }

                if (obe.CodigoSunat == "")
                {
                    throw new ArgumentException("Documento no contiene Codigo de SUNAT");
                }

                var lst = new BAdministracionSistema().listarParametro();
                if (Convert.ToInt32(lst.Count()) > 0)
                {
                    CorrelativoTXT = String.Format("{0:0000000000}", Convert.ToInt32(lst[0].pm_correlativo_doc_sunat) + 1);
                }

                string Nombre = Valores.strRUC + obe.CodigoSunat + "ANU" + CorrelativoTXT + String.Format("{0:00}", Convert.ToDateTime(obe.FechaEmision).Day) + String.Format("{0:00}", Convert.ToDateTime(obe.FechaEmision).Month) + Convert.ToDateTime(obe.FechaEmision).Year + obe.TipoDocumento.Trim() + "210";

                string fileName = lstParametro[0].DirecciónXML + Nombre;
                if (!fileName.Contains(".txt"))
                {
                    ExportarATXT(obe, fileName + ".txt");
                    new BAdministracionSistema().updateCorrelativoDocumentoSunat(lst[0].pm_icod_parametro, Convert.ToInt32(CorrelativoTXT));
                    new BVentas().actualizarDocumentoBajaCorrelaticoTXT(obe.IdCabecera, Convert.ToInt32(CorrelativoTXT));
                    new BVentas().actualizarDocumentosBajaResponse(obe.IdCabecera, 2);
                    cargar();
                    filtroBuscarGeneral();

                }
                else
                {
                    ExportarATXT(obe, fileName);
                    new BAdministracionSistema().updateCorrelativoDocumentoSunat(lst[0].pm_icod_parametro, Convert.ToInt32(CorrelativoTXT));
                    new BVentas().actualizarDocumentoBajaCorrelaticoTXT(obe.IdCabecera, Convert.ToInt32(CorrelativoTXT));
                    new BVentas().actualizarDocumentosBajaResponse(obe.IdCabecera, 1);
                    cargar();
                    filtroBuscarGeneral();
                }
                sfdTXT.FileName = string.Empty;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportarATXT(ESunatDocumentosBaja oBe, String ruta)
        {
            StreamWriter sw = new StreamWriter(ruta);
            string error = string.Empty;
            int cont = 0;
            string columna = string.Empty;
            try
            {
                //cont++;
                columna = "1";
                sw.Write("210|");
                columna = "2";
                sw.Write(CorrelativoTXT + "|");
                columna = "3";
                sw.Write(oBe.TipoDocumento.Trim() + "|");
                columna = "4";
                sw.Write(Valores.strRUC + "|");               
                columna = "5";
                sw.Write(oBe.CodigoSunat + "|");
                columna = "6";
                sw.Write(oBe.FechaEmision + " " + oBe.horaEmision + "|");
                columna = "7";
                sw.Write(oBe.Serie + "|");
                columna = "8";
                sw.Write(oBe.Correlativo + "|");
                columna = "9";
                sw.Write(oBe.UsuarioOSE + "|");
                columna = "10";
                sw.Write(oBe.MotivoBaja + "|");
                sw.WriteLine();

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message + "\nFila " + cont + "\nDocumento " + error + "\nColumna Nº " + columna);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}