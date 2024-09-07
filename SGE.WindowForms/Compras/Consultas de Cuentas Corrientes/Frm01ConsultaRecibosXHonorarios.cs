using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Cuentas_por_Pagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    public partial class Frm01ConsultaRecibosXHonorarios : DevExpress.XtraEditors.XtraForm
    {
        private List<EDocPorPagar> Lista = new List<EDocPorPagar>();

        public Frm01ConsultaRecibosXHonorarios()
        {
            InitializeComponent();
        }

        private void FrmConsultaRecibosXHonorarios_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
            Carga();
        }

        private void Carga()
        {
            EDocPorPagar objE_DocPorPagar = new EDocPorPagar();
            objE_DocPorPagar.anio = Parametros.intEjercicio;
            objE_DocPorPagar.mesec_iid_mes = Convert.ToInt32(lkpMes.EditValue);

            Lista = new BCuentasPorPagar().ListarEDocPorPagarRHO(objE_DocPorPagar).Where(obe => obe.tdocc_icod_tipo_doc == Parametros.intTipoDocReciboPorHonorarios).ToList();
            grd.DataSource = Lista;
        }

        private void consultar()
        {
            if (Lista.Count > 0)
            {
                EDocPorPagar Obe = (EDocPorPagar)viewRHO.GetRow(viewRHO.FocusedRowHandle);
                FrmConsultaPagosDocumentosXPagar p = new FrmConsultaPagosDocumentosXPagar();
                p.eDocXPagar = Obe;
                p.ShowDialog();
            }
            else
                XtraMessageBox.Show("No hay registro por consultar", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void consultaDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consultar();
        }

        private void lkpMes_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpMes.EditValue != null)
                Carga();
        }

        private void imprimir()
        {
            RptConsultaRecibosXHonorarios rpt = new RptConsultaRecibosXHonorarios();
            rpt.Cargar(Lista, lkpMes.Text);
        }
        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void exportarAExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdRuta.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grd.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grd.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }

            }
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewRHO.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewRHO.ClearColumnsFilter();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            consultar();
        }
    }
}