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
    public partial class FrmProdTablaRegistro : DevExpress.XtraEditors.XtraForm
    {
        public List<EProdTablaRegistro> lstTabla = new List<EProdTablaRegistro>();
        public int intTabla;
        public FrmProdTablaRegistro()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            EProdTablaRegistro obe = new EProdTablaRegistro() { iid_tipo_tabla = intTabla };
            lstTabla = new BCentral().ListarProdTablaRegistro(obe);
            grdTablaRegistro.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.tarec_iid_tabla_registro == intIcod);
            viewTablaRegistro.FocusedRowHandle = index;
            viewTablaRegistro.Focus();
        }
        private void nuevo()
        {
            FrmManteProdTablaRegistro frm = new FrmManteProdTablaRegistro();
            frm.MiEvento += new FrmManteProdTablaRegistro.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            if (lstTabla.Count == 0)
            {
                frm.txtCodigo.Text = "001";
            }
            else
            {
                frm.txtCodigo.Text = String.Format("{0:000}", (lstTabla.Max(ob => Convert.ToInt32(ob.tarec_iid_correlativo)) + 1));
            }
            frm.SetInsert();
            frm.txtDescripcion.Focus();
            frm.Show();
            frm.txtDescripcion.Focus();

        }

        private void modificar()
        {
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            FrmManteProdTablaRegistro frm = new FrmManteProdTablaRegistro();
            frm.MiEvento += new FrmManteProdTablaRegistro.DelegadoMensaje(reload);
            frm.intTabla = intTabla;
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();

        }
        private void eliminar()
        {
            EProdTablaRegistro Obe = (EProdTablaRegistro)viewTablaRegistro.GetRow(viewTablaRegistro.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BCentral().EliminarProdTablaRegistro(Obe);
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
                                                   obj.tarec_iid_correlativo.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.descripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

    }
}