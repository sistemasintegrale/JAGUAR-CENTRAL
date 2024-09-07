using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmBalanceCtaContMonto : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EBalance obePosFinan = new EBalance();
        public List<EBalanceCtas> Lista = new List<EBalanceCtas>();
        private BContabilidad obl = new BContabilidad();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        public int cecoc_icod_centro_costo = 0;
        public string cCostoInicio = "";
        public int vcocc_fecha_vcontable;

        #endregion

        public frmBalanceCtaContMonto()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarBalanceCtasxIcodPosFinMontos(Convert.ToInt32(obePosFinan.blgc_icod_balance), Convert.ToInt32(vcocc_fecha_vcontable));
            grd.DataSource = Lista;
            /*Movimientos*/
            lst02 = new BContabilidad().listarMayorCCostoMensual_Balance(Parametros.intEjercicio, Convert.ToInt32(vcocc_fecha_vcontable), Convert.ToInt32(Lista[0].blgd_iid_cuenta_contable), Convert.ToInt32(Lista[0].blgd_iid_cuenta_contable), cCostoInicio, cCostoInicio, 2);

        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(Lista[0].blgd_iid_cuenta_contable) && x.cecoc_icod_centro_costo == cecoc_icod_centro_costo).ToList();

            FrmMayorCCostoMov frm = new FrmMayorCCostoMov();
            frm.mlist = lstMovimientos;
            //frm.Text = "Detalle del Centro de Costo " + cCostoInicio + " " + "Cuenta " + Obe.strNroCuenta;
            frm.Show();

        }



        #endregion

        private void btnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void Modify(int Cab_icod_correlativo)
        {
            Cargar();
            int index = Lista.FindIndex(obe => obe.blgd_icod_ctas_balance == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }


    }
}