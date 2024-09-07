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

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frm03Transportista : DevExpress.XtraEditors.XtraForm
    {
        List<ETransportista> lstTransportista = new List<ETransportista>();

        public frm03Transportista()
        {
            InitializeComponent();
        }
        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstTransportista = new BVentas().listarTransportista();
            grdTransportista.DataSource = lstTransportista;
            viewTransportista.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTransportista.FindIndex(x => x.tranc_icod_transportista == intIcod);
            viewTransportista.FocusedRowHandle = index;
            viewTransportista.Focus();
        }
        private void nuevo()
        {
            frmManteTransportista frm = new frmManteTransportista();
            frm.MiEvento += new frmManteTransportista.DelegadoMensaje(reload);
            frm.lstTransportista = lstTransportista;
            frm.txtCodigo.Text = (lstTransportista.Count == 0) ? "001" : lstTransportista.Max(x => x.tranc_iid_transportista + 1);
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }
        private void modificar()
        {
            ETransportista Obe = (ETransportista)viewTransportista.GetRow(viewTransportista.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTransportista frm = new frmManteTransportista();
            frm.MiEvento += new frmManteTransportista.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstTransportista = lstTransportista;
            frm.SetModify();
            frm.Show();
            frm.setValues();

        }

        private void ver()
        {
            ETransportista Obe = (ETransportista)viewTransportista.GetRow(viewTransportista.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteTransportista frm = new frmManteTransportista();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }

        private void eliminar()
        {
            try
            {
                ETransportista Obe = (ETransportista)viewTransportista.GetRow(viewTransportista.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewTransportista.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el transportista " + Obe.tranc_vnombre_transportista + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarTransportista(Obe);
                    cargar();
                    if (lstTransportista.Count >= index + 1)
                        viewTransportista.FocusedRowHandle = index;
                    else
                        viewTransportista.FocusedRowHandle = index - 1;
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

        private void filtrar()
        {
            grdTransportista.DataSource = lstTransportista.Where(x =>
                                                   x.tranc_iid_transportista.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.tranc_vnombre_transportista.Contains(txtDescripcion.Text.ToUpper())
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
            filtrar();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewTransportista.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewTransportista.ClearColumnsFilter();
        }

        private void grdTransportista_Click(object sender, EventArgs e)
        {

        }
    }
}