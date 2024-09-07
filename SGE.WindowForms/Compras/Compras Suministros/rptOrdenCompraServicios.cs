using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class rptOrdenCompraServicios : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrdenCompraServicios()
        {
            InitializeComponent();
        }
        public void cargar(EOrdenCompraServicio _BE, List<EOrdenCompraServicio> Lista, string total, List<ERegistroFirmas> lstFirmas)
        {
            string strTipoMoneda = "";
            string SimbTipoMoneda = "";
            string strTipoPago = "";
            string strfecha = "";

            strfecha = _BE.ocsc_sfecha_ocs.ToString("MMMM dd DEL yyyy").ToUpper();
            if (_BE.tablc_iid_tipo_moneda == 3)
            {
                strTipoMoneda = "Soles";
                SimbTipoMoneda = "S/";
            }
            else if (_BE.tablc_iid_tipo_moneda == 338)
            {
                strTipoMoneda = "Euros";
                SimbTipoMoneda = "€ ";
            }

            else if (_BE.tablc_iid_tipo_moneda == 4)
            {
                strTipoMoneda = "Dolares";
                SimbTipoMoneda = "$ ";
            }
            //-----------------------------////
            //if (_BE.tablc_iid_tipo_moneda==116)
            //{
            //strTipoPago = "CONTADO";
            //}else if (_BE.tablc_iforma_pago==117)
            //{
            //strTipoPago = "CREDITO";
            //}else if (_BE.tablc_iforma_pago==118)
            //{
            //strTipoPago = "LETRA";
            //}else{
            //strTipoPago = "OTROS";
            //}
            lblLima.Text = strfecha;
            lblOCL.Text = _BE.ocsc_vnumero_ocs.Substring(4, 5) + "-" + _BE.ocsc_ianio;
            //xrlblEmpresa.Text = Valores.strNombreEmpresa+" - AÑO " + Parametros.intEjercicio;
            //xrlblTitulo1.Text = "IMPORTACIÓN Nº " + _BE.ococ_numero_orden_compra;
            //xrlblTitulo2.Text = "DEL " + String.Format("{0:MM/dd/yyyy}", _BE.ococ_sfecha_orden_compra);
            //lblLima.Text = _BE.ToString();
            lblProveedor.Text = _BE.proc_vnombrecompleto;
            lblCuidad.Text = _BE.ocsc_vciudad;
            lblContacto.Text = _BE.ocsc_vcontacto;

            lblReferencia.Text = _BE.ocsc_vreferncia;
            lblRUC.Text = _BE.proc_vruc;
            lblTelefono.Text = _BE.ocsc_vtelefono;
            lblDireccion.Text = _BE.proc_vdireccion;
            lblCotizacion.Text = _BE.ocsc_vcotizacion;
            lblTipoMoneda.Text = strTipoMoneda;
            lblFechExpedicion.Text = String.Format("{0:dd/MM/yyyy}", _BE.ocsc_sfecha_ocs);

            //------------------------------------//
            lblfIGV.Text = Convert.ToInt32(_BE.ocsc_npor_imp_igv).ToString() + " %";
            lblfGarantia.Text = _BE.ocsc_vgarantia;
            lblfNota.Text = _BE.ocsc_vnota_ocs;
            lblfLugarEntrega.Text = _BE.ocsc_vlugar_entrega;
            lblfFormaPago.Text = _BE.ocsc_vforma_pago;
            lblmSimTipoMoneda1.Text = SimbTipoMoneda;
            lblmSimTipoMoneda2.Text = SimbTipoMoneda;
            lblmSimTipoMoneda3.Text = SimbTipoMoneda;
            lblmSimTipoMoneda4.Text = SimbTipoMoneda;
            lblcostos.Text = _BE.cecoc_vcodigo_centro_costo;
            lblCostosDescripcion.Text = _BE.cecoc_vdescripcion;


            this.DataSource = Lista;



            lblmItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_iitem", "")});

            lblmCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_vcodigo_servicio_prov", "")});

            lblmDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_vdescripcion", "{0:n2}")});

            lblmCaracteristicas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_vcaracteristicas", "")});

            //lblmFechaEntrega.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "ocsd_sfecha_entrega", "{0:dd/MM/yyyy}")});

            lblmUDM.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strAbrevUniMed", "{0:n2}")});

            lblmCant.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_ncantidad", "{0:n2}")});

            lblmVrlUnit.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_ncunitaria", "{0:n2}")});

            lblmDescto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_ndescuento", "{0:n2}")});

            lblTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocsd_nvalor_total", "{0:n2}")});

            //-------------------------------------------------------------------//



            lblmSubTotal.Text = String.Format("{0:n2}", _BE.ocsc_nmonto_sub_total);
            lblIGV.Text = String.Format("{0:n2}", _BE.ocsc_nmonto_impuesto);
            lblmTotal.Text = String.Format("{0:n2}", _BE.ocsc_nmonto_total);
            lblmSon.Text = total + " " + strTipoMoneda;

            lblElaborado.Text = lstFirmas[0].regf_ocs_elavorado;
            lblRevisado.Text = lstFirmas[0].regf_ocs_revisado;

            this.ShowPreview();



        }
    }
}
