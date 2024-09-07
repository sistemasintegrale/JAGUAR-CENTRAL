using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarFacturaXOrdenCompra : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        List<EFacturaCompra> lstFacCompra = new List<EFacturaCompra>();
        public EFacturaCompra _Be { get; set; }
        public int ococ_icod_orden_compra = 0;
        #endregion

        #region "Eventos"

        public FrmListarFacturaXOrdenCompra()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {
            Carga();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstFacCompra = new BCompras().listarFacCompra(Parametros.intEjercicio).Where(ob => ob.fcoc_flag_importacion == false).Where(x => x.ococ_icod_orden_compra == ococ_icod_orden_compra).ToList();
            grdDocCompra.DataSource = lstFacCompra;
        }


        private void BuscarCriterio()
        {
            //string cod = txtRUC.Text, desc = txtRazonSocial.Text;
            //grdProveedor.DataSource = lstFacCompra.Where(obe => ((cod != string.Empty) ? obe.proc_vnombrecompleto.Contains(cod) : obe.fcoc_num_doc.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EFacturaCompra)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            EFacturaCompra obe = (EFacturaCompra)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            if (obe == null)
                return;
            try
            {
                frmManteFacturaCompra frm = new frmManteFacturaCompra();
                frm.MiEvento += new frmManteFacturaCompra.DelegadoMensaje(reload);
                frm.Obe = obe;
                frm.SetCancel();
                frm.Show();
                frm.setValues();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void reload(int intIcod)
        {
            Carga();
            int index = lstFacCompra.FindIndex(x => x.fcoc_icod_doc == intIcod);
            viewDocCompra.FocusedRowHandle = index;
            viewDocCompra.Focus();
        }

    }
}


