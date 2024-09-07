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
    public partial class frmListarDXCNoCancelado : DevExpress.XtraEditors.XtraForm
    {
        List<EDocXCobrar> lstDocumentos = new List<EDocXCobrar>();
        public EDocXCobrar _oBe { get; set; }
        public bool flagSoloFavBov = false;
        public int IcodCliente = 0;
        public frmListarDXCNoCancelado()
        {
            InitializeComponent();
        }

        private void DgAcept()
        {
            _oBe = (EDocXCobrar)viewDXC.GetRow(viewDXC.FocusedRowHandle);
            if (_oBe != null)
                this.DialogResult = DialogResult.OK;
        }

        private void frmListarDXCNoCancelado_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstDocumentos = new BCuentasPorCobrar().listarDxcPendientes(Parametros.intEjercicio).Where(x => x.cliec_icod_cliente == IcodCliente).ToList();
            if (flagSoloFavBov)
                lstDocumentos = lstDocumentos.Where(x => x.tdocc_icod_tipo_doc == 9 || x.tdocc_icod_tipo_doc == 26).ToList();
            grdDXC.DataSource = lstDocumentos;
        }

        private void viewDXC_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void filtrar()
        {
            grdDXC.DataSource = lstDocumentos.Where(x => x.doxcc_vnumero_doc.Contains(txtDocumento.Text)
                && x.cliec_vnombre_cliente.ToUpper().Contains(txtCliente.Text.ToUpper())).ToList();
        }

        private void txtDocumento_KeyUp(object sender, KeyEventArgs e)
        {
            filtrar();
        }
    }
}