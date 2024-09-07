using DevExpress.XtraReports.UI;
using SGE.Entity;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Compras.Compras_Suministros
{
    public partial class rptOrdenCompra : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrdenCompra()
        {
            InitializeComponent();
        }
        public void cargar(EOrdenCompra _BE, List<EOrdenCompra> Lista, string total, List<ERegistroFirmas> lstFirmas)
        {
            string strTipoMoneda = "";
            string SimbTipoMoneda = "";
            string strTipoPago = "";

            string strfecha = "";
            strfecha = _BE.ococ_sfecha_orden_compra.ToString("MMMM dd DEL yyyy").ToUpper();

            if (_BE.tablc_iid_tipo_moneda == 3)
            {
                strTipoMoneda = "Soles";
                SimbTipoMoneda = "S./";
            }
            else
            {
                strTipoMoneda = "Dolares Americanos";
                SimbTipoMoneda = "$ ";
            }
            //-----------------------------////
            if (_BE.tablc_iforma_pago == 116)
            {
                strTipoPago = "CONTADO";
            }
            else if (_BE.tablc_iforma_pago == 117)
            {
                strTipoPago = "CREDITO";
            }
            else if (_BE.tablc_iforma_pago == 118)
            {
                strTipoPago = "LETRA";
            }
            else
            {
                strTipoPago = "OTROS";
            }

            lblOCL.Text = _BE.ococ_numero_orden_compra.Insert(4, "-");
            //xrlblEmpresa.Text = Valores.strNombreEmpresa+" - AÑO " + Parametros.intEjercicio;
            //xrlblTitulo1.Text = "IMPORTACIÓN Nº " + _BE.ococ_numero_orden_compra;
            //xrlblTitulo2.Text = "DEL " + String.Format("{0:MM/dd/yyyy}", _BE.ococ_sfecha_orden_compra);
            lblLima.Text = strfecha;
            lblProveedor.Text = _BE.proc_vnombrecompleto;
            //lblCuidad.Text = _BE..ToString();
            lblContacto.Text = _BE.ococ_vcontacto;
            lblCotizacion.Text = _BE.ococ_vcotizacion;
            //lblReferencia.Text = _BE.ococ_vrecepcion;
            lblRUC.Text = _BE.proc_vruc;
            lblTelefono.Text = _BE.proc_vtelefono;
            lblDireccion.Text = _BE.proc_vdireccion;
            lblfIGV.Text = Convert.ToInt32(_BE.ococ_npor_imp_igv).ToString() + " %";
            lblTipoMoneda.Text = strTipoMoneda;
            lblFechExpedicion.Text = String.Format("{0:dd/MM/yyyy}", _BE.ococ_sfecha_orden_compra);

            //------------------------------------//
            lblfGarantia.Text = _BE.ococ_vcomprobante;
            //lblfNota.Text = _BE.ococ_vnota_ocl;
            lblfLugarEntrega.Text = _BE.ococ_vlugar_entrega;
            lblfFormaPago.Text = strTipoPago;
            lblForm_Pago_Detall.Text = _BE.ococ_vforma_pago;

            this.DataSource = Lista;

            //GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("cpn_icod_concepto_nacional") });

            //lblrubros.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "cpn_vdescripcion_concepto_nacional", "")});
            //Enlaces con el Detalle

            lblmItem.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_iitem", "")});

            lblmCodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strCodigoProducto", "")});

            lblmDescripcion.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_vdescripcion", "{0:n2}")});

            lblmCaracteristicas.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_vcaracteristicas", "{0:n2}")});

            //lblmFechaEntrega.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "ocod_dfecha_entrega", "{0:dd/MM/yyyy}")});

            lblmUDM.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "strAbrevUniMed", "{0:n2}")});

            lblmCant.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_ncantidad", "{0:n2}")});

            lblmVrlUnit.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_ncunitario", "{0:n4}")});

            lblmDescto.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_ndescuento_item", "{0:n2}")});

            lblTotal.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", Lista, "ocod_nmonto_total", "{0:n2}")});

            //-------------------------------------------------------------------//

            //lblmSubTotal.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "ococ_nmonto_neto", "{0:n2}")});

            //lblIGV.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "ococ_nmonto_imp", "{0:n2}")});

            //lblmTotal.DataBindings.AddRange(new XRBinding[] {
            //new XRBinding("Text", Lista, "ococ_nmonto_total", "{0:n2}")});            

            lblmSubTotal.Text = String.Format("{0:n2}", _BE.ococ_nmonto_neto);
            lblIGV.Text = String.Format("{0:n2}", _BE.ococ_nmonto_imp);
            lblmTotal.Text = String.Format("{0:n2}", _BE.ococ_nmonto_total);
            lblmSon.Text = total + " " + strTipoMoneda;

            lblElaborado.Text = lstFirmas[0].regf_ocl_elavorado;
            lblAutoriza.Text = lstFirmas[0].regf_ocl_autorizado;
            //lblRevisado.Text = lstFirmas[0].regf_ocl_revisado;
            this.ShowPreview();
        }
    }
}
