using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmConsultaKardexPorFecAlmProd : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultaKardexPorFecAlmProd));
        List<EKardex> lstKardex = new List<EKardex>();
        public EKardex Obe = new EKardex();
        public DateTime f1, f2;
        public int intAlmacen, intProducto;
        public frmConsultaKardexPorFecAlmProd()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            buscar();
        }
        void imprimir()
        {
            rptKardexProducto rpt = new rptKardexProducto();

            lstKardex = new BAlmacen().listarKardexPorFechaAlmacenProducto(f1, f2, Obe.almac_icod_almacen, Obe.prdc_icod_producto);
            rpt.cargar(Obe, lstKardex);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void buscar()
        {
            try
            {
                lstKardex = new BAlmacen().listarKardexPorFechaAlmacenProducto(f1, f2, intAlmacen, intProducto);
                grdKardex.DataSource = lstKardex;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}