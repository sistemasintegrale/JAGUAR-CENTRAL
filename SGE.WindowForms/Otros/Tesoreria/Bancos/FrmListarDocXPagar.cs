using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Tesoreria.Bancos
{
    public partial class FrmListarDocXPagar : DevExpress.XtraEditors.XtraForm
    {
        private List<EDocPorPagar> mlist = new List<EDocPorPagar>();
        public EDocPorPagar _Be { get; set; }

        public int? intProveedor;
        public int intTipoDoc;
        public bool flag_filtrar_proveedor;
        public bool flag_canje;
        public bool flag_no_pendiente = false;
        public bool flag_docs_para_ncp = false;
        public int NCPI = 0;
        public int TD = 0;

        public FrmListarDocXPagar()
        {
            InitializeComponent();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Aceptar();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Aceptar();
        }

        private void Aceptar()
        {
            if (gv.RowCount > 0)
            {
                _Be = (EDocPorPagar)gv.GetRow(gv.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void BuscarCriterio()
        {
            grd.DataSource = mlist.Where(obj => obj.doxpc_vnumero_doc.Contains(txtCodigo.Text)).ToList();
        }

        private void FrmListarDocXPagar_Load(object sender, EventArgs e)
        {
            if (flag_no_pendiente)
            {
                mlist = new BCuentasPorPagar().listarEDocPorPagarNoPendientes(Parametros.intEjercicio, Convert.ToInt32(intProveedor));
                grd.DataSource = mlist;
            }
            if (flag_docs_para_ncp)
            {
                /*Viene desde NOTA DE CREDITO IMPORTACION*/
                if (NCPI == 1)
                {
                    mlist = new BCuentasPorPagar().ListarEDocPorPagarTodosPorProveedor(Parametros.intEjercicio, Convert.ToInt32(intProveedor)).Where(x => x.tdocc_icod_tipo_doc == TD).ToList();
                    grd.DataSource = mlist;
                }
                else
                    mlist = new BCuentasPorPagar().ListarEDocPorPagarTodosPorProveedor(Parametros.intEjercicio, Convert.ToInt32(intProveedor));
                grd.DataSource = mlist;
            }
            else
            {
                mlist = new BCuentasPorPagar().listarEDocPorPagarPendientes(Parametros.intEjercicio);
                //if (flag_filtrar_proveedor)
                //    mlist = mlist.Where(dxp => dxp.tdocc_icod_tipo_doc == intTipoDoc && dxp.proc_icod_proveedor == intProveedor).ToList();
                //else 
                if (flag_canje)
                    mlist = mlist.Where(dxp => dxp.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor && dxp.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor && dxp.proc_icod_proveedor == intProveedor).ToList();
                else
                    if (intTipoDoc == 86)
                {
                    mlist = mlist.Where(dxp => dxp.tdocc_icod_tipo_doc == intTipoDoc || dxp.tdocc_icod_tipo_doc == 119).ToList();
                    grd.DataSource = mlist;
                }
                else
                {
                    mlist = mlist.Where(dxp => dxp.tdocc_icod_tipo_doc == intTipoDoc).ToList();
                    grd.DataSource = mlist;
                }

            }
        }

        private void btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle == 0)
                gv.MoveLast();
            else
                gv.MovePrev();
        }

        private void btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gv.FocusedRowHandle == gv.RowCount - 1)
                gv.MoveFirst();
            else
                gv.MoveNext();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCodigo.ContainsFocus)
                BuscarCriterio();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}