using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm01CentroCosto : DevExpress.XtraEditors.XtraForm
    {
        List<ECentroCosto> lstCentroCosto = new List<ECentroCosto>();
        public frm01CentroCosto()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstCentroCosto = new BContabilidad().listarCentroCosto();
            grdCentroCosto.DataSource = lstCentroCosto;
            viewCentroCosto.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstCentroCosto.FindIndex(x => x.cecoc_icod_centro_costo == intIcod);
            viewCentroCosto.FocusedRowHandle = index;
            viewCentroCosto.Focus();
        }
        private void nuevo()
        {
            frmManteCentroCosto frm = new frmManteCentroCosto();
            frm.MiEvento += new frmManteCentroCosto.DelegadoMensaje(reload);
            frm.lstCentroCosto = lstCentroCosto;
            frm.SetInsert();
            frm.Show();
        }
        private void modificar()
        {
            ECentroCosto Obe = (ECentroCosto)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteCentroCosto frm = new frmManteCentroCosto();
            frm.MiEvento += new frmManteCentroCosto.DelegadoMensaje(reload);
            frm.lstCentroCosto = lstCentroCosto;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            ECentroCosto Obe = (ECentroCosto)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteCentroCosto frm = new frmManteCentroCosto();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                ECentroCosto Obe = (ECentroCosto)viewCentroCosto.GetRow(viewCentroCosto.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewCentroCosto.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BContabilidad().eliminarCentroCosto(Obe);
                    cargar();
                    if (lstCentroCosto.Count >= index + 1)
                        viewCentroCosto.FocusedRowHandle = index;
                    else
                        viewCentroCosto.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            if (lstCentroCosto.Count > 0)
            {
                rpt01CentroCostos rpt = new rpt01CentroCostos();
                rpt.cargar(lstCentroCosto);
            }
        }
        private void buscarCriterio()
        {
            grdCentroCosto.DataSource = lstCentroCosto.Where(x =>
                                                   x.cecoc_vcodigo_centro_costo.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.cecoc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
    }
}