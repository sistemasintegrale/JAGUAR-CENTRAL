using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
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
    public partial class frm02FamiliaProducto : DevExpress.XtraEditors.XtraForm
    {
        List<EFamiliaCab> lstFamiliaCab = new List<EFamiliaCab>();
        List<EFamiliaDet> lstFamiliaDet = new List<EFamiliaDet>();

        public frm02FamiliaProducto()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
            cargarGridSize();
        }

        private void cargarGridSize()
        {
            grdFamilia.Height = (this.Height) / 2;
            //grdSubFamilia.Height = (this.Height) / 2 + 10;
        }

        private void cargar()
        {
            lstFamiliaCab = new BAlmacen().listarFamiliaCab();
            if (lstFamiliaCab.Count == 1)
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(lstFamiliaCab[0].famic_icod_familia);

            grdFamilia.DataSource = lstFamiliaCab;
            viewFamilia.Focus();
        }
        #region Familia Cab
        void reload(int intIcod)
        {
            cargar();
            int index = lstFamiliaCab.FindIndex(x => x.famic_icod_familia == intIcod);
            viewFamilia.FocusedRowHandle = index;
            viewFamilia.Focus();
        }
        private void nuevoFamilia()
        {
            frmManteFamiliaCab frm = new frmManteFamiliaCab();
            frm.MiEvento += new frmManteFamiliaCab.DelegadoMensaje(reload);
            frm.lstFamiliaCab = lstFamiliaCab;
            if (lstFamiliaCab.Count == 0)
            {
                frm.txtAbreviatura.Text = "001";
            }
            else
            {
                frm.txtAbreviatura.Text = String.Format("{0:000}", (lstFamiliaCab.Max(ob => Convert.ToInt32(ob.famic_vabreviatura)) + 1));
            }
            frm.SetInsert();
            frm.Show();
            frm.txtAbreviatura.Focus();

        }
        private void modificarFamilia()
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteFamiliaCab frm = new frmManteFamiliaCab();
            frm.MiEvento += new frmManteFamiliaCab.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.lstFamiliaCab = lstFamiliaCab;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();

        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteFamiliaCab frm = new frmManteFamiliaCab();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminarFamilia()
        {
            try
            {
                EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewFamilia.FocusedRowHandle;
                var lstProducto = new BAlmacen().listarProducto(Parametros.intEjercicio);
                if (lstProducto.Where(x => x.famic_icod_familia == Obe.famic_icod_familia).ToList().Count > 0)
                {
                    XtraMessageBox.Show("La Familia no puede ser eliminada porque ha sido usada en los registros de productos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (lstFamiliaDet.Count > 0)
                {
                    XtraMessageBox.Show("La Familia no puede ser eliminada porque existen sub-familias asociadas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Línea " + Obe.famic_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarFamiliaCab(Obe);
                    cargar();
                    if (lstFamiliaCab.Count >= index + 1)
                        viewFamilia.FocusedRowHandle = index;
                    else
                        viewFamilia.FocusedRowHandle = index - 1;

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimirFamilia()
        {

        }
        private void buscarCriterio()
        {
            grdFamilia.DataSource = lstFamiliaCab.Where(x =>
                                                   x.famic_vabreviatura.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.famic_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();

            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia);
                grdSubFamilia.DataSource = lstFamiliaDet;
                viewSubFamilia.GroupPanelText = String.Format("Sub-Líneas de : {0} - {1}", Obe.famic_vabreviatura, Obe.famic_vdescripcion);
            }


        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoFamilia();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificarFamilia();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eliminarFamilia();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewFamilia.IsFocusedView)
                nuevoFamilia();
            if (viewSubFamilia.IsFocusedView)
                nuevoSubFamilia();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewFamilia.IsFocusedView)
                modificarFamilia();
            if (viewSubFamilia.IsFocusedView)
                modificarSubFamilia();
        }

        private void btnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (viewFamilia.IsFocusedView)
                eliminarFamilia();
            if (viewSubFamilia.IsFocusedView)
                eliminarSubFamilia();
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimirFamilia();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
        #endregion
        #region Familia Det
        void reloadSubFamilia(int intIcod)
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia);
            grdSubFamilia.DataSource = lstFamiliaDet;
            int index = lstFamiliaDet.FindIndex(x => x.famid_icod_familia_tipo == intIcod);
            viewSubFamilia.FocusedRowHandle = index;
            viewSubFamilia.GroupPanelText = String.Format("SUB-FAMILIAS DE : {0} - {1}", Obe.famic_vabreviatura, Obe.famic_vdescripcion);
            viewSubFamilia.Focus();
        }
        private void nuevoSubFamilia()
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe == null)
            {
                XtraMessageBox.Show("Para poder registrar Sub Líneas de Productos, debe registrar una Familia Producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmManteFamiliaDet frm = new frmManteFamiliaDet();
            frm.MiEvento += new frmManteFamiliaDet.DelegadoMensaje(reloadSubFamilia);
            frm.intIcodFamiliaCab = Obe.famic_icod_familia;
            frm.lstFamiliaDet = lstFamiliaDet;
            if (lstFamiliaDet.Count == 0)
            {
                frm.txtAbreviatura.Text = "001";
            }
            else
            {
                frm.txtAbreviatura.Text = String.Format("{0:000}", (lstFamiliaDet.Max(ob => Convert.ToInt32(ob.famid_vabreviatura)) + 1));
            }
            frm.SetInsert();
            frm.Text = String.Format("Mantenimiento - Registro de Sub Línea Tipo de {0}", Obe.famic_vdescripcion);
            frm.Show();
            frm.txtAbreviatura.Focus();
            frm.bteCuentaSerPropio.Enabled = false;

        }
        private void modificarSubFamilia()
        {
            EFamiliaCab ObeF = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            EFamiliaDet Obe = (EFamiliaDet)viewSubFamilia.GetRow(viewSubFamilia.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteFamiliaDet frm = new frmManteFamiliaDet();
            frm.MiEvento += new frmManteFamiliaDet.DelegadoMensaje(reloadSubFamilia);
            frm.lstFamiliaDet = lstFamiliaDet;
            frm.intIcodFamiliaCab = ObeF.famic_icod_familia;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Text = String.Format("Mantenimiento - Registro de Sub Línea Tipo de {0}", ObeF.famic_vdescripcion);
            frm.Show();
            frm.setValues();
            frm.txtDescripcion.Focus();
            frm.bteCuentaSerPropio.Enabled = false;

        }
        private void eliminarSubFamilia()
        {
            try
            {
                EFamiliaDet Obe = (EFamiliaDet)viewSubFamilia.GetRow(viewSubFamilia.FocusedRowHandle);
                if (Obe == null)
                    return;
                var lstProducto = new BAlmacen().listarProducto(Parametros.intEjercicio);
                int index = viewSubFamilia.FocusedRowHandle;
                if (lstProducto.Where(x => x.famid_icod_familia == Obe.famid_icod_familia_tipo).ToList().Count > 0)
                {
                    XtraMessageBox.Show("La Sub-Familia no puede ser eliminada porque ha sido usada en los registros de productos", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Sub Línea " + Obe.famid_vdescripcion + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarFamiliaDet(Obe);
                    reloadSubFamilia(0);
                    if (lstFamiliaDet.Count >= index + 1)
                        viewSubFamilia.FocusedRowHandle = index;
                    else
                        viewSubFamilia.FocusedRowHandle = index - 1;

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nuevoSubFamilia();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            modificarSubFamilia();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            eliminarSubFamilia();
        }

        #endregion

        private void viewFamilia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia);
                grdSubFamilia.DataSource = lstFamiliaDet;
                viewSubFamilia.GroupPanelText = String.Format("Sub-Líneas de : {0} - {1}", Obe.famic_vabreviatura, Obe.famic_vdescripcion);
            }
        }

        private void impFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void impSubfamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rptFamilia rpt = new rptFamilia();
            rpt.cargar("RELACIÓN DE LINEAS DE PRODUCTOS", "", lstFamiliaCab);
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewFamilia.ClearColumnsFilter();

            viewSubFamilia.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewSubFamilia.ClearColumnsFilter();
        }

        private void viewFamilia_FocusedRowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            EFamiliaCab Obe = (EFamiliaCab)viewFamilia.GetRow(viewFamilia.FocusedRowHandle);
            if (Obe != null)
            {
                lstFamiliaDet = new BAlmacen().listarFamiliaDet(Obe.famic_icod_familia);
                grdSubFamilia.DataSource = lstFamiliaDet;
                viewSubFamilia.GroupPanelText = String.Format("Sub-Líneas de : {0} - {1}", Obe.famic_vabreviatura, Obe.famic_vdescripcion);
            }
        }
    }
}