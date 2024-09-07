using SGE.BusinessLogic;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Ventas
{
    public partial class FrmListarPedidoVenta : DevExpress.XtraEditors.XtraForm
    {
        private int xposition = 0;
        List<EPedidoClienCab> lstFacCompra = new List<EPedidoClienCab>();
        public EPedidoClienCab _Be { get; set; }

        public FrmListarPedidoVenta()
        {
            InitializeComponent();
        }




        private void DgAcept()
        {
            _Be = (EPedidoClienCab)viewDocCompra.GetRow(viewDocCompra.FocusedRowHandle);
            if (_Be != null)
                this.DialogResult = DialogResult.OK;
        }

        private void BuscarCriterio()
        {
            string cod = txtCodigo.Text, desc = txtRazon.Text;
            grdDocCompra.DataSource = lstFacCompra.Where(obe => ((cod != string.Empty) ? obe.lpedi_Numerolista.ToUpper().Contains(cod.ToUpper()) : obe.cliec_vnombre_cliente.ToUpper().Contains(desc.ToUpper()))).ToList();
        }

        private void FrmListarCliente_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            lstFacCompra = new BVentas().listarPedidoVenta();
            grdDocCompra.DataSource = lstFacCompra.ToList();
            viewDocCompra.Focus();
        }


        private void btnsalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DgAcept();
        }



        private void txtCodigo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCodigo.ContainsFocus)
            {
                txtRazon.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void txtRazon_EditValueChanged(object sender, EventArgs e)
        {
            if (txtRazon.ContainsFocus)
            {
                txtCodigo.Text = string.Empty;
                BuscarCriterio();
            }
        }

        private void viewDocCompra_DoubleClick(object sender, EventArgs e)
        {
            DgAcept();
        }
    }
}