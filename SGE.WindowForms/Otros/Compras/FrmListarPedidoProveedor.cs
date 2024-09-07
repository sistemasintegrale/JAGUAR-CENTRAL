using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarPedidoProveedor : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"
        public int proc_icod_proveedor = 0;
        private List<EPedidoProvCab> lstProveedores = new List<EPedidoProvCab>();
        public EPedidoProvCab _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;

        #endregion

        #region "Eventos"

        public FrmListarPedidoProveedor()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {
            if (bNoExceptuadosRetencion)
                CargaNoExceptuadosRetencion();
            else
                Carga();

            txtRUC.Focus();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstProveedores = new BCompras().listarPedidoCompra().Where(ob => ob.proc_icod_proveedor == proc_icod_proveedor).ToList();
            grdDocCompra.DataSource = lstProveedores;
        }

        public void CargaNoExceptuadosRetencion()
        {
            //mlist = new BProveedoresExceptuadosRetencion().ListarProveedoresNoExcetuadosRetencion();
            grdDocCompra.DataSource = lstProveedores;
        }

        private void BuscarCriterio()
        {
            //string cod = txtRUC.Text, desc = txtRazonSocial.Text;
            //grdDocCompra.DataSource = lstProveedores.Where(obe => ((cod != string.Empty) ? obe.vcod_proveedor.Contains(cod) : obe.vnombrecompleto.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EPedidoProvCab)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
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


        private void txtRUC_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRUC.ContainsFocus)
            {
                txtRazonSocial.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRazonSocial.ContainsFocus)
            {
                txtRUC.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


