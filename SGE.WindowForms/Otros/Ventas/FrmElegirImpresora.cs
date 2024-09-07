using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmElegirImpresora : DevExpress.XtraEditors.XtraForm
    {

        public string url_impresora;

        List<Eimpresora> mlist = new List<Eimpresora>();
        public FrmElegirImpresora()
        {
            InitializeComponent();
        }

        private void FrmElegirImpresora_Load(object sender, EventArgs e)
        {

        }
        public void cargar()
        {

            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                Eimpresora _BE = new Eimpresora();
                _BE.nombreimpresora = strPrinter;
                _BE.url_impresora = strPrinter;
                mlist.Add(_BE);

            }
            dgrImpresora.DataSource = mlist;

        }
        public class Eimpresora
        {
            public string url_impresora { get; set; }
            public string nombreimpresora { get; set; }

            public Eimpresora()
            {

            }
        }

        private void dgrImpresora_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ckImpresora.Checked)
            {
                url_impresora = ImpresoraPorDefecto();
            }
            else
            {
                Eimpresora Obe = (Eimpresora)gvimpresora.GetRow(gvimpresora.FocusedRowHandle);
                if (Obe != null)
                {
                    url_impresora = Obe.url_impresora;
                }

            }
            this.DialogResult = DialogResult.OK;

        }
        private string ImpresoraPorDefecto()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }

            return string.Empty;
        }

        private void ckImpresora_CheckedChanged(object sender, EventArgs e)
        {
            if (ckImpresora.Checked == true)
            {
                dgrImpresora.Enabled = false;
            }
            else
            {
                dgrImpresora.Enabled = true;
            }
        }

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}