using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class frm02OrdenCompraServicio : DevExpress.XtraEditors.XtraForm
    {
        List<EOrdenCompraServicio> lstpresupuesto = new List<EOrdenCompraServicio>();
        int xposition;
        public frm02OrdenCompraServicio()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            this.cargar();
        }

        private void cargar()
        {
            lstpresupuesto = new BCompras().ListarOrdenCompraServicio();
            grdManoObra.DataSource = lstpresupuesto;
            viewManoObra.Focus();
        }

        #region Marca
        void Agregar(int intcod)
        {
            cargar();
            viewManoObra.FocusedRowHandle = lstpresupuesto.Count - 1;
            viewManoObra.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            viewManoObra.FocusedRowHandle = xposition;
            viewManoObra.Focus();
        }
        private void nuevo()
        {
            frmManteOrdenCompraServicio frm = new frmManteOrdenCompraServicio();
            frm.MiEvento += new frmManteOrdenCompraServicio.DelegadoMensaje(Agregar);
            frm.txtSerie.Text = Parametros.intEjercicio.ToString();
            frm.SetInsert();
            frm.Show();
            frm.btnProveedor.Enabled = true;
            frm.txtSerie.Properties.ReadOnly = false;
            frm.txtNumero.Properties.ReadOnly = false;
            frm.txtporIGV.Text = Parametros.strPorcIGV.ToString();
            frm.dtmFechafactura.EditValue = DateTime.Now;
            frm.lkpMoneda.EditValue = 3;
            frm.txtporIGV.Enabled = true;
        }
        private void modificar()
        {
            string descripcion_situacion = "";
            int sit = 0;
            EOrdenCompraServicio Obe = (EOrdenCompraServicio)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            xposition = viewManoObra.FocusedRowHandle;
            if (Obe == null)
                return;

            if (Obe.tablc_iid_situacion_ocs == Parametros.intSituacOCAnulado)
            {
                sit = Parametros.intSituacOCAnulado;
                descripcion_situacion = "ANULADA";
            }
            if (Obe.tablc_iid_situacion_ocs == Parametros.intSituacOCAutorizado)
            {
                sit = Parametros.intSituacOCAutorizado;
                descripcion_situacion = "AUTORIZADO";
            }

            if (sit == 0)
            {
                frmManteOrdenCompraServicio frm = new frmManteOrdenCompraServicio();
                frm.MiEvento += new frmManteOrdenCompraServicio.DelegadoMensaje(reload);
                frm.ococ_icod_orden_compra = Obe.ocsc_icod_ocs;
                frm.Obe = Obe;
                frm.SetModify();
                frm.Show();
                frm.setValues();
                frm.btnProveedor.Enabled = false;
                frm.lkpMoneda.Enabled = false;
            }
            else
            {
                XtraMessageBox.Show("No puede ser Modificado, la Orden de Compra esta " + descripcion_situacion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {

            EOrdenCompraServicio Obe = (EOrdenCompraServicio)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                xposition = viewManoObra.FocusedRowHandle;
                frmManteOrdenCompraServicio frm = new frmManteOrdenCompraServicio();
                frm.MiEvento += new frmManteOrdenCompraServicio.DelegadoMensaje(reload);
                frm.ococ_icod_orden_compra = Obe.ocsc_icod_ocs;
                frm.Obe = Obe;
                frm.Show();
                frm.setValues();
                frm.btnProveedor.Enabled = false;
                frm.setDobleClick();
            }

        }
        private void eliminar()
        {
            string descripcion_situacion = "";
            int sit = 0;
            EOrdenCompra Obe = (EOrdenCompra)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            xposition = viewManoObra.FocusedRowHandle;
            if (Obe == null)
                return;

            if (Obe.tablc_iid_situacion_oc == Parametros.intSituacOCAnulado)
            {
                sit = 1;
                descripcion_situacion = "ANULADA";
            }
            if (Obe.tablc_iid_situacion_oc == Parametros.intSituacOCAutorizado)
            {
                sit = 2;
                descripcion_situacion = "AUTORIZADO";
            }
            if (sit == 0)
            {
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BCompras().EliminarOrdenCompra(Obe);
                    reload(0);
                }
            }
            else
            {
                XtraMessageBox.Show("No puede ser Modificado, la Orden de Compra esta " + descripcion_situacion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.modificar();
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


        #endregion

        private void btnNuevo_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            eliminar();
        }


        private void txtNumero_KeyUp(object sender, KeyEventArgs e)
        {
            this.BuscarCriterio();
        }
        private void BuscarCriterio()
        {
            grdManoObra.DataSource = lstpresupuesto.Where(obj => obj.ocsc_vnumero_ocs.ToUpper().Contains(txtpresupuesto.Text) &&
                                                obj.proc_vnombrecompleto.ToUpper().Contains(txtCliente.Text.ToUpper())).ToList();
        }

        private void confirmarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Anular();
        }
        private void Anular()
        {
            int sit = 0;
            string descripcion_situacion = "";
            EOrdenCompraServicio Obe = (EOrdenCompraServicio)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            xposition = viewManoObra.FocusedRowHandle;
            if (Obe == null)
                return;

            if (Obe.tablc_iid_situacion_ocs == Parametros.intSituacOCAnulado)
            {
                sit = 1;
                descripcion_situacion = "ANULADA";
            }
            if (Obe.tablc_iid_situacion_ocs == Parametros.intSituacOCAutorizado)
            {
                sit = 2;
                descripcion_situacion = "AUTORIZADO";
            }

            if (sit == 0)
            {
                if (XtraMessageBox.Show("¿Esta seguro que desea Anular el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BCompras().AnularOrdenCompraServicio(Obe);
                    reload(0);
                }
            }
            else
            {
                XtraMessageBox.Show("No puede ser Anulado, la Orden de Compra esta " + descripcion_situacion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void Imprimir()
        {
            List<EOrdenCompra> lstPresupuestoplantilla = new List<EOrdenCompra>();
            EOrdenCompra Obe = (EOrdenCompra)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                //lstPresupuestoplantilla = new BCompras().ListarOrdenCompraDetalle(Obe.ococ_icod_orden_compra);
                //rpt03OrdenCompra frmOc = new rpt03OrdenCompra();
                //frmOc.carga(Obe, lstPresupuestoplantilla);
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenCompra Obe = (EOrdenCompra)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                xposition = viewManoObra.FocusedRowHandle;
                reload(0);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EOrdenCompra Obe = (EOrdenCompra)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                xposition = viewManoObra.FocusedRowHandle;
                reload(0);
            }
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int sit = 0;
            string descripcion_situacion = "";
            EOrdenCompraServicio Obe = (EOrdenCompraServicio)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            xposition = viewManoObra.FocusedRowHandle;
            if (Obe == null)
                return;

            if (Obe.tablc_iid_situacion_ocs == Parametros.intSituacOCAnulado)
            {
                sit = 1;
                descripcion_situacion = "ANULADA";
            }
            if (Obe.tablc_iid_situacion_ocs == Parametros.intSituacOCAutorizado)
            {
                sit = 2;
                descripcion_situacion = "AUTORIZADO";
            }

            if (sit == 0)
            {
                if (XtraMessageBox.Show("¿Esta seguro que desea Eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BCompras().EliminarOrdenCompraServicio(Obe);
                    reload(0);
                }
            }
            else
            {
                XtraMessageBox.Show("No puede ser Eliminado, la Orden de Compra esta " + descripcion_situacion, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ImprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenCompraServicio Obe = (EOrdenCompraServicio)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                List<ERegistroFirmas> lstFirmas = new BCompras().listarRegistroFirmas();
                string total = Convertir.ConvertNumeroEnLetras(Obe.ocsc_nmonto_total.ToString());
                List<EOrdenCompraServicio> mlist = new List<EOrdenCompraServicio>();
                mlist = new BCompras().ListarOrdenCompraServicioDetalle(Obe.ocsc_icod_ocs);
                rptOrdenCompraServicios rpt = new rptOrdenCompraServicios();
                rpt.cargar(Obe, mlist, total, lstFirmas);

            }
        }
    }
}