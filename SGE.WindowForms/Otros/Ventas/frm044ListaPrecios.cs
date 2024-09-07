using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Almacen.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class frm044ListaPrecios : DevExpress.XtraEditors.XtraForm
    {
        List<EParametro> lstParametro = new List<EParametro>();

        List<EProducto> lstProductos = new List<EProducto>();

        public delegate void DelegadoMensaje(int Cab_icod_correlativo);
        public event DelegadoMensaje MiEvento;
        public frm044ListaPrecios()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {

            lstParametro = new BAdministracionSistema().listarParametro();
            txtTipoCambio.Text = lstParametro[0].pm_ntipo_cambio.ToString();
            cargar();
        }

        private void cargar()
        {
            lstParametro = new BAdministracionSistema().listarParametro();

            lstProductos = new BAlmacen().listarProducto(Parametros.intEjercicio);
            grdProducto.DataSource = lstProductos;
            viewProducto.Focus();


        }
        void reload(int intIcod)
        {
            cargar();
            int index = lstProductos.FindIndex(x => x.prdc_icod_producto == intIcod);
            viewProducto.FocusedRowHandle = index;
            viewProducto.Focus();
        }
        private void nuevo()
        {



        }
        private void modificar()
        {


        }
        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (Obe == null)
                return;
            frmManteProductoAntiguo frm = new frmManteProductoAntiguo();
            frm.Obe = Obe;
            frm.SetCancel();
            frm.Show();
            frm.setValues();

        }


        private void buscarCriterio()
        {
            grdProducto.DataSource = lstProductos.Where(x =>
                                                   x.prdc_vcode_producto.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.prdc_vdescripcion_larga.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevo();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modificar();
        }

        private void btnNuevo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            nuevo();
        }

        private void btnModificar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            modificar();
        }



        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewProducto.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewProducto.ClearColumnsFilter();
        }

        private void viewProducto_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void viewProducto_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            EProducto Obe = (EProducto)viewProducto.GetRow(viewProducto.FocusedRowHandle);
            if (Obe != null)
            {
                Obe.flag_select_mod = true;
            }

            lstParametro = new BAdministracionSistema().listarParametro();
            //txtTotalPedido.Text = lstDetalle.Sum(ot => ot.lpedid_nCant_pedido).ToString();
            foreach (var _be in lstProductos)
            {
                if (_be.flag_select_mod == true)
                {
                    if (Convert.ToDecimal(_be.prdc_nPrecio_dolares) != 0)
                    {
                        _be.prdc_nPrecio_soles = Math.Round(Convert.ToDecimal(_be.prdc_nPrecio_dolares * Convert.ToDecimal(lstParametro[0].pm_ntipo_cambio)), 1);
                        _be.prdc_nPrecio_venta = Math.Round(Convert.ToDecimal(_be.prdc_nPrecio_soles - Convert.ToDecimal(_be.prdc_nPrecio_soles * _be.prdc_nPor_Descuento) / 100), 1);
                        _be.prdc_nPrecio_venta_Dol = Math.Round(Convert.ToDecimal(_be.prdc_nPrecio_dolares - Convert.ToDecimal(_be.prdc_nPrecio_dolares * _be.prdc_nPor_Descuento) / 100), 2);
                        _be.prdc_ntipo_cambio = Convert.ToDecimal(lstParametro[0].pm_ntipo_cambio);
                    }
                    else
                    {
                        _be.prdc_nPrecio_venta = Math.Round(Convert.ToDecimal(_be.prdc_nPrecio_soles - Convert.ToDecimal(_be.prdc_nPrecio_soles * _be.prdc_nPor_Descuento) / 100), 0);
                        _be.prdc_ntipo_cambio = Convert.ToDecimal(lstParametro[0].pm_ntipo_cambio);
                    }
                }
            }


        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var _bee in lstProductos.Where(ob => ob.flag_select_mod == true).ToList())
            {
                new BAlmacen().modificarProducto(_bee);
            }
            this.MiEvento(1);
            this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Esta opción modificará los precios de acuerdo al T/C: " + lstParametro[0].pm_ntipo_cambio.ToString() + " ¿Desea Continuar?", "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                lstParametro = new BAdministracionSistema().listarParametro();
                foreach (var _B in lstProductos)
                {
                    if (_B.prdc_nPrecio_dolares != 0)
                    {
                        _B.prdc_nPrecio_soles = Math.Round(Convert.ToDecimal(_B.prdc_nPrecio_dolares * Convert.ToDecimal(lstParametro[0].pm_ntipo_cambio)), 1);
                        _B.prdc_nPrecio_venta = Math.Round(Convert.ToDecimal(_B.prdc_nPrecio_soles - Convert.ToDecimal(_B.prdc_nPrecio_soles * _B.prdc_nPor_Descuento) / 100), 1);
                        _B.prdc_ntipo_cambio = Convert.ToDecimal(lstParametro[0].pm_ntipo_cambio);
                        _B.flag_select_mod = true;
                    }
                }
                grdProducto.RefreshDataSource();
            }
        }

        private void tipoDeCambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoCambio Fmr = new FrmTipoCambio();
            Fmr.Show();
        }

        private void txtTipoCambio_EditValueChanged(object sender, EventArgs e)
        {

        }


    }
}