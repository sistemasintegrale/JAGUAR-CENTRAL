using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptBalanceGeneral : DevExpress.XtraReports.UI.XtraReport
    {

        int cont = 0;


        public rptBalanceGeneral()
        {
            InitializeComponent();
        }
        public void cargar(List<EBalance> Lista, string Mes, int Año)
        {
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "BALANCE GENERAL AL MES DE " + Mes.ToUpper() + " DE " + Año;
            xrlblTitulo2.Text = "EXPRESADO EN M.N.";

            this.DataSource = Lista;

            lblConcepto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "blgc_vconcepto", "")});

            lblMonto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "Monto", "{0:n2}")});

            lblTipoLinea.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tablc_icod_linea_registro", "")});

            this.ShowPreview();
        }

        struct EBalance2
        {
            public string CONCEPTO { get; set; }
            public decimal MONTO { get; set; }
        }

        private void lblTipoLinea_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 359 || Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 358)
            {
                Linea1.Visible = true;
            }
            else
            {
                Linea1.Visible = false;
            }

            if (Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 355 || Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 354 || Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 359 || Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 358)
            {
                lblConcepto.Font = new Font("Courier New", 8, FontStyle.Bold);
            }
            else
            {
                lblConcepto.Font = new Font("Courier New", 8);
            }


            if (Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 359)
            {
                Linea2.Visible = true;
                Linea3.Visible = true;
                xrPageBreak1.Visible = true;
            }
            else
            {
                Linea2.Visible = false;
                Linea3.Visible = false;
                xrPageBreak1.Visible = false;
            }

            if (Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 355 || Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 354)
            {
                lblMonto.Text = "";
            }


        }
    }
}
