using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Cuentas_Corrientes
{
    public partial class Frm05PaseCierre : DevExpress.XtraEditors.XtraForm
    {
        BCuentasPorPagar Obl = new BCuentasPorPagar();
        List<EProveedor> mlistProveedor = new List<EProveedor>();
        private int xposition = 0;

        public Frm05PaseCierre()
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

        private void cierreDelEjercicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAnioEjercicio a = new BCuentasPorCobrar().VerificarExistenciaAnoSiguiente(Parametros.intEjercicio);
            if (a.anioc_iid_anio_ejercicio == (Parametros.intEjercicio + 1))
            {
                if (XtraMessageBox.Show("Esta opción servirá únicamente para efectuar la inicialización de saldos x pagar del prox. año. Si desea continuar presione el botón OK", "Información del Sistema", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cierre();
                    XtraMessageBox.Show("Pase Exitoso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Año " + (Parametros.intEjercicio + 1).ToString() + " no se encuentra registrado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Cierre()
        {
            try
            {
                new BCuentasPorPagar().CierreDocumentoXPagar(Parametros.intEjercicio, Valores.intUsuario);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}