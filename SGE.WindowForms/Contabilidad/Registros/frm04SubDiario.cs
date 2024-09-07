using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Contabilidad;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Registros
{
    public partial class frm04SubDiario : DevExpress.XtraEditors.XtraForm
    {
        List<ESubDiario> lstSubDiario = new List<ESubDiario>();
        public frm04SubDiario()
        {
            InitializeComponent();
        }

        private void frm01CentroCosto_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstSubDiario = new BContabilidad().listarSubDiario();
            grdSubDiario.DataSource = lstSubDiario;
            viewSubDiario.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstSubDiario.FindIndex(x => x.subdi_icod_subdiario == intIcod);
            viewSubDiario.FocusedRowHandle = index;
            viewSubDiario.Focus();
        }
        private void nuevo()
        {
            frmManteSubDiario frm = new frmManteSubDiario();
            frm.MiEvento += new frmManteSubDiario.DelegadoMensaje(reload);
            frm.lstSubDiario = lstSubDiario;
            frm.SetInsert();
            frm.Show();
        }
        private void modificar()
        {
            ESubDiario Obe = (ESubDiario)viewSubDiario.GetRow(viewSubDiario.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteSubDiario frm = new frmManteSubDiario();
            frm.MiEvento += new frmManteSubDiario.DelegadoMensaje(reload);
            frm.lstSubDiario = lstSubDiario;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewCentroCosto_DoubleClick(object sender, EventArgs e)
        {
            ESubDiario Obe = (ESubDiario)viewSubDiario.GetRow(viewSubDiario.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteSubDiario frm = new frmManteSubDiario();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                ESubDiario Obe = (ESubDiario)viewSubDiario.GetRow(viewSubDiario.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewSubDiario.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BContabilidad().eliminarSubDiario(Obe);
                    cargar();
                    if (lstSubDiario.Count >= index + 1)
                        viewSubDiario.FocusedRowHandle = index;
                    else
                        viewSubDiario.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            if (lstSubDiario.Count > 0)
            {
                rpt04Subdiario rpt = new rpt04Subdiario();
                rpt.cargar(lstSubDiario);
            }
        }
        private void buscarCriterio()
        {
            grdSubDiario.DataSource = lstSubDiario.Where(x =>
                                                   x.subdi_icod_subdiario.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.subdi_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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