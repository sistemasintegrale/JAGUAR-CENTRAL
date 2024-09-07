using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm02EstadoCuentaProveedores : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        List<EProveedor> mlistProveedor = new List<EProveedor>();
        private int xposition = 0;

        public Frm02EstadoCuentaProveedores()
        {
            InitializeComponent();
        }

        private void FrmEstadoCuentaProveedores_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        private void Cargar()
        {
            mlistProveedor = Obl.ListarProveedoresSaldos(Parametros.intEjercicio, 0);
            dgrProveedores.DataSource = mlistProveedor;
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            dgrProveedores.DataSource = mlistProveedor.Where(obj =>
                                                   obj.vnombrecompleto.ToUpper().Contains(txtNombre.Text.ToUpper()) &&
                                                   obj.vcod_proveedor.ToString().Contains(txtcodigo.Text.ToUpper())
                                             ).ToList();

        }
        void form2_MiEvento()
        {
            Cargar();
        }
        private void todos_Click(object sender, EventArgs e)
        {
            if (mlistProveedor.Count > 0)
            {
                EProveedor Eproveedor = (EProveedor)viewProveedores.GetRow(viewProveedores.FocusedRowHandle);
                if (Eproveedor != null)
                {
                    FrmConsultarDocXPagarProveedor dxc = new FrmConsultarDocXPagarProveedor();
                    dxc.MiEvento += new FrmConsultarDocXPagarProveedor.DelegadoMensaje(form2_MiEvento);

                    dxc.Eproveedor = Eproveedor;
                    dxc.filtro = false;
                    //dxc.mnu.Items[1].Visible = true;
                    //dxc.mnu.Items[2].Visible = true;
                    dxc.Show();
                    xposition = viewProveedores.FocusedRowHandle;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (mlistProveedor.Count > 0)
            {
                EProveedor Eproveedor = (EProveedor)viewProveedores.GetRow(viewProveedores.FocusedRowHandle);

                FrmConsultarDocXPagarProveedor dxc = new FrmConsultarDocXPagarProveedor();
                dxc.MiEvento += new FrmConsultarDocXPagarProveedor.DelegadoMensaje(form2_MiEvento);
                dxc.Eproveedor = Eproveedor;
                dxc.filtro = true;
                //dxc.mnu.Items[1].Visible = true;
                //dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = viewProveedores.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void imprimirLista_Click(object sender, EventArgs e)
        {
            mlistProveedor = mlistProveedor.Where(ob => ob.vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd()) && ob.vcod_proveedor.Contains(txtcodigo.Text.TrimStart().TrimEnd())).ToList();
            if (mlistProveedor.Count > 0)
            {
                rptEstadoCuentaProveedorLista rpt = new rptEstadoCuentaProveedorLista();
                rpt.cargar(mlistProveedor, Parametros.intEjercicio.ToString());
            }
        }

        private void imprimirConDocumentos_Click(object sender, EventArgs e)
        {
            List<EDocPorPagar> listaTempProveedor = new List<EDocPorPagar>();
            rptEstadoCuentaDocumentosProveedor rpt = new rptEstadoCuentaDocumentosProveedor();
            listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedor(Parametros.intEjercicio, 0).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            if (listaTempProveedor.Count > 0)
            {
                rpt.cargar(listaTempProveedor, Parametros.intEjercicio.ToString(), false);
            }

        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            List<EDocPorPagar> listaTempProveedor = new List<EDocPorPagar>();
            rptEstadoCuentaDocumentosProveedor rpt = new rptEstadoCuentaDocumentosProveedor();
            listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedor(Parametros.intEjercicio, 0).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            if (listaTempProveedor.Count > 0)
            {
                rpt.cargar(listaTempProveedor.Where(ob => ob.tablc_iid_situacion_documento != Parametros.intSitDocCancelado).ToList(), Parametros.intEjercicio.ToString(), false);
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewProveedores.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewProveedores.ClearColumnsFilter();
        }
    }
}