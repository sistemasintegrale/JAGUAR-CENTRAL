using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    public partial class rptConsultaNCreditoClientes : DevExpress.XtraReports.UI.XtraReport
    {
        public rptConsultaNCreditoClientes()
        {
            InitializeComponent();
        }

        decimal[] totalSoles = new decimal[3];
        decimal[] totalDolares = new decimal[3];

        public void Cargar(List<ENotaCreditoCliente> ListaNC, List<ENotaCreditoPago> ListaDxC)
        {
            lblEmpresa.Text = Modules.Valores.strNombreEmpresa + " - AÑO " + DateTime.Now.Year;
            lblTitulo1.Text = "NOTAS DE CRÉDITO DEL AÑO - " + Parametros.intEjercicio.ToString();

            List<ENotaCreditoPago> Lista = new List<ENotaCreditoPago>();
            ENotaCreditoPago obj;
            bool opc = false;

            foreach (var item in ListaNC)
            {
                opc = false;
                obj = new ENotaCreditoPago();
                foreach (var item2 in ListaDxC.Where(ob => ob.doxcc_icod_correlativo_nota_credito == item.doxcc_icod_correlativo))
                {
                    ////datos de la nota de crédito
                    //obj.doxcc_icod_nc = item.doxcc_icod_correlativo;
                    //obj.doxcc_vnumero_ncre = item.doxcc_vnumero_doc;
                    //obj.tarec_vdescripcion = item.tarec_vdescripcion;
                    //obj.str_doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yyyy");
                    //obj.cliec_vnombre_cliente = item.cliec_vnombre_cliente;
                    //obj.simboloMoneda = item.simboloMoneda;
                    //obj.str_doxcc_nmonto_total = Convert.ToDecimal(item.doxcc_nmonto_total).ToString("N2");
                    //obj.str_doxcc_nmonto_pagado = Convert.ToDecimal(item.doxcc_nmonto_pagado).ToString("N2");
                    //obj.str_doxcc_nmonto_saldo = Convert.ToDecimal(item.doxcc_nmonto_saldo).ToString("N2");


                    ////datos del pago
                    //obj.tdocc_vabreviatura_tipo_doc = item2.tdocc_vabreviatura_tipo_doc;
                    //obj.doxcc_vnumero_pago = item2.ncpac_vnumero_doc_nota_credito;
                    //obj.str_ncpac_sfecha_pago = Convert.ToDateTime(item2.ncpac_sfecha_pago).ToString("dd/MM/yyyy");
                    //obj.ncpac_vdescripcion = item2.ncpac_vdescripcion;
                    //Lista.Add(obj);
                    //opc = true;
                }

                if (!opc)
                {
                    ////datos de la nota de crédito
                    //obj.doxcc_icod_nc = item.doxcc_icod_correlativo;
                    //obj.doxcc_vnumero_ncre = item.doxcc_vnumero_doc;
                    //obj.tarec_vdescripcion = item.tarec_vdescripcion;
                    //obj.str_doxcc_sfecha_doc = Convert.ToDateTime(item.doxcc_sfecha_doc).ToString("dd/MM/yyyy");
                    //obj.cliec_vnombre_cliente = item.cliec_vnombre_cliente;
                    //obj.simboloMoneda = item.simboloMoneda;
                    //obj.str_doxcc_nmonto_total = Convert.ToDecimal(item.doxcc_nmonto_total).ToString("N2");
                    //obj.str_doxcc_nmonto_pagado = Convert.ToDecimal(item.doxcc_nmonto_pagado).ToString("N2");
                    //obj.str_doxcc_nmonto_saldo = Convert.ToDecimal(item.doxcc_nmonto_saldo).ToString("N2");
                    //Lista.Add(obj);
                }

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
            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("doxcc_icod_nc") });

            #region GroupHeader

            lblNumNCre.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxcc_vnumero_ncre", "")});

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

            #region Detail

            lblTDPag.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "tdocc_vabreviatura_tipo_doc", "")});

            lblNumDocPag.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxcc_vnumero_pago", "")});

            lblFechaPag.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "str_ncpac_sfecha_pago", "")});

            lblObsPag.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxcc_vobservaciones", "")});

            #endregion

            #region FooterReport

            lblDocTT.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "doxcc_vnumero_ncre", "")});

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
