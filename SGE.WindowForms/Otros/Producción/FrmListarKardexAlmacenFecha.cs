using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Producción.Consulta;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Produccion
{
    public partial class FrmListarKardexAlmacenFecha : DevExpress.XtraEditors.XtraForm
    {
        public string descrip_producto;
        public DateTime fechaIni;
        public DateTime fechaFina;
        public DateTime f1, f2;
        public int intAlmacen, intProducto;
        public bool intervaloFechas;
        List<EProdKardex> mlista = new List<EProdKardex>();
        public FrmListarKardexAlmacenFecha()
        {
            InitializeComponent();
        }
        public void Buscar()
        {

            //this.Text = "KARDEX " + obj.vcodigo_externo + " - " + obj.descripcion_producto;
            //if (intervaloFechas == false)
            //{
            //    mlista = new BCentral().ListarProdKardexFechaAlmacen(obj);
            //    dgrStockAlmacen.DataSource = mlista;
            //}
            //if (intervaloFechas == true)
            //{
            //    mlista = new BCentral().ListarProdKardexFechaIntervaloAlmacen(f1, f2, intAlmacen, intProducto);
            //    dgrStockAlmacen.DataSource = mlista;
            //}
            try
            {
                mlista = new BCentral().ListarProdKardexFechaIntervaloAlmacen(f1, f2, intAlmacen, intProducto);
                dgrStockAlmacen.DataSource = mlista;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmListarKardexAlmacenFecha_Load(object sender, EventArgs e)
        {
            Buscar();
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlista.Count > 0)
            {
                rptKardex2 rpt = new rptKardex2();
                rpt.cargar(descrip_producto, fechaIni.ToString(), fechaFina.ToString(), mlista);
            }
        }
    }
}