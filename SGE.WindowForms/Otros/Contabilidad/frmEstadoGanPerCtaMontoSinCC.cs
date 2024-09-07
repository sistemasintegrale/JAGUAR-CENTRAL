using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.WindowForms.Otros.Contabilidad
{
    public partial class frmEstadoGanPerCtaMontoSinCC : DevExpress.XtraEditors.XtraForm
    {
        #region Propiedades

        public EEstadoGanPerFuncion obePosFinan = new EEstadoGanPerFuncion();
        public List<EEstadoGanPerCtas> Lista = new List<EEstadoGanPerCtas>();
        private BContabilidad obl = new BContabilidad();
        List<EVoucherContableDet> lst02 = new List<EVoucherContableDet>();
        public int cecoc_icod_centro_costo = 0;
        public string cCostoInicio = "";
        public int vcocc_fecha_vcontable;

        #endregion

        public frmEstadoGanPerCtaMontoSinCC()
        {
            InitializeComponent();
        }

        private void frmPosFinanCtaCont_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void Cargar()
        {
            Lista = obl.ListarEstadoGanPerCtasxIcodPosFinMontos(Convert.ToInt32(obePosFinan.egpfc_icod_estado_gan_per_funcion), Convert.ToInt32(cecoc_icod_centro_costo), Convert.ToInt32(vcocc_fecha_vcontable), 2);
            grd.DataSource = Lista;
            /*Movimientos*/

            lst02 = new BContabilidad().listarMayorCCostoMensual_2(Parametros.intEjercicio, Convert.ToInt32(vcocc_fecha_vcontable), Convert.ToInt32(Lista[0].egpd_iid_cuenta_contable), Convert.ToInt32(Lista[0].egpd_iid_cuenta_contable), cCostoInicio, cCostoInicio, 1);




        }

        #region Mantenimiento

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var lstMovimientos = lst02.Where(x => x.ctacc_icod_cuenta_contable == Convert.ToInt32(Lista[0].egpd_iid_cuenta_contable) && x.cecoc_icod_centro_costo == cecoc_icod_centro_costo).ToList();

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
            int index = Lista.FindIndex(obe => obe.egpd_icod_ctas_estado_gan_per == Cab_icod_correlativo);
            gv.FocusedRowHandle = index;
        }


    }
}