using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Cuentas_Corrientes
{
    public partial class frm01RegistroDeDocumentosPorPagarSaldoInicial : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm01RegistroDeDocumentosPorPagarSaldoInicial));

        private List<EDocPorPagar> Lista = new List<EDocPorPagar>();

        private int xposition = 0;

        public frm01RegistroDeDocumentosPorPagarSaldoInicial()
        {
            InitializeComponent();
        }

        private void Carga()
        {
            EDocPorPagar obj = new EDocPorPagar()
            {
                anio = Parametros.intEjercicio,
                mesec_iid_mes = 0,
            };
            Lista = new BCuentasPorPagar().ListarEDocPorPagar(obj);
            grdDXP.DataSource = Lista;
        }


        void Modify(long Cab_icod_correlativo)
        {
            Carga();
            int index = Lista.FindIndex(obe => obe.doxpc_icod_correlativo == Cab_icod_correlativo);
            viewDXP.FocusedRowHandle = index;
        }

        void AddEvent()
        {
            this.viewDXP.DoubleClick += new System.EventHandler(this.ViewDocumentosPorPagar_DoubleClick);
        }

        private void RegistroDeDocumentosPorPagar_Load(object sender, EventArgs e)
        {
            Carga();
            cargaLkpTipoDoc();
        }

        private void nuevo()
        {
            FrmManteSaldosIniciales p = new FrmManteSaldosIniciales();
            p.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(Modify);
            viewDXP.MoveLast();
            p.anio = Parametros.intEjercicio;
            p.mes = 0;
            p.SetInsert();
            p.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void Datos()
        {
            EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
            if (!(new BCuentasPorPagar().VerificarExistenPagos(Obe.doxpc_icod_correlativo, Convert.ToInt32(Obe.tdocc_icod_tipo_doc))))
            {
                FrmManteSaldosIniciales p = new FrmManteSaldosIniciales();
                p.MiEvento += new FrmManteSaldosIniciales.DelegadoMensaje(Modify);

                //cargar los controles con los datos
                p.Cab_icod_correlativo = Convert.ToInt64(Obe.doxpc_icod_correlativo);
                p.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                p.anio = Convert.ToInt32(Obe.anio);
                p.mes = 0;
                p.btnProveedor.Tag = Obe.proc_icod_proveedor;
                p.btnProveedor.Text = Obe.proc_vcod_proveedor;
                p.lblProveedor.Text = Obe.proc_vnombrecompleto;
                p.btnDocumento.Tag = Obe.tdocc_icod_tipo_doc;
                p.btnDocumento.Text = Obe.tdocc_vabreviatura_tipo_doc;
                p.lblClaseDocumento.Text = Obe.tdodc_descripcion;
                p.lkpMoneda.EditValue = Obe.tablc_iid_tipo_moneda;
                p.Moneda = Obe.tablc_iid_tipo_moneda;
                p.btnClaseDocumento.Tag = Obe.tdodc_iid_correlativo;
                p.btnClaseDocumento.Text = string.Format("{0:00}", Obe.idDetalleClaseDocumento);

                if (Obe.doxpc_vnumero_doc.Length == 12)
                {
                    if (Obe.doxpc_numdoc_tipo == 1)
                    {
                        p.txtSerie.Text = Obe.doxpc_vnumero_doc.Remove(4);
                        p.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Remove(0, 4);
                    }
                    else
                        p.txtNroDocumento.Text = Obe.doxpc_vnumero_doc;
                }
                else
                {
                    if (Obe.doxpc_numdoc_tipo == 1)
                    {
                        p.txtSerie.Text = Obe.doxpc_vnumero_doc.Remove(3);
                        p.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc.Remove(0, 3);
                    }
                    else
                        p.txtNroDocumento.Text = Obe.doxpc_vnumero_doc;
                }


                p.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc;
                p.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc;
                p.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                p.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                p.txtNoGravada.Text = Obe.doxpc_nmonto_total_documento.ToString();

                p.SetModify();
                p.Show();
            }
            else
                XtraMessageBox.Show("No se puede modificar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }
        private void ViewDocumentosPorPagar_DoubleClick(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                if (Obe.doxpc_origen != "H")
                {
                    FrmManteSaldosIniciales fmr = new FrmManteSaldosIniciales();
                    fmr.oDetail = Lista;
                    fmr.SetCancel();
                    fmr.Show();
                    fmr.Cab_icod_correlativo = Convert.ToInt64(Obe.doxpc_icod_correlativo);
                    fmr.txtCorrelativo.Text = Obe.doxpc_viid_correlativo;
                    fmr.anio = Convert.ToInt32(Obe.anio);
                    fmr.mes = 0;
                    fmr.btnProveedor.Tag = Obe.proc_icod_proveedor;
                    fmr.btnProveedor.Text = Obe.proc_iid_proveedor;
                    fmr.lblProveedor.Text = Obe.proc_vnombrecompleto;
                    fmr.btnDocumento.Tag = Obe.tdocc_icod_tipo_doc;
                    fmr.btnDocumento.Text = Obe.tdocc_vabreviatura_tipo_doc;
                    fmr.btnClaseDocumento.Tag = Obe.tdodc_iid_correlativo;
                    fmr.btnClaseDocumento.Text = Obe.clase_viid_correlativo;
                    fmr.txtNumeroDocumento.Text = Obe.doxpc_vnumero_doc;
                    fmr.dtmFechaDocumento.EditValue = Obe.doxpc_sfecha_doc;
                    fmr.dtmFechaVencimiento.EditValue = Obe.doxpc_sfecha_vencimiento_doc;
                    fmr.txtTipoDeCambio.Text = Obe.doxpc_nmonto_tipo_cambio.ToString();
                    fmr.txtConcepto.Text = Obe.doxpc_vdescrip_transaccion;
                    fmr.lblPrecioCompra.Text = Obe.doxpc_nmonto_total_documento.ToString();
                    fmr.lblSaldo.Text = Obe.doxpc_nmonto_total_saldo.ToString();

                    fmr.btnProveedor.Enabled = false;
                    fmr.txtCorrelativo.Enabled = false;
                    fmr.lkpMoneda.Enabled = false;
                    fmr.btnDocumento.Enabled = false;
                    fmr.btnClaseDocumento.Enabled = false;
                    fmr.txtNumeroDocumento.Enabled = false;
                    fmr.btnGuardar.Enabled = false;
                }
                else
                {
                }
            }
            this.viewDXP.DoubleClick -= new System.EventHandler(this.ViewDocumentosPorPagar_DoubleClick);
        }

        private void modificar()
        {
            if (Lista.Count > 0)
            {
                Datos();
            }
            else
                XtraMessageBox.Show("No hay registro por modificar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void pagosDirectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor)
                    {
                        FrmPagoDirectoDocumentosXPagar frm = new FrmPagoDirectoDocumentosXPagar();
                        frm.MiEvento += new FrmPagoDirectoDocumentosXPagar.DelegadoMensaje(Modify);
                        frm.mes = Convert.ToInt32(0);
                        frm.eDocXPagar = Obe;
                        frm.ShowDialog();
                        xposition = viewDXP.FocusedRowHandle;
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pagosAdelantoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor)
                    {
                        using (FrmConsultaPagosAdelantos p = new FrmConsultaPagosAdelantos())
                        {
                            p.MiEvento += new FrmConsultaPagosAdelantos.DelegadoMensaje(Modify);
                            p.mes = Convert.ToInt32(0);
                            p.eDocXPagar = Obe;
                            p.ShowDialog();
                            xposition = viewDXP.FocusedRowHandle;
                        }
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pagoNCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor && Obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor)
                    {
                        FrmConsultaPagosNotaCredito p = new FrmConsultaPagosNotaCredito();
                        p.MiEvento += new FrmConsultaPagosNotaCredito.DelegadoMensaje(Modify);
                        p.mes = Convert.ToInt32(0);
                        p.eDocXPagar = Obe;
                        p.Show();
                        xposition = viewDXP.FocusedRowHandle;
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void canjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                    if (Obe.tdodc_iid_correlativo != 1 && Obe.tdodc_iid_correlativo != 36)
                    {
                        FrmCanjeDocumentoXCobrarDocumentosXPagar p = new FrmCanjeDocumentoXCobrarDocumentosXPagar();
                        p.MiEvento += new FrmCanjeDocumentoXCobrarDocumentosXPagar.DelegadoMensaje(Modify);
                        p.mes = Convert.ToInt32(0);
                        p.eDocXPagar = Obe;
                        p.Mon = Obe.vMoneda;
                        p.IcodMon = Obe.tablc_iid_tipo_moneda;
                        p.ShowDialog();
                        xposition = viewDXP.FocusedRowHandle;
                    }
                    else
                        XtraMessageBox.Show("No se debe registrar el Pago de un Adelanto o Nota de Crédito", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consultaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lista.Count > 0)
                {
                    EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);

                    if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocAdelantoProveedor) //adelanto
                    {
                        FrmConsultaPagosAdelantos a = new FrmConsultaPagosAdelantos();
                        a.eDocXPagar = Obe;
                        a.ShowDialog();
                        xposition = viewDXP.FocusedRowHandle;
                        //FrmConsultaPagosDocumentosXPagar p = new FrmConsultaPagosDocumentosXPagar();
                        //p.eDocXPagar = Obe;
                        //p.ShowDialog();
                        //xposition = viewDXP.FocusedRowHandle;
                    }
                    else if (Obe.tdocc_icod_tipo_doc == Parametros.intTipoDocNotaCreditoProveedor) //nota de crédito
                    {
                        FrmConsultaPagosConNCredito nc = new FrmConsultaPagosConNCredito();
                        nc.codENotaCreditoCliente = Obe.doxpc_icod_correlativo;
                        nc.ShowDialog();
                        xposition = viewDXP.FocusedRowHandle;
                    }
                    else
                    {
                        FrmConsultaPagosDocumentosXPagar p = new FrmConsultaPagosDocumentosXPagar();
                        p.eDocXPagar = Obe;
                        p.ShowDialog();
                        xposition = viewDXP.FocusedRowHandle;
                    }
                }
                else
                    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)viewDXP.GetRow(viewDXP.FocusedRowHandle);
                if (!(new BCuentasPorPagar().VerificarExistenPagos(Obe.doxpc_icod_correlativo, Obe.tdocc_icod_tipo_doc)))
                {
                    if (XtraMessageBox.Show("¿Está seguro de Eliminar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        BCuentasPorPagar Obl = new BCuentasPorPagar();
                        Obe.intUsuario = Valores.intUsuario;
                        Obe.strPc = WindowsIdentity.GetCurrent().Name;
                        Obl.EliminarEDocPorPagar(Obe, null, null);
                        viewDXP.DeleteRow(viewDXP.FocusedRowHandle);
                    }
                }
                else
                {
                    XtraMessageBox.Show("No se puede eliminar porque existen pagos asociados", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                XtraMessageBox.Show("No hay registros por eliminar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
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
                grdDXP.DataSource = Lista;
            }
            else
            {
                List<EDocPorPagar> listaTempDxC = new List<EDocPorPagar>();
                if (lkpTipoDoc.Text != "TODOS")
                    listaTempDxC = Lista.Where(ob => ob.tdocc_vabreviatura_tipo_doc == lkpTipoDoc.Text).ToList();
                else
                    listaTempDxC = Lista;
                listaTempDxC = listaTempDxC.Where(ob => ob.doxpc_vnumero_doc.Contains(txtNumDoc.Text.TrimStart().TrimEnd())).ToList();
                listaTempDxC = listaTempDxC.Where(ob => ob.proc_vnombrecompleto.Contains(txtCliente.Text.TrimStart().TrimEnd())).ToList();
                grdDXP.DataSource = listaTempDxC;
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

        private void cargaLkpTipoDoc()
        {
            List<string> listaTD = new List<string>();
            listaTD = Lista.OrderBy(ord => ord.tdocc_vabreviatura_tipo_doc).Select(sel => sel.tdocc_vabreviatura_tipo_doc).Distinct().ToList();
            listaTD.Add("TODOS");
            lkpTipoDoc.Properties.DataSource = listaTD;
            lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Lista.ForEach(x =>
            {
                var lst = (new BCuentasPorPagar().listarDxpPagos(x.doxpc_icod_correlativo, Parametros.intEjercicio)).Where(ob => ob.pdxpc_vorigen == "D").ToList();
                if (lst.Count > 0)
                    x.flag = true;

                //var lst = new BAdelantoPago().ListarPagoAdelantoXIdDocXPagar(x.doxpc_icod_correlativo);
                //if (lst.Count > 0)
                //    x.flag = true;                    
                //var lst = new BNotaCreditoPago().ListarPagoNotaCreditoXIdDocXPagar(x.doxpc_icod_correlativo);
                //if (lst.Count > 0)
                //    x.flag = true;       
            });

            grdDXP.DataSource = Lista.Where(x => x.flag == true).ToList();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewDXP.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewDXP.ClearColumnsFilter();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                rptSaldoInicialDocPagar rpt = new rptSaldoInicialDocPagar();
                rpt.cargar(Lista, "SALDOS INICIALES DE DOCUMENTOS POR PAGAR");
            }
        }
    }
}