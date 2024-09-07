using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Caracteristicas
{
    public partial class FrmTabla : DevExpress.XtraEditors.XtraForm
    {
        private List<EProdTabla> lstTabla = new List<EProdTabla>();
        public FrmTabla()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTabla = new BCentral().listarProdTabla().Where(x => x.tablc_iid_tipo_tabla != 11).ToList();
            grdTabla.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.tablc_iid_tipo_tabla == intIcod);
            viewTabla.FocusedRowHandle = index;
            viewTabla.Focus();
        }
        private void nuevo()
        {
            FrmManteProdTabla frm = new FrmManteProdTabla();
            frm.MiEvento += new FrmManteProdTabla.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.SetInsert();
            frm.Show();

        }

        private void modificar()
        {
            EProdTabla Obe = (EProdTabla)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            FrmManteProdTabla frm = new FrmManteProdTabla();
            frm.MiEvento += new FrmManteProdTabla.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();

        }
        private void eliminar()
        {
            EProdTabla Obe = (EProdTabla)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BCentral().eliminarProdTabla(Obe);
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
        private void listarTablaRegistro()
        {
            EProdTabla Obe = (EProdTabla)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;

            FrmProdTablaRegistro frm = new FrmProdTablaRegistro();
            frm.intTabla = Obe.tablc_iid_tipo_tabla;
            frm.Show();
        }
        private void detalleDeTablaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarTablaRegistro();
        }

        private void btnDetalle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listarTablaRegistro();
        }
        private void buscarCriterio()
        {
            grdTabla.DataSource = lstTabla.Where(obj =>
                                                   obj.tablc_iid_tipo_tabla.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.tablc_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
    }
}