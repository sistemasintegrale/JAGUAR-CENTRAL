using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Almacen.Listados;
using SGE.WindowForms.Reportes.Almacen.Consultas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Almacén.Consultas
{
    public partial class frm06KardexPorMotivo : DevExpress.XtraEditors.XtraForm
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm06KardexPorMotivo));
        List<EKardex> lstKardex = new List<EKardex>();
        DateTime f1, f2;
        bool flagButtonPressed = false;

        public frm06KardexPorMotivo()
        {
            InitializeComponent();
        }

        public class ETipoMov
        {
            public int intTipoMov { get; set; }
            public string strTipoMov { get; set; }
        }


        private void imprimir()
        {
            if (lstKardex.Count > 0)
            {
                rptKardex rpt = new rptKardex();
                rpt.cargar(String.Format("MOVIMIENTOS DEL PRODUCTO: {0}", bteProducto.Text), String.Format("{0} POR {1}", lkpTipoMov.Text.ToUpper(), lkpMotivo.Text.ToUpper()), lstKardex);
            }
        }
        private void cargar()
        {
            BaseEdit oBase = null;
            try
            {
                f1 = Convert.ToDateTime("01/01/" + Parametros.intEjercicio.ToString());
                f2 = DateTime.Now;

                if (Convert.ToInt32(bteProducto.Tag) == 0)
                {
                    oBase = bteProducto;
                    throw new ArgumentException("Seleccione el producto a consultar");
                }

                lstKardex = new BAlmacen().listarKardexPorMotivoAlmacenProducto(f1, f2, Convert.ToInt32(lkpTipoMov.EditValue), Convert.ToInt32(lkpMotivo.EditValue), Convert.ToInt32(bteProducto.Tag));
                grdKardex.DataSource = lstKardex;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            imprimir();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            flagButtonPressed = true;
            cargar();
        }


        private void bteProducto_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            listarProducto();
        }

        private void listarProducto()
        {
            try
            {
                frmListarProducto Producto = new frmListarProducto();
                if (Producto.ShowDialog() == DialogResult.OK)
                {
                    bteProducto.Tag = Producto._Be.prdc_icod_producto;
                    bteProducto.Text = Producto._Be.prdc_vdescripcion_larga;
                    lstKardex.Clear();
                    viewKardex.RefreshData();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void cbActivarFiltro_CheckedChanged(object sender, EventArgs e)
        {
            viewKardex.OptionsView.ShowAutoFilterRow = cbActivarFiltro.Checked;
            viewKardex.ClearColumnsFilter();
        }

        private void lkpTipoMov_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lkpTipoMov.EditValue) == Parametros.intKardexIn)
                BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(Parametros.intIngresoAlmacen), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
            else if (Convert.ToInt32(lkpTipoMov.EditValue) == Parametros.intKardexOut)
                BSControls.LoaderLook(lkpMotivo, new BGeneral().listarTablaRegistro(Parametros.intSalidaAlmacen), "tarec_vdescripcion", "tarec_iid_tabla_registro", true);
        }

        private void frm07KardexPorMotivo_Load(object sender, EventArgs e)
        {
            List<ETipoMov> lstTipoMov = new List<ETipoMov>();
            for (int i = 0; i < 2; i++)
            {
                ETipoMov x_ = new ETipoMov();
                x_.intTipoMov = i;
                x_.strTipoMov = (i == 0) ? "Salidas" : "Ingresos";
                lstTipoMov.Add(x_);
            }
            BSControls.LoaderLook(lkpTipoMov, lstTipoMov, "strTipoMov", "intTipoMov", true);
        }

        private void lkpMotivo_EditValueChanged(object sender, EventArgs e)
        {
            if (flagButtonPressed)
                cargar();
        }
    }
}