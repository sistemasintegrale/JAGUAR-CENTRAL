using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using SGE.WindowForms.Reportes.Almacen.Registros;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm07NotaSalida : DevExpress.XtraEditors.XtraForm
    {
        private List<ENotaSalida> lstNotasalida = new List<ENotaSalida>();

        public frm07NotaSalida()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstNotasalida = new BAlmacen().listarNotaSalida(Parametros.intEjercicio, Convert.ToInt32(lkpAlmacen.EditValue), null, null);
            grdNotaSalida.DataSource = lstNotasalida;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotasalida.FindIndex(x => x.nsalc_icod_nota_salida == intIcod);
            viewNotaSalida.FocusedRowHandle = index;
            viewNotaSalida.Focus();
        }

        private void viewNotasalida_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ENotaSalida Obe = (ENotaSalida)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteNotaSalida frm = new frmManteNotaSalida();
                frm.MiEvento += new frmManteNotaSalida.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotasalida;
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
                frmManteNotaSalida frm = new frmManteNotaSalida();
                frm.MiEvento += new frmManteNotaSalida.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotasalida;
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
                ENotaSalida Obe = (ENotaSalida)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteNotaSalida frm = new frmManteNotaSalida();
                frm.MiEvento += new frmManteNotaSalida.DelegadoMensaje(reload);
                frm.lstCabecerasNI = lstNotasalida;
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
                ENotaSalida Obe = (ENotaSalida)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewNotaSalida.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar nota de salida " + Obe.nsalc_numero_nota_salida + "?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarNotaSalida(Obe);
                    cargar();
                    if (lstNotasalida.Count >= index + 1)
                        viewNotaSalida.FocusedRowHandle = index;
                    else
                        viewNotaSalida.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void imprimir()
        {
            ENotaSalida obe = (ENotaSalida)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
            if (obe == null)
                return;
            var lstDetalle = new BAlmacen().listarNotaSalidaDetalle(obe.nsalc_icod_nota_salida);
            rptNotaSalida rpt = new rptNotaSalida();
            rpt.cargar(String.Format("NOTA DE SALIDA N° {0}", obe.nsalc_numero_nota_salida), obe.strAlmacen, lstDetalle, obe);

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

        private void frm07NotaSalida_Load(object sender, EventArgs e)
        {
            var lstAlmacenes = new BAlmacen().listarAlmacenes();
            EAlmacen obe = new EAlmacen() { almac_vdescripcion = "TODOS...", almac_icod_almacen = 0 };
            lstAlmacenes.Insert(0, obe);
            BSControls.LoaderLook(lkpAlmacen, lstAlmacenes, "almac_vdescripcion", "almac_icod_almacen", true);
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
            viewNotaSalida.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewNotaSalida.ClearColumnsFilter();
        }
    }
}