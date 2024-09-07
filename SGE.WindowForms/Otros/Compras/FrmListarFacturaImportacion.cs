using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarFacturaImportacion : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        private List<EImportacionFactura> lstImpFactura = new List<EImportacionFactura>();
        public EImportacionFactura _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int Icod_Importacion = 0;
        #endregion

        #region "Eventos"

        public FrmListarFacturaImportacion()
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
            lstImpFactura = new BCompras().ListarImportacionFactura(Icod_Importacion);
            grdProveedor.DataSource = lstImpFactura;
        }

        public void CargaNoExceptuadosRetencion()
        {
            //mlist = new BProveedoresExceptuadosRetencion().ListarProveedoresNoExcetuadosRetencion();
            grdProveedor.DataSource = lstImpFactura;
        }

        private void BuscarCriterio()
        {
            string cod = txtRUC.Text, desc = txtRazonSocial.Text;
            grdProveedor.DataSource = lstImpFactura.Where(obe => ((cod != string.Empty) ? obe.proc_vnombrecompleto.Contains(cod) : obe.fcoc_num_doc.Contains(desc))).ToList();

        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EImportacionFactura)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
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

        void reload(int intIcod)
        {
            Carga();
            grdProveedor.DataSource = lstImpFactura;
            int index = lstImpFactura.FindIndex(x => x.impd1_icod_import_factura == intIcod);
            viewProveedor.FocusedRowHandle = index;
            viewProveedor.Focus();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EImportacionFactura Obe = (EImportacionFactura)viewProveedor.GetRow(viewProveedor.FocusedRowHandle);
            frmListarFacImpoDet frm = new frmListarFacImpoDet();
            frm.IcodFacDet = Convert.ToInt32(Obe.fcoc_icod_doc);
            frm.Show();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }





    }
}


