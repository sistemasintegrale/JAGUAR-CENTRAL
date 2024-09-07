using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class frm01AlmacenContable : DevExpress.XtraEditors.XtraForm
    {
        List<EAlmacenContable> lstAlmacenes = new List<EAlmacenContable>();

        public frm01AlmacenContable()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstAlmacenes = new BContabilidad().listarAlmacenContable();
            grdAlmacen.DataSource = lstAlmacenes;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstAlmacenes.FindIndex(x => x.almcc_icod_almacen == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        }

        private void nuevo()
        {
            frmManteAlmacenContable frm = new frmManteAlmacenContable();
            frm.MiEvento += new frmManteAlmacenContable.DelegadoMensaje(reload);
            if (lstAlmacenes.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstAlmacenes.Max(x => x.almcc_iid_almacen + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstAlmacenes = lstAlmacenes;
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            EAlmacenContable Obe = (EAlmacenContable)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAlmacenContable frm = new frmManteAlmacenContable();
            frm.MiEvento += new frmManteAlmacenContable.DelegadoMensaje(reload);
            frm.lstAlmacenes = lstAlmacenes;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EAlmacenContable Obe = (EAlmacenContable)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAlmacenContable frm = new frmManteAlmacenContable();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                EAlmacenContable Obe = (EAlmacenContable)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el almacén " + Obe.almcc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BContabilidad().eliminarAlmacenContable(Obe);
                    cargar();
                    if (lstAlmacenes.Count >= index + 1)
                        viewAlmacen.FocusedRowHandle = index;
                    else
                        viewAlmacen.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            //if (lstAlmacenes.Count > 0)
            //{
            //    rptAlmacen rpt = new rptAlmacen();
            //    rpt.cargar("RELACIÓN DE ALMACENES", "", lstAlmacenes);
            //}

        }

        private void filtrar()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.almcc_iid_almacen.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.almcc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
            viewAlmacen.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewAlmacen.ClearColumnsFilter();
        }
    }
}