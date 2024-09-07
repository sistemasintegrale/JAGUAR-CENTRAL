using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmListarPersonal : DevExpress.XtraEditors.XtraForm
    {
        List<EVendedor> lstPersonal = new List<EVendedor>();
        public EPersonal _Be { get; set; }
        public bool flag_personal_all = false;

        public frmListarPersonal()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            lstPersonal = new BOperaciones().listarVendedor();

            grdPersonal.DataSource = lstPersonal;
            viewPersonal.Focus();
        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstPersonal.FindIndex(x => x.vdrsc_icod_vendedor == intIcod);
            viewPersonal.FocusedRowHandle = index;
            viewPersonal.Focus();
        }
        private void nuevo()
        {
            frmManteVendedor frm = new frmManteVendedor();
            frm.MiEvento += new frmManteVendedor.DelegadoMensaje(reload);
            if (lstPersonal.Count > 0)
                frm.txtCodigo.Text = String.Format("{0:0000}", lstPersonal.Max(x => x.vdrsc_vcod_vendedor + 1));
            else
                frm.txtCodigo.Text = "0001";
            frm.lstPersonal = lstPersonal;
            frm.SetInsert();
            frm.Show();
            frm.txtNombres.Focus();
        }
        private void modificar()
        {
            EVendedor Obe = (EVendedor)viewPersonal.GetRow(viewPersonal.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteVendedor frm = new frmManteVendedor();
            frm.MiEvento += new frmManteVendedor.DelegadoMensaje(reload);
            frm.lstPersonal = lstPersonal;
            frm.Obe = Obe;
            frm.SetModify();
            frm.Show();
            frm.setValues();
            frm.txtNombres.Focus();
        }
        private void returnSeleccion()
        {
            if (lstPersonal.Count > 0)
            {
                //_Be = (EVendedor)viewPersonal.GetRow(viewPersonal.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
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
        private void viewPersonal_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void filtrar()
        {
            if (lstPersonal.Count > 0)
            {
                grdPersonal.DataSource = lstPersonal.Where(x => x.vdrsc_vcod_vendedor.ToString().Contains(txtCodigo.Text)
                    && x.vdrsc_vnombre_vendedor.ToUpper().Contains(txtDescripcion.Text.ToUpper())).ToList();
            }
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }
    }
}