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
    public partial class FrmAreaEmpresa : DevExpress.XtraEditors.XtraForm
    {
        private List<EAreaEmpresa> lstTabla = new List<EAreaEmpresa>();
        public FrmAreaEmpresa()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTabla = new BCentral().listarAreaEmpresa().Where(x => x.aremc_iid_codigo != 11).ToList();
            grdTabla.DataSource = lstTabla;
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstTabla.FindIndex(x => x.aremc_iid_codigo == intIcod);
            viewTabla.FocusedRowHandle = index;
            viewTabla.Focus();
        }
        private void nuevo()
        {
            FrmManteAreaEmpresa frm = new FrmManteAreaEmpresa();
            frm.MiEvento += new FrmManteAreaEmpresa.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.SetInsert();
            frm.Show();

        }

        private void modificar()
        {
            EAreaEmpresa Obe = (EAreaEmpresa)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            FrmManteAreaEmpresa frm = new FrmManteAreaEmpresa();
            frm.MiEvento += new FrmManteAreaEmpresa.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();

        }
        private void eliminar()
        {
            EAreaEmpresa Obe = (EAreaEmpresa)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BCentral().eliminarAreaEmpresa(Obe);
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
        private void listarSubAreaEmpresa()
        {
            EAreaEmpresa Obe = (EAreaEmpresa)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;

            FrmSubAreaEmpresa frm = new FrmSubAreaEmpresa();
            frm.intTabla = Obe.aremc_iid_codigo;
            frm.Show();
        }
        private void detalleDeTablaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarSubAreaEmpresa();
        }

        private void btnDetalle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listarSubAreaEmpresa();
        }
        private void buscarCriterio()
        {
            grdTabla.DataSource = lstTabla.Where(obj =>
                                                   obj.aremc_iid_codigo.ToString().ToUpper().Contains(txtCodigo.Text.ToUpper()) &&
                                                   obj.aremc_vdescripcion.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
        private void listarAreaDescripción()
        {
            EAreaProduccion Obe = (EAreaProduccion)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;

            FrmAreaDescripción frm = new FrmAreaDescripción();
            frm.intTabla = Obe.arprc_iid_codigo;
            frm.Show();
        }

        private void detalleDeDescripciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listarAreaDescripción();
        }
    }
}