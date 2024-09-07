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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class frm16EstadoResultadodeCC : DevExpress.XtraEditors.XtraForm
    {
        List<EEstadoGanPer> lstEstadoGanPer = new List<EEstadoGanPer>();
        public List<EEstadoGanPerCtas> Lista2 = new List<EEstadoGanPerCtas>();
        public List<EEstadoGanPerCtas> Lista3 = new List<EEstadoGanPerCtas>();
        private BContabilidad obl = new BContabilidad();
        DataTable data = new DataTable();
        private string fecInicio;
        private string fecFin;
        public frm16EstadoResultadodeCC()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(bteCCostoI.Tag) > 0)
            {
                grid_setap(Convert.ToInt32(bteCCostoI.Tag), Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            }
            else
            {
                grid_setap(0, Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
            }

            grd.DataSource = data;
            grd.RefreshDataSource();

        }
        private void Cargar()
        {

        }




        private void SetUpDate()
        {


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Cargar();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void controlEnable(bool Enable)
        {

        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            data = gv.GetRow(gv.FocusedRowHandle) as DataTable;

            using (frmEstadoGanPerCtaContMontodeCC frm = new frmEstadoGanPerCtaContMontodeCC())
            {
                //frm.obePosFinan = Obe;
                frm.egpc_icod_estado_gan_per = Convert.ToInt32(gv.GetRowCellValue(gv.FocusedRowHandle, "egpc_icod_estado_gan_per"));
                frm.anio = Parametros.intEjercicio;
                frm.vcocc_fecha_vcontable = Convert.ToInt32(lkpMes.EditValue);
                frm.cCostoInicio = bteCCostoI.Text;
                frm.ShowDialog();
            }
        }

        private void frm13EstadoResultadoCC_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;

            grid_setap(0, Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));


        }

        private void bteCCostoI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmListarCentroCosto frmCCosto = new frmListarCentroCosto();

            if (frmCCosto.ShowDialog() == DialogResult.OK)
            {
                bteCCostoI.Text = frmCCosto._Be.cecoc_vcodigo_centro_costo;
                bteCCostoI.Tag = frmCCosto._Be.cecoc_icod_centro_costo;

            }
        }

        //formando Grid

        public void grid_setap(int id_centroC, int anio, int id_mes)
        {
            try
            {
                DataTable data_SM = new DataTable();
                data_SM = new BContabilidad().listaCentroCosto_Plantilla_sinMovimiento();
                data = new BContabilidad().listaCentroCosto_Plantilla(id_centroC, anio, id_mes);
                DataRow row_1;


                int coun = 0;
                foreach (DataRow row_ in data_SM.Rows)
                {
                    int f = Convert.ToInt32(data.Rows[coun]["egpc_icod_estado_gan_per"]);

                    int ite = Convert.ToInt32(row_["egpc_icod_estado_gan_per"]);
                    if (coun <= 20)
                    {
                        if (f != ite)
                        {
                            row_1 = data.NewRow();
                            row_1["egpc_icod_estado_gan_per"] = row_["egpc_icod_estado_gan_per"];
                            row_1["egpc_vlinea"] = row_["egpc_vlinea"];
                            row_1["concepto"] = row_["egpc_vconcepto"];
                            data.Rows.Add(row_1);
                        }
                        else
                        {
                            coun += 1;
                        }
                    }

                }



                grd.DataBindings.Clear();
                gv.Columns.Clear();
                grd.DataSource = data;
                gv.Columns[0].Caption = "Linea";
                gv.Columns[1].Caption = "Concepto";


                gv.Columns[2].Visible = false;
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    gv.Columns[i].OptionsColumn.AllowEdit = false;
                    gv.Columns[i].OptionsColumn.AllowFocus = false;
                    gv.Columns[i].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
                    gv.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

                    gv.Columns[i].OptionsColumn.AllowMove = false;
                }
                gv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gv.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gv.OptionsView.ColumnAutoWidth = false;
                gv.Columns[1].Width = 300;
                gv.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            catch (Exception)
            {


            }




        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exportarPlantillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt32(bteCCostoI.Tag) > 0)
                {
                    grid_setap(Convert.ToInt32(bteCCostoI.Tag), Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));

                }
                else
                {
                    grid_setap(0, Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                }
                if (data.Rows.Count > 0)
                {
                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {
                        grdExcelCD.DataSource = data;
                        gvExcelCD.Columns[0].Caption = "Linea";
                        gvExcelCD.Columns[1].Caption = "Concepto";
                        gvExcelCD.Columns[1].Width = 79;
                        gvExcelCD.Columns[1].ImageAlignment = StringAlignment.Near;
                        gvExcelCD.Columns[2].Visible = false;

                        gvExcelCD.OptionsView.ColumnAutoWidth = true;
                        gvExcelCD.OptionsView.RowAutoHeight = true;

                        gvExcelCD.Columns[0].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;


                        string fileName = sfdRuta.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grdExcelCD.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");

                        }
                        else
                        {
                            grdExcelCD.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grdExcelCD.DataSource = null;
                    }
                }
                else
                    throw new ArgumentException("No hay registros para exportar");

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void exportarDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable Data_detall = new DataTable();
                Data_detall = new BContabilidad().listaCentroCosto_Detalle_excel(Parametros.intEjercicio, Convert.ToInt32(lkpMes.EditValue));
                if (Data_detall.Rows.Count > 0)
                {
                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {
                        grDetalleEx.DataSource = Data_detall;
                        grDetalla.Columns[0].Caption = "Cuenta";
                        grDetalla.Columns[1].Visible = false;
                        grDetalla.Columns[2].Visible = false;
                        grDetalla.Columns[3].Caption = "Concepto";
                        grDetalla.Columns[3].Width = 79;
                        grDetalla.Columns[4].Visible = false;
                        grDetalla.Columns[3].ImageAlignment = StringAlignment.Near;
                        grDetalla.OptionsView.ColumnAutoWidth = true;
                        grDetalla.OptionsView.RowAutoHeight = true;
                        string fileName = sfdRuta.FileName;
                        if (!fileName.Contains(".xlsx"))
                        {
                            grDetalleEx.ExportToXlsx(fileName + ".xlsx");
                            System.Diagnostics.Process.Start(fileName + ".xlsx");
                        }
                        else
                        {
                            grDetalleEx.ExportToXlsx(fileName);
                            System.Diagnostics.Process.Start(fileName);
                        }
                        grDetalleEx.DataSource = null;
                    }
                }
                else
                    throw new ArgumentException("No hay registros para exportar");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grd_Click(object sender, EventArgs e)
        {

        }

    }
}