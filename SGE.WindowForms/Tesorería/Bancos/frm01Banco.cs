using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Tesorería.Bancos
{
    public partial class frm01Banco : DevExpress.XtraEditors.XtraForm
    {
        List<EBanco> lstBancos = new List<EBanco>();
        public frm01Banco()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstBancos = new BTesoreria().listarBancos();
            grdBanco.DataSource = lstBancos;
            viewBanco.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstBancos.FindIndex(x => x.bcoc_icod_banco == intIcod);
            viewBanco.FocusedRowHandle = index;
            viewBanco.Focus();
        }
        private void nuevo()
        {
            frmManteBanco frm = new frmManteBanco();
            frm.MiEvento += new frmManteBanco.DelegadoMensaje(reload);
            if (lstBancos.Count == 0)
                frm.txtCodigo.Text = "001";
            else
                frm.txtCodigo.Text = String.Format("{0:000}", lstBancos.Max(x => x.bcoc_iid_banco + 1));
            frm.lstBancos = lstBancos;
            frm.SetInsert();
            frm.Show();
            frm.txtDescripcion.Focus();
        }
        private void modificar()
        {
            try
            {
                EBanco Obe = (EBanco)viewBanco.GetRow(viewBanco.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteBanco frm = new frmManteBanco();
                frm.MiEvento += new frmManteBanco.DelegadoMensaje(reload);
                frm.Obe = Obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.txtDescripcion.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                EBanco Obe = (EBanco)viewBanco.GetRow(viewBanco.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteBanco frm = new frmManteBanco();
                frm.Obe = Obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void eliminar()
        {
            try
            {
                EBanco Obe = (EBanco)viewBanco.GetRow(viewBanco.FocusedRowHandle);
                if (Obe == null)
                    return;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BTesoreria().eliminarBanco(Obe);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            EBanco Obe = (EBanco)viewBanco.GetRow(viewBanco.FocusedRowHandle);
            if (Obe == null)
                return;
        }
        private void cuentasBancarias()
        {
            EBanco Obe = (EBanco)viewBanco.GetRow(viewBanco.FocusedRowHandle);
            if (Obe == null)
                return;
            frmCuentasBancarias frm = new frmCuentasBancarias();
            frm.objBanco = Obe;
            frm.viewBancoCuentas.GroupPanelText = String.Format("CUENTAS - {0}", Obe.bcoc_vnombre_banco);
            frm.Show();
        }
        private void buscarCriterio()
        {
            grdBanco.DataSource = lstBancos.Where(x =>
                                                   x.bcoc_iid_banco.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.bcoc_vnombre_banco.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
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

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void cuentasBancariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cuentasBancarias();
        }

        private void btnCuentas_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cuentasBancarias();
        }
    }
}