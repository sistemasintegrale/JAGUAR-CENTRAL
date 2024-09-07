using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Mantenimiento
{
    public partial class frm02AnioEjercicio : DevExpress.XtraEditors.XtraForm
    {
        List<EAnioEjercicio> lstAnioEjercicio = new List<EAnioEjercicio>();
        public frm02AnioEjercicio()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            lstAnioEjercicio = new BContabilidad().listarAnioEjercicio();
            grdAnioEjercicio.DataSource = lstAnioEjercicio;
        }
        private void rptFrm02AnioEjercicio_Load(object sender, EventArgs e)
        {
            cargar();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstAnioEjercicio.FindIndex(x => x.anioc_icod_anio_ejercicio == intIcod);
            viewAnioEjercicio.FocusedRowHandle = index;
            viewAnioEjercicio.Focus();
        }

        private void nuevo()
        {
            frmManteAnioEjercicio frm = new frmManteAnioEjercicio();
            frm.MiEvento += new frmManteAnioEjercicio.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.lstAnioEjercicio = lstAnioEjercicio;
            frm.Show();

        }

        private void modificar()
        {
            EAnioEjercicio Obe = (EAnioEjercicio)viewAnioEjercicio.GetRow(viewAnioEjercicio.FocusedRowHandle);
            if (Obe == null)
                return;

            frmManteAnioEjercicio frm = new frmManteAnioEjercicio();
            frm.MiEvento += new frmManteAnioEjercicio.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstAnioEjercicio = lstAnioEjercicio;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }

        private void eliminar()
        {
            try
            {
                EAnioEjercicio Obe = (EAnioEjercicio)viewAnioEjercicio.GetRow(viewAnioEjercicio.FocusedRowHandle);
                if (Obe != null)
                {
                    int index = viewAnioEjercicio.FocusedRowHandle;
                    if (XtraMessageBox.Show("¿Está seguro que desea eliminar el año de ejercicio?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        new BContabilidad().eliminarAnioEjercicio(Obe);
                        cargar();
                        if (lstAnioEjercicio.Count >= index + 1)
                            viewAnioEjercicio.FocusedRowHandle = index;
                        else
                            viewAnioEjercicio.FocusedRowHandle = index - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void view()
        {
            EAnioEjercicio Obe = (EAnioEjercicio)viewAnioEjercicio.GetRow(viewAnioEjercicio.FocusedRowHandle);
            if (Obe == null)
                return;

            frmManteAnioEjercicio frm = new frmManteAnioEjercicio();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.setValues();
        }

        private void imprimir()
        {
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

        private void viewAnioEjercicio_DoubleClick(object sender, EventArgs e)
        {
            view();
        }

        private void txtAnio_KeyUp(object sender, KeyEventArgs e)
        {
            if (lstAnioEjercicio.Count == 0)
                return;
            grdAnioEjercicio.DataSource = lstAnioEjercicio.Where(x => x.anioc_iid_anio_ejercicio.ToString().Contains(txtAnio.Text)).ToList();
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
    }
}