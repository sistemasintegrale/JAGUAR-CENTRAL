using DevExpress.XtraEditors;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Reportes.Contabilidad.Registros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmRangoCuentas : DevExpress.XtraEditors.XtraForm
    {
        public List<ECuentaContable> mlista = new List<ECuentaContable>();
        public frmRangoCuentas()
        {
            InitializeComponent();
        }

        private void FrmImpresionCuentaContable_Load(object sender, EventArgs e)
        {

        }
        private void bteCuentaI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                frm.flagSeleccionImpresion = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuentaI.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuentaI.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaI.Text = frm._Be.ctacc_nombre_descripcion;
                }
            }
        }
        private void bteCuentaF_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (frmListarCuentaContable frm = new frmListarCuentaContable())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bteCuentaF.Text = frm._Be.ctacc_numero_cuenta_contable;
                    bteCuentaF.Tag = frm._Be.ctacc_icod_cuenta_contable;
                    txtCuentaF.Text = frm._Be.ctacc_nombre_descripcion;
                }
            }
        }
        private void metodo2()
        {
            List<ECuentaContable> aux = new List<ECuentaContable>();

            aux = new BContabilidad().listarCuentaContableImpresion(bteCuentaI.Text.Replace(".", "").ToString(),
                bteCuentaF.Text.Replace(".", "").ToString());
            aux.ForEach(x =>
            {

                if (x.ctacc_nivel_cuenta == 2)
                {
                    x.ctacc_numero_cuenta_contable = "   " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
                else if (x.ctacc_nivel_cuenta == 3)
                {
                    x.ctacc_numero_cuenta_contable = "      " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
                else if (x.ctacc_nivel_cuenta == 4)
                {
                    x.ctacc_numero_cuenta_contable = "        " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
                else if (x.ctacc_nivel_cuenta == 5)
                {
                    x.ctacc_numero_cuenta_contable = "          " + x.ctacc_numero_cuenta_contable;
                    x.ctacc_nombre_descripcion = "   " + x.ctacc_nombre_descripcion;
                }
            });

            rpt03CuentaContable reporte = new rpt03CuentaContable();
            reporte.cargar(aux, Convert.ToInt32(bteCuentaI.Text.Replace(".", "").Trim()), Convert.ToInt32(bteCuentaF.Text.Replace(".", "").Trim()));
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void btnImprimir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bteCuentaI.Tag == null || bteCuentaF.Tag == null)
                XtraMessageBox.Show("Ingrese un rango de cuentas", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                metodo2();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}