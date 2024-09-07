using DevExpress.XtraReports.UI;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;
using System.Drawing;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptOrdenPedido : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrdenPedido()
        {
            InitializeComponent();
        }
        public void cargar(EOrdenPedidoCab Obe, List<EOrdenPedidoDet> mlisdet)
        {
            var lista = mlisdet;

            lista.ForEach(x =>
            {
                var imagen = WebDavTest.Get($"{Rutas.RutaImagenes}{Rutas.RutaOrdenPedido }{x.orpec_iid_orden_pedido}/", $"{x.orped_icod_item_orden_pedido}.png");
                if (!(imagen is null))
                    x.imagen2 = Image.FromStream(imagen);
                imagen = null;
            });

            lblCliente.Text = Obe.cliec_vnombre_cliente.ToUpper();
            lblFPedido.Text = Obe.orpec_sfecha_pedido.ToString();
            lblEntInicial.Text = Obe.orpec_sfecha_entrega_inicial.ToString();
            lblEntFinal.Text = Obe.orpec_sfecha_entrega_final.ToString();
            lblOCompra.Text = Obe.orpec_vnumero_OC;
            lblFPago.Text = Obe.orpec_vforma_pago;
            lblTransp.Text = Obe.tranc_vnombre_transportista;
            lblRuEmisor.Text = Valores.strRUC;
            lblNombreEmisor.Text = Valores.strNombreEmpresa;
            lblNroDocumento.Text = "P" + "-" + Obe.orpec_icod_orden_pedido;
            lblTotalDocena.Text = Obe.orpec_ntotal_docenas.ToString();
            lblTotal.Text = Obe.orpec_itotal_unidades.ToString();
            lblFichaTec.Angle = 90;
            lblResponsable.Angle = 90;
            lblMarca.Angle = 90;
            lblTipo.Angle = 90;

            this.DataSource = lista;
            xrImagen.DataBindings.AddRange(new XRBinding[] { new XRBinding("Image", mlisdet, "imagen2", "") });
            lblItem.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "Codigo", "") });
            lblFichaTec.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_vnum_ficha_tecnica", "") });
            lblSerie.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "Serie", "") });
            lblResponsable.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "NomVendedor", "") });
            lblcodigo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strmodelo", "") });
            lblColor.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "pr_viid_color", "") });
            lblMarca.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "pr_viid_marca", "") });
            lblTipo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "pr_viid_tipo", "") });
            lblCant1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla1", "") });
            lblCant2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla2", "") });
            lblCant3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla3", "") });
            lblCant4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla4", "") });
            lblCant5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla5", "") });
            lblCant6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla6", "") });
            lblCant7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_cant_talla7", "") });
            lblTotalPares.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_itotal_item", "") });
            lblDocenas.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "orped_ndocenas", "{0:n2}") });
            this.ShowPreview();
        }


    }
}
