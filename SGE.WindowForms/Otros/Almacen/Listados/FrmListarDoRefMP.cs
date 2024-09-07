using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Otros.Almacen.Listados
{
    public partial class FrmListarDoRefMP : DevExpress.XtraEditors.XtraForm
    {
        public int? intIcodCliente = null;
        public bool bDocFacBol = true;
        public EFacturaCab EDocPorCobrar { get; set; }
        public int intOpcionPlanillaVenta = 0;
        public bool flag_multiple = false;
        public int TipoDoc = 0;
        public List<EFacturaCab> lstFacturas = new List<EFacturaCab>();
        public FrmListarDoRefMP()
        {
            InitializeComponent();
        }
        private void FrmListarDocxCobrar_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            lstFacturas = new BVentas().listarFacturaCab(Parametros.intEjercicio).Where(x => x.favc_tipo_factura == 2).ToList();
            grdDocxCobrar.DataSource = lstFacturas;

        }
        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void Acceptar()
        {
            EDocPorCobrar = (EFacturaCab)viewDocxCobrar.GetRow(viewDocxCobrar.FocusedRowHandle);

            if (flag_multiple)
            {
                txtNumero.Focus();
                viewDocxCobrar.MoveLast();
                viewDocxCobrar.MoveFirst();
            }

            if (EDocPorCobrar != null)
                this.DialogResult = DialogResult.OK;
        }
        private void viewDocxCobrar_DoubleClick(object sender, EventArgs e)
        {
            if (!flag_multiple)
                Acceptar();
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Acceptar();
        }

        private void txtNumero_EditValueChanged(object sender, EventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            grdDocxCobrar.DataSource = lstFacturas.Where(x => x.favc_vnumero_factura.Contains(txtNumero.Text)).ToList();
        }
    }
}