using DevExpress.XtraReports.UI;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    public partial class rptGRE : DevExpress.XtraReports.UI.XtraReport
    {
        public rptGRE()
        {
            InitializeComponent();
        }
        public void cargar(EGuiaRemision oBe, List<EGuiaRemisionDet> mlisdetalle)
        {
            this.DataSource = mlisdetalle;
            #region Cabecera de la Factura

            lblMotivo.Text = new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMotivoGuiaRemision).Where(x => x.tarec_iid_tabla_registro == oBe.tablc_iid_motivo).FirstOrDefault().tarec_vdescripcion;
            lblNroGuia.Text = "N° " + oBe.remic_vnumero_remision.Insert(4, "-");
            lblFechaEmision.Text = oBe.remic_sfecha_remision.ToString("dd/MM/yyyy");
            lblFechaInicioTraslado.Text = oBe.remic_sfecha_inicio.ToString("dd/MM/yyyy");
            lblPuntoPartida.Text = oBe.remic_vdireccion_procedencia;
            lblPuntoLLegada.Text = oBe.remic_vdireccion_destinatario;

            lbldestinatario.Text = oBe.remic_vnombre_destinatario;
            lblTransportista.Text = oBe.strTransportista;
            LBLRUCC.Text = oBe.remic_vruc_transportista;
            lblVehiMarcPlac.Text = oBe.remic_vmarca_placa;
            lblCertiInscripcion.Text = oBe.remic_vcertif_inscripcion;
            lblLicenConducir.Text = oBe.remic_vlicencia;
            lblRucDest.Text = oBe.remic_vruc_destinatario;



            if (oBe.tdocc_vabreviatura_tipo_doc == "FAV")
            {
                xrCheckBox18.Checked = true;
            }
            if (oBe.tdocc_vabreviatura_tipo_doc == "BOV")
            {
                xrCheckBox17.Checked = true;
            }
            lblNumCompF.Text = oBe.remic_num_comprobante;


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
            lblTipo.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "dremc_vdescripcion_prod", "")
            });
            lblRangoTallas.DataBindings.AddRange(new XRBinding[]
            {
            new XRBinding("Text", mlisdetalle , "TallaAcumulada", "")
            });

            PictureBoxQR.Image = Convertir.GenerarQR(oBe.LinkDescarga);

            this.ShowPreview();
        }

    }
}
