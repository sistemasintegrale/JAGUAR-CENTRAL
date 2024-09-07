using DevExpress.XtraEditors;
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
    public partial class frm14EstadoResultadoBalance : DevExpress.XtraEditors.XtraForm
    {
        List<EBalance> lstEstadoGanPer = new List<EBalance>();
        public List<EBalanceCtas> Lista2 = new List<EBalanceCtas>();
        private BContabilidad obl = new BContabilidad();
        private string fecInicio;
        private string fecFin;
        public frm14EstadoResultadoBalance()
        {
            InitializeComponent();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            /*Variables Monto Parcial*/
            decimal MontoParcial = 0;
            List<EBalance> listaMontoParcial = new List<EBalance>();
            /*Variables Monto Total*/
            decimal MontoTotal = 0;
            List<EBalance> listaMontoTotal = new List<EBalance>();
            /*Montos Totales Activo y Pasivo Matrimonio*/
            decimal MontoActivo = 0;
            decimal MontoPasMatri = 0;
            if (lstEstadoGanPer.Count > 0)
            {
                lstEstadoGanPer.Clear();
            }
            List<EBalance> Lista = new List<EBalance>();
            Lista = new BContabilidad().ListarBalance();
            /*Lista Para Convertir Segun Signo*/
            List<EBalanceCtas> ListaConvertirSignos = new List<EBalanceCtas>();
            foreach (var _bee in Lista)
            {

                EBalance BBalance = new EBalance();
                BBalance.tablc_icod_linea_registro = _bee.tablc_icod_linea_registro;
                BBalance.blgc_icod_balance = _bee.blgc_icod_balance;
                BBalance.blgc_vlinea = _bee.blgc_vlinea;
                BBalance.blgc_vconcepto = _bee.blgc_vconcepto;
                Lista2 = obl.ListarBalanceCtasxIcodPosFinMontos(Convert.ToInt32(BBalance.blgc_icod_balance), Convert.ToInt32(lkpMes.EditValue));
                foreach (var item in Lista2)
                {
                    if (_bee.tablc_icod_signo_monto == 360)
                    {
                        if (item.MontosCC != 0)
                        {
                            BBalance.Monto = BBalance.Monto + (item.MontosCC * (-1));
                        }

                    }
                    if (_bee.tablc_icod_signo_monto == 362)
                    {
                        if (item.MontosCC != 0)
                        {
                            BBalance.Monto = BBalance.Monto + item.MontosCC;
                        }

                    }
                }
                #region Monto Parcial
                EBalance BMontoParcial = new EBalance();
                BMontoParcial.Monto = BBalance.Monto;
                listaMontoParcial.Add(BMontoParcial);
                MontoParcial = listaMontoParcial.Sum(x => x.Monto);
                #endregion
                #region Monto Total
                EBalance BMontoTotal = new EBalance();
                BMontoTotal.Monto = BBalance.Monto;
                listaMontoTotal.Add(BMontoTotal);
                MontoTotal = listaMontoTotal.Sum(x => x.Monto);
                MontoPasMatri = MontoTotal;
                #endregion

                if (_bee.tablc_icod_linea_registro == 358)
                {
                    BBalance.Monto = MontoParcial;
                    MontoParcial = 0;
                    listaMontoParcial.Clear();

                }
                if (_bee.tablc_icod_linea_registro == 359)
                {
                    BBalance.Monto = MontoTotal;
                    MontoActivo = MontoTotal;
                    MontoTotal = 0;
                    listaMontoTotal.Clear();

                }
                if (_bee.blgc_vlinea == "230")
                {
                    BBalance.Monto = MontoActivo - MontoPasMatri;
                }
                if (_bee.blgc_vlinea == "240")
                {
                    decimal Ejercicio = 0;
                    Ejercicio = lstEstadoGanPer.Where(x => x.blgc_vlinea == "230").ToList().Sum(x => x.Monto);
                    BBalance.Monto = BBalance.Monto + Ejercicio;
                }
                if (_bee.blgc_vlinea == "250")
                {
                    decimal Ejercicio = 0;
                    Ejercicio = lstEstadoGanPer.Where(x => x.blgc_vlinea == "230").ToList().Sum(x => x.Monto);
                    BBalance.Monto = BBalance.Monto + Ejercicio;
                }
                lstEstadoGanPer.Add(BBalance);
            }
            grd.DataSource = lstEstadoGanPer;
            grd.RefreshDataSource();

        }
        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBalance Obe = (EBalance)gv.GetRow(gv.FocusedRowHandle);
            if (Obe.tablc_icod_linea_registro == 354 || Obe.tablc_icod_linea_registro == 355)
            {
                XtraMessageBox.Show("No Tiene Detalle");
            }
            else
            {
                using (frmBalanceCtaContMonto frm = new frmBalanceCtaContMonto())
                {
                    frm.obePosFinan = Obe;
                    frm.vcocc_fecha_vcontable = Convert.ToInt32(lkpMes.EditValue);
                    frm.ShowDialog();
                }
            }

        }



        private void frm14EstadoResultadoBalance_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Mes; Mes = lkpMes.Text; int Año; Año = Parametros.intEjercicio;
            rptBalanceGeneral rpt = new rptBalanceGeneral();
            rpt.cargar(lstEstadoGanPer, Mes, Año);
        }

        private void imprimirDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EBalance Obe = (EBalance)gv.GetRow(gv.FocusedRowHandle);
            List<EBalanceCtas> Lista = new List<EBalanceCtas>();
            Lista = obl.ListarBalanceCtasxFechaContable(Convert.ToInt32(lkpMes.EditValue));
            string Mes; Mes = lkpMes.Text; int Año; Año = Parametros.intEjercicio;
            rptBalanceGeneralConDetalle rpt = new rptBalanceGeneralConDetalle();
            rpt.cargar(Lista, lstEstadoGanPer, Mes, Año);
        }


    }
}