using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    public partial class frm03GarantiaClientes : DevExpress.XtraEditors.XtraForm
    {
        List<EGarantiaClientes> lstGarantiaClientes = new List<EGarantiaClientes>();
        public EProyectos _Be { get; set; }

        public frm03GarantiaClientes()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {


            lstGarantiaClientes = new BVentas().listarGarantiaClientes();
            grdGarantiaProveedores.DataSource = lstGarantiaClientes;
            viewGarantiaProveedores.Focus();
        }

        #region Marca
        void reload(int intIcod)
        {
            cargar();
            int index = lstGarantiaClientes.FindIndex(x => x.garc_icod_garantia == intIcod);
            viewGarantiaProveedores.FocusedRowHandle = index;
            viewGarantiaProveedores.Focus();
        }
        private void nuevo()
        {
            frmManteGarantiaClientes frm = new frmManteGarantiaClientes();
            frm.MiEvento += new frmManteGarantiaClientes.DelegadoMensaje(reload);
            frm.lstGarantiaClientes = lstGarantiaClientes;
            frm.SetInsert();
            if (lstGarantiaClientes.Count == 0)
            {
                frm.txtNumero.Text = "000001";
            }
            else
            {
                frm.txtNumero.Text = String.Format("{0:0000000}", (lstGarantiaClientes.Max(ob => Convert.ToInt32(ob.garc_vnumero_garantia)) + 1));
            }
            frm.Show();
            frm.lkpSituacion.Enabled = false;
        }
        private void modificar()
        {
            EGarantiaClientes Obe = (EGarantiaClientes)viewGarantiaProveedores.GetRow(viewGarantiaProveedores.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteGarantiaClientes frm = new frmManteGarantiaClientes();
            frm.MiEvento += new frmManteGarantiaClientes.DelegadoMensaje(reload);
            frm.lstGarantiaClientes = lstGarantiaClientes;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtNumero.Enabled = false;
        }
        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            EGarantiaClientes Obe = (EGarantiaClientes)viewGarantiaProveedores.GetRow(viewGarantiaProveedores.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteGarantiaClientes frm = new frmManteGarantiaClientes();
            frm.MiEvento += new frmManteGarantiaClientes.DelegadoMensaje(reload);
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();
        }
        private void eliminar()
        {
            try
            {
                EGarantiaClientes Obe = (EGarantiaClientes)viewGarantiaProveedores.GetRow(viewGarantiaProveedores.FocusedRowHandle);
                if (Obe == null)
                    return;
                int index = viewGarantiaProveedores.FocusedRowHandle;
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Obe.intUsuario = Valores.intUsuario;
                    Obe.strPc = WindowsIdentity.GetCurrent().Name;
                    new BVentas().eliminarGarantiaClientes(Obe);
                    cargar();
                    if (lstGarantiaClientes.Count >= index + 1)
                        viewGarantiaProveedores.FocusedRowHandle = index;
                    else
                        viewGarantiaProveedores.FocusedRowHandle = index - 1;
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
        private void Filtrar()
        {
            grdGarantiaProveedores.DataSource = lstGarantiaClientes.Where(x => x.garc_vnumero_garantia.Trim().Contains(txtCodigo.Text.Trim()) &&
                   x.garc_vnumero_garantia.ToUpper().Trim().Contains(txtDescripcion.Text.Trim().ToUpper())).ToList();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            Filtrar();
        }

    }
}