using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;
using System.Drawing;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptNotaPedido : XtraReport
    {
        public rptNotaPedido()
        {
            InitializeComponent();
        }
        public void cargar(ENotaPedidoCab Obe, List<ENotaPedidoDet> mlisdet)
        {
            mlisdet.ForEach(x =>
            {
                var imagen = WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaNotaPedido }{x.nopec_iid_nota_pedido}/", $"{x.noped_icod_item_nota_pedido}.png");
                if (!(imagen is null))
                    x.imagen2 = Image.FromStream(imagen);
                imagen = null;
            });
            lblCliente.Text = Obe.cliec_vnombre_cliente.ToUpper();
            lblFPedido.Text = Obe.nopec_sfecha_pedido.ToString();
            lblEntInicial.Text = Obe.nopec_sfecha_entrega_inicial.ToString();
            lblEntFinal.Text = Obe.nopec_sfecha_entrega_final.ToString();
            lblOCompra.Text = Obe.nopec_vnumero_OC;
            lblFPago.Text = Obe.nopec_vforma_pago;
            lblTransp.Text = Obe.tranc_vnombre_transportista;
            lblRuEmisor.Text = Valores.strRUC;
            lblNombreEmisor.Text = Valores.strNombreEmpresa;
            lblNroDocumento.Text = "P" + "-" + Obe.nopec_icod_nota_pedido;
            lblTotalDocena.Text = Obe.nopec_ntotal_docenas.ToString();
            lblTotal.Text = Obe.nopec_itotal_unidades.ToString();
            lblMarca.Angle = 90;
            lblTipo.Angle = 90;
            this.DataSource = mlisdet;
            xrImagen.DataBindings.AddRange(new XRBinding[] { new XRBinding("Image", mlisdet, "imagen2", "") });
            lblItem.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "Codigo", "") });
            lblSerie.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "Serie", "") });
            lblResponsable.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "NomVendedor", "") });
            lblcodigo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strmodelo", "") });
            lblColor.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "pr_viid_color", "") });
            lblMarca.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "pr_viid_marca", "") });
            lblTipo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "pr_viid_tipo", "") });
            lblCant1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla1", "") });
            lblCant2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla2", "") });
            lblCant3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla3", "") });
            lblCant4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla4", "") });
            lblCant5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla5", "") });
            lblCant6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla6", "") });
            lblCant7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_cant_talla7", "") });
            lblTotalPares.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_itotal_item", "") });
            lblDocenas.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "noped_ndocenas", "{0:n2}") });
            this.ShowPreview();
        }


    }
}
