using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class frmCuentasBancarias : DevExpress.XtraEditors.XtraForm
    {
        List<EBancoCuenta> lstBancoCuentas = new List<EBancoCuenta>();
        public EBanco objBanco = new EBanco();

        public frmCuentasBancarias()
        {
            InitializeComponent();
        }

        private void frmCuentasBancarias_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstBancoCuentas = new BTesoreria().listarBancoCuentas(objBanco.bcoc_icod_banco);
            grdBancoCuentas.DataSource = lstBancoCuentas;
            viewBancoCuentas.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstBancoCuentas.FindIndex(x => x.bcod_icod_banco_cuenta == intIcod);
            viewBancoCuentas.FocusedRowHandle = index;
            viewBancoCuentas.Focus();
        }
        private void nuevo()
        {
            frmManteCuentasBancarias frm = new frmManteCuentasBancarias();
            frm.MiEvento += new frmManteCuentasBancarias.DelegadoMensaje(reload);
            if (lstBancoCuentas.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstBancoCuentas.Max(x => x.bcod_iicod_banco_cuenta + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstBancoCuentas = lstBancoCuentas;
            frm.objBanco = objBanco;
            frm.SetInsert();
            frm.txtAnalitica.Text = String.Format("{0:00}{1:00}", objBanco.bcoc_iid_banco, lstBancoCuentas.Count + 1);
            frm.bteAnalitica.Text = "BANCOS";
            frm.bteAnalitica.Tag = 1;
            frm.Show();
            frm.txtNroCuenta.Focus();

        }
        private void modificar()
        {
            EBancoCuenta Obe = (EBancoCuenta)viewBancoCuentas.GetRow(viewBancoCuentas.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteCuentasBancarias frm = new frmManteCuentasBancarias();
            frm.MiEvento += new frmManteCuentasBancarias.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.objBanco = objBanco;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtNroCuenta.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EBancoCuenta Obe = (EBancoCuenta)viewBancoCuentas.GetRow(viewBancoCuentas.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteCuentasBancarias frm = new frmManteCuentasBancarias();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            EBancoCuenta Obe = (EBancoCuenta)viewBancoCuentas.GetRow(viewBancoCuentas.FocusedRowHandle);
            if (Obe == null)
                return;
            try
            {
                var flagTieneMovimientos = new BTesoreria().verificarBancoCuentaMovimientos(Obe.bcod_icod_banco_cuenta);
                if (flagTieneMovimientos)
                    throw new ArgumentException(String.Format("La Cuenta Bancaria {0} no puede ser eliminada!, debido a que tiene movimientos bancarios", Obe.bcod_vnumero_cuenta));
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BTesoreria().eliminarBancoCuenta(Obe);
                    cargar();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


    }
}