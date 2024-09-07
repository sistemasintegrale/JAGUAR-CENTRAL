using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmListarClasificacionProducto : DevExpress.XtraEditors.XtraForm
    {
        List<EClasificacionProducto> lstClasificacionProducto = new List<EClasificacionProducto>();
        public EClasificacionProducto _Be { get; set; }
        public frmListarClasificacionProducto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstClasificacionProducto = new BContabilidad().listarClasificacionProducto();
            grdClasificacion.DataSource = lstClasificacionProducto;
            viewClasificacion.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstClasificacionProducto.FindIndex(x => x.clasc_icod_clasificacion == intIcod);
            viewClasificacion.FocusedRowHandle = index;
            viewClasificacion.Focus();
        }

        private void nuevo()
        {
            frmManteClasificacion frm = new frmManteClasificacion();
            frm.MiEvento += new frmManteClasificacion.DelegadoMensaje(reload);
            if (lstClasificacionProducto.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstClasificacionProducto.Max(x => x.clasc_iid_clasificacion + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstClasificacionProducto = lstClasificacionProducto;
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            EClasificacionProducto Obe = (EClasificacionProducto)viewClasificacion.GetRow(viewClasificacion.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteClasificacion frm = new frmManteClasificacion();
            frm.MiEvento += new frmManteClasificacion.DelegadoMensaje(reload);
            frm.lstClasificacionProducto = lstClasificacionProducto;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();

        }

        private void returnSeleccion()
        {
            _Be = (EClasificacionProducto)viewClasificacion.GetRow(viewClasificacion.FocusedRowHandle);
            if (_Be == null)
                return;
            this.DialogResult = DialogResult.OK;
        }

        private void eliminar()
        {
            try
            {
                EClasificacionProducto Obe = (EClasificacionProducto)viewClasificacion.GetRow(viewClasificacion.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewClasificacion.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro " + Obe.clasc_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BContabilidad().eliminarClasificacionProducto(Obe);
                    cargar();
                    if (lstClasificacionProducto.Count >= index + 1)
                        viewClasificacion.FocusedRowHandle = index;
                    else
                        viewClasificacion.FocusedRowHandle = index - 1;
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
            grdClasificacion.DataSource = lstClasificacionProducto.Where(x =>
                                                   x.clasc_iid_clasificacion.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.clasc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
            viewClasificacion.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewClasificacion.ClearColumnsFilter();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}