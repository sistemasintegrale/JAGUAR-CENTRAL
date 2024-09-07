using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Otros.Compras;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Compras.Consultas_de_Compras
{
    public partial class frm10ConsultarOC : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm10ConsultarOC));
        List<EOrdenCompra> lstOrdenCompra = new List<EOrdenCompra>();
        List<EOrdenCompra> lstOrdenCompraRPTConsultar = new List<EOrdenCompra>();
        int xposition;
        public int Generado = 0;
        public int EntregadoParcial = 0;
        public int EntregadoTotal = 0;
        DateTime f1, f2;
        public frm10ConsultarOC()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            f1 = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
            f2 = DateTime.Now;
            dtFecha1.EditValue = f1;
            dtFecha2.EditValue = f2;
            cargar();
        }


        private void cargar()
        {
            lstOrdenCompra = new BCompras().ListarOrdenCompra();
            grdManoObra.DataSource = lstOrdenCompra;
        }


        private void filtrar()
        {
            grdManoObra.DataSource = lstOrdenCompra.Where(x => x.ococ_numero_orden_compra.Contains(txtCodigo.Text.ToUpper()) &&
                x.proc_vnombrecompleto.Contains(txtDescripcion.Text.ToUpper())).ToList();
        }

        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void txtDescripcion_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        //private void cbActivarFiltro_CheckedChanged_1(object sender, EventArgs e)
        //{
        //    viewManoObra.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
        //    viewManoObra.ClearColumnsFilter();
        //}
        void reload(int intIcod)
        {
            cargar();
            viewManoObra.FocusedRowHandle = xposition;
            viewManoObra.Focus();
        }
        private void verPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenCompra Obe = (EOrdenCompra)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                xposition = viewManoObra.FocusedRowHandle;
                frmManteOrdenCompraConsultar frm = new frmManteOrdenCompraConsultar();
                frm.MiEvento += new frmManteOrdenCompraConsultar.DelegadoMensaje(reload);
                frm.ococ_icod_orden_compra = Obe.ococ_icod_orden_compra;
                frm.Obe = Obe;
                frm.Show();
                frm.setValues();
                frm.btnProveedor.Enabled = false;
                frm.setDobleClick();

            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<EOrdenCompra> lstOrdenCompraActualizar = new List<EOrdenCompra>();
            lstOrdenCompraActualizar = new BCompras().ListarOrdenCompra();
            List<EOrdenCompra> lstOrdenCompraDetalleActualizar = new List<EOrdenCompra>();
            lstOrdenCompraActualizar.ForEach(x =>
            {
                lstOrdenCompraDetalleActualizar = new BCompras().ListarOrdenCompraDetalle(x.ococ_icod_orden_compra);
                lstOrdenCompraDetalleActualizar.ForEach(xd =>
                {
                    new BCompras().ActualizarCantidadRecibidaOC(x.ococ_icod_orden_compra, Convert.ToInt32(xd.prdc_icod_producto), Convert.ToInt32(xd.ocod_icod_detalle_oc));
                });
            });
            cargar();
        }

        private void cbGenerado_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGenerado.Checked == true)
            {
                Generado = 281;
            }
            else
            {
                Generado = 0;
            }
            lstOrdenCompraRPTConsultar = new BCompras().ListarOrdenCompraRPTConsultar(Generado, EntregadoParcial, EntregadoTotal, f1, f2);
            grdManoObra.DataSource = lstOrdenCompraRPTConsultar;
        }

        private void cbEntregadoParcial_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEntregadoParcial.Checked == true)
            {
                EntregadoParcial = 282;
            }
            else
            {
                EntregadoParcial = 0;
            }
            lstOrdenCompraRPTConsultar = new BCompras().ListarOrdenCompraRPTConsultar(Generado, EntregadoParcial, EntregadoTotal, f1, f2);
            grdManoObra.DataSource = lstOrdenCompraRPTConsultar;
        }

        private void cbEntregadoTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEntregadoTotal.Checked == true)
            {
                EntregadoTotal = 283;
            }
            else
            {
                EntregadoTotal = 0;
            }
            lstOrdenCompraRPTConsultar = new BCompras().ListarOrdenCompraRPTConsultar(Generado, EntregadoParcial, EntregadoTotal, f1, f2);
            grdManoObra.DataSource = lstOrdenCompraRPTConsultar;
        }

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTodos.Checked == true)
            {
                lstOrdenCompra = new BCompras().ListarOrdenCompra();
                grdManoObra.DataSource = lstOrdenCompra;
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (lstAlmacenes.Count > 0)
            //{
            rptOrdenCompraConsultar rpt = new rptOrdenCompraConsultar();
            rpt.cargar("RELACION ORDEN COMPRA", String.Format("DEL {0} AL {1}", f1.ToShortDateString(), f2.ToShortDateString()),
                Convert.ToBoolean(cbGenerado.Checked), Convert.ToBoolean(cbEntregadoParcial.Checked), Convert.ToBoolean(cbEntregadoTotal.Checked),
                lstOrdenCompraRPTConsultar);
            //}      
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void dtFecha1_EditValueChanged(object sender, EventArgs e)
        {
            f1 = Convert.ToDateTime(dtFecha1.EditValue);
            lstOrdenCompraRPTConsultar = new BCompras().ListarOrdenCompraRPTConsultar(Generado, EntregadoParcial, EntregadoTotal, f1, f2);
            grdManoObra.DataSource = lstOrdenCompraRPTConsultar;
        }

        private void dtFecha2_EditValueChanged(object sender, EventArgs e)
        {
            f2 = Convert.ToDateTime(dtFecha2.EditValue);
            lstOrdenCompraRPTConsultar = new BCompras().ListarOrdenCompraRPTConsultar(Generado, EntregadoParcial, EntregadoTotal, f1, f2);
            grdManoObra.DataSource = lstOrdenCompraRPTConsultar;
        }

        private void consultarFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOrdenCompra Obe = (EOrdenCompra)viewManoObra.GetRow(viewManoObra.FocusedRowHandle);
            if (Obe != null)
            {
                xposition = viewManoObra.FocusedRowHandle;
                FrmListarFacturaXOrdenCompra frm = new FrmListarFacturaXOrdenCompra();
                frm.ococ_icod_orden_compra = Obe.ococ_icod_orden_compra;
                frm.Show();

            }
        }

    }
}