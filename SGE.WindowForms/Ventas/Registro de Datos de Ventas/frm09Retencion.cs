using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class frm09Retencion : DevExpress.XtraEditors.XtraForm
    {
        List<ERetencion> lstRetencion = new List<ERetencion>();
        int index;
        public frm09Retencion()
        {
            InitializeComponent();
        }

        private void frm08DocCompra_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            cargar();
        }

        private void cargar()
        {
            lstRetencion = new BVentas().listarRetencionCab(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue)).Where(x => x.retc_vnumero_comprob_reten.Contains(textEdit1.Text) &&
                x.strCliente.Contains(textEdit2.Text.ToUpper())).ToList();
            grdPercepcion.DataSource = lstRetencion;
        }

        void reload(int intIcod)
        {
            cargar();
            index = lstRetencion.FindIndex(x => x.retc_icod_comprobante_retencion == intIcod);
            viewPercepcion.FocusedRowHandle = index;
            viewPercepcion.Focus();
        }

        private void nuevo()
        {
            frmManteRetencion frm = new frmManteRetencion();
            frm.MiEvento += new frmManteRetencion.DelegadoMensaje(reload);
            frm.mes = Convert.ToInt32(lkpMes.EditValue);
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            ERetencion obe = (ERetencion)viewPercepcion.GetRow(viewPercepcion.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                frmManteRetencion frm = new frmManteRetencion();
                frm.MiEvento += new frmManteRetencion.DelegadoMensaje(reload);
                frm.Obe = obe;
                frm.mes = Convert.ToInt32(lkpMes.EditValue);
                frm.SetModify();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            ERetencion obe = (ERetencion)viewPercepcion.GetRow(viewPercepcion.FocusedRowHandle);
            if (obe == null)
                return;
            int index = viewPercepcion.FocusedRowHandle;
            try
            {
                index = viewPercepcion.FocusedRowHandle;
                new BVentas().eliminarRetencionCab(obe);
                cargar();
                viewPercepcion.FocusedRowHandle = index;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ver()
        {
            ERetencion obe = (ERetencion)viewPercepcion.GetRow(viewPercepcion.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                //frmManteFacturaCompra frm = new frmManteFacturaCompra();
                //frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
                //frm.Obe = obe;
                //frm.SetCancel();
                //frm.Show();
                //frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void filtrar()
        {
            grdPercepcion.DataSource = lstRetencion.Where(x => x.retc_vnumero_comprob_reten.Contains(textEdit1.Text) &&
                x.strCliente.Contains(textEdit2.Text.ToUpper())).ToList();
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            ver();
        }

        private void textEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                cargar();
        }

        private void mnu_Opening(object sender, CancelEventArgs e)
        {

        }

    }
}