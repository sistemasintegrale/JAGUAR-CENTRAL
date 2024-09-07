using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SGE.WindowForms.Contabilidad.Consultas
{
    public partial class frm13EstadoResultadoCC : DevExpress.XtraEditors.XtraForm
    {
        List<EEstadoGanPer> lstEstadoGanPer = new List<EEstadoGanPer>();
        public List<EEstadoGanPerCtas> Lista2 = new List<EEstadoGanPerCtas>();
        private BContabilidad obl = new BContabilidad();
        private string fecInicio;
        private string fecFin;
        public frm13EstadoResultadoCC()
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
            List<EEstadoGanPer> Lista = new List<EEstadoGanPer>();
            Lista = new BContabilidad().ListarEstadoGanPer();
            foreach (var _bee in Lista)
            {
                EEstadoGanPer BEstadoGanPer = new EEstadoGanPer();
                BEstadoGanPer.egpc_icod_estado_gan_per = _bee.egpc_icod_estado_gan_per;
                BEstadoGanPer.egpc_vlinea = _bee.egpc_vlinea;
                BEstadoGanPer.egpc_vconcepto = _bee.egpc_vconcepto;
                Lista2 = obl.ListarEstadoGanPerCtasxIcodPosFinMontos(Convert.ToInt32(BEstadoGanPer.egpc_icod_estado_gan_per), Convert.ToInt32(bteCCostoI.Tag), Convert.ToInt32(lkpMes.EditValue), 1);
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
                EEstadoGanPer BMontoParcial = new EEstadoGanPer();
                BMontoParcial.Monto = BEstadoGanPer.Monto;
                listaMontoParcial.Add(BMontoParcial);
                MontoParcial = listaMontoParcial.Sum(x => x.Monto);
                #endregion
                #region Monto Total
                EEstadoGanPer BMontoTotal = new EEstadoGanPer();
                BMontoTotal.Monto = BEstadoGanPer.Monto;
                listaMontoTotal.Add(BMontoTotal);
                MontoTotal = listaMontoTotal.Sum(x => x.Monto);
                #endregion
                if (_bee.tablc_icod_linea_registro == 358)
                {
                    BEstadoGanPer.Monto = MontoParcial;
                    MontoParcial = 0;
                    listaMontoParcial.Clear();

                }
                if (_bee.tablc_icod_linea_registro == 359)
                {
                    BEstadoGanPer.Monto = MontoTotal;
                    MontoTotal = 0;
                    listaMontoTotal.Clear();

                }
                lstEstadoGanPer.Add(BEstadoGanPer);
            }
            grd.DataSource = lstEstadoGanPer;
            grd.RefreshDataSource();

        }
        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEstadoGanPer Obe = (EEstadoGanPer)gv.GetRow(gv.FocusedRowHandle);
            using (frmEstadoGanPerCtaContMonto frm = new frmEstadoGanPerCtaContMonto())
            {
                frm.obePosFinan = Obe;
                frm.cecoc_icod_centro_costo = Convert.ToInt32(bteCCostoI.Tag);
                frm.vcocc_fecha_vcontable = Convert.ToInt32(lkpMes.EditValue);
                frm.cCostoInicio = bteCCostoI.Text;
                frm.ShowDialog();
            }
        }
        private void frm13EstadoResultadoCC_Load(object sender, EventArgs e)
        {
            BSControls.LoaderLook(lkpMes, new BGeneral().listarTablaRegistro(Parametros.intTipoTablaMeses).Where(x => x.tarec_icorrelativo_registro != 0 && x.tarec_icorrelativo_registro != 13).ToList(), "tarec_vdescripcion", "tarec_icorrelativo_registro", true);
            lkpMes.EditValue = DateTime.Now.Month;
        }
        private void bteCCostoI_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmListarCentroCosto frmCCosto = new frmListarCentroCosto();

            if (frmCCosto.ShowDialog() == DialogResult.OK)
            {
                bteCCostoI.Text = frmCCosto._Be.cecoc_vcodigo_centro_costo;
                bteCCostoI.Tag = frmCCosto._Be.cecoc_icod_centro_costo;

            }
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void grd_Click(object sender, EventArgs e)
        {

        }


    }
}