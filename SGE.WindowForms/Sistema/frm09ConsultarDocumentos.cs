using DevExpress.XtraGrid.Views.Grid;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm09ConsultarDocumentos : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        List<EDocPorPagar> Lista1 = new List<EDocPorPagar>();
        List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        private int xposition = 0;

        public frm09ConsultarDocumentos()
        {
            InitializeComponent();
        }

        private void FrmEstadoCuentaProveedores_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        private void Cargar()
        {
            Lista = Obl.ConsultarDocumentos(Parametros.intEjercicio);
            Lista.ForEach(x =>
            {
                if (Convert.ToDecimal(x.doxpc_nmonto_total_saldo) != x.Diferencia)
                {
                    x.Color = "Con";
                }
                else
                    x.Color = "Sin";
            });

            dgr.DataSource = Lista;

        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            //dgrProveedores.DataSource = mlistProveedor.Where(obj =>
            //                                       obj.vnombrecompleto.ToUpper().Contains(txtNombre.Text.ToUpper()) &&
            //                                       obj.vcod_proveedor.ToString().Contains(txtcodigo.Text.ToUpper())
            //                                 ).ToList();

        }
        void form2_MiEvento()
        {
            Cargar();
        }
        private void todos_Click(object sender, EventArgs e)
        {
            //if (mlistProveedor.Count > 0)
            //{
            //    EProveedor Eproveedor = (EProveedor)viewProveedores.GetRow(viewProveedores.FocusedRowHandle);
            //    if (Eproveedor != null)
            //    {
            //        FrmConsultarDocXPagarProveedor dxc = new FrmConsultarDocXPagarProveedor();
            //        dxc.MiEvento += new FrmConsultarDocXPagarProveedor.DelegadoMensaje(form2_MiEvento);

            //        dxc.Eproveedor = Eproveedor;
            //        dxc.filtro = false;
            //        //dxc.mnu.Items[1].Visible = true;
            //        //dxc.mnu.Items[2].Visible = true;
            //        dxc.Show();
            //        xposition = viewProveedores.FocusedRowHandle;
            //    }
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);       
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            //if (mlistProveedor.Count > 0)
            //{
            //    EProveedor Eproveedor = (EProveedor)viewProveedores.GetRow(viewProveedores.FocusedRowHandle);

            //    FrmConsultarDocXPagarProveedor dxc = new FrmConsultarDocXPagarProveedor();
            //    dxc.MiEvento += new FrmConsultarDocXPagarProveedor.DelegadoMensaje(form2_MiEvento);
            //    dxc.Eproveedor = Eproveedor;
            //    dxc.filtro = true;
            //    //dxc.mnu.Items[1].Visible = true;
            //    //dxc.mnu.Items[2].Visible = true;
            //    dxc.Show();
            //    xposition = viewProveedores.FocusedRowHandle;
            //}
            //else
            //    XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirLista_Click(object sender, EventArgs e)
        {
            //mlistProveedor = mlistProveedor.Where(ob => ob.vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.vcod_proveedor.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            //if(mlistProveedor.Count>0)
            //{
            //rptEstadoCuentaProveedorLista rpt = new rptEstadoCuentaProveedorLista();
            //rpt.cargar(mlistProveedor, Parametros.intEjercicio.ToString());
            //    }
        }

        private void imprimirConDocumentos_Click(object sender, EventArgs e)
        {
            //List<EDocPorPagar> listaTempProveedor = new List<EDocPorPagar>();
            //rptEstadoCuentaDocumentosProveedor rpt = new rptEstadoCuentaDocumentosProveedor();
            //listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedor(Parametros.intEjercicio, 0).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            //if (listaTempProveedor.Count > 0)
            //{
            //    rpt.cargar(listaTempProveedor, Parametros.intEjercicio.ToString(), false);
            //}

        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            //List<EDocPorPagar> listaTempProveedor = new List<EDocPorPagar>();
            //rptEstadoCuentaDocumentosProveedor rpt = new rptEstadoCuentaDocumentosProveedor();
            //listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedor(Parametros.intEjercicio, 0).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            //if (listaTempProveedor.Count > 0)
            //{
            //    rpt.cargar(listaTempProveedor.Where(ob => ob.tablc_iid_situacion_documento != Parametros.intSitDocCancelado).ToList(), Parametros.intEjercicio.ToString(), false);
            //}
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewProveedores.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewProveedores.ClearColumnsFilter();
        }

        private void view_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string Saldo = View.GetRowCellDisplayText(e.RowHandle, View.Columns["doxpc_nmonto_total_saldo"]);
                string SaldoD = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Diferencia"]);
                decimal Saldo2 = Convert.ToDecimal(Saldo);
                decimal SaldoD2 = Convert.ToDecimal(SaldoD);
                if (Saldo2 != SaldoD2)
                {
                    e.Appearance.BackColor = Color.LightSalmon;
                }
            }
        }

        private void verPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)view.GetRow(view.FocusedRowHandle);

                switch (Obe.tdocc_icod_tipo_doc)
                {
                    case 1:
                        FrmConsultarAdelantoPagos av = new FrmConsultarAdelantoPagos();
                        av.eDocXPagar = Obe;
                        av.doxcc_icod_correlativo_adelanto = Obe.doxpc_icod_correlativo;
                        av.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                    case 86:
                        FrmConsultaPagosNotaCredito FrmNotaC = new FrmConsultaPagosNotaCredito();
                        FrmNotaC.eDocXPagar = Obe;
                        FrmNotaC.Cab_icod_correlativo = Obe.doxpc_icod_correlativo;
                        FrmNotaC.Show();
                        xposition = view.FocusedRowHandle;
                        FrmNotaC.mnu.Enabled = false;
                        break;
                    default:
                        FrmConsultaPagosDocumentosXPagar a = new FrmConsultaPagosDocumentosXPagar();
                        a.eDocXPagar = Obe;
                        a.Show();
                        xposition = view.FocusedRowHandle;
                        break;
                }

            }
        }
    }
}