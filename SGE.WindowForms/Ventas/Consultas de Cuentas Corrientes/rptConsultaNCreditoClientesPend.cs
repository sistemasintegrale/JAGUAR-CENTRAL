using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class rptConsultaNCreditoClientesPend : DevExpress.XtraReports.UI.XtraReport
    {
        public rptConsultaNCreditoClientesPend()
        {
            InitializeComponent();
        }

        decimal[] totalSoles = new decimal[3];
        decimal[] totalDolares = new decimal[3];

        public void Cargar(List<ENotaCreditoCliente> Lista)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "NOTAS DE CRÉDITO PENDIENTES DEL AÑO - " + Parametros.intEjercicio.ToString();

            foreach (var item in Lista)
            {
                //calculando totales por tipo de moneda
                if (item.tablc_iid_tipo_moneda == Parametros.intSoles)
                {
                    totalSoles[0] += Convert.ToDecimal(item.doxcc_nmonto_total);
                    totalSoles[1] += Convert.ToDecimal(item.doxcc_nmonto_pagado);
                    totalSoles[2] += Convert.ToDecimal(item.doxcc_nmonto_saldo);
                }
                else
                {
                    totalDolares[0] += Convert.ToDecimal(item.doxcc_nmonto_total);
                    totalDolares[1] += Convert.ToDecimal(item.doxcc_nmonto_pagado);
                    totalDolares[2] += Convert.ToDecimal(item.doxcc_nmonto_saldo);
                }
            }
            this.DataSource = Lista;

            #region GroupHeader

            lblNumNCre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxcc_vnumero_doc", "")});

            lblSitNCre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tarec_vdescripcion", "")});

            lblFechaNCre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_sfecha_doc", "")});

            lblClienteNCre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cliec_vnombre_cliente", "")});

            lblMonedaNC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "simboloMoneda", "")});

            lblMTotalNC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_nmonto_total", "")});

            lblMPagadoNC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_nmonto_pagado", "")});

            lblSaldoNC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_nmonto_saldo", "")});

            #endregion

            this.ShowPreview();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (totalSoles[0] <= 0)
            {
                lblMonSTT.Visible = false;
                lblMTotalSTT.Visible = false;
                lblMPagSTT.Visible = false;
                lblMSaldoSTT.Visible = false;

                PointF pos = new PointF();
                pos.Y = lblMonSTT.LocationF.Y;

                pos.X = lblMonDTT.LocationF.X;
                lblMonDTT.LocationF = pos;
                pos.X = lblMTotalDTT.LocationF.X;
                lblMTotalDTT.LocationF = pos;
                pos.X = lblMPagDTT.LocationF.X;
                lblMPagDTT.LocationF = pos;
                pos.X = lblMSaldoDTT.LocationF.X;
                lblMSaldoDTT.LocationF = pos;

                pos.X = line1.LocationF.X;
                pos.Y = line1.LocationF.Y - 18;
                line1.LocationF = pos;
                pos.Y += 3;
                line2.LocationF = pos;

            }
            else
            {
                lblMTotalSTT.Text = totalSoles[0].ToString("N2");
                lblMPagSTT.Text = totalSoles[1].ToString("N2");
                lblMSaldoSTT.Text = totalSoles[2].ToString("N2");
            }

            if (totalDolares[0] <= 0)
            {
                lblMonDTT.Visible = false;
                lblMTotalDTT.Visible = false;
                lblMPagDTT.Visible = false;
                lblMSaldoDTT.Visible = false;

                PointF pos = new PointF();
                pos.X = line1.LocationF.X;
                pos.Y = line1.LocationF.Y - 18;
                line1.LocationF = pos;
                pos.Y += 3;
                line2.LocationF = pos;
            }
            else
            {
                lblMTotalDTT.Text = totalDolares[0].ToString("N2");
                lblMPagDTT.Text = totalDolares[1].ToString("N2");
                lblMSaldoDTT.Text = totalDolares[2].ToString("N2");
            }
        }


    }
}
