using DevExpress.XtraReports.UI;
using SGE.Entity;
using System.Collections.Generic;

namespace SGE.WindowForms.Reportes.Produccion
{
    public partial class rptReporteProduccion : DevExpress.XtraReports.UI.XtraReport
    {
        List<MODELO> lista = new List<MODELO>();
        public rptReporteProduccion()
        {
            InitializeComponent();
        }


        internal void cargar(EOrdenProduccion obe)
        {
            GenerarLista(obe);
            this.DataSource = lista;
            lblTitulo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Titulo), "") });
            lblFichaTecnica.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.FichaTecnica), "") });
            lblOrdenProduccion.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.OrdenProduccion), "") });
            lblModelo.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Modelo), "") });
            lblColor.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Color), "") });
            lblMarca.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Marca), "") });
            lblSerie.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie), "") });
            lblSerie1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie1), "") });
            lblSerie2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie2), "") });
            lblSerie3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie3), "") });
            lblSerie4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie4), "") });
            lblSerie5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie5), "") });
            lblSerie6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie6), "") });
            lblSerie7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Serie7), "") });
            lblCantidad1.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad1), "") });
            lblCantidad2.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad2), "") });
            lblCantidad3.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad3), "") });
            lblCantidad4.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad4), "") });
            lblCantidad5.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad5), "") });
            lblCantidad6.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad6), "") });
            lblCantidad7.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Cantidad7), "") });
            lblTotal.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", lista, nameof(MODELO.Total), "") });


        }

        private void GenerarLista(EOrdenProduccion obe)
        {
            for (int i = 1; i <= 12; i++)
            {
                lista.Add(new MODELO()
                {
                    Titulo = nombre(i),
                    FichaTecnica = string.IsNullOrEmpty(obe.orprc_vficha_tecnica) ? "" : "FT - " + obe.orprc_vficha_tecnica,
                    Marca = obe.DesMarca.ToString(),
                    OrdenProduccion = obe.orprc_icod_orden_produccion,
                    Modelo = obe.DesModelo,
                    Color = obe.strcolor,
                    Serie = obe.orprc_iserie.ToString(),
                    Serie1 = obe.orprc_talla1.ToString(),
                    Serie2 = obe.orprc_talla2.ToString(),
                    Serie3 = obe.orprc_talla3.ToString(),
                    Serie4 = obe.orprc_talla4.ToString(),
                    Serie5 = obe.orprc_talla5.ToString(),
                    Serie6 = obe.orprc_talla6.ToString(),
                    Serie7 = obe.orprc_talla7.ToString(),
                    Cantidad1 = obe.orprc_cant_talla1.ToString(),
                    Cantidad2 = obe.orprc_cant_talla2.ToString(),
                    Cantidad3 = obe.orprc_cant_talla3.ToString(),
                    Cantidad4 = obe.orprc_cant_talla4.ToString(),
                    Cantidad5 = obe.orprc_cant_talla5.ToString(),
                    Cantidad6 = obe.orprc_cant_talla6.ToString(),
                    Cantidad7 = obe.orprc_cant_talla7.ToString(),
                    Total = obe.orprc_ntotal.ToString()
                });
            }
        }

        private string nombre(int index)
        {
            switch (index)
            {
                case 1: return "CORTADO";
                case 2: return "DESBASTADO";
                case 3: return "PINTADO";
                case 4: return "PERFILADO";
                case 5: return "TERMO";
                case 6: return "EMPASTADO";
                case 7: return "PRE. PLANTA";
                case 8: return "REARMADO";
                case 9: return "ALISTADO";
                default: return "0";
            }
        }

        class MODELO
        {
            public string Titulo { get; set; }
            public string FichaTecnica { get; set; }
            public string OrdenProduccion { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string Color { get; set; }
            public string Serie { get; set; }
            public string Serie1 { get; set; }
            public string Serie2 { get; set; }
            public string Serie3 { get; set; }
            public string Serie4 { get; set; }
            public string Serie5 { get; set; }
            public string Serie6 { get; set; }
            public string Serie7 { get; set; }
            public string Cantidad1 { get; set; }
            public string Cantidad2 { get; set; }
            public string Cantidad3 { get; set; }
            public string Cantidad4 { get; set; }
            public string Cantidad5 { get; set; }
            public string Cantidad6 { get; set; }
            public string Cantidad7 { get; set; }
            public string Total { get; set; }

        }
    }
}
