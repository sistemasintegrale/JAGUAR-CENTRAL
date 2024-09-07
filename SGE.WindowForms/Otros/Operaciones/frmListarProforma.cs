using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Operaciones
{
    public partial class frmListarProforma : DevExpress.XtraEditors.XtraForm
    {
        List<EProforma> lstProformas = new List<EProforma>();
        public EProforma _Be { get; set; }
        public frmListarProforma()
        {
            InitializeComponent();
        }

        private void frmListarProforma_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstProformas = new BOperaciones().listarProforma(Parametros.intEjercicio).Where(x => Convert.ToInt32(x.prfc_iid_orden_trabajo) == 0
                && x.prfc_iid_situacion_proforma == 1).ToList();
            grdProforma.DataSource = lstProformas;
        }

        private void returnSeleccion()
        {
            if (lstProformas.Count > 0)
            {
                _Be = (EProforma)viewProforma.GetRow(viewProforma.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdProforma.DataSource = lstProformas.Where(x =>
                                                   x.prfc_vnumero_proforma.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strDesCliente.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }

        private void viewProforma_DoubleClick(object sender, EventArgs e)
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

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }
    }
}