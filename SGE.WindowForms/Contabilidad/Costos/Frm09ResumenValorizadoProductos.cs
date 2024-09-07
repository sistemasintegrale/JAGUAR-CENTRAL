using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Costos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class Frm09ResumenValorizadoProductos : DevExpress.XtraEditors.XtraForm
    {
        DateTime dtFInicio;
        DateTime dtFFin;
        List<EKardexValorizado> lstResumenValorizado = new List<EKardexValorizado>();
        public Frm09ResumenValorizadoProductos()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(dtInicio.EditValue).Year != Parametros.intEjercicio)
                    throw new ArgumentException("El rango de fehcas debe estar dentro el Año de Ejercicio " + Parametros.intEjercicio.ToString());
                if (Convert.ToDateTime(dtFin.EditValue).Year != Parametros.intEjercicio)
                    throw new ArgumentException("El rango de fehcas debe estar dentro el Año de Ejercicio " + Parametros.intEjercicio.ToString());

                dtFInicio = Convert.ToDateTime(dtInicio.EditValue);
                dtFFin = Convert.ToDateTime(dtFin.EditValue);
                cargar();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargar()
        {
            lstResumenValorizado = new BAlmacen().listarResumenValorizadoProductos(Parametros.intEjercicio, dtFInicio, dtFFin);
            grdResumen.DataSource = lstResumenValorizado;
            viewResumen.Focus();
        }
        private void imprimir()
        {
            if (lstResumenValorizado.Count == 0)
                return;
            rptResumenValorizado rpt = new rptResumenValorizado();
            string titulo = "RESUMEN VALORIZADO DEL " + dtFInicio.ToShortDateString() + " AL " + dtFFin.ToShortDateString();
            rpt.carga(lstResumenValorizado, titulo, "");

        }

        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void Frm04ResumenValorizadoProductos_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
            {
                dtInicio.EditValue = Convert.ToDateTime("01/" + DateTime.Now.Month + "/" + Parametros.intEjercicio);
                dtFin.EditValue = DateTime.Now;
            }
        }

        private void kardexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EKardexValorizado Obe = (EKardexValorizado)viewResumen.GetRow(viewResumen.FocusedRowHandle);
            if (Obe != null)
            {
                FrmListaMovimientoValorizadoProducto KardexProducto = new FrmListaMovimientoValorizadoProducto();
                KardexProducto.Text = String.Format("Movimientos desde {0} hasta {1} ", dtFInicio.ToShortDateString(), dtFFin.ToShortDateString());
                KardexProducto.Producto = viewResumen.GetFocusedRowCellValue("strDesProducto").ToString();
                KardexProducto.UM = viewResumen.GetFocusedRowCellValue("unidc_vabreviatura_unidad_medida").ToString();
                //KardexProducto.Estado = viewResumen.GetFocusedRowCellValue("estac_vdescripcion").ToString();
                //KardexProducto.Situacion = viewResumen.GetFocusedRowCellValue("situc_vdescripcion").ToString();
                KardexProducto.FechaIni = dtFInicio;
                KardexProducto.FechaFin = dtFFin;
                KardexProducto.IdAlmacen = Obe.almcc_icod_almacen;
                KardexProducto.IdProducto = Obe.prdc_icod_producto;
                KardexProducto.Show();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EKardexValorizado obe = (EKardexValorizado)viewResumen.GetRow(viewResumen.FocusedRowHandle);
            if (obe != null)
            {
                rpt02StockValorizado rpt = new rpt02StockValorizado();
                string[] arreglo = obe.prdc_icod_producto.ToString().Split("-".ToCharArray());
                obe.prdc_icod_producto = Convert.ToInt32(arreglo[0]);
                var lista = new BAlmacen().ListarKardexValorizadoInventario(obe.almcc_icod_almacen, obe.prdc_icod_producto, dtFInicio, dtFFin);
                rpt.Cargar(obe, lista, obe.strDesAlmacenCtbl);
            }
        }
    }
}