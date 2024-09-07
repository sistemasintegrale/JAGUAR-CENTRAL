using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using System;
using System.Collections.Generic;

namespace SGE.WindowForms.Tesorería.Consultas
{
    public partial class Frm03RelacionChequesBancos : DevExpress.XtraEditors.XtraForm
    {
        private List<ELibroBancos> mlist = new List<ELibroBancos>();
        public Frm03RelacionChequesBancos()
        {
            InitializeComponent();
        }

        private void Frm03RelacionChequesBancos_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpBanco, new BTesoreria().listarBancos(), "bcoc_vnombre_banco", "bcoc_icod_banco", true);
            lkpBanco.ItemIndex = 0;
        }

        private void lkpCuentas_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpCuentas.EditValue != null)
            {
                carga();
            }
        }
        private void carga()
        {
            mlist = new BTesoreria().ListarMovimientoCuentasCheques(Parametros.intEjercicio, Convert.ToInt32(lkpCuentas.EditValue));
            dgrLibroBancos.DataSource = mlist;
        }

        private void lkpBanco_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpBanco.EditValue != null)
            {
                var lstCuentas = new BTesoreria().listarBancoCuentas(Convert.ToInt32(lkpBanco.EditValue));
                BSControls.LoaderLook(lkpCuentas, lstCuentas, "bcod_vnumero_cuenta", "bcod_icod_banco_cuenta", true);
                if (lstCuentas.Count == 0)
                {
                    mlist.Clear();
                    viewLibroBancos.RefreshData();
                }

            }
        }

        private void imprimirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mlist.Count > 0)
            {
                rpt03ChequesBancos rpt = new rpt03ChequesBancos();
                rpt.carga(mlist, lkpBanco.Text, lkpCuentas.Text);
            }
        }
    }
}