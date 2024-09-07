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
    public partial class Frm04EstadoCuentaProveedoresFecha : DevExpress.XtraEditors.XtraForm
    {

        BCuentasPorPagar Obl = new BCuentasPorPagar();
        List<EProveedor> mlistProveedor = new List<EProveedor>();
        private int xposition = 0;

        public Frm04EstadoCuentaProveedoresFecha()
        {
            InitializeComponent();
        }

        private void FrmEstadoCuentaProveedoresFecha_Load(object sender, EventArgs e)
        {
            deInicio.EditValue = DateTime.Now;
            Cargar();
        }
        private void BuscarCriterio()
        {
            dgrProveedores.DataSource = mlistProveedor.Where(obj =>
                                                   obj.vnombrecompleto.ToUpper().Contains(txtNombre.Text.ToUpper()) &&
                                                   obj.vcod_proveedor.ToString().Contains(txtcodigo.Text.ToUpper())
                                             ).ToList();

        }
        private void Cargar()
        {
            mlistProveedor = Obl.ListarProveedoresSaldosAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue));
            dgrProveedores.DataSource = mlistProveedor;
        }
        void form2_MiEvento()
        {
            Cargar();
        }
        private void todos_Click(object sender, EventArgs e)
        {
            if (mlistProveedor.Count > 0)
            {
                EProveedor Eproveedor = (EProveedor)viewEstadoCuenta.GetRow(viewEstadoCuenta.FocusedRowHandle);
                if (Eproveedor != null)
                {
                    FrmConsultarDocXPagarProveedorAUnaFecha dxc = new FrmConsultarDocXPagarProveedorAUnaFecha();
                    dxc.MiEvento += new FrmConsultarDocXPagarProveedorAUnaFecha.DelegadoMensaje(form2_MiEvento);
                    dxc.Eproveedor = Eproveedor;
                    dxc.filtro = false;
                    dxc.sfecha = Convert.ToDateTime(deInicio.EditValue);
                    //dxc.mnu.Items[1].Visible = true;
                    //dxc.mnu.Items[2].Visible = true;
                    dxc.Show();
                    xposition = viewEstadoCuenta.FocusedRowHandle;
                }
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void pendientes_Click(object sender, EventArgs e)
        {
            if (mlistProveedor.Count > 0)
            {
                EProveedor Eproveedor = (EProveedor)viewEstadoCuenta.GetRow(viewEstadoCuenta.FocusedRowHandle);

                FrmConsultarDocXPagarProveedorAUnaFecha dxc = new FrmConsultarDocXPagarProveedorAUnaFecha();
                dxc.MiEvento += new FrmConsultarDocXPagarProveedorAUnaFecha.DelegadoMensaje(form2_MiEvento);
                dxc.Eproveedor = Eproveedor;
                dxc.sfecha = Convert.ToDateTime(deInicio.EditValue);
                dxc.filtro = true;
                //dxc.mnu.Items[1].Visible = true;
                //dxc.mnu.Items[2].Visible = true;
                dxc.Show();
                xposition = viewEstadoCuenta.FocusedRowHandle;
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void deInicio_EditValueChanged(object sender, EventArgs e)
        {
            this.Cargar();
        }

        private void txtcodigo_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }

        private void imprimirLista_Click(object sender, EventArgs e)
        {
            rptEstadoCuentaProveedorLista rpt = new rptEstadoCuentaProveedorLista();
            rpt.cargar(mlistProveedor, Parametros.intEjercicio.ToString());
        }

        private void imprimirConDocumentos_Click(object sender, EventArgs e)
        {
            List<EDocPorPagar> listaTempProveedor = new List<EDocPorPagar>();
            rptEstadoCuentaDocumentosProveedor rpt = new rptEstadoCuentaDocumentosProveedor();
            listaTempProveedor = new BCuentasPorPagar().EstadoCuentaDocumentosProveedorAUnaFecha(Parametros.intEjercicio, Convert.ToDateTime(deInicio.EditValue)).Where(ob => ob.proc_vnombrecompleto.Contains(txtNombre.Text.TrimStart().TrimEnd())).ToList();
            if (listaTempProveedor.Count > 0)
            {
                rpt.cargar(listaTempProveedor, Parametros.intEjercicio.ToString(), false);
            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewEstadoCuenta.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewEstadoCuenta.ClearColumnsFilter();
        }
    }
}