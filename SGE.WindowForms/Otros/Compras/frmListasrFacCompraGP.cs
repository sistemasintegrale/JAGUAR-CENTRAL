using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Compras
{
    public partial class frmListasrFacCompraGP : DevExpress.XtraEditors.XtraForm
    {
        List<EFacturaCompra> lstFactura = new List<EFacturaCompra>();
        public EFacturaCompra _Be { get; set; }
        public int proc_icod_proveedor = 0;
        public frmListasrFacCompraGP()
        {
            InitializeComponent();
        }


        public void cargar()
        {
            //lstFactura = new BCompras().listarFacCompra(Parametros.intEjercicio).Where(ob => ob.fcoc_iestado_recep == 273 & ob.fcoc_flag_importacion == true).ToList();
            lstFactura = new BCompras().listarFacCompra(Parametros.intEjercicio).Where(x => x.proc_icod_proveedor == proc_icod_proveedor).ToList();
            grdAlmacen.DataSource = lstFactura;
            viewAlmacen.Focus();
        }

        private void viewBanco_DoubleClick(object sender, EventArgs e)
        {
            returnSeleccion();
        }
        private void returnSeleccion()
        {
            if (lstFactura.Count > 0)
            {
                _Be = (EFacturaCompra)viewAlmacen.GetRow(viewAlmacen.FocusedRowHandle);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void buscarCriterio()
        {
            grdAlmacen.DataSource = lstFactura.Where(x =>
                                                   x.fcoc_icod_doc.ToString().Contains(txtCodigo.Text.ToUpper()) &&
                                                   x.strProveedor.Contains(txtDescripcion.Text.ToUpper())
                                             ).ToList();
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

        private void frmFacCompra2_Load_1(object sender, EventArgs e)
        {
            cargar();
        }
    }
}