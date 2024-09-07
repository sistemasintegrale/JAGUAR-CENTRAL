using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Producción;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Producción.Caracteristicas
{
    public partial class frm06ParametroProduccion : DevExpress.XtraEditors.XtraForm
    {
        List<EParametroProduccion> lstParametro = new List<EParametroProduccion>();
        public frm06ParametroProduccion()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstParametro = new BAdministracionSistema().listarParametroProduccion();
            grdTipoDocumento.DataSource = lstParametro;
            viewTipoDocumento.Focus();
        }
        void reload()
        {
            cargar();
        }
        private void modificar()
        {
            EParametroProduccion Obe = (EParametroProduccion)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteParametroProduccion frm = new frmManteParametroProduccion();
            frm.MiEvento += new frmManteParametroProduccion.DelegadoMensaje(reload);
            frm.Show();
            frm.oBe = Obe;
            frm.txtNroOrdenPedido.Text = Obe.pmprc_inumero_orden_pedido.ToString();
            frm.txtNroFichaTecnica.Text = Obe.pmprc_inumero_ficha_tecnica.ToString();
            frm.txtOrdenProduccion.Text = Obe.pmprc_inumero_orden_produccion.ToString();
            frm.txtNotaPedido.Text = Obe.pmprc_inumero_nota_pedido.ToString();
            frm.txtRutaBase.Text = Obe.pmprc_vruta_base;
            frm.txtRutaFichaTecnica.Text = Obe.pmprc_vficha_tecnica;
            frm.txtRutaOrdenPedido.Text = Obe.pmprc_vorden_pedido;
            frm.txtRutaOrdenProduccion.Text = Obe.pmprc_vorden_produccion;
            frm.txtRutaModelo.Text = Obe.pmprc_vmodelo;
            frm.txtRuta.Text = Obe.pmprc_vruta;
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EParametroProduccion Obe = (EParametroProduccion)viewTipoDocumento.GetRow(viewTipoDocumento.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteParametroProduccion frm = new frmManteParametroProduccion();
            frm.MiEvento += new frmManteParametroProduccion.DelegadoMensaje(reload);
            frm.Show();
        }
    }
}