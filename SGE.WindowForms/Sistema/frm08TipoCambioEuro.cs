using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm08TipoCambioEuro : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoCambioEuro> lstTipoCambioEuro = new List<ETipoCambioEuro>();
        public frm08TipoCambioEuro()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstTipoCambioEuro = new BAdministracionSistema().listarTipoCambioEuro(Parametros.intEjercicio);
            grdTipoCambio.DataSource = lstTipoCambioEuro;
            viewTipoCambio.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTipoCambioEuro.FindIndex(x => x.tceu_icod_tipo_cambio_euro == intIcod);
            viewTipoCambio.FocusedRowHandle = index;
            viewTipoCambio.Focus();
        }
        private void nuevo()
        {
            frmManteTipoCambioEuro frm = new frmManteTipoCambioEuro();
            frm.MiEvento += new frmManteTipoCambioEuro.DelegadoMensaje(reload);
            frm.lstTipoCambioEuro = lstTipoCambioEuro;
            frm.SetInsert();
            frm.dteFecha.EditValue = DateTime.Now;
            frm.Show();
        }
        private void modificar()
        {
            ETipoCambioEuro Obe = (ETipoCambioEuro)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoCambioEuro frm = new frmManteTipoCambioEuro();
            frm.MiEvento += new frmManteTipoCambioEuro.DelegadoMensaje(reload);
            frm.lstTipoCambioEuro = lstTipoCambioEuro;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            ETipoCambio Obe = (ETipoCambio)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoCambio frm = new frmManteTipoCambio();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            ETipoCambioEuro Obe = (ETipoCambioEuro)viewTipoCambio.GetRow(viewTipoCambio.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Obe.intUsuario = Valores.intUsuario;
                Obe.strPc = WindowsIdentity.GetCurrent().Name;
                new BAdministracionSistema().eliminarTipoCambioEuro(Obe);
                cargar();
            }
        }
        private void imprimir()
        {

        }

        private void buscarCriterio()
        {
            grdTipoCambio.DataSource = lstTipoCambioEuro.Where(x =>
                                                   x.tceu_sfecha_tipo_cambio_euro.ToShortDateString().StartsWith(txtCodigo.Text.ToUpper())).ToList();
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
    }
}