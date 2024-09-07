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
    public partial class frm06NotaIngreso : DevExpress.XtraEditors.XtraForm
    {
        private List<ENotaIngreso> lstNotaIngreso = new List<ENotaIngreso>();

        public frm06NotaIngreso()
        {
            InitializeComponent();
        }

        private void FrmRegistroNotaIngreso_Load(object sender, EventArgs e)
        {
            var lstAlmacenes = new BAlmacen().listarAlmacenes();
            EAlmacen obe = new EAlmacen() { almac_vdescripcion = "TODOS...", almac_icod_almacen = 0 };
            lstAlmacenes.Insert(0, obe);
            BSControls.LoaderLook(lkpAlmacen, lstAlmacenes, "almac_vdescripcion", "almac_icod_almacen", true);
            cargar();
        }

        private void cargar()
        {
            lstNotaIngreso = new BAlmacen().listarNotaIngreso(Parametros.intEjercicio, Convert.ToInt32(lkpAlmacen.EditValue), null, null).Where(ob => Convert.ToInt32(ob.fcoc_icod_doc) == 0).ToList();
            grdNotaIngreso.DataSource = lstNotaIngreso;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaIngreso.FindIndex(x => x.ningc_icod_nota_ingreso == intIcod);
            viewNotaIngreso.FocusedRowHandle = index;
            viewNotaIngreso.Focus();
        }

        private void viewNotaIngreso_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ENotaIngreso Obe = (ENotaIngreso)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteNotaIngreso frm = new frmManteNotaIngreso();
                frm.MiEvento += new frmManteNotaIngreso.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotaIngreso;
                frm.oBe = Obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
                frm.txtReferencia.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nuevo()
        {
            try
            {
                frmManteNotaIngreso frm = new frmManteNotaIngreso();
                frm.MiEvento += new frmManteNotaIngreso.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotaIngreso;
                frm.Show();
                frm.SetInsert();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificar()
        {
            try
            {

                ENotaIngreso Obe = (ENotaIngreso)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteNotaIngreso frm = new frmManteNotaIngreso();
                frm.MiEvento += new frmManteNotaIngreso.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotaIngreso;
                frm.oBe = Obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.txtReferencia.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void eliminar()
        {
            try
            {
                ENotaIngreso Obe = (ENotaIngreso)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewNotaIngreso.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar nota de ingreso " + Obe.ningc_numero_nota_ingreso + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarNotaIngreso(Obe);
                    cargar();
                    if (lstNotaIngreso.Count >= index + 1)
                        viewNotaIngreso.FocusedRowHandle = index;
                    else
                        viewNotaIngreso.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            ENotaIngreso obe = (ENotaIngreso)viewNotaIngreso.GetRow(viewNotaIngreso.FocusedRowHandle);
            if (obe == null)
                return;
            var lstDetalle = new BAlmacen().listarNotaIngresoDetalle(obe.ningc_icod_nota_ingreso);
            rptNotaIngreso rpt = new rptNotaIngreso();
            rpt.cargar(String.Format("NOTA DE INGRESO N° {0}", obe.ningc_numero_nota_ingreso), obe.strAlmacen, lstDetalle, obe);

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

        private void lkpAlmacen_EditValueChanged(object sender, EventArgs e)
        {
            cargar();
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

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewNotaIngreso.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewNotaIngreso.ClearColumnsFilter();
        }
    }
}