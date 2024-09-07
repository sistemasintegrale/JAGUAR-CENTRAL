using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;
using System.Drawing;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptFichaTecnicaPruebaAnterior : DevExpress.XtraReports.UI.XtraReport
    {
        public rptFichaTecnicaPruebaAnterior()
        {
            InitializeComponent();
        }
        public void cargar(EFichaTecnicaCab Obe, List<EFichaTecnicaDet> mlisdet)
        {
            /*-------------------------------------*/

            /*
             * Datos de cliente 
            */
            lblModelo.Text = Obe.strmodelo;
            lblColor.Text = Obe.strcolor;
            lblSerie.Text = Obe.strserie;
            lblLinea.Text = Obe.strlinea;
            lblTipoTrabajo.Text = Obe.strtipo_trabajo;
            lblTipo.Text = Obe.strtipo;
            /*
             * Datos de emisor
             */
            lblRuEmisor.Text = Valores.strRUC;
            lblNombreEmisor.Text = Valores.strNombreEmpresa;
            lblNroDocumento.Text = "FT" + "-" + Obe.fitec_icod_ficha_tecnica;
            lblObservaciones.Text = Obe.fitec_vobservaciones;

            if (Obe.fitec_vruta == "" || Obe.fitec_vruta == null)
            {

            }
            else
            {
                ptrimagen.Image = Image.FromFile(Obe.fitec_vruta);
            }

            this.DataSource = mlisdet;


            lblArea.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strarea", "")});

            lblCodigoMat.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strubicacion", "")});

            lblMaterial.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strProducto", "")});

            lblPar.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "fited_ncantidad", "")});

            lblUMedida.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strUnidadMedida", "")});

            this.ShowPreview();
        }


    }
}
