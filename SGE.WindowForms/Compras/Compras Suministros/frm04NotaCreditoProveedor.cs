using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class frm04NotaCreditoProveedor : DevExpress.XtraEditors.XtraForm
    {

        List<ENotaCreditoProvedor> lstNotaCreditoProveedor = new List<ENotaCreditoProvedor>();

        public frm04NotaCreditoProveedor()
        {
            InitializeComponent();
        }
        private void filtrar()
        {
            grdDocCompra.DataSource = lstNotaCreditoProveedor.Where(x => x.ncpc_nro_nota_cred.Trim().Contains(txtNroDoc.Text.Trim()) &&
                x.strProveedor.ToUpper().Trim().Contains(textEdit1.Text.Trim().ToUpper())).ToList();
        }
        private void frm04NotaCreditoProveedorr_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstNotaCreditoProveedor = new BCompras().listarNotaCreditoProveedor(Parametros.intEjercicio).Where(x => x.ncpc_flag_importacion == false).ToList();
            grdDocCompra.DataSource = lstNotaCreditoProveedor;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstNotaCreditoProveedor.FindIndex(x => x.ncpc_icod_nota_cred == intIcod);
            viewDocCompra.FocusedRowHandle = index;
            viewDocCompra.Focus();
        }
        private void nuevo()
        {
            frmManteNCP frm = new frmManteNCP();
            frm.MiEvento += new frmManteNCP.DelegadoMensaje(reload);
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            ENotaCreditoProvedor obe = (ENotaCreditoProvedor)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);


            if (obe == null)
                return;

            if (obe.tablc_iid_situacion == 2)
            {
                XtraMessageBox.Show("El documento no puede ser Modificado por que está Parcialmente pagado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (obe.tablc_iid_situacion == 3)
            {
                XtraMessageBox.Show("El documento no puede ser Modificado por que está Cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                frmManteNCP frm = new frmManteNCP();
                frm.MiEvento += new frmManteNCP.DelegadoMensaje(reload);
                frm.Obe = obe;
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
            ENotaCreditoProvedor obe = (ENotaCreditoProvedor)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            if (obe == null)
                return;

            if (obe.tablc_iid_situacion == 2)
            {
                XtraMessageBox.Show("El documento no puede ser Eliminado por que está Parcialmente pagado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (obe.tablc_iid_situacion == 3)
            {
                XtraMessageBox.Show("El documento no puede ser Eliminado por que está Cancelado", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            int index = viewDocCompra.FocusedRowHandle;
            try
            {
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar la Nota de Crédito?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    index = viewDocCompra.FocusedRowHandle;
                    new BCompras().eliminarNCProveedor(obe);
                    cargar();
                    viewDocCompra.FocusedRowHandle = index;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void txtNroDoc_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }



        private void actulizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ENotaCreditoProvedor obe = (ENotaCreditoProvedor)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            int index = viewDocCompra.FocusedRowHandle;
            cargar();
            viewDocCompra.FocusedRowHandle = index;
        }

    }
}