using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System.Drawing.Printing;
using System.Data;
using System.Linq;

namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    public partial class rptControlCCostos : DevExpress.XtraReports.UI.XtraReport
    {
        
        int cont = 0;


        public rptControlCCostos()
        {
            InitializeComponent();
        }

        List<EPersonalCCostos> ListaCD;
        List<EPersonal> ListaSD;
        public void cargar(List<EPersonalCCostos> ListaCD, List<EPersonal> ListaSD)
        {
            this.ListaCD = ListaCD;
            this.ListaSD = ListaSD;
            xrlblEmpresa.Text = Valores.strNombreEmpresa;
            xrlblTitulo1.Text = "CENTRO DE COSTOS POR PERSONA";
            //xrlblTitulo2.Text = "EXPRESADO EN M.N.";
            this.DataSource = CrearData();
            DetailReport.DataMember = "EPersonalCCostosX";
            
          
               



            
                lblcodigo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "perc_iid_personal", "")});

                lblApllyNomb.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "ApellNomb", "")});

                lbldocumento.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "perc_vnum_doc", "")});


                lblfechaRegistro.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "perc_sfecha_registro", "{0:dd/MM/yy}")});

                lblCargo.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "strCargo", "")});

                lblArea.DataBindings.AddRange(new XRBinding[] {
            new XRBinding("Text", null, "strArea", "")});
                
                //----------------detalle
                lblAñoDetalle.DataBindings.Add(new XRBinding("Text", null, "EPersonalCCostosX.pccd_iaño", ""));
                lblMesDetalle.DataBindings.Add(new XRBinding("Text", null, "EPersonalCCostosX.strMes", ""));
                lblNumCCostosDet.DataBindings.Add(new XRBinding("Text", null, "EPersonalCCostosX.ccoc_numero_centro_costo", ""));
                lblDescripcionCCostosDet.DataBindings.Add(new XRBinding("Text", null, "EPersonalCCostosX.ccoc_vdescripcion_ccosto", ""));
                lblfechaDet.DataBindings.Add(new XRBinding("Text", null, "EPersonalCCostosX.pccd_sfecha", "{0:dd/MM/yy}"));    
            
       
            
            this.ShowPreview();
        }



        private EPersonalCollection CrearData()
        {
            EPersonalCollection vdaots = new EPersonalCollection();

            foreach (EPersonal item in ListaSD)
            {
                foreach (EPersonalCCostos item2 in ListaCD.Where(obe => obe.perc_icod_personal == item.perc_icod_personal).ToList())
                {
                    item.ePersonalCCostos.Add(item2);
                }
                vdaots.Add(item);
            }
            return vdaots;
        }
          
      

        
    }
}
