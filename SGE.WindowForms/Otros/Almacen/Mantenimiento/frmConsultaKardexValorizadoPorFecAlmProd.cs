using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    public partial class frmConsultaKardexValorizadoPorFecAlmProd : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultaKardexPorFecAlmProd));
        List<EKardexValorizado> lstKardex = new List<EKardexValorizado>();
        public DateTime f1, f2;
        public int intAlmacen, intProducto;
        public frmConsultaKardexValorizadoPorFecAlmProd()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            try
            {
                lstKardex = new BAlmacen().listarKardexValorizadoPorFechaAlmacenProducto(f1, f2, intAlmacen, intProducto, Parametros.intEjercicio);
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