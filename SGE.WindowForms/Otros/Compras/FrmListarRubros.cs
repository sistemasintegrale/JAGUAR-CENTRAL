using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace SGE.WindowForms.Otros.Compras
{
    public partial class FrmListarRubros : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        List<EImportacionConceptos> lstpreImportacionConcepto = new List<EImportacionConceptos>();
        public EImportacionConceptos _Be { get; set; }
        public bool bNoExceptuadosRetencion = false;
        public int impc_icod_importacion = 0;

        #endregion

        #region "Eventos"

        public FrmListarRubros()
        {
            InitializeComponent();
        }

        private void FrmListarProveedor_Load(object sender, EventArgs e)
        {


            Carga();

            txtConcepto.Focus();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstpreImportacionConcepto = new BCompras().ListarImportacionConceptos(impc_icod_importacion);
            grdManoObra.DataSource = lstpreImportacionConcepto;
        }



        private void BuscarCriterio()
        {
            //string cod = txtnumeroOC.Text, desc = txtProveedor.Text;
            //grdManoObra.DataSource = lstpreImportacionConcepto.Where(obe => ((cod != string.Empty) ? obe.ococ_numero_orden_compra.Contains(cod) : obe.proc_vnombrecompleto.Contains(desc))).ToList();
            grdManoObra.DataSource = lstpreImportacionConcepto.Where(x =>
                                                  x.cpn_vdescripcion_concepto_nacional.ToString().Contains(txtConcepto.Text.ToUpper()) &&
                                                  x.cpnd_vdescripcion.Contains(txtRubros.Text.ToUpper())
                                            ).ToList();


        }

        private void viewProveedor_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void DgAcept()
        {
            _Be = (EImportacionConceptos)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
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
            if (txtConcepto.ContainsFocus)
            {
                txtRubros.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazonSocial_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRubros.ContainsFocus)
            {
                txtConcepto.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void viewManoObra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}


