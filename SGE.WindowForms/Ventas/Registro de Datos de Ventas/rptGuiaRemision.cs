using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class rptGuiaRemision : DevExpress.XtraReports.UI.XtraReport
    {
        public rptGuiaRemision()
        {
            InitializeComponent();
        }
        public void cargar(EGuiaRemision oBe, List<EGuiaRemisionDet> mlisdetalle, string Departamento, string Provincia, string Distrito,
            Boolean Ventas, Boolean Compras, Boolean Transformacion, Boolean Consignacion, Boolean Devolucion, Boolean Transferencia, Boolean TransladoEmision, Boolean Otros)
        {

            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura

            lblNroGuia.Text = oBe.remic_vnumero_remision;
            decimal Dia;
            decimal Mes;
            decimal Anio;
            lblDia.Text = (Convert.ToDecimal(oBe.remic_sfecha_remision.Day)).ToString();
            lblMes.Text = (Convert.ToDecimal(oBe.remic_sfecha_remision.Month)).ToString();
            lblAnio.Text = (Convert.ToDecimal(oBe.remic_sfecha_remision.Year)).ToString();

            decimal DiaT;
            decimal MesT;
            decimal AnioT;
            lblDiaT.Text = (Convert.ToDecimal(oBe.remic_sfecha_inicio.Day)).ToString();
            lblMesT.Text = (Convert.ToDecimal(oBe.remic_sfecha_inicio.Month)).ToString();
            lblAnioT.Text = (Convert.ToDecimal(oBe.remic_sfecha_inicio.Year)).ToString();

            int cant1 = oBe.remic_vdireccion_procedencia.Trim().Length;

            int cant11 = cant1 / 2;

            lblPuntoPartida1.Text = oBe.remic_vdireccion_procedencia.Substring(0, cant11);
            lblPuntoPartida2.Text = oBe.remic_vdireccion_procedencia.Substring(cant11, oBe.remic_vdireccion_procedencia.Length - cant11);

            if (oBe.remic_vdireccion_destinatario.Trim().Length < 50)
            {
                lblpuntollegada1.Text = oBe.remic_vdireccion_destinatario;
            }
            else
            {
                lblpuntollegada1.Text = oBe.remic_vdireccion_destinatario.Substring(0, 50);
            }


            int sobrante = oBe.remic_vdireccion_destinatario.Trim().Length - 50;
            if (sobrante > 0)
            {
                lblpuntollegada2.Text = oBe.remic_vdireccion_destinatario.Substring(sobrante, oBe.remic_vdireccion_destinatario.Length - sobrante);
            }





            //txtSerie.Text = oBe.remic_vnumero_remision.Substring(0, 3);
            //txtNumero.Text = oBe.remic_vnumero_remision.Substring(3, oBe.remic_vnumero_remision.Length - 3);


            //lblPuntoPartida1.Text = oBe.remic_vdireccion_procedencia;
            //lblpuntollegada1.Text = oBe.remic_vdireccion_destinatario;
            lblcodCliente.Text = oBe.cliec_vcod_cliente;
            lbldestinatario.Text = oBe.remic_vnombre_destinatario;
            lblTransportista.Text = oBe.strTransportista;
            LBLRUCC.Text = oBe.remic_vruc_transportista;
            lblVehiMarcPlac.Text = oBe.remic_vmarca_placa;
            lblCertiInscripcion.Text = oBe.remic_vcertif_inscripcion;
            lblLicenConducir.Text = oBe.remic_vlicencia;

            lblTipoCom.Text = oBe.tdocc_vabreviatura_tipo_doc;
            lblNumCompF.Text = oBe.remic_num_comprobante;

            if (oBe.tdocc_vabreviatura_tipo_doc == "FAV")
            {
                lblIdicadorF.Visible = true;
                lblNumCompF.Visible = true;
            }
            else
            {
                lblIndicadorB.Visible = true;
                lblNumCompB.Visible = true;
            }

            chkVentas.Checked = Ventas;
            chkCompras.Checked = Compras;
            chkTransBienesTransf.Checked = Transformacion;
            chkConsignacion.Checked = Consignacion;
            chkDevolucion.Checked = Devolucion;
            chkTransEntreEmpresa.Checked = Transferencia;
            chkTrnsCompPago.Checked = TransladoEmision;
            chkOtros.Checked = Otros;

            if (Ventas == true)
            {
                lblVentas.Visible = true;
            }
            else if (Compras == true)
            {
                lblCompras.Visible = true;
            }
            else if (Transformacion == true)
            {
                lblTransBienesTransf.Visible = true;
            }
            else if (Consignacion == true)
            {
                lblConsignacion.Visible = true;
            }
            else if (Devolucion == true)
            {
                lblDevolucion.Visible = true;
            }
            else if (Transferencia == true)
            {
                lblTransEntreEmpresa.Visible = true;
            }
            else if (TransladoEmision == true)
            {
                lblTrnsCompPago.Visible = true;
            }
            else if (Otros == true)
            {
                lblOtros.Visible = true;
            }
            lblTotalCajas.Text = Convert.ToInt32(oBe.remic_inum_cajas).ToString();
            lblTotalPares.Text = Convert.ToInt32(oBe.remic_itotal_pares).ToString();


            #endregion


            lblItem.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_icodigo", "")
            });
            lblCantidad.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_vcantidad_producto", "")
            });
            lblUnidad.DataBindings.AddRange(new XRBinding[]
           {
            new XRBinding("Text", mlisdetalle , "strAbreUM", "")
           });
            lblDescripccion.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_vdescripcion_prod", "")
            });
            lblRangoTallas.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "TallaAcumulada", "")
            });
            this.ShowPreview();


        }

        private void subRptAvanceVentasGiroXFechasResumen_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


    }
}
