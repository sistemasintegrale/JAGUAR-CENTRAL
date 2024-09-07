using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class frm03RptComprobantesNoCuadrados : XtraForm
    {
        string strMes;
        int intMes;
        public frm03RptComprobantesNoCuadrados()
        {
            InitializeComponent();
        }

        private List<EComprobante> mlist = new List<EComprobante>();

        private void frmRptComprobantesNoCuadrados_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMeses, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMeses.EditValue = DateTime.Now.Month;
        }

        public void Carga()
        {
            mlist = new BContabilidad().ListarComprobantesNoCuadrados(Parametros.intEjercicio);
            foreach (EComprobante obj in mlist)
            {
                obj.iid_dia = obj.sfec_nota_contable.Day;
                obj.vmoneda = (obj.vmoneda == "SOLES") ? "S/." : "US$";
            }
        }

        //public void ejecutar()
        //{
        //    strMes = new BMes().ejecutar();
        //}
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BReporteContabilidad bReporteContabilidad = new BReporteContabilidad();            
            //rptComprobantesNoCuadrados rpt = new rptComprobantesNoCuadrados();
            //rpt.cargar(bReporteContabilidad.ComprobantesNoCuadrados(Parametros.intPeriodo, intMes), Parametros.intPeriodo.ToString(), strMes, "<RFRM03>");
        }

        private void lkpMeses_EditValueChanged(object sender, EventArgs e)
        {


        }
        private void view()
        {
            EComprobante oBe = (EComprobante)viewComprobante.GetRow(viewComprobante.FocusedRowHandle);
            if (oBe != null)
            {
                FrmDetNoCuadrados frm = new FrmDetNoCuadrados();
                //frm.MiEvento += new FrmManteDetalleComprobante.DelegadoMensaje(form2_MiEvento);   
                frm.Text = "Movimientos del Comprobante " + oBe.strNroVco;
                frm.code = oBe.iid_voucher_contable;
                frm.Show();
            }
        }
        private void movimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view();
        }

        private void viewComprobante_DoubleClick(object sender, EventArgs e)
        {
            view();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            controEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void controEnable(bool Enable)
        {
            panel1.Visible = Enable;
            lkpMeses.Enabled = !Enable;
            btnBuscar.Enabled = !Enable;
            btnRefresh.Enabled = !Enable;

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            strMes = lkpMeses.Text;
            intMes = Convert.ToInt32(lkpMeses.EditValue);
            controEnable(true);
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Carga();
            //ejecutar();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            controEnable(false);
            dgrComprobante.DataSource = mlist.Where(obj => obj.iid_mes == Convert.ToInt32(lkpMeses.EditValue)
            && obj.iid_anio == Parametros.intEjercicio).ToList();
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        if (mlist.Where(obj => obj.iid_mes == Convert.ToInt32(lkpMeses.EditValue) && obj.iid_anio == Parametros.intEjercicio).Count() > 0)
            //        {
            //            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            //            {
            //                grdExcel.DataSource = new BReporteContabilidad().ComprobantesNoCuadrados(Parametros.intEjercicio, intMes);
            //                string fileName = sfdRuta.FileName;
            //                if (!fileName.Contains(".xlsx"))
            //                {
            //                    grdExcel.ExportToXlsx(fileName + ".xlsx");
            //                    System.Diagnostics.Process.Start(fileName + ".xlsx");
            //                }
            //                else
            //                {
            //                    grdExcel.ExportToXlsx(fileName);
            //                    System.Diagnostics.Process.Start(fileName);
            //                }
            //                grdExcel.DataSource = null;
            //            }
            //        }
            //        else
            //            throw new ArgumentException("No hay registros para exportar");

            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(ex.Message,"Información del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            //    }
        }
    }
}