using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarOrdenCompraImportaciones : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        List<EOrdenCompraImportacion> lstpresupuesto = new List<EOrdenCompraImportacion>();
        public EOrdenCompraImportacion _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int proc_icod_proveedor = 0;

        #endregion

        #region "Eventos"

        public FrmListarOrdenCompraImportaciones()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {


            Carga();

            txtnumeroOC.Focus();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstpresupuesto = new BCompras().ListarOrdenCompraImportacion().Where(_be => _be.proc_icod_proveedor == proc_icod_proveedor).ToList();
            grdManoObra.DataSource = lstpresupuesto;
        }



        private void BuscarCriterio()
        {
            string cod = txtnumeroOC.Text, desc = txtProveedor.Text;
            grdManoObra.DataSource = lstpresupuesto.Where(obe => ((cod != string.Empty) ? obe.ocic_vnumero_oci.Contains(cod) : obe.proc_vnombrecompleto.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EOrdenCompraImportacion)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
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
            if (txtnumeroOC.ContainsFocus)
            {
                txtProveedor.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtProveedor.ContainsFocus)
            {
                txtnumeroOC.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


