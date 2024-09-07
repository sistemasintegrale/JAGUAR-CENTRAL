using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm03TipoTarjeta : DevExpress.XtraEditors.XtraForm
    {
        List<ETipoTarjeta> lstTipoTarjeta = new List<ETipoTarjeta>();
        public frm03TipoTarjeta()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstTipoTarjeta = new BVentas().listarTipoTarjeta();
            grdUnidadMedida.DataSource = lstTipoTarjeta;
            viewUnidadMedida.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTipoTarjeta.FindIndex(x => x.tcrc_icod_tipo_tarjeta_cred == intIcod);
            viewUnidadMedida.FocusedRowHandle = index;
            viewUnidadMedida.Focus();
        }
        private void nuevo()
        {
            frmManteTipoTarjeta frm = new frmManteTipoTarjeta();
            frm.MiEvento += new frmManteTipoTarjeta.DelegadoMensaje(reload);
            frm.lstTipoTarjeta = lstTipoTarjeta;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }
        private void modificar()
        {
            ETipoTarjeta Obe = (ETipoTarjeta)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoTarjeta frm = new frmManteTipoTarjeta();
            frm.MiEvento += new frmManteTipoTarjeta.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstTipoTarjeta = lstTipoTarjeta;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            ETipoTarjeta Obe = (ETipoTarjeta)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTipoTarjeta frm = new frmManteTipoTarjeta();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                ETipoTarjeta Obe = (ETipoTarjeta)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewUnidadMedida.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el tipo de tarjeta " + Obe.tcrc_vdescripcion_tipo_tarjeta_cred + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarTipoTarjeta(Obe);
                    cargar();
                    if (lstTipoTarjeta.Count >= index + 1)
                        viewUnidadMedida.FocusedRowHandle = index;
                    else
                        viewUnidadMedida.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {

        }

        private void buscarCriterio()
        {
            grdUnidadMedida.DataSource = lstTipoTarjeta.Where(x =>
                                                   x.tcrc_iid_tipo_tarjeta_cred.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tcrc_vdescripcion_tipo_tarjeta_cred.Contains(txtDescripcion.Text.ToUpper())
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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewUnidadMedida.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewUnidadMedida.ClearColumnsFilter();
        }
    }
}