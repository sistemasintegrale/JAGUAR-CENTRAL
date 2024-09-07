using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Maintenance;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frm01Archivos : DevExpress.XtraEditors.XtraForm
    {
        public List<EArchivos> lstArchivos = new List<EArchivos>();
        public EOrdenCompra oBeCab = new EOrdenCompra();
        public EArchivos _Be { get; set; }
        public int ocod_icod_detalle_oc = 0;
        public frm01Archivos()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        public BSMaintenanceStatus oState;
        private BSMaintenanceStatus mStatus;
        public BSMaintenanceStatus Status
        {
            get { return (mStatus); }
            set
            {
                mStatus = value;
                StatusControl();
            }
        }
        private void StatusControl()
        {
            bool Enabled = (Status == BSMaintenanceStatus.View);
            if (Status == BSMaintenanceStatus.ModifyCurrent)
            {

            }
        }
        public void SetInsert()
        {
            Status = BSMaintenanceStatus.CreateNew;
        }

        public void SetModify()
        {
            Status = BSMaintenanceStatus.ModifyCurrent;
        }

        public void SetCancel()
        {
            Status = BSMaintenanceStatus.View;
        }
        public void cargar()
        {


            // lstArchivos = new BVentas().listarArchivos(ocod_icod_detalle_oc);
            grdManoObra.DataSource = lstArchivos;
            viewManoObra.Focus();
        }

        #region Marca
        void reload(int intIcod)
        {
            cargar();
            int index = lstArchivos.FindIndex(x => x.arch_icod_archivos == intIcod);
            viewManoObra.FocusedRowHandle = index;
            viewManoObra.Focus();
        }

        private void nuevo()
        {
            using (frmManteArchivos frm = new frmManteArchivos())
            {
                if (lstArchivos.Count > 0)
                    frm.txtCodigo.Text = String.Format("{0:0000}", lstArchivos.Max(x => x.arch_iid_correlativo + 1));
                else
                    frm.txtCodigo.Text = "0001";
                frm.lstArchivos = lstArchivos;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    lstArchivos = frm.lstArchivos;
                    grdManoObra.DataSource = lstArchivos;
                    viewManoObra.RefreshData();
                }
            }
        }
        private void modificar()
        {
            EArchivos Obe = (EArchivos)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteArchivos frm = new frmManteArchivos();
            frm.MiEvento += new frmManteArchivos.DelegadoMensaje(reload);
            frm.lstArchivos = lstArchivos;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
        }
        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            EArchivos Obe = (EArchivos)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteArchivos frm = new frmManteArchivos();
            frm.MiEvento += new frmManteArchivos.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                EArchivos Obe = (EArchivos)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewManoObra.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarArchivos(Obe);
                    cargar();
                    if (lstArchivos.Count >= index + 1)
                        viewManoObra.FocusedRowHandle = index;
                    else
                        viewManoObra.FocusedRowHandle = index - 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void grdManoObra_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
        public void returnSeleccion()
        {

            if (lstArchivos.Count > 0)
            {
                using (frmManteOrdenCompraDetalleDetalle frm = new frmManteOrdenCompraDetalleDetalle())
                {
                    //_Be = (EArchivos)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
                    this.DialogResult = DialogResult.OK;
                }



            }
        }
    }
}