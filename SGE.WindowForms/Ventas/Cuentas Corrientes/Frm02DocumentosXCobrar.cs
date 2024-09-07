using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.Generacion_de_Vouchers;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    public partial class Frm02DocumentosXCobrar : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm02DocumentosXCobrar));

        private List<EDocXCobrar> Lista = new List<EDocXCobrar>();
        private int xposition = 0;

        public Frm02DocumentosXCobrar()
        {
            InitializeComponent();
        }

        void form2_MiEvento()
        {
            Carga();
        }

        void Modify()
        {
            Carga();
            viewDXC.FocusedRowHandle = xposition;
        }

        void AddEvent()
        {
            this.viewDXC.DoubleClick += new System.EventHandler(this.grv_DoubleClick);
        }

        private void DocumentosXCobrar_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            cargaLkpTipoDoc();
        }

        private void cargaLkpTipoDoc()
        {
            List<string> listaTD = new List<string>();
            listaTD = Lista.OrderBy(ord => ord.Abreviatura).Select(sel => sel.Abreviatura).Distinct().ToList();
            listaTD.Add("TODOS");
            lkpTipoDoc.Properties.DataSource = listaTD;
            lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().listarDxc(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            Lista = Lista.OrderBy(ord => ord.Abreviatura).ThenBy(ord => ord.doxcc_vnumero_doc).ToList();
            grdDXC.DataSource = Lista;
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
            cargaLkpTipoDoc();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteDocumentosXCobrar frm = new FrmManteDocumentosXCobrar();
            frm.MiEvento += new FrmManteDocumentosXCobrar.DelegadoMensaje(form2_MiEvento);
            viewDXC.MoveLast();
            List<EParametro> parametros = new BAdministracionSistema().listarParametro();
            frm.txtIGV.Text = parametros[0].pm_nigv_parametro.ToString();
            frm.SetInsert();
            frm.lkpTipoDocRef.Enabled = false;
            frm.bteNroDocRef.Enabled = false;
            frm.dtFechaReferencia.Enabled = false;
            if (frm.ShowDialog() == DialogResult.OK)
                cargaLkpTipoDoc();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
                if (Obe.tablc_iid_situacion_documento == Parametros.intSitDocCobrarGenerado)
                {
                    if (Obe.doxcc_origen == "2")
                    {
                        FrmManteDocumentosXCobrar frm = new FrmManteDocumentosXCobrar();
                        frm._BE = Obe;
                        frm.MiEvento += new FrmManteDocumentosXCobrar.DelegadoMensaje(Modify);
                        frm.SetModify();
                        frm.ShowDialog();
                        xposition = viewDXC.FocusedRowHandle;
                    }
                    else
                    {
                        XtraMessageBox.Show("El documento no puede ser modificado, por que fue registrada desde " + Obe.DescripcionOrigen, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    //XtraMessageBox.Show("El documento no puede ser modificado, por que se encuentra " + Obe.Situacion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (XtraMessageBox.Show("El documento no puede ser modificado, por que se encuentra, ¿desea modificar de todas maneras?" + Obe.Situacion, "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FrmManteDocumentosXCobrar frm = new FrmManteDocumentosXCobrar();
                        frm._BE = Obe;
                        frm.MiEvento += new FrmManteDocumentosXCobrar.DelegadoMensaje(Modify);
                        //frm.SetModify();
                        frm.StatusControl2();
                        frm.Values();
                        frm.Totalizar();
                        frm.lkpTipoDXC.Enabled = false;
                        frm.ShowDialog();
                        xposition = viewDXC.FocusedRowHandle;
                    }
                    else
                    {
                        return;
                    }
                }
            }

        }

        private void grv_DoubleClick(object sender, EventArgs e)
        {
            EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
            FrmManteDocumentosXCobrar frm = new FrmManteDocumentosXCobrar();
            frm._BE = Obe;
            frm.MiEvento += new FrmManteDocumentosXCobrar.DelegadoMensaje(Modify);
            frm.SetCancel();
            frm.txtConcepto.Enabled = true;
            frm.txtConcepto.Properties.ReadOnly = false;
            frm.ShowDialog();
            xposition = viewDXC.FocusedRowHandle;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
                if (Obe.doxcc_origen == "2")
                {
                    if (!(new BCuentasPorCobrar().VerificarExistenPagos(Obe.doxcc_icod_correlativo, Convert.ToInt32(Obe.tdocc_icod_tipo_doc))))
                    {
                        if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            BCuentasPorCobrar Obl = new BCuentasPorCobrar();
                            Obe.intUsuario = Valores.intUsuario;
                            Obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                            Obl.EliminarDocumentoXCobrar(Obe);
                            viewDXC.DeleteRow(viewDXC.FocusedRowHandle);
                            cargaLkpTipoDoc();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("No se puede eliminar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                    XtraMessageBox.Show("El documento procede de un área distinta, no puede eliminarlo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void AnulartoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
                if (Obe.doxcc_origen == "2")
                {
                    if (!(new BCuentasPorCobrar().VerificarExistenPagos(Obe.doxcc_icod_correlativo, Convert.ToInt32(Obe.tdocc_icod_tipo_doc))))
                    {
                        if (XtraMessageBox.Show("¿Está seguro de Anular?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            BCuentasPorCobrar Obl = new BCuentasPorCobrar();
                            Obe.intUsuario = Valores.intUsuario;
                            Obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                            Obl.AnularDocumentoXCobrar(Obe);
                            Carga();
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("No se puede anular porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                    XtraMessageBox.Show("El documento procede de un área distinta, no puede anularlo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro por anular", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void PagoDirecto_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
                if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoCliente && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoCliente)
                {
                    FrmPagoDirectoDocumentosXCobrar p = new FrmPagoDirectoDocumentosXCobrar();
                    p.MiEvento += new FrmPagoDirectoDocumentosXCobrar.DelegadoMensaje(form2_MiEvento);
                    p.mes = Convert.ToInt32(lkpMes.EditValue);
                    p.eDocXCobrar = Obe;
                    p.ShowDialog();
                    xposition = viewDXC.FocusedRowHandle;
                }
                else
                    XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void PagoAdelanto_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
                if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoCliente && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoCliente)
                {
                    using (FrmPagoAdelanto p = new FrmPagoAdelanto())
                    {
                        p.MiEvento += new FrmPagoAdelanto.DelegadoMensaje(form2_MiEvento);
                        p.mes = Convert.ToInt32(lkpMes.EditValue);
                        p.eDocXCobrar = Obe;
                        p.ShowDialog();
                        xposition = viewDXC.FocusedRowHandle;

                    }
                }
                else
                    XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void PagoNC_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
                if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoCliente && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoCliente)
                {
                    using (FrmPagoNotaCredito p = new FrmPagoNotaCredito())
                    {
                        p.MiEvento += new FrmPagoNotaCredito.DelegadoMensaje(form2_MiEvento);
                        p.mes = Convert.ToInt32(lkpMes.EditValue);
                        p.eDocXCobrar = Obe;
                        p.ShowDialog();
                        xposition = viewDXC.FocusedRowHandle;
                    }
                }
                else
                    XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ConsultaPago_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);

                if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente) //adelanto
                {
                    FrmConsultaPagosAdelantos a = new FrmConsultaPagosAdelantos();
                    a.eDocXCobrar = Obe;
                    a.Show();
                    xposition = viewDXC.FocusedRowHandle;

                }
                else if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente) //nota de crédito
                {
                    FrmConsultaPagosConNCredito nc = new FrmConsultaPagosConNCredito();
                    nc.codENotaCreditoCliente = Obe.doxcc_icod_correlativo;
                    nc.Show();
                    xposition = viewDXC.FocusedRowHandle;
                }
                else
                {
                    FrmConsultaPagosDocumentosXCobrar p = new FrmConsultaPagosDocumentosXCobrar();
                    p.eDocXCobrar = Obe;
                    p.ShowDialog();
                    xposition = viewDXC.FocusedRowHandle;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void Generar_Click(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
            {

            }
            else
            {
                XtraMessageBox.Show("Seleccionar un Mes", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lkpMes.Focus();
            }
        }



        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoDoc.ContainsFocus)
                filtroBuscar();
        }

        private void txtNumDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNumDoc.ContainsFocus)
                filtroBuscar();
        }

        private void txtCliente_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCliente.ContainsFocus)
                filtroBuscar();
        }

        private void filtroBuscar()
        {
            if (lkpTipoDoc.Text == "TODOS" && txtNumDoc.Text.Trim() == "" && txtCliente.Text.TrimStart().TrimEnd() == "")
            {
                grdDXC.DataSource = Lista;
            }
            else
            {
                List<EDocXCobrar> listaTempDxC = new List<EDocXCobrar>();
                if (lkpTipoDoc.Text != "TODOS")
                    listaTempDxC = Lista.Where(ob => ob.Abreviatura == lkpTipoDoc.Text).ToList();
                else
                    listaTempDxC = Lista;
                listaTempDxC = listaTempDxC.Where(ob => ob.doxcc_vnumero_doc.Contains(txtNumDoc.Text.TrimStart().TrimEnd())).ToList();
                listaTempDxC = listaTempDxC.Where(ob => ob.cliec_vnombre_cliente.Contains(txtCliente.Text.TrimStart().TrimEnd())).ToList();
                grdDXC.DataSource = listaTempDxC;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            frmGenerarVoucher frm = new frmGenerarVoucher();
            frm.Show();
        }

        private void viewDXC_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string strSituacion = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Situacion"]);
                if (strSituacion == "ANULADO")
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void generarVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea ejecutar el proceso de Contabilización para el periodo de {0}?", lkpMes.Text.ToUpper()), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            controlEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }

        private void generarVouchers()
        {
            try
            {
                new BContabilidad().generarVouchersVentas(Convert.ToInt32(lkpMes.EditValue), Valores.intUsuario, WindowsIdentity.GetCurrent().Name);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema");
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                generarVouchers();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            controlEnable(false);
            viewDXC.Focus();
            XtraMessageBox.Show("El proceso de GENERACIÓN DE VOUCHER ha sido ejecutado satisfactoriamente", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void controlEnable(bool Enable)
        {
            panel1.Visible = Enable;
            lkpMes.Enabled = !Enable;
            lkpTipoDoc.Enabled = !Enable;
            txtNumDoc.Enabled = !Enable;
            txtCliente.Enabled = !Enable;
            mnu.Enabled = !Enable;
            grdDXC.Enabled = !Enable;
        }

        private void actualizarTipoDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea ejecutar el proceso de Actualización de Tipo Cambio para todos los documentos?", lkpMes.Text.ToUpper()), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            new BCuentasPorCobrar().ActualizarTipoCambio("DXC");
            XtraMessageBox.Show("Actualización realizada", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void viewDXC_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void cuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
            if (Obe is null)
                return;
            var listaCuotas = new BVentas().CuotasFacturaListar(Obe.doxcc_icod_correlativo);
            var listaCuotasEliminar = new List<ECuotasFactura>();
            EFacturaCab obe = new SGE.DataAccess.VentasData().getFacturaCabXDxp(Obe.doxcc_icod_correlativo);
            FrmManteCuotasFactura frm = new FrmManteCuotasFactura();
            frm.Text = $"Cuotas de la FAV {obe.favc_vnumero_factura}";
            frm.eFacturaCab = obe;
            frm.txtMontoCredito.Text = obe.facv_nmonto_credito.ToString();
            frm.listaCuotas = listaCuotas;
            frm.listaCuotasEliminar = listaCuotasEliminar;
            frm.Status = Maintenance.BSMaintenanceStatus.ModifyCurrent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                listaCuotas = frm.listaCuotas;
                listaCuotasEliminar = frm.listaCuotasEliminar;

                listaCuotas.ForEach(x =>
                {
                    x.favc_icod_factura = obe.favc_icod_factura;
                    x.doxcc_icod_correlativo = (int)obe.doxcc_icod_correlativo;
                    if (x.operacion == 2)
                    {
                        new SGE.DataAccess.VentasData().CuotasFacturaModificar(x);
                    }
                    if (x.fcc_icod == 0)
                    {
                        x.fcc_icod = new SGE.DataAccess.VentasData().CuotasFacturaIngresar(x);
                    }
                });

                listaCuotasEliminar.ForEach(x =>
                {
                    new SGE.DataAccess.VentasData().CuotasFacturaEliminar(x);
                });
            }
        }

        private void mnu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EDocXCobrar Obe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
            if (Obe is null)
                return;
            if (Obe.Abreviatura == "FAV")
            {
                cuotasToolStripMenuItem.Visible = true;
            }
            else
            {
                cuotasToolStripMenuItem.Visible = false;
            }

        }
    }
}