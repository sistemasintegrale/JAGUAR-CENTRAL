using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class FrmResumenMovimientoProductos : DevExpress.XtraEditors.XtraForm
    {
        int ACTUALIZARkARDEX = 0;
        List<EProdconsultaKardex> mlist = new List<EProdconsultaKardex>();
        public FrmResumenMovimientoProductos()
        {
            InitializeComponent();
        }

        private void FrmResumenMovimientoProductos_Load(object sender, EventArgs e)
        {
            dtpFechaIni.EditValue = Convert.ToDateTime("01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());
            dtpFechaFin.EditValue = DateTime.Now;
        }

        private void ListarKardexPorFecha()
        {

            EProdReporte oRe = new EProdReporte
            {
                sfechaInicio = Convert.ToDateTime(dtpFechaIni.EditValue),
                sfechaFinal = Convert.ToDateTime(dtpFechaFin.EditValue),
                puvec_icod_punto_venta = 2
            };

            mlist = new BCentral().ListarProdResumenMovimientoProductos(oRe);
            dgrNotaIngreso.DataSource = mlist;
            ACTUALIZARkARDEX = 1;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Validate())
                ListarKardexPorFecha();
        }
        private Boolean Validate()
        {
            Boolean Flag = true;
            try
            {
                if (Convert.ToDateTime(dtpFechaIni.EditValue) >= Convert.ToDateTime(dtpFechaFin.EditValue))
                    throw new Exception("La fecha de Inicio debe ser menor a la fecha final");
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Flag = false;
            }
            return Flag;
        }





        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count != 0)
            {
                FrmListarKardexAlmacenFecha frm = new FrmListarKardexAlmacenFecha();
                frm.intervaloFechas = true;
                EProdconsultaKardex ob = (EProdconsultaKardex)viewProductoEspecifico.GetRow(viewProductoEspecifico.FocusedRowHandle);
                EProdStockProducto obj = new EProdStockProducto
                {
                    stocc_iid_producto = Convert.ToInt32(ob.kardc_iid_producto),
                    stocc_iid_almacen = Convert.ToInt32(ob.kardc_iidalmacen),
                    fecha = Convert.ToDateTime(dtpFechaIni.EditValue),
                    fechafin = Convert.ToDateTime(dtpFechaFin.EditValue),
                    descripcion_producto = ob.pr_vdescripcion_producto,
                    vcodigo_externo = ob.pr_vcodigo_externo
                };
                frm.Buscar();
                frm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No Hay Registros", "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtconsultaKardex;
            if (mlist.Count > 0)
            {
                dtconsultaKardex = BSConverter<EProdconsultaKardex>.Convert(mlist);
                rptResumenMovProductosFechas rptMotivoResumen = new rptResumenMovProductosFechas();
                rptMotivoResumen.cargar(dtconsultaKardex, dtpFechaIni.Text, dtpFechaFin.Text);
                rptMotivoResumen.ShowPreview();
            }
        }



    }
}