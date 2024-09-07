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
    public partial class Frm09EstadoCuentasRHProveedores : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        List<EProveedor> mlistProveedor = new List<EProveedor>();
        private int xposition = 0;

        public Frm09EstadoCuentasRHProveedores()
        {
            InitializeComponent();
        }

        private void FrmEstadoCuentasRHProveedores_Load(object sender, EventArgs e)
        {
            this.Cargar();
        }
        private void Cargar()
        {
            mlistProveedor = Obl.ListarProveedoresSaldos(Parametros.intEjercicio, 51);
            dgrProveedores.DataSource = mlistProveedor;
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

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }

        private void todos_Click(object sender, EventArgs e)
        {
            if (mlistProveedor.Count > 0)
            {
                EProveedor Eproveedor = (EProveedor)viewRHO.GetRow(viewRHO.FocusedRowHandle);
                if (Eproveedor != null)
                {
                    FrmConsultarDocXPagarProveedorRHO dxc = new FrmConsultarDocXPagarProveedorRHO();
                    dxc.MiEvento += new FrmConsultarDocXPagarProveedorRHO.DelegadoMensaje(form2_MiEvento);
                    dxc.Eproveedor = Eproveedor;
                    dxc.filtro = false;
                    //dxc.mnu.Items[1].Visible = true;
                    //dxc.mnu.Items[2].Visible = true;
                    dxc.Show();
                    xposition = viewRHO.FocusedRowHandle;
                }
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
            listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedor(Parametros.intEjercicio, 51).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            if (listaTempProveedor.Count > 0)
            {
                rpt.cargar(listaTempProveedor, Parametros.intEjercicio.ToString(), false);
            }
        }

        private void imprimirSoloPendientes_Click(object sender, EventArgs e)
        {
            List<EDocPorPagar> listaTempProveedor = new List<EDocPorPagar>();
            rptEstadoCuentaDocumentosProveedor rpt = new rptEstadoCuentaDocumentosProveedor();
            listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedor(Parametros.intEjercicio, 51).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            if (listaTempProveedor.Count > 0)
            {
                rpt.cargar(listaTempProveedor.Where(ob => ob.tablc_iid_situacion_documento == 292 && ob.tablc_iid_situacion_documento == 293).ToList(), Parametros.intEjercicio.ToString(), false);
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewRHO.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewRHO.ClearColumnsFilter();
        }
    }
}