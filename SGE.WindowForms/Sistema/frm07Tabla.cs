using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Administracion_del_Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Sistema
{
    public partial class frm07Tabla : DevExpress.XtraEditors.XtraForm
    {
        private List<ETabla> lstTabla = new List<ETabla>();
        public frm07Tabla()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstTabla = new BAdministracionSistema().listarTabla();
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
            frmManteTabla frm = new frmManteTabla();
            frm.MiEvento += new frmManteTabla.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.SetInsert();
            frm.Show();

        }

        private void modificar()
        {
            ETabla Obe = (ETabla)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            frmManteTabla frm = new frmManteTabla();
            frm.MiEvento += new frmManteTabla.DelegadoMensaje(reload);
            frm.lstTabla = lstTabla;
            frm.Obe = Obe;
            frm.Show();
            frm.setValues();
            frm.SetModify();

        }
        private void eliminar()
        {
            ETabla Obe = (ETabla)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;
            if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                new BAdministracionSistema().eliminarTabla(Obe);
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
            ETabla Obe = (ETabla)viewTabla.GetRow(viewTabla.FocusedRowHandle);
            if (Obe == null)
                return;

            frmTablaRegistro frm = new frmTablaRegistro();
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