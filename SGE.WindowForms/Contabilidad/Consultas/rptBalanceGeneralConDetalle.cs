using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class rptBalanceGeneralConDetalle : DevExpress.XtraReports.UI.XtraReport
    {

        int cont = 0;


        public rptBalanceGeneralConDetalle()
        {
            InitializeComponent();
        }

        List<EBalanceCtas> ListaCD;
        List<EBalance> ListaSD;
        public void cargar(List<EBalanceCtas> ListaCD, List<EBalance> ListaSD, string Mes, int Año)
        {
            this.ListaCD = ListaCD;
            this.ListaSD = ListaSD;
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "BALANCE GENERAL AL MES DE " + Mes.ToUpper() + " DE " + Año;
            xrlblTitulo2.Text = "EXPRESADO EN M.N.";
            this.DataSource = CrearData();
            DetailReport.DataMember = "EBalanceCtasC";







            lblConcepto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "blgc_vconcepto", "")});

            lblMonto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "Monto", "{0:n2}")});

            lblTipoLinea.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "tablc_icod_linea_registro", "")});



            //----------------detalle

            //lblConceptoDet.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", null, "ctacc_nombre_descripcion", "")});

            //lblMontoDet.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", null, "MontosCC", "{0:n2}")});
            lblCodigoDet.DataBindings.Add(new XRBinding("Text", null, "EBalanceCtasC.ctacc_numero_cuenta_contable"));
            lblConceptoDet.DataBindings.Add(new XRBinding("Text", null, "EBalanceCtasC.ctacc_nombre_descripcion"));
            lblMontoDet.DataBindings.Add(new XRBinding("Text", null, "EBalanceCtasC.MontosCC", "{0:n2}"));



            this.ShowPreview();
        }



        private EBalanceCollection CrearData()
        {
            EBalanceCollection vdaots = new EBalanceCollection();

            foreach (EBalance item in ListaSD)
            {
                foreach (EBalanceCtas item2 in ListaCD.Where(obe => obe.blgc_icod_balance == item.blgc_icod_balance).ToList())
                {
                    item.eBalanceCtas.Add(item2);
                }
                vdaots.Add(item);
            }
            return vdaots;
        }





        private void lblMonto_TextChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDecimal(GetCurrentColumnValue("Monto")) == 0) 
            //{
            //lblMonto.Text = "";
            //}


        }

        private void lblConcepto_TextChanged(object sender, EventArgs e)
        {
            //if (Convert.ToDecimal(GetCurrentColumnValue("Monto")) == 0)
            //{
            //    lblConcepto.Font = new Font("Courier New", 10, FontStyle.Bold);
            //}
            //else
            //{
            //    lblConcepto.Font = new Font("Courier New", 7);       
            //}
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
