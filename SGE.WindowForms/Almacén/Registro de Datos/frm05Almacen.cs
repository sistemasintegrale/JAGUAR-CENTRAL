using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Reportes.Almacen.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm05Almacen : DevExpress.XtraEditors.XtraForm
    {
        List<EAlmacen> lstAlmacenes = new List<EAlmacen>();
        public frm05Almacen()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstAlmacenes = new BAlmacen().listarAlmacenes();
            grdAlmacen.DataSource = lstAlmacenes;
            viewAlmacen.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstAlmacenes.FindIndex(x => x.almac_icod_almacen == intIcod);
            viewAlmacen.FocusedRowHandle = index;
            viewAlmacen.Focus();
        }
        private void nuevo()
        {
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            if (lstAlmacenes.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:00}", lstAlmacenes.Max(x => x.almac_iid_almacen + 1));
            else
                frm.txtCodigo.Text = "01";
            frm.lstAlmacenes = lstAlmacenes;
            frm.SetInsert();
            frm.Show();
            frm.txtNombre.Focus();
        }
        private void modificar()
        {

            EAlmacen Obe = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.MiEvento += new frmManteAlmacen.DelegadoMensaje(reload);
            cargar();
            frm.lstAlmacenes = lstAlmacenes;
            frm.Obe = Obe;
            frm.setValues();
            frm.SetModify();
            frm.Show();
            frm.txtNombre.Focus();

        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EAlmacen Obe = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteAlmacen frm = new frmManteAlmacen();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                EAlmacen Obe = (EAlmacen)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewAlmacen.FocusedRowHandle;
                List<EKardex> lstKardex = new List<EKardex>();
                lstKardex = new BAlmacen().listarkardex();
                lstKardex.ForEach(x =>
                {
                    if (x.almac_icod_almacen == Obe.almac_icod_almacen)
                    {
                        throw new ArgumentException(String.Format("El Almacen no se puede eliminar, cuenta con movimientos : {0}", Obe.almac_vdescripcion));
                    }
                });
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el almacén " + Obe.almac_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarAlmacen(Obe);
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
            if (lstAlmacenes.Count > 0)
            {
                rptAlmacen rpt = new rptAlmacen();
                rpt.cargar("RELACIÓN DE ALMACENES", "", lstAlmacenes);
            }
        }
        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstAlmacenes.Where(x =>
                                                   x.almac_iid_almacen.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.almac_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
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
            viewAlmacen.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewAlmacen.ClearColumnsFilter();
        }

        private void Refresacar()
        {
            int xposition = viewAlmacen.FocusedRowHandle;
            cargar();
            viewAlmacen.FocusedRowHandle = xposition;
        }


    }
}