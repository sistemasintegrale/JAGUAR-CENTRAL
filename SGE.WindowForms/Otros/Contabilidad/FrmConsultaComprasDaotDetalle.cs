using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class FrmConsultaComprasDaotDetalle : DevExpress.XtraEditors.XtraForm
    {
        public FrmConsultaComprasDaotDetalle()
        {
            InitializeComponent();
        }

        List<EComprasDaotDetalle> Lista = new List<EComprasDaotDetalle>();
        public long icod_prov;
        public string nombre_prov;

        private void FrmConsultaComprasDaotDetalle_Load(object sender, EventArgs e)
        {
            this.Text += nombre_prov;
            Carga();
            grd.DataSource = Lista;
        }

        public void Carga()
        {
            Lista = new BCompras().ListarComprasDaotDetallexProveedor(icod_prov, Parametros.intEjercicio);
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}