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
    public partial class FrmListarDXPGarantiaProveedores : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        public int intIcodProveedor = 0;
        public bool bDocFacBol = true;
        public bool flag_list_all = false;
        public int intMoneda;
        public bool flag_multiple = false;
        public int intTipoDocEspecifico = 0;
        public int intClaseDocEspecifico = 0;
        public int proc_icod_proveedor = 0;

        public List<EDocPorPagar> lstDocPorPagar = new List<EDocPorPagar>();
        public EDocPorPagar _oBe { get; set; }

        #endregion

        #region "Eventos"

        public FrmListarDXPGarantiaProveedores()
        {
            InitializeComponent();
        }

        private void FrmListarDocumentoPorPagarProveedor_Load(object sender, EventArgs e)
        {
            Carga();
            txtNumeroDocumento.Focus();
        }

        private void viewDocumentos_DoubleClick(object sender, EventArgs e)
        {
            if (!flag_multiple)
                Aceptar();
        }

        private void viewDocumentos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                Aceptar();
        }

        #endregion

        #region "Metodos"

        public void Carga()
        {
            lstDocPorPagar = new BCuentasPorPagar().ListarDocumentoPorPagarTodo(Parametros.intEjercicio).Where(x => x.proc_icod_proveedor == proc_icod_proveedor && x.tdocc_vabreviatura_tipo_doc == "FAC").ToList();
            dgrDocumentos.DataSource = lstDocPorPagar;
        }

        private void Aceptar()
        {
            _oBe = (EDocPorPagar)viewDocumentos.GetRow(viewDocumentos.FocusedRowHandle);
            if (flag_multiple)
            {
                txtNumeroDocumento.Focus();
                viewDocumentos.MoveLast();
                viewDocumentos.MoveFirst();
            }
            if (_oBe != null)
                this.DialogResult = DialogResult.OK;
        }

        private void filtrar()
        {
            dgrDocumentos.DataSource = lstDocPorPagar.Where(obj =>
                                                   obj.doxpc_vnumero_doc.Contains(txtNumeroDocumento.Text)
                                                   && obj.proc_vnombrecompleto.ToUpper().Contains(txtProveedor.Text.ToUpper())
                                                   ).ToList();
        }

        #endregion

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Aceptar();
        }

        private void txtNumeroDocumento_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtProveedor_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }
    }
}