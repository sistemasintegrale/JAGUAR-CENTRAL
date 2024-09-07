using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm03UnidadMedida : DevExpress.XtraEditors.XtraForm
    {
        List<EUnidadMedida> lstUnidadMedida = new List<EUnidadMedida>();
        public frm03UnidadMedida()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstUnidadMedida = new BAlmacen().listarUnidadMedida();
            grdUnidadMedida.DataSource = lstUnidadMedida;
            viewUnidadMedida.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstUnidadMedida.FindIndex(x => x.unidc_icod_unidad_medida == intIcod);
            viewUnidadMedida.FocusedRowHandle = index;
            viewUnidadMedida.Focus();
        }
        private void nuevo()
        {
            frmManteUnidadMedida frm = new frmManteUnidadMedida();
            frm.MiEvento += new frmManteUnidadMedida.DelegadoMensaje(reload);
            frm.lstUnidadMedida = lstUnidadMedida;
            frm.SetInsert();
            frm.Show();
            frm.txtCodigo.Focus();
        }
        private void modificar()
        {
            EUnidadMedida Obe = (EUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteUnidadMedida frm = new frmManteUnidadMedida();
            frm.MiEvento += new frmManteUnidadMedida.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstUnidadMedida = lstUnidadMedida;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EUnidadMedida Obe = (EUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteUnidadMedida frm = new frmManteUnidadMedida();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                EUnidadMedida Obe = (EUnidadMedida)viewUnidadMedida.GetRow(viewUnidadMedida.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewUnidadMedida.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Unidad de Medida " + Obe.unidc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarUnidadMedida(Obe);
                    cargar();
                    if (lstUnidadMedida.Count >= index + 1)
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
            grdUnidadMedida.DataSource = lstUnidadMedida.Where(x =>
                                                   x.unidc_vabreviatura_unidad_medida.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.unidc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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