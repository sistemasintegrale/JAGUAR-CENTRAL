using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    public partial class rptSaldoInicialDocumentoCobrar : DevExpress.XtraReports.UI.XtraReport
    {
        public rptSaldoInicialDocumentoCobrar()
        {
            InitializeComponent();
        }

        public void cargar(List<EDocXCobrar> Lista, string strTitulo)
        {
            this.DataSource = Lista;
            lblEmpresa.Text = Valores.strNombreEmpresa + "- AÑO " + Parametros.intEjercicio;

            lblTitulo1.Text = strTitulo;
            lblTitulo2.Text = "";

            //Enlace al grupo Cliente
            //{0:dd/MM/yyyy}
            xrlDocumento.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "Abreviatura", "") });
            xrlNroDocumento.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_vnumero_doc", "") });
            xrlSituacion.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "Situacion", "") });
            xrlFecha.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_sfecha_doc", "{0:dd/MM/yyyy}") });
            xrlCodigo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "cliec_icod_cliente", "") });
            xrlCliente.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "cliec_vnombre_cliente", "") });
            xrlMoneda.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "SimboloMoneda", "") });
            xrlTc.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_tipo_cambio", "{0:n4}") });
            xrlPago.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "FormaPago", "") });
            xrlMAfecto.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_afecto", "{0:n}") });
            //xrlValorCif.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxpc_nmonto_referencial_cif", "{0:n}") });
            xrlMInafecto.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_inafecto", "{0:n}") });
            //xrlMServicio.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxpc_nmonto_servicio_no_domic", "{0:n}") });
            xrlValorVenta.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "ValorVenta", "{0:n}") });
            xrlMImpIgv.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_impuesto", "{0:n}") });
            xrlMTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_total", "{0:n}") });
            //xrlMontoISC.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxpc_nmonto_isc", "{0:n}") });
            xrlMPagado.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_pagado", "{0:n}") });
            xrlSaldo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_saldo", "{0:n}") });


            xrlMTotalTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_total", "{0:n}") });
            //xrlMontoISC.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxpc_nmonto_isc", "{0:n}") });
            xrlMPagadoTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_pagado", "{0:n}") });
            xrlSaldoTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_nmonto_saldo", "{0:n}") });


            xrlDescripcion.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", Lista, "doxcc_vobservaciones", "{0:n}") });

            this.ShowPreview();
        }
    }
}
