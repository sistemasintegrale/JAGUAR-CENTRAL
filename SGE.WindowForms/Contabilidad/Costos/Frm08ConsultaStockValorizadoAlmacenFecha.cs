using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGI.WindowsForm.Otros.Costos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Costos
{
    public partial class Frm08ConsultaStockValorizadoAlmacenFecha : DevExpress.XtraEditors.XtraForm
    {
        #region "Propiedades"

        private List<EKardexValorizado> mLista = new List<EKardexValorizado>();

        #endregion

        #region "Eventos"

        public Frm08ConsultaStockValorizadoAlmacenFecha()
        {
            InitializeComponent();
        }

        private void FrmConsultaStockValorizadoAlmacenFecha_Load(object sender, EventArgs e)
        {
            dtmFechaFin.EditValue = DateTime.Now;
            CargarControles();
        }
        private void CargarControles()
        {
            BSControls.LoaderLook(lkpAlmacen, new BContabilidad().listarAlmacenContable(), "almcc_vdescripcion", "almcc_icod_almacen", true);
        }


        private void btnProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            DateTime dtFecFin = Convert.ToDateTime(dtmFechaFin.EditValue);

            if (string.IsNullOrEmpty(lkpAlmacen.Text))
            {
                XtraMessageBox.Show("Seleccione un almacén", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(btnProducto.Text))
            {
                XtraMessageBox.Show("Seleccione un producto", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Carga(Convert.ToInt32(lkpAlmacen.Tag), Convert.ToInt32(btnProducto.Tag), DateTime.Parse("01/01/" + Parametros.intEjercicio), dtmFechaFin.DateTime);
            //Carga(Convert.ToInt32(btnAlmacen.Tag), Convert.ToInt32(btnProducto.Tag), dtmFechaInicio.DateTime, dtmFechaFin.DateTime);
        }

        private void ingresoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EKardexValorizado Obe = (EKardexValorizado)viewKardex.GetRow(viewKardex.FocusedRowHandle);
            if (Obe != null)
            {
                if (Obe.dcmlSalida > 0)
                {
                    XtraMessageBox.Show("Operación válida solo para los movimientos de ingreso", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                FrmManteActualizaCostoManual frm = new FrmManteActualizaCostoManual();
                frm.MiEvento += new FrmManteActualizaCostoManual.DelegadoMensaje(form2_MiEvento);
                frm.Obe = Obe;
                frm.lblProducto.Text = Obe.strDesProducto;
                frm.IdKardexValorizado = Convert.ToInt32(Obe.kardv_icod_correlativo);
                frm.MontoTotal = Convert.ToDecimal(Obe.kardv_monto_total_compra);
                frm.MontoManual = Convert.ToDecimal(Obe.kardv_nmonto_ingreso_manual);
                frm.Show();
                frm.SetInsert();
            }
        }

        #endregion

        #region "Metodos"

        public void Carga(int IdAlmacen, int IdProducto, DateTime FechaIni, DateTime FechaFin)
        {
            mLista = new BAlmacen().ListarKardexValorizadoInventario(Convert.ToInt32(lkpAlmacen.EditValue), IdProducto, FechaIni, FechaFin);
            //mLista = new BAlmacen().ListarKardexValorizadoInventarioResumen(FechaIni, FechaFin);
            grdKardex.DataSource = mLista;
        }

        void form2_MiEvento()
        {
            Carga(Convert.ToInt32(lkpAlmacen.Tag), Convert.ToInt32(btnProducto.Tag), DateTime.Parse("01/01/" + Parametros.intEjercicio), dtmFechaFin.DateTime);
        }



        private void listarProducto()
        {
            try
            {
                frmListarProducto frm = new frmListarProducto();
                frm.flag_solo_prods = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnProducto.Tag = frm._Be.prdc_icod_producto;
                    btnProducto.Text = frm._Be.prdc_vdescripcion_larga;

                }
                dtmFechaInicio.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal? NullVal;
            NullVal = null;
            EKardexValorizado obe = (EKardexValorizado)viewKardex.GetRow(viewKardex.FocusedRowHandle);
            if (obe != null)
            {
                rpt02StockValorizado rpt = new rpt02StockValorizado();
                var lista =
                    new BAlmacen().ListarKardexValorizadoInventario(obe.almcc_icod_almacen,
                    obe.prdc_icod_producto,
                    Convert.ToDateTime("01/01/" + Parametros.intEjercicio),
                    dtmFechaFin.DateTime);
                lista.ForEach(x =>
                {
                    x.kardv_monto_total_compra = (x.kardv_monto_total_compra != null) ?
                        Math.Round(Convert.ToDecimal(x.kardv_monto_total_compra), 2) : NullVal;
                });
                rpt.Cargar(obe, lista, lkpAlmacen.Text);
            }
        }

    }
}