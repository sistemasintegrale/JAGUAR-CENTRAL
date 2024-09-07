using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Producción.Consulta
{
    public partial class FrmListarStockAlmacenFecha : DevExpress.XtraEditors.XtraForm
    {
        List<EProdStockProducto> mlista = new List<EProdStockProducto>();
        DateTime f1, f2;
        public FrmListarStockAlmacenFecha()
        {
            InitializeComponent();
        }
        private void Cargar()
        {
            BSControls.LoaderLook(lkpPuntoVenta, new BCentral().ListarPuntoVenta(), "puvec_vdescripcion", "puvec_icod_punto_venta", true);
            //lkpPuntoVenta.EditValue = ValPVTC.puvec_icod_punto_venta;
            lkpPuntoVenta.EditValue = 2;

        }
        private void FrmListarStockAlmacenCentralFecha_Load(object sender, EventArgs e)
        {
            dtpFechaFin.EditValue = DateTime.Now;
            Cargar();
        }
        private void Buscar()
        {
            BaseEdit oBase = null;
            try
            {
                f1 = DateTime.MinValue.AddYears(Parametros.intEjercicio - 1);

                f2 = Convert.ToDateTime(dtpFechaFin.EditValue);
                if (f2.Year != Parametros.intEjercicio)
                {
                    oBase = dtpFechaFin;
                    throw new ArgumentException("La fecha no esta dentro del año de ejercicio");
                }

                mlista = new BCentral().ListarProdStockPorAlmacen(f1, f2, Convert.ToInt32(LkpAlmacen.EditValue), null);
                dgrStockAlmacen.DataSource = mlista;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (mlista.Count != 0)
            //{
            //    FrmListarKardexAlmacenFecha frm = new FrmListarKardexAlmacenFecha();
            //    frm.intervaloFechas = true;
            //    EProdStockProducto ob = (EProdStockProducto)viewStockProducto.GetRow(viewStockProducto.FocusedRowHandle);
            //    EProdStockProducto obj = new EProdStockProducto
            //    {
            //        id_producto = ob.id_producto,
            //        id_almacen = Convert.ToInt32(LkpAlmacen.EditValue),
            //        fecha =DateTime.MinValue.AddYears(Parametros.intEjercicio - 1),
            //        fechafin = Convert.ToDateTime(dtpFechaFin.EditValue),
            //        puvec_icod_punto_venta = Convert.ToInt32(lkpPuntoVenta.EditValue),
            //        descripcion_producto = ob.descripcion_producto,
            //        vcodigo_externo = ob.vcodigo_externo
            //    };
            //    frm.Buscar(obj);
            //    frm.ShowDialog();
            //}
            //else
            //{
            //    XtraMessageBox.Show("No Hay Registros", "Informacion del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            try
            {
                EProdStockProducto Obe = (EProdStockProducto)viewStockProducto.GetRow(viewStockProducto.FocusedRowHandle);
                if (Obe == null)
                    return;
                FrmListarKardexAlmacenFecha frm = new FrmListarKardexAlmacenFecha();

                frm.Text = String.Format("Kardex: {0} - {1}", Obe.descripcion_almacen, Obe.descripcion_producto);
                frm.f1 = f1;
                frm.f2 = f2;
                frm.intAlmacen = Obe.stocc_iid_almacen;
                frm.intProducto = Obe.stocc_iid_producto;

                frm.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lkpPuntoVenta_EditValueChanged(object sender, EventArgs e)
        {
            BSControls.LoaderLook(LkpAlmacen, new BCentral().ListarProdAlmacen().Where(ob => ob.puvec_icod_punto_venta == Convert.ToInt32(lkpPuntoVenta.EditValue)).ToList(), "descripcion", "id_almacen", true);
            //BSControls.LoaderLook(LkpAlmacen, new BAlmacen().ListarAlmacen().ToList(), "descripcion", "id_almacen", true);
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e) => Services.ExportarExcel(dgrStockAlmacen);

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                //rptListarStockAlmacenCentralFecha rpt = new rptListarStockAlmacenCentralFecha();
                //rpt.cargar(mlista, dtpFechaIni.Text, LkpAlmacen.Text);
            }
        }

    }
}