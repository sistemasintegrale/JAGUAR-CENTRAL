using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frmListarVentaDirecta : DevExpress.XtraEditors.XtraForm
    {
        List<EVentaDirecta> lstVentaDirecta = new List<EVentaDirecta>();
        public EVentaDirecta _Be { get; set; }


        public frmListarVentaDirecta()
        {
            InitializeComponent();
        }

        private void Frm06VentaDirecta_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstVentaDirecta = new BVentas().listarVentaDirecta(Parametros.intEjercicio).Where(x => x.tablc_iid_situacion == 1).ToList();
            grdVentaDirecta.DataSource = lstVentaDirecta;
        }

        void reload(int intIcod)
        {
            cargar();
            int index = lstVentaDirecta.FindIndex(x => x.dvdc_icod_doc_venta_directa == intIcod);
            viewVentaDirecta.FocusedRowHandle = index;
            viewVentaDirecta.Focus();
        }

        private void filtrar()
        {
            grdVentaDirecta.DataSource = lstVentaDirecta.Where(x => x.dvdc_vnumero_doc_venta_directa.Trim().Contains(txtNumero.Text.Trim()) &&
                x.strDesCliente.ToUpper().Trim().Contains(txtCliente.Text.Trim().ToUpper()) && x.strPlaca.Contains(txtPlaca.Text.ToUpper())).ToList();
        }

        #region No Necesario
        private void nuevo()
        {
            FrmManteVentaDirecta frm = new FrmManteVentaDirecta();
            frm.MiEvento += new FrmManteVentaDirecta.DelegadoMensaje(reload);
            frm.txtNumero.Text = (lstVentaDirecta.Count + 1).ToString();
            frm.SetInsert();
            frm.Show();
        }

        private void modificar()
        {
            EVentaDirecta obe = (EVentaDirecta)viewVentaDirecta.GetRow(viewVentaDirecta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                if (obe.tablc_iid_situacion != 1)
                    throw new ArgumentException(String.Format("La boleta no puede ser modificada, su situación es ", obe.strSituacion));

                FrmManteVentaDirecta frm = new FrmManteVentaDirecta();
                frm.MiEvento += new FrmManteVentaDirecta.DelegadoMensaje(reload);
                frm.oBe = obe;
                frm.SetModify();
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminar()
        {
            EVentaDirecta obe = (EVentaDirecta)viewVentaDirecta.GetRow(viewVentaDirecta.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                if (obe.tablc_iid_situacion != 1)
                    throw new ArgumentException(String.Format("El registro no puede ser eliminado, su situación es ", obe.strSituacion));
                if (XtraMessageBox.Show("¿Esta seguro que desea eliminar el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new BVentas().eliminarVentaDirecta(obe);
                    reload(0);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void anular()
        {
            try
            {
                if (XtraMessageBox.Show("¿Esta seguro que desea anular el registro?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //new BVentas().eliminarVentaDirecta(obe);
                    //reload(0);

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

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anular();
        }
        #endregion

        private void DgAcept()
        {
            _Be = (EVentaDirecta)viewVentaDirecta.GetRow(viewVentaDirecta.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }
        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void viewVentaDirecta_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}