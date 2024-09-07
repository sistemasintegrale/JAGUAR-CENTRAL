using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class rptRegistroVentas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRegistroVentas()
        {
            InitializeComponent();
        }

        private decimal[] sumPH = new decimal[10];
        private decimal[] sumPF = new decimal[10];
        private int printGroupF = 0;
        private int indPag = 0;

        public void Cargar(List<ERegistroVentas> Lista, string Mes)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblRUC.Text = "RUC: " + Modules.Valores.strRUC;
            lblTitulo1.Text = "REGISTRO DE VENTAS - " + Mes.ToUpper() + " DE " + Parametros.intEjercicio.ToString();
            this.DataSource = Lista;
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("tdocc_icod_tipo_doc") });


            #region GroupHeader

            lblTipoDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_icod_tipo_doc", "")});

            #endregion

            #region Detail

            lblCorrelativo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_icod_correlativo", "")}); //id del documento

            lblFEmision.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_sfecha_doc", "")}); //fecha emisión

            lblFVencimiento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxcc_sfecha_vencimiento_doc", "")}); //fecha vencimiento

            lblCoa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_vcodigo_tipo_doc_sunat", "")}); //COA

            lblNumDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxcc_vnumero_doc", "")}); //número documento

            lblTipoDocCli.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tip_doc_cliente", "")}); // tipo de doc del cliente

            lblDocId.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "num_doc_cliente", "")}); //número doc del cliente

            lblRazSoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "cliec_vnombre_cliente", "")}); //razón social

            lblTC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_tipo_cambio", "")}); //tipo de cambio

            lblBIOpGrav.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_biog", "")}); //base imp. op. grav.

            //si tiene ivap el monto inafecto se coloca en base imponible ivap, si no en valor exonerado

            lblValEx.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_valor_ex", "")}); //valor exonerado

            //lblValEx.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "", "")}); //valor factoring PEND.*********************

            //lblValExp.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_valor_exp", "")}); //valor exportación

            //lblBIvap.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_ivap", "")}); //base imp. ivap

            lblValVenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_valor_venta", "")}); //valor venta

            lblIGV.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_monto_igv", "")}); //monto igv

            //lblISC.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_monto_isc", "")}); //monto isc

            //lblIVAP.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_monto_ivap", "")}); //monto ivap

            lblPrecVenta.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_precio_venta", "")}); //precio venta

            lblFModif.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nc_dxc_fecha", "")}); //fecha doc referencia nc

            lblTipoDoc_ref.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nc_dxc_tipodoc", "")}); //tipo doc ref nc

            lblNDoc_ref.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nc_dxc_numdoc", "")}); //num doc ref nc

            #endregion

            #region GroupFooter

            lblTotTipoDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_icod_tipo_doc", "")});

            lblVTipoDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_vdescripcion", "")});




            lblBIOpGravT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_biog", "")}); //base imp. op. grav.

            //si tiene ivap el monto inafecto se coloca en base imponible ivap, si no en valor exonerado

            lblValExT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_valor_ex", "")}); //valor exonerado

            //lblValEx.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "", "")}); //valor factoring PEND.*********************

            //lblValExpT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_valor_exp", "")}); //valor exportación

            //lblBIvapT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_ivap", "")}); //base imp. ivap

            lblValVentaT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_valor_venta", "")}); //valor venta

            lblIGVT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_monto_igv", "")}); //monto igv

            //lblISCT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_monto_isc", "")}); //monto isc

            //lblIVAPT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_monto_ivap", "")}); //monto ivap

            lblPrecVentaT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_precio_venta", "")}); //precio venta

            #endregion

            #region FooterReport

            lblTotalT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_icod_tipo_doc", "")}); //precio venta




            lblBIOpGravTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_biog", "")}); //base imp. op. grav.

            //si tiene ivap el monto inafecto se coloca en base imponible ivap, si no en valor exonerado

            lblValExTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_valor_ex", "")}); //valor exonerado

            //lblValEx.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "", "")}); //valor factoring PEND.*********************

            //lblValExpTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_valor_exp", "")}); //valor exportación

            //lblBIvapTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_ivap", "")}); //base imp. ivap

            lblValVentaTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_valor_venta", "")}); //valor venta

            lblIGVTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_monto_igv", "")}); //monto igv

            //lblISCTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_monto_isc", "")}); //monto isc

            //lblIVAPTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "rpt_monto_ivap", "")}); //monto ivap

            lblPrecVentaTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "rpt_precio_venta", "")}); //precio venta

            #endregion

            #region PageFooter

            //lblBIOpGravTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_gravado", "")}); //base imp. op. grav.

            ////si tiene ivap el monto inafecto se coloca en base imponible ivap, si no en valor exonerado

            //lblValExTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_exonerado_rpt", "")}); //valor exonerado

            ////lblValEx.DataBindings.AddRange(new XRBinding[] {
            ////new XRBinding("Text", Lista, "", "")}); //valor factoring PEND.*********************

            //lblValExpTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_exportacion", "")}); //valor exportación

            //lblBIvapTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_base_ivap_rpt", "")}); //base imp. ivap

            //lblValVentaTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "valor_venta", "")}); //valor venta

            //lblIGVTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_igv", "")}); //monto igv

            //lblISCTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_isc", "")}); //monto isc

            //lblIVAPTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "monto_iva", "")}); //monto ivap

            //lblPrecVentaTF.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "precio_venta", "")}); //precio venta
            #endregion

            this.ShowPreview();


        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            sumPH[0] += (string.IsNullOrWhiteSpace(lblBIOpGrav.Text)) ? 0 : Convert.ToDecimal(lblBIOpGrav.Text);
            sumPH[1] += (string.IsNullOrWhiteSpace(lblValEx.Text)) ? 0 : Convert.ToDecimal(lblValEx.Text);
            //sumPH[2] += (string.IsNullOrWhiteSpace(lblValFact.Text)) ? 0 : Convert.ToDecimal(lblValFact.Text);
            //sumPH[3] += (string.IsNullOrWhiteSpace(lblValExp.Text)) ? 0 : Convert.ToDecimal(lblValExp.Text);
            //sumPH[4] += (string.IsNullOrWhiteSpace(lblBIvap.Text)) ? 0 : Convert.ToDecimal(lblBIvap.Text);
            sumPH[5] += (string.IsNullOrWhiteSpace(lblValVenta.Text)) ? 0 : Convert.ToDecimal(lblValVenta.Text);
            sumPH[6] += (string.IsNullOrWhiteSpace(lblIGV.Text)) ? 0 : Convert.ToDecimal(lblIGV.Text);
            //sumPH[7] += (string.IsNullOrWhiteSpace(lblISC.Text)) ? 0 : Convert.ToDecimal(lblISC.Text);
            //sumPH[8] += (string.IsNullOrWhiteSpace(lblIVAP.Text)) ? 0 : Convert.ToDecimal(lblIVAP.Text);
            sumPH[9] += (string.IsNullOrWhiteSpace(lblPrecVenta.Text)) ? 0 : Convert.ToDecimal(lblPrecVenta.Text);

            sumPF[0] += (string.IsNullOrWhiteSpace(lblBIOpGrav.Text)) ? 0 : Convert.ToDecimal(lblBIOpGrav.Text);
            sumPF[1] += (string.IsNullOrWhiteSpace(lblValEx.Text)) ? 0 : Convert.ToDecimal(lblValEx.Text);
            //sumPF[2] += (string.IsNullOrWhiteSpace(lblValFact.Text)) ? 0 : Convert.ToDecimal(lblValFact.Text);
            //sumPF[3] += (string.IsNullOrWhiteSpace(lblValExp.Text)) ? 0 : Convert.ToDecimal(lblValExp.Text);
            //sumPF[4] += (string.IsNullOrWhiteSpace(lblBIvap.Text)) ? 0 : Convert.ToDecimal(lblBIvap.Text);
            sumPF[5] += (string.IsNullOrWhiteSpace(lblValVenta.Text)) ? 0 : Convert.ToDecimal(lblValVenta.Text);
            sumPF[6] += (string.IsNullOrWhiteSpace(lblIGV.Text)) ? 0 : Convert.ToDecimal(lblIGV.Text);
            //sumPF[7] += (string.IsNullOrWhiteSpace(lblISC.Text)) ? 0 : Convert.ToDecimal(lblISC.Text);
            //sumPF[8] += (string.IsNullOrWhiteSpace(lblIVAP.Text)) ? 0 : Convert.ToDecimal(lblIVAP.Text);
            sumPF[9] += (string.IsNullOrWhiteSpace(lblPrecVenta.Text)) ? 0 : Convert.ToDecimal(lblPrecVenta.Text);

        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblTotalT.Text))
            {
                lblBIOpGravTF.Text = sumPF[0].ToString("n2");

                lblValExTF.Text = sumPF[1].ToString("n2");

                //lblValFactTF.Text = sumPF[2].ToString("n2");

                //lblValExpTF.Text = sumPF[3].ToString("n2");

                //lblBIvapTF.Text = sumPF[4].ToString("n2");

                lblValVentaTF.Text = sumPF[5].ToString("n2");

                lblIGVTF.Text = sumPF[6].ToString("n2");

                //lblISCTF.Text = sumPF[7].ToString("n2");

                //lblIVAPTF.Text = sumPF[8].ToString("n2");

                lblPrecVentaTF.Text = sumPF[9].ToString("n2");

                string cdd = lblVTipoDoc.Text;

                if (printGroupF == 0)
                {
                    for (int i = 0; i < sumPF.Length; i++)
                    {
                        sumPF[i] = 0;
                    }
                }

                if (printGroupF == 1)
                {
                    printGroupF += 1;
                }
                else
                {
                    printGroupF = 0;
                }
            }
            else
            {
                lblVan.Text = "";

                lblBIOpGravTF.Text = "";

                lblValExTF.Text = "";

                //lblValFactTF.Text = "";

                //lblValExpTF.Text = "";

                //lblBIvapTF.Text = "";

                lblValVentaTF.Text = "";

                lblIGVTF.Text = "";

                //lblISCTF.Text = "";

                //lblIVAPTF.Text = "";

                lblPrecVentaTF.Text = "";

            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            printGroupF += 1;
        }

        private void lblVienen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (indPag == 2)
            {
                lblVienen.Text = "VIENEN...";
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (indPag != 0)
            {
                lblBIOpGravTH.Text = sumPH[0].ToString("n2");

                lblValExTH.Text = sumPH[1].ToString("n2");

                //lblValFactTH.Text = sumPH[2].ToString("n2");

                //lblValExpTH.Text = sumPH[3].ToString("n2");

                //lblBIvapTH.Text = sumPH[4].ToString("n2");

                lblValVentaTH.Text = sumPH[5].ToString("n2");

                lblIGVTH.Text = sumPH[6].ToString("n2");

                //lblISCTH.Text = sumPH[7].ToString("n2");

                //lblIVAPTH.Text = sumPH[8].ToString("n2");

                lblPrecVentaTH.Text = sumPH[9].ToString("n2");
            }
            indPag++;
        }
    }
}
