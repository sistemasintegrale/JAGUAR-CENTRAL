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
    public partial class rptEstadoGGPPDet : DevExpress.XtraReports.UI.XtraReport
    {

        int cont = 0;


        public rptEstadoGGPPDet()
        {
            InitializeComponent();
        }
        List<EEstadoGanPerCtasFuncion> ListaCD;
        List<EEstadoGanPerFuncion> ListaSD;
        public void cargar(List<EEstadoGanPerCtasFuncion> ListaDetalle, List<EEstadoGanPerFuncion> Lista, string Mes, int Año)
        {
            this.ListaCD = ListaDetalle;
            this.ListaSD = Lista;
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "ESTADO DE GANANCIAS Y PERDIDAS (FUNCION)";
            xrlblTitulo2.Text = "AL MES DE " + Mes.ToUpper() + " DE " + Año + " - EN M.N.";
            this.DataSource = CrearData();
            DetailReport.DataMember = "EEstadoGanPerCtasC";





            lblConcepto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "egpfc_vconcepto", "")});

            lblMonto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "Monto", "{0:n2}")});

            lblTipoLinea.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "tablc_icod_linea_registro", "")});

            //-----Detalle
            lblCodigoDet.DataBindings.Add(new XRBinding("Text", null, "EEstadoGanPerCtasC.ctacc_numero_cuenta_contable"));
            lblConceptoDet.DataBindings.Add(new XRBinding("Text", null, "EEstadoGanPerCtasC.ctacc_nombre_descripcion"));
            lblMontoDet.DataBindings.Add(new XRBinding("Text", null, "EEstadoGanPerCtasC.MontosCC", "{0:n2}"));


            this.ShowPreview();
        }


        private EEstadoGanPerFuncionCollection CrearData()
        {
            EEstadoGanPerFuncionCollection vdaots = new EEstadoGanPerFuncionCollection();

            foreach (EEstadoGanPerFuncion item in ListaSD)
            {
                foreach (EEstadoGanPerCtasFuncion item2 in ListaCD.Where(obe => obe.egpfc_icod_estado_gan_per_funcion == item.egpfc_icod_estado_gan_per_funcion).ToList())
                {
                    item.eEstadoGanPerCtas.Add(item2);
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
            if (Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 356 && Convert.ToDecimal(GetCurrentColumnValue("Monto")) < 0)
            {
                Parentesis1.Visible = true;
                Parentesis2.Visible = true;
                lblMonto.Text = Math.Abs(Convert.ToDecimal(GetCurrentColumnValue("Monto"))).ToString("N2");
            }
            else
            {
                Parentesis1.Visible = false;
                Parentesis2.Visible = false;
            }



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

            }
            else
            {
                Linea2.Visible = false;
                Linea3.Visible = false;

            }

            if (Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 355 || Convert.ToInt32(GetCurrentColumnValue("tablc_icod_linea_registro")) == 354)
            {
                lblMonto.Text = "";
            }



        }
    }
}
