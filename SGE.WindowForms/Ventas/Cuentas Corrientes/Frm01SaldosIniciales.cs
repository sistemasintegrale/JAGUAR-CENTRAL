using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Cobrar;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    public partial class Frm01SaldosIniciales : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm01SaldosIniciales));

        private List<EDocXCobrar> Lista = new List<EDocXCobrar>();
        private int xposition = 0;

        public Frm01SaldosIniciales()
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
            grv.FocusedRowHandle = xposition;
        }

        void AddEvent()
        {
            this.grv.DoubleClick += new System.EventHandler(this.grv_DoubleClick);
        }


        private void FrmSaldosIniciales_Load(object sender, EventArgs e)
        {
            Carga();
            cargaLkpTipoDoc();
        }

        private void Carga()
        {
            Lista = new BCuentasPorCobrar().listarDxc(Parametros.intEjercicio, 0);
            Lista = Lista.OrderBy(ord => ord.Abreviatura).ThenBy(ord => ord.doxcc_vnumero_doc).ToList();
            grd.DataSource = Lista;
        }

        private void cargaLkpTipoDoc()
        {
            List<string> listaTD = new List<string>();
            listaTD = Lista.OrderBy(ord => ord.Abreviatura).Select(sel => sel.Abreviatura).Distinct().ToList();
            listaTD.Add("TODOS");
            lkpTipoDoc.Properties.DataSource = listaTD;
            lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManteSaldosIniciales p = new FrmManteSaldosIniciales();
            p.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(form2_MiEvento);
            grv.MoveLast();
            //List<EParametro> parametros = new BParametro().ListarParametro();            
            //p.txtIGV.Text = parametros[0].pm_nigv_parametro.ToString();
            p.SetInsert();
            p.ShowDialog();
            cargaLkpTipoDoc();
        }

        private void Datos()
        {
            EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
            if (!(new BCuentasPorCobrar().VerificarExistenPagos(Obe.doxcc_icod_correlativo, Convert.ToInt32(Obe.tdocc_icod_tipo_doc))))
            {
                FrmManteSaldosIniciales p = new FrmManteSaldosIniciales();
                p.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(Modify);
                p.icod = Convert.ToInt64(Obe.doxcc_icod_correlativo);
                p.bteTipoDocumento.Tag = Obe.tdocc_icod_tipo_doc;
                p.bteTipoDocumento.Text = Obe.Abreviatura;
                p.bteClaseDocumento.Tag = Obe.tdodc_iid_correlativo;
                p.bteClaseDocumento.Text = Obe.ClaseDocumento;

                if (Obe.doxcc_vnumero_doc.Length == 12)
                {
                    p.txtSerie.Text = Obe.doxcc_vnumero_doc.Remove(4);
                    p.txtNumeroDocumento.Text = Obe.doxcc_vnumero_doc.Remove(0, 4);
                }
                else
                {
                    p.txtSerie.Text = Obe.doxcc_vnumero_doc.Remove(3);
                    p.txtNumeroDocumento.Text = Obe.doxcc_vnumero_doc.Remove(0, 3);
                }


                p.lblDescripcionClaseDocumento.Text = Obe.DescripcionClaseDocumento;
                p.bteCliente.Tag = Obe.cliec_icod_cliente;
                p.bteCliente.Text = Obe.cliec_vnombre_cliente;
                p.deFechaDocumento.EditValue = Obe.doxcc_sfecha_doc;
                p.LkpTipoMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
                p.txtTipoCambio.Text = Obe.doxcc_nmonto_tipo_cambio.ToString();
                p.txtConcepto.Text = Obe.doxcc_vobservaciones;
                p.deFechaVencimiento.EditValue = Obe.doxcc_sfecha_vencimiento_doc;
                p.txtOperacionGrabada.Text = Obe.doxcc_nmonto_afecto.ToString();
                p.txtInafecto.Text = Obe.doxcc_nmonto_inafecto.ToString();

                p.lblSubTotalValor.Text = (Obe.doxcc_nmonto_afecto + Obe.doxcc_nmonto_inafecto).ToString();
                p.lblPrecioVentaValor.Text = Obe.doxcc_nmonto_total.ToString();
                p.lblSaldoValor.Text = Obe.doxcc_nmonto_saldo.ToString();
                p.situacion = Obe.tablc_iid_situacion_documento;

                p.afecto = Obe.doxcc_nmonto_afecto;
                p.inafecto = Obe.doxcc_nmonto_inafecto;
                p.impuesto = Obe.doxcc_nmonto_impuesto;
                p.subtotal = (Obe.doxcc_nmonto_afecto + Obe.doxcc_nmonto_inafecto);
                p.total = Obe.doxcc_nmonto_total;

                xposition = grv.FocusedRowHandle;
                p.SetModify();
                p.ShowDialog();
            }
            else
                XtraMessageBox.Show("No se puede modificar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
                if (Obe.tablc_iid_situacion_documento != 111)
                {
                    if (Obe.doxcc_origen == "2")
                        Datos();
                    else
                        XtraMessageBox.Show("El documento procede de un área distinta, no puede modificarlo", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("El documento se encuentra anulado, no puede ser modificado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void grv_DoubleClick(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                FrmManteSaldosIniciales p = new FrmManteSaldosIniciales();
                p.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(AddEvent);
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
                p.icod = Convert.ToInt64(Obe.doxcc_icod_correlativo);
                p.SetCancel();
                p.Show();
                p.bteTipoDocumento.Tag = Obe.tdocc_icod_tipo_doc;
                p.bteTipoDocumento.Text = Obe.Abreviatura;
                p.bteClaseDocumento.Tag = Obe.tdodc_iid_correlativo;
                p.bteClaseDocumento.Text = Obe.ClaseDocumento;
                p.txtSerie.Text = Obe.doxcc_vnumero_doc.Remove(3);
                p.txtNumeroDocumento.Text = Obe.doxcc_vnumero_doc.Remove(0, 3);
                p.lblDescripcionClaseDocumento.Text = Obe.DescripcionClaseDocumento;
                p.bteCliente.Tag = Obe.cliec_icod_cliente;
                p.bteCliente.Text = Obe.cliec_vnombre_cliente;
                p.deFechaDocumento.EditValue = Obe.doxcc_sfecha_doc;
                p.LkpTipoMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
                p.txtTipoCambio.Text = Obe.doxcc_nmonto_tipo_cambio.ToString();
                p.txtConcepto.Text = Obe.doxcc_vobservaciones;
                p.deFechaVencimiento.EditValue = Obe.doxcc_sfecha_vencimiento_doc;
                p.txtOperacionGrabada.Text = Obe.doxcc_nmonto_afecto.ToString();
                p.txtInafecto.Text = Obe.doxcc_nmonto_inafecto.ToString();
                p.lblSubTotalValor.Text = (Obe.doxcc_nmonto_afecto + Obe.doxcc_nmonto_inafecto).ToString();
                p.lblPrecioVentaValor.Text = Obe.doxcc_nmonto_total.ToString();
                p.lblSaldoValor.Text = Obe.doxcc_nmonto_saldo.ToString();
                p.BtnGuardar.Enabled = false;
            }
            this.grv.DoubleClick -= new System.EventHandler(this.grv_DoubleClick);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
                if (!(new BCuentasPorCobrar().VerificarExistenPagos(Obe.doxcc_icod_correlativo, Convert.ToInt32(Obe.tdocc_icod_tipo_doc))))
                {
                    if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        BCuentasPorCobrar Obl = new BCuentasPorCobrar();
                        Obe.intUsuario = Valores.intUsuario;
                        Obe.strPc = WindowsIdentity.GetCurrent().Name.ToString();
                        Obl.EliminarDocumentoXCobrar(Obe);
                        grv.DeleteRow(grv.FocusedRowHandle);
                        cargaLkpTipoDoc();
                    }
                }
                else
                {
                    XtraMessageBox.Show("No se puede eliminar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void PagoDirecto_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
                if (Obe.tdocc_icod_tipo_doc != 83 && Obe.tdocc_icod_tipo_doc != 36)
                {
                    FrmPagoDirectoDocumentosXCobrar p = new FrmPagoDirectoDocumentosXCobrar();
                    p.MiEvento += new FrmPagoDirectoDocumentosXCobrar.DelegadoMensaje(form2_MiEvento);
                    p.mes = 0;
                    p.eDocXCobrar = Obe;
                    p.Show();
                    xposition = grv.FocusedRowHandle;
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
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
                if (Obe.tdocc_icod_tipo_doc != 1 && Obe.tdocc_icod_tipo_doc != 36)
                {
                    using (FrmPagoAdelanto p = new FrmPagoAdelanto())
                    {
                        p.MiEvento += new FrmPagoAdelanto.DelegadoMensaje(form2_MiEvento);
                        p.mes = 0;
                        p.eDocXCobrar = Obe;
                        p.ShowDialog();
                        xposition = grv.FocusedRowHandle;
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
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
                if (Obe.tdocc_icod_tipo_doc != 1 && Obe.tdocc_icod_tipo_doc != 36)
                {
                    using (FrmPagoNotaCredito p = new FrmPagoNotaCredito())
                    {
                        p.MiEvento += new FrmPagoNotaCredito.DelegadoMensaje(form2_MiEvento);
                        p.mes = 0;
                        p.eDocXCobrar = Obe;
                        p.ShowDialog();
                        xposition = grv.FocusedRowHandle;
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
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);

                if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoCliente) //adelanto
                {
                    FrmConsultaPagosAdelantos a = new FrmConsultaPagosAdelantos();
                    a.eDocXCobrar = Obe;
                    a.Show();
                    xposition = grv.FocusedRowHandle;
                }
                else if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoCliente) //nota de crédito
                {
                    FrmConsultaPagosConNCredito nc = new FrmConsultaPagosConNCredito();
                    nc.codENotaCreditoCliente = Obe.doxcc_icod_correlativo;
                    nc.Show();
                    xposition = grv.FocusedRowHandle;
                }
                else
                {
                    FrmConsultaPagosDocumentosXCobrar p = new FrmConsultaPagosDocumentosXCobrar();
                    p.eDocXCobrar = Obe;
                    p.ShowDialog();
                    xposition = grv.FocusedRowHandle;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void FrmSaldosIniciales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
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
                    XtraMessageBox.Show("No se puede eliminar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                XtraMessageBox.Show("No hay registro por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoDoc.ContainsFocus)
                filtroBuscar();
        }

        private void filtroBuscar()
        {
            if (lkpTipoDoc.Text == "TODOS" && txtNumDoc.Text.Trim() == "" && txtCliente.Text.TrimStart().TrimEnd() == "")
            {
                grd.DataSource = Lista;
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
                grd.DataSource = listaTempDxC;
            }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //Lista.ForEach(x =>
            //{
            //    var lst = (new BDocumentoXCobrarPago().ListarPagoDocumentoXCobrarXIdDocXCobrar(x.doxcc_icod_correlativo)).Where(ob => ob.pdxcc_vorigen == "D").ToList();
            //    if (lst.Count > 0)
            //        x.flag = true;

            //    //var lst = new BAdelantoPago().ListarPagoAdelantoXIdDocXPagar(x.doxpc_icod_correlativo);
            //    //if (lst.Count > 0)
            //    //    x.flag = true;                    
            //    //var lst = new BNotaCreditoPago().ListarPagoNotaCreditoXIdDocXPagar(x.doxpc_icod_correlativo);
            //    //if (lst.Count > 0)
            //    //    x.flag = true;       
            //});

            //grd.DataSource = Lista.Where(x => x.flag == true).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                rptSaldoInicialDocumentoCobrar rpt = new rptSaldoInicialDocumentoCobrar();
                rpt.cargar(Lista, "SALDOS INICIALES DE DOCUMENTOS POR COBRAR");
            }
        }

        private void cuotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
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
            EDocXCobrar Obe = (EDocXCobrar)grv.GetRow(grv.FocusedRowHandle);
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