using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using System.Linq;
using SGE.Entity;
using SGE.BusinessLogic;
using SGE.WindowForms.Otros.Costos;
using SGE.WindowForms.Contabilidad.Costos;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class Frm07ConsultaStockValorizadoFecha : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        private List<EKardexValorizado> mLista = new List<EKardexValorizado>();
        private string FechaFin = "";
        
        #endregion

        #region "Eventos"

        public Frm07ConsultaStockValorizadoFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultaStockValorizadoFecha_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
            {
                dtmFechaHasta.EditValue = DateTime.Now;
            }
            else
            {
                dtmFechaHasta.DateTime = Convert.ToDateTime("31/12/" + Parametros.intEjercicio);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            DateTime dtFecFin = Convert.ToDateTime(dtmFechaHasta.EditValue);
            if (dtFecFin.Year == Parametros.intEjercicio)
            {
                Carga(Convert.ToDateTime("01/01/" + Parametros.intEjercicio), dtmFechaHasta.DateTime);
                FechaFin = dtmFechaHasta.Text;
            }
            else
            {
                XtraMessageBox.Show("La consulta no está dentro del año de ejercicio", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (mLista.Count > 0)
        //    {
        //        FrmListaMovimientoValorizadoProducto KardexProducto = new FrmListaMovimientoValorizadoProducto();
        //        KardexProducto.MiEvento += new FrmListaMovimientoValorizadoProducto.DelegadoMensaje(Modify);
        //        KardexProducto.modificarFechaToolStripMenuItem.Enabled = true;
        //        KardexProducto.Text = "SGE - Movimientos hasta " + dtmFechaHasta.Text;
        //        KardexProducto.Producto = viewStock.GetFocusedRowCellValue("strDesProducto").ToString();
        //        KardexProducto.UM = viewStock.GetFocusedRowCellValue("unidc_vabreviatura_unidad_medida").ToString();
        //        //KardexProducto.Estado = viewStock.GetFocusedRowCellValue("estac_vdescripcion").ToString();
        //        //KardexProducto.Situacion = viewStock.GetFocusedRowCellValue("situc_vdescripcion").ToString();
        //        KardexProducto.FechaIni = DateTime.Parse("01/01/" + Parametros.intEjercicio);
        //        KardexProducto.FechaFin = dtmFechaHasta.DateTime;
        //        KardexProducto.IdAlmacen = int.Parse(viewStock.GetFocusedRowCellValue("almcc_icod_almacen").ToString());
        //        KardexProducto.IdProducto = int.Parse(viewStock.GetFocusedRowCellValue("prdc_icod_producto").ToString());
        //        KardexProducto.Show();
        //    }
        //}

        #endregion

        #region "Metodos"

        public void Carga(DateTime FechaIni, DateTime FechaFin)
        {
            mLista = new BAlmacen().ListarKardexValorizadoInventarioResumen(FechaIni, FechaFin);
            grdStock.DataSource = mLista;
        }

        void Modify()
        {
            Carga(Convert.ToDateTime("01/01/" + Parametros.intEjercicio), dtmFechaHasta.DateTime);
        }

        #endregion

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EKardexValorizado obe = (EKardexValorizado)viewStock.GetRow(viewStock.FocusedRowHandle);
            if (obe != null)
            {
                rpt02StockValorizado rpt = new rpt02StockValorizado();

                var lista = new BAlmacen().ListarKardexValorizadoInventario(obe.almcc_icod_almacen, obe.prdc_icod_producto, Convert.ToDateTime("01/01/" + Parametros.intEjercicio), dtmFechaHasta.DateTime);
                rpt.Cargar(obe, lista, obe.strDesAlmacenCtbl);
            }
        }

        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mLista.Count > 0)
            {
                rptStockValorizadoFecha rpt = new rptStockValorizadoFecha();
                rpt.carga(mLista.OrderBy(obe => obe.prdc_icod_producto).ToList(), "STOCK ACTUAL VALORIZADO DE PRODUCTOS", "POR ALMACENES AL " + FechaFin);
            }
        }

        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Verkrdex();
        }
        private void Verkrdex()
        {
            try
            {
                EKardexValorizado Obe = (EKardexValorizado)viewStock.GetRow(viewStock.FocusedRowHandle);
                if (Obe == null)
                    return;
                frmConsultaKardexValorizadoPorFecAlmProd frm = new frmConsultaKardexValorizadoPorFecAlmProd();
                frm.Text = String.Format("Kardex: {0} - {1}", Obe.strDesAlmacenCtbl, Obe.strDesProducto);
                frm.f1 = Convert.ToDateTime("01/01/" + Parametros.intEjercicio);
                frm.f2 = dtmFechaHasta.DateTime;
                frm.intAlmacen = Obe.almcc_icod_almacen;
                frm.intProducto = Obe.prdc_icod_producto;
                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}