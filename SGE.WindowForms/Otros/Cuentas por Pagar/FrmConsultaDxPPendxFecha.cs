using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Cuentas_por_Pagar
{
    public partial class FrmConsultaDxPPendxFecha : DevExpress.XtraEditors.XtraForm
    {
        public FrmConsultaDxPPendxFecha()
        {
            InitializeComponent();
        }

        public DateTime fechaDoc;
        List<EDocPorPagar> Lista = new List<EDocPorPagar>();
        public EDocPorPagar obeDocxPagar = new EDocPorPagar();
        public List<long?> ListaDxPOcultar = new List<long?>(); //cuando se elimina una cuenta ya no se podrá mostrar el mismo documento
        public EProveedor obeProveedor = new EProveedor();

        private void FrmConsultaDxPPendxFecha_Load(object sender, EventArgs e)
        {
            Carga();
            cargaLkpTipoDoc();
        }

        private void cargaLkpTipoDoc()
        {
            List<string> listaTD = new List<string>();
            listaTD = Lista.OrderBy(ord => ord.tdocc_vabreviatura_tipo_doc).Select(sel => sel.tdocc_vabreviatura_tipo_doc).Distinct().ToList();
            listaTD.Add("TODOS");
            lkpTipoDoc.Properties.DataSource = listaTD;
            lkpTipoDoc.ItemIndex = listaTD.Count - 1;
        }

        private void Carga()
        {
            Lista = new BCuentasPorPagar().ListaDxPPendXFecha(fechaDoc, obeProveedor.iid_icod_proveedor, obeProveedor.anac_icod_analitica).Where(obe =>
                obe.tdocc_icod_tipo_doc != Parametros.intTipoDocNotaCreditoProveedor && obe.tdocc_icod_tipo_doc != Parametros.intTipoDocAdelantoProveedor).ToList();
            foreach (var item in ListaDxPOcultar)
            {
                Lista = Lista.Where(obe => obe.doxpc_icod_correlativo != item).ToList();
            }
            grd.DataSource = Lista;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            Aceptar();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Aceptar();
        }

        private void Aceptar()
        {
            obeDocxPagar = (EDocPorPagar)gv.GetRow(gv.FocusedRowHandle);
            this.DialogResult = DialogResult.OK;
        }

        private void lkpTipoDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpTipoDoc.ContainsFocus)
                filtroBuscar();
        }

        private void txtNumDoc_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNumDoc.ContainsFocus)
                filtroBuscar();
        }

        private void filtroBuscar()
        {
            if (lkpTipoDoc.Text == "TODOS" && txtNumDoc.Text.Trim() == "")
            {
                grd.DataSource = Lista;
            }
            else
            {
                List<EDocPorPagar> listaTempDxC = new List<EDocPorPagar>();
                if (lkpTipoDoc.Text != "TODOS")
                    listaTempDxC = Lista.Where(ob => ob.tdocc_vabreviatura_tipo_doc == lkpTipoDoc.Text).ToList();
                else
                    listaTempDxC = Lista;
                listaTempDxC = listaTempDxC.Where(ob => ob.doxpc_vnumero_doc.Contains(txtNumDoc.Text.TrimStart().TrimEnd())).ToList();
                grd.DataSource = listaTempDxC;
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
    }
}