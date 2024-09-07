using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Ventas.Reporte
{
    public partial class rptFichaTecnicaPrueba : DevExpress.XtraReports.UI.XtraReport
    {
        public string ruta;
        public rptFichaTecnicaPrueba()
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
            List<string> observaciones = Obe.fitec_vobservaciones.Split('\n').ToList();
            int cont = 1;
            var obs = "";
            int count = observaciones.Count;
            foreach (var item in observaciones)
            {
                mlisdet[cont - 1].indicaciones = cont + "- " + item + ".";

                if (cont == count)
                {
                    obs = obs + cont + "- " + item.Insert(item.Length, ".") + "\n";
                }
                else
                    obs = obs + cont + "- " + item.Insert(item.Length - 1, ".") + "\n";
                cont++;
            }
            //lblObservaciones.Text = obs;
            if (Obe.fitec_vruta == "" || Obe.fitec_vruta == null)
            {
            }
            else
            {
                try
                {
                    var strrutas = new BAdministracionSistema().listarParametro();
                    string ruta = strrutas[0].DirecciónXML;

                }
                catch (Exception)
                {
                }
            }

            this.DataSource = mlisdet;

            lblNumero.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "numero", "")});

            lblArea.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strarea", "")});

            lblCodigoMat.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strubicacion", "")});

            lblMaterial.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strProducto", "")});

            lblPar.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "fited_ncantidad", "")});

            lblUM.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "strUnidadMedida", "")});

            lblIndicacionesdet.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", mlisdet, "indicaciones", "")});

            this.ShowPreview();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {




        }
    }
}
