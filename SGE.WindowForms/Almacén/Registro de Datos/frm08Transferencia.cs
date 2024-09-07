using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Registro_de_Datos
{
    public partial class frm08Transferencia : DevExpress.XtraEditors.XtraForm
    {
        private List<ETransferenciaAlmacen> lstTransferencias = new List<ETransferenciaAlmacen>();
        DateTime f1, f2;
        public frm08Transferencia()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            lstTransferencias = new BAlmacen().listarTransferenciaAlmacen(Parametros.intEjercicio, f1, f2);
            grdNotaSalida.DataSource = lstTransferencias;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstTransferencias.FindIndex(x => x.trfc_icod_transf == intIcod);
            viewNotaSalida.FocusedRowHandle = index;
            viewNotaSalida.Focus();
        }

        private void viewNotasalida_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ETransferenciaAlmacen Obe = (ETransferenciaAlmacen)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteTransferencia frm = new frmManteTransferencia();
                frm.MiEvento += new frmManteTransferencia.DelegadoMensaje(reload);
                frm.lstTransferencias = lstTransferencias;
                frm.oBe = Obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
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
                frmManteTransferencia frm = new frmManteTransferencia();
                frm.MiEvento += new frmManteTransferencia.DelegadoMensaje(reload);
                frm.lstTransferencias = lstTransferencias;
                frm.SetInsert();
                frm.Show();
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

                ETransferenciaAlmacen Obe = (ETransferenciaAlmacen)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmManteTransferencia frm = new frmManteTransferencia();
                frm.MiEvento += new frmManteTransferencia.DelegadoMensaje(reload);
                frm.lstTransferencias = lstTransferencias;
                frm.oBe = Obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();

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
                ETransferenciaAlmacen Obe = (ETransferenciaAlmacen)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewNotaSalida.FocusedRowHandle;
                if (XtraMessageBox.Show(String.Format("¿Esta seguro que desea eliminar Transerencia N° {0:0000} ?", Obe.trfc_inum_transf), "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BAlmacen().eliminarTransferenciaAlmacen(Obe);
                    cargar();
                    if (lstTransferencias.Count >= index + 1)
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
            ETransferenciaAlmacen obe = (ETransferenciaAlmacen)viewNotaSalida.GetRow(viewNotaSalida.FocusedRowHandle);
            if (obe == null)
                return;
            var lstDetalle = new BAlmacen().listarTransferenciaAlmacenDet(obe.trfc_icod_transf);
            //rptNotaSalida rpt = new rptNotaSalida();
            //rpt.cargar(String.Format("NOTA DE SALIDA N° {0}", obe.nsalc_numero_nota_salida), obe.strAlmacen, lstDetalle, obe);            

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
            f1 = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
            f2 = DateTime.Now;
            dtFecha1.EditValue = f1;
            dtFecha2.EditValue = f2;
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

        private void dtFecha1_EditValueChanged(object sender, EventArgs e)
        {
            f1 = Convert.ToDateTime(dtFecha1.EditValue);
            cargar();
        }

        private void dtFecha2_EditValueChanged(object sender, EventArgs e)
        {
            f2 = Convert.ToDateTime(dtFecha2.EditValue);
            cargar();
        }

        private void filtrar()
        {
            grdNotaSalida.DataSource = lstTransferencias.Where(x => String.Format("{0:0000}", x.trfc_inum_transf).Contains(txtNumero.Text)
                && x.strAlmacenSal.Contains(txtAlmacenSal.Text.ToUpper()) && x.strAlmacenIng.Contains(txtAlmacenIng.Text.ToUpper())).ToList();
        }

        private void txtNumero_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }
    }
}