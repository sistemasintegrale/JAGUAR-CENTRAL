using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Contabilidad.Libros_Oficiales
{
    public partial class rptRegistroComprasSinQuiebre : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRegistroComprasSinQuiebre()
        {
            InitializeComponent();
        }

        private decimal[] sumPH = new decimal[12];
        private decimal[] sumPF = new decimal[12];
        private int printGroupF = 0;
        private int indPag = 0;

        public void Cargar(List<ERegistroCompras> Lista, string Mes)
        {
            lblEmpresa.Text = Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblRUC.Text = "RUC: " + Valores.strRUC;
            lblTitulo1.Text = "REGISTRO DE COMPRAS - " + Mes.ToUpper() + " DE " + Parametros.intEjercicio.ToString();
            this.DataSource = Lista;

            #region Detail

            lblCorrelativo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxpc_viid_correlativo", "")}); //id del documento

            lblFEmision.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_sfecha_doc", "")}); //fecha emisión

            lblFVencimiento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_sfecha_vencimiento_doc", "")}); //fecha vencimiento

            lblCoa.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_vcodigo_tipo_doc_sunat", "")}); //COA

            lblNumDoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxpc_vnumero_doc", "")}); //número documento

            lblTipoDocProv.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tip_doc_proveedor", "")}); // tipo de doc del proveedor

            lblDocId.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "num_doc_proveedor", "")}); //número doc del proveedor

            lblRazSoc.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "proc_vnombrecompleto", "")}); //razón social

            //MONTOS

            lblBIOpGrav.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_destino_gravado", "")});

            lblBIOpCom.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_destino_mixto", "")});

            lblOpDtNoGrv.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_destino_nogravado", "")});

            lblAdqNoGrav.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_nogravado", "")});

            lblCIF.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_referencial_cif", "")});

            lblIGVOG.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_imp_destino_gravado", "")});

            lblIGVOCom.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_imp_destino_mixto", "")});

            lblIGVDtONoG.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_imp_destino_nogravado", "")});

            //lblISC.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "str_doxpc_nmonto_isc", "")});

            //lblBImpIVAP.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "str_doxpc_nmonto_base_impon_ivap", "")});

            //lblIVAPC.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "str_doxpc_nmonto_ivap", "")});

            lblPreComp.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_total_documento", "")});

            //detracción
            lblDocDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxpc_vnro_deposito_detraccion", "")});
            lblFDet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_sfec_deposito_detraccion", "")});

            lblTC.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_tipo_cambio", "")});

            //N/C
            lblFModif.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nc_dxc_fecha", "")});
            lblTipoDoc_ref.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nc_dxc_tipodoc", "")});
            lblNDoc_ref.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "nc_dxc_numdoc", "")});

            #endregion

            #region FooterReport

            lblTotalT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_icod_tipo_doc", "")});


            lblBIOpGravTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_destino_gravado", "")});

            lblBIOpComTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_destino_mixto", "")});

            lblOpDtNoGrvTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_destino_nogravado", "")});

            lblAdqNoGravTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_nogravado", "")});

            lblCIFTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_referencial_cif", "")});

            lblIGVOGTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_imp_destino_gravado", "")});

            lblIGVOComTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_imp_destino_mixto", "")});

            lblIGVDtONoGTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_imp_destino_nogravado", "")});

            //lblISCTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "str_doxpc_nmonto_isc", "")});

            //lblBImpIVAPTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "str_doxpc_nmonto_base_impon_ivap", "")});

            //lblIVAPCTT.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "str_doxpc_nmonto_ivap", "")});

            lblPreCompTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_doxpc_nmonto_total_documento", "")});
            #endregion

            this.ShowPreview();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            sumPH[0] += (string.IsNullOrWhiteSpace(lblBIOpGrav.Text)) ? 0 : Convert.ToDecimal(lblBIOpGrav.Text);
            sumPH[1] += (string.IsNullOrWhiteSpace(lblBIOpCom.Text)) ? 0 : Convert.ToDecimal(lblBIOpCom.Text);
            sumPH[2] += (string.IsNullOrWhiteSpace(lblOpDtNoGrv.Text)) ? 0 : Convert.ToDecimal(lblOpDtNoGrv.Text);
            sumPH[3] += (string.IsNullOrWhiteSpace(lblAdqNoGrav.Text)) ? 0 : Convert.ToDecimal(lblAdqNoGrav.Text);
            sumPH[4] += (string.IsNullOrWhiteSpace(lblCIF.Text)) ? 0 : Convert.ToDecimal(lblCIF.Text);
            sumPH[5] += (string.IsNullOrWhiteSpace(lblIGVOG.Text)) ? 0 : Convert.ToDecimal(lblIGVOG.Text);
            sumPH[6] += (string.IsNullOrWhiteSpace(lblIGVOCom.Text)) ? 0 : Convert.ToDecimal(lblIGVOCom.Text);
            sumPH[7] += (string.IsNullOrWhiteSpace(lblIGVDtONoG.Text)) ? 0 : Convert.ToDecimal(lblIGVDtONoG.Text);
            //sumPH[8] += (string.IsNullOrWhiteSpace(lblISC.Text)) ? 0 : Convert.ToDecimal(lblISC.Text);
            //sumPH[9] += (string.IsNullOrWhiteSpace(lblBImpIVAP.Text)) ? 0 : Convert.ToDecimal(lblBImpIVAP.Text);
            //sumPH[10] += (string.IsNullOrWhiteSpace(lblIVAPC.Text)) ? 0 : Convert.ToDecimal(lblIVAPC.Text);
            sumPH[11] += (string.IsNullOrWhiteSpace(lblPreComp.Text)) ? 0 : Convert.ToDecimal(lblPreComp.Text);

            sumPF[0] += (string.IsNullOrWhiteSpace(lblBIOpGrav.Text)) ? 0 : Convert.ToDecimal(lblBIOpGrav.Text);
            sumPF[1] += (string.IsNullOrWhiteSpace(lblBIOpCom.Text)) ? 0 : Convert.ToDecimal(lblBIOpCom.Text);
            sumPF[2] += (string.IsNullOrWhiteSpace(lblOpDtNoGrv.Text)) ? 0 : Convert.ToDecimal(lblOpDtNoGrv.Text);
            sumPF[3] += (string.IsNullOrWhiteSpace(lblAdqNoGrav.Text)) ? 0 : Convert.ToDecimal(lblAdqNoGrav.Text);
            sumPF[4] += (string.IsNullOrWhiteSpace(lblCIF.Text)) ? 0 : Convert.ToDecimal(lblCIF.Text);
            sumPF[5] += (string.IsNullOrWhiteSpace(lblIGVOG.Text)) ? 0 : Convert.ToDecimal(lblIGVOG.Text);
            sumPF[6] += (string.IsNullOrWhiteSpace(lblIGVOCom.Text)) ? 0 : Convert.ToDecimal(lblIGVOCom.Text);
            sumPF[7] += (string.IsNullOrWhiteSpace(lblIGVDtONoG.Text)) ? 0 : Convert.ToDecimal(lblIGVDtONoG.Text);
            sumPF[8] += (string.IsNullOrWhiteSpace(lblIGVDtONoG.Text)) ? 0 : Convert.ToDecimal(lblIGVDtONoG.Text);
            //sumPF[9] += (string.IsNullOrWhiteSpace(lblBImpIVAP.Text)) ? 0 : Convert.ToDecimal(lblBImpIVAP.Text);
            //sumPF[10] += (string.IsNullOrWhiteSpace(lblIVAPC.Text)) ? 0 : Convert.ToDecimal(lblIVAPC.Text);
            sumPF[11] += (string.IsNullOrWhiteSpace(lblPreComp.Text)) ? 0 : Convert.ToDecimal(lblPreComp.Text);

        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblTotalT.Text))
            {
                lblBIOpGravTF.Text = sumPF[0].ToString();

                lblBIOpComTF.Text = sumPF[1].ToString();

                lblOpDtNoGrvTF.Text = sumPF[2].ToString();

                lblAdqNoGravTF.Text = sumPF[3].ToString();

                lblCIFTF.Text = sumPF[4].ToString();

                lblIGVOGTF.Text = sumPF[5].ToString();

                lblIGVOComTF.Text = sumPF[6].ToString();

                lblIGVDtONoGTF.Text = sumPF[7].ToString();

                //lblISCTF.Text = sumPF[8].ToString();

                //lblBImpIVAPTF.Text = sumPF[9].ToString();

                //lblIVAPCTF.Text = sumPF[10].ToString();

                lblPreCompTF.Text = sumPF[11].ToString();

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

                lblBIOpComTF.Text = "";

                lblOpDtNoGrvTF.Text = "";

                lblAdqNoGravTF.Text = "";

                lblCIFTF.Text = "";

                lblIGVOGTF.Text = "";

                lblIGVOComTF.Text = "";

                lblIGVDtONoGTF.Text = "";

                //lblISCTF.Text = "";

                //lblBImpIVAPTF.Text = "";

                //lblIVAPCTF.Text = "";

                lblPreCompTF.Text = "";

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
                lblBIOpGravTH.Text = sumPH[0].ToString();

                lblBIOpComTH.Text = sumPH[1].ToString();

                lblOpDtNoGrvTH.Text = sumPH[2].ToString();

                lblAdqNoGravTH.Text = sumPH[3].ToString();

                lblCIFTH.Text = sumPH[4].ToString();

                lblIGVOGTH.Text = sumPH[5].ToString();

                lblIGVOComTH.Text = sumPH[6].ToString();

                lblIGVDtONoGTH.Text = sumPH[7].ToString();

                //lblISCTH.Text = sumPH[8].ToString();

                //lblBImpIVAPTH.Text = sumPH[9].ToString();

                //lblIVAPCTH.Text = sumPH[10].ToString();

                lblPreCompTH.Text = sumPH[11].ToString();
            }
            indPag++;
        }
    }
}
