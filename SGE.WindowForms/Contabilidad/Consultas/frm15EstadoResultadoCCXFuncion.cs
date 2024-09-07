using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class frm15EstadoResultadoCCXFuncion : DevExpress.XtraEditors.XtraForm
    {
        List<EEstadoGanPerFuncion> lstEstadoGanPer = new List<EEstadoGanPerFuncion>();
        public List<EEstadoGanPerCtas> Lista2 = new List<EEstadoGanPerCtas>();
        private BContabilidad obl = new BContabilidad();
        private string fecInicio;
        private string fecFin;
        public frm15EstadoResultadoCCXFuncion()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*Variables Monto Parcial*/
            decimal MontoParcial = 0;
            List<EEstadoGanPer> listaMontoParcial = new List<EEstadoGanPer>();
            /*Variables Monto Total*/
            decimal MontoTotal = 0;
            List<EEstadoGanPer> listaMontoTotal = new List<EEstadoGanPer>();
            if (lstEstadoGanPer.Count > 0)
            {
                lstEstadoGanPer.Clear();
            }
            List<EEstadoGanPerFuncion> Lista = new List<EEstadoGanPerFuncion>();
            Lista = new BContabilidad().ListarEstadoGanPerFuncion();
            foreach (var _bee in Lista)
            {
                EEstadoGanPerFuncion BEstadoGanPer = new EEstadoGanPerFuncion();
                BEstadoGanPer.egpfc_icod_estado_gan_per_funcion = _bee.egpfc_icod_estado_gan_per_funcion;
                BEstadoGanPer.egpfc_vlinea = _bee.egpfc_vlinea;
                BEstadoGanPer.egpfc_vconcepto = _bee.egpfc_vconcepto;
                Lista2 = obl.ListarEstadoGanPerCtasxIcodPosFinMontos(Convert.ToInt32(BEstadoGanPer.egpfc_icod_estado_gan_per_funcion), 0, Convert.ToInt32(lkpMes.EditValue), 2);
                foreach (var item in Lista2)
                {
                    //BEstadoGanPer.Monto = Lista2.Sum(xs => xs.MontosCC);
                    if (_bee.tablc_icod_signo_monto == 360)
                    {
                        if (item.MontosCC != 0)
                        {
                            BEstadoGanPer.Monto = BEstadoGanPer.Monto + (item.MontosCC * (-1));
                        }

                    }
                    if (_bee.tablc_icod_signo_monto == 362)
                    {
                        if (item.MontosCC != 0)
                        {
                            BEstadoGanPer.Monto = BEstadoGanPer.Monto + item.MontosCC;
                        }

                    }
                }
                #region Monto Parcial
                //EEstadoGanPer BMontoParcial = new EEstadoGanPer();
                //BMontoParcial.Monto = BEstadoGanPer.Monto;
                //listaMontoParcial.Add(BMontoParcial);
                //MontoParcial = listaMontoParcial.Sum(x => x.Monto);
                #endregion
                #region Monto Total
                EEstadoGanPer BMontoTotal = new EEstadoGanPer();
                BMontoTotal.Monto = BEstadoGanPer.Monto;
                listaMontoTotal.Add(BMontoTotal);
                MontoTotal = listaMontoTotal.Sum(x => x.Monto);
                #endregion
                if (_bee.tablc_icod_linea_registro == 358)
                {
                    BEstadoGanPer.Monto = MontoTotal;
                    //MontoParcial = 0;
                    //listaMontoParcial.Clear();

                }
                if (_bee.tablc_icod_linea_registro == 359)
                {
                    BEstadoGanPer.Monto = MontoTotal;
                    //MontoTotal = 0;
                    //listaMontoTotal.Clear();

                }
                lstEstadoGanPer.Add(BEstadoGanPer);
            }
            grd.DataSource = lstEstadoGanPer;
            grd.RefreshDataSource();

        }
        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEstadoGanPerFuncion Obe = (EEstadoGanPerFuncion)gv.GetRow(gv.FocusedRowHandle);
            using (frmEstadoGanPerCtaMontoSinCC frm = new frmEstadoGanPerCtaMontoSinCC())
            {
                frm.obePosFinan = Obe;
                frm.vcocc_fecha_vcontable = Convert.ToInt32(lkpMes.EditValue);
                frm.ShowDialog();
            }
        }
        private void frm13EstadoResultadoCC_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Mes; Mes = lkpMes.Text; int Año; Año = Parametros.intEjercicio; rptEstadoGGPP rpt = new rptEstadoGGPP(); rpt.cargar(lstEstadoGanPer, Mes, Año);
        }

        private void imprimirDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEstadoGanPerFuncion Obe = (EEstadoGanPerFuncion)gv.GetRow(gv.FocusedRowHandle);
            List<EEstadoGanPerCtasFuncion> Lista = new List<EEstadoGanPerCtasFuncion>();
            Lista = obl.ListarEstadoGanPerCtasxFechaContable(0, Convert.ToInt32(lkpMes.EditValue), 2);

            string Mes; Mes = lkpMes.Text; int Año;
            Año = Parametros.intEjercicio;
            rptEstadoGGPPDet rpt = new rptEstadoGGPPDet();
            rpt.cargar(Lista, lstEstadoGanPer, Mes, Año);
        }



    }
}