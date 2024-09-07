using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListarDXPImpoDet : DevExpress.XtraEditors.XtraForm
    {
        List<EDXPImportacion> lstDXPImpDet = new List<EDXPImportacion>();
        public EDXPImportacion _Be { get; set; }
        public int IcodImpoDet = 0;
        public decimal MontoSoles = 0;
        public frmListarDXPImpoDet()
        {
            InitializeComponent();
        }

        private void frmAlamcen_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {

            lstDXPImpDet = new BCompras().listarDXPImpDet(IcodImpoDet);
            grdDXPImpDet.DataSource = lstDXPImpDet;
            viewDXPImpDet.Focus();
            TotalSoles();
            TotalDolares();


        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstDXPImpDet.Count > 0)
            {
                _Be = (EDXPImportacion)viewDXPImpDet.GetRow(viewDXPImpDet.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            //grdProyectos.DataSource = lstDXPImpDet.Where(x =>
            //                                       x.pryc_vcorrelativo.ToString().Contains(txtCodigo.Text.ToUpper()) &&
            //                                       x.pryc_vdescripcion.Contains(txtDescripcion.Text.ToUpper())
            //                                 ).ToList();
        }


        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            buscarCriterio();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            returnSeleccion();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void TotalSoles()
        {
            decimal TotalSoles = 0;
            decimal TotalSolesConvertir = 0;
            decimal Suma = 0;
            EDXPImportacion Obe1 = new EDXPImportacion();
            List<EDXPImportacion> lisImpo = new List<EDXPImportacion>();
            lstDXPImpDet.ForEach(x =>
            {
                if (x.tablc_iid_tipo_moneda == 4)
                {
                    TotalSolesConvertir = x.dxpd2_nmonto_importacion * x.doxpc_nmonto_tipo_cambio;
                    lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSolesConvertir });
                }
                else
                {
                    TotalSoles = x.dxpd2_nmonto_importacion;
                    lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSoles });
                }
                //Suma = lisImpo.Sum(xs => xs.dxpd2_nmonto_importacion);
                Suma = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc != 86 || xp.tdocc_icod_tipo_doc != 119).Sum(xs => xs.dxpd2_nmonto_importacion);

            });

            new BCompras().modificarImportacionConceptosDXPMontoSoles(Convert.ToInt32(IcodImpoDet), Suma);
            lstDXPImpDet = new BCompras().listarDXPImpDet(IcodImpoDet);
            grdDXPImpDet.DataSource = lstDXPImpDet;
            txtSoles.Text = Suma.ToString();
        }
        private void TotalDolares()
        {
            decimal TotalSoles = 0;
            decimal TotalSolesConvertir = 0;
            decimal Suma = 0;
            EDXPImportacion Obe1 = new EDXPImportacion();
            List<EDXPImportacion> lisImpo = new List<EDXPImportacion>();
            lstDXPImpDet.ForEach(x =>
            {
                if (x.tablc_iid_tipo_moneda == 3)
                {
                    TotalSolesConvertir = x.dxpd2_nmonto_importacion / x.doxpc_nmonto_tipo_cambio;
                    lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSolesConvertir });
                }
                else
                {
                    TotalSoles = x.dxpd2_nmonto_importacion;
                    lisImpo.Add(new EDXPImportacion { dxpd2_nmonto_importacion = TotalSoles });
                }
                //Suma = lisImpo.Sum(xs => xs.dxpd2_nmonto_importacion);
                Suma = lisImpo.Where(xp => xp.tdocc_icod_tipo_doc != 86 || xp.tdocc_icod_tipo_doc != 119).Sum(xs => xs.dxpd2_nmonto_importacion);

            });


            new BCompras().modificarImportacionConceptosDXPMontoDolares(Convert.ToInt32(IcodImpoDet), Suma);
            txtDolares.Text = Suma.ToString();

        }
    }
}