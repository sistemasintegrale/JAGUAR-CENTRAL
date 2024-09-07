using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmEstadoGanPerCtaContMontodeCC : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EEstadoGanPer obePosFinan = new EEstadoGanPer();
        public List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
        private BContabilidad obl = new BContabilidad();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        public int cecoc_icod_centro_costo = 0;
        public string cCostoInicio = "";
        public int vcocc_fecha_vcontable;
        public int anio;
        public int egpc_icod_estado_gan_per;

        #endregion

        DataTable data = new DataTable();
        public frmEstadoGanPerCtaContMontodeCC()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            data = obl.listaCentroCosto_D(Convert.ToInt32(egpc_icod_estado_gan_per), anio, Convert.ToInt32(vcocc_fecha_vcontable));


            /*Movimientos*/
            grd.DataBindings.Clear();
            gv.Columns.Clear();
            grd.DataSource = data;
            gv.Columns[0].Caption = "Cuenta";
            gv.Columns[1].Visible = false;
            gv.Columns[2].Visible = false;
            gv.Columns[3].Caption = "Concepto";
            gv.Columns[3].Width = 300;
            gv.Columns[4].Visible = false;


            gv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            gv.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            gv.OptionsView.ColumnAutoWidth = false;
            for (int i = 0; i < data.Columns.Count; i++)
            {
                gv.Columns[i].OptionsColumn.AllowEdit = false;
                gv.Columns[i].OptionsColumn.AllowFocus = false;
                gv.Columns[i].OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
                gv.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gv.Columns[i].OptionsColumn.AllowMove = false;
            }



        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(Lista[0].egpd_iid_cuenta_contable) && x.cecoc_icod_centro_costo == cecoc_icod_centro_costo).ToList();

            FrmMayorCCostoMov frm = new FrmMayorCCostoMov();
            frm.mlist = lstMovimientos;
            //frm.Text = "Detalle del Centro de Costo " + cCostoInicio + " " + "Cuenta " + Obe.strNroCuenta;
            frm.Show();

        }



        #endregion

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Modify(int Cab_icod_correlativo)
        {
            Cargar();
            int index = Lista.FindIndex(obe => obe.egpd_icod_ctas_estado_gan_per == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (data.Rows.Count > 0)
                {
                    if (sfdRuta.ShowDialog(this) == DialogResult.OK)
                    {


                        grdExcelCD.DataSource = data;
                        gvExcelCD.Columns[0].Caption = "Cuenta";
                        gvExcelCD.Columns[1].Visible = false;
                        gvExcelCD.Columns[2].Visible = false;
                        gvExcelCD.Columns[3].Caption = "Concepto";
                        gvExcelCD.Columns[3].Width = 79;
                        gvExcelCD.Columns[4].Visible = false;
                        gvExcelCD.Columns[3].ImageAlignment = StringAlignment.Near;


                        gvExcelCD.OptionsView.ColumnAutoWidth = true;
                        gvExcelCD.OptionsView.RowAutoHeight = true;


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


    }
}