using DevExpress.XtraReports.UI;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System.Collections.Generic;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptFichaTecnicaPrueba2 : DevExpress.XtraReports.UI.XtraReport
    {
        public string ruta;
        public rptFichaTecnicaPrueba2()
        {
            InitializeComponent();
        }
        public void cargar(EFichaTecnicaCab Obe, List<EFichaTecnicaDet> mlisdet)
        {
            int contdet = 0;
            lblModelo.Text = Obe.strmodelo;
            lblColor.Text = Obe.strcolor;
            lblSerie.Text = Obe.strserie;
            lblLinea.Text = Obe.strlinea;
            lblTipoTrabajo.Text = Obe.strtipo_trabajo;
            lblTipo.Text = Obe.strtipo;
            lblEmpresa.Text = Valores.strNombreEmpresa;
            lblNroDocumento.Text = "FT" + " - " + Obe.fitec_icod_ficha_tecnica;
            mlisdet.ForEach(x =>
            {
                contdet++;
                x.numero = contdet;
                x.strProducto = x.strProducto == null ? x.fited_vdescripcion : x.strProducto;
            });

            lblobservaciones.Text = Obe.fitec_vobservaciones;

            ptrimagen.Image = Obe.imagen;


            GroupHeader1.GroupFields.AddRange(new GroupField[] { new GroupField("strareaG") });

            this.DataSource = mlisdet;

            lblNumero.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "numero", "") });
            lblArea.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strarea", "") });
            lblCodigoMat.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strubicacion", "") });
            lblMaterial.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strProducto", "") });
            lblPar.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "fited_ncantidad", "") });
            lblUM.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", mlisdet, "strUnidadMedida", "") });
            this.ShowPreview();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {




        }
    }
}
