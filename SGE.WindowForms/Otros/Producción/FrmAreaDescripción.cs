using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmAreaDescripción : DevExpress.XtraEditors.XtraForm
    {
        public List<EAreaDescripcion> lstTabla = new List<EAreaDescripcion>();
        public int intTabla;
        public FrmAreaDescripción()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            EAreaDescripcion obe = new EAreaDescripcion() { arprc_iid_codigo = intTabla };
            lstTabla = new BCentral().ListarAreaDescripcion(obe);
            grdTablaRegistro.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.arded_iid_area_descripcion == intIcod);
            viewTablaRegistro.FocusedRowHandle = index;
            viewTablaRegistro.Focus();
        }
        private void nuevo()
        {
            FrmManteAreaDescripción frm = new FrmManteAreaDescripción();
            frm.MiEvento += new FrmManteAreaDescripción.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            if (lstTabla.Count == 0)
            {
                frm.txtCodigo.Text = "001";
            }
            else
            {
                frm.txtCodigo.Text = String.Format("{0:000}", (lstTabla.Max(ob => Convert.ToInt32(ob.arded_codigo)) + 1));
            }
            frm.SetInsert();
            frm.txtDescripcion.Focus();
            frm.Show();
            frm.txtDescripcion.Focus();

        }

        private void modificar()
        {
            EAreaDescripcion Obe = (EAreaDescripcion)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteAreaDescripción frm = new FrmManteAreaDescripción();
            frm.MiEvento += new FrmManteAreaDescripción.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();

        }
        private void eliminar()
        {
            EAreaDescripcion Obe = (EAreaDescripcion)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BCentral().EliminarAreaDescripcion(Obe);
                cargar();
            }


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

        private void tablaDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        private void buscarCriterio()
        {
            grdTablaRegistro.DataSource = lstTabla.Where(obj =>
                                                   obj.arded_codigo.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.descripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

    }
}