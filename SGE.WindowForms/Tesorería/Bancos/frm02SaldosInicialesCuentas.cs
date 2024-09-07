using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Otros.Tesoreria.Bancos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGE.WindowForms.Tesorería.Bancos
{
    public partial class frm02SaldosInicialesCuentas : DevExpress.XtraEditors.XtraForm
    {
        List<EBancoCuenta> lstCuentas = new List<EBancoCuenta>();
        public frm02SaldosInicialesCuentas()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            lstCuentas = new BTesoreria().listarSaldoInicialBancoCuenta(Parametros.intEjercicio);
            grdBancoCuentas.DataSource = lstCuentas;
        }
        private void Frm02SaldosInicialesCuentas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            buscar();
        }

        private void buscar()
        {

        }

        private void registrar_monto()
        {
            EBancoCuenta Obe = (EBancoCuenta)viewBancoCuentas.GetRow(viewBancoCuentas.FocusedRowHandle);
            if (Obe != null)
            {
                frmManteSaldoInicialCuentas frm = new frmManteSaldoInicialCuentas();
                frm.Obe = Obe;
                int index = viewBancoCuentas.FocusedRowHandle;
                frm.SetInsert();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cargar();
                    viewBancoCuentas.FocusedRowHandle = index;
                    viewBancoCuentas.Focus();
                }
            }
        }
        private void view()
        {
            EBancoCuenta Obe = (EBancoCuenta)viewBancoCuentas.GetRow(viewBancoCuentas.FocusedRowHandle);
            if (Obe != null)
            {
                frmManteSaldoInicialCuentas frm = new frmManteSaldoInicialCuentas();
                frm.Obe = Obe;
                frm.SetCancel();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
        private void imprimir()
        {
            if (lstCuentas.Count > 0)
            {

            }
        }

        private void ResgistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registrar_monto();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        private void viewBancoCuentas_DoubleClick(object sender, EventArgs e)
        {
            view();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}