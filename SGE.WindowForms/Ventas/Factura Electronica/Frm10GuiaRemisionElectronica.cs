using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.DataAccess;
using SGE.Entity;
using SGE.Entity.FacturaElectronica;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas;
using SIDE.COMUN.DTO.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    public partial class Frm10GuiaRemisionElectronica : DevExpress.XtraEditors.XtraForm
    {
        List<EGuiaRemision> lstFacturas = new List<EGuiaRemision>();
        DateTime FechaActual;
        public Frm10GuiaRemisionElectronica()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            var parametros = new BAdministracionSistema().listarParametro().First();
            lstFacturas = new BVentas().listarGuiaRemisionElectronica(parametros.pm_sfecha_inicio);
            grdGuiaRemision.DataSource = lstFacturas;
            foreach (var item in lstFacturas)
            {
                FechaActual = DateTime.Now;
                TimeSpan ts = FechaActual - Convert.ToDateTime(item.remic_sfecha_remision);
                int Dias = ts.Days;
                item.Dias = Dias;
                if (item.Exito)
                {

                    item.Dias = 0;
                }

            }


        }

        void reload(int intIcod)
        {
            lkpEstadoSunat_EditValueChanged(null, null);
            int index = lstFacturas.FindIndex(x => x.remic_icod_remision == intIcod);
            viewGuiaRemision.FocusedRowHandle = index;
            viewGuiaRemision.Focus();
        }




        private void Frm04Factura_Load(object sender, EventArgs e)
        {
            cargarLkp();

        }

        void cargarLkp()
        {
            List<TipoEstadoSunat> lst = new List<TipoEstadoSunat>();
            lst.Add(new TipoEstadoSunat { intCodigo = 2, strTipo = "ACEPTADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 6, strTipo = "DE BAJA" });
            lst.Add(new TipoEstadoSunat { intCodigo = 3, strTipo = "RECHAZADO" });
            lst.Add(new TipoEstadoSunat { intCodigo = 4, strTipo = "PENDIENTE ENVIAR" });
            lst.Add(new TipoEstadoSunat { intCodigo = 5, strTipo = "PENDIENTE DE VALIDAR" });

            BSControls.LoaderLook(lkpEstadoSunat, lst, "strTipo", "intCodigo", true);
            lkpEstadoSunat.ItemIndex = lst.Count - 2;
        }
        public class TipoEstadoSunat
        {
            public int intCodigo { get; set; }
            public string strTipo { get; set; }
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }



        private void eliminar()
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                EGuiaRemision _BEguiaConstatar = new BVentas().listarGuiaRemisionxID(obe.remic_icod_remision);
                if (_BEguiaConstatar.tablc_iid_situacion_remision != 218)
                {
                    throw new ArgumentException(String.Format("La Guia de Remisión no puede ser eliminada, su situación es {0}", _BEguiaConstatar.StrSitucion));
                    int xPosition = viewGuiaRemision.FocusedRowHandle;
                    cargar();
                    viewGuiaRemision.FocusedRowHandle = xPosition;
                }

                if (XtraMessageBox.Show("¿Esta seguro que desea ELIMINAR la Guía de Remisión?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    List<EGuiaRemisionDet> lstDetalle = new List<EGuiaRemisionDet>();
                    lstDetalle = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);
                    new BVentas().eliminarGuiaRemision(obe, lstDetalle);
                    reload(obe.remic_icod_remision);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }



        private void viewGuiaRemision_DoubleClick(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {

                frmManteGuiaRemision frm = new frmManteGuiaRemision();
                frm.MiEvento += new frmManteGuiaRemision.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.Show();
                frm.CargarControles();

                frm.SetCancel();

                frm.btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void viewGuiaRemision_RowStyle(object sender, RowStyleEventArgs e)
        {
            DateTime FechaActual;
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["EstadoSunat"]);
                string FechaEmision = View.GetRowCellDisplayText(e.RowHandle, View.Columns["remic_sfecha_remision"]);
                if (strSituacion == "ACEPTADO")
                {
                    e.Appearance.BackColor = Color.LightGreen;

                }
                else
                {

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

        private async void enviarASunatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            enableLoading(true);
            var Obe = new VentasData().GetByIDGuiaRemisionElectronica(obe.remic_icod_remision);
            var mlisdet = new VentasData().GetByIDGuiaRemisionElectronicaDet(obe.remic_icod_remision);

            if (obe.remic_itipo_guia == 1)
                Obe.ubiPartida = new BCentral().ListarProdAlmacen().Where(x => x.id_almacen == obe.almac_icod_almacen).First().codigo_ubigeo;
            else
                Obe.ubiPartida = new BAlmacen().listarAlmacenes().Where(x => x.almac_icod_almacen == obe.almac_icod_almacen).First().almac_vubigeo;

            var model = new DocumentoElectronicoResponse();
            var objdocumento = new GuiaRemision();
            objdocumento.DireccionLlegada = new Direccion();
            objdocumento.DireccionPartida = new Direccion();
            objdocumento.Remitente = new Contribuyente();
            objdocumento.Destinatario = new Contribuyente();
            objdocumento.BienesATransportar = new List<DetalleGuia>();
            if (Obe.ModalidadTraslado == "02")
            {
                var conductor = new BVentas().ConductorGet(obe.remic_icod_remision);
                Obe.NroDocumentoConductor = $"{conductor.cod_vdni}-{conductor.cod_vnombres}-{conductor.cod_vapellidos}-{conductor.cod_vlicencia}";
            }

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);
            var response = await model.GuiaRemisionElectronica(objdocumento);

            var responseGRE = new EGuiaRemisionElectronicaResponse();
            responseGRE.remic_icod_remision = obe.remic_icod_remision;
            responseGRE.FechaEnvio = DateTime.Now;
            responseGRE.Exito = response.Exito;
            responseGRE.MensajeError = response.MensajeError;
            responseGRE.MensajeRespuesta = response.MensajeRespuesta;
            responseGRE.NumeroTicket = response.NroTicketCdr;
            responseGRE.LinkDescarga = response.Urldescarga;
            Obe.EstadoEnvio = response.CodigoRespuesta is null ? 4 : Convert.ToInt32(response.CodigoRespuesta);
            new VentasData().GuiaRemisionElectronicaResponseGuardar(responseGRE);

            if (response.Exito)
            {
                if (Convert.ToInt32(response.CodigoRespuesta) == 0)
                {
                    new VentasData().GuiaRemisionElectronicaCambiarEstado(responseGRE.remic_icod_remision, Constantes.PendienteValidar);
                    XtraMessageBox.Show($"La GRE {objdocumento.IdDocumento} se Envió Conrrectamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    new VentasData().GuiaRemisionElectronicaCambiarEstado(responseGRE.remic_icod_remision, Constantes.Rechazado);
                    XtraMessageBox.Show($"{responseGRE.MensajeRespuesta}", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                new VentasData().GuiaRemisionElectronicaCambiarEstado(responseGRE.remic_icod_remision, Constantes.Rechazado);
                XtraMessageBox.Show($"{responseGRE.MensajeError}", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }



            backgroundWorker1.RunWorkerAsync();
            reload(responseGRE.remic_icod_remision);
        }






        private void imprimirElectrónicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.remic_itipo_guia == 1)
            {
                var lstdet = new BVentas().listarGuiaRemisionDet(obe.remic_icod_remision, Parametros.intEjercicio);


                List<EGuiaRemisionDet> mlistDetalle = new List<EGuiaRemisionDet>();
                int cont = 1;
                foreach (var _BE in lstdet)
                {
                    mlistDetalle.Add(_BE);

                    string[] partes = partes = _BE.dremc_vobservaciones.Split('@');
                    int cont_estapacios = 0;
                    for (int i = 0; i < partes.Length; i++)
                    {
                        if (partes[i] == "")
                        {
                            cont_estapacios = cont_estapacios + 1;
                        }
                    }
                    if (cont_estapacios != partes.Length)
                    {
                        for (int i = 0; i < partes.Length; i++)
                        {
                            if (i == 0)
                            {
                                EGuiaRemisionDet __be = new EGuiaRemisionDet();
                                __be.strDesProducto = "" + partes[i];
                                __be.OrdenItemImprimir = cont + 1;
                                cont++;
                                mlistDetalle.Add(__be);
                            }
                            else
                            {
                                if (partes[i] != "")
                                {
                                    EGuiaRemisionDet __be = new EGuiaRemisionDet();
                                    __be.strDesProducto = "" + partes[i];
                                    __be.OrdenItemImprimir = cont + 1;
                                    cont++;
                                    mlistDetalle.Add(__be);
                                }
                            }
                        }
                    }

                }


                foreach (EGuiaRemisionDet conve in lstdet)
                {
                    int[] tallas = new int[10];
                    int[] Cantidad = new int[10];
                    tallas[0] = Convert.ToInt32(conve.dremc_talla1);
                    tallas[1] = Convert.ToInt32(conve.dremc_talla2);
                    tallas[2] = Convert.ToInt32(conve.dremc_talla3);
                    tallas[3] = Convert.ToInt32(conve.dremc_talla4);
                    tallas[4] = Convert.ToInt32(conve.dremc_talla5);
                    tallas[5] = Convert.ToInt32(conve.dremc_talla6);
                    tallas[6] = Convert.ToInt32(conve.dremc_talla7);
                    tallas[7] = Convert.ToInt32(conve.dremc_talla8);
                    tallas[8] = Convert.ToInt32(conve.dremc_talla9);
                    tallas[9] = Convert.ToInt32(conve.dremc_talla10);
                    Cantidad[0] = Convert.ToInt32(conve.dremc_cant_talla1);
                    Cantidad[1] = Convert.ToInt32(conve.dremc_cant_talla2);
                    Cantidad[2] = Convert.ToInt32(conve.dremc_cant_talla3);
                    Cantidad[3] = Convert.ToInt32(conve.dremc_cant_talla4);
                    Cantidad[4] = Convert.ToInt32(conve.dremc_cant_talla5);
                    Cantidad[5] = Convert.ToInt32(conve.dremc_cant_talla6);
                    Cantidad[6] = Convert.ToInt32(conve.dremc_cant_talla7);
                    Cantidad[7] = Convert.ToInt32(conve.dremc_cant_talla8);
                    Cantidad[8] = Convert.ToInt32(conve.dremc_cant_talla9);
                    Cantidad[9] = Convert.ToInt32(conve.dremc_cant_talla10);
                    for (int i = 0; i < 10; i++)
                    {
                        if (Cantidad[i] != 0)
                        {
                            conve.TallaAcumulada = conve.TallaAcumulada + " - " + tallas[i].ToString() + "/" + Cantidad[i].ToString();
                        }
                    }
                }
                rptGRE rpt = new rptGRE();
                rpt.cargar(obe, mlistDetalle);
            }
            else
            {
                var lstdet = new BVentas().listarGuiaRemisionMPDet(obe.remic_icod_remision, Parametros.intEjercicio);
                rptGRESuministros rpt = new rptGRESuministros();
                rpt.cargar(obe, lstdet);
            }
        }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            enviarASunatToolStripMenuItem.Visible = obe.EstadoSunat == "PENDIENTE ENVIAR" ? true : obe.EstadoSunat == "RECHAZADO" ? true : false;
            descargarCDRToolStripMenuItem.Visible = obe.EstadoSunat != "PENDIENTE ENVIAR" ? true : false;
            darDeBajaToolStripMenuItem.Visible = obe.EstadoSunat == "ACEPTADO";
        }

        private void grdGuiaRemision_Click(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            enableLoading(false);
        }
        private void enableLoading(bool flag)
        {
            picLoading.Visible = flag;

            mnuMarca.Enabled = !flag;
            if (flag)
                picLoading.Dock = DockStyle.Fill;
            else
                picLoading.Dock = DockStyle.None;
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
                cargarLkp();

            }

        }

        private void lkpEstadoSunat_EditValueChanged(object sender, EventArgs e)
        {

            cargar();
            grdGuiaRemision.DataSource = lstFacturas.Where(x => x.EstadoSunat == lkpEstadoSunat.Text);
            grdGuiaRemision.RefreshDataSource();

        }

        private async void descargarCDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.EstadoSunat == "PENDIENTE ENVIAR")
                return;
            var parametro = new BAdministracionSistema().listarParametro().FirstOrDefault();
            var ObeE = new VentasData().GetByIDGuiaRemisionElectronica(obe.remic_icod_remision);
            string nombreArchivo = $"{parametro.Ruc}-{ObeE.TipoDocumento}-{ObeE.IdDocumento}.zip";


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = nombreArchivo;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                enableLoading(true);
                if (obe.EstadoSunat == "PENDIENTE DE VALIDAR")
                {
                    var model = new DocumentoElectronicoResponse();
                    var response = await model.DescargarCDR(obe.NumeroTicket);
                    var responseGRE = new EGuiaRemisionElectronicaResponse();
                    responseGRE.Exito = response.Exito;
                    responseGRE.MensajeError = response.MensajeError;
                    responseGRE.MensajeRespuesta = response.MensajeRespuesta;
                    responseGRE.NumeroTicket = response.NroTicketCdr;
                    responseGRE.LinkDescarga = response.Urldescarga;
                    responseGRE.CodigoRespuesta = Convert.ToInt32(response.CodigoRespuesta);

                    if (response.TramaZipCdr.Contains("{\"codRespuesta\":\"98\"}"))
                    {
                        XtraMessageBox.Show("El Documento Electrónico está en Proceso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        backgroundWorker1.RunWorkerAsync();
                        reload(obe.remic_icod_remision);
                        return;
                    }

                    new VentasData().GuiaRemisionElectronicaResponseModificar(responseGRE);
                    if (Convert.ToInt32(response.CodigoRespuesta) == 0)
                    {
                        new VentasData().GuiaRemisionElectronicaCambiarEstado(obe.remic_icod_remision, Constantes.Aceptado);
                        string rutaArchivoEnviado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nombreArchivo}");
                        if (File.Exists(rutaArchivoEnviado))
                            File.Delete(rutaArchivoEnviado);
                        File.WriteAllBytes($"{rutaArchivoEnviado}", Convert.FromBase64String(response.TramaZipCdr));
                        WebDavTest.Put($"{parametro.DirecciónXML}{Rutas.CarpataGRE}", $"{nombreArchivo}", rutaArchivoEnviado);
                        XtraMessageBox.Show(response.MensajeRespuesta, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        WebDavTest.Descargar($"{parametro.DirecciónXML}{Rutas.CarpataGRE}{nombreArchivo}", saveFileDialog.FileName);
                        Process.Start(saveFileDialog.FileName.Replace($"\\{nombreArchivo}", ""));
                    }
                    else
                    {
                        new VentasData().GuiaRemisionElectronicaCambiarEstado(obe.remic_icod_remision, Constantes.Rechazado);
                        XtraMessageBox.Show(response.MensajeRespuesta, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    WebDavTest.Descargar($"{parametro.DirecciónXML}{Rutas.CarpataGRE}{nombreArchivo}", saveFileDialog.FileName);
                    Process.Start(saveFileDialog.FileName.Replace($"\\{nombreArchivo}", ""));
                }
            }

            backgroundWorker1.RunWorkerAsync();
            reload(obe.remic_icod_remision);
        }

        private async void descargarXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            enableLoading(true);
            var Obe = new VentasData().GetByIDGuiaRemisionElectronica(obe.remic_icod_remision);
            var mlisdet = new VentasData().GetByIDGuiaRemisionElectronicaDet(obe.remic_icod_remision);
            Obe.ubiLlegada = new VentasData().ListarCliente().Where(x => x.cliec_icod_cliente == obe.cliec_icod_cliente).First().cliec_vubigeo;
            if (string.IsNullOrEmpty(Obe.ubiLlegada))
            {
                XtraMessageBox.Show("Ingrese el Codigo de Ubigeo del Cliente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                backgroundWorker1.RunWorkerAsync();
                return;
            }
            Obe.ubiPartida = new BCentral().ListarProdAlmacen().Where(x => x.id_almacen == obe.almac_icod_almacen).First().codigo_ubigeo;
            if (string.IsNullOrEmpty(Obe.ubiPartida))
            {
                XtraMessageBox.Show("Ingrese el Codigo de Ubigeo del Almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                backgroundWorker1.RunWorkerAsync();
                return;
            }
            DocumentoElectronicoResponse model = new DocumentoElectronicoResponse();
            GuiaRemision objdocumento = new GuiaRemision();
            objdocumento.DireccionLlegada = new Direccion();
            objdocumento.DireccionPartida = new Direccion();
            objdocumento.Remitente = new Contribuyente();
            objdocumento.Destinatario = new Contribuyente();
            objdocumento.BienesATransportar = new List<DetalleGuia>();

            objdocumento = FacturaElectronicaDto.ModelDto(Obe, mlisdet);
            var parametros = new BAdministracionSistema().listarParametro().First();
            EFacturaElectronicaResponse response = await model.GuiaRemisionElectronicaXML(objdocumento);

            if (!response.Exito)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = $"{parametros.Ruc}-{Obe.TipoDocumento}-{Obe.IdDocumento}.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{parametros.Ruc}-{Obe.TipoDocumento}-{Obe.IdDocumento}.xml");
                File.Copy(rutaArchivo, saveFileDialog.FileName);
                Process.Start(saveFileDialog.FileName);
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void darDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EGuiaRemision obe = (EGuiaRemision)viewGuiaRemision.GetRow(viewGuiaRemision.FocusedRowHandle);
            if (obe == null)
                return;
            if (XtraMessageBox.Show("Está Seguro de dar de Baja la Guia Remsión?", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return;
            enableLoading(true);
            new VentasData().GuiaRemisionElectronicaCambiarEstado(obe.remic_icod_remision, Constantes.DeBaja);

            backgroundWorker1.RunWorkerAsync();
            reload(obe.remic_icod_remision);
        }
    }
}